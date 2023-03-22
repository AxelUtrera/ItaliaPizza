using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.IO;
using System.Reflection;

namespace Test
{
	[TestClass]
	public class ProductLogicTests
	{
		[TestMethod]
		public void GetAllProduct_ReturnsAllProducts()
		{
			Model.Product productExpected = new Model.Product()
			{
				ProductName = "Tomato",
				Description = "Red",
				ProductCode = "code",
				Price = 10.0,
				Preparation = true,
				Restrictions = "Be a man",
				Active = true
			};

			string imagePath = "View.Img.addPizza.jpg";
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(imagePath))
			{
				if (stream != null)
				{
					byte[] imageBytes = new byte[stream.Length];
					stream.Read(imageBytes, 0, (int)stream.Length);
					productExpected.Picture = Convert.FromBase64String(Convert.ToBase64String(imageBytes));
				}
				else
				{
					Assert.Fail("Failed to read image file into memory");
				}
			}

			var actualProducts = ProductLogic.GetAllProduct();

			CollectionAssert.Contains(actualProducts, productExpected);
		}
	}
}
