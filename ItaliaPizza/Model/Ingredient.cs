using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Ingredient
    {
        protected int idIngredinet;
        protected string ingredientName;
        protected double quantity;
        protected bool isActive;

        public int IdIngredient { get { return idIngredinet; } set { idIngredinet = value; } }
        public string IngredientName { get { return ingredientName; } set { ingredientName = value; } }
        public double Quantity { get { return quantity; } set { quantity = value; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }

		public Ingredient() { }

        public override string ToString()
        {
            return ingredientName;
        }
    }
}