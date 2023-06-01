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
    public class InventoryReportLogic
    {
        public static List<InventoryReport> getInventoryReport()
        {
            List<InventoryReport> report = new List<InventoryReport>();

            try
            {
                using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
                {
                    var ingredientsFound = context.ingredient.ToList();
                    foreach (var aux in ingredientsFound)
                    {
                        InventoryReport ingredients = new InventoryReport
                        {
                            Name = aux.ingredientName,
                            Quantity = (int)aux.quantity,
                            TypeOfProduct = "Ingrediente",
                            WarningTreshold = aux.warningTreshold,
                            UnitOfMeasurement = aux.unitOfMeasurement.unitOfMeasurementName,
                            RealQuantity = 0,
                            IdItem = aux.idIngredient.ToString()
                        };

                        report.Add(ingredients);
                    }
                    var productsFound = context.product.Where(i => i.preparation == false).ToList();
                    foreach (var aux in productsFound)
                    {
                        InventoryReport products = new InventoryReport
                        {
                            Name = aux.productName,
                            Quantity = (int)aux.quantity,
                            TypeOfProduct = "Producto final",
                            WarningTreshold = aux.warningTreshold,
                            IdItem = aux.productCode

                        };
                        report.Add(products);
                    }

                }
            }
            catch (EntityException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return report;
        }
    }
}
