using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                if(ordersDataBase != null)
                {
                    orders = ConvertToOrderModel(ordersDataBase);
                }
            }

            return orders;
        }


       public static List<Order> ConvertToOrderModel(List<orders> ordersToConvert)
       {
            List<Order> ordersConverted = new List<Order>();

            foreach(orders toConvert in ordersToConvert)
            {
                ordersConverted.Add(new Order()
                {
                    idOrder = toConvert.idOrder,
                    status = toConvert.status,
                    date = toConvert.date,
                    hour = toConvert.hour,
                    idWorker = toConvert.idWorker,
                    typeOrder = toConvert.typeOrder,
                    //cambiar el valor de nameclient en el momento en que se utilice.
                    nameClient = ""
                });
            }

            return ordersConverted;
       }

    }
}
