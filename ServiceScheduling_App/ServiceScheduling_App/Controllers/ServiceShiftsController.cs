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

    public class ServiceShiftType
    {
        public int id;
        public string servTitle;
        public string location;
        public DayOfWeek dayOfWeek;
        public TimeSpan startTime;
        public TimeSpan endTime;

        public ServiceShiftType(int id, string servTitle, string location, DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            this.id = id;
            this.servTitle = servTitle;
            this.location = location;
            this.dayOfWeek = dayOfWeek;
            this.startTime = startTime;
            this.endTime = endTime;
        }
    }

    public class ServiceShiftsController : Controller
    {
        private readonly AppContext _context;

        public ServiceShiftsController(AppContext context)
        {
            _context = context;


            var testData = GetServiceShiftTypeDetailsList();
            testData = testData;
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
