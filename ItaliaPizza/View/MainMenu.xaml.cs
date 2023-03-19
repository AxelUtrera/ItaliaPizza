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
    /// Lógica de interacción para MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_CashBox_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Order_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Product_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Kitchen_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_Suplier_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Button_User_Click(object sender, MouseButtonEventArgs e)
        {
            UsersMenu usersMenu = new UsersMenu();
            usersMenu.Show();
        }
    }
}
