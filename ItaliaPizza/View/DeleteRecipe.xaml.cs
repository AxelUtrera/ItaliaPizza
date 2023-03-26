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
using Model;

namespace View
{
    /// <summary>
    /// Lógica de interacción para DeleteRecipe.xaml
    /// </summary>
    public partial class DeleteRecipe : Window
    {
        List<Recipe> recipes;
        List<Ingredient> selectedIngredients;
        public DeleteRecipe()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
            selectedIngredients = new List<Ingredient>();
            SetRecipeToComboBox();

        }
        public void SetRecipeToComboBox()
        {
            recipes = Logic.RecipeLogic.GetRecipes();
            ComboBox_Recipes.ItemsSource = recipes;
        }

        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("¿Esta seguro de eliminar la receta seleccionada?", "Eliminar Receta", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                _ = new Recipe();
                Recipe recipe = ComboBox_Recipes.SelectedItem as Recipe;
                if (Logic.RecipeLogic.DeleteRecipe(recipe.IdRecipe))
                {
                    MessageBox.Show("Receta Eliminada");
                    SetRecipeToComboBox();
                    ListBox_SelectedIngredients.ItemsSource = null;
                    TextRange textRange = new TextRange(RichTextBox_Description.Document.ContentStart, RichTextBox_Description.Document.ContentEnd);
                    textRange.Text = "Selecciona una receta";
                }
                else
                {
                    MessageBox.Show("La receta no pudo ser Eliminada");
                }
            }
        }

        private void ComboBoxTituloSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_Recipes.SelectedItem != null)
            {
                _ = new Recipe();
                Recipe recipe = ComboBox_Recipes.SelectedItem as Recipe;
                selectedIngredients = Logic.IngredientLogic.GetRecipeIngredients(recipe.IdRecipe);
                ListBox_SelectedIngredients.ItemsSource = selectedIngredients;
                TextRange textRange = new TextRange(RichTextBox_Description.Document.ContentStart, RichTextBox_Description.Document.ContentEnd);
                textRange.Text = recipe.DescriptionRecipe;
            }
        }
    }
}
