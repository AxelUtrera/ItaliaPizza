using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public int idOrder { get; set; }
        public string date { get; set; }
        public string hour { get; set; }
        public string status { get; set; }
        public string idWorker { get; set; }
        public string typeOrder { get; set; }
        public string nameCustomer { get; set; }

        List<ProductToView> listProducts { get; set; }
        public double subtotalOrder { get; set; }
        public double IVA { get; set; }
        public double total { set; get; }
    }
}
