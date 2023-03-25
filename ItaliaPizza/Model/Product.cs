using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
		public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public double Price { get; set; }
		public bool Preparation { get; set; }
		public string ProductName { get; set; }
        public int IdRecipe { get; set; }
        public bool Active { get; set; }
        public bool IsActive { get; set; }
        public byte[] Picture { get; set; }
        public string Restrictions { get; set; }

		public Product(string name, string description, string productCode, byte[] picture, double price, bool preparation, string productName, string restrictions, int idRecipe, bool active)
		{
			this.Name = name;
			this.Description = description ?? throw new ArgumentNullException(nameof(description));
			this.ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
			this.Picture = picture;
			this.Price = price;
			this.Preparation = preparation;
			this.ProductName = productName ?? throw new ArgumentNullException(nameof(productName));
			this.Restrictions = restrictions ?? throw new ArgumentNullException(nameof(restrictions));
			this.IdRecipe = idRecipe;
			this.Active = active;
		}

		public Product()
        {
        }
	}
}
