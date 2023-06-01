using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Lógica de interacción para RecordIngredient.xaml
    /// </summary>
    public partial class RecordIngredient : Window
    {
        List<Ingredient> ingredients;
        private List<UnitOfMeasurement> unitOfMeasurements;

        public RecordIngredient()
        {
            ingredients = new List<Ingredient>();
            InitializeComponent();
            ShowIngredients();
            SetUnitsOfMeasurenetToComboBox();
        }

        private void ShowIngredients()
        {
            ingredients = IngredientLogic.GetIngredients();
            if (ingredients.Count > 0)
            {
                ListBox_Ingredients.ItemsSource = ingredients;
            }
            else
            {
                MessageBox.Show("No existen ingredientes registrados", "Ingredientes", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private bool ValidateText()
        {

            bool result = false;
            Regex regex = new Regex("^[a-zA-ZñÑ ]+$");
            if (!string.IsNullOrWhiteSpace(TextBox_Name.Text) && regex.IsMatch(TextBox_Name.Text))
            {
                result = true;
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un nombre de ingrediente valido", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;

        }

        public void SetUnitsOfMeasurenetToComboBox()
        {
            unitOfMeasurements = UnitOfMeasurementLogic.GetAllUnitsOfMeasurement();
            ComboBox_UnitOfMeasurement.ItemsSource = unitOfMeasurements;
        }
        public bool AllreadyExist()
        {
            bool result = false;
            int status = IngredientLogic.AllreadyExist(TextBox_Name.Text.ToUpper());
            if (status == 0)
            {
                var response = MessageBox.Show("Ya existe un ingrediente con este nombre registrado \n ¿Deseas activarlo?", "Ingrediente Desactivado", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (response == MessageBoxResult.Yes)
                {
                    if (IngredientLogic.ActivateIngredient(TextBox_Name.Text.ToUpper()))
                    {
                        MessageBox.Show("El ingrediente ha sido activado con exito", "Ingrediente Activado", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        ViewBox_SeeIngredients.Visibility = Visibility.Visible;
                        ViewBox_NewRegister.Visibility = Visibility.Hidden;
                        ShowIngredients();
                        ResetComponets();
                    }
                    else
                    {
                        MessageBox.Show("El ingrediente no ha podido ser activado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                result = true;
            }
            else if (status==1)
            {
                result = true;
                MessageBox.Show("Ya existe un ingrediente con este nombre registrado \n Por favor, ingresa otro nombre", "Ingrediente ya registrado", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return result;
        }

        private void ListBox_Ingredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_Ingredients.SelectedItem != null)
            {
                _ = new Ingredient();
                Ingredient ingredient = ListBox_Ingredients.SelectedItem as Ingredient;

                TextBox_Name_AllreadyRegistered.Text = ingredient.IngredientName;
                TextBox_MinimumQuantity_AllreadyRegistered.Text = ingredient.WarningTreshold.ToString();
                TextBox_UnitOfMeasurement_AllreadyRegistered.Text = ingredient.Measurement.ToString();
                TextBox_ExistingQuantity_AllreadyResgistered.Text = ingredient.Quantity.ToString();
            }
        }

        private void ComboBox_UnitOfMeasurement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Image_ButtonAdd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewBox_NewRegister.Visibility = Visibility.Visible;
            ViewBox_SeeIngredients.Visibility = Visibility.Hidden;
        }

        private void Image_ButtonEditIngredient_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            EditIngredient editIngredientWindow = new EditIngredient();
            Close();
            editIngredientWindow.ShowDialog();
        }

        private void Image_Delete_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ListBox_Ingredients.SelectedItem != null)
            {
                Ingredient ingredient = ListBox_Ingredients.SelectedItem as Ingredient;
                var result = MessageBox.Show("¿Estas seguro de eliminar el ingrediente seleccionado?: \n Nombre: " + ingredient.IngredientName, "Eliminar ingrediente", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    if (IngredientLogic.DeactivateIngredient(ingredient))
                    {
                        MessageBox.Show("Ingrediente eliminado con exito", "Eliminacion exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                        ShowIngredients();
                        ResetComponets();
                    }
                    else
                    {
                        MessageBox.Show("Error, el ingrediente no pudo ser eliminado \n Intentelo mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un ingrediente", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetComponets()
        {
            TextBox_Name_AllreadyRegistered.Text = string.Empty;
            TextBox_ExistingQuantity_AllreadyResgistered.Text = string.Empty;
            TextBox_MinimumQuantity_AllreadyRegistered.Text = string.Empty;
            TextBox_UnitOfMeasurement_AllreadyRegistered.Text = string.Empty;
            TextBox_Name.Text = string.Empty;
            UpDown_MinimumQuantity.Value = 0;
            UpDown_Quantity.Value= 0;
        }

        private void Image_ButtonExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            KitchenMenu kitchenMenuWindow = new KitchenMenu();
            Close();
            kitchenMenuWindow.Show();
        }

        private void Image_ButtonBack_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ViewBox_NewRegister.Visibility = Visibility.Hidden;
            ViewBox_SeeIngredients.Visibility = Visibility.Visible;
        }


        private void Button_ButtonSave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ComboBox_UnitOfMeasurement.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una unidad de medida", "Adevertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                Ingredient ingredient = new Ingredient();
                if (ValidateText() && !AllreadyExist())
                {
                    UnitOfMeasurement unitOfMeasurement = ComboBox_UnitOfMeasurement.SelectedItem as UnitOfMeasurement;
                    ingredient.IngredientName = TextBox_Name.Text.ToUpper();
                    ingredient.IdMeasurement = unitOfMeasurement.IdUnitOfMeasurement;
                    ingredient.Quantity = (double)UpDown_Quantity.Value;
                    ingredient.WarningTreshold = (int)UpDown_MinimumQuantity.Value;
                    ingredient.Measurement = ComboBox_UnitOfMeasurement.SelectedItem.ToString();

                    var result = MessageBox.Show("¿Estas seguro de registrar un nuevo ingrediente?\n nombre: " + ingredient.IngredientName + "\n Cantidad: " + ingredient.Quantity.ToString() +
                            "\n Unidad de Medida: " + ingredient.Measurement + "\n Cantidad Minima: " + ingredient.WarningTreshold, "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                    if (result == MessageBoxResult.Yes)
                    {
                        /*if ()
                        {
                            en proceso
                        }*/
                        if (IngredientLogic.RegistIngredient(ingredient))
                        {
                            MessageBox.Show("Registro Exitoso, se ha registrado un nuevo ingrediente", "Registro exitoso", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            TextBox_Name.Text = "";
                            UpDown_Quantity.Value = 1;
                            ShowIngredients();
                            ViewBox_NewRegister.Visibility = Visibility.Hidden;
                            ViewBox_SeeIngredients.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            MessageBox.Show("El registro no pudo ser realizado", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
    }
}
