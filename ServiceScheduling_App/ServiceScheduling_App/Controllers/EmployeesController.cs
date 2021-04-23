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

namespace ServiceScheduling_App.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly AppContext _context;

        
        public EmployeesController(AppContext context)
        {
            _context = context;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var appContext = _context.Employees.Include(e => e.JobType);
            return View(await appContext.ToListAsync());
        }

        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            id = HttpContext.Session.GetInt32("empID");

            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.JobType)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {

            ViewData["CertId"] = new SelectList(_context.CertificationTypes, "CertId", "CertTitle");

            ViewData["JobId"] = new SelectList(_context.JobTypes, "JobId", "JobTitle");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,FullName,JobId,Email,Password")] Employee employee)
        {
            // Checks whether the request values were able to bind to the model
            if (ModelState.IsValid)
            {
                // Adds the employee object to the database context
                _context.Add(employee);
                // Asynchronously saves the context changes to the database
                await _context.SaveChangesAsync();
                return RedirectToAction("LoggedIn","Home");

            }
            
            // Returns view
            return View();
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            ViewData["JobId"] = new SelectList(_context.JobTypes, "JobId", "JobTitle");
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,FullName,JobId,Email,Password")] Employee employee)
        {
            // Checks whether the id for the employee is correct
            if (id != employee.EmpId)
            {
                return NotFound();
            }

            // Checks whether the request values were able to bind to the model
            if (ModelState.IsValid)
            {
                try
                {
                    // Updates the employee in the database context
                    _context.Update(employee);
                    // Asynchronously saves the context changes to the database
                    await _context.SaveChangesAsync();
                }
                // If database changes failed to save then catch
                catch (DbUpdateConcurrencyException)
                {
                    // Checks if the employee cannot be found in the database
                    // based on their employee id
                    if (!EmployeeExists(employee.EmpId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // Redirect to index page
                return RedirectToAction(nameof(Index));
            }
            // Return view
            return View();
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.JobType)
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmpId == id);
        }

        [HttpGet]
        public JsonResult ChartDetailsRevenue()
        {
            Console.WriteLine("Called");
            return Json(new Chart("line", new[] { 65, 59, 80, 81, 56, 55, 40 }));
        }

        [HttpGet]
        public JsonResult ChartDetailsOutcome()
        {
            Console.WriteLine("Called");
            return Json(new Chart("bar", new[] { 65, 59, 80, 81, 56, 55, 40 }));
        }

        [HttpGet]
        public JsonResult ChartDetailsPerformance()
        {
            Console.WriteLine("Called");
            return Json(new Chart("line", new[] { 65, 59, 80, 81, 56, 55, 40 }));
        }
    }
}
