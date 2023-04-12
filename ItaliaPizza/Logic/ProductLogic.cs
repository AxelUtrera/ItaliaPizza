using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace Logic
{
	public class ProductLogic
	{
		public static List<ProductToView> GetAllProductToView()
		{
			List<ProductToView> productsObtained = new List<ProductToView>();

			try
			{
				using (var database = new ItaliaPizzaEntities())
				{
					var allProducts = from product in database.product
									  select product;

					foreach (var product in allProducts)
					{
						ProductToView recoverProduct = new ProductToView()
						{
							Name = product.productName,
							Description = product.description,
							ProductCode = product.productCode,
							Price = "$" + product.price.ToString(),
							Restrictions = product.restrictions,
							Active = product.active == true ? "Si" : "No",
							Image = ImageLogic.ConvertToBitMapImage(product.picture),
							Preparation = product.preparation,
							IdRecipe = product.idRecipe 
						};

						productsObtained.Add(recoverProduct);
					}
				}
			}
			catch (ArgumentNullException ex)
			{

			}
			catch (DbEntityValidationException ex)
			{

			}
			return productsObtained;
		
        }


		

		public int ModifyExistentProduct(ProductToView productToModify)
		{
			int operationResult = 500;
			Product productToModifyConverted = ConvertToProduct(productToModify);

			if (productToModify != null)
			{
				try
				{
                    using (var dataBase = new ItaliaPizzaEntities())
                    {
                        var updatedProduct = dataBase.product.First(p => p.productCode == productToModify.ProductCode);
                        updatedProduct.productCode = productToModifyConverted.ProductCode;
                        updatedProduct.productName = productToModifyConverted.Name;
                        updatedProduct.price = productToModifyConverted.Price;
                        updatedProduct.description = productToModifyConverted.Description;
                        updatedProduct.restrictions = productToModifyConverted.Restrictions;
                        updatedProduct.picture = productToModifyConverted.Picture;
                        updatedProduct.active = productToModifyConverted.Active;
                        updatedProduct.idRecipe = productToModifyConverted.IdRecipe;
                        updatedProduct.preparation = productToModifyConverted.Preparation;
	
                        int returnValue = dataBase.SaveChanges();

                        if (returnValue > 0)
                        {
                            operationResult = 200;
                        }
                    }
                }
                catch(ArgumentException ex)
				{

				}
				catch (DbEntityValidationException ex)
				{

				}
			}
			return operationResult;
		}


		public Product ConvertToProduct(ProductToView productViewToConvert)
		{

			Product productResultant = null;

            if (productViewToConvert != null)
			{
				productResultant = new Product()
				{
					Name = productViewToConvert.Name,
					Description = productViewToConvert.Description,
					ProductCode = productViewToConvert.ProductCode,
					Price = Double.Parse(productViewToConvert.Price.Replace("$","")),
					Preparation = productViewToConvert.Preparation,

					IdRecipe = productViewToConvert.IdRecipe,
					Active = productViewToConvert.Active.Equals("Si") ? true : false,
					Picture = ImageLogic.ConvertToBytes(productViewToConvert.Image),
					Restrictions = productViewToConvert.Restrictions
				};
			}

			return productResultant;
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
                    
					if(database.SaveChanges() > 0) {
                        responseCode = 200;
                    }

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

					database.SaveChanges();

					responseCode = 200;

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

	}
}
