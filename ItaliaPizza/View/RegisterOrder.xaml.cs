using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace View
{
    /// <summary>
    /// Lógica de interacción para RegisterOrder.xaml
    /// </summary>
    public partial class RegisterOrder : Window
    {
        
        private ObservableCollection<ProductToView> productsOnTable;

        //los siguientes 2 son los atributos que se envian al metodo que guarda la orden en la base de datos.
        private List<ProductToView> productsInOrder = new List<ProductToView>();
        public Address addressOrder = new Address();

        public RegisterOrder()
        {
            InitializeComponent();
            AddProductsToTable();
        }

      
        private void AddProductsToTable()
        {
            List<ProductToView> listProducts = ProductLogic.GetAllProductToView();
            productsOnTable = new ObservableCollection<ProductToView>(listProducts);
            ProductTable.ItemsSource = listProducts;
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


        private void Button_AddProduct_Click(object sender, MouseButtonEventArgs e)
        {
            ProductToView productSelected = new ProductToView();
            var row = FindInTable<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                // Obtener el objeto asociado a la fila
                var item = row.DataContext;
                productSelected = (ProductToView)item;
            }

            AddToOrder(productSelected);
            ReloadOrderTable();
            Label_Subtotal.Content = "$" +SetSubtotal().ToString("N2");
            Label_IVA.Content = "$"+SetIVA().ToString("N2");
            Label_Total.Content = "$" + SetTotal().ToString("N2");
        }
       

        private void AddToOrder(ProductToView product)
        {
            ProductToView productInOrder = productsInOrder.FirstOrDefault(p => p.Name.Equals(product.Name));

            if (productInOrder != null)
            {
                productInOrder.Quantity += 1;
                product.SubtotalProduct = "$"+(Double.Parse(product.Price.Replace("$", "")) * product.Quantity).ToString();

            }
            else
            {
                product.Quantity = 1;
                product.SubtotalProduct = "$"+(Double.Parse(product.Price.Replace("$", "")) * product.Quantity).ToString();
                productsInOrder.Add(product);
            }
        }


        private double SetSubtotal()
        {
            double subtotal = 0;
            foreach(ProductToView item in productsInOrder)
            {
                subtotal += Double.Parse(item.SubtotalProduct.Replace("$", ""));
            }
            return subtotal;
        }


        private double SetTotal()
        {
            return SetSubtotal() + SetIVA();
        }


        private double SetIVA()
        {
            double subtotal = SetSubtotal();
            double IVA = subtotal * .16;
            return IVA;
        }


        private void ReloadOrderTable()
        {
            OrderClientTable.ItemsSource = null;
            OrderClientTable.ItemsSource = productsInOrder;
        }


        private void Texbox_TextSearchChanged(object sender, TextChangedEventArgs e)
        {
            var searchTextbox = sender as TextBox;
            if (searchTextbox.Text != "")
            {
                var filteredList = productsOnTable.Where(x => x.Name.ToLower().Contains(searchTextbox.Text.ToLower()));
                ProductTable.ItemsSource = null;
                ProductTable.ItemsSource = filteredList;
            }
            else
            {
                ProductTable.ItemsSource = productsOnTable;
            }
        }


        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Orders orderView = new Orders();
            orderView.Show();
            this.Close();
        }


        private void ProductsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void ClientOrderTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void Button_RegisterOrder_Click(object sender, RoutedEventArgs e)
        {
            if (productsInOrder.Count != 0 && Label_ClientName.Content != null)
            {
                //Agregar codigo para enviar a la siguiente pantalla.

            }
            else
            {
                MessageBox.Show("Por favor agregue poroductos y el cliente a la orden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_LocalOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageNameClient nameClient = new MessageNameClient();
            nameClient.Closed += MessageNameClient_Closed;
            nameClient.ShowDialog();
        }


        private void MessageNameClient_Closed(object sender, EventArgs e)
        {
            MessageNameClient messageNameClient = (MessageNameClient)sender;
            string nombreCliente = messageNameClient.nameClient;
            Label_ClientName.Content = nombreCliente;
        }


        private void Button_OrderToAddress_Click(object sender, RoutedEventArgs e)
        {
            CustomerView customerView = new CustomerView();
            customerView.Closed += CustomerView_Closed;
            customerView.ShowDialog();
        }


        private void CustomerView_Closed(object sender, EventArgs e)
        {
            CustomerView customerView = (CustomerView)sender;
            Address customerData = customerView.addressSelected;
            addressOrder = customerData;
            Label_ClientName.Content = customerData.nameCustomer;
        }


        private void Button_QuitProductOfOrder_Click(object sender, MouseButtonEventArgs e)
        {
            ProductToView productSelected = new ProductToView();
            var row = FindInTable<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                // Obtener el objeto asociado a la fila
                var item = row.DataContext;
                productSelected = (ProductToView)item;
            }

            MessageBoxResult response = MessageBox.Show("¿Desea quitar el producto?", "Quitar producto", MessageBoxButton.YesNo, MessageBoxImage.Question);
            
            if (response == MessageBoxResult.Yes)
            {
                productsInOrder.Remove(productSelected);
            }
            ReloadOrderTable();
            Label_Subtotal.Content = "$" + SetSubtotal().ToString("N2");
            Label_IVA.Content = "$" + SetIVA().ToString("N2");
            Label_Total.Content = "$" + SetTotal().ToString("N2");
        }


        private void Button_MinusProductOfOrder_Click(object sender, MouseButtonEventArgs e)
        {
            ProductToView productSelected = new ProductToView();
            var row = FindInTable<DataGridRow>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                var item = row.DataContext;
                productSelected = (ProductToView)item;
            }
            ProductToView productToMinus = productsInOrder.FirstOrDefault(p => p.Name.Equals(productSelected.Name));
            if(productToMinus != null)
            {
                if(productToMinus.Quantity - 1 != 0)
                {
                    productToMinus.Quantity -= 1;
                }
                else
                {
                    productsInOrder.Remove(productToMinus);
                }
            }
            ReloadOrderTable();
            Label_Subtotal.Content = "$" + SetSubtotal().ToString("N2");
            Label_IVA.Content = "$" + SetIVA().ToString("N2");
            Label_Total.Content = "$" + SetTotal().ToString("N2");
        }
    }
}
