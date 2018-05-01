using IGILab1Norm;
using lab2.Models.Filters;
using lab2.Models.Sorts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class ClientsViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public ClientSort ClientSort { get; set; }
        public ClientFilter ClientFilter { get; set; }

        public ClientsViewModel()
        {
            ClientFilter = new ClientFilter();
            ClientSort = new ClientSort();
        }
    }
}
