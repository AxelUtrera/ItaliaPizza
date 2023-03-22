using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Lógica de interacción para Products.xaml
    /// </summary>
    public partial class Products : Window
    {

        public ObservableCollection<Product> products;
        public Products()
        {
            InitializeComponent();
            SetItemsToProductsTable();
        }

        public void SetItemsToProductsTable()
        {
            List<Product> allProducts = ProductLogic.GetAllProduct();
            products = new ObservableCollection<Product>(allProducts);
            ProductsTable.ItemsSource = products;
        }
        

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

		private void ProductsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_DeleteProduct_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_EditProduct_Click(object sender, MouseButtonEventArgs e)
        {

        }

		private void Button_Add_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AddProduct addProduct = new AddProduct();
				addProduct.Show();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
			}
		}

	}
}
