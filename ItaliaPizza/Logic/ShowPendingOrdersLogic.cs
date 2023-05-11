using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
	public class ShowPendingOrdersLogic
	{
		public List<ProductWithRecipe> GetPendingProducts()
		{
			using (var context = new ItaliaPizzaEntities())
			{
				var productList = (from p in context.product join r in context.recipe on p.idRecipe equals r.idRecipe where p.active && p.preparation select new ProductWithRecipe
				{
					ProductCode = p.productCode,
					Name = p.productName,
					Picture = p.picture,
					Quantity = p.quantity,
					IdRecipe = p.idRecipe,
					NameRecipe = r.recipeName

				}).ToList();

				return productList;
			}
		}

		public ProductWithRecipe GetProductWithHighestIdRecipe()
		{
			using (var context = new ItaliaPizzaEntities())
			{
				var product = (from p in context.product join r in context.recipe on p.idRecipe equals r.idRecipe where p.active && p.preparation orderby p.idRecipe descending select new ProductWithRecipe
				{
					ProductCode = p.productCode,
					Name = p.productName,
					Picture = p.picture,
					Quantity = p.quantity,
					IdRecipe = p.idRecipe,
					NameRecipe = r.recipeName

				}).FirstOrDefault();

				return product;
			}
		}

	}
}

namespace Model
{
	public class ProductWithRecipe
	{
		public string ProductCode { get; set; }
		public string Name { get; set; }
		public byte[] Picture { get; set; }
		public double Quantity { get; set; }
		public int IdRecipe { get; set; }
		public string NameRecipe { get; set; }
	}
}