using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using cstestproject2;
using cstestproject2.Models;

namespace cstestproject2.Controllers
{
    public class ServiceAppointment
    {
        public int AppointmentId;
        public string ServTitle;
        public int MaxEmpNo;
        public int MaxClientNo;
        public DateTime StartDateTime;
        public DateTime EndDateTime;
        public int Rate;

        public ServiceAppointment(int appointmentId, string servTitle, int maxEmpNo, int maxClientNo, DateTime startDateTime, DateTime endDateTime, int rate)
        {
            AppointmentId = appointmentId;
            ServTitle = servTitle;
            MaxEmpNo = maxEmpNo;
            MaxClientNo = maxClientNo;
            StartDateTime = startDateTime;
            EndDateTime = endDateTime;
            Rate = rate;
        }
    }

    public class AppointmentsController : Controller
    {
        private readonly AppContext _context;

        public AppointmentsController(AppContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Appointments.Include(a => a.Service);
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
                .FirstOrDefaultAsync(m => m.AppId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            ViewBag.ServiceAppointment = GetServiceAppointmentDetailsList();
            ViewBag.Employees = GetEmployeesList();
            ViewBag.Clients = GetClientsList();

            return View(appointment);
        }

        // A list of Services
        private List<Employee> GetEmployeesList()
        {
            List<Employee> employees = _context.Employees.ToList<Employee>();

            return employees;
        }

        // A list of Services
        private List<Client> GetClientsList()
        {
            List<Client> clients = _context.Clients.ToList<Client>();

            return clients;
        }

        // A list of Services
        private List<SelectListItem> GetServicesList()
        {
            List<Service> services = _context.Services.ToList<Service>();

            List<SelectListItem> list = services.ConvertAll<SelectListItem>(a =>
            {
                return new SelectListItem()
                {
                    Text = a.ServTitle,
                    Value = a.ServTitle,
                    Selected = false
                };
            });

            return list;
        }

        // A list of Services
        private ServiceAppointment GetServiceAppointmentDetailsList()
        {

            var query = _context.Appointments
            .Join(
            _context.Services,
            appointment => appointment.ServTitle,
            service => service.ServTitle,
            (appointment, service) => new
            {
                AppointmentId = appointment.AppId,
                ServTitle = service.ServTitle,
                MaxEmpNo = service.MaxEmpNo,
                MaxClientNo = service.MaxEmpNo,
                Start = appointment.StartDateTime,
                End = appointment.EndDateTime,
                Rate = service.Rate
            }
            ).ToList();

            ServiceAppointment serviceAppointment = new ServiceAppointment(query[0].AppointmentId, query[0].ServTitle, query[0].MaxEmpNo, query[0].MaxClientNo, query[0].Start, query[0].End, query[0].Rate);


            return serviceAppointment;
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewBag.ServicesList = GetServicesList();
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppId,ServTitle,StartDateTime,EndDateTime")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
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
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppId,ServTitle,StartDateTime,EndDateTime")] Appointment appointment)
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
