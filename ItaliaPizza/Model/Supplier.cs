using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Supplier
    {
        public int IdSupplier { get; set; }
        public string SupplierAddress { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Rfc { get; set; }
        public string SupplierType { get; set; }
        public string SupplierName { get; set; }
        public bool Active { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Supplier supplier &&
                   IdSupplier == supplier.IdSupplier &&
                   SupplierAddress == supplier.SupplierAddress &&
                   Email == supplier.Email &&
                   PhoneNumber == supplier.PhoneNumber &&
                   Rfc == supplier.Rfc &&
                   SupplierType == supplier.SupplierType &&
                   SupplierName == supplier.SupplierName &&
                   Active == supplier.Active;
        }

        public override int GetHashCode()
        {
            int hashCode = 80054980;
            hashCode = hashCode * -1521134295 + IdSupplier.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SupplierAddress);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Rfc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SupplierType);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SupplierName);
            hashCode = hashCode * -1521134295 + Active.GetHashCode();
            return hashCode;
        }
    }
}
