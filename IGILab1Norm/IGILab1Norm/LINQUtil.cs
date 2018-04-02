using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGILab1Norm
{
    class LINQUtil
    {
        private int selectNumber = 5;
        private RentContext db;
        private static LINQUtil instanse;
        public LINQUtil()
        {
            db = new RentContext();
            DbInitializer.Initialize(db);
        }

        public static LINQUtil LINQHandler()
        {
            if (instanse == null)
                instanse = new LINQUtil();
            return instanse;
        }

        public RentContext GetDatabase { get { return db; } }

        public static void Print(string caption, IEnumerable items)
        {
            Console.WriteLine(caption);
            foreach (var item in items)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine();
        }


        public ICollection SelectCars()
        {
            var cars = db.Cars
                .OrderBy(car => car.CarMark);
            return cars.Take(selectNumber).ToList();
        }

        public ICollection SelectCarsByMark(string mark)
        {
            var cars = db.Cars
               .Where(car => car.CarMark.Contains(mark))
               .OrderBy(car => car.CarID);
            return cars.Take(selectNumber).ToList();
        }

        public ICollection SelectClients()
        {
            var clients = db.Clients
                .OrderBy(client => client.ClienFIO);
            return clients.Take(selectNumber).ToList();
        }

        public ICollection SelectClientsByName(string name)
        {
            var clients = db.Clients
                .Where(client => client.ClienFIO.Contains(name))
                .OrderBy(client => client.ClienFIO);
            return clients.Take(selectNumber).ToList();
        }

        public ICollection SelectRentSumByClientName()
        {
            var tours = db.Rents
                .Include(t => t.Car)
                .Include(t => t.Client)
                .GroupBy(t => t.Client.ClienFIO, t => t.RentPrice)
                .Select(gr => new
                {
                    ClientFIO = gr.Key,
                    Summ = gr.Sum()
                })
                .OrderBy(t => t.ClientFIO);
            return tours.Take(selectNumber).ToList();
        }

        public ICollection SelectJoinedRents()
        {
            var rents = db.Rents
                .Include(t => t.Car)
                .Include(t => t.Client)
                .Select(rent => new
                {
                    Id = rent.RentID,
                    Price = rent.RentPrice,
                    StartDate = rent.RentDate,
                    EndDate = rent.DateGet,
                    CarMark = rent.Car.CarMark,
                    Client = rent.Client.ClienFIO,
                })
                .OrderBy(t => t.Id);
            return rents.Take(selectNumber).ToList();
        }

        public ICollection SelectRentByCarMark(string mark)
        {
            var tours = db.Rents
                .Include(t => t.Car)
                .Include(t => t.Client)
                .Where(rent => rent.Car.CarMark.Contains(mark))
                .OrderBy(rent => rent.RentID);
            return tours.Take(selectNumber).ToList();
        }

        public void InsertCar(Car car)
        {
            db.Cars.Add(car);
            db.SaveChanges();
        }

        public void InsertRent(Rent rent)
        {   
            db.Rents.Add(rent);
            db.SaveChanges();
        }

        public void DeleteClientByName(string clientFIO)
        {
            var Clients = db.Clients
                .Where(client => client.ClienFIO.Equals(clientFIO));
            var rents = db.Rents
                .Include(rent => rent.Client)
                .Where(rent => rent.Client.ClienFIO.Equals(clientFIO));

            db.Rents.RemoveRange(rents);
            db.SaveChanges();

            db.Clients.RemoveRange(Clients);
            db.SaveChanges();
        }

        public void DeleteRentById(int rentId)
        {
            var rents = db.Rents
               .Where(rent => rent.RentID == rentId);

            db.Rents.RemoveRange(rents);
            db.SaveChanges();
        }

        public void UpdateClient(Client newClient)
        {
            var oldClient = db.Clients
                .Where(c => c.Id == newClient.Id)
                .FirstOrDefault();

            if (oldClient != null)
            {
                oldClient.ClienFIO = newClient.ClienFIO;
                oldClient.Birthday = newClient.Birthday;
                oldClient.Phone = newClient.Phone;
            };

            db.SaveChanges();
        }

    }
}
