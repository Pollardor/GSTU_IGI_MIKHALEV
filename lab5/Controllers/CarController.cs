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
    [Authorize(Roles = "admin, user")]
    public class CarController : Controller
    {
        private RentContext _db;

        public CarController(RentContext db)
        {
            _db = db;
        }

        [HttpPost]
        [SetToSession("Car filter")]
        public IActionResult Index(CarFilter carFilter, int page = 1)
        {
            CarsViewModel viewModel = new CarsViewModel();

            var sessionSort = HttpContext.Session.Get("Car sort");
            if(sessionSort != null && sessionSort.Count > 0)
            {
                CarSort.State currentSort = (CarSort.State)Enum.Parse(typeof(CarSort.State), sessionSort["sortState"]);
                HttpContext.Session.Set("Car sort", currentSort);
                viewModel.CarSort = new CarSort(currentSort);
            }
            viewModel.CarFilter = carFilter;

            SetCars(viewModel, page);

            return View(viewModel);
        }

        [HttpGet]
        [SetToSession("Car sort")]
        public IActionResult Index(CarSort.State sortState = CarSort.State.None, int page = 1)
        {
            CarsViewModel viewModel = new CarsViewModel();

            var sessionFilter = HttpContext.Session.Get("Car filter");
            var sessionSort = HttpContext.Session.Get("Car sort");
            CarSort.State currentSort = CarSort.State.None;
            if (sessionSort != null && sessionSort.Count > 0)
            {
                currentSort = (CarSort.State)Enum.Parse(typeof(CarSort.State), sessionSort["sortState"]);
                HttpContext.Session.Set("Car sort", currentSort);
                viewModel.CarSort = new CarSort(currentSort);
            }
            if (sessionFilter != null)
            {
                viewModel.CarFilter = Converter.DictionaryToObject<CarFilter>(sessionFilter);
                HttpContext.Session.Set("Car filter", viewModel.CarFilter);
            }
            if (sortState == CarSort.State.None && currentSort != CarSort.State.None)
                sortState = currentSort;
            viewModel.CarSort = new CarSort(sortState);

            SetCars(viewModel, page);

            return View(viewModel);
        }

        private void SetCars(CarsViewModel viewModel, int page)
        {
            var cars = _db.Cars.ToList();
            int pageSize = 15;
            switch (viewModel.CarSort.Models.CurrentSort)
            {
                case CarSort.State.AgeAsc:
                    cars = cars.OrderBy(t => t.CarAge).ToList();
                    break;
                case CarSort.State.AgeDesc:
                    cars = cars.OrderByDescending(t => t.CarAge).ToList();
                    break;
                case CarSort.State.DateTOAsc:
                    cars = cars.OrderBy(t => t.DateTO).ToList();
                    break;
                case CarSort.State.DateTODesc:
                    cars = cars.OrderByDescending(t => t.DateTO).ToList();
                    break;
                case CarSort.State.DayPriceAsc:
                    cars = cars.OrderBy(t => t.DayPrice).ToList();
                    break;
                case CarSort.State.DayPriceDesc:
                    cars = cars.OrderByDescending(t => t.DayPrice).ToList();
                    break;
                case CarSort.State.MarkAsc:
                    cars = cars.OrderBy(t => t.CarMark).ToList();
                    break;
                case CarSort.State.MarkDesc:
                    cars = cars.OrderByDescending(t => t.CarMark).ToList();
                    break;
                case CarSort.State.MileageAsc:
                    cars = cars.OrderBy(t => t.Mileage).ToList();
                    break;
                case CarSort.State.MileageDesc:
                    cars = cars.OrderByDescending(t => t.Mileage).ToList();
                    break;
                case CarSort.State.PriceAsc:
                    cars = cars.OrderBy(t => t.CarPrice).ToList();
                    break;
                case CarSort.State.PriceDesc:
                    cars = cars.OrderByDescending(t => t.CarPrice).ToList();
                    break;
            }

            try
            {
                string strYear = viewModel.CarFilter.YearTO;
                string strPrice = viewModel.CarFilter.DayPrice;
                if (!String.IsNullOrEmpty(strYear))
                {
                    int year = Int32.Parse(strYear);
                    cars = cars.Where(t => t.DateTO.Year == year).ToList();
                }
                if (!String.IsNullOrEmpty(strPrice))
                { 
                    int price = Int32.Parse(strPrice);
                    cars = cars.Where(t => t.DayPrice == price).ToList();
                }
            } catch (Exception ex)
            {

            }
            string mechFIO = viewModel.CarFilter.MechFIO;
            if(mechFIO != null)
                cars = cars.Where(t => t.MechFIO.Contains(mechFIO)).ToList();
            var count = cars.Count;
            var items = cars.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            
            viewModel.PageViewModel = pageViewModel;
            viewModel.Cars = items;
            Car car = new Car();
            ViewData["AddingCar"] = car;
        }

        [HttpPost]
        public IActionResult AddNewCar(Car car)
        {
            _db.Cars.Add(car);
            _db.SaveChanges();
            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.Cars.Where(x => x.CarID == id).FirstOrDefault();
            _db.Cars.Remove(item);
            _db.SaveChanges();

            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult SaveChanges(Car car)
        {
            _db.Cars.Update(car);
            _db.SaveChanges();
            return Redirect("Index");
        }


        [HttpPost]
        public IActionResult Edit(int id)
        {
            var item = _db.Cars.Where(x => x.CarID == id).FirstOrDefault();

            return View(item);
        }
    }
}
