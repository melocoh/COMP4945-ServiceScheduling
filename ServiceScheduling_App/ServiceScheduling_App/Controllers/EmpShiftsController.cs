using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Web.Script.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Controllers
{

    /****************************************************** Objects for storing data from queries ***********************************************************/

    // ServiceShiftType
    // object that contains elements from ServiceShift and ServiceType
    // @usage form input prototype
    public class ServiceShiftType
    {
        public int id;
        public string servTitle;
        public string location;
        public DayOfWeek dayOfWeek;
        public TimeSpan startTime;
        public TimeSpan endTime;
        public string startToEndTime;

        public ServiceShiftType(int id, string servTitle, string location, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            this.id = id;
            this.servTitle = servTitle;
            this.location = location;
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
            this.startToEndTime = startTime.Hours.ToString("D2") + ":" + startTime.Minutes.ToString("D2") + " - " + endTime.Hours.ToString("D2") + ":" + endTime.Minutes.ToString("D2");
        }

        public ServiceShiftType(int id, string servTitle)
        {
            this.id = id;
            this.servTitle = servTitle;
        }
    }

    // EmployeeService
    // object that contains elements from ServiceShift, ServiceType, EmpShift, and Employee
    // @usage form input
    public class EmployeeService
    {
        public int serId;
        public string servTitle;
        public int maxNoEmp;
        public int serviceShiftId;
        public DayOfWeek dayOfWeek;
        public string location;
        public TimeSpan startTime;
        public TimeSpan endTime;
        public int empId;
        public string fullName;
        public string startToEndTime;

        public EmployeeService(int serId, string servTitle, int maxNoEmp, int serviceShiftId, DayOfWeek dayOfWeek, string location, TimeSpan startTime, TimeSpan endTime, int empId, string fullName)
        {
            this.serId = serId;
            this.servTitle = servTitle;
            this.maxNoEmp = maxNoEmp;
            this.serviceShiftId = serviceShiftId;
            this.dayOfWeek = dayOfWeek;
            this.location = location;
            this.startTime = startTime;
            this.endTime = endTime;
            this.empId = empId;
            this.fullName = fullName;
            this.startToEndTime = startTime.Hours.ToString("D2") + ":" + startTime.Minutes.ToString("D2") + " - " + endTime.Hours.ToString("D2") + ":" + endTime.Minutes.ToString("D2");
        }

        public EmployeeService(int serId, string servTitle, string location, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            this.serId = serId;
            this.servTitle = servTitle;
            this.location = location;
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
            this.startToEndTime = startTime.Hours.ToString("D2") + ":" + startTime.Minutes.ToString("D2") + " - " + endTime.Hours.ToString("D2") + ":" + endTime.Minutes.ToString("D2");
        }

        public EmployeeService(int serId, string servTitle)
        {
            this.serId = serId;
            this.servTitle = servTitle;
        }
    }

    //contains information about a weekly session for employees
    public class EmployeeServiceInfo
    {
        public int numberOfEmployees { get; set; }
        public int numberOfMaxEmployees { get; set; }
        public List<string> employeeNames { get; set; }
        public int serviceShiftId { get; set; }

        public EmployeeServiceInfo(int numberOfMaxEmployees, int serviceShiftId)
        {
            this.numberOfEmployees = 0;
            this.numberOfMaxEmployees = numberOfMaxEmployees;
            this.serviceShiftId = serviceShiftId;
            employeeNames = new List<string>();
        }

        public bool addEmployee(string newEmployeeName)
        {
            if (employeeNames.Count < numberOfMaxEmployees)
            {
                employeeNames.Add(newEmployeeName);
                numberOfEmployees++;

                return true;
            }
            else
            {
                return false;
            }
        }
    }

    // Contains information about a single shift for employees to be displayed on calendar
    public class EmployeeCalShift
    {
        public string serviceTitle { get; set; }
        public string location { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }

        public EmployeeCalShift(string serviceTitle, string location, DateTime startDate, DateTime endDate)
        {
            this.serviceTitle = serviceTitle;
            this.location = location;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }

        /*************************************************** Query executing objects ********************************************************/

        public class EmployeeServiceControl
    {
        // ServiceShiftType list that holds every element
        public List<EmployeeService> employeeServiceList;

        // ServiceShiftType list that holds id and serTitle
        public List<EmployeeService> serviceIdTitleList;

        // string list that holds location
        public List<string> serLocationList;

        // DayOfWeek list that holds days
        public List<DayOfWeek> dayOfWeekList;

        // string list that holds startTime - endTime
        public List<string> startToEndTimeList;

        //load all the data from tables into this object
        public EmployeeServiceControl(AppContext context)
        {
            GetServiceEmployee(context);
        }

        // Joins all the queries together
        public void GetServiceEmployee(AppContext context)
        {
            /* 
             * LINQ query that joins:
             * - ServiceType
             * - ServiceShift
             * - EmpShift
             * - Employee
             */
            var query = context.ServiceShifts
            .Join(
            context.ServiceTypes,
            serviceShifts => serviceShifts.ServId,
            serviceTypes => serviceTypes.ServId,
            (serviceShift, serviceType) => new
            {
                serviceShift,
                serviceType

            }
            ).Join(context.EmpShifts,
            combinedEntry => combinedEntry.serviceShift.ServiceShiftId,
            empShifts => empShifts.ServiceShiftId,
            (combinedEntry, empShifts) => new
            {
                combinedEntry,
                empShifts
            }
            ).Join(context.Employees,
            combinedEntry2 => combinedEntry2.empShifts.EmpId,
            employees => employees.EmpId,
            (combinedEntry2, employees) => new
            {
                serId = combinedEntry2.combinedEntry.serviceShift.ServId,
                servTitle = combinedEntry2.combinedEntry.serviceType.ServTitle,
                maxNoEmp = combinedEntry2.combinedEntry.serviceType.MaxNoEmp,
                serviceShiftId = combinedEntry2.empShifts.ServiceShiftId,
                dayOfWeek = combinedEntry2.empShifts.ServiceShift.DayOfWeek,
                location = combinedEntry2.empShifts.ServiceShift.SerLocation,
                startTime = combinedEntry2.empShifts.ServiceShift.TimeStart,
                endTime = combinedEntry2.empShifts.ServiceShift.TimeEnd,
                empId = employees.EmpId,
                fullName = employees.FullName
            }).ToList();

            // instantiates all lists
            employeeServiceList = new List<EmployeeService>();
            serviceIdTitleList = new List<EmployeeService>();
            serLocationList = new List<string>();
            dayOfWeekList = new List<DayOfWeek>();
            startToEndTimeList = new List<string>();

            for (int i = 0; i < query.Count; i++)
            {
                // calls all / two parameter constructor
                EmployeeService employeeService = new EmployeeService(query[i].serId, query[i].servTitle, query[i].maxNoEmp, 
                    query[i].serviceShiftId, query[i].dayOfWeek, query[i].location, query[i].startTime, query[i].endTime, 
                    query[i].empId, query[i].fullName);
                EmployeeService serviceIdTitle = new EmployeeService(query[i].serId, query[i].servTitle);

                // populates all lists
                serviceIdTitleList.Add(employeeService);
                employeeServiceList.Add(employeeService);
                serLocationList.Add(employeeService.location);
                dayOfWeekList.Add(employeeService.dayOfWeek);
                startToEndTimeList.Add(employeeService.startToEndTime);
            }

            // ensures distinct data in the list
            employeeServiceList = employeeServiceList.Distinct().ToList();
            serviceIdTitleList = serviceIdTitleList.Distinct(new DistinctItemComparer2()).ToList(); // custom Distinct because there are different type elements
        }

        //returns a list of all services
        public List<string> getAllServices()
        {
            List<string> allServices = new List<string>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                allServices.Add(employeeServiceList[i].servTitle);
            }

            return allServices;
        }

        //returns a list of all services locations filtered by their title
        public List<string> FilterLocations(string servTitle)
        {
            List<string> filteredLocations = new List<string>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if (employeeServiceList[i].servTitle == servTitle)
                {
                    filteredLocations.Add(employeeServiceList[i].location);
                }
            }

            return filteredLocations.Distinct().ToList();
        }

        //returns a list of all services day of the week filtered by their title and location
        public List<DayOfWeek> FilterDayOfTheWeek(string servTitle, string location)
        {
            List<DayOfWeek> filteredDayOfTheWeek = new List<DayOfWeek>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if ((employeeServiceList[i].servTitle == servTitle) && (employeeServiceList[i].location == location))
                {
                    filteredDayOfTheWeek.Add(employeeServiceList[i].dayOfWeek);
                }
            }

            return filteredDayOfTheWeek.Distinct().ToList();
        }

        //returns a list of all services (start + end time) filtered by their title, location, and day of the week
        public List<string> FilterStartAndEndTime(string servTitle, string location, string dayOfWeek)
        {
            List<string> filteredStartAndEndTime = new List<string>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if ((employeeServiceList[i].servTitle == servTitle) && (employeeServiceList[i].location == location) && (employeeServiceList[i].dayOfWeek.ToString() == dayOfWeek))
                {
                    string startTime = employeeServiceList[i].startTime.Hours.ToString("D2") + ":" + employeeServiceList[i].startTime.Minutes.ToString("D2");
                    string endTime = employeeServiceList[i].endTime.Hours.ToString("D2") + ":" + employeeServiceList[i].endTime.Minutes.ToString("D2");

                    filteredStartAndEndTime.Add(startTime + " - " + endTime);
                }
            }

            return filteredStartAndEndTime.Distinct().ToList();
        }


        //returns a list of service information that contains:
        //serviceShiftId
        //numberOfEmployees
        //numberOfMaxEmployees
        //List<string> employeeNames
        //it is filtered by a service title, location, day of the week, and (start + end time)
        public string FilterAvailableShifts(string servTitle, string location, string dayOfWeek, string startToEndTime)
        {
            // list of available
            List<EmployeeServiceInfo> availableShifts = new List<EmployeeServiceInfo>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                string startTime = employeeServiceList[i].startTime.Hours.ToString("D2") + ":" + employeeServiceList[i].startTime.Minutes.ToString("D2");
                string endTime = employeeServiceList[i].endTime.Hours.ToString("D2") + ":" + employeeServiceList[i].endTime.Minutes.ToString("D2");
                string combinedTime = startTime + " - " + endTime;
                if ((employeeServiceList[i].servTitle == servTitle) && (employeeServiceList[i].location == location) && (employeeServiceList[i].dayOfWeek.ToString() == dayOfWeek) && (combinedTime == startToEndTime))
                {
                    int currentSerShiftID = employeeServiceList[i].serviceShiftId;
                    int currentSerShiftMaxEmployees = employeeServiceList[i].maxNoEmp;
                    string currentSerShiftEmployeeName = employeeServiceList[i].fullName;
                    bool addedEmployeeToList = false;
                    for (int f = 0; f < availableShifts.Count; f++)
                    {
                        if (currentSerShiftID == availableShifts[f].serviceShiftId)
                        {
                            //session already exists so add to it
                            availableShifts[f].addEmployee(currentSerShiftEmployeeName);
                            addedEmployeeToList = true;
                            break;
                        }
                    }

                    //session doesn't exist so create it
                    if (!addedEmployeeToList)
                    {
                        EmployeeServiceInfo newEmployeeServiceInfo = new EmployeeServiceInfo(currentSerShiftMaxEmployees, currentSerShiftID);
                        newEmployeeServiceInfo.addEmployee(currentSerShiftEmployeeName);
                        availableShifts.Add(newEmployeeServiceInfo);
                    }
                }
            }

            var serializer = JsonSerializer.Serialize(availableShifts);

            return serializer;
        }

        // Returns a list of service information that contains:
        // Service name
        // Location
        // Start DateTime
        // End DateTime
        // It is filtered by Employee ID
        public string FilterEmployeeShifts(int empId)
        {
            // list of employee shifts
            List<EmployeeCalShift> empShifts = new List<EmployeeCalShift>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if (employeeServiceList[i].empId == empId)
                {
                    string service = employeeServiceList[i].servTitle;
                    string location = employeeServiceList[i].location;
                    DateTime baseDate = getBaseDateFromDay(employeeServiceList[i].dayOfWeek);
                    DateTime start = baseDate.Add(employeeServiceList[i].startTime);
                    DateTime end = baseDate.Add(employeeServiceList[i].endTime);
                    empShifts.Add(new EmployeeCalShift(service, location, start, end));
                }
            }
            var serializer = JsonSerializer.Serialize(empShifts);

            return serializer;
        }

        public DateTime getBaseDateFromDay(DayOfWeek day)
        {
            DateTime dt = DateTime.Now;
            int diff = dt.DayOfWeek - day;
            return dt.AddDays(-1 * diff).Date;
        }
    }


    // Custom class that makes rows distinct
    class DistinctItemComparer2 : IEqualityComparer<EmployeeService>
    {
        public bool Equals(EmployeeService x, EmployeeService y)
        {
            return x.serId == y.serId;
        }

        public int GetHashCode(EmployeeService obj)
        {
            return obj.serId.GetHashCode();
        }
    }

    /*************************************************** EmpShiftsController ********************************************************/


    public class EmpShiftsController : Controller
    {
        private readonly AppContext _context;

        public EmployeeServiceControl employeeServiceControl;

        // Constructor
        public EmpShiftsController(AppContext context)
        {
            ViewBag.ShowLogOut = true;
            _context = context;

            employeeServiceControl = new EmployeeServiceControl(_context);
        }

        //// Converts List to SelectListItems
        //// @returns a list with the Text = service titles and Value = service Id
        private List<SelectListItem> GetSerTitleList()
        {

            List<SelectListItem> list = employeeServiceControl.serviceIdTitleList.ConvertAll<SelectListItem>(item =>
            {
                return new SelectListItem()
                {
                    Text = item.servTitle,
                    Value = item.serId.ToString(),
                    Selected = false
                };
            });
            return list;
        }

        // GET: EmpShifts
        public async Task<IActionResult> Index()
        {
            ViewBag.ShowLogOut = true;

            int? id = HttpContext.Session.GetInt32("empID");
            if (id == null)
            {
                return RedirectToAction("NotAuthorized","Home");
            }
            ViewBag.accountID = id;
            ViewBag.accountName = _context.Employees.Where(emp => emp.EmpId == id).FirstOrDefault().FullName;

            // INCLUDE AND WHERE QUERY for FINAL Q3
            var appContext = _context.EmpShifts.Include(e => e.Employee).Include(e => e.ServiceShift).Include(e => e.ServiceShift.ServiceType).Where(e => e.EmpId == id);
            return View(await appContext.ToListAsync());
        }

        // GET: EmpShifts/GetCalendarShifts
        [HttpGet]
        public ActionResult GetCalendarShifts()
        {
            ViewBag.ShowLogOut = true;

            int? id = HttpContext.Session.GetInt32("empID");
            if (id == null)
            {
                return RedirectToAction("RoleSelection", "Home");
            }

            // Switch commented res to view employee shifts from logged in user
            // or Nicki Minaj respectively
            var res = employeeServiceControl.FilterEmployeeShifts((int)id);
            //var res = employeeServiceControl.FilterEmployeeShifts(4);

            return Ok(res);
        }

        // GET: EmpShifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.ShowLogOut = true;

            if (id == null)
            {
                return NotFound();
            }

            var empShift = await _context.EmpShifts
                .Include(e => e.Employee)
                .Include(e => e.ServiceShift)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (empShift == null)
            {
                return NotFound();
            }

            return View(empShift);
        }

        // GET: EmpShifts/Create
        public IActionResult CreateOrJoin()
        {
            ViewBag.ShowLogOut = true;

            // service title viewbag
            ViewBag.SerTitle = GetSerTitleList();

            return View();
        }

        // GET: EmpShifts/Create
        public IActionResult Create()
        {
            ViewBag.ShowLogOut = true;

            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId");
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId");
            return View();
        }

        // GET: EmpShifts/GetFilteredLocations
        [HttpGet]
        public ActionResult GetFilteredLocations(string servTitle)
        {
            var res = employeeServiceControl.FilterLocations(servTitle);

            return Ok(res);
        }

        // GET: EmpShifts/GetFilteredDayOfWeek
        [HttpGet]
        public ActionResult GetFilteredDayOfWeek(string servTitle, string location)
        {
            var res = employeeServiceControl.FilterDayOfTheWeek(servTitle, location);

            return Ok(res);
        }

        // GET: EmpShifts/GetFilteredTime
        [HttpGet]
        public ActionResult GetFilteredTime(string servTitle, string location, string dayOfWeek)
        {

            var res = employeeServiceControl.FilterStartAndEndTime(servTitle, location, dayOfWeek);

            return Ok(res);
        }

        // GET: EmpShifts/GetFilteredShifts
        [HttpGet]
        public ActionResult GetFilteredShifts(string servTitle, string location, string dayOfWeek, string startToEndTime)
        {

            return Ok(employeeServiceControl.FilterAvailableShifts(servTitle, location, dayOfWeek, startToEndTime));
        }

        // POST: EmpShifts/JoinShift
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult JoinShift(int ServiceShiftId)
        {
            int? EmpId = HttpContext.Session.GetInt32("empID");

            if (EmpId == null)
            {
                return RedirectToAction("RoleSelection", "Home");
            }

            EmpShift empShift = new EmpShift((int)EmpId, ServiceShiftId);

            if (ModelState.IsValid)
            {
                _context.Add(empShift);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return Ok();
        }

        // POST: EmpShifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,ServiceShiftId")] EmpShift empShift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empShift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", empShift.EmpId);
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId", empShift.ServiceShiftId);
            return View(empShift);
        }

        // GET: EmpShifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ShowLogOut = true;

            if (id == null)
            {
                return NotFound();
            }

            var empShift = await _context.EmpShifts.FindAsync(id);
            if (empShift == null)
            {
                return NotFound();
            }
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", empShift.EmpId);
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId", empShift.ServiceShiftId);
            return View(empShift);
        }

        // POST: EmpShifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,ServiceShiftId")] EmpShift empShift)
        {
            if (id != empShift.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empShift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpShiftExists(empShift.EmpId))
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
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId", empShift.EmpId);
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId", empShift.ServiceShiftId);
            return View(empShift);
        }

        // GET: EmpShifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.ShowLogOut = true;

            if (id == null)
            {
                return NotFound();
            }

            var empShift = await _context.EmpShifts
                .Include(e => e.Employee)
                .Include(e => e.ServiceShift)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (empShift == null)
            {
                return NotFound();
            }

            return View(empShift);
        }

        // POST: EmpShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empShift = await _context.EmpShifts.FindAsync(id);
            _context.EmpShifts.Remove(empShift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpShiftExists(int id)
        {
            return _context.EmpShifts.Any(e => e.EmpId == id);
        }
    }
}
