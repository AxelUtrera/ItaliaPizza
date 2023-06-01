using Model;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para CashBoxMenu.xaml
    /// </summary>
    public partial class CashBoxMenu : Window
    {
        public static Worker workerLogged;

        public CashBoxMenu()
        {
            InitializeComponent();
        }

        private void Image_ButtonPendingPayOrders_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PendingPayOrders.loggedWorker = workerLogged;
            PendingPayOrders pendingPayOrders = new PendingPayOrders();
            pendingPayOrders.Show();
            Close();
        }
        private void Image_ButtonTakeOutCash_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            TakeOutCash.workerLogged = workerLogged;
            TakeOutCash takeOutCashWindow = new TakeOutCash();
            Close();
            takeOutCashWindow.ShowDialog();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu.workerLogged = workerLogged;
            MainMenu mainMenuWindow = new MainMenu();
            Close();
            mainMenuWindow.ShowDialog();
        }
    }
}
