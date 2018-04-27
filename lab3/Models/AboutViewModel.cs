using IGILab1Norm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class AboutViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public Car Car { get; set; }
    }
}
