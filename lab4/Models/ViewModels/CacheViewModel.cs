using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IGILab1Norm;

namespace lab2.Models
{
    public class CacheViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public IEnumerable<Rent> Rents { get; set; }
    }
}
