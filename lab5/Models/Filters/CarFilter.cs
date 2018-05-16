using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.Filters
{
    public class CarFilter
    {
        public string YearTO { get; set; }
        public string DayPrice { get; set; }
        public string MechFIO { get; set; }

        public CarFilter()
        {
            YearTO = "";
            DayPrice = "";
            MechFIO = "";
        }
    }
}
