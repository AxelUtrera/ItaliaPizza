using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SupplyOrderLogic
    {

        public List<SupplierOrder> RecoverActiveSupplierOrders()
        {
            List<SupplierOrder> supplierOrders = new List<SupplierOrder>();
            using (var database = new ItaliaPizzaEntities())
            {

            }

            return supplierOrders;
        }
    }
}
