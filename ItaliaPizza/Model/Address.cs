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
        public string nameCustomer { get; set; }

        public string completeDirection { get
            {
               return street + ", #" + number + ", " + city + ", " + neighborhood;
            } 
        }

        public override bool Equals(object obj)
        {
            return obj is Address address &&
                   idAddress == address.idAddress &&
                   street == address.street &&
                   number == address.number &&
                   city == address.city &&
                   zipcode == address.zipcode &&
                   neighborhood == address.neighborhood &&
                   instructions == address.instructions &&
                   idCustomer == address.idCustomer;
        }

        public override int GetHashCode()
        {
            int hashCode = -1055604850;
            hashCode = hashCode * -1521134295 + idAddress.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(street);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(number);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(city);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(zipcode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(neighborhood);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(instructions);
            hashCode = hashCode * -1521134295 + idCustomer.GetHashCode();
            return hashCode;
        }
    }
}
