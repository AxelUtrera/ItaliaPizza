using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Logic.Tests
{
    [TestClass]
    public class IngredientLogicTest
    {
        [TestMethod]
        public void Test01_GetIngredients_SuccessfulTes()
        {
            List <Ingredient> allIngredients= IngredientLogic.GetIngredients();
            Assert.IsNotNull(allIngredients);
        }
        [TestMethod]
        public void Test02_RegistIngredient_SuccessfulTes()
        {
            Ingredient ingredient = new Ingredient()
            {
                IsActive = true,
                IngredientName = "Test",
                Quantity = 1,
            };
            Assert.IsTrue( IngredientLogic.RegistIngredient(ingredient));
        }
      
        public void Test03_RegistIngredient_NotValidTest()
        {
            Ingredient ingredient = new Ingredient()
            {
                IsActive = true,
                IngredientName = "Test",
                Quantity = 1,
            };
            Assert.IsTrue(IngredientLogic.RegistIngredient(ingredient));
        }

        [TestMethod]
        public void Test04_RegistRecipeIngredients_SuccessfulTes()
        {
            Recipe recipe= new Recipe();
            List<Ingredient> ingredientsSelected = new List<Ingredient>();            
            for (int i=12; i<=21; i++)
            {
                var ingredient = new Ingredient()
                {                    
                    Quantity = i,
                    IdIngredient = i
                };                
                ingredientsSelected.Add(ingredient);
            };
            recipe.Ingredients = ingredientsSelected;
            recipe.IdRecipe = 1;
            Assert.IsTrue(IngredientLogic.RegistRecipeIngredients(recipe));
            
        }

        [TestMethod]
        public void Test05_RegistRecipeIngredients_NotValidTest()
        {
            Recipe recipe = new Recipe();
            List<Ingredient> ingredientsSelected = new List<Ingredient>();
            for (int i = 12; i <= 21; i++)
            {
                var ingredient = new Ingredient()
                {
                    IngredientName = "Test" + i,
                    Quantity = i,
                    IdIngredient = i
                };
                ingredientsSelected.Add(ingredient);
            };
            recipe.Ingredients = ingredientsSelected;
            recipe.IdRecipe = 0;
            Assert.IsFalse(IngredientLogic.RegistRecipeIngredients(recipe));            
        }

        [TestMethod]
        public void Test06_GetRecipeIngredients_Succesfully()
        {
            List<Ingredient> recipeIngrediens = new List<Ingredient>();
            for (int i = 12; i <= 21; i++)
            {
                var ingredient = new Ingredient()
                {        
                    IngredientName="Test",
                    Quantity = i,
                    IdIngredient = i                    
                };
                recipeIngrediens.Add(ingredient);                
            };
            Assert.IsTrue(IngredientLogic.GetRecipeIngredients(1).Count==recipeIngrediens.Count);
        }

        [TestMethod]
        public void Test07_GetRecipeIngredients_NotValidTest()
        {
            Assert.IsFalse(IngredientLogic.GetRecipeIngredients(1).Count == 0);
        }

        [TestMethod]
        public void Test08_DeleteRecipeIngredients_Successfully()
        {
            Assert.IsTrue(IngredientLogic.DeleteRecipeIngredients(1));
        }
        [TestMethod]
        public void Test09_DeleteRecipeIngredients_NotValidTest()
        {
            Assert.IsFalse(IngredientLogic.DeleteRecipeIngredients(0));
        }
    }
}
