using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LodgeDogDB.Context;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LodgeDogDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly mySampleDatabaseContext _context;

        public HomeController(mySampleDatabaseContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.ownerNum = new SelectList(_context.Owners, "Number", "Number");
            return View();
        }
    }
}