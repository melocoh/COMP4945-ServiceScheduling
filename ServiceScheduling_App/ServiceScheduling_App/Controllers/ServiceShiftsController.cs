using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling_App;
using ServiceScheduling_App.Models;

namespace ServiceScheduling_App.Controllers
{

    public class ServiceShiftType
    {
        public int id;
        public string servTitle;
        public string location;
        public DayOfWeek dayOfWeek;
        public TimeSpan startTime;
        public TimeSpan endTime;

        public ServiceShiftType(int id, string servTitle, string location, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            this.id = id;
            this.servTitle = servTitle;
            this.location = location;
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }

    public class ServiceShiftTypeController
    {
        public List<ServiceShiftType> serviceAppointmentList;

        public List<int> servIdList;

        public List<string> servTitleList;

        public List<string> serLocationList;

        public List<DayOfWeek> dayOfWeekList;

        public List<TimeSpan> timeStartList;

        public List<TimeSpan> timeEndList;

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


            serviceAppointmentList = new List<ServiceShiftType>();
            servIdList = new List<int>();
            servTitleList = new List<string>();
            serLocationList = new List<string>();
            dayOfWeekList = new List<DayOfWeek>();
            timeStartList = new List<TimeSpan>();
            timeEndList = new List<TimeSpan>();

            for (int i = 0; i < query.Count; i++)
            {
                serviceAppointmentList.Add(new ServiceShiftType(query[i].Id, query[i].Title, query[i].Location, query[i].DayOfTheWeek, query[i].StartTime, query[i].EndTime));
                servIdList.Add(query[i].Id);
                servTitleList.Add(query[i].Title);
                serLocationList.Add(query[i].Location);
                dayOfWeekList.Add(query[i].DayOfTheWeek);
                timeStartList.Add(query[i].StartTime);
                timeEndList.Add(query[i].EndTime);
            }

        }
    }

    public class ServiceShiftsController : Controller
    {
        private readonly AppContext _context;

        public ServiceShiftsController(AppContext context)
        {
            _context = context;

        }

        // GET: ServiceShifts
        public async Task<IActionResult> Index()
        {
            var appContext = _context.ServiceShifts.Include(s => s.ServiceType);
            return View(await appContext.ToListAsync());
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

        // Converts List to SelectListItems
        // @returns a list of service titles
        private List<SelectListItem> GetSerTitleList()
        {
            ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

            List<SelectListItem> list = serviceShiftTypeControllerObj.servTitleList.ConvertAll<SelectListItem>(item =>
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

        //// Converts List to SelectListItems
        //// @returns a list of service start and end time
        //private List<SelectListItem> GetSerStartEndTime()
        //{
        //    ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

        //    List<SelectListItem> list = serviceShiftTypeControllerObj.dayOfWeekList.ConvertAll<SelectListItem>(item =>
        //    {
        //        return new SelectListItem()
        //        {
        //            Text = item.ToString(),
        //            Value = item.ToString(),
        //            Selected = false
        //        };
        //    });

        //    return list;
        //}

        //Converts List to SelectListItems
        // @returns a list of service start time
        private List<SelectListItem> GetSerStartTime()
        {
            ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

            List<SelectListItem> list = serviceShiftTypeControllerObj.timeEndList.ConvertAll<SelectListItem>(item =>
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

        //Converts List to SelectListItems
        // @returns a list of service end time
        private List<SelectListItem> GetSerEndTime()
        {
            ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

            List<SelectListItem> list = serviceShiftTypeControllerObj.timeStartList.ConvertAll<SelectListItem>(item =>
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
            //ViewBag.SerStartEndTime = GetSerStartEndTime();

            // service start and end time viewbag
            ViewBag.SerStartTime = GetSerStartTime();

            // service start and end time viewbag
            ViewBag.SerEndTime = GetSerEndTime();

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
