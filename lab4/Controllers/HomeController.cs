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
        private IMemoryCache memoryCache;

        public HomeController(RentContext db, IMemoryCache memoryCache)
        {
            this.db = db;
            this.memoryCache = memoryCache;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public IActionResult Cache()
        {
            CacheViewModel cachedViewModel = memoryCache.Get<CacheViewModel>("Cache elements");

            return View(cachedViewModel);
        }

        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
