using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling_App;
using ServiceScheduling_App.Models;
using ServiceScheduling_App.ViewModels;

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace ServiceScheduling_App.Controllers
{


    // EmployeeService
    // object that contains elements from ServiceShift, ServiceType, EmpShift, and Employee
    // @usage form input
    public class ClientEmployeeService
    {
        public int serId;
        public string servTitle;
        public int maxNoClient;
        public int serviceShiftId;
        public DayOfWeek dayOfWeek;
        public string location;
        public TimeSpan startTime;
        public TimeSpan endTime;
        public int empId;
        public string fullName;
        public double rate;

        public ClientEmployeeService(int serId, string servTitle, int maxNoClient, int serviceShiftId, DayOfWeek dayOfWeek, string location, TimeSpan startTime, TimeSpan endTime, int empId, string fullName, double rate)
        {
            this.serId = serId;
            this.servTitle = servTitle;
            this.maxNoClient = maxNoClient;
            this.serviceShiftId = serviceShiftId;
            this.dayOfWeek = dayOfWeek;
            this.location = location;
            this.startTime = startTime;
            this.endTime = endTime;
            this.empId = empId;
            this.fullName = fullName;
            this.rate = rate;
        }


    }



    public class ClientAppointment
    {
        public int appID;
        public int serviceShiftID;
        public int ClientID;
        public DateTime startDateTime;
        public DateTime endDateTime;
        public DateTime entryDate;

        public ClientAppointment(int appID, int serviceShiftID, int clientID, DateTime startDateTime, DateTime endDateTime, DateTime entryDate)
        {
            this.appID = appID;
            this.serviceShiftID = serviceShiftID;
            ClientID = clientID;
            this.startDateTime = startDateTime;
            this.endDateTime = endDateTime;
            this.entryDate = entryDate;
        }
    }



    public class BookingAppointmentData
    {
        public int serviceShiftID { get; set; }
        public DateTime date { get; set; }
        public string dateStr { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int clientCount { get; set; }
        public int maxClients { get; set; }
        public string location { get; set; }
        public List<string> employeeNameList { get; set; }
        public double rate { get; set; }
        public string startAndEndTime { get; set; }
        public string dayOfTheWeek { get; set; }

        public BookingAppointmentData(int serviceShiftID, DateTime date, string startTime, string endTime, int maxClients, string location, double rate)
        {
            this.serviceShiftID = serviceShiftID;
            this.date = date;
            this.dateStr = date.ToString("MM/dd/yyyy");
            this.startTime = startTime;
            this.endTime = endTime;
            this.clientCount = 0;
            this.maxClients = maxClients;
            this.location = location;
            this.rate = rate;
            this.employeeNameList = new List<string>();

            //strip away the seconds
            TimeSpan startTimeHourMinute = TimeSpan.Parse(startTime);
            TimeSpan endTimeHourMinute = TimeSpan.Parse(endTime);
            this.startAndEndTime = startTimeHourMinute.Hours.ToString("D2") + ":" + startTimeHourMinute.Minutes.ToString("D2") + " - "
                + endTimeHourMinute.Hours.ToString("D2") + ":" + endTimeHourMinute.Minutes.ToString("D2");

            this.dayOfTheWeek = date.DayOfWeek.ToString();
        }

        public void addClient()
        {
            clientCount++;
        }

        public void addEmployee(string employeeName)
        {
            employeeNameList.Add(employeeName);

        }
    }









    /*************************************************** Query executing objects ********************************************************/

    public class ClientEmployeeServiceControl
    {
        // ServiceShiftType list that holds every element
        public List<ClientEmployeeService> clientEmployeeServiceList;
        //client appointments + appointment + appointmentsession
        public List<ClientAppointment> clientAppointmentList;


        // ServiceShiftType list that holds id and serTitle
        //public List<ClientEmployeeService> serviceIdTitleList;
        public List<string> serviceTitleList;

        // string list that holds location
        public List<string> serLocationList;

        // DayOfWeek list that holds days
        public List<DayOfWeek> dayOfWeekList;

        // string list that holds startTime - endTime
        public List<string> startToEndTimeList;

        //load all the data from tables into this object
        public ClientEmployeeServiceControl(AppContext context)
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
                serId = combinedEntry2.combinedEntry.serviceShift.ServId,//
                servTitle = combinedEntry2.combinedEntry.serviceType.ServTitle,//
                maxNoClient = combinedEntry2.combinedEntry.serviceType.MaxNoClient,//
                serviceShiftId = combinedEntry2.empShifts.ServiceShiftId,//
                dayOfWeek = combinedEntry2.empShifts.ServiceShift.DayOfWeek,//
                location = combinedEntry2.empShifts.ServiceShift.SerLocation,//
                startTime = combinedEntry2.empShifts.ServiceShift.TimeStart,//
                endTime = combinedEntry2.empShifts.ServiceShift.TimeEnd,//
                empId = employees.EmpId,//
                fullName = employees.FullName,//
                rate = combinedEntry2.combinedEntry.serviceType.Rate//
            }).ToList();

            // instantiates all lists
            clientEmployeeServiceList = new List<ClientEmployeeService>();

            serviceTitleList = new List<string>();
            serLocationList = new List<string>();
            dayOfWeekList = new List<DayOfWeek>();


            for (int i = 0; i < query.Count; i++)
            {
                // calls all / two parameter constructor
                ClientEmployeeService clientEmployeeService = new ClientEmployeeService(query[i].serId, query[i].servTitle, query[i].maxNoClient,
                    query[i].serviceShiftId, query[i].dayOfWeek, query[i].location, query[i].startTime, query[i].endTime,
                    query[i].empId, query[i].fullName, query[i].rate);
                EmployeeService serviceIdTitle = new EmployeeService(query[i].serId, query[i].servTitle);

                clientEmployeeServiceList.Add(clientEmployeeService);

            }

            // ensures distinct data in the list
            clientEmployeeServiceList = clientEmployeeServiceList.Distinct().ToList();
            //serviceIdTitleList = serviceIdTitleList.Distinct(new DistinctItemComparer2()).ToList(); // custom Distinct because there are different type elements





            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //MERGE AppointmentSession, Appointment, and ClientAppointment
            //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var query2 = context.AppointmentSession
            .Join(
            context.Appointments,
            appointmentSession => appointmentSession.AppId,
            appointments => appointments.AppId,
            (appointmentSession, appointments) => new
            {
                appointmentSession,
                appointments
            }
            ).Join(context.ClientAppointments,
            combinedEntry => combinedEntry.appointments.AppId,
            clientAppointments => clientAppointments.AppId,
            (combinedEntry, clientAppointments) => new
            {
                appID = clientAppointments.AppId,
                serviceShiftID = combinedEntry.appointments.ServiceShiftId,
                clientID = clientAppointments.ClientId,
                startDate = combinedEntry.appointmentSession.StartDateTime,
                endDate = combinedEntry.appointmentSession.EndDateTime,
                entryDate = combinedEntry.appointmentSession.StartDateTime,

            }).ToList();


            // instantiates all lists
            clientAppointmentList = new List<ClientAppointment>();


            for (int i = 0; i < query2.Count; i++)
            {
                // calls all / two parameter constructor
                ClientAppointment clientAppointment = new ClientAppointment(query2[i].appID, query2[i].serviceShiftID, query2[i].clientID, query2[i].startDate, query2[i].endDate, query2[i].entryDate);


                clientAppointmentList.Add(clientAppointment);
            }

            // ensures distinct data in the list
            //clientEmployeeServiceList = clientEmployeeServiceList.Distinct().ToList();



        }

        //returns a list of all services
        public List<string> getAllServices()
        {
            List<string> allServices = new List<string>();
            for (int i = 0; i < clientEmployeeServiceList.Count; i++)
            {
                allServices.Add(clientEmployeeServiceList[i].servTitle);
            }

            return allServices;
        }

        //returns a list of all services locations filtered by their title
        public List<string> FilterLocations(string servTitle)
        {
            List<string> filteredLocations = new List<string>();
            for (int i = 0; i < clientEmployeeServiceList.Count; i++)
            {
                if (clientEmployeeServiceList[i].servTitle == servTitle)
                {
                    filteredLocations.Add(clientEmployeeServiceList[i].location);
                }
            }

            return filteredLocations.Distinct().ToList();
        }

        //returns a list of all services day of the week filtered by their title and location
        public List<DayOfWeek> FilterDayOfTheWeek(string servTitle, string location)
        {
            List<DayOfWeek> filteredDayOfTheWeek = new List<DayOfWeek>();
            for (int i = 0; i < clientEmployeeServiceList.Count; i++)
            {
                if ((clientEmployeeServiceList[i].servTitle == servTitle) && (clientEmployeeServiceList[i].location == location))
                {
                    filteredDayOfTheWeek.Add(clientEmployeeServiceList[i].dayOfWeek);
                }
            }

            return filteredDayOfTheWeek.Distinct().ToList();
        }


        public string FilterBookingAppointment(string servTitle, string location, DayOfWeek dayOfWeek, DateTime minDateRange, DateTime maxDateRange)
        {
            List<BookingAppointmentData> bookingAppointmentDataList = new List<BookingAppointmentData>();


            //loop through all of the clientEmployeeServiceList list
            for (int i = 0; i < clientEmployeeServiceList.Count; i++)
            {
                //filter based on the methods arguments
                if ((clientEmployeeServiceList[i].servTitle == servTitle) && (clientEmployeeServiceList[i].location == location)
                        && (clientEmployeeServiceList[i].dayOfWeek == dayOfWeek))
                {
                    for (DateTime currentDateTime = minDateRange; currentDateTime < maxDateRange; currentDateTime += new TimeSpan(1, 0, 0, 0))
                    {
                        if (currentDateTime.DayOfWeek == dayOfWeek)
                        {

                            bool foundInsideList = false;
                            //check if we already we already have already have a record
                            for (int f = 0; f < bookingAppointmentDataList.Count; f++)
                            {
                                if ((bookingAppointmentDataList[f].date == currentDateTime) && (clientEmployeeServiceList[i].serviceShiftId == bookingAppointmentDataList[f].serviceShiftID))
                                {
                                    //it already exists....
                                    //add the employee to it
                                    bookingAppointmentDataList[f].addEmployee(clientEmployeeServiceList[i].fullName);
                                    foundInsideList = true;
                                    break;
                                }
                            }


                            if (!foundInsideList)
                            {
                                bookingAppointmentDataList.Add(new BookingAppointmentData(clientEmployeeServiceList[i].serviceShiftId, currentDateTime,
                                    clientEmployeeServiceList[i].startTime.ToString(), clientEmployeeServiceList[i].endTime.ToString(),
                                    clientEmployeeServiceList[i].maxNoClient, clientEmployeeServiceList[i].location, clientEmployeeServiceList[i].rate));

                                bookingAppointmentDataList[bookingAppointmentDataList.Count - 1].addEmployee(clientEmployeeServiceList[i].fullName);
                            }



                        }

                    }
                }

            }


            //loop through our new employee divs and add the clients to them
            for (int i = 0; i < bookingAppointmentDataList.Count; i++)
            {
                //loop through our new employee divs and add the clients to them
                for (int f = 0; f < clientAppointmentList.Count; f++)
                {
                    DateTime dateFromEntry = new DateTime(clientAppointmentList[f].entryDate.Year,
                        clientAppointmentList[f].entryDate.Month,
                        clientAppointmentList[f].entryDate.Day);


                    if ((bookingAppointmentDataList[i].serviceShiftID == clientAppointmentList[f].serviceShiftID) &&
                            (bookingAppointmentDataList[i].date == dateFromEntry))
                    {
                        bookingAppointmentDataList[i].addClient();
                    }
                }
            }

            //return bookingAppointmentDataList;

            var serializer = JsonSerializer.Serialize(bookingAppointmentDataList);

            return serializer;
        }
    }


















































    public class AppointmentsController : Controller
    {
        private readonly AppContext _context;

        public ClientEmployeeServiceControl clientEmployeeServiceControl;

        public AppointmentsController(AppContext context)
        {
            _context = context;

            clientEmployeeServiceControl = new ClientEmployeeServiceControl(_context);
            //clientEmployeeServiceControl.FilterBookingAppointment("Rap", "Richmond", DayOfWeek.Saturday, new DateTime(2021,10,1,0,0,0), new DateTime(2021, 11, 30, 0, 0, 0));
        }


        // GET: Appointments/GetFilteredLocations
        [HttpGet]
        public ActionResult GetFilteredLocations(string servTitle)
        {

            return Ok(clientEmployeeServiceControl.FilterLocations(servTitle));
        }

        // GET: Appointments/GetFilteredDayOfWeek
        [HttpGet]
        public ActionResult GetFilteredDayOfWeek(string servTitle, string location)
        {

            return Ok(clientEmployeeServiceControl.FilterDayOfTheWeek(servTitle, location));
        }

        // GET: Appointments/GetFilteredLocations
        [HttpGet]
        public ActionResult GetFilterBookingAppointment(string servTitle, string location, DayOfWeek dayOfWeek, DateTime minDateRange, DateTime maxDateRange)
        {

            return Ok(clientEmployeeServiceControl.FilterBookingAppointment(servTitle, location, dayOfWeek, minDateRange, maxDateRange));
        }

        // GET: Appointments/GetExistingAppointment
        [HttpGet]
        public ActionResult GetExistingAppointment(DateTime startDateTime, DateTime endDateTime, int serviceShiftId)
        {
            //startDateTime = new DateTime(2021, 11, 16, 12, 00, 00);
            //endDateTime = new DateTime(2021, 11, 16, 13, 00, 00);

            startDateTime = new DateTime(2021, 1, 16, 12, 00, 00);
            endDateTime = new DateTime(2021, 1, 16, 13, 00, 00);

            serviceShiftId = 2;

            // returns an array of appointment objects
            var appcontext = _context.Appointments.Include(a => a.AppointmentSessions).Include(b => b.ServiceShift)
                .Where(s => (s.AppointmentSessions.Any(s => s.StartDateTime == startDateTime)
                && s.AppointmentSessions.Any(s => s.EndDateTime == endDateTime))); // OR if serviceshiftId  matches param

            // if null, then create
            if(appcontext == null || !appcontext.Any())
            {
                // create function that inserts new appointment, appointment session, empAppointment, clientAppointment 
            } else
            {
                // create function that inserts new client Appointment
            }

            return View();
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("empID") == null)
            {
                return RedirectToAction("RoleSelection", "Home");
            }
            var appContext = _context.Appointments.Include(a => a.ServiceShift);
            return View(await appContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.ServiceShift)
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Booking
        public IActionResult Booking()
        {
            ViewData["Service"] = new SelectList(_context.ServiceTypes, "ServId", "ServTitle");

            ViewData["Location"] = new SelectList(_context.ServiceShifts, "SerLocation", "SerLocation");

            ViewData["Day"] = new SelectList(_context.ServiceShifts, "DayOfWeek", "DayOfWeek");

            ViewData["NumOfWeeks"] = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "2", Value = "2" },
                new SelectListItem() { Text = "3", Value = "3" }
            };

            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId");
            return View();
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppId,ServiceShiftId,EntryDate,TotalFee")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId", appointment.ServiceShiftId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId", appointment.ServiceShiftId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppId,ServiceShiftId,EntryDate,TotalFee")] Appointment appointment)
        {
            if (id != appointment.AppId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppId))
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
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId", appointment.ServiceShiftId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.ServiceShift)
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppId == id);
        }
    }
}
