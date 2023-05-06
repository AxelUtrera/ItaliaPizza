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
    /// Lógica de interacción para SuppliersMenu.xaml
    /// </summary>
    public partial class SuppliersMenu : Window
    {
        public SuppliersMenu()
        {
            InitializeComponent();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show();
            Close();
        }

        private void Button_SuppliersView_Click(object sender, RoutedEventArgs e)
        {
            SuppliersView view = new SuppliersView();
            view.Show();
        }

        private void Button_SuppliersOrdersView_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
