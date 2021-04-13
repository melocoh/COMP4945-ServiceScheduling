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
    public class EmployeeServiceControl
    {
        public List<EmployeeService> employeeServiceList;
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

        public List<string> getAllServices()
        {
            List<string> allServices = new List<string>();
            for(int i=0;i< employeeServiceList.Count; i++)
            {
                allServices.Add(employeeServiceList[i].servTitle);
            }

            return allServices;
        }

        public List<string> getFilterLocations(string servTitle)
        {
            List<string> filteredLocations = new List<string>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                if(employeeServiceList[i].servTitle== servTitle)
                {
                    filteredLocations.Add(employeeServiceList[i].location);
                }
            }

            return filteredLocations;
        }

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




        public List<EmployeeServiceInfo> getAvailableSessions(string servTitle, string location, DayOfWeek dayOfWeek, string startToEndTime)
        {
            List<EmployeeServiceInfo> availableSessions = new List<EmployeeServiceInfo>();
            for (int i = 0; i < employeeServiceList.Count; i++)
            {
                string startTime = employeeServiceList[i].StartTime.Hours.ToString("D2") + ":" + employeeServiceList[i].StartTime.Minutes.ToString("D2");
                string endTime = employeeServiceList[i].EndTime.Hours.ToString("D2") + ":" + employeeServiceList[i].EndTime.Minutes.ToString("D2");
                string combinedTime = startTime + " - " + endTime;
                if ((employeeServiceList[i].servTitle == servTitle) && (employeeServiceList[i].location == location) && (employeeServiceList[i].DayOfWeek == dayOfWeek) && (combinedTime== startToEndTime))
                {
                    //filteredStartAndEndTime.Add(new EmployeeServiceInfo());
                }
            }



            return availableSessions;
        }
    }

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

    public class EmployeeServiceInfo
    {
        public int numberOfEmployees;
        public int numberOfMaxEmployees;
        public List<string> employeeNames;
        public EmployeeServiceInfo(int numberOfEmployees, int numberOfMaxEmployees, List<string> employeeNames)
        {
            this.numberOfEmployees = numberOfEmployees;
            this.numberOfMaxEmployees = numberOfMaxEmployees;
            this.employeeNames = employeeNames;
        }
    }







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

    public class ServiceShiftsController : Controller
    {
        private readonly AppContext _context;

        public ServiceShiftsController(AppContext context)
        {
            _context = context;

            EmployeeServiceControl employeeServiceControl = new EmployeeServiceControl(context);
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
