using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cstestproject2;
using cstestproject2.Models;

namespace cstestproject2.Controllers
{
    public class ServiceSessionsController : Controller
    {
        private readonly AppContext _context;

        public ServiceSessionsController(AppContext context)
        {
            _context = context;
        }

        // GET: ServiceSessions
        public async Task<IActionResult> Index()
        {
            return View(await _context.ServiceSessions.ToListAsync());
        }

        // GET: ServiceSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceSession = await _context.ServiceSessions
                .FirstOrDefaultAsync(m => m.ServSessionId == id);
            if (serviceSession == null)
            {
                return NotFound();
            }

            return View(serviceSession);
        }

        // A list of Services
        private List<SelectListItem> GetServicesList()
        {
            List<Service> clients = _context.Services.ToList<Service>();

            List<SelectListItem> list = clients.ConvertAll<SelectListItem>(a =>
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

        // GET: ServiceSessions/Create
        public IActionResult Create()
        {

            ViewBag.ServicesList = GetServicesList();

            ViewBag.Locations = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Burnaby", Value = "Burnaby" },
                new SelectListItem() { Text = "Richmond", Value = "Richmond" },
                new SelectListItem() { Text = "Vancouver", Value = "Vancouver" }
            };

            ViewBag.ShiftTimes = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "9:00 - 10:00", Value = "9:00 - 10:00" },
                new SelectListItem() { Text = "12:00 - 1:00", Value = "12:00 - 1:00" },
                new SelectListItem() { Text = "16:00 - 18:00", Value = "16:00 - 18:00" }
            };

            return View();
        }

        // POST: ServiceSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServSessionId,Service,Location,DayOfTheWeek,Time")] ServiceSession serviceSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(serviceSession);
        }

        // GET: ServiceSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceSession = await _context.ServiceSessions.FindAsync(id);
            if (serviceSession == null)
            {
                return NotFound();
            }
            return View(serviceSession);
        }

        // POST: ServiceSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServSessionId,Service,Location,DayOfTheWeek,Time")] ServiceSession serviceSession)
        {
            if (id != serviceSession.ServSessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceSessionExists(serviceSession.ServSessionId))
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
            return View(serviceSession);
        }

        // GET: ServiceSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceSession = await _context.ServiceSessions
                .FirstOrDefaultAsync(m => m.ServSessionId == id);
            if (serviceSession == null)
            {
                return NotFound();
            }

            return View(serviceSession);
        }

        // POST: ServiceSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceSession = await _context.ServiceSessions.FindAsync(id);
            _context.ServiceSessions.Remove(serviceSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceSessionExists(int id)
        {
            return _context.ServiceSessions.Any(e => e.ServSessionId == id);
        }
    }
}
