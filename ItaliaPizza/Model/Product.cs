using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Product
    {
		private string preparation;
		private string active;
		private bool active1;
		private string productName1;
		private bool preparation1;
		private string productName2;
		private int v1;
		private int v2;
		private bool active2;


		public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public double Price { get; set; }
        public bool Preparation { get; set; }
        public int IdRecipe { get; set; }
        public bool Active { get; set; }
        public byte[] Picture { get; set; }
        public string Restrictions { get; set; }


        public Product()
        {
        }


        public Product(string name, string description, string productCode, byte[] picture, double price, bool preparation, string restrictions, int idRecipe, bool active)
        {
            this.Name = name;
            this.Description = description ?? throw new ArgumentNullException(nameof(description));
            this.ProductCode = productCode ?? throw new ArgumentNullException(nameof(productCode));
            this.Picture = picture;
            this.Price = price;
            this.Preparation = preparation;
            this.Restrictions = restrictions ?? throw new ArgumentNullException(nameof(restrictions));
            this.IdRecipe = idRecipe;
            this.Active = active;
        }


		public Product(string productName, string description, byte[] picture, double price, string preparation, string restrictions, string active)
		{
			Name = productName;
			Description = description;
			Picture = picture;
			Price = price;
			this.preparation = preparation;
			Restrictions = restrictions;
			this.active = active;
		}


		public Product(string productName, string description, byte[] picture, double price, string preparation, string restrictions, bool active1)
		{
			Name = productName;
			Description = description;
			Picture = picture;
			Price = price;
			this.preparation = preparation;
			Restrictions = restrictions;
			this.active1 = active1;
		}


		public Product(string productName, string description, byte[] picture, double price, bool preparation1, string productName2, string restrictions, int v1, int v2, bool active2)
		{
			this.Name = productName;
			Description = description;
			Picture = picture;
			Price = price;
			this.preparation1 = preparation1;
			this.productName2 = productName2;
			Restrictions = restrictions;
			this.v1 = v1;
			this.v2 = v2;
			this.active2 = active2;
		}


	}
}
