﻿using Logic;
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

        private ObservableCollection<Product> products;
        public Products()
        {
            InitializeComponent();
            SetItemsToProductsTable();
        }

        private void SetItemsToProductsTable()
        {
            List<Product> allProducts = ProductLogic.getAllProduct();
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
