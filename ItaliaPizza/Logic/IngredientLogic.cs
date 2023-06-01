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
                    var queryIngredients = context.ingredient.Where(i => i.active == true).ToList();

                    if (queryIngredients != null)
                    {
                        foreach (var aux in queryIngredients)
                        {
                            Ingredient ingredient = new Ingredient
                            {
                                IdIngredient = aux.idIngredient,
                                Measurement = aux.unitOfMeasurement.unitOfMeasurementName,
                                IdMeasurement = (int)aux.idUnitOfMeasurement,
                                IngredientName = aux.ingredientName,
                                Quantity = aux.quantity,
                                WarningTreshold = aux.warningTreshold
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
        public static bool EditIngredient(Ingredient updatedIngredient)
        {
            bool result = false;
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {

                var ingredients = new DataAccess.ingredient
                {
                    ingredientName = updatedIngredient.IngredientName,
                    quantity = updatedIngredient.Quantity,
                    active = true,
                    idUnitOfMeasurement = updatedIngredient.IdMeasurement,
                    warningTreshold = updatedIngredient.WarningTreshold
                };
                context.ingredient.Add(ingredients);
                try
                {
                    context.SaveChanges();
                    result = true;
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return result;


        }
        public static bool DeleteIngredient(Ingredient updatedIngredient)
        {
            bool result = false;
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {
                var foundIngredient = (from ingredient in context.ingredient
                                       where ingredient.idIngredient.Equals(updatedIngredient.IdIngredient)
                                       select ingredient).FirstOrDefault();
                if (foundIngredient != null)
                {

                    context.ingredient.Remove(foundIngredient);
                    try
                    {
                        context.SaveChanges();
                        result = true;
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro el ingrediente seleccionado");
                }
            }
            return result;
        }

        public static bool DeactivateIngredient(Ingredient ingredient2)
        {
            bool result = false;
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {
                try
                {
                    var foundIngredient = (from ingredient in context.ingredient
                                           where ingredient.idIngredient.Equals(ingredient2.IdIngredient)
                                           select ingredient).FirstOrDefault();
                    if (foundIngredient != null)
                    {
                        foundIngredient.active = false;
                        context.SaveChanges();
                        result = true;
                    }
                }
                catch (EntityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
        public static bool ActivateIngredient(string ingredientName)
        {
            bool result = false;
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {
                try
                {
                    var foundIngredient = (from ingredient in context.ingredient
                                           where ingredient.ingredientName.Equals(ingredientName)
                                           select ingredient).FirstOrDefault();
                    if (foundIngredient != null)
                    {
                        foundIngredient.active = true;
                        context.SaveChanges();
                        result = true;
                    }
                }
                catch (EntityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
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
                    active = true,
                    idUnitOfMeasurement = ingredient.IdMeasurement,
                    warningTreshold = ingredient.WarningTreshold
                };
                try
                {
                    context.ingredient.Add(ingredients);
                    context.SaveChanges();
                    registered = true;
                }
                catch (EntityException ex)
                {
                    throw new EntityException(ex.ToString());
                }
                return registered;
            }
        }
        public static bool RegistRecipeIngredients(Recipe recipe)
        {
            List<recipeIngredient> ingredients = new List<recipeIngredient>();
            bool response = false;
            try
            {
                using (var context = new ItaliaPizzaEntities())
                {

                    foreach (var ingredient in recipe.Ingredients)
                    {

                        var recipeIngredient = new DataAccess.recipeIngredient
                        {

                            idRecipe = recipe.IdRecipe,
                            idIngredient = ingredient.IdIngredient,
                            quantity = (int)ingredient.Quantity

                        };
                        ingredients.Add(recipeIngredient);
                    }
                    context.recipeIngredient.AddRange(ingredients);
                    context.SaveChanges();
                    response = true;

                }
            }
            catch (Exception)
            {
                response = false;
            }

            return response;
        }
        public static bool DeleteRecipeIngredients(int idRecipe)
        {
            bool response = false;
            using (var context = new ItaliaPizzaEntities())
            {
                try
                {
                    var foundRecipeIngredients = context.recipeIngredient.Where(x => x.idRecipe.Equals(idRecipe));
                    if (foundRecipeIngredients.FirstOrDefault() != null)
                    {
                        context.recipeIngredient.RemoveRange(foundRecipeIngredients);
                        context.SaveChanges();
                        response = true;
                    }
                }
                catch (Exception)
                {
                    response = false;
                }
            }
            return response;
        }
        public static List<Ingredient> GetRecipeIngredients(int idRecipe)
        {

            List<Ingredient> ingredients = new List<Ingredient>();
            try
            {
                using (var context = new ItaliaPizzaEntities())
                {
                    var foundRecipeIngredients = context.recipeIngredient.Where(x => x.idRecipe.Equals(idRecipe)).ToList();
                    foreach (var aux in foundRecipeIngredients)
                    {


                        Ingredient ingredient = new Ingredient
                        {
                            IdIngredient = aux.idIngredient,
                            IngredientName = aux.ingredient.ingredientName,
                            Quantity = aux.quantity,
                            Measurement = aux.ingredient.unitOfMeasurement.unitOfMeasurementName,
                            IdMeasurement = (int)aux.ingredient.idUnitOfMeasurement
                        };
                        ingredients.Add(ingredient);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return ingredients;
        }
        public static bool IncremetQuantity(Ingredient newINgredient)
        {
            bool response = false;
            using (var context = new ItaliaPizzaEntities())
            {
                try
                {
                    var foundIngredients = (from ingredient in context.ingredient
                                            where ingredient.idIngredient.Equals(newINgredient.IdIngredient)
                                            select ingredient).FirstOrDefault();
                    if (foundIngredients != null)
                    {
                        foundIngredients.quantity += newINgredient.Quantity;
                        context.SaveChanges();
                        response = true;
                    }
                }
                catch (EntityException ex)
                {
                    response = false;
                }
            }
            return response;
        }

        public static int AllreadyExist(string ingredientName)
        {
            //"This function returns a status code: 3 if not found, 1 if active, and 2 if inactive."
            int status = -1;
            using (var context = new ItaliaPizzaEntities())
            {
                try
                {
                    var foundIngredients = (from ingredient in context.ingredient
                                            where ingredient.ingredientName.Equals(ingredientName)
                                            select ingredient).FirstOrDefault();

                    if (foundIngredients != null)
                    {
                        status = foundIngredients.active?  1 : 0;
                        
                    }
                }
                catch (EntityException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return status;
        }
        public static bool UpdateQuantity(Ingredient ingredient)
        {
            bool result= false;
            using (var context = new ItaliaPizzaEntities())
            {
                try
                {
                    var foundIngredients = context.ingredient.Where(x => x.idIngredient.Equals(ingredient.IdIngredient)).FirstOrDefault();
                    if (foundIngredients != null)
                    {
                        foundIngredients.quantity = ingredient.Quantity;
                        context.SaveChanges();
                        result = true;
                    }
                }
                catch (EntityException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return result;
        }
    }
}