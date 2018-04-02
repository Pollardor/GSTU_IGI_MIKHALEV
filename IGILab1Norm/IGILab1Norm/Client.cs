using System;
using System.Collections.Generic;
using System.Text;


namespace IGILab1Norm
{
    class Client
    {
        public int Id { get; set; }
        public string ClienFIO { get; set; }
        public string ClientSex { get; set; }
        public DateTime Birthday { get; set; }
        public string Adres { get; set; }
        public string Phone { get; set; }
        public string PassNum { get; set; }
        public virtual ICollection<Rent> Rents { get; set; }
        public Client()
        {
            Rents = new List<Rent>();
        }

        public override string ToString()
        {
            return "[ ID = " + Id + ". ClientFIO = " + ClienFIO + ". ClientSex = " + ClientSex + ". Birthday = " + Birthday.ToShortDateString()
                + ". Adress = " + Adres + ". Phone = " + Phone + ". PassNum = " + PassNum + ". ]";
        }
    }
}
