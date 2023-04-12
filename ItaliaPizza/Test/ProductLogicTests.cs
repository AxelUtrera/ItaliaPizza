using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;


namespace Test
{
	[TestClass]
	public class ProductLogicTests
	{
		[TestMethod]
		public void Test01_AddNewProduct_ValidProduct()
		{
			var newProduct = new Product
			{
				Name = "Margherita Pizza",
				Description = "Pizza:3",
				ProductCode = "123AP",
				Picture = new byte[] { 0x12, 0x34, 0x56 },
				Price = 10.99,
				Preparation = true,
				Restrictions = "For classy people only haha",
				Active = true
			};

			var responseCode = ProductLogic.AddNewProduct(newProduct);

			Assert.AreEqual(200, responseCode);
		}

		[TestMethod]
		public void Test02_AddNewProduct_InvalidProduct()
		{
			var newProduct = new Product
			{
				Name = "Invalid Product",
				Description = "This product has invalid data and should not be added to the database.",
				ProductCode = "MoreThanFiveCharacters!",
				Picture = new byte[0],
				Price = -32,
				Preparation = false,
				Restrictions = "Invalid",
				Active = false
			};

			var responseCode = ProductLogic.AddNewProduct(newProduct);

			Assert.AreEqual(500, responseCode);
		}


        [TestMethod()]
        public void Test03_GetAllProductToView_SuccessFulTest()
        {
            List<ProductToView> listProductResult = ProductLogic.GetAllProductToView();
            Assert.IsNotNull(listProductResult);
        }

		[TestMethod()]
		public void Test04_EditProduct_SuccessfulTest()
		{
			List<ProductToView> listProductResult = ProductLogic.GetAllProductToView();
			ProductToView productToViewForTest = listProductResult[1];
			productToViewForTest.Name = "Coca 2.0";
            ProductLogic productLogic = new ProductLogic();
            Assert.AreEqual(productLogic.ModifyExistentProduct(productToViewForTest),200);
        }

        [TestMethod()]
        public void Test05_ConvertToProduct_SuccessfulTest()
        {
            List<ProductToView> listProductResult = ProductLogic.GetAllProductToView();
            ProductToView productToViewForTest = listProductResult[1];
			ProductLogic product = new ProductLogic();

            Assert.IsInstanceOfType(product.ConvertToProduct(productToViewForTest), typeof(Product));
        }

    }
}