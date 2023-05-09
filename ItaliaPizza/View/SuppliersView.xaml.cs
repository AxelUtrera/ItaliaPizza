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
    /// Lógica de interacción para SuppliersView.xaml
    /// </summary>
    public partial class SuppliersView : Window
    {
        public ObservableCollection<Supplier> activeSuppliers;

        public SuppliersView()
        {
            InitializeComponent();
            RecoverActiveSuppliers();
        }


        private void RecoverActiveSuppliers()
        {
            List<Supplier> suppliers = SupplierLogic.RecoverActiveSuppliers();
            activeSuppliers = new ObservableCollection<Supplier>(suppliers);
            SuppliersTable.ItemsSource = activeSuppliers;
        }


        private void Button_SupplierRegister_Click(object sender, RoutedEventArgs e)
        {
            SupplierRegister supplierRegister = new SupplierRegister();
            supplierRegister.Show();
            Close();
        }


        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            SuppliersMenu suppliersMenu = new SuppliersMenu();
            suppliersMenu.Show();
            Close();
        }


        private void Button_DeleteSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersTable.SelectedItem != null)
            {
                int supplierId = ((Supplier)SuppliersTable.SelectedItem).IdSupplier;

                var messageResponse = MessageBox.Show("¿Desea eliminar al proveedor?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (messageResponse == MessageBoxResult.Yes)
                {
                    int statusCode = SupplierLogic.DeleteSupplierById(supplierId);

                    if (statusCode == 200)
                    {
                        MessageBox.Show("Proveedor eliminado con éxito", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        RecoverActiveSuppliers();
                    }
                    else
                    {
                        MessageBox.Show("El sistema no esta disponible, intentelo mas tarde", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_ModifySupplier_Click(object sender, RoutedEventArgs e)
        {
            if (SuppliersTable.SelectedItem != null)
            {
                var selectedSupplier = SuppliersTable.SelectedItem as Supplier;
                SupplierRegister supplierRegister = new SupplierRegister();
                supplierRegister.SetModifySupplierData(selectedSupplier);
                supplierRegister.Show();
            }
            else
            {
                MessageBox.Show("Seleccione un proveedor", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Textbox_SearchSuppliers_Input(object sender, TextChangedEventArgs e)
        {
            var searchText = sender as TextBox;
            if (searchText != null)
            {
                var filteredList = activeSuppliers.Where(x => x.SupplierName.Contains(searchText.Text));
                SuppliersTable.ItemsSource = null;
                SuppliersTable.ItemsSource = filteredList;
            }
            else
            {
                SuppliersTable.ItemsSource = activeSuppliers;
            }
        }

    }
}
