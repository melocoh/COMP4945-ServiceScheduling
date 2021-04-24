using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling_App;
using ServiceScheduling_App.Models;

namespace ServiceScheduling_App.Controllers
{

    // Contains information about a single service shift to be displayed on calendar
    public class ServiceCalShift
    {
        public string serviceTitle { get; set; }
        public string location { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public ServiceCalShift(string serviceTitle, string location, DateTime startDate, DateTime endDate)
        {
            this.serviceTitle = serviceTitle;
            this.location = location;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }

    // Custom class that makes rows distinct
    class DistinctItemComparer : IEqualityComparer<ServiceShiftType>
    {
        public bool Equals(ServiceShiftType x, ServiceShiftType y)
        {
            return x.id == y.id;
        }

        public int GetHashCode(ServiceShiftType obj)
        {
            return obj.id.GetHashCode();
        }
    }

    /************************** Melody's code for the form, but will merge with Eric's later **************************/
    public class ServiceShiftTypeController
    {
        public List<ServiceShiftType> serviceShiftTypeList;

        public List<ServiceShiftType> serviceIdTitleList;

        public List<int> servIdList;

        public List<string> servTitleList;

        public List<string> serLocationList;

        public List<DayOfWeek> dayOfWeekList;

        public List<TimeSpan> timeStartList;

        public List<TimeSpan> timeEndList;

        public List<string> startToEndTimeList;



        public ServiceShiftTypeController(AppContext context)
        {
            var query = context.ServiceShifts
            .Join(
            context.ServiceTypes,
            serviceShifts => serviceShifts.ServId,
            serviceTypes => serviceTypes.ServId,
            (serviceShift, serviceType) => new
            {
                Id = serviceShift.ServId,
                Title = serviceType.ServTitle,
                Location = serviceShift.SerLocation,
                DayOfTheWeek = serviceShift.DayOfWeek,
                StartTime = serviceShift.TimeStart,
                EndTime = serviceShift.TimeEnd
            }
            ).ToList();

            serviceShiftTypeList = new List<ServiceShiftType>();
            serviceIdTitleList = new List<ServiceShiftType>();
            serLocationList = new List<string>();
            dayOfWeekList = new List<DayOfWeek>();
            startToEndTimeList = new List<string>();

            for (int i = 0; i < query.Count; i++)
            {
                // calls all parameter constructor
                ServiceShiftType serviceShiftType = new ServiceShiftType(query[i].Id, query[i].Title, query[i].Location, query[i].DayOfTheWeek, query[i].StartTime, query[i].EndTime);
                // calls two parameter constructor
                ServiceShiftType serviceIdTitle = new ServiceShiftType(query[i].Id, query[i].Title);

                // populates all the lists
                serviceIdTitleList.Add(serviceIdTitle);
                serviceShiftTypeList.Add(serviceShiftType);
                serLocationList.Add(serviceShiftType.location);
                dayOfWeekList.Add(serviceShiftType.dayOfWeek);
                startToEndTimeList.Add(serviceShiftType.startToEndTime);
            }

            // ServiceShiftType list that holds every element
            serviceShiftTypeList = serviceShiftTypeList.Distinct().ToList();

            // ServiceShiftType list that holds id and serTitle
            // uses custom Distinct method
            serviceIdTitleList = serviceIdTitleList.Distinct(new DistinctItemComparer()).ToList();

            // string list that holds location
            serLocationList = serLocationList.Distinct().ToList();

            // DayOfWeek list that holds days
            dayOfWeekList = dayOfWeekList.Distinct().ToList();

            // string list that holds startTime - endTime
            startToEndTimeList = startToEndTimeList.Distinct().ToList();
        }
    }
    /***************************************************************************************************/


    public class ServiceShiftsController : Controller
    {
        private readonly AppContext _context;

        public ServiceShiftsController(AppContext context)
        {
            ViewBag.ShowLogOut = true;
            _context = context;
        }


        // Get the first ServiceShiftType
        private ServiceShiftType GetServiceShiftTypeDetails()
        {

            var query = _context.ServiceShifts
            .Join(
            _context.ServiceTypes,
            serviceShifts => serviceShifts.ServId,
            serviceTypes => serviceTypes.ServId,
            (serviceShift, serviceType) => new
            {
                Id = serviceShift.ServId,
                Title = serviceType.ServTitle,
                Location = serviceShift.SerLocation,
                DayOfTheWeek = serviceShift.DayOfWeek,
                StartTime = serviceShift.TimeStart,
                EndTime = serviceShift.TimeEnd

            }
            ).ToList();

            ServiceShiftType serviceAppointment = new ServiceShiftType(query[0].Id, query[0].Title, query[0].Location, query[0].DayOfTheWeek, query[0].StartTime, query[0].EndTime);

            return serviceAppointment;
        }

        // Get the full list of ServiceShiftTypes
        private List<ServiceShiftType> GetServiceShiftTypeDetailsList()
        {

            var query = _context.ServiceShifts
            .Join(
            _context.ServiceTypes,
            serviceShifts => serviceShifts.ServId,
            serviceTypes => serviceTypes.ServId,
            (serviceShift, serviceType) => new
            {
                Id = serviceShift.ServId,
                Title = serviceType.ServTitle,
                Location = serviceShift.SerLocation,
                DayOfTheWeek = serviceShift.DayOfWeek,
                StartTime = serviceShift.TimeStart,
                EndTime = serviceShift.TimeEnd

            }
            ).ToList();

            List<ServiceShiftType> serviceAppointmentList = new List<ServiceShiftType>();
            for (int i = 0; i < query.Count; i++)
                serviceAppointmentList.Add(new ServiceShiftType(query[i].Id, query[i].Title, query[i].Location, query[i].DayOfTheWeek, query[i].StartTime, query[i].EndTime));

            return serviceAppointmentList;
        }

        // Returns a list of service information that contains:
        // Service name
        // Location
        // Start DateTime
        // End DateTime
        // It is filtered by Employee ID
        public string FilterServiceShiftsById(int id)
        {
            // list of service shifts
            List<ServiceCalShift> servShifts = new List<ServiceCalShift>();
            List<ServiceShiftType> serviceShifts = GetServiceShiftTypeDetailsList();

            for (int i = 0; i < serviceShifts.Count; i++)
            {
                if (serviceShifts[i].id == id)
                {
                    string service = serviceShifts[i].servTitle;
                    string location = serviceShifts[i].location;
                    DateTime baseDate = getBaseDateFromDay(serviceShifts[i].dayOfWeek);
                    DateTime start = baseDate.Add(serviceShifts[i].startTime);
                    DateTime end = baseDate.Add(serviceShifts[i].endTime);
                    servShifts.Add(new ServiceCalShift(service, location, start, end));
                }
            }
            var serializer = JsonSerializer.Serialize(servShifts);

            return serializer;
        }

        public DateTime getBaseDateFromDay(DayOfWeek day)
        {
            DateTime dt = DateTime.Now;
            int diff = dt.DayOfWeek - day;
            return dt.AddDays(-1 * diff).Date;
        }

        /************************************** Select Lists for Form **************************************/

        // Converts List to SelectListItems
        // @returns a list with the Text = service titles and Value = service Id
        private List<SelectListItem> GetSerTitleList()
        {
            ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

            List<SelectListItem> list = serviceShiftTypeControllerObj.serviceIdTitleList.ConvertAll<SelectListItem>(item =>
            {
                return new SelectListItem()
                {
                    Text = item.servTitle,
                    Value = item.id.ToString(),
                    Selected = false
                };
            });

            return list;
        }

        // Converts List to SelectListItems
        // @returns a list of service locations
        private List<SelectListItem> GetSerLocationList()
        {
            ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

            List<SelectListItem> list = serviceShiftTypeControllerObj.serLocationList.ConvertAll<SelectListItem>(item =>
            {
                return new SelectListItem()
                {
                    Text = item,
                    Value = item,
                    Selected = false
                };
            });

            return list;
        }

        // Converts List to SelectListItems
        // @returns a list of service days of the week
        private List<SelectListItem> GetSerDayOfWeek()
        {
            ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

            List<SelectListItem> list = serviceShiftTypeControllerObj.dayOfWeekList.ConvertAll<SelectListItem>(item =>
            {
                return new SelectListItem()
                {
                    Text = item.ToString(),
                    Value = item.ToString(),
                    Selected = false
                };
            });

            return list;
        }

        // Converts List to SelectListItems
        // @returns a list of service start and end time
        private List<SelectListItem> GetSerStartEndTime()
        {
            ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

            List<SelectListItem> list = serviceShiftTypeControllerObj.startToEndTimeList.ConvertAll<SelectListItem>(item =>
            {
                return new SelectListItem()
                {
                    Text = item.ToString(),
                    Value = item.ToString(),
                    Selected = false
                };
            });

            return list;
        }

        /**************************************************************************************************************/

        // GET: ServiceShifts
        public async Task<IActionResult> Index()
        {
            //var appContext = _context.ServiceShifts.Include(s => s.ServiceType);
            //return View(await appContext.ToListAsync());
            ViewBag.ServiceTypes = GetSerTitleList();
            return View();
        }

        // GET: ServiceShifts/:id
        public async Task<IActionResult> GetServiceShifts(int id)
        {
            var res = FilterServiceShiftsById(id);
            return Ok(res);
        }

        // GET: ServiceShifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceShift = await _context.ServiceShifts
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.ServiceShiftId == id);
            if (serviceShift == null)
            {
                return NotFound();
            }

            return View(serviceShift);
        }

        // GET: ServiceShifts/Create
        public IActionResult Create()
        {
            ViewData["ServId"] = new SelectList(_context.ServiceTypes, "ServId", "ServId");

            // service title viewbag
            ViewBag.SerTitle = GetSerTitleList();

            // service location viewbag
            ViewBag.SerLocation = GetSerLocationList();

            // service day of week viewbag
            ViewBag.SerDayOfWeek = GetSerDayOfWeek();

            // service start and end time viewbag
            ViewBag.SerStartEndTime = GetSerStartEndTime();

            return View();
        }

        // POST: ServiceShifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceShiftId,ServId,DayOfWeek,TimeStart,TimeEnd,SerLocation")] ServiceShift serviceShift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceShift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServId"] = new SelectList(_context.ServiceTypes, "ServId", "ServId", serviceShift.ServId);
            return View(serviceShift);
        }

        // GET: ServiceShifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceShift = await _context.ServiceShifts.FindAsync(id);
            if (serviceShift == null)
            {
                return NotFound();
            }
            ViewData["ServId"] = new SelectList(_context.ServiceTypes, "ServId", "ServId", serviceShift.ServId);
            return View(serviceShift);
        }

        // POST: ServiceShifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceShiftId,ServId,DayOfWeek,TimeStart,TimeEnd,SerLocation")] ServiceShift serviceShift)
        {
            if (id != serviceShift.ServiceShiftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceShift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceShiftExists(serviceShift.ServiceShiftId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServId"] = new SelectList(_context.ServiceTypes, "ServId", "ServId", serviceShift.ServId);
            return View(serviceShift);
        }

        // GET: ServiceShifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceShift = await _context.ServiceShifts
                .Include(s => s.ServiceType)
                .FirstOrDefaultAsync(m => m.ServiceShiftId == id);
            if (serviceShift == null)
            {
                return NotFound();
            }

            return View(serviceShift);
        }

        // POST: ServiceShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceShift = await _context.ServiceShifts.FindAsync(id);
            _context.ServiceShifts.Remove(serviceShift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceShiftExists(int id)
        {
            return _context.ServiceShifts.Any(e => e.ServiceShiftId == id);
        }
    }
}
