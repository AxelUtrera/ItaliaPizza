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
        protected string measurement;
        protected UnitOfMeasurement unitOfMeasurement;
        protected int idMeasurement;
        protected int warningTreshold;


        public UnitOfMeasurement UnitOfMeasurement { get { return unitOfMeasurement; } set { unitOfMeasurement = value; } }
        public int IdIngredient { get { return idIngredinet; } set { idIngredinet = value; } }
        public string IngredientName { get { return ingredientName; } set { ingredientName = value; } }
        public double Quantity { get { return quantity; } set { quantity = value; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public string Measurement { get { return measurement; } set { measurement = value; } }
        public int IdMeasurement { get { return (int)idMeasurement; } set { idMeasurement = value; } }
        public int WarningTreshold { get { return warningTreshold; } set { warningTreshold = value; } }


        public Ingredient()
        {

        }
        public override string ToString()
        {
            return ingredientName;
        }
    }
}