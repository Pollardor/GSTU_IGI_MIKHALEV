using IGILab1Norm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab6.ViewModels
{
    public class RentViewModel
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
        public string ClientName { get; set; }
        public string CarMark { get; set; }

    }
}
