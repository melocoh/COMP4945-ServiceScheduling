using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ServiceScheduling_App;
using cstestproject2.Models;

namespace ServiceScheduling_App.Controllers
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
            return View(await _context.ServiceSession.ToListAsync());
        }

        // GET: ServiceSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceSession = await _context.ServiceSession
                .FirstOrDefaultAsync(m => m.ServSessionId == id);
            if (serviceSession == null)
            {
                return NotFound();
            }

            return View(serviceSession);
        }

        // GET: ServiceSessions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServiceSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServSessionId,Service,Location,DayOfTheWeek,DateTime")] ServiceSession serviceSession)
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

            var serviceSession = await _context.ServiceSession.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("ServSessionId,Service,Location,DayOfTheWeek,DateTime")] ServiceSession serviceSession)
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

            var serviceSession = await _context.ServiceSession
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
            var serviceSession = await _context.ServiceSession.FindAsync(id);
            _context.ServiceSession.Remove(serviceSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceSessionExists(int id)
        {
            return _context.ServiceSession.Any(e => e.ServSessionId == id);
        }
    }
}
