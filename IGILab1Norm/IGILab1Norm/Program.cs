using System;
using System.Collections;
using System.Collections.Generic;

namespace IGILab1Norm
{
    class Program
    {
        static void Main(string[] args)
        {
            LINQUtil lINQUtil = LINQUtil.LINQHandler();
            LINQUtil.Print("All cars select: ", lINQUtil.SelectCars());
            Console.ReadKey();
            LINQUtil.Print("All clients select: ", lINQUtil.SelectClients());
            Console.ReadKey();
            LINQUtil.Print("Select clients by name: ", lINQUtil.SelectClientsByName("Бо"));
            Console.ReadKey();
            LINQUtil.Print("Select rent sum by client name: ", lINQUtil.SelectRentSumByClientName());
            Console.ReadKey();
            LINQUtil.Print("Select rents with information about cars and clients: ", lINQUtil.SelectJoinedRents());
            Console.ReadKey();
            Car addCar = new Car() { CarMark = "LADA", RegNum = "1234 AB-3", BodyNum = "LADA162541276", EngNum = "126754762145L",
                Mileage = 20000, CarPrice = 10, DayPrice = 999, DateTO = DateTime.MinValue, MechFIO = "Андрей Мельченко", CarAge = 999 };
            Console.WriteLine("Insert car " + addCar.ToString());
            lINQUtil.InsertCar(addCar);
            LINQUtil.Print("Check: ", lINQUtil.SelectCarsByMark("LADA"));
            Console.ReadKey();
            Client updateClient = new Client() { Id = 178, ClienFIO = "Иванов Иван", ClientSex = "Male", Birthday = DateTime.Now.AddYears(-20),
                Adres = "Советская д. 234 кв. 5", PassNum = "HB1234568", Phone = "+375446433158" };
            lINQUtil.UpdateClient(updateClient);
            LINQUtil.Print("Updated client: ", lINQUtil.SelectClientsByName("Иванов"));
            Console.ReadKey();
            Rent addRent = new Rent() { RentDate = DateTime.Now, DateGet = DateTime.Now.AddDays(30), Car = addCar,
                Client = updateClient, RentPrice = 678, Paid = false, WorkerFIO = "Алын Малиев" };
            lINQUtil.InsertRent(addRent);
            LINQUtil.Print("Inserted rent: ", lINQUtil.SelectRentByCarMark("LADA"));
            Console.ReadKey();
            lINQUtil.DeleteClientByName("Иванов");
            LINQUtil.Print("Delete client: ", lINQUtil.SelectClientsByName("Иванов"));
            Console.ReadKey();
            lINQUtil.DeleteRentById(176);
            LINQUtil.Print("Delete rent by id 176: ", lINQUtil.SelectJoinedRents());
            Console.ReadKey();
        }
    }
}
