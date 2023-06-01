using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class SupplierOrder
    {
        public int IdSupplier { get; set; }
        public string OrderNumber { get; set; }
        public string SupplierName { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderType { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SupplierOrder order &&
                   IdSupplier == order.IdSupplier &&
                   OrderNumber == order.OrderNumber &&
                   SupplierName == order.SupplierName &&
                   OrderDate == order.OrderDate &&
                   OrderType == order.OrderType;
        }

        public override int GetHashCode()
        {
            int hashCode = 99548818;
            hashCode = hashCode * -1521134295 + IdSupplier.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SupplierName);
            hashCode = hashCode * -1521134295 + OrderDate.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(OrderType);
            return hashCode;
        }
    }
}
