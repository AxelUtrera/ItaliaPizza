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
    /// Lógica de interacción para KitchenMenu.xaml
    /// </summary>
    public partial class KitchenMenu : Window
    {
        public static Worker workerLogged;
        public KitchenMenu()
        {
            InitializeComponent();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu();
            Close();
            mainMenu.ShowDialog();
        }

        private void Image_ButtonRecipes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            KitchenMenuRecipe kitchenMenuRecipeWindow = new KitchenMenuRecipe();
            Close();
            kitchenMenuRecipeWindow.ShowDialog();
        }

        private void Image_ButtonIngredients_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            RecordIngredient recordIngredientWindow = new RecordIngredient();
            Close();
            recordIngredientWindow.ShowDialog();
        }
    }
}
