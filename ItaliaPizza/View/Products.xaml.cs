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
using System.Xml.Linq;

namespace View
{
    /// <summary>
    /// Lógica de interacción para Products.xaml
    /// </summary>
    public partial class Products : Window
    {

        private ObservableCollection<ProductToView> products;

        public Products()
        {
            InitializeComponent();
            SetItemsToProductsTable();
        }

        public void SetItemsToProductsTable()
        {
            List<ProductToView> allProducts = ProductLogic.GetAllProductToView();
            products = new ObservableCollection<ProductToView>(allProducts);
            ProductsTable.ItemsSource = products;
        }
        

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            this.Close();
        }

		private void ProductsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

		private void Button_DeleteProduct_Click(object sender, MouseButtonEventArgs e)
		{
			if (ProductsTable.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("¿Está seguro de cancelar la eliminación del producto?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

				if (result == MessageBoxResult.Yes)
				{
					string productCode = ((ProductToView)ProductsTable.SelectedItem).ProductCode;
					int statusCode = ProductLogic.DeleteProduct(productCode);

					if (statusCode == 200)
					{
						MessageBox.Show("Producto eliminado correctamente.");
						this.Close();
						Products productsWindow = new Products();
						productsWindow.ShowDialog();
					}
					else
					{
						MessageBox.Show("Ha ocurrido un error, inténtelo de nuevo.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
				}
				else
				{
					Close();
				}
			}
			else
			{
				MessageBox.Show("Por favor seleccione un producto.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}


        private T FindInTable<T>(DependencyObject current) where T : DependencyObject
        {
            T objectObtained = null;
            do
            {
                if (current is T ancestor)
                {
                    objectObtained = ancestor;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return objectObtained;
        }


        private void Button_EditProduct_Click(object sender, MouseButtonEventArgs e)
        {
            ProductToView productSelected = new ProductToView();

            var row = FindInTable<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                // Obtener el objeto asociado a la fila
                var item = row.DataContext;
                productSelected = (ProductToView)item;
            }

            EditProduct.productToView = productSelected;

            if (productSelected != null)
            {
                EditProduct ed = new EditProduct();
                ed.Show();
                this.Close();
            }
        }


		private void Button_Add_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				AddProduct addProduct = new AddProduct();
				addProduct.Show();
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Error: {ex.Message}");
			}
		}


        private void Texbox_TextSearchChanged(object sender, TextChangedEventArgs e)
        {
            var searchTextbox = sender as TextBox;
            if (searchTextbox.Text != "")
            {
                var filteredList = products.Where(x => x.Name.Contains(searchTextbox.Text));
                ProductsTable.ItemsSource = null;
                ProductsTable.ItemsSource = filteredList;
            }
            else
            {
                ProductsTable.ItemsSource = products;
            }
        }

    }
}
