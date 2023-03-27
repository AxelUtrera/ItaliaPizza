using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

					var recipe = database.recipe.Find(newProduct.IdRecipe);

					if (recipe == null && recipe != database.recipe.Find(1))
					{
						recipe = new recipe
						{
							idRecipe = newProduct.IdRecipe,
							description = "Producto preparado", 
							recipeName = newProduct.Name,
							active = newProduct.Active
						};

						database.recipe.Add(recipe);

						Console.WriteLine("Producto agregado correctamente.");

						Console.WriteLine("New recipe created.");
					}

					Console.WriteLine("Product added to database.");

					//database.SaveChanges();

					var productisSaved = database.SaveChanges();

					if (productisSaved != 0)
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

					Console.WriteLine("Error al agregar producto.");
					responseCode = 500;
				}
			}

			return responseCode;
		}

		public static int DeleteProduct(string productCode)
		{
			int responseCode;

			using (var database = new ItaliaPizzaEntities())
			{
				try
				{
					var productToDelete = database.product.SingleOrDefault(p => p.productCode == productCode);

					if (productToDelete != null)
					{
						database.product.Remove(productToDelete);
						database.SaveChanges();

						Console.WriteLine("Product deleted successfully.");
						responseCode = 200;
					}
					else
					{
						Console.WriteLine("Product not found.");
						responseCode = 404;
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error while deleting product: {0}", ex.Message);
					responseCode = 500;
				}
			}

			return responseCode;
		}

	}
}
