using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.Filters
{
    public class RentFilter
    {
        public string WorkerFIO { get; set; }
        public string MonthRent { get; set; }

        public RentFilter()
        {
            WorkerFIO = "";
            MonthRent = "";
        }
    }
}
