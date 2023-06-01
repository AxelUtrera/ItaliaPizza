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
    /// Lógica de interacción para SuppliersOrderView.xaml
    /// </summary>
    public partial class SuppliersOrderView : Window
    {

        public ObservableCollection<SupplierOrder> activeOrders;

        public SuppliersOrderView()
        {
            InitializeComponent();
            SetSuppliersOrdersInfo();
        }

        private void Button_SupplierOrderRegister_Click(object sender, RoutedEventArgs e)
        {
            SupplierOrderRegister supplierOrderRegister = new SupplierOrderRegister();
            supplierOrderRegister.Show();
            Close();
        }

        private void Textbox_SearchSuppliersOrder_Input(object sender, TextChangedEventArgs e)
        {
            var searchText = sender as TextBox;
            if (searchText != null)
            {
                var filteredList = activeOrders.Where(x => x.OrderNumber.Contains(searchText.Text));
                OrdersTable.ItemsSource = null;
                OrdersTable.ItemsSource = filteredList;
            }
            else
            {
                OrdersTable.ItemsSource = activeOrders;
            }
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            SuppliersMenu suppliersMenu = new SuppliersMenu();
            suppliersMenu.Show();
            Close();
        }

        private void Button_DeleteSupplierOrder_Click(object sender, RoutedEventArgs e)
        {
            if(OrdersTable.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea eliminar el pedido a proveedor?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if(result == MessageBoxResult.Yes)
                {
                    string orderNumber = ((SupplierOrder)OrdersTable.SelectedItem).OrderNumber;

                    if(SupplyOrderLogic.DeleteSupplierOrder(orderNumber) == 200)
                    {
                        MessageBox.Show("Pedido a proveedor eliminado con exito", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        SetSuppliersOrdersInfo();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar eliminar al usuario, inténtelo de nuevo.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un pedido", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        private void SetSuppliersOrdersInfo()
        {
            List<SupplierOrder> supplierOrders = SupplyOrderLogic.RecoverActiveOrders();
            activeOrders = new ObservableCollection<SupplierOrder>(supplierOrders);

            OrdersTable.ItemsSource = activeOrders;
        }

    }
}
