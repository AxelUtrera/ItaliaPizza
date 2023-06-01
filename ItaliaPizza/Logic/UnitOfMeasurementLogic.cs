using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UnitOfMeasurementLogic
    {
        public static int GetIdUnitOfMeasurement(string unitOfMeasurementName)
        {
            int idUnitOfMeasurement = 0;
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {
                var foundUnitsOfMeasurement = (from unitOfMeasurement in context.unitOfMeasurement
                                               where unitOfMeasurement.unitOfMeasurementName.Equals(unitOfMeasurementName)
                                               select unitOfMeasurement).FirstOrDefault();
                if (foundUnitsOfMeasurement != null)
                {
                    idUnitOfMeasurement = foundUnitsOfMeasurement.idUnitOfMeasurement;
                }
            }
            return idUnitOfMeasurement;
        }
        public static List<UnitOfMeasurement> GetAllUnitsOfMeasurement()
        {
            List<UnitOfMeasurement> unitOfMeasurements = new List<UnitOfMeasurement>();
            try
            {
                using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
                {
                    var foundMeasurements = context.unitOfMeasurement.ToList();

                    if (foundMeasurements != null)
                    {
                        foreach (var aux in foundMeasurements)
                        {
                            UnitOfMeasurement measurement = new UnitOfMeasurement
                            {
                                IdUnitOfMeasurement = aux.idUnitOfMeasurement,
                                UnitOfMeasurementName = aux.unitOfMeasurementName
                            };
                            unitOfMeasurements.Add(measurement);
                        }
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException(ex.Message, ex);
            }
            return unitOfMeasurements;
        }
    }
}
