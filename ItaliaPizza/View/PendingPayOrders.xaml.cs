using Logic;
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
using Model;

namespace View
{
    public partial class PendingPayOrders : Window
    {
        public static Worker loggedWorker;
        public static PendingPayOrders instance { get; private set; }

        public PendingPayOrders()
        {
            InitializeComponent();
            RecoverPayPendingOrders();
            instance = this;
        }

        private void Textbox_SearchSuppliersOrder_Input(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_PayOrder_Click(object sender, RoutedEventArgs e)
        {
            Order selectedOrder = OrdersTable.SelectedItem as Order;

            if(selectedOrder != null)
            {
                PayOrder.workerLogged = loggedWorker;
                PayOrder.payingOrder = selectedOrder;
                PayOrder payOrder = new PayOrder();
                payOrder.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleccione una orden", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu main = new MainMenu();
            main.Show();
            Close();
        }


        public void RecoverPayPendingOrders()
        {
            List<Order> ordersRecover = OrderLogic.GetPayPendingOrders();
            ObservableCollection<Order> orderView = new ObservableCollection<Order>(ordersRecover);
            OrdersTable.ItemsSource = orderView;
        }
    }
}
