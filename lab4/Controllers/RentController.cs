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
    public class RentController : Controller
    {
        private RentContext _db;

        public RentController(RentContext db)
        {
            _db = db;
        }

        [HttpPost]
        [SetToSession("Rent filter")]
        public IActionResult Index(RentFilter rentFilter)
        {
            RentsViewModel viewModel = new RentsViewModel();

            var sessionSort = HttpContext.Session.Get("Rent sort");
            if (sessionSort != null && sessionSort.Count > 0)
            {
                RentSort.State currentSort = (RentSort.State)Enum.Parse(typeof(RentSort.State), sessionSort["sortState"]);
                viewModel.RentSort = new RentSort(currentSort);
            }
            viewModel.RentFilter = rentFilter;

            SetRents(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        [SetToSession("Rent sort")]
        public IActionResult Index(RentSort.State sortState = RentSort.State.None)
        {
            RentsViewModel viewModel = new RentsViewModel();

            var sessionFilter = HttpContext.Session.Get("Rent filter");
            if (sessionFilter != null)
            {
                viewModel.RentFilter = Converter.DictionaryToObject<RentFilter>(sessionFilter);
            }
            viewModel.RentSort = new RentSort(sortState);

            SetRents(viewModel);

            return View(viewModel);
        }

        private void SetRents(RentsViewModel viewModel)
        {
            var rents = _db.Rents.ToList();
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
            viewModel.Rents = rents;
        }
    }
}
