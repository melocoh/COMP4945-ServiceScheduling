﻿using cstestproject2.Models;
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

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        public IActionResult EmployeeRegistration()
        {
            ViewBag.CertificationTypes = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Hamburger University Degree", Value = "1" },
                new SelectListItem() { Text = "First Aid Certification", Value = "2" },
                new SelectListItem() { Text = "Scuba Diving Certification", Value = "3" },
                new SelectListItem() { Text = "Nurse Practitioning Certification", Value = "4" }
            };

            return View();
        }

        public IActionResult test()
        {
            return View();
        }

        public IActionResult EmployeeShift()
        {
            return View();
        }

        public IActionResult EmployeeShiftForm()
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
