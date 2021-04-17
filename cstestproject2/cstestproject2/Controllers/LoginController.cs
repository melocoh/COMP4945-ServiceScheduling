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
    public class LoginController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppContext _context;

        public LoginController(ILogger<HomeController> logger, AppContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
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

        public IActionResult EmployeeSignIn()
        {
            return View();
        }

        public IActionResult Login(Employee credentials)
        {

            Employee account = _context.Employees.Where(emp => emp.Email == credentials.Email).FirstOrDefault<Employee>();
            if (account != null)
            {
                ViewBag.AccountName = account.FullName;
                HttpContext.Session.SetInt32("empID", account.EmpId);
                return View("LoggedIn");
            }

            ModelState.AddModelError("Email", "User with that email not found.");
            return View("EmployeeSignIn");
        }

        [HttpPost]
        public IActionResult SubmitRegistration(Employee formData)
        {
            _context.Add(formData);
            _context.SaveChanges();
            HttpContext.Session.SetInt32("empID", formData.EmpId);
            return RedirectToAction("Login", formData);
        }

        
    }
}
