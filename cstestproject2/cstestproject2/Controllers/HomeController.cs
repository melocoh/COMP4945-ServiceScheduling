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
        private bool loggedin = false;

        public HomeController(ILogger<HomeController> logger, AppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(Employee credentials)
        {
            Employee account = _context.Employees.Where(emp => emp.Email == credentials.Email).FirstOrDefault<Employee>();
            ViewBag.AccountName = account.FullName;

            HttpContext.Session.SetInt32("empID", account.EmpId);
            return View("LoggedIn");
        }

        //CLIENT SIDE OF LOGGING IN
        //register works but not login
        public IActionResult LoginClient(Client credentials)
        {
            Client account = _context.Clients.Where(emp => emp.Email == credentials.Email).FirstOrDefault<Client>();
            ViewBag.AccountName = account.FullName;

            HttpContext.Session.SetInt32("clientID", account.ClientId);
            return View("LoggedInClient");
        }

        [HttpPost]
        public IActionResult SubmitRegistration(Employee formData)
        {
            _context.Add(formData);
            _context.SaveChanges();
            
            return RedirectToAction("Login", formData);
        }

        [HttpPost]
        public IActionResult SubmitRegistrationClient(Client formData)
        {
            _context.Add(formData);
            _context.SaveChanges();

            return RedirectToAction("LoginClient", formData);
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

            ViewBag.Locations = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Burnaby", Value = "Burnaby" },
                new SelectListItem() { Text = "Richmond", Value = "Richmond" },
                new SelectListItem() { Text = "Vancouver", Value = "Vancouver" }
            };

            ViewBag.ServiceTypes = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Teach", Value = "Teach" },
                new SelectListItem() { Text = "Chew food", Value = "Chew food" },
                new SelectListItem() { Text = "Rap", Value = "Rap" },
                new SelectListItem() { Text = "Provide vaccine", Value = "Provide vaccine" }
            };

            return View();
        }

        public IActionResult Profile()
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
