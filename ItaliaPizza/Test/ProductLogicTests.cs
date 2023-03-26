using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Model;
namespace Logic.Tests
{
    [TestClass()]
    public class ProductLogicTests
    {
        [TestMethod()]
        public void Test01_GetAllProductToView_SuccessFulTest()
        {
            List<ProductToView> listProductResult = ProductLogic.GetAllProductToView();
            Assert.IsNotNull(listProductResult);
        }
    }
}