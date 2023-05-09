using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Lógica de interacción para SupplierRegister.xaml
    /// </summary>
    public partial class SupplierRegister : Window
    {
        Regex phoneNumberFormat = new Regex("^[0-9]{10,10}$");
        Regex emailFormat = new Regex("^\\S+@\\S+\\.\\S+$");
        Regex rfcFormat = new Regex("^[A-z0-9]{13,13}$");
        Regex stringFormat = new Regex("^.{1,50}$");

        public SupplierRegister()
        {
            InitializeComponent();
            SetComboboxItems();
        }


        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            SuppliersView suppliersView = new SuppliersView();
            suppliersView.Show();
            Close();
        }


        private void Button_CancelRegisterSupplier_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("¿Desea cancelar el registro del proveedor", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (response == MessageBoxResult.Yes)
            {
                SuppliersView suppliersView = new SuppliersView();
                suppliersView.Show();
                Close();
            }
        }


        private void Button_RegisterSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Supplier supplier = new Supplier
                {
                    SupplierName = TextBox_SupplierName.Text,
                    Email = TextBox_SupplierEmail.Text,
                    PhoneNumber = TextBox_SupplierPhoneNumber.Text,
                    Rfc = TextBox_SupplierRFC.Text,
                    SupplierType = ComboBox_SupplierType.SelectedItem.ToString(),
                    SupplierAddress = TextBox_SupplierAddress.Text,
                };


                if (SupplierLogic.RegisterSupplier(supplier) == 200)
                {
                    MessageBox.Show("Proveedor registrado con exito", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    SuppliersView suppliersView = new SuppliersView();
                    suppliersView.Show();
                    Close();
                }

            }
        }


        private void SetComboboxItems()
        {
            ComboBox_SupplierType.ItemsSource = new string[]
            {
                "Productos Finales",
                "Ingredientes"
            };
        }

        private bool ValidateFields()
        {
            SetFieldsDefault();
            bool isValid = true;

            if (TextBox_SupplierName.Text.Equals(string.Empty) || !stringFormat.IsMatch(TextBox_SupplierName.Text))
            {
                TextBox_SupplierName.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_SupplierEmail.Text.Equals(string.Empty) || !emailFormat.IsMatch(TextBox_SupplierEmail.Text))
            {
                TextBox_SupplierEmail.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_SupplierRFC.Text.Equals(string.Empty) || !rfcFormat.IsMatch(TextBox_SupplierRFC.Text))
            {
                TextBox_SupplierRFC.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_SupplierPhoneNumber.Text.Equals(string.Empty) || !phoneNumberFormat.IsMatch(TextBox_SupplierPhoneNumber.Text))
            {
                TextBox_SupplierPhoneNumber.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (ComboBox_SupplierType.SelectedItem == null)
            {
                ComboBox_SupplierType.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_SupplierAddress.Text.Equals(string.Empty) || !stringFormat.IsMatch(TextBox_SupplierAddress.Text))
            {
                TextBox_SupplierAddress.BorderThickness = new Thickness(2);
                isValid = false;
            }

            return isValid;
        }


        public void SetModifySupplierData(Supplier supplierToModify)
        {
            if (supplierToModify != null)
            {
                Button_Register.IsEnabled = false;
                Button_Register.Visibility = Visibility.Hidden;

                Button_Modify.IsEnabled = true;
                Button_Modify.Visibility = Visibility.Visible;

                TextBox_SupplierName.Text = supplierToModify.SupplierName;
                TextBox_IdSupplier.Text = supplierToModify.IdSupplier.ToString();
                TextBox_SupplierEmail.Text = supplierToModify.Email;
                TextBox_SupplierRFC.Text = supplierToModify.Rfc;
                TextBox_SupplierAddress.Text = supplierToModify.SupplierAddress;
                TextBox_SupplierPhoneNumber.Text = supplierToModify.PhoneNumber;
                ComboBox_SupplierType.SelectedItem = supplierToModify.SupplierType;

            }
        }

        private void SetFieldsDefault()
        {
            TextBox_SupplierName.BorderThickness = new Thickness(0);
            TextBox_SupplierEmail.BorderThickness = new Thickness(0);
            TextBox_SupplierRFC.BorderThickness = new Thickness(0);
            TextBox_SupplierPhoneNumber.BorderThickness = new Thickness(0);
            ComboBox_SupplierType.BorderThickness = new Thickness(0);
            TextBox_SupplierAddress.BorderThickness = new Thickness(0);
        }

        private void Button_ModifySupplier_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateFields())
            {
                Supplier supplier = new Supplier
                {
                    IdSupplier = Int32.Parse(TextBox_IdSupplier.Text),
                    SupplierName = TextBox_SupplierName.Text,
                    Email = TextBox_SupplierEmail.Text,
                    PhoneNumber = TextBox_SupplierPhoneNumber.Text,
                    Rfc = TextBox_SupplierRFC.Text,
                    SupplierType = ComboBox_SupplierType.SelectedItem.ToString(),
                    SupplierAddress = TextBox_SupplierAddress.Text,
                };

                if (SupplierLogic.ModifySupplier(supplier) == 200)
                {
                    MessageBox.Show("Proveedor modificado con exito", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    SuppliersView suppliersView = new SuppliersView();
                    suppliersView.Show();
                    Close();
                }
            }
        }
    }
}