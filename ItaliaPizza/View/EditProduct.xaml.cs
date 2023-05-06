using Logic;
using Microsoft.Win32;
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
    /// Lógica de interacción para EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {

        public static ProductToView productToView { get; set; }

        public EditProduct()
        {
            InitializeComponent();
            SetDataToWindow();
            ComboBox_Recipe.ItemsSource = GetRecipeNames();
        }

        private List<String> GetRecipeNames()
        {
            List<String> recipeNames = new List<string>();
            List<Recipe> dataBaseRecipes = ProductLogic.GetRecipesFromDatabase();
            foreach(Recipe recipe in dataBaseRecipes)
            {
                recipeNames.Add(recipe.NameRecipe);
            }

            return recipeNames;
        }


        public void SetDataToWindow()
        {
            if (productToView != null)
            {
                Textbox_Name.Text = productToView.Name;
                Textbox_ProductCode.Text = productToView.ProductCode;
                ComboBox_Preparation.SelectedIndex = productToView.Preparation ? 0 : 1;
                ComboBox_State.SelectedIndex = productToView.Active.Equals("Si") ? 0 : 1;
                Textbox_Price.Text = productToView.Price.Replace("$", "");
                Textbox_Description.Text = productToView.Description;
                Textbox_Restrictions.Text = productToView.Restrictions;
                Image_ProductImage.Source = productToView.Image;
                Textbox_Quantity.Text = productToView.Quantity.ToString();
                if(productToView.Preparation)
                {
                    ComboBox_Recipe.SelectedItem = RecipeLogic.getNameRecipeById(productToView.IdRecipe);
                    Label_Recipe.Visibility = Visibility.Visible;
                    ComboBox_Recipe.Visibility = Visibility.Visible;
                }
            }
        }


        public bool ValidateData()
        {
            bool isValid = true;
            Regex nameFormat = new Regex("^[A-Za-z0-9 ]+$");
            Regex onlyNumbersFormat = new Regex("^([1-9]\\d*|0)(\\.\\d+)?$");
            Regex onlyIntegers = new Regex("^[1-9][0-9]*$");

            if (Textbox_Quantity.Text.Equals(string.Empty) || !(onlyIntegers.IsMatch(Textbox_Quantity.Text)))
            {
                Textbox_Quantity.BorderThickness = new Thickness(2);
                Label_Quantity_Error.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (Textbox_Name.Text.Equals(string.Empty) || !(nameFormat.IsMatch(Textbox_Name.Text)))
            {
                Textbox_Name.BorderThickness = new Thickness(2);
                Label_Name_Error.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (ComboBox_Preparation.SelectedItem == null)
            {
                Label_Preparation_Error.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (ComboBox_State.SelectedItem == null)
            {
                Label_State_Error.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (Textbox_Price.Text.Equals(string.Empty) || !(onlyNumbersFormat.IsMatch(Textbox_Price.Text)))
            {
                Textbox_Price.BorderThickness = new Thickness(2);
                Label_Price_Error.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (Textbox_Description.Text.Equals(string.Empty) || Textbox_Description.Text.Length >=  150)
            {
                Textbox_Description.BorderThickness = new Thickness(2);
                Label_Description_Error.Visibility = Visibility.Visible;
                isValid = false;
            }

            if (Textbox_Restrictions.Text.Equals(string.Empty) || Textbox_Restrictions.Text.Length >= 150)
            {
                Textbox_Restrictions.BorderThickness = new Thickness(2);
                Label_Restrictions_Error.Visibility = Visibility.Visible;
                isValid = false;
            }

            return isValid;
        }


        private void ResetFields()
        {
            Textbox_Name.BorderThickness = new Thickness(0);
            Label_Name_Error.Visibility = Visibility.Hidden;
            Textbox_Price.BorderThickness = new Thickness(0);
            Label_Price_Error.Visibility = Visibility.Hidden;
            Textbox_Restrictions.BorderThickness = new Thickness(0);
            Label_Restrictions_Error.Visibility = Visibility.Hidden;
            Textbox_Name.BorderThickness = new Thickness(0);
            Label_Name_Error.Visibility = Visibility.Hidden;
            Textbox_Quantity.BorderThickness = new Thickness(0);
            Label_Quantity_Error.Visibility = Visibility.Hidden;

        }


        private ProductToView GetProductEdited()
        {
            ProductToView productToEdit = new ProductToView() {
                Name = Textbox_Name.Text,
                ProductCode = Textbox_ProductCode.Text,
                Price = Textbox_Price.Text,
                Description = Textbox_Description.Text,
                Restrictions = Textbox_Restrictions.Text,
                Active = ComboBox_State.SelectedIndex == 0 ? "Si" : "No",
                IdRecipe = ComboBox_Recipe.SelectedIndex != -1 ? RecipeLogic.GetIdRecipe(ComboBox_Recipe.Text) : 1,
                Image = ImageLogic.ConvertToBitMapImage(Image_ProductImage.Source),
                Preparation = ComboBox_Preparation.SelectedIndex == 0 ? true : false,
                Quantity = Double.Parse(Textbox_Quantity.Text)
            };

            return productToEdit;
        }


        private void Button_SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp",
                Title = "Seleccione la imagen"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                BitmapImage productPicture = new BitmapImage(new Uri(imagePath));
                Image_ProductImage.Source = productPicture;
            }
        }


        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Products productsView = new Products();
            productsView.Show();
            this.Close();
        }


        private void Button_SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
            ProductLogic productLogic = new ProductLogic();
            if (ValidateData())
            {
                if (productLogic.ModifyExistentProduct(GetProductEdited()) == 200)
                {
                    MessageBox.Show("Los cambios has sido guardados", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    Products products = new Products();
                    products.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hubo un error al guardar los cambios intentelo mas tarde", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ComboBoxPreparation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ComboBoxItem selectedItem = (ComboBoxItem)ComboBox_Preparation.SelectedItem;
            string selectedPreparation = selectedItem.Content.ToString();

            if (selectedPreparation == "Si")
            {
                Label_Recipe.Visibility = Visibility.Visible;
                ComboBox_Recipe.Visibility = Visibility.Visible;
            }
            else
            {
                Label_Recipe.Visibility = Visibility.Collapsed;
                ComboBox_Recipe.Visibility = Visibility.Collapsed;
            }
        }
    }
}
