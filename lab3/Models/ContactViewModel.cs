using IGILab1Norm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class ContactViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public Client Client { get; set; }
    }
}
