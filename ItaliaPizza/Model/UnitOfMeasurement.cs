using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UnitOfMeasurement
    {
        protected int idUnitOfMeasurement;
        protected string unitOfMeasurementName;

        public int IdUnitOfMeasurement { get { return idUnitOfMeasurement; } set { idUnitOfMeasurement = value; } }
        public string UnitOfMeasurementName { get { return unitOfMeasurementName; } set { unitOfMeasurementName = value; } }

        public UnitOfMeasurement() { }
        public override string ToString()
        {
            return unitOfMeasurementName;
        }
    }
}
