using IGILab1Norm;
using lab2.Extensions;
using lab2.Extensions.Filters;
using lab2.Models;
using lab2.Models.Filters;
using lab2.Models.Sorts;
using lab2.Models.ViewModels;
using lab2.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Controllers
{
    [LoggerFilter]
    [ExceptionFilter]
    [Authorize(Roles = "admin")]
    public class RentController : Controller
    {
        private RentContext _db;

        public RentController(RentContext db)
        {
            _db = db;
        }

        [HttpPost]
        [SetToSession("Rent filter")]
        public IActionResult Index(RentFilter rentFilter, int page = 1)
        {
            RentsViewModel viewModel = new RentsViewModel();

            var sessionSort = HttpContext.Session.Get("Rent sort");
            if (sessionSort != null && sessionSort.Count > 0)
            {
                RentSort.State currentSort = (RentSort.State)Enum.Parse(typeof(RentSort.State), sessionSort["sortState"]);
                HttpContext.Session.Set("Rent sort", currentSort);
                viewModel.RentSort = new RentSort(currentSort);
            }
            viewModel.RentFilter = rentFilter;

            SetRents(viewModel, page);

            return View(viewModel);
        }

        [HttpGet]
        [SetToSession("Rent sort")]
        public IActionResult Index(RentSort.State sortState = RentSort.State.None, int page = 1)
        {
            RentsViewModel viewModel = new RentsViewModel();

            var sessionFilter = HttpContext.Session.Get("Rent filter");
            var sessionSort = HttpContext.Session.Get("Rent sort");
            RentSort.State currentSort = RentSort.State.None;
            if (sessionSort != null && sessionSort.Count > 0)
            {
                currentSort = (RentSort.State)Enum.Parse(typeof(RentSort.State), sessionSort["sortState"]);
                HttpContext.Session.Set("Rent sort", currentSort);
                viewModel.RentSort = new RentSort(currentSort);
            }
            if (sessionFilter != null)
            {
                viewModel.RentFilter = Converter.DictionaryToObject<RentFilter>(sessionFilter);
                HttpContext.Session.Set("Rent filter",viewModel.RentFilter);
            }
            if (sortState == RentSort.State.None && currentSort != RentSort.State.None)
                sortState = currentSort;
            viewModel.RentSort = new RentSort(sortState);

            SetRents(viewModel, page);

            return View(viewModel);
        }

        private void SetRents(RentsViewModel viewModel, int page)
        {
            var rents = _db.Rents.ToList();
            int pageSize = 15;
            switch (viewModel.RentSort.Models.CurrentSort)
            {
                case RentSort.State.DateGetAsc:
                    rents = rents.OrderBy(t => t.DateGet).ToList();
                    break;
                case RentSort.State.DateGetDesc:
                    rents = rents.OrderByDescending(t => t.DateGet).ToList();
                    break;
                case RentSort.State.RentDateAsc:
                    rents = rents.OrderBy(t => t.RentDate).ToList();
                    break;
                case RentSort.State.RentDateDesc:
                    rents = rents.OrderByDescending(t => t.RentDate).ToList();
                    break;
                case RentSort.State.WorkerFIOAsc:
                    rents = rents.OrderBy(t => t.WorkerFIO).ToList();
                    break;
                case RentSort.State.WorkerFIODesc:
                    rents = rents.OrderByDescending(t => t.WorkerFIO).ToList();
                    break;
            }

            try
            {
                string strMonth = viewModel.RentFilter.MonthRent;
                if (!String.IsNullOrEmpty(strMonth))
                {
                    int month = Int32.Parse(strMonth);
                    rents = rents.Where(t => t.RentDate.Month == month).ToList();
                }
            }
            catch (Exception ex)
            {

            }
            string workerFIO = viewModel.RentFilter.WorkerFIO;
            if (workerFIO != null)
                rents = rents.Where(t => t.WorkerFIO.Contains(workerFIO)).ToList();

            var count = rents.Count;
            var items = rents.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            viewModel.PageViewModel = pageViewModel;
            viewModel.Rents = items;
            Rent rent = new Rent();
            ViewData["AddingRent"] = rent;
            ViewData["Cars"] = _db.Cars.ToList();
            ViewData["Clients"] = _db.Clients.ToList();
        }

        [HttpPost]
        public IActionResult AddNewRent(Rent rent, int client, int car)
        {
            rent.CarID = car;
            rent.Car = _db.Cars.Where(x => x.CarID == car).FirstOrDefault();
            rent.ClientID = client;
            rent.Client = _db.Clients.Where(x => x.Id == client).FirstOrDefault();
            _db.Rents.Add(rent);
            _db.SaveChanges();
            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.Rents.Where(x => x.RentID == id).FirstOrDefault();
            _db.Rents.Remove(item);
            _db.SaveChanges();

            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult SaveChanges(Rent rent, int client, int car)
        {
            rent.CarID = car;
            rent.Car = _db.Cars.Where(x => x.CarID == car).FirstOrDefault();
            rent.ClientID = client;
            rent.Client = _db.Clients.Where(x => x.Id == client).FirstOrDefault();
            _db.Rents.Update(rent);
            _db.SaveChanges();
            return Redirect("Index");
        }


        [HttpPost]
        public IActionResult Edit(int id)
        {
            var item = _db.Rents.Where(x => x.RentID == id).FirstOrDefault();
            ViewData["Cars"] = _db.Cars.ToList();
            ViewData["Clients"] = _db.Clients.ToList();

            return View(item);
        }
    }
}
