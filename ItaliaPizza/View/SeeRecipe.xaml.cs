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
    public partial class SeeRecipe : Window
    {
        List<Recipe> recipesList;
        List<Ingredient> selectedIngredients;
        public SeeRecipe()
        {
            InitializeComponent();
            List<Recipe> recipesList = new List<Recipe>();
            selectedIngredients = new List<Ingredient>();
            SetRecipeToComboBox();
        }

        private void ComboBoxTituloSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ = new Recipe();
            Recipe recipe = ComboBox_Recipes.SelectedItem as Recipe;
            selectedIngredients = Logic.IngredientLogic.GetRecipeIngredients(recipe.IdRecipe);
            ListBox_SelectedIngredients.ItemsSource = selectedIngredients;
            _ = new TextRange(RichTextBox_Description.Document.ContentStart, RichTextBox_Description.Document.ContentEnd)
            {
                Text = recipe.DescriptionRecipe
            };
        }

        public void SetRecipeToComboBox()
        {
            recipesList = Logic.RecipeLogic.GetRecipes();
            ComboBox_Recipes.ItemsSource = recipesList;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            KitchenMenu kitchenMenuWindow = new KitchenMenu();
            Close();
            kitchenMenuWindow.Show();
        }
    }
}