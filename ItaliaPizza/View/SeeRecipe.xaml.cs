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
    /// Lógica de interacción para SeeRecipe.xaml
    /// </summary>
    public partial class SeeRecipe : Window
    {
        List<Recipe> recipes;
        List<Ingredient> selectedIngredients;
        public SeeRecipe()
        {
            InitializeComponent();
            recipes = new List<Recipe>();
            selectedIngredients = new List<Ingredient>();
            setRecipeToComboBox();
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
        public void setRecipeToComboBox()
        {
            recipes = Logic.RecipeLogic.GetRecipes();
            ComboBox_Recipes.ItemsSource = recipes;
        }
    }
}
