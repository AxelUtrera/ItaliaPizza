using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace View
{
    /// <summary>
    /// Lógica de interacción para Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        public static Worker workerLogged { get; set; }
        private ObservableCollection<Order> ordersToTable;

        public Orders()
        {
            InitializeComponent();
            AddOrdersToTable();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            RegisterOrder newOrderView = new RegisterOrder();
            RegisterOrder.workerLogged = workerLogged;
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
                var filteredList = ordersToTable.Where(x => x.nameCustomer.ToLower().Contains(searchTextbox.Text.ToLower()));
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
            Order orderSelected = new Order();
            var row = FindInTable<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                var item = row.DataContext;
                orderSelected = (Order)item;
            }

            if(orderSelected != null)
            {
                string[] statusValues = { "Pendiente", "En preparación", "Preparado", "Entregado" };
                int indexActualStatus = Array.IndexOf(statusValues, orderSelected.status);

                MessageBoxResult resultMessageBox = MessageBox.Show($"El estatus del pedido cambiará a: {statusValues[indexActualStatus + 1]}", "Cambiar estado de pedido", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    OrderLogic.ChangeOrderStatus(orderSelected);
                }

                AddOrdersToTable();
            }
        }


        private void Button_Edit_Click(object sender, MouseButtonEventArgs e)
        {
            Order orderSelected = new Order();
            var row = FindInTable<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                // Obtener el objeto asociado a la fila
                var item = row.DataContext;
                orderSelected = (Order)item;
            }

   
            List<ProductToView> productsInOrder = ProductLogic.GetProductByIdOrder(orderSelected.idOrder);
            RegisterOrder registerOrderWindow = new RegisterOrder();
            registerOrderWindow.Label_WindowTittle.Content = "Editar pedido";
            registerOrderWindow.Button_DeliveryOrder.IsEnabled = false;
            registerOrderWindow.Button_DeliveryOrder.Opacity = .5;
            registerOrderWindow.Button_LocalOrder.IsEnabled = false;
            registerOrderWindow.Button_LocalOrder.Opacity = .5;
            registerOrderWindow.Button_RegisterOrder.Visibility = Visibility.Collapsed;
            registerOrderWindow.Button_SaveChanges.Visibility = Visibility.Visible;
            registerOrderWindow.Label_ClientName.Content = orderSelected.nameCustomer;
            registerOrderWindow.SetDataOnProductInOrderTable(productsInOrder, orderSelected);
            registerOrderWindow.Show();

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


        private void Button_Cancel_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
