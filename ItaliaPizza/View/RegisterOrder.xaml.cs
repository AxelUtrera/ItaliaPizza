using DataAccess;
using Logic;
using Model;
using Syncfusion.Windows.Forms.Collections;
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
    /// Lógica de interacción para RegisterOrder.xaml
    /// </summary>
    public partial class RegisterOrder : Window
    {

        private ObservableCollection<ProductToView> productsOnTable;

        public RegisterOrder()
        {
            InitializeComponent();
            AddProductsToTable();
        }


        private void AddProductsToTable()
        {
            List<ProductToView> listProducts = ProductLogic.GetAllProductToView();
            productsOnTable = new ObservableCollection<ProductToView>(listProducts);
            ProductTable.ItemsSource = listProducts;
        }

        private void Button_AddProduct_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Texbox_TextSearchChanged(object sender, TextChangedEventArgs e)
        {
            var searchTextbox = sender as TextBox;
            if (searchTextbox.Text != "")
            {
                var filteredList = productsOnTable.Where(x => x.Name.ToLower().Contains(searchTextbox.Text.ToLower()));
                ProductTable.ItemsSource = null;
                ProductTable.ItemsSource = filteredList;
            }
            else
            {
                ProductTable.ItemsSource = productsOnTable;
            }
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Orders orderView = new Orders();
            orderView.Show();
            this.Close();
        }

        private void ProductsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ClientOrderTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_PayOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_LocalOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_OrderToAddress_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
