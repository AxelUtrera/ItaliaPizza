using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.Entity;
using System.Windows;
using System.Data.Entity.Infrastructure;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Core;

namespace Logic
{
    public class IngredientLogic
    {
        public static List<Ingredient> GetIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            try
            {
                using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
                {
                    var queryIngredients = context.ingredient.ToList();

                    if (queryIngredients != null)
                    {
                        foreach (var aux in queryIngredients)
                        {
                            Ingredient ingredient = new Ingredient
                            {
                                IdIngredient = aux.idIngredient,
                                IngredientName = aux.ingredientName,
                                Quantity = aux.quantity
                            };
                            ingredients.Add(ingredient);
                        }
                    }
                }
            }
            catch (EntityException ex)
            {
                throw new EntityException(ex.Message, ex);
            }
            return ingredients;
        }

        public static bool RegistIngredient(Ingredient ingredient)
        {
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {
                bool registered = false;
                var ingredients = new DataAccess.ingredient
                {
                    ingredientName = ingredient.IngredientName,
                    quantity = ingredient.Quantity,
                    active = ingredient.IsActive
                };
                try
                {
                    context.ingredient.Add(ingredients);                   
                    context.SaveChanges();
                    registered= true;
                }
                catch (EntityException ex)
                {                    
                    throw new EntityException (ex.ToString());
                }
                return registered;
            }
        }
        public static bool RegistRecipeIngredients(List<Ingredient> listIngredients, int idRecipe) 
        {
            bool response=false;
            try
            {
                using (var context = new ItaliaPizzaEntities())
                {
                    foreach (var ingredient in listIngredients)
                    {
                        response = context.Database.ExecuteSqlCommand(
                        "Insert into recipeIngredient(idIngredient,idRecipe, quantity) Values(@idIngredient, @idRecipe, @quantity)",
                        new SqlParameter("idIngredient", ingredient.IdIngredient),
                        new SqlParameter("idRecipe", idRecipe),
                        new SqlParameter("quantity", ingredient.Quantity)
                        ) != 0;
                    }

                }
            }catch (SqlException ex)
            {                
                throw ex;
            }
            
            return response;
        }
        public static bool DeleteRecipeIngredients(int idRecipe)
        {
            bool response = false;
            try
            {
                using (var context = new ItaliaPizzaEntities())
                {
                    var foundRecipeIngredients = context.recipeIngredient.Where(x => x.idRecipe.Equals(idRecipe));
                    context.recipeIngredient.RemoveRange(foundRecipeIngredients);
                    context.SaveChanges();
                }
                return response;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return response;
            }
        }
        public static List<Ingredient> GetRecipeIngredients(int idRecipe)
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            List<Ingredient> idIngredients = new List<Ingredient>();
            try
            {
                SqlConnection connection = new SqlConnection("server=.\\SQLEXPRESS; database= ItaliaPizza; integrated security= true");
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from recipeIngredient where idRecipe =" + idRecipe.ToString(), connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient.IdIngredient = Convert.ToInt32(reader["idIngredient"]);
                    ingredient.Quantity = Convert.ToInt32(reader["quantity"]);
                    idIngredients.Add(ingredient);

                }
                foreach (var aux in idIngredients)
                {
                    using (var context = new ItaliaPizzaEntities())
                    {
                        var foundIngredient = (from ingredient in context.ingredient
                                               where ingredient.idIngredient.Equals(aux.IdIngredient)
                                               select ingredient).FirstOrDefault();

                        if (foundIngredient != null)
                        {
                            Ingredient ingredient = new Ingredient()
                            {
                                IngredientName = foundIngredient.ingredientName,
                                IdIngredient = foundIngredient.idIngredient,
                                Quantity = aux.Quantity,
                            };
                            ingredients.Add(ingredient);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return ingredients;
        }
    }
}