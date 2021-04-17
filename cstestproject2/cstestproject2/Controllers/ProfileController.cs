using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cstestproject2;
using cstestproject2.Models;

namespace cstestproject2.Controllers
{
    public class ProfileController : Controller
    {
    private readonly AppContext _context;

    public ProfileController(AppContext context)
    {
        _context = context;
    }

    public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("empID");
            Employee account = _context.Employees.FirstOrDefault(m => m.EmpId == id);
            return View(account);
        }
    }
}
