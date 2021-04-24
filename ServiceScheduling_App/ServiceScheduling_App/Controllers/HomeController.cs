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

        public HomeController(ILogger<HomeController> logger, AppContext context)
        {   
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Login(Employee credentials)
        {
            Employee account = _context.Employees.Where(emp => emp.Email == credentials.Email && emp.Password == credentials.Password).FirstOrDefault<Employee>();
            if (account != null)
            {
                ViewBag.ShowLogOut = true;
                ViewBag.AccountName = account.FullName;
                HttpContext.Session.SetInt32("empID", account.EmpId);
                
                return View("LoggedIn");
            }

            ModelState.AddModelError("Email", "Incorrect credentials");
            return View("EmployeeSignIn");
        }

        //CLIENT SIDE OF LOGGING IN
        //register works but not login
        public IActionResult LoginClient(Client credentials)
        {
            Client account = _context.Clients.Where(client => client.Email == credentials.Email).FirstOrDefault<Client>();
            if (account != null)
            {
                ViewBag.ShowLogOut = true;
                ViewBag.AccountName = account.FullName;
                HttpContext.Session.SetInt32("clientID", account.ClientId);
                
                return View("LoggedInClient");
            }
            return View("ClientSignIn");
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

        //Builds Certification List Viewbag
        private List<SelectListItem> GetCertificateTypeList()
        {
            // grabs list of JobType objects from the JobType table
            List<CertificationType> certificationTypeList = _context.CertificationTypes.ToList<CertificationType>();

            //certificationTypeList.Distinct();

            // converts jobTypeList into selectListItem list
            List<SelectListItem> list = certificationTypeList.ConvertAll<SelectListItem>(item =>
            {
                return new SelectListItem()
                {
                    Text = item.CertTitle,
                    Value = item.CertId.ToString(),
                    Selected = false
                };
            });
            return list;
        }

        [HttpPost]
        public IActionResult SubmitRegistration(Employee formData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formData);
                _context.SaveChanges();
            }

            return RedirectToAction("Login", formData);
        }

        [HttpPost]
        public IActionResult SubmitRegistrationClient(Client formData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formData);
                _context.SaveChanges();
            }

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

            ViewBag.CertificationTypes = GetCertificateTypeList();

            return View();
        }

        public IActionResult Profile()
        {
            if(HttpContext.Session.GetInt32("empID") != null)
            {
                ViewBag.ShowLogOut = true;
                return RedirectToAction("Details", "Employees");
            } else if (HttpContext.Session.GetInt32("clientID") != null)
            {
                ViewBag.ShowLogOut = true;
                return RedirectToAction("Details", "Clients");
            }
            ViewBag.AlertMessage = "Log-in to view profile information.";
            return View("RoleSelection");
        }

        public async Task<IActionResult> Main()
        {
            // if logged in as employee
            if (HttpContext.Session.GetInt32("empID") != null)
            {
                ViewBag.ShowLogOut = true;

                int id = (int)HttpContext.Session.GetInt32("empID");
                var account = await _context.Employees.FindAsync(id);

                if (account == null)
                {
                    return NotFound();
                }

                ViewBag.AccountName = account.FullName;

                return View("LoggedIn", "Home");
            }
            else if (HttpContext.Session.GetInt32("clientID") != null) // if logged in as client
            {
                ViewBag.ShowLogOut = true;
                int id = (int)HttpContext.Session.GetInt32("clientID");
                var account = await _context.Clients.FindAsync(id);

                if (account == null)
                {
                    return NotFound();
                }

                ViewBag.AccountName = account.FullName;

                return View("LoggedInClient", "Home");
            }

            return View("Index", "Home");
        }

        public void GetNotifications()
        {

        }
    }
}
