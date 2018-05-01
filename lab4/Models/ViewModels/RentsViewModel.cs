using IGILab1Norm;
using lab2.Models.Filters;
using lab2.Models.Sorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class RentsViewModel
    {
        public IEnumerable<Rent> Rents { get; set; }
        public RentFilter RentFilter { get; set; }
        public RentSort RentSort { get; set; }

        public RentsViewModel()
        {
            RentFilter = new RentFilter();
            RentSort = new RentSort();
        }
    }
}
