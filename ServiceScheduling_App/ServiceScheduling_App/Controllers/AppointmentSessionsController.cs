using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling_App;
using ServiceScheduling_App.Models;

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;

namespace ServiceScheduling_App.Controllers
{
    public class AppointmentSessionsController : Controller
    {
        private readonly AppContext _context;

        public AppointmentSessionsController(AppContext context)
        {
            _context = context;
        }

        // GET: AppointmentSessions
        public async Task<IActionResult> Index()
        {
            ViewBag.ShowLogOut = true;
            int? empId = HttpContext.Session.GetInt32("empID");
            int? clientId = HttpContext.Session.GetInt32("clientID");

            if ( empId == null && clientId == null)
            {
                return RedirectToAction("RoleSelection", "Home");
            } 
            else if (empId != null) 
            {
                var appContext = _context.AppointmentSession.Include(a => a.Appointment).Include(a => a.Appointment.EmpAppointments).Where(a => a.Appointment.EmpAppointments.Any(e => e.EmpId == empId));
                return View(await appContext.ToListAsync());
            } else
            {
                var appContext = _context.AppointmentSession.Include(a => a.Appointment).Include(a => a.Appointment.ClientAppointments).Where(a => a.Appointment.ClientAppointments.Any(e => e.ClientId == clientId));
                return View(await appContext.ToListAsync());
            }
            
        }

        // GET: AppointmentSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.ShowLogOut = true;

            if (id == null)
            {
                return NotFound();
            }

            ViewBag.ShowLogOut = true;
            var appointmentSession = await _context.AppointmentSession
                .Include(a => a.Appointment)
                .FirstOrDefaultAsync(m => m.AppSessionId == id);
            if (appointmentSession == null)
            {
                return NotFound();
            }

            return View(appointmentSession);
        }

        // GET: AppointmentSessions/Create
        public IActionResult Create()
        {
            ViewBag.ShowLogOut = true;
            ViewData["AppId"] = new SelectList(_context.Appointments, "AppId", "AppId");
            return View();
        }

        // POST: AppointmentSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppSessionId,AppId,SessionNo,Status,StartDateTime,EndDateTime")] AppointmentSession appointmentSession)
        {
            ViewBag.ShowLogOut = true;

            if (ModelState.IsValid)
            {
                _context.Add(appointmentSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppId"] = new SelectList(_context.Appointments, "AppId", "AppId", appointmentSession.AppId);
            ViewBag.ShowLogOut = true;
            return View(appointmentSession);
        }

        // GET: AppointmentSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.ShowLogOut = true;

            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ShowLogOut = true;
            var appointmentSession = await _context.AppointmentSession.FindAsync(id);
            if (appointmentSession == null)
            {
                return NotFound();
            }
            ViewData["AppId"] = new SelectList(_context.Appointments, "AppId", "AppId", appointmentSession.AppId);
            return View(appointmentSession);
        }

        // POST: AppointmentSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppSessionId,AppId,SessionNo,Status,StartDateTime,EndDateTime")] AppointmentSession appointmentSession)
        {
            if (id != appointmentSession.AppSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentSessionExists(appointmentSession.AppSessionId))
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
            ViewData["AppId"] = new SelectList(_context.Appointments, "AppId", "AppId", appointmentSession.AppId);
            return View(appointmentSession);
        }

        // GET: AppointmentSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.ShowLogOut = true;

            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ShowLogOut = true;
            var appointmentSession = await _context.AppointmentSession
                .Include(a => a.Appointment)
                .FirstOrDefaultAsync(m => m.AppSessionId == id);
            if (appointmentSession == null)
            {
                return NotFound();
            }

            return View(appointmentSession);
        }

        // POST: AppointmentSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointmentSession = await _context.AppointmentSession.FindAsync(id);
            _context.AppointmentSession.Remove(appointmentSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentSessionExists(int id)
        {
            return _context.AppointmentSession.Any(e => e.AppSessionId == id);
        }
    }
}
