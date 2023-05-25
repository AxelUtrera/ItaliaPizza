using Logic;
using Model;
using Syncfusion.Windows.Forms.Tools.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Lógica de interacción para SupplierOrderRegister.xaml
    /// </summary>
    public partial class SupplierOrderRegister : Window
    {

        List<Product> selectedProducts = new List<Product>();
        List<Product> avaibleProducts = new List<Product>();
        List<Ingredient> avaibleIngredients = new List<Ingredient>();
        List<Ingredient> selectedIngredients = new List<Ingredient>();

        public SupplierOrderRegister()
        {
            InitializeComponent();
            SetSuppliersComboBox();
        }

        private void SetSupplierData(object sender, SelectionChangedEventArgs e)
        {
            Supplier selectedSupplier = ComboBox_Suppliers.SelectedItem as Supplier;
            TextBox_SupplierType.Text = selectedSupplier.SupplierType.ToString();
            TextBox_SupplierRFC.Text = selectedSupplier.Rfc.ToString();
            TextBox_OrderNumber.Text = SupplyOrderLogic.ObtainOrderNumber().ToString();
            DatePicker_DateOrder.SelectedDate = DateTime.Now;
            TextBox_SupplierEmail.Text = selectedSupplier.Email;
            TextBox_SupplierPhoneNumber.Text = selectedSupplier.PhoneNumber;

            SetProductsInfo();
        }


        private void Button_CancelRegisterSupplierOrder_Click(object sender, RoutedEventArgs e)
        {
            var optionSelected = MessageBox.Show("¿Desea cancelar el registro de pedido a proveedor?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(optionSelected == MessageBoxResult.Yes)
            {
                SuppliersOrderView suppliersOrderView = new SuppliersOrderView();
                suppliersOrderView.Show();
                Close();
            }
        }


        private void Button_RegisterSupplierOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Supplier selectedSupplier = ComboBox_Suppliers.SelectedItem as Supplier;
                SupplierOrder newOrder = new SupplierOrder
                {
                    IdSupplier = selectedSupplier.IdSupplier,
                    OrderNumber = TextBox_OrderNumber.Text,
                    OrderDate = DatePicker_DateOrder.SelectedDate.Value.Date,
                    OrderType = TextBox_SupplierType.Text
                };

                if(TextBox_SupplierType.Text.Equals("Productos Finales"))
                {
                    Dictionary<string, int> orderProducts = new Dictionary<string, int>();

                    if (ValidateItemsQuantity("Productos"))
                    {
                        foreach (var item in ListBox_SelectedProducts.Items)
                        {
                            ListBoxItem listBoxItem = (ListBoxItem)(ListBox_SelectedProducts.ItemContainerGenerator.ContainerFromItem(item));

                            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
                            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                            TextBox myTextBox = (TextBox)myDataTemplate.FindName("TextBox_TotalOrder", myContentPresenter);

                            Product product = listBoxItem.DataContext as Product;
                            int quantity = Int32.Parse(myTextBox.Text);

                            orderProducts.Add(product.ProductCode, quantity);
                        }

                        if (SupplyOrderLogic.RegisterSupplierProductsOrder(newOrder, orderProducts) == 200)
                        {
                            MessageBox.Show("Pedido a proveedor registrado con exito", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            CloseWindow();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese cantidades validas", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                } 
                else if (TextBox_SupplierType.Text.Equals("Ingredientes"))
                {
                    Dictionary<int, int> orderIngredients = new Dictionary<int, int>();

                    if (ValidateItemsQuantity("Ingredientes"))
                    {
                        foreach (var item in ListBox_SelectedIngredients.Items)
                        {
                            ListBoxItem listBoxItem = (ListBoxItem)(ListBox_SelectedIngredients.ItemContainerGenerator.ContainerFromItem(item));

                            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
                            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                            TextBox myTextBox = (TextBox)myDataTemplate.FindName("TextBox_TotalOrder", myContentPresenter);

                            Ingredient ingredient = listBoxItem.Content as Ingredient;
                            int quantity = Int32.Parse(myTextBox.Text);

                            orderIngredients.Add(ingredient.IdIngredient, quantity);

                            if (SupplyOrderLogic.RegisterSupplierIngredientOrder(newOrder, orderIngredients) == 200)
                            {
                                MessageBox.Show("Pedido a proveedor registrado con exito", "", MessageBoxButton.OK, MessageBoxImage.Information);
                                CloseWindow();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ingrese cantidades validas", "", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        private bool ValidateItemsQuantity(string orderType)
        {
            bool isValid = true;

            if (orderType.Equals("Productos"))
            {
                foreach (var item in ListBox_SelectedProducts.Items)
                {
                    ListBoxItem listBoxItem = (ListBoxItem)(ListBox_SelectedProducts.ItemContainerGenerator.ContainerFromItem(item));

                    ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
                    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                    TextBox myTextBox = (TextBox)myDataTemplate.FindName("TextBox_TotalOrder", myContentPresenter);

                    if (myTextBox.Text.Equals("0") || myTextBox.Text.Equals(""))
                    {
                        isValid = false;
                    }
                }
            }
            else if (orderType.Equals("Ingredientes"))
            {
                foreach (var item in ListBox_SelectedIngredients.Items)
                {
                    ListBoxItem listBoxItem = (ListBoxItem)(ListBox_SelectedIngredients.ItemContainerGenerator.ContainerFromItem(item));

                    ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
                    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                    TextBox myTextBox = (TextBox)myDataTemplate.FindName("TextBox_TotalOrder", myContentPresenter);
                    
                    if(myTextBox.Text.Equals("0") || myTextBox.Text.Equals(""))
                    {
                        isValid = false;
                    }
                }
            }

            return isValid;
        }


        private void SetSuppliersComboBox()
        {
            List<Supplier> activeSuppliers = SupplierLogic.RecoverActiveSuppliers();
            ComboBox_Suppliers.ItemsSource = activeSuppliers;
            ComboBox_Suppliers.DisplayMemberPath = "SupplierName";
        }


        private void SetProductsInfo()
        {
            if (TextBox_SupplierType.Text.Equals("Productos Finales"))
            {
                Border_Products.Visibility = Visibility.Visible;
                Border_SelectedProducts.Visibility = Visibility.Visible;
                Border_Ingredients.Visibility = Visibility.Hidden;
                Border_SelectedIngredients.Visibility = Visibility.Hidden;

                avaibleProducts = SupplyOrderLogic.RecoverProducts();
                ListBox_AvaibleProducts.ItemsSource = avaibleProducts;
            }

            if (TextBox_SupplierType.Text.Equals("Ingredientes"))
            {
                Border_Products.Visibility = Visibility.Hidden;
                Border_SelectedProducts.Visibility = Visibility.Hidden;
                Border_Ingredients.Visibility = Visibility.Visible;
                Border_SelectedIngredients.Visibility = Visibility.Visible;

                avaibleIngredients = SupplyOrderLogic.RecoverIngredients();
                ListBox_AvaibleIngredients.ItemsSource = avaibleIngredients;
            }

        }


        private void Button_SelectProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = GetProductInfo(sender);

            selectedProducts.Add(product);
            avaibleProducts.Remove(product);

            ListBox_SelectedProducts.ItemsSource = null;
            ListBox_SelectedProducts.ItemsSource = selectedProducts;

            ListBox_AvaibleProducts.ItemsSource = null;
            ListBox_AvaibleProducts.ItemsSource = avaibleProducts;
            
        }


        private Product GetProductInfo(object sender)
        {
            Button buttonSelectProduct = (Button)sender;
            StackPanel parent = (StackPanel)buttonSelectProduct.Parent;
            Product ingredientRequest = (Product)parent.DataContext;
            return ingredientRequest;
        }


        private void Button_SelectIngredient_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = GetIngredientInfo(sender);

            selectedIngredients.Add(ingredient);
            avaibleIngredients.Remove(ingredient);

            ListBox_SelectedIngredients.ItemsSource = null;
            ListBox_SelectedIngredients.ItemsSource = selectedIngredients;

            ListBox_AvaibleIngredients.ItemsSource = null;
            ListBox_AvaibleIngredients.ItemsSource = avaibleIngredients;
        }


        private Ingredient GetIngredientInfo(object sender)
        {
            Button buttonSelectIngredient = (Button)sender;
            StackPanel parent = (StackPanel)buttonSelectIngredient.Parent;
            Ingredient ingredientRequest = (Ingredient)parent.DataContext;
            return ingredientRequest;
        }


        private void Button_RemoveSelectedIngredient_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = GetIngredientInfo(sender);

            avaibleIngredients.Add(ingredient);
            selectedIngredients.Remove(ingredient);

            ListBox_SelectedIngredients.ItemsSource = null;
            ListBox_SelectedIngredients.ItemsSource = selectedIngredients;

            ListBox_AvaibleIngredients.ItemsSource = null;
            ListBox_AvaibleIngredients.ItemsSource = avaibleIngredients;
        }


        private void Button_RemoveSelectedProduct_Click(object sender, RoutedEventArgs e)
        {
            Product product = GetProductInfo(sender);

            avaibleProducts.Add(product);
            selectedProducts.Remove(product);

            ListBox_SelectedProducts.ItemsSource = null;
            ListBox_SelectedProducts.ItemsSource = selectedProducts;

            ListBox_AvaibleProducts.ItemsSource = null;
            ListBox_AvaibleProducts.ItemsSource = avaibleProducts;
        }


        private bool ValidateFields()
        {
            bool isValid = true;

            if(ComboBox_Suppliers.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un proveedor", "", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if(DatePicker_DateOrder.SelectedDate == null)
            {
                MessageBox.Show("Seleccione una fecha valida", "", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if(TextBox_SupplierType.Text.Equals("Productos Finales") && ListBox_SelectedProducts.Items.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un producto", "", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            if (TextBox_SupplierType.Text.Equals("Ingredientes") && ListBox_SelectedIngredients.Items.Count == 0)
            {
                MessageBox.Show("Seleccione al menos un ingrediente", "", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }

            return isValid;
        }


        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }


        private void CloseWindow()
        {
            SuppliersOrderView suppliersOrderView = new SuppliersOrderView();
            suppliersOrderView.Show();
            Close();
        }


        private void TextBox_ValidationNumber(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
