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
    public class EmpShiftsController : Controller
    {
        private readonly AppContext _context;

        public EmpShiftsController(AppContext context)
        {
            _context = context;
        }

        // GET: EmpShifts
        public async Task<IActionResult> Index()
        {
            var appContext = _context.EmpShifts.Include(e => e.Employee).Include(e => e.ServiceShift);
            return View(await appContext.ToListAsync());
        }

        // GET: EmpShifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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
        public IActionResult Create()
        {
            ViewData["EmpId"] = new SelectList(_context.Employees, "EmpId", "EmpId");
            ViewData["ServiceShiftId"] = new SelectList(_context.ServiceShifts, "ServiceShiftId", "ServiceShiftId");
            return View();
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
