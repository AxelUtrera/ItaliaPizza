using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Model;

namespace Logic.Test
{
    [TestClass]
    public class RecipeLogicTest
    {
        [TestMethod]
        public void Test01_GetRecipes_SuccessfulTest()
        {
            List<Recipe> recipesList = Logic.RecipeLogic.GetRecipes();
            bool response = recipesList.Count > 0;
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void Test02_GetRecipes_NotValidTest()
        {
            List<Recipe> recipesList = Logic.RecipeLogic.GetRecipes();
            bool response = recipesList.Count <= 0;
            Assert.IsFalse(response);
        }

        [TestMethod]
        public void Test03_RegistRecipe_SuccessfulTest()
        {
            List<Ingredient> ingredientsList = new List<Ingredient>();
            for (int i = 0; i >= 5; i++)
            {
                Ingredient ingredient = new Ingredient()
                {
                    IngredientName = "TestName:" + i.ToString(),
                    IsActive = true,
                    IdIngredient = i,
                    Quantity = 1,

                };
                ingredientsList.Add(ingredient);
            }

            Recipe recipe = new Recipe()
            {
                NameRecipe = "TestReceta4",
                DescriptionRecipe = "TestDescription",
                IsActive = false,
                Ingredients = ingredientsList

            };
            Assert.IsTrue(Logic.RecipeLogic.RegistRecipe(recipe));
        }

        [TestMethod]
        public void Test04_GetIdRecipe_SuccessfulTest()
        {
            bool response = Logic.RecipeLogic.GetIdRecipe("TestReceta") == 12;
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void Test05_AllreadyExist_Idle()
        {
            string response = "idle";
            Assert.IsTrue(RecipeLogic.AllreadyExist("TestReceta4").Equals(response));
        }

        [TestMethod]
        public void Test06_ActivateRecipe_SuccessfulTest()
        {
            int id = RecipeLogic.GetIdRecipe("TestReceta4");
            Assert.IsTrue(Logic.RecipeLogic.ActivateRecipe(id));
        }

        [TestMethod]
        public void Test07_ActivateRecipe_NotValidTest()
        {
            Assert.IsFalse(RecipeLogic.ActivateRecipe(0101));
        }

        [TestMethod]
        public void Test08_EditRecipe_SuccessfulTest()
        {
            Recipe recipe = new Recipe()
            {
                NameRecipe = "TestReceta4",
                DescriptionRecipe = "EditDescriptionRecipe1",
                IdRecipe = RecipeLogic.GetIdRecipe("TestReceta2")
            };
            Assert.IsTrue(RecipeLogic.EditRecipe(recipe));
        }

        [TestMethod]
        public void Test09_EditRecipe_NotValidTest()
        {
            Recipe recipe = new Recipe()
            {
                NameRecipe = "TestReceta4",
                DescriptionRecipe = "EditDescriptionRecipe1",
                IdRecipe = 99099
            };
            Assert.IsFalse(RecipeLogic.EditRecipe(recipe));
        }

        [TestMethod]
        public void Test10_AllreadyExist_Exist()
        {
            string response = "exist";
            Assert.IsTrue(RecipeLogic.AllreadyExist("TestReceta4").Equals(response));
        }

        [TestMethod]
        public void Test11_AllreadyExist_DoesNotExist()
        {
            string response = "doesNotExist";
            Assert.IsTrue(RecipeLogic.AllreadyExist("TestReceta999").Equals(response));
        }

        [TestMethod]
        public void Test12_DeleteRecipe_SuccessfulTest()
        {
            int id = RecipeLogic.GetIdRecipe("TestReceta4");
            Assert.IsTrue(RecipeLogic.DeleteRecipe(id));
        }

        [TestMethod]
        public void Test13_DeleteRecipe_NotValidTest()
        {
            int id = 1010101;
            Assert.IsFalse(RecipeLogic.DeleteRecipe(id));
        }
    }
}
