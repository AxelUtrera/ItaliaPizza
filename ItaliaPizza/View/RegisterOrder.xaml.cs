using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public static Worker workerLogged { get; set; }
        public static Order orderToCreate = new Order();
        private List<ProductToView> productsInOrder = new List<ProductToView>();
        private Address addressOrder = new Address();
        private List<ProductToView> productsOnTable;
       
        public RegisterOrder()
        {
            InitializeComponent();
            AddProductsToTable();
        }


        public void SetDataOnProductInOrderTable(List<ProductToView> productsList, Order orderToEdit)
        {
            productsInOrder = productsList;
            orderToCreate = orderToEdit;
            OrderClientTable.ItemsSource = productsList;

            Label_Subtotal.Content = "$" + SetSubtotal().ToString("N2");
            Label_IVA.Content = "$" + SetIVA().ToString("N2");
            Label_Total.Content = "$" + SetTotal().ToString("N2");
        }


        private void AddProductsToTable()
        {
            List<ProductToView> listProducts = ProductLogic.GetAllProductToView();
            productsOnTable = listProducts;
            ProductTable.ItemsSource = productsOnTable;
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

            if (productSelected != null)
            {
                if (productSelected.Quantity > 0)
                {
                    int productIndex = productsOnTable.FindIndex(p => p.Name.Equals(productSelected.Name));
                    productsOnTable[productIndex].Quantity -= 1;
                    AddToOrder(productSelected);
                }
                else
                {
                    MessageBox.Show("No quedan mas productos de este tipo", "Sin disponibilidad", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                Console.WriteLine(productSelected.Quantity);
            }

            ReloadProductTable();
            ReloadOrderTable();
            Label_Subtotal.Content = "$" +SetSubtotal().ToString("N2");
            Label_IVA.Content = "$"+SetIVA().ToString("N2");
            Label_Total.Content = "$" + SetTotal().ToString("N2");
        }


        private void ReloadProductTable()
        {
            ProductTable.ItemsSource = null;
            ProductTable.ItemsSource = productsOnTable;
        }


        private void AddToOrder(ProductToView product)
        {
            ProductToView inOrder = productsInOrder.FirstOrDefault(p => p.Name.Equals(product.Name));

            if (inOrder != null)
            {
                inOrder.Quantity += 1;
                inOrder.SubtotalProduct = "$"+(Double.Parse(inOrder.Price.Replace("$", "")) * inOrder.Quantity);
            }
            else
            {   
                ProductToView newProductForOrder = new ProductToView() 
                { 
                    Name = product.Name,
                    Price = product.Price,
                    SubtotalProduct = product.SubtotalProduct,
                    Quantity = 1,
                    ProductCode = product.ProductCode,
                    Active = product.Active,
                    Description = product.Description
                };

                newProductForOrder.SubtotalProduct = "$"+(Double.Parse(newProductForOrder.Price.Replace("$", "")) * newProductForOrder.Quantity);
                productsInOrder.Add(newProductForOrder);
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
            MessageBoxResult result = MessageBox.Show("¿Quiere salir?, los datos no seran guardados","Cancelar registro", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                Orders orders = new Orders();
                orders.Show();
                this.Close();
            }
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
                orderToCreate.typeOrder = addressOrder.idCustomer != 0 ? "Domicilio" : "Local";
                orderToCreate.status = "Pendiente";
                DateTime actualDateTime = DateTime.Now;
                orderToCreate.date = actualDateTime.ToString("dd/MM/yyyy");
                orderToCreate.hour = actualDateTime.ToString("HH:mm:ss");
                orderToCreate.idWorker = workerLogged.Username;
                orderToCreate.nameCustomer = Label_ClientName.Content.ToString();

                string dataTotal = Label_Total.Content.ToString();
                string cleanTotal = dataTotal.Replace("$", "");
                orderToCreate.total = Double.Parse(cleanTotal);

                if (addressOrder.idCustomer != 0) {
                    if (OrderLogic.AddOrder(orderToCreate, productsInOrder, addressOrder) == 200 && ProductLogic.UpdateQuantityProduct(productsInOrder) == 200)
                    {
                        MessageBox.Show("Se agrego un nuevo pedido!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al registrar la orden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    if (OrderLogic.AddOrder(orderToCreate, productsInOrder) == 200 && ProductLogic.UpdateQuantityProduct(productsInOrder) == 200)
                    {
                        MessageBox.Show("Se agrego un nuevo pedido!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ocurrio un error al registrar la orden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                Orders orders = new Orders();
                orders.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor agregue poroductos y el cliente a la orden.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_LocalOrder_Click(object sender, RoutedEventArgs e)
        {
            MessageNameClient nameClient = new MessageNameClient();
            addressOrder.idCustomer = 0;
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
            if(customerData != null) 
            {
                addressOrder = customerData;
                Label_ClientName.Content = customerData.nameCustomer;
            }
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
                ProductToView productOnProductTable = productsOnTable.FirstOrDefault(p => p.Name.Equals(productSelected.Name));
                productOnProductTable.Quantity += productSelected.Quantity;
            }

            ReloadProductTable();
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
                    double subtotalProductToDouble = Double.Parse(productToMinus.SubtotalProduct.Replace("$", ""));
                    double priceProductToDouble = Double.Parse(productToMinus.Price.Replace("$", ""));
                    productToMinus.SubtotalProduct = "$" + (subtotalProductToDouble - priceProductToDouble);
                    AddOneAtQuantityOnProductTable(productSelected);
                }
                else
                {
                    AddOneAtQuantityOnProductTable(productSelected);
                    productsInOrder.Remove(productToMinus);
                }
            }

            ReloadOrderTable();
            ReloadProductTable();
            Label_Subtotal.Content = "$" + SetSubtotal().ToString("N2");
            Label_IVA.Content = "$" + SetIVA().ToString("N2");
            Label_Total.Content = "$" + SetTotal().ToString("N2");
        }


        private void AddOneAtQuantityOnProductTable(ProductToView productSelected)
        {
            ProductToView productOnTableProduct = productsOnTable.FirstOrDefault(p => p.Name.Equals(productSelected.Name));
            productOnTableProduct.Quantity += 1;
        }


        private void Button_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            List<ProductToView> productsRegistered = ProductLogic.GetProductByIdOrder(orderToCreate.idOrder);
            if (productsInOrder.Count == 0)
            {
                MessageBox.Show("El pedido se encuentra vacio, si desea cancelarlo, hagalo desde lista de pedidos", "Pedido vacio", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else if(!ValidateChangeOrder(productsRegistered, productsInOrder))
            {
                string dataTotal = Label_Total.Content.ToString();
                string cleanTotal = dataTotal.Replace("$", "");
                orderToCreate.total = Double.Parse(cleanTotal);
                MessageBoxResult response = MessageBox.Show("¿Esta seguro de guardar los cambios?", "Guardar cambios", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (response == MessageBoxResult.Yes)
                {
                    if (OrderLogic.UpdateProductsInOrder(productsInOrder, orderToCreate) == 200)
                    {
                        MessageBox.Show("Los cambios han sido guardados", "Cambios guardados", MessageBoxButton.OK, MessageBoxImage.Information);
                        Orders orders = new Orders();
                        orders.Show();
                        this.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("El pedido no ha sido modificado", "Pedido sin modificar", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

        private bool ValidateChangeOrder(List<ProductToView> listDataBase, List<ProductToView> listOrder)
        {
            bool resultValidation = true;

            if (listDataBase.Count == listOrder.Count)
            {
                foreach(ProductToView productToView in listDataBase)
                {
                    ProductToView productInOrder = listOrder.FirstOrDefault(p => p.Name == productToView.Name);
                    if (productInOrder != null)
                    {
                        if(productToView.Quantity != productInOrder.Quantity)
                        {
                            resultValidation = false;
                            break;
                        }
                    }
                    else
                    {
                        resultValidation = false;
                        break;
                    }
                }
            }
            else
            {
                resultValidation = false;
            }

            return resultValidation;
        }
    }
}
