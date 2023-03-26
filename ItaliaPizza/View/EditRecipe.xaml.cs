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
using Model;

namespace View
{
    /// <summary>
    /// Lógica de interacción para EditRecipe.xaml
    /// </summary>
    public partial class EditRecipe : Window
    {
        List<Recipe> recipes;
        List<Ingredient> selectedIngredients;
        public EditRecipe()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
            selectedIngredients = new List<Ingredient>();
            SetRecipeToComboBox();
            showIngredients();
        }
        private void showIngredients()
        {
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients = Logic.IngredientLogic.GetIngredients();
            if (ingredients.Count > 0)
            {
                ListBox_Ingredients.ItemsSource = ingredients;
            }
            else
            {
                MessageBox.Show("No existen ingredientes registrados");
            }
        }
        public void SetRecipeToComboBox()
        {
            recipes = Logic.RecipeLogic.GetRecipes();
            ComboBox_Recipes.ItemsSource = recipes;
        }

        private void ComboBoxTituloSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recipe recipe = new Recipe();
            recipe = ComboBox_Recipes.SelectedItem as Recipe;
            selectedIngredients = Logic.IngredientLogic.GetRecipeIngredients(recipe.IdRecipe);
            ListBox_SelectedIngredients.ItemsSource = selectedIngredients;
            TextRange textRange = new TextRange(RichTextBox_Description.Document.ContentStart, RichTextBox_Description.Document.ContentEnd);
            textRange.Text = recipe.DescriptionRecipe;
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Ingredients.SelectedItem != null)
            {
                int numericValue = 0;
                if (TextBox_Amount.Text != null && int.TryParse(TextBox_Amount.Text, out numericValue) && numericValue > 0)
                {
                    Ingredient ingredient = new Ingredient();
                    ingredient = ListBox_Ingredients.SelectedItem as Ingredient;

                    if (selectedIngredients.Contains(ingredient))
                    {
                        int index = selectedIngredients.IndexOf(ingredient);
                        selectedIngredients[index].Quantity += numericValue;
                    }
                    else
                    {
                        ingredient.Quantity = numericValue;
                        selectedIngredients.Add(ingredient);
                    }
                    ListBox_SelectedIngredients.ItemsSource = null;
                    ListBox_SelectedIngredients.ItemsSource = selectedIngredients;
                }
                else
                {
                    MessageBox.Show("Por favor, Ingrese una cantidad valida");
                }
            }
            else
            {
                MessageBox.Show("Por favor, Selecciona un ingrediente primero");
            }
        }

        private void Botton_Empty_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Esta seguro de vaciar la lista de ingredientes seleccionados?", "Vaciar lista", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                selectedIngredients.Clear();
                ListBox_SelectedIngredients.ItemsSource = null;
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_SelectedIngredients.SelectedItem != null)
            {
                Ingredient ingredient = new Ingredient();
                ingredient = ListBox_SelectedIngredients.SelectedItem as Ingredient;
                selectedIngredients.Remove(ingredient);
                ListBox_SelectedIngredients.ItemsSource = null;
                ListBox_SelectedIngredients.ItemsSource = selectedIngredients;
            }
            else
            {
                MessageBox.Show("Selecciona un ingrediente a eliminar por favor");
            }
        }

        private void ListBox_Ingredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox_IngredientSelected.Text = ListBox_Ingredients.SelectedItem.ToString();
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {

            TextRange textRange = new TextRange(RichTextBox_Description.Document.ContentStart,
                RichTextBox_Description.Document.ContentEnd);
            int validateResult = ValidateText();
            if (validateResult == 1 && ComboBox_Recipes.SelectedItem != null)
            {
                _ = new Recipe();
                Recipe recipe = ComboBox_Recipes.SelectedItem as Recipe;
                recipe.DescriptionRecipe = textRange.Text;
                recipe.Ingredients = selectedIngredients;
                if (Logic.RecipeLogic.EditRecipe(recipe))
                {
                    Logic.IngredientLogic.DeleteRecipeIngredients(recipe.IdRecipe);
                    if (Logic.IngredientLogic.RegistRecipeIngredients(recipe))
                    {
                        MessageBox.Show("Modificacion Exitosa");
                    }
                    else
                    {
                        MessageBox.Show("La modificacion no pudo ser realizada");
                    }
                }
                else
                {
                    MessageBox.Show("La modificacion no fue realizada");
                }
            }
            else if (validateResult == 0)
            {
                MessageBox.Show("Por favor, no ingreses caracteres extraños");
                RichTextBox_Description.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, llena todos los campos");
                RichTextBox_Description.Focus();
            }
        }
        private int ValidateText()
        {
            TextRange textRange = new TextRange(RichTextBox_Description.Document.ContentStart,
               RichTextBox_Description.Document.ContentEnd);
            int result = 0;
            if (!string.IsNullOrWhiteSpace(textRange.Text) && selectedIngredients.Count > 0)
            {
                Regex regex = new Regex(@"^(?=.?[#?!@$%^&-])");
                Match match = regex.Match(textRange.Text);
                if (!match.Success)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }
            }
            else
            {
                result = 2;
            }
            return result;
        }
    }
}

