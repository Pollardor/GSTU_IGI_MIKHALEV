using System;
using System.Collections.Generic;
using System.Text;


namespace IGILab1Norm
{
    class Car
    {
        public int CarID { get; set; }
        public string CarMark { get; set; }
        public string RegNum { get; set; }
        public string BodyNum { get; set; }
        public string EngNum { get; set; }
        public int Mileage { get; set; }
        public int CarPrice { get; set; }
        public int DayPrice { get; set; }
        public DateTime DateTO { get; set; }
        public string MechFIO { get; set; }
        public int CarAge { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
        public Car()
        {
            Rents = new List<Rent>();
        }

        public override string ToString()
        {
            return "[ ID = " + CarID + ". Mark = " + CarMark + ". RegNum = " + RegNum + ". BodyNum = " + BodyNum + ". EngNum = " + EngNum +
                ". Mileage = " + Mileage + ". CarPrice = " + CarPrice + ". DayPrice " + DayPrice + ". DateTO = " + DateTO.ToShortDateString() +
                ". MechFIO = " + MechFIO + ". CarAge = " + CarAge + ". ]";
        }

    }
}
