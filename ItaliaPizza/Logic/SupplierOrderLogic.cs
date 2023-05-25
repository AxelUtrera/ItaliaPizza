using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class SupplyOrderLogic
    {
        public static int ObtainOrderNumber()
        {
            int orderNumber = 0;

            using(var database = new ItaliaPizzaEntities())
            {
                var lastOrderNumber = database.supplierOrder.OrderByDescending(x => x.orderNumber).FirstOrDefault();

                if(lastOrderNumber != null)
                {
                    orderNumber = Int32.Parse(lastOrderNumber.orderNumber) + 1;
                } 
                else if(lastOrderNumber == null)
                {
                    orderNumber = 1001;
                }
            }
            return orderNumber;
        }


        public static List<Product> RecoverProducts()
        {
            List<Product> obtainedProducts = new List<Product>();

            using(var database = new ItaliaPizzaEntities())
            {
                var products = database.product.Where(x => x.preparation == false && x.active == true).ToList();
                foreach(var product in products)
                {
                    obtainedProducts.Add(new Product()
                    {
                        ProductCode = product.productCode,
                        Name = product.productName,
                        Price = product.price
                    });
                }
            }

            return obtainedProducts;
        }


        public static List<Ingredient> RecoverIngredients()
        {
            List<Ingredient> obtainedIngredients = new List<Ingredient>();

            using (var database = new ItaliaPizzaEntities())
            {
                var ingredients = database.ingredient.Where(x => x.active == true).ToList();
                foreach (var ingredient in ingredients)
                {
                    obtainedIngredients.Add(new Ingredient()
                    {
                        IdIngredient = ingredient.idIngredient,
                        IngredientName = ingredient.ingredientName
                    });
                }
            }

            return obtainedIngredients;
        }
    }
}
