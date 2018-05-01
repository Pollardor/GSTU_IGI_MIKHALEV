using IGILab1Norm;
using lab2.Extensions;
using lab2.Extensions.Filters;
using lab2.Models;
using lab2.Models.Filters;
using lab2.Models.Sorts;
using Lab2.Utils;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Controllers
{
    [LoggerFilter]
    [ExceptionFilter]
    public class CarController : Controller
    {
        private RentContext _db;

        public CarController(RentContext db)
        {
            _db = db;
        }

        [HttpPost]
        [SetToSession("Car filter")]
        public IActionResult Index(CarFilter carFilter)
        {
            CarsViewModel viewModel = new CarsViewModel();

            var sessionSort = HttpContext.Session.Get("Car sort");
            if(sessionSort != null && sessionSort.Count > 0)
            {
                CarSort.State currentSort = (CarSort.State)Enum.Parse(typeof(CarSort.State), sessionSort["sortState"]);
                viewModel.CarSort = new CarSort(currentSort);
            }
            viewModel.CarFilter = carFilter;

            SetCars(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        [SetToSession("Car sort")]
        public IActionResult Index(CarSort.State sortState = CarSort.State.None)
        {
            CarsViewModel viewModel = new CarsViewModel();

            var sessionFilter = HttpContext.Session.Get("Car filter");
            if (sessionFilter != null)
            {
                viewModel.CarFilter = Converter.DictionaryToObject<CarFilter>(sessionFilter);
            }
            viewModel.CarSort = new CarSort(sortState);

            SetCars(viewModel);

            return View(viewModel);
        }

        private void SetCars(CarsViewModel viewModel)
        {
            var cars = _db.Cars.ToList();
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
            viewModel.Cars = cars;
        }
    }
}
