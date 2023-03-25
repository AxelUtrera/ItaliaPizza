using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProductLogic
    {
        public static List<Product> GetAllProduct()
        {
            List<Product> productsObtained = new List<Product>();

            using (var database = new ItaliaPizzaEntities())
            {
                var allProducts = from product in database.product
                                  select product;

                foreach (var product in allProducts)
                {
                    {
                        Product recoverProduct = new Product()
                        {
                            Name = product.productName,
                            Description = product.description,
                            ProductCode = product.productCode,
                            Picture = product.picture,
                            Price = product.price,
                            Preparation = product.preparation,
                            ProductName = product.productName,
                            Restrictions = product.restrictions,
                            IdInventory = product.idInventory,
                            IdRecipe = product.idRecipe,
                            Active = product.active
                        };

                        productsObtained.Add(recoverProduct);
                    }
                }
                return productsObtained;
            }

        }

		public static int AddNewProduct(Product newProduct)
		{
			int responseCode = 500;

			using (var database = new ItaliaPizzaEntities())
			{
				try
				{
					if (string.IsNullOrEmpty(newProduct.ProductCode))
					{
						string productCode = GenerateProductCode();
						newProduct.ProductCode = productCode;
					}

					database.product.Add(new product
					{
						productName = newProduct.Name,
						description = newProduct.Description,
						productCode = newProduct.ProductCode,
						picture = newProduct.Picture,
						price = newProduct.Price,
						preparation = newProduct.Preparation,
						restrictions = newProduct.Restrictions,
						active = newProduct.Active
					});
					var productisSaved = database.SaveChanges();
					if (productisSaved!=0)
					{
                        responseCode = 200;
                    }
					
				}
				catch (DbEntityValidationException ex)
				{
					foreach (var validationError in ex.EntityValidationErrors)
					{
						Console.WriteLine("Validation error for entity {0}:", validationError.Entry.Entity.GetType().Name);

						foreach (var error in validationError.ValidationErrors)
						{
							Console.WriteLine("- Property: {0}, Error: {1}", error.PropertyName, error.ErrorMessage);
						}
					}
				}

			}

			return responseCode;

		}

		private static string GenerateProductCode()
		{
			return "PROD" + new Random().Next(1000, 9999);
		}
	}
}
