using IGILab1Norm;
using lab2.Models.Filters;
using lab2.Models.Sorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class CarsViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public CarFilter CarFilter { get; set; }
        public CarSort CarSort { get; set; }

        public CarsViewModel()
        {
            CarFilter = new CarFilter();
            CarSort = new CarSort();
        }
    }
}
