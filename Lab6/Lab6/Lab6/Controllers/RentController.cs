using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IGILab1Norm;
using Lab6.ViewModels;

namespace Lab6.Controllers
{
    [Route("api/[controller]")]
    public class RentController : Controller
    {
        private readonly RentContext _context;
        public RentController(RentContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Produces("application/json")]
        public List<RentViewModel> Get()
        {
            var ovm = _context.Rents.Include(t => t.Car).Include(f => f.Client).Select(o =>
                new RentViewModel
                {
                    RentID = o.RentID,
                    CarID = o.CarID,
                    ClientID = o.ClientID,
                    Car = o.Car,
                    Client = o.Client,
                    RentDate = o.RentDate,
                    DateGet = o.DateGet,
                    RentPrice = o.RentPrice,
                    Paid = o.Paid,
                    WorkerFIO = o.WorkerFIO,
                    ClientName = o.Client.ClienFIO,
                    CarMark = o.Car.CarMark

                });
            return ovm.OrderByDescending(t => t.RentID).Take(20).ToList();
        }

        [HttpGet("cars")]
        [Produces("application/json")]
        public IEnumerable<Car> GetCars()
        {
            return _context.Cars.ToList();
        }

        [HttpGet("clients")]
        [Produces("application/json")]
        public IEnumerable<Client> GetClients()
        {
            return _context.Clients.ToList();
        }



        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Rent operation = _context.Rents.FirstOrDefault(x => x.RentID == id);
            if (operation == null)
                return NotFound();
            return new ObjectResult(operation);
        }


        [HttpPost]
        public IActionResult Post([FromBody]Rent operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }

            Car car = _context.Cars.ToList()[0];
            for (int i = 1; i < operation.CarID; i++)
                car = _context.Cars.ToList()[i];
            Client client = _context.Clients.ToList()[0];
            for (int i = 1; i < operation.ClientID; i++)
                car = _context.Cars.ToList()[i];
            operation.CarID = car.CarID;
            operation.Car = _context.Cars.Where(x => x.CarID == car.CarID).First();
            operation.ClientID = client.Id;
            operation.Client = _context.Clients.Where(x => x.Id == client.Id).First();
            _context.Rents.Add(operation);
            _context.SaveChanges();
            return Ok(operation);
        }


        [HttpPut]
        public IActionResult Put([FromBody]Rent operation)
        {
            if (operation == null)
            {
                return BadRequest();
            }
            if (!_context.Rents.Any(x => x.RentID == operation.RentID))
            {
                return NotFound();
            }
            Car car = _context.Cars.ToList()[0];
            for (int i = 1; i < operation.CarID; i++)
                car = _context.Cars.ToList()[i];
            Client client = _context.Clients.ToList()[0];
            for (int i = 1; i < operation.ClientID; i++)
                car = _context.Cars.ToList()[i];
            operation.CarID = car.CarID;
            operation.Car = _context.Cars.Where(x => x.CarID == car.CarID).First();
            operation.ClientID = client.Id;
            operation.Client = _context.Clients.Where(x => x.Id == client.Id).First();
            _context.Update(operation);
            _context.SaveChanges();


            return Ok(operation);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Rent operation = _context.Rents.FirstOrDefault(x => x.RentID == id);
            if (operation == null)
            {
                return NotFound();
            }
            _context.Rents.Remove(operation);
            _context.SaveChanges();
            return Ok(operation);
        }
    }
}
