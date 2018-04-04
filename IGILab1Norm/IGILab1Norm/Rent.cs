using System;
using System.Collections.Generic;
using System.Text;

namespace IGILab1Norm
{
    class Rent
    {
        public int RentID { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime DateGet { get; set; }
        public int CarID { get; set; }
        public virtual Car Car { get; set; }
        public int ClientID { get; set; }
        public virtual Client Client { get; set; }
        public int RentPrice { get; set; }
        public bool Paid { get; set; }
        public string WorkerFIO { get; set; }
        public override string ToString()
        {
            return "[ ID = " + RentID + ". RentDate = " + RentDate.ToShortDateString() + ". DateGet = " + DateGet.ToShortDateString()
                + ". CarID = " + Car.CarID + ". ClientId = " + Client.Id + ". RentPrice = " + RentPrice + ". Paid = " + Paid.ToString() +
                ". WorkerFIO = " + WorkerFIO + ". ]";
        }
    }
}
