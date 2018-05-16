using IGILab1Norm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.ViewModels
{
    public class PartialViewModel
    {
        public IEnumerable<Car> Cars { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
