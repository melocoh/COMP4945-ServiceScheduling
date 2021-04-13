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
    //keeps track of the database tables:
    //ServiceShifts, ServiceTypes, EmpShifts, and Employees
    //contains the methods to work with the data from the tables
    public class EmployeeServiceControl
    {
        //database tables
        public List<EmployeeService> employeeServiceList;

        //load all the data from tables into this object
        public EmployeeServiceControl(AppContext context)
        {
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
                MaxNoEmp = combinedEntry2.combinedEntry.serviceType.MaxNoEmp,
                ServiceShiftId = combinedEntry2.empShifts.ServiceShiftId,
                DayOfWeek = combinedEntry2.empShifts.ServiceShift.DayOfWeek,
                location = combinedEntry2.empShifts.ServiceShift.SerLocation,
                StartTime = combinedEntry2.empShifts.ServiceShift.TimeStart,
                EndTime = combinedEntry2.empShifts.ServiceShift.TimeEnd,
                EmpId = employees.EmpId,
                FullName = employees.FullName
            }).ToList();


            employeeServiceList = new List<EmployeeService>();
            for (int i = 0; i < query.Count; i++)
            {
                employeeServiceList.Add(new EmployeeService(query[i].serId, query[i].servTitle, query[i].MaxNoEmp, query[i].ServiceShiftId, query[i].DayOfWeek, query[i].location,
                    query[i].StartTime, query[i].EndTime, query[i].EmpId, query[i].FullName));
            }
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
        public List<string> getFilterLocations(string servTitle)
        {
            List<string> filteredLocations = new List<string>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if (employeeServiceList[i].servTitle == servTitle)
                {
                    filteredLocations.Add(employeeServiceList[i].location);
                }
            }

            return filteredLocations;
        }

        //returns a list of all services day of the week filtered by their title and location
        public List<DayOfWeek> getFilterDayOfTheWeek(string servTitle, string location)
        {
            List<DayOfWeek> filteredDayOfTheWeek = new List<DayOfWeek>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if ((employeeServiceList[i].servTitle == servTitle) && (employeeServiceList[i].location == location))
                {
                    filteredDayOfTheWeek.Add(employeeServiceList[i].DayOfWeek);
                }
            }

            return filteredDayOfTheWeek;
        }

        //returns a list of all services (start + end time) filtered by their title, location, and day of the week
        public List<string> getStartAndEndTime(string servTitle, string location, DayOfWeek dayOfWeek)
        {
            List<string> filteredStartAndEndTime = new List<string>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if ((employeeServiceList[i].servTitle == servTitle) && (employeeServiceList[i].location == location) && (employeeServiceList[i].DayOfWeek == dayOfWeek))
                {
                    string startTime = employeeServiceList[i].StartTime.Hours.ToString("D2") + ":" + employeeServiceList[i].StartTime.Minutes.ToString("D2");
                    string endTime = employeeServiceList[i].EndTime.Hours.ToString("D2") + ":" + employeeServiceList[i].EndTime.Minutes.ToString("D2");

                    filteredStartAndEndTime.Add(startTime + " - " + endTime);
                }
            }

            return filteredStartAndEndTime;
        }


        //returns a list of service information that contains:
        //serviceShiftId
        //numberOfEmployees
        //numberOfMaxEmployees
        //List<string> employeeNames
        //it is filtered by a service title, location, day of the week, and (start + end time)
        public List<EmployeeServiceInfo> getAvailableSessions(string servTitle, string location, DayOfWeek dayOfWeek, string startToEndTime)
        {
            List<EmployeeServiceInfo> availableSessions = new List<EmployeeServiceInfo>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                string startTime = employeeServiceList[i].StartTime.Hours.ToString("D2") + ":" + employeeServiceList[i].StartTime.Minutes.ToString("D2");
                string endTime = employeeServiceList[i].EndTime.Hours.ToString("D2") + ":" + employeeServiceList[i].EndTime.Minutes.ToString("D2");
                string combinedTime = startTime + " - " + endTime;
                if ((employeeServiceList[i].servTitle == servTitle) && (employeeServiceList[i].location == location) && (employeeServiceList[i].DayOfWeek == dayOfWeek) && (combinedTime == startToEndTime))
                {
                    int currentSerShiftID = employeeServiceList[i].ServiceShiftId;
                    int currentSerShiftMaxEmployees = employeeServiceList[i].MaxNoEmp;
                    string currentSerShiftEmployeeName = employeeServiceList[i].FullName;
                    bool addedEmployeeToList = false;
                    for (int f=0;f< availableSessions.Count; f++)
                    {
                        if(currentSerShiftID == availableSessions[f].serviceShiftId)
                        {
                            //session already exists so add to it
                            availableSessions[f].addEmployee(currentSerShiftEmployeeName);
                            addedEmployeeToList = true;
                            break;
                        }
                    }

                    //session doesn't exist so create it
                    if (!addedEmployeeToList)
                    {
                        EmployeeServiceInfo newEmployeeServiceInfo = new EmployeeServiceInfo(currentSerShiftMaxEmployees, currentSerShiftID);
                        newEmployeeServiceInfo.addEmployee(currentSerShiftEmployeeName);
                        availableSessions.Add(newEmployeeServiceInfo);
                    }
                }
            }



            return availableSessions;
        }
    }

    //contains one entry from the database tables:
    //ServiceShifts, ServiceTypes, EmpShifts, and Employees
    public class EmployeeService
    {
        public int serId;
        public string servTitle;
        public int MaxNoEmp;
        public int ServiceShiftId;
        public DayOfWeek DayOfWeek;
        public string location;
        public TimeSpan StartTime;
        public TimeSpan EndTime;
        public int EmpId;
        public string FullName;

        public EmployeeService(int serId, string servTitle, int maxNoEmp, int serviceShiftId, DayOfWeek dayOfWeek, string location, TimeSpan startTime, TimeSpan endTime, int empId, string fullName)
        {
            this.serId = serId;
            this.servTitle = servTitle;
            MaxNoEmp = maxNoEmp;
            ServiceShiftId = serviceShiftId;
            DayOfWeek = dayOfWeek;
            this.location = location;
            StartTime = startTime;
            EndTime = endTime;
            EmpId = empId;
            FullName = fullName;
        }


    }

    //contains information about a weekly session for employees
    public class EmployeeServiceInfo
    {
        public int numberOfEmployees;
        public int numberOfMaxEmployees;
        public List<string> employeeNames;
        public int serviceShiftId;
        public EmployeeServiceInfo(int numberOfMaxEmployees, int serviceShiftId)
        {
            this.numberOfEmployees = 0;
            this.numberOfMaxEmployees = numberOfMaxEmployees;
            this.serviceShiftId = serviceShiftId;
            employeeNames = new List<string>();
        }

        public bool addEmployee(string newEmployeeName)
        {
            if(employeeNames.Count< numberOfMaxEmployees)
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






    // object of serviceshifttype details
    // @usage form input
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
    }




    /************************** Melody's code for the form, but will merge with Eric's later **************************/
    public class ServiceShiftTypeController
    {
        public List<ServiceShiftType> serviceAppointmentList;

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


            serviceAppointmentList = new List<ServiceShiftType>();
            servIdList = new List<int>();
            servTitleList = new List<string>();
            serLocationList = new List<string>();
            dayOfWeekList = new List<DayOfWeek>();
            timeStartList = new List<TimeSpan>();
            timeEndList = new List<TimeSpan>();
            startToEndTimeList = new List<string>();

            for (int i = 0; i < query.Count; i++)
            {
                ServiceShiftType serviceShiftType = new ServiceShiftType(query[i].Id, query[i].Title, query[i].Location, query[i].DayOfTheWeek, query[i].StartTime, query[i].EndTime);
                serviceAppointmentList.Add(serviceShiftType);
                servIdList.Add(serviceShiftType.id);
                servTitleList.Add(serviceShiftType.servTitle);
                serLocationList.Add(serviceShiftType.location);
                dayOfWeekList.Add(serviceShiftType.dayOfWeek);
                timeStartList.Add(query[i].StartTime);
                timeEndList.Add(query[i].EndTime);
                startToEndTimeList.Add(serviceShiftType.startToEndTime);
            }

            serviceAppointmentList = serviceAppointmentList.Distinct().ToList();
            servIdList = servIdList.Distinct().ToList();
            servTitleList = servTitleList.Distinct().ToList();
            serLocationList = serLocationList.Distinct().ToList();
            dayOfWeekList = dayOfWeekList.Distinct().ToList();
            timeStartList = timeStartList.Distinct().ToList();
            timeEndList = timeEndList.Distinct().ToList();
            startToEndTimeList = startToEndTimeList.Distinct().ToList();

        }
    }
    /***************************************************************************************************/




    public class ServiceShiftsController : Controller
    {
        private readonly AppContext _context;

        public ServiceShiftsController(AppContext context)
        {
            _context = context;

            EmployeeServiceControl employeeServiceControl = new EmployeeServiceControl(context);

            TimeSpan startTime = new TimeSpan(2, 14, 18);
            TimeSpan endTime = new TimeSpan(4, 14, 18);
            string startTimeString = startTime.Hours.ToString("D2") + ":" + startTime.Minutes.ToString("D2");
            string endTimeString = endTime.Hours.ToString("D2") + ":" + endTime.Minutes.ToString("D2");
            EmployeeService newEmployeeService1 = new EmployeeService(10, "personal fanner", 3, 8, DayOfWeek.Wednesday, "home <3", startTime, endTime, 6, "Eric Sondraal");
            EmployeeService newEmployeeService2 = new EmployeeService(10, "personal fanner", 3, 8, DayOfWeek.Wednesday, "home <3", startTime, endTime, 5, "Ryan Sondraal");
            employeeServiceControl.employeeServiceList.Add(newEmployeeService1);
            employeeServiceControl.employeeServiceList.Add(newEmployeeService2);

            List<EmployeeServiceInfo> test1 =
                employeeServiceControl.getAvailableSessions("personal fanner", "home <3", DayOfWeek.Wednesday, startTimeString + " - " + endTimeString);

            var test2 = employeeServiceControl.getAllServices();
            var test3 = employeeServiceControl.getFilterLocations("personal fanner");
            var test4 = employeeServiceControl.getFilterDayOfTheWeek("personal fanner","home <3");
            var test5 = employeeServiceControl.getStartAndEndTime("personal fanner", "home <3", DayOfWeek.Wednesday);

            //employeeServiceControl.getAvailableSessions()
            //var testData = GetServiceShiftTypeDetailsList();
            //testData = testData;
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

        /************************************** Select Lists for Form **************************************/

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

        ////Converts List to SelectListItems
        //// @returns a list of service start time
        //private List<SelectListItem> GetSerStartTime()
        //{
        //    ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

        //    List<SelectListItem> list = serviceShiftTypeControllerObj.timeEndList.ConvertAll<SelectListItem>(item =>
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

        ////Converts List to SelectListItems
        //// @returns a list of service end time
        //private List<SelectListItem> GetSerEndTime()
        //{
        //    ServiceShiftTypeController serviceShiftTypeControllerObj = new ServiceShiftTypeController(_context);

        //    List<SelectListItem> list = serviceShiftTypeControllerObj.timeStartList.ConvertAll<SelectListItem>(item =>
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

        /**************************************************************************************************************/

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

            // service start and end time viewbag
            //ViewBag.SerStartTime = GetSerStartTime();

            // service start and end time viewbag
            //ViewBag.SerEndTime = GetSerEndTime();

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
