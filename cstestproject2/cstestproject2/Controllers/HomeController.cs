using cstestproject2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace cstestproject2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppContext _context;

        public HomeController(ILogger<HomeController> logger, AppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var empID = HttpContext.Session.GetInt32("empID");
            if (empID != null)
            {
                Employee account = _context.Employees.Where(emp => emp.EmpId == empID).FirstOrDefault<Employee>();
                return RedirectToAction("Login", account);
            }
            return View();
        }

        

        public void GetNotifications()
        {
            HttpContext.Session.GetInt32("empID");
            var query = _context.Appointments
            .Join(_context.Services,
                   appointment => appointment.AppId,
            service => service.ServId,
            (appointment, service) => new
            {
                ServId = service.ServId,
                Start = appointment.StartDateTime,
                End = appointment.EndDateTime,
            }
            ).ToList().Where(appointServ => appointServ.Start > DateTime.Now);
        }
        
        public IActionResult Chart()
        {
            return View();
        }

        public IActionResult Profile()
        {
            return View();
        }

        public IActionResult test()
        {
            return View();
        }

        public IActionResult Service()
        {
            return View();
        }

        public IActionResult EmployeeShift()
        {
            return View();
        }


        public IActionResult Schedule()
        {
            return View();
        }
        public IActionResult EmployeeShiftForms()
        {
            return View();
        }

        public IActionResult Appointment()
        {
            return View("Index");
        }

        [HttpGet]
        public JsonResult ChartDetails()
        {
            Console.WriteLine("Called");
            return Json(new Chart("bar", new[] { 100, 200, 300 }));
        }

        public IActionResult Modal()
        {
            ViewBag.SelectOptions = new List<SelectListItem>()
            {
                new SelectListItem(){Text = "Option 1", Value = "1"},
                new SelectListItem(){Text = "Option 2", Value = "2"},
                new SelectListItem(){Text = "Option 3", Value = "3"},
            };
            return View();
        }

        public IActionResult BookingAppointment()
        {
            ViewBag.ServiceTypes = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Teach", Value = "Teach" },
                new SelectListItem() { Text = "Chew food", Value = "Chew food" },
                new SelectListItem() { Text = "Rap", Value = "Rap" },
                new SelectListItem() { Text = "Provide vaccine", Value = "Provide vaccine" }
            };

            ViewBag.Locations = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Burnaby", Value = "Burnaby" },
                new SelectListItem() { Text = "Richmond", Value = "Richmond" },
                new SelectListItem() { Text = "Vancouver", Value = "Vancouver" }
            };

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
