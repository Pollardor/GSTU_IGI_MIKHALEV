using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lab2.Models;
using IGILab1Norm;
using Microsoft.Extensions.Caching.Memory;
using lab2.Extensions;

namespace lab2.Controllers
{
    public class HomeController : Controller
    {
        private RentContext db = new RentContext();

        public HomeController(RentContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
