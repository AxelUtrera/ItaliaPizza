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
        public KitchenMenu()
        {
            InitializeComponent();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Image_SeeRecipe_Click(object sender, MouseButtonEventArgs e)
        {
            SeeRecipe seeRecipeWindow = new SeeRecipe();
            Close();
            seeRecipeWindow.ShowDialog();
        }

        private void Image_DeleteRecipe_Click(object sender, MouseButtonEventArgs e)
        {
            DeleteRecipe deleteRecipeWIndow = new DeleteRecipe();
            Close();
            deleteRecipeWIndow.ShowDialog();
        }

        private void Image_EditRecipe_Click(object sender, MouseButtonEventArgs e)
        {
            EditRecipe editRecipeWindow = new EditRecipe();
            Close();
            editRecipeWindow.ShowDialog();
        }

        private void Image_RecordRecipe_Click(object sender, MouseButtonEventArgs e)
        {
            RecordRecipe recordRecipeWindow = new RecordRecipe();
            Close();
            recordRecipeWindow.ShowDialog();
        }

		private void Button_Click(object sender, RoutedEventArgs e)
		{
            ShowPendingOrders showPendingOrders = new ShowPendingOrders();
            Close();
            showPendingOrders.ShowDialog();
        }
    }
}
