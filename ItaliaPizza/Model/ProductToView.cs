using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Model
{
    public class ProductToView
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProductCode { get; set; }
        public string Price { get; set; }
        public bool Preparation { get; set; }
        public string IdRecipe { get; set; }
        public string Active { get; set; }
        public string Restrictions { get; set; }
        public BitmapImage Image { get; set; }


        public ProductToView()
        {
        }
    }
}
