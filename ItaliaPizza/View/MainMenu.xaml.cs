using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Infrastructure;
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
    public partial class MainMenu : Window
    {
        public static Worker workerLogged;
        
        public MainMenu()
        {
            InitializeComponent();
            AddNameUser();
        }


        public void AddNameUser()
        {
                string nameWorker = UserLogic.GetUserById(workerLogged.IdUser).Name;
                Label_NameUser.Content = nameWorker;
        }


        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Login loginView = new Login();
            loginView.Show();
            this.Close();
        }

        private void Button_CashBox_Click(object sender, MouseButtonEventArgs e)
        {
            PendingPayOrders.loggedWorker = workerLogged;
            PendingPayOrders pendingPayOrders = new PendingPayOrders();
            pendingPayOrders.Show();
            Close();
        }


        private void Button_Order_Click(object sender, MouseButtonEventArgs e)
        {
            Orders orders = new Orders();
            Orders.workerLogged = workerLogged;
            orders.ShowDialog();
        }


        private void Button_Product_Click(object sender, MouseButtonEventArgs e)
        {
            Products productWindow = new Products();
            productWindow.ShowDialog();
        }


        private void Button_Kitchen_Click(object sender, MouseButtonEventArgs e)
        {
            KitchenMenu.workerLogged = workerLogged;
            KitchenMenu kitchenMenu = new KitchenMenu();
            Close();
            kitchenMenu.ShowDialog();
        }


        private void Button_Suplier_Click(object sender, MouseButtonEventArgs e)
        {
            SuppliersMenu suppliersMenu = new SuppliersMenu();
            suppliersMenu.Show();
            Close();
        }


        private void Button_User_Click(object sender, MouseButtonEventArgs e)
        {
            UsersView usersView = new UsersView();
            usersView.ShowDialog();
        }

        private void Button_InventaryReport_Click(object sender, RoutedEventArgs e)
        {
            InventoryReport.workerLogged = workerLogged;
            InventoryReport inventoryReport = new InventoryReport();
            Close();
            inventoryReport.ShowDialog();
        }
    }
}
