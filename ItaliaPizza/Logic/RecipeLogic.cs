using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using System.Windows;
using Model;
using System.Data.Entity;
using System.Security.Principal;
using System.CodeDom;

namespace Logic
{
    public class RecipeLogic
    {
        public static bool RegistRecipe(Recipe recipe)
        {
            bool result = false;
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {
                var recipes = new DataAccess.recipe
                {
                    recipeName = recipe.NameRecipe,
                    description = recipe.DescriptionRecipe,
                    active = recipe.IsActive,
                };
                try
                {
                    context.recipe.Add(recipes);
                    context.SaveChanges();
                    result = true;
                }
                catch (DbUpdateException ex)
                {
                                      
                }
               
            }
            return result;
        }

        public static bool ActivateRecipe(int idRecipe)
        {
            bool result = false;
            using (ItaliaPizzaEntities context= new ItaliaPizzaEntities())
            {
                var foundRecipe = (from recipe in context.recipe 
                                  where recipe.idRecipe.Equals(idRecipe) 
                                  select recipe).FirstOrDefault();
                if(foundRecipe != null)
                {
                    foundRecipe.active=true;
                    try
                    {
                        context.SaveChanges();
                        result = true;
                    }catch (DbUpdateException ex)
                    {
                        throw new DbUpdateException(ex.Message);
                    }
                }
            }
            return result;
        }
        public static bool DeleteRecipe(int idRecipe)
        {
            bool result = false;
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {
                var foundRecipe = (from recipe in context.recipe
                                   where recipe.idRecipe.Equals(idRecipe)
                                   select recipe).FirstOrDefault();
                if (foundRecipe != null)
                {
                    foundRecipe.active = false;
                    try
                    {
                        context.SaveChanges();
                        result = true;
                    }
                    catch (DbUpdateException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro esta receta");
                }

            }
            return result;
        }

        public static bool EditRecipe(Recipe recipe1)
        {
            using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
            {


                var foudRecipe = (from recipe in context.recipe
                                  where recipe.idRecipe.Equals(recipe1.IdRecipe)
                                  select recipe).FirstOrDefault();

                if (foudRecipe != null)
                {
                    foudRecipe.description = recipe1.DescriptionRecipe;
                }
                try
                {
                    context.SaveChanges();
                    return true;
                }
                catch (DbUpdateException)
                {
                    return false;
                }
            }
        }
        public static string AllreadyExist(string nameRecipe)
        {
            string state="doesNotExist";
            try
            {
                using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
                {
                    var foudRecipe = (from recipe in context.recipe
                                      where recipe.recipeName.Equals(nameRecipe)
                                      select recipe).FirstOrDefault();
                    if (foudRecipe != null)
                    {
                        if (foudRecipe.active == false) { state = "idle";}
                        else { state = "exist";}                        
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.ToString());
            }
            return state;
        }
        public static List<Recipe> GetRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();
            try
            {
                using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
                {
                    var foundRecipes = (from recipe in context.recipe
                                        where recipe.active == true
                                        select recipe).ToList();
                    if (foundRecipes != null)
                    {
                        foreach (var aux in foundRecipes)
                        {
                            Recipe recipe = new Recipe
                            {
                                IdRecipe = aux.idRecipe,
                                NameRecipe = aux.recipeName,
                                DescriptionRecipe = aux.description,
                                IsActive = aux.active
                            };

                            recipes.Add(recipe);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());                
            }
            return recipes;
        }
        public static int GetIdRecipe(string nameRecipe)
        {
            int id = 0;
            try
            {
                using (ItaliaPizzaEntities context = new ItaliaPizzaEntities())
                {
                    var foundRecipe = (from recipe in context.recipe
                                       where
                                       recipe.recipeName.Equals(nameRecipe)
                                       select recipe).FirstOrDefault();
                    if (foundRecipe != null)
                    {
                        id = foundRecipe.idRecipe;
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException(ex.ToString());
            }
            return id;
        }
    }
}
