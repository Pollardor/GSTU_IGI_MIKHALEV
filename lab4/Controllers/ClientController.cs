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
    public class ClientController : Controller
    {

        private RentContext _db;

        public ClientController(RentContext db)
        {
            _db = db;
        }

        [HttpPost]
        [SetToSession("Client filter")]
        public IActionResult Index(ClientFilter clientFilter)
        {
            ClientsViewModel viewModel = new ClientsViewModel();

            var sessionSort = HttpContext.Session.Get("Client sort");
            if (sessionSort != null && sessionSort.Count > 0)
            {
                ClientSort.State currentSort = (ClientSort.State)Enum.Parse(typeof(ClientSort.State), sessionSort["sortState"]);
                viewModel.ClientSort = new ClientSort(currentSort);
            }
            viewModel.ClientFilter = clientFilter;

            SetClients(viewModel);

            return View(viewModel);
        }

        [HttpGet]
        [SetToSession("Client sort")]
        public IActionResult Index(ClientSort.State sortState = ClientSort.State.None)
        {
            ClientsViewModel viewModel = new ClientsViewModel();

            var sessionFilter = HttpContext.Session.Get("Client filter");
            if (sessionFilter != null)
            {
                viewModel.ClientFilter = Converter.DictionaryToObject<ClientFilter>(sessionFilter);
            }
            viewModel.ClientSort = new ClientSort(sortState);

            SetClients(viewModel);

            return View(viewModel);
        }

        private void SetClients(ClientsViewModel viewModel)
        {
            var clients = _db.Clients.ToList();
            switch (viewModel.ClientSort.Models.CurrentSort)
            {
                case ClientSort.State.FIOAsc:
                    clients = clients.OrderBy(t => t.ClienFIO).ToList();
                    break;
                case ClientSort.State.FIODesc:
                    clients = clients.OrderByDescending(t => t.ClienFIO).ToList();
                    break;
            }

            string clientFIO = viewModel.ClientFilter.ClientFIO;
            string passport = viewModel.ClientFilter.PassNum;
            string phone = viewModel.ClientFilter.Phone;
            if (clientFIO != null)
                clients = clients.Where(t => t.ClienFIO.Contains(clientFIO)).ToList();
            if (passport != null)
                clients = clients.Where(t => t.PassNum.Contains(passport)).ToList();
            if (phone != null)
                clients = clients.Where(t => t.Phone.Contains(phone)).ToList();
            viewModel.Clients = clients;
        }

    }
}
