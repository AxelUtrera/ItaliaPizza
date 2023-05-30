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
        public void Test02_ModifySupplier_ValidSupplier()
        {
            Supplier supplierTest = new Supplier
            {
                IdSupplier = 1,
                SupplierName = "Test Modified",
                SupplierAddress = "Test Address",
                Email = "email@test.com",
                PhoneNumber = "1234567890",
                Rfc = "AAAAAAAAAAAAA",
                SupplierType = "Test",
                Active = false
            };

            int expectedResult = 200;
            int obtainedResult = SupplierLogic.ModifySupplier(supplierTest);

            Assert.AreEqual(expectedResult, obtainedResult);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test03_ModifySupplier_InvalidSupplier()
        {
            Supplier supplierTest = new Supplier
            {
                IdSupplier = 99,
                SupplierName = "Test Modified",
                SupplierAddress = "Test Address",
                Email = "email@test.com",
                PhoneNumber = "1234567890",
                Rfc = "AAAAAAAAAAAAA",
                SupplierType = "Test",
                Active = false
            };

            SupplierLogic.ModifySupplier(supplierTest);
        }


        [TestMethod]
        public void Test04_RecoverActiveSuppliers_Valid()
        {
            List<Supplier> suppliers = SupplierLogic.RecoverActiveSuppliers();

            Assert.IsTrue(suppliers.Count () > 0);
        }


        [TestMethod]
        public void Test05_DeleteSupplierById_Valid()
        {
            int suplierToDelete = 2;

            int expectedResult = 200;
            int resultObtained = SupplierLogic.DeleteSupplierById(suplierToDelete);

            Assert.AreEqual(expectedResult, resultObtained);
        }


        [TestMethod]
        public void Test06_DeleteSupplierById_Invalid()
        {
            int suplierToDelete = 2;

            int expectedResult = 200;
            int resultObtained = SupplierLogic.DeleteSupplierById(suplierToDelete);

            Assert.AreEqual(expectedResult, resultObtained);
        }
    }
}
