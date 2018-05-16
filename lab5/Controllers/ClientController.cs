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
    public class ClientController : Controller
    {

        private RentContext _db;

        public ClientController(RentContext db)
        {
            _db = db;
        }

        [HttpPost]
        [SetToSession("Client filter")]
        public IActionResult Index(ClientFilter clientFilter, int page = 1)
        {
            ClientsViewModel viewModel = new ClientsViewModel();

            var sessionSort = HttpContext.Session.Get("Client sort");
            if (sessionSort != null && sessionSort.Count > 0)
            {
                ClientSort.State currentSort = (ClientSort.State)Enum.Parse(typeof(ClientSort.State), sessionSort["sortState"]);
                HttpContext.Session.Set("Client sort", currentSort);
                viewModel.ClientSort = new ClientSort(currentSort);
            }
            viewModel.ClientFilter = clientFilter;

            SetClients(viewModel, page);

            return View(viewModel);
        }

        [HttpGet]
        [SetToSession("Client sort")]
        public IActionResult Index(ClientSort.State sortState = ClientSort.State.None, int page = 1)
        {
            ClientsViewModel viewModel = new ClientsViewModel();

            var sessionFilter = HttpContext.Session.Get("Client filter");
            var sessionSort = HttpContext.Session.Get("Client sort");
            ClientSort.State currentSort = ClientSort.State.None;
            if (sessionSort != null && sessionSort.Count > 0)
            {
                currentSort = (ClientSort.State)Enum.Parse(typeof(ClientSort.State), sessionSort["sortState"]);
                HttpContext.Session.Set("Client sort", currentSort);
                viewModel.ClientSort = new ClientSort(currentSort);
            }
            if (sessionFilter != null)
            {
                viewModel.ClientFilter = Converter.DictionaryToObject<ClientFilter>(sessionFilter);
                HttpContext.Session.Set("Client filter", viewModel.ClientFilter);
            }
            if (sortState == ClientSort.State.None && currentSort != ClientSort.State.None)
                sortState = currentSort;
            viewModel.ClientSort = new ClientSort(sortState);

            SetClients(viewModel, page);

            return View(viewModel);
        }

        private void SetClients(ClientsViewModel viewModel, int page)
        {
            var clients = _db.Clients.ToList();
            int pageSize = 15;
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
            var count = clients.Count;
            var items = clients.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            viewModel.PageViewModel = pageViewModel;
            viewModel.Clients = items;
            Client client = new Client();
            ViewData["AddingClient"] = client;
        }

        [HttpPost]
        public IActionResult AddNewClient(Client client)
        {
            _db.Clients.Add(client);
            _db.SaveChanges();
            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var item = _db.Clients.Where(x => x.Id == id).FirstOrDefault();
            _db.Clients.Remove(item);
            _db.SaveChanges();

            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult SaveChanges(Client client)
        {
            _db.Clients.Update(client);
            _db.SaveChanges();
            return Redirect("Index");
        }


        [HttpPost]
        public IActionResult Edit(int id)
        {
            var item = _db.Clients.Where(x => x.Id == id).FirstOrDefault();

            return View(item);
        }
    }
}
