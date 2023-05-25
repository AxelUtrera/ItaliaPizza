using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestClass]
    public class SupplierLogicTests
    {

        [TestMethod]
        public void Test01_RegisterSupplier_ValidSupplier()
        {
            Supplier supplierTest = new Supplier
            {
                SupplierName = "Test",
                SupplierAddress = "Test Address",
                Email = "email@test.com",
                PhoneNumber = "1234567890",
                Rfc = "AAAAAAAAAAAAA",
                SupplierType = "Test",
                Active = false
            };

            int expectedResult = 200;
            int obtainedResult = SupplierLogic.RegisterSupplier(supplierTest);

            Assert.AreEqual(expectedResult, obtainedResult);
        }


        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void Test02_RegisterSupplier_InvalidSupplier()
        {
            Supplier supplierTest = new Supplier
            {
                SupplierName = null,
                SupplierAddress = "Test Address",
                Email = "email@test.com",
                PhoneNumber = "1234567890",
                Rfc = "AAAAAAAAAAAAA",
                SupplierType = "Test",
                Active = false
            };

            int obtainedResult = SupplierLogic.RegisterSupplier(supplierTest);

        }
    }
}
