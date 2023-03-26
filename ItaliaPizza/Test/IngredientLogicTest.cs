using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

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
            int idRecipe = 1;
            List<Ingredient> ingredientsSelected = new List<Ingredient>();
            Ingredient ingredient = new Ingredient();
            for (int i=1; i<=4; i++)
            {
                ingredient.IngredientName = "Test"+i;
                ingredient.Quantity = i;
                ingredient.IdIngredient = i;
                ingredientsSelected.Add(ingredient);
            };
            Assert.IsTrue(IngredientLogic.RegistRecipeIngredients(ingredientsSelected, idRecipe));
            
        }

        [TestMethod]
        public void Test05_RegistRecipeIngredients_NotValidTest()
        {
            int idRecipe = 0;
            List<Ingredient> ingredientsSelected = new List<Ingredient>();
            Ingredient ingredient = new Ingredient();
            for (int i = 1; i <= 4; i++)
            {
                ingredient.IngredientName = "Test" + i;
                ingredient.Quantity = i;
                ingredientsSelected.Add(ingredient);
            };
            Assert.IsFalse(IngredientLogic.RegistRecipeIngredients(ingredientsSelected, idRecipe));
        }
        [TestMethod]
        public void Test06_DeleteRecipeIngredients_Successfully()
        {
            IngredientLogic.DeleteRecipeIngredients(4);
        }
    }
}
