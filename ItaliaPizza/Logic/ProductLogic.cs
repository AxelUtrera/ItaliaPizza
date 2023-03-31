using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProductLogic
    {
        public static List<ProductToView> GetAllProductToView()
        {
            List<ProductToView> productsObtained = new List<ProductToView>();
			try
			{
                using (var context = new ItaliaPizzaEntities())
                {
                    var allProducts = from product in context.product
                                      select product;

                    foreach (var product in allProducts)
                    {
                        {
                            ProductToView recoverProduct = new ProductToView()
                            {
                                Name = product.productName,
                                Description = product.description,
                                ProductCode = product.productCode,
                                Price = "$" + product.price.ToString(),
                                Restrictions = product.restrictions,
                                Active = product.active == true ? "Si" : "No"
                            };

                            productsObtained.Add(recoverProduct);
                        }
                    }
                    
                }


			}
			catch (DbUpdateException ex)
			{

			}
            return productsObtained;
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
						idRecipe = newProduct.IdRecipe,
						active = newProduct.Active
					});
                    
					if(database.SaveChanges() > 0) {
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
