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
    /// Lógica de interacción para EditIngredient.xaml
    /// </summary>
    public partial class EditIngredient : Window
    {
        private List<Ingredient> ingredients;
        private List<UnitOfMeasurement> unitOfMeasurements;

        public EditIngredient()
        {
            InitializeComponent();
            ShowIngredients();
            ingredients = new List<Ingredient>();
            unitOfMeasurements = new List<UnitOfMeasurement>();
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
        public void SetUnitsOfMeasurenetToComboBox()
        {

            unitOfMeasurements = Logic.UnitOfMeasurementLogic.GetAllUnitsOfMeasurement();

            ComboBox_UnitOfMeasurement.ItemsSource = unitOfMeasurements;
        }

        private void ListBox_Ingredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox_Ingredients.SelectedItem != null)
            {
                _ = new Ingredient();
                Ingredient ingredientSelected = ListBox_Ingredients.SelectedItem as Ingredient;

                TextBox_Name.Text = ingredientSelected.IngredientName;
                TextBox_ID.Text = ingredientSelected.IdIngredient.ToString();
                UpDown_Quantity.Value = ingredientSelected.Quantity;
                UpDown_MinimumQuantity.Value = ingredientSelected.WarningTreshold;
                ComboBox_UnitOfMeasurement.SelectedIndex = ingredientSelected.IdMeasurement - 1;
            }

        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_ID.Text != "")
            {
                Ingredient ingredient = new Ingredient
                {
                    IdMeasurement = UnitOfMeasurementLogic.GetIdUnitOfMeasurement(ComboBox_UnitOfMeasurement.SelectedItem.ToString()),
                    Quantity = (double)UpDown_Quantity.Value,
                    IngredientName = TextBox_Name.Text,
                    WarningTreshold = (int)UpDown_MinimumQuantity.Value,
                    IdIngredient = Int32.Parse(TextBox_ID.Text)
                };
                var response = MessageBox.Show("¿Estas seguro de modificar el ingrediente seleccionado?", "Editar Ingrediente", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (response == MessageBoxResult.Yes)
                {
                    if (UpdateIngredient(ingredient))
                    {
                        MessageBox.Show("¡Ingrediente actualizado con exito!", "Actualizacion Exitosa", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                        ResetComponents();
                    }
                    else
                    {
                        MessageBox.Show("El ingrediente no pudo ser actualizado, intentelo mas tarde", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, Selecciona un ingrediente", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }
        public bool UpdateIngredient(Ingredient ingredient)
        {
            bool result = false;
            if (IngredientLogic.DeleteIngredient(ingredient) && IngredientLogic.RegistIngredient(ingredient))
            {
                result = true;
            }
            return result;
        }

        private void ComboBox_UnitOfMeasurement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ResetComponents()
        {
            TextBox_ID.Text = "";
            TextBox_Name.Text = "";
            UpDown_MinimumQuantity.Value = 1;
            UpDown_Quantity.Value = 1;
            ingredients = IngredientLogic.GetIngredients();
            if (ingredients.Count > 0)
            {
                ListBox_Ingredients.ItemsSource = ingredients;
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            Ingredient ingredient = ListBox_Ingredients.SelectedItem as Ingredient;
            var response = MessageBox.Show("¿Estas segudo de eliminar el ingrediente: " + ingredient.IngredientName + "?", "Eliminar ingrediente", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (response == MessageBoxResult.Yes)
            {
                if (IngredientLogic.DeleteIngredient(ingredient))
                {
                    MessageBox.Show("El ingrediente seleccionado ha sido exitosamente eliminado", "Eliminar ingrediente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    ShowIngredients();
                    ResetComponents();
                }
            }
        }

        private void Image_ButtonExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RecordIngredient recordIngredientWindow = new RecordIngredient();
            Close();
            recordIngredientWindow.ShowDialog();
        }
    }
}