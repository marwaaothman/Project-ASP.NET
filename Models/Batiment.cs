using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI1.Models
{
    public class Batiment
    {
       
        public int BatimentId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public int ClientId { get; set; }
        public Client  Client { get; set; }
    }
}
