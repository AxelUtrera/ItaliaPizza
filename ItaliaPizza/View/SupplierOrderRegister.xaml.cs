using Logic;
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
            DatePicker_StimatedOrderArrive.SelectedDate = DateTime.Now.AddDays(1);
            TextBox_SupplierEmail.Text = selectedSupplier.Email;

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

            }
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
    }
}
