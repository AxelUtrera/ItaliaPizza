using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ProductToView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Price { get; set; }
        public string Preparation { get; set; }
        public string IdRecipe { get; set; }
        public string Active { get; set; }
        public string Restrictions { get; set; }


        public ProductToView()
        {
        }
    }
}
