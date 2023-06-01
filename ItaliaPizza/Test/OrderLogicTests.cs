using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccess;
using Model;
using Syncfusion.Windows.Shared;
using System;
using Moq;
using System.Data.Entity;

namespace Logic.Tests
{
    internal class OrderProductComparer : Comparer<orderProduct>
    {
        public override int Compare(orderProduct x, orderProduct y)
        {
            if (x.idOrderProduct == y.idOrderProduct && x.idOrder == y.idOrder && x.idProduct == y.idProduct && x.quantity == y.quantity)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
    }

    [TestClass()]
    public class OrderLogicTests
    {
        [TestMethod]
        public void GetOrders_OrdersExist_ReturnsNonDeliveredOrders()
        {
            var expected = 3;
            var result = OrderLogic.GetOrders().Count;
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void ConvertToOrderModel_ValidInput_ReturnsConvertedOrders()
        {
            var ordersToConvert = new List<orders>
            {
                new orders { idOrder = 1, status = "Pendiente", date = "2023-05-07", hour = "10:00", idWorker = "J1000", typeOrder = "Local" },
                new orders { idOrder = 2, status = "En proceso", date = "2023-05-08", hour = "15:30", idWorker = "J1000", typeOrder = "Domicilio" }
            };

            var result = OrderLogic.ConvertToOrderModel(ordersToConvert);

            Assert.AreEqual(2, result.Count);

            Assert.AreEqual(1, result[0].idOrder);
            Assert.AreEqual("Pendiente", result[0].status);
            Assert.AreEqual("2023-05-07", result[0].date);
            Assert.AreEqual("10:00", result[0].hour);
            Assert.AreEqual("J1000", result[0].idWorker);
            Assert.AreEqual("Local", result[0].typeOrder);

            Assert.AreEqual(2, result[1].idOrder);
            Assert.AreEqual("En proceso", result[1].status);
            Assert.AreEqual("2023-05-08", result[1].date);
            Assert.AreEqual("15:30", result[1].hour);
            Assert.AreEqual("J1000", result[1].idWorker);
            Assert.AreEqual("Domicilio", result[1].typeOrder);
        }


        [TestMethod]
        public void AddOrder_ValidOrderAndProducts_Returns200()
        {
            Order orderToCreate = new Order
            {
                date = "01/05/2023",
                hour = "12:30:00",
                idWorker = "J1000",
                nameCustomer = "John Doe",
                status = "Pendiente",
                typeOrder = "Local",
                total = 50.00
            };

            List<ProductToView> productsInOrder = new List<ProductToView>
            {
                 new ProductToView { ProductCode = "9COSG", Quantity = 2 },
                 new ProductToView { ProductCode = "B4XFM", Quantity = 3 }
            };

            int result = OrderLogic.AddOrder(orderToCreate, productsInOrder);

            Assert.AreEqual(200, result);
        }

    }
}