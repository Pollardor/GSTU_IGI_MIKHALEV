using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGILab1Norm
{
    static class DbInitializer
    {
        public static void Initialize(RentContext db)
        {
            db.Database.EnsureCreated();

            if (db.Rents.Any())
                return;

            int carNumber = 30,
                clientNumber = 30,
                rentNumber = 30;
            Random random = new Random(carNumber);

            string[] carMarks = { "BMW", "Mercedes", "Volvo", "Volga", "Lada", "Honda", "Mazda", "Toyota", "Ford", "Dodge",
                "Porshe", "Mazerati" };
            char[] regNumbersLetters = { 'А', 'В', 'Е', 'І', 'К', 'М', 'Н', 'О', 'Р', 'С', 'Т', 'Х' };
            string[] mechName = { "Андрей", "Артур", "Артем", "Антон", "Азамат", "Александр", "Рамзан", "Абрам" };
            string[] mechSurName = { "Скоробогатько", "Чепенко", "Волочаев", "Бубешко", "Порошенко", "Цекало", "Поперечный", "Усачев" };
            string carMark, mech, regNum, bodyNum, engNum;
            int mileage, carPrice, dayPrice, carAge;
            DateTime dateTO;

            for (int i = 1; i <= carNumber; i++)
            {
                carMark = carMarks[random.Next(carMarks.Length)];
                regNum = random.Next(1000, 9999) + " " + regNumbersLetters[random.Next(regNumbersLetters.Length)] + 
                    regNumbersLetters[random.Next(regNumbersLetters.Length)] + "-" + random.Next(0, 7);
                bodyNum = carMark.ToUpper() + random.Next(100000, 1000000);
                engNum = Math.Abs(bodyNum.GetHashCode()) + "" + bodyNum[0];
                mileage = random.Next(100000);
                carPrice = random.Next(1000000);
                dateTO = DateTime.Now.AddMonths(-1 * i);
                dayPrice = random.Next(1000);
                mech = mechName[random.Next(mechName.Length)] + " " + mechSurName[random.Next(mechSurName.Length)];
                carAge = random.Next(20);

                db.Cars.Add(new Car { CarMark = carMark, RegNum = regNum, BodyNum = bodyNum, EngNum = engNum,
                    Mileage = mileage, CarPrice = carPrice, DateTO = dateTO, DayPrice = dayPrice, MechFIO = mech, CarAge = carAge });
            }
            db.SaveChanges();

            string[] clientName = { "Богдан", "Берта", "Боб", "Василий", "Вацлав", "Вилли", "Виолетта", "Виктория" };
            string[] clientSurname = { "Сыпченко", "Шевченко", "Коваленко", "Овчаренко", "Петренко", "Иващенко", "Павленко", "Пономаренко" };
            string[] sex = { "Female", "Male" };
            string[] adressMass = { "Советская", "проспект Ленина", "проспект Победы", "Кирова", "70-лет БССР",
                "Богдана-Хмельницкого", "Луночарского", "5-я Междугородняя", "7-я Междугородняя", "9-я Междугородняя" };
            string[] phoneCode = { "+37525", "+37544", "+37529" };
            string[] passCodes = { "HB" };

            string clientFIO, clientSex, adress, phone, passNumber;
            DateTime birthday;

            for(int i = 1; i <= clientNumber; i++)
            {
                clientFIO = clientName[random.Next(clientName.Length)] + " " + clientSurname[random.Next(clientSurname.Length)];
                clientSex = sex[random.Next(sex.Length)];
                birthday = DateTime.Now.AddYears(-20 - i);
                adress = adressMass[random.Next(adressMass.Length)] + " д. " + random.Next(150) + " кв. " + random.Next(200);
                phone = phoneCode[random.Next(phoneCode.Length)] + random.Next(1000000, 9999999);
                passNumber = passCodes[random.Next(passCodes.Length)] + random.Next(1000000, 9999999); 

                db.Clients.Add(new Client() { ClienFIO = clientFIO, ClientSex = clientSex, Birthday = birthday,
                    Adres = adress, Phone = phone, PassNum = passNumber });
            }
            db.SaveChanges();

            string[] workerName = { "Баваль", "Бахт", "Лила", "Лоло", "Бахтало", "Тагар", "Патрина", "Раджи" };
            string[] workerSurname = { "Камидзе", "Гульчик", "Питерсон", "Бугш", "Путерсон", "Клопенко" };
            DateTime rentDate, dateGet;
            int rentPrice, carID, clientID;
            string worker;
            bool paid = false;

            for(int i = 0; i < rentNumber; i++)
            {
                rentDate = DateTime.Now.AddMonths(-2 * i);
                dateGet = DateTime.Now.AddDays(10 + i / 2);
                rentPrice = (dateGet - rentDate).Days * db.Cars.Local.ElementAt(i).DayPrice;
                carID = db.Cars.Local.ElementAt(random.Next(0, i == 0 ? i : i - 1)).CarID;
                clientID = db.Clients.Local.ElementAt(random.Next(0, i == 0 ? i : i - 1)).Id;
                worker = workerName[random.Next(workerName.Length)] + " " + workerSurname[random.Next(workerSurname.Length)];
                paid = random.Next() % 2 == 1;

                db.Rents.Add(new Rent() { RentDate = rentDate, DateGet = dateGet, CarID = db.Cars.Local.ElementAt(random.Next(0, i == 0 ? i : i - 1)).CarID,
                    ClientID = db.Clients.Local.ElementAt(random.Next(0, i == 0 ? i : i - 1)).Id, RentPrice = rentPrice, Paid = paid, WorkerFIO = worker });
            }
            db.SaveChanges();
        }
    }
}