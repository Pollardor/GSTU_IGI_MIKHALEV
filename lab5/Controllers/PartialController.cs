using IGILab1Norm;
using lab2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Controllers
{
    public class PartialController : Controller
    {

        private RentContext _db;

        public PartialController(RentContext db)
        {
            this._db = db;
        }

        public PartialViewResult Index()
        {
            PartialViewModel viewModel = new PartialViewModel
            {
                Cars = _db.Cars.ToList()
            };
            return PartialView(viewModel);
        }
    }
}
