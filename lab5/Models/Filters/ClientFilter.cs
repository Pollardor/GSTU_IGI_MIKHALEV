using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab2.Models.Filters
{
    public class ClientFilter
    {
        public string ClientFIO { get; set; }
        public string PassNum { get; set; }
        public string Phone { get; set; }

        public ClientFilter()
        {
            ClientFIO = "";
            PassNum = "";
            Phone = "";
        }
    }
}
