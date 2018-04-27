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

        [HttpPost]
        public IActionResult Index(Rent rent)
        {
            HttpContext.Session.Set("Rent", rent);
            db.Rents.Add(rent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rents = db.Rents.ToList();
            Rent rent = HttpContext.Session.Get<Rent>("Rent");
            return View(new IndexViewModel() { Rents = rents, Rent = rent });
        }

        [HttpPost]
        public IActionResult About(Car car)
        {
            HttpContext.Session.Set("Car", car);
            db.Cars.Add(car);
            db.SaveChanges();
            return RedirectToAction("About");
        }

        [HttpGet]
        public IActionResult About()
        {
            var cars = db.Cars.ToList();
            Car car = HttpContext.Session.Get<Car>("Car");
            return View(new AboutViewModel() { Cars = cars, Car = car });
        }

        [HttpPost]
        public IActionResult Contact(Client client)
        {
            HttpContext.Response.Cookies.Set("Client", client);
            db.Clients.Add(client);
            db.SaveChanges();
            return RedirectToAction("Contact");
        }

        [HttpGet]
        public IActionResult Contact()
        {
            var clients = db.Clients.ToList();
            Client client = HttpContext.Request.Cookies.Get<Client>("Client");
            return View(new ContactViewModel() { Clients = clients, Client = client });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
