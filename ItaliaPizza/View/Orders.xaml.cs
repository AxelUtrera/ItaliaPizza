﻿using DataAccess;
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
    /// Lógica de interacción para Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {

        private ObservableCollection<Order> ordersToTable;

        public Orders()
        {
            InitializeComponent();
            AddOrdersToTable();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            RegisterOrder newOrderView = new RegisterOrder();
            newOrderView.Show();
            this.Close();
        }

        private void AddOrdersToTable()
        {
            List<Order> allOrders = OrderLogic.GetOrders();
            ordersToTable = new ObservableCollection<Order>(allOrders);
            OrdersTable.ItemsSource = ordersToTable;
        }

        private void Texbox_TextSearchChanged(object sender, TextChangedEventArgs e)
        {
            var searchTextbox = sender as TextBox;
            if (searchTextbox.Text != "")
            {
                var filteredList = ordersToTable.Where(x => x.nameClient.ToLower().Contains(searchTextbox.Text.ToLower()));
                OrdersTable.ItemsSource = null;
                OrdersTable.ItemsSource = filteredList;
            }
            else
            {
                OrdersTable.ItemsSource = ordersToTable;
            }
        }

        private void OrdersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Update_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Edit_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Cancel_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}