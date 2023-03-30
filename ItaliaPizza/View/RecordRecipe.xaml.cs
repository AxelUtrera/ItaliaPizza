using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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
    public partial class RecordRecipe : Window
    {
        private readonly List<Ingredient> selectedIngredients;
        public RecordRecipe()
        {
            InitializeComponent();
            ShowIngredients();
            selectedIngredients = new List<Ingredient>();

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
        private void ShowIngredients()
        {
            _ = new List<Ingredient>();
            List<Ingredient> ingredients = IngredientLogic.GetIngredients();
            if (ingredients.Count > 0)
            {
                ListBox_Ingredients.ItemsSource = ingredients;
            }
            else
            {
                MessageBox.Show("No existen ingredientes registrados", "Ingredientes", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ListBox_Ingredients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBox_IngredientSelected.Text = ListBox_Ingredients.SelectedItem.ToString();
            TextBox_Amount.Focus();
            TextBox_Amount.Text = "";
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_Ingredients.SelectedItem != null)
            {
                if (TextBox_Amount.Text != null && int.TryParse(TextBox_Amount.Text, out int numericValue) && numericValue > 0)
                {
                    _ = new Ingredient();
                    Ingredient ingredient = ListBox_Ingredients.SelectedItem as Ingredient;
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
                    MessageBox.Show("Por favor, Ingrese una cantidad valida", "Valor invalido", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Por favor, Selecciona un ingrediente primero", "Ingredientes", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBox_SelectedIngredients.SelectedItem != null)
            {
                _ = new Ingredient();
                Ingredient ingredient = ListBox_SelectedIngredients.SelectedItem as Ingredient;
                selectedIngredients.Remove(ingredient);
                ListBox_SelectedIngredients.ItemsSource = null;
                ListBox_SelectedIngredients.ItemsSource = selectedIngredients;
            }
            else
            {
                MessageBox.Show("Selecciona un ingrediente a eliminar por favor");
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(RichTextBox_Description.Document.ContentStart,
                RichTextBox_Description.Document.ContentEnd);
            int validateResult = ValidateText();
            if (validateResult == 1)
            {

                Recipe recipe = new Recipe()
                {
                    NameRecipe = TextBox_Tittle.Text,
                    DescriptionRecipe = textRange.Text,
                    IsActive = true,
                };
                int idRecipe;
                switch (Logic.RecipeLogic.AllreadyExist(recipe.NameRecipe))
                {
                    case "exist":
                        MessageBox.Show("Ya se encuentra una receta registrada con este mismo nombre", "Receta Registrada", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    case "doesNotExist":
                        if (Logic.RecipeLogic.RegistRecipe(recipe))
                        {
                            idRecipe = Logic.RecipeLogic.GetIdRecipe(recipe.NameRecipe);
                            recipe.Ingredients = selectedIngredients;
                            recipe.IdRecipe = idRecipe;

                            if (idRecipe > 0)
                            {
                                if (IngredientLogic.RegistRecipeIngredients(recipe))
                                {
                                    MessageBox.Show("Registro Exitoso");
                                }
                                else
                                {
                                    MessageBox.Show("El registro no pudo ser realizado", "Registro", MessageBoxButton.OK, MessageBoxImage.Warning);

                                }
                            }
                            else
                            {
                                MessageBox.Show("El registro no pudo ser realizado", "Registro", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                        break;

                    case "idle":
                        idRecipe = RecipeLogic.GetIdRecipe(recipe.NameRecipe);
                        var result = MessageBox.Show("Esta receta ya se encuentra registrada pero inactiva.\n ¿Deseas reactivarla?", "Receta Inactiva", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                        if (result == MessageBoxResult.Yes)
                        {
                            Logic.RecipeLogic.ActivateRecipe(idRecipe);
                            var result2 = MessageBox.Show("¿Deseas guardar los cambios en la receta?.", "Guardar cambios", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                            if (result2 == MessageBoxResult.Yes)
                            {
                                recipe.Ingredients = selectedIngredients;
                                MessageBox.Show(recipe.IdRecipe.ToString());
                                RecipeLogic.EditRecipe(recipe);
                                IngredientLogic.DeleteRecipeIngredients(idRecipe);
                                IngredientLogic.RegistRecipeIngredients(recipe);
                            }
                        }
                        break;
                }
            }
            else if (validateResult == 0)
            {
                MessageBox.Show("Por favor, no ingreses caracteres extraños", "Caracteres invalidos", MessageBoxButton.OK, MessageBoxImage.Warning);
                RichTextBox_Description.Focus();
            }
            else
            {
                MessageBox.Show("Por favor, llena todos los campos", "Campos vacios", MessageBoxButton.OK, MessageBoxImage.Warning);
                RichTextBox_Description.Focus();
            }
        }
        private int ValidateText()
        {
            TextRange textRange = new TextRange(RichTextBox_Description.Document.ContentStart,
               RichTextBox_Description.Document.ContentEnd);
            int result = 0;

            if (!string.IsNullOrWhiteSpace(TextBox_Tittle.Text) && !string.IsNullOrWhiteSpace(textRange.Text) && selectedIngredients.FirstOrDefault() != null)
            {
                Regex regex = new Regex(@"^[a-zA-Z0-9\s!?\(\)\n]+$");

                if (regex.IsMatch(textRange.Text) || regex.IsMatch(TextBox_Tittle.Text))
                {
                    result = 1;
                }
            }
            else
            {
                result = 2;
            }
            return result;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            KitchenMenu kitchenMenuWindow = new KitchenMenu();
            Close();
            kitchenMenuWindow.Show();
        }
    }
}
