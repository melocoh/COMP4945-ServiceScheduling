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
    public class ServicesController : Controller
    {
        private readonly AppContext _context;

        public ServicesController(AppContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index()
        {
            return View(await _context.Services.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }






        //// A list of Services
        //private ServiceAppointment GetServiceShiftTypeDetailsList()
        //{

        //    //var query = _context.
        //    //.Join(
        //    //_context.Services,
        //    //serviceShift => serviceShift.,
        //    //serviceType => serviceType.ServId,
        //    //(appointment, service) => new
        //    //{
        //    //    AppointmentId = appointment.AppId,
        //    //    ServTitle = service.ServTitle,
        //    //    MaxEmpNo = service.MaxEmpNo,
        //    //    MaxClientNo = service.MaxEmpNo,
        //    //    Start = appointment.StartDateTime,
        //    //    End = appointment.EndDateTime,
        //    //    Rate = service.Rate
        //    //}
        //    //).ToList();

        //    //ServiceAppointment serviceAppointment = new ServiceAppointment(query[0].AppointmentId, query[0].ServTitle, query[0].MaxEmpNo, query[0].MaxClientNo, query[0].Start, query[0].End, query[0].Rate);


        //    return 0;
        //}







        // GET: Services/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServId,ServTitle,Rate,MaxEmpNo,MaxClientNo")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServId,ServTitle,Rate,MaxEmpNo,MaxClientNo")] Service service)
        {
            if (id != service.ServId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServId))
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
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .FirstOrDefaultAsync(m => m.ServId == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.FindAsync(id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServId == id);
        }
    }
}
