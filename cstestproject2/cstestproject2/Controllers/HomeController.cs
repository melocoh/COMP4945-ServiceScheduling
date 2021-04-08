using cstestproject2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        private bool loggedin = true;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (loggedin)
            {
                ViewBag.AccountName = "John Smith";
                return View("LoggedIn");
            }
            return View();
        }

        public IActionResult RoleSelection()
        {
            return View();
        }

        public IActionResult ClientSignIn()
        {
            return View();
        }

        public IActionResult ClientRegistration()
        {
            return View();
        }

        public IActionResult EmployeeSignIn()
        {
            return View();
        }

        public IActionResult Chart()
        {
            Console.WriteLine("In chart page");
            return View();
        }

        public IActionResult EmployeeRegistration()
        {
            ViewBag.CertificationTypes = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Hamburger University Degree", Value = "Hamburger University Degree" },
                new SelectListItem() { Text = "First Aid Certification", Value = "First Aid Certification" },
                new SelectListItem() { Text = "Scuba Diving Certification", Value = "Scuba Diving Certification" },
                new SelectListItem() { Text = "Nurse Practitioning Certification", Value = "Nurse Practitioning Certification" }
            };

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
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
