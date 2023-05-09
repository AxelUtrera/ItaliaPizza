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
    /// Lógica de interacción para SuppliersOrderView.xaml
    /// </summary>
    public partial class SuppliersOrderView : Window
    {
        public SuppliersOrderView()
        {
            InitializeComponent();
        }

        private void Button_SupplierOrderRegister_Click(object sender, RoutedEventArgs e)
        {
            SupplierOrderRegister supplierOrderRegister = new SupplierOrderRegister();
            supplierOrderRegister.Show();
            Close();
        }

        private void Textbox_SearchSuppliersOrder_Input(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            SuppliersMenu suppliersMenu = new SuppliersMenu();
            suppliersMenu.Show();
            Close();
        }

        private void Button_DeleteSupplierOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_ModifySupplierOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        
    }
}
