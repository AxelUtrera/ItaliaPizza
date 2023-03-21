using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class ProductLogic
    {
        public static List<Product> getAllProduct()
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
    }
}
