using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using ServiceScheduling_App.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceScheduling_App.Controllers
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

        // Converts List to SelectListItems
        // @returns a list with the Text = service titles and Value = service Id
        private List<SelectListItem> GetJobTypeList()
        {
            // grabs list of JobType objects from the JobType table
            List<JobType> jobTypeList = _context.JobTypes.ToList<JobType>();

            jobTypeList.Distinct();

            // converts jobTypeList into selectListItem list
            List<SelectListItem> list = jobTypeList.ConvertAll<SelectListItem>(item =>
            {
                return new SelectListItem()
                {
                    Text = item.JobTitle,
                    Value = item.JobId.ToString(),
                    Selected = false
                };
            });
            return list;
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

        public IActionResult EmployeeRegistration()
        {
            ViewBag.JobTypes = GetJobTypeList();


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
    }
}
