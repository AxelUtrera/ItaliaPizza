using Logic;
using Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System;

namespace View
{
    /// <summary>
    /// Lógica de interacción para Clients.xaml
    /// </summary>
    public partial class CustomerView : Window
    {
        public Address addressSelected { get; set; }

        private ObservableCollection<Address> addressesOnTable;

        public CustomerView()
        {
            InitializeComponent();
            FillTableCustomer();
        }


        private void FillTableCustomer()
        {
            List<Address> listAddresses = AddressLogic.GetAllAddresses();
            addressesOnTable = new ObservableCollection<Address>(listAddresses);
            AddressesTable.ItemsSource = listAddresses;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("No se ha seleccionado ningun usuario, ¿Quiere salir de todos modos?","Cliente no seleccionado",MessageBoxButton.YesNo, MessageBoxImage.Information);
            if(result == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }


        private void Texbox_TextSearchChanged(object sender, TextChangedEventArgs e)
        {
            var searchTextbox = sender as TextBox;
            if (searchTextbox.Text != "")
            {
                var filteredList = addressesOnTable.Where(x => x.nameCustomer.ToLower().Contains(searchTextbox.Text.ToLower()));
                AddressesTable.ItemsSource = null;
                AddressesTable.ItemsSource = filteredList;
            }
            else
            {
                AddressesTable.ItemsSource = addressesOnTable;
            }
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


        private void Button_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Address customerSelected = new Address();
            var row = FindInTable<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                // Obtener el objeto asociado a la fila
                var item = row.DataContext;
                customerSelected = (Address)item;
            }

            MessageBoxResult result = MessageBox.Show("¿El pedido es para "+ customerSelected.nameCustomer+ "?", "Cliente seleccionado", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(result == MessageBoxResult.Yes)
            {
                addressSelected = customerSelected;
                this.Close();
            }
        }
    }
}
