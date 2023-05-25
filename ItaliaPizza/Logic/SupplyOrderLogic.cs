using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Logic
{
    public class SupplyOrderLogic
    {
        public static int ObtainOrderNumber()
        {
            int orderNumber = 0;

            using(var database = new ItaliaPizzaEntities())
            {
                var lastOrderNumber = database.supplierOrder.OrderByDescending(x => x.orderNumber).FirstOrDefault();

                if(lastOrderNumber != null)
                {
                    orderNumber = Int32.Parse(lastOrderNumber.orderNumber) + 1;
                } 
                else if(lastOrderNumber == null)
                {
                    orderNumber = 1001;
                }
            }
            return orderNumber;
        }


        public static List<Product> RecoverProducts()
        {
            List<Product> obtainedProducts = new List<Product>();

            using(var database = new ItaliaPizzaEntities())
            {
                var products = database.product.Where(x => x.preparation == false && x.active == true).ToList();
                foreach(var product in products)
                {
                    obtainedProducts.Add(new Product()
                    {
                        ProductCode = product.productCode,
                        Name = product.productName,
                        Quantity = product.quantity
                    });
                }
            }

            return obtainedProducts;
        }


        public static List<Ingredient> RecoverIngredients()
        {
            List<Ingredient> obtainedIngredients = new List<Ingredient>();

            using (var database = new ItaliaPizzaEntities())
            {
                var ingredients = database.ingredient.Where(x => x.active == true).ToList();
                foreach (var ingredient in ingredients)
                {
                    obtainedIngredients.Add(new Ingredient()
                    {
                        IdIngredient = ingredient.idIngredient,
                        IngredientName = ingredient.ingredientName,
                        Quantity = ingredient.quantity
                    });
                }
            }

            return obtainedIngredients;
        }


        public static int RegisterSupplierProductsOrder(SupplierOrder order, Dictionary<string, int> orderItems)
        {
            int statusCode = 500;

            using( var database = new ItaliaPizzaEntities())
            {
                int result = 0;

                database.supplierOrder.Add(new supplierOrder
                {
                    orderNumber = order.OrderNumber,
                    orderDate = order.OrderDate,
                    status = "ACTIVA",
                    idSupplier = order.IdSupplier,
                });

                result = database.SaveChanges();

                foreach(KeyValuePair<string, int> product in orderItems)
                {
                    database.supplierProduct.Add(new supplierProduct
                    {
                        idSupplierOrder = order.OrderNumber.ToString(),
                        productCode = product.Key,
                        quantity = product.Value
                    });
                    result = database.SaveChanges();

                    var addQuantity = database.product.Where(x => x.productCode == product.Key).First();
                    if(addQuantity != null)
                    {
                        addQuantity.quantity += product.Value;
                    }
                    result = database.SaveChanges();
                }

                if(result != 0)
                {
                    statusCode = 200;
                }
            }

            return statusCode;
        }


        public static int RegisterSupplierIngredientOrder(SupplierOrder order, Dictionary<int, int> orderItems)
        {
            int statusCode = 500;

            using (var database = new ItaliaPizzaEntities())
            {
                int result = 0;

                database.supplierOrder.Add(new supplierOrder
                {
                    orderNumber = order.OrderNumber,
                    orderDate = order.OrderDate,
                    status = "ACTIVA",
                    idSupplier = order.IdSupplier,
                });

                result = database.SaveChanges();

                foreach (KeyValuePair<int, int> ingredient in orderItems)
                {
                    database.supplierIngredient.Add(new supplierIngredient
                    {
                        idSupplierOrder = order.OrderNumber,
                        idIngredient = ingredient.Key,
                        quantity = ingredient.Value
                    });
                    result = database.SaveChanges();

                    var addQuantity = database.ingredient.Where(x => x.idIngredient == ingredient.Key).First();
                    if (addQuantity != null)
                    {
                        addQuantity.quantity += ingredient.Value;
                    }
                    result = database.SaveChanges();
                }

                if (result != 0)
                {
                    statusCode = 200;
                }
            }

            return statusCode;
        }


        public static List<SupplierOrder> RecoverActiveOrders()
        {
            List<SupplierOrder> ordersObtained = new List<SupplierOrder>();

            using (var database = new ItaliaPizzaEntities())
            {
                var ordersRecover = database.supplierOrder.Where(x => x.status.Equals("ACTIVA")).ToList();

                foreach(var order in ordersRecover)
                {
                    int idSup = order.idSupplier;

                    var supplier = database.supplier.Where(y => y.idSupplier == idSup).First();

                    ordersObtained.Add(new SupplierOrder
                    {
                        OrderNumber = order.orderNumber,
                        IdSupplier = idSup,
                        SupplierName = supplier.supplierName,
                        OrderDate = order.orderDate,
                        OrderType = supplier.supplierType
                    });
                }

            }

            return ordersObtained;
        }


        public static int DeleteSupplierOrder(string orderNumber)
        {
            int statusCode = 500;

            using(var database = new ItaliaPizzaEntities())
            {
                var supplierOrder = database.supplierOrder.Where(x => x.orderNumber.Equals(orderNumber)).First();

                supplierOrder.status = "INACTIVO";

                int result = database.SaveChanges();

                if(result != 0)
                {
                    statusCode = 200;
                }
            }

            return statusCode;
        }
    }
}
