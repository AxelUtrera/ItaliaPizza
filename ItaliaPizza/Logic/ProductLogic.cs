﻿using DataAccess;
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
        public static List<ProductToView> GetAllProductToView()
        {
            List<ProductToView> productsObtained = new List<ProductToView>();

            using (var database = new ItaliaPizzaEntities())
            {
                var allProducts = from product in database.product
                                  select product;

                foreach (var product in allProducts)
                {
                    {
                        ProductToView recoverProduct = new ProductToView()
                        {
                            Name = product.productName,
                            Description = product.description,
                            ProductCode = product.productCode,
                            Price = "$"+product.price.ToString(),
                            Restrictions = product.restrictions,
							IdRecipe = product.idRecipe,
							//Active = product.active
                            Active = product.active == true ? "Si" : "No"
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
