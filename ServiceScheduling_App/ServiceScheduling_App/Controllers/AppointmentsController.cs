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

namespace ServiceScheduling_App.Controllers
{
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
