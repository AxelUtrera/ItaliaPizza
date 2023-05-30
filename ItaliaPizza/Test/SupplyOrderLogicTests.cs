using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class SupplyOrderLogicTests
    {

        [TestMethod]
        public void Test01_RegisterSupplierProductsOrder_Valid()
        {
            SupplierOrder supplierOrder = new SupplierOrder
            {
                OrderNumber = "Test",
                OrderDate = DateTime.Now,
                IdSupplier = 1
            };

            Dictionary<string, int> products = new Dictionary<string, int>
            {
                {"15YHD", 1}
            };

            int resultExpected = 200;
            int resultObtained = SupplyOrderLogic.RegisterSupplierProductsOrder(supplierOrder, products);

            Assert.AreEqual(resultExpected, resultObtained);
        }

        [TestMethod]
        public void Test02_RegisterSupplierIngredientOrder_Valid()
        {
            SupplierOrder supplierOrder = new SupplierOrder
            {
                OrderNumber = "TestI",
                OrderDate = DateTime.Now,
                IdSupplier = 2
            };

            Dictionary<int, int> ingredients = new Dictionary<int, int>
            {
                {2, 1}
            };

            int resultExpected = 200;
            int resultObtained = SupplyOrderLogic.RegisterSupplierIngredientOrder(supplierOrder, ingredients);

            Assert.AreEqual(resultExpected, resultObtained);
        }


        [TestMethod]
        public void Test03_RecoverActiveOrders_Valid()
        {
            List<SupplierOrder> orders = SupplyOrderLogic.RecoverActiveOrders();

            Assert.IsTrue(orders.Count() > 0);
        }


        [TestMethod]
        public void Test04_DeleteSupplierOrder_Valid()
        {
            string orderToDelete = "TestI";

            int expectedResult = 200;
            int obtainedResult = SupplyOrderLogic.DeleteSupplierOrder(orderToDelete);

            Assert.AreEqual(expectedResult, obtainedResult);
        }


        [TestMethod]
        public void Test04_DeleteSupplierOrder_Invalid()
        {
            string orderToDelete = "TestI";

            int expectedResult = 500;
            int obtainedResult = SupplyOrderLogic.DeleteSupplierOrder(orderToDelete);

            Assert.AreEqual(expectedResult, obtainedResult);
        }
    }
}
