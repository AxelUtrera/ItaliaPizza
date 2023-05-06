using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SupplierOrder
    {
        public string OrderNumber { get; set; }
        public string SupplierName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ArriveDate { get; set; }
        public double Total { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SupplierOrder order &&
                   OrderNumber == order.OrderNumber &&
                   SupplierName == order.SupplierName &&
                   OrderDate == order.OrderDate &&
                   ArriveDate == order.ArriveDate &&
                   Total == order.Total;
        }

        public override int GetHashCode()
        {
            int hashCode = -1835294654;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SupplierName);
            hashCode = hashCode * -1521134295 + OrderDate.GetHashCode();
            hashCode = hashCode * -1521134295 + ArriveDate.GetHashCode();
            hashCode = hashCode * -1521134295 + Total.GetHashCode();
            return hashCode;
        }
    }
}
