using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Recipe
    {
        protected int idRecipe;
        protected string nameRecipe;
        protected string descriptionRecipe;
        protected List<Ingredient> ingredientsRecipe;
        protected bool isActive;

        public int IdRecipe { get { return idRecipe; } set { idRecipe = value; } }
        public string NameRecipe { get { return nameRecipe; } set { nameRecipe = value; } }
        public string DescriptionRecipe { get { return descriptionRecipe; } set { descriptionRecipe = value; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public List<Ingredient> Ingredients { get { return ingredientsRecipe; } set { ingredientsRecipe = value; } }

        public Recipe() { }
        public override string ToString()
        {
            return NameRecipe;
        }

    }
}
