using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class InventoryReport
    {
        protected string name;
        protected int warningTreshold;
        protected int quantity;
        protected int realQuantity;
        protected string unitOfMeasurement;
        protected string typeOfProduct;
        protected string idItem;

        public int WarningTreshold { get { return warningTreshold; } set { warningTreshold = value; } }
        public int Quantity { get { return quantity; } set { quantity = value; } }
        public int RealQuantity { get { return realQuantity; } set { realQuantity = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string UnitOfMeasurement { get { return unitOfMeasurement; } set { unitOfMeasurement = value; } }
        public string TypeOfProduct { get { return typeOfProduct; } set { typeOfProduct = value; } }
        public string IdItem { get { return idItem; } set { idItem = value; } }

        public InventoryReport()
        {

        }
        public override string ToString()
        {
            return name;
        }
    }
}
