using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }   

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult getEmail(string email)
        {
            string result = email + " succes";
            
            return Json(result);
        }
        [HttpGet]
        public IActionResult getInfo(string info)
        {
            string result = info + " succes";
            
            return Json(result);
        }
    }
}
