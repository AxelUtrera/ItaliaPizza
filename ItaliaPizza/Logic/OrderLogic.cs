using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Navigation;

namespace Logic
{
    public class OrderLogic
    {


        public static List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();

            using (ItaliaPizzaEntities database = new ItaliaPizzaEntities())
            {
                var ordersDataBase = database.orders.Where(o => !o.status.Equals("Entregado")).ToList();

                if (ordersDataBase != null)
                {
                    orders = ConvertToOrderModel(ordersDataBase);
                }
            }
            return orders;
        }


        public static List<Order> GetPayPendingOrders()
        {
            List<Order> orders = new List<Order>();

            using (ItaliaPizzaEntities database = new ItaliaPizzaEntities())
            {
                var ordersDataBase = database.orders.Where(o => !o.status.Equals("Entregado") && !o.status.Equals("Pagado")).ToList();

                if (ordersDataBase != null)
                {
                    orders = ConvertToOrderModel(ordersDataBase);
                }
            }

            return orders;
        }


        public static int ChangeOrderStatus(int idOrder)
        {
            int resultCode = 500;
            int result = 0;

            using (var database = new ItaliaPizzaEntities())
            {
                var orderToChange = database.orders.Where(x => x.idOrder == idOrder).First();

                if(orderToChange != null)
                {
                    orderToChange.status = "Pagado";
                    result = database.SaveChanges();
                }

                if(result != 0)
                {
                    resultCode = 200;
                }
            }

            return resultCode;
        }


        public static List<Order> ConvertToOrderModel(List<orders> ordersToConvert)
        {
            List<Order> ordersConverted = new List<Order>();

            foreach (orders toConvert in ordersToConvert)
            {
                ordersConverted.Add(new Order()
                {
                    idOrder = toConvert.idOrder,
                    status = toConvert.status,
                    date = toConvert.date,
                    hour = toConvert.hour,
                    idWorker = toConvert.idWorker,
                    typeOrder = toConvert.typeOrder,
                    nameCustomer = toConvert.nameCustomer,
                    total = (double)toConvert.total
                });
            }

            return ordersConverted;
        }


        public static List<orderProduct> GetIdProductsByIdOrder(int idOrder)
        {
            List<orderProduct> listOrderProducts = new List<orderProduct>();

            using (ItaliaPizzaEntities database = new ItaliaPizzaEntities())
            {
                var productRecovered = database.orderProduct.Where(p => p.idOrder == idOrder).ToList();
                if(productRecovered != null)
                {
                    listOrderProducts = productRecovered;
                }
            }
            return listOrderProducts;
        }


        public static List<Product> GetInfoProductById(int idOrder)
        {
            List<orderProduct> orderProducts = GetIdProductsByIdOrder(idOrder);

            List<Product> productInfo = new List<Product>();

            using(var database = new ItaliaPizzaEntities())
            {
                foreach(var item in orderProducts)
                {
                    var info = database.product.Where(x => x.productCode.Equals(item.idProduct)).First();

                    Product product = new Product
                    {
                        Name = info.productName,
                        ProductCode = info.productCode,
                        Price = info.price,
                        IdRecipe = info.idRecipe,
                        Preparation = info.preparation,
                        Quantity = item.quantity
                    };
                    productInfo.Add(product);
                }
            }

            return productInfo;
        }

        
        //Agrega una orden local.
        public static int AddOrder(Order orderToCreate, List<ProductToView> productsInOrder)
        {
            int result = 500;
            using (ItaliaPizzaEntities dataBase = new ItaliaPizzaEntities())
            {
                var resultOrder = dataBase.orders.Add(new orders()
                {
                    date = orderToCreate.date,
                    hour = orderToCreate.hour,
                    idWorker = orderToCreate.idWorker,
                    nameCustomer = orderToCreate.nameCustomer,
                    status = orderToCreate.status,
                    typeOrder = orderToCreate.typeOrder,
                    total = orderToCreate.total
                });
                dataBase.SaveChanges();
                if(resultOrder != null)
                {
                    if (AddProductAtOrder(productsInOrder, resultOrder.idOrder) == 200)
                    {
                        result = 200;
                    }
                }
            }
            return result;
        }


        //Agrega una orden a domicilio.
        public static int AddOrder(Order orderToCreate, List<ProductToView> productsInOrder, Address addressToDelivery)
        {
            int result = 500;
            using (ItaliaPizzaEntities dataBase = new ItaliaPizzaEntities())
            {
                var resultOrder = dataBase.orders.Add(new orders()
                {
                    date = orderToCreate.date,
                    hour = orderToCreate.hour,
                    idWorker = orderToCreate.idWorker,
                    nameCustomer = orderToCreate.nameCustomer,
                    status = orderToCreate.status,
                    typeOrder = orderToCreate.typeOrder,
                    total = orderToCreate.total
                });
                dataBase.SaveChanges();
                if (resultOrder != null)
                {
                    if (AddProductAtOrder(productsInOrder, resultOrder.idOrder, addressToDelivery) == 200)
                    {
                        result = 200;
                    }
                }
            }
            return result;
        }


        //Agrega los productos sin una direccion asociada.
        private static int AddProductAtOrder(List<ProductToView> productsInOrder, int codeOrder)
        {
            int resultOperation = 500;
            using (ItaliaPizzaEntities dataBase = new ItaliaPizzaEntities())
            {
                foreach (ProductToView product in productsInOrder)
                {
                    var resultProducts = dataBase.orderProduct.Add(new orderProduct()
                    {
                        idOrder = codeOrder,
                        idProduct = product.ProductCode,
                        quantity = product.Quantity
                    });
                    dataBase.SaveChanges();
                    if (resultProducts != null)
                    {
                        resultOperation = 200;
                    }
                }
            }
            return resultOperation;
        }


        //Agrega los productos con una direccion asociada.
        private static int AddProductAtOrder(List<ProductToView> productsInOrder, int codeOrder, Address addressToDelivery)
        {
            int resultOperation = 500;
            using (ItaliaPizzaEntities dataBase = new ItaliaPizzaEntities())
            {
                foreach (ProductToView product in productsInOrder)
                {
                    var resultProducts = dataBase.orderProduct.Add(new orderProduct()
                    {
                        idOrder = codeOrder,
                        idProduct = product.ProductCode,
                        quantity = product.Quantity
                    });
                    if (resultProducts != null)
                    {
                        resultOperation = 200;
                    }
                }
                dataBase.SaveChanges();

                if (!(AddDeliveryAddress(addressToDelivery, codeOrder) == 200))
                {
                    resultOperation = 500;
                }
            }
            return resultOperation;
        }

        //Agrega la direccion de un pedido.
        private static int AddDeliveryAddress(Address addressToDelivery, int codeOrder)
        {
            int result = 500;
            using (ItaliaPizzaEntities dataBase = new ItaliaPizzaEntities())
            {
                var resultDeliveryOrder = dataBase.deliveryOrder.Add(new deliveryOrder()
                {
                    idOrder = codeOrder,
                    idCustomer = addressToDelivery.idCustomer,
                    idAddress = addressToDelivery.idAddress
                });
                

                if (dataBase.SaveChanges() > 0)
                {
                    result = 200;   
                }
            }
            return result;
        }


        public static int UpdateProductsInOrder(List<ProductToView> listProductToUpdate, Order orderToUpdate)
        {
            int resultOperation = 500;

            using (ItaliaPizzaEntities database = new ItaliaPizzaEntities())
            {
                foreach (ProductToView productToUpdate in listProductToUpdate)
                {
                    orderProduct orderDataBase = database.orderProduct.FirstOrDefault(p => p.idOrder.Equals(orderToUpdate.idOrder) && 
                                                                                           p.idProduct.Equals(productToUpdate.ProductCode));
                    if(orderDataBase != null)
                    {
                        orderDataBase.quantity = productToUpdate.Quantity;
                        database.Entry(orderDataBase).State = EntityState.Modified;
                    }
                    else
                    {
                        database.orderProduct.Add(new orderProduct()
                        {
                            idOrder = orderToUpdate.idOrder,
                            idProduct = productToUpdate.ProductCode,
                            quantity = productToUpdate.Quantity,
                        });
                    }
                }
                var result = database.SaveChanges();

                if (result > 0)
                {
                    resultOperation = 200;
                }
            }

            RemoveProductsNoRequired(listProductToUpdate, orderToUpdate);
            return resultOperation;   
        }


        private static int RemoveProductsNoRequired(List<ProductToView> productUpdated, Order orderUpdated)
        {
            int resultOperation = 500;

            using(ItaliaPizzaEntities database = new ItaliaPizzaEntities())
            {
                var productCodes = productUpdated.Select(p => p.ProductCode).ToList();

                var productsToDelete = database.orderProduct
                    .Where(p => !productCodes.Contains(p.idProduct) && orderUpdated.idOrder == p.idOrder)
                    .ToList();

                database.orderProduct.RemoveRange(productsToDelete);

                var resultDelete = database.SaveChanges();
                if (resultDelete > 0)
                {
                    resultOperation = 200;
                }
            }
            return resultOperation;
        }


        public static int AddPaymentToCashBox(double total, string username)
        {
            int resultOperation = 500;

            using(var database = new ItaliaPizzaEntities())
            {
                database.transactions.Add(new transactions
                {
                    reason = "Order Payment",
                    idCashbox = 1,
                    worker = username,
                    amount = (int)total,
                });

                int resultObtained = database.SaveChanges();

                if(resultObtained != 0)
                {
                    resultOperation = AddMoneyToCashBox(total);
                }
            }

            return resultOperation;
        }


        private static int AddMoneyToCashBox(double total)
        {
            int resultOperation = 500;

            using(var database = new ItaliaPizzaEntities())
            {
                var cashbox = database.cashbox.FirstOrDefault();

                if(cashbox != null)
                {
                    cashbox.incomes += total;
                    cashbox.totalAmount += total;
                }

                var resultObtained = database.SaveChanges();

                if(resultObtained != 0)
                {
                    resultOperation = 200;
                }
            }

            return resultOperation;
        }


        public static int DownInventory(int idOrder)
        {
            int resultOperation = 500;

            List<Product> orderProducts = GetInfoProductById(idOrder);

            using(var database = new ItaliaPizzaEntities())
            {
                foreach(var product in orderProducts)
                {
                    if(product.Preparation == false)
                    {
                        DownProductInventory(product.ProductCode, product.Quantity);
                    }
                    if(product.Preparation == true)
                    {
                        DownProductInventory(product.ProductCode, product.Quantity);
                        DownIngredientInventory(product, product.Quantity);
                    }
                }
            }


            return resultOperation;
        }


        private static void DownProductInventory(string productCode, double quantity)
        {
            using(var database = new ItaliaPizzaEntities())
            {
                var productObtained = database.product.FirstOrDefault(x => x.productCode.Equals(productCode));
                if(productObtained != null)
                {
                    productObtained.quantity -= quantity;
                }
                
                database.SaveChanges();
            }
        }


        private static void DownIngredientInventory(Product product, double quantity)
        {
            using(var database = new ItaliaPizzaEntities())
            {
                var recipe = database.recipeIngredient.Where(x => x.idRecipe == product.IdRecipe).ToList();

                if(recipe.Count() > 0)
                {
                    foreach(var item in recipe)
                    {
                        var ingredient = database.ingredient.FirstOrDefault(x => x.idIngredient == item.idIngredient);
                        ingredient.quantity -= item.quantity * quantity;
                    }
                    database.SaveChanges();
                }
            }
        }
    }
}
