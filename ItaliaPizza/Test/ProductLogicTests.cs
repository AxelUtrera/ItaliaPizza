using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;

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
				ProductCode = "12KKP",
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

		[TestMethod]
		public void Test03_DeleteProduct_WithValidProductCode()
		{
			Product testProduct = new Product
			{
				Name = "Test Product",
				Description = "Test Description",
				ProductCode = "312IO",
				Picture = new byte[] { 0x12, 0x34, 0x56 },
				Restrictions = "I cannot do a merge without break things :(",
				Price = 9.99,
				Active = true,
			};
			
			int addStatusCode = ProductLogic.AddNewProduct(testProduct);
			Assert.AreEqual(200, addStatusCode, "Failed to add test product to the database.");

			int deleteStatusCode = ProductLogic.DeleteProduct(testProduct.ProductCode);

			Assert.AreEqual(200, deleteStatusCode, "Failed to delete the test product from the database.");
		}

		[TestMethod]
		public void DeleteProduct_WithNonexistentProductCode_ReturnsNotFound()
		{
			string nonexistentProductCode = ":P";

			int statusCode = ProductLogic.DeleteProduct(nonexistentProductCode);

			Assert.AreEqual(404, statusCode, "Something goes wrong...");
		}

	}


}
