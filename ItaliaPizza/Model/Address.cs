using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Address
    {
        public int idAddress { get; set; }
        public string street { get; set; }
        public string number { get; set; }
        public string city { get; set; }
        public string zipcode { get; set; }
        public string neighborhood { get; set; }
        public string instructions { get; set; }
        public int idCustomer { get; set; }
    
    }
}
