﻿using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Logic;
using Microsoft.Win32;
using Model;
using System.IO;
using System.Linq;

namespace View
{
	public partial class AddProduct : Window
	{

		public AddProduct()
		{
			InitializeComponent();
			string productCode = GenerateProductCode();
			ProductCodeLabel.Content = productCode;

			RecipeComboBox.Items.Clear();
			RecipeComboBox.ItemsSource = ProductLogic.GetRecipesFromDatabase();
			RecipeComboBox.DisplayMemberPath = "NameRecipe";
		}
		
		private string GenerateProductCode()
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			var result = new string(
				Enumerable.Repeat(chars, 5)
						  .Select(s => s[random.Next(s.Length)])
						  .ToArray());
			return result.Substring(0, 5);
		}

		private void AddProductButton_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(ProductNameTextBox?.Text) || string.IsNullOrEmpty(DescriptionTextBox?.Text) || string.IsNullOrEmpty(PriceTextBox?.Text) || string.IsNullOrEmpty(PreparationComboBox?.Text) || string.IsNullOrEmpty(ActiveComboBox?.Text) || string.IsNullOrEmpty(DecimalUpDownControl?.Text))
			{
				MessageBox.Show("Debe completar todos los campos para agregar un nuevo producto.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			string name = ProductNameTextBox.Text;
			string productName = ProductNameTextBox.Text;
			string description = DescriptionTextBox.Text;
			string productCode = ProductCodeLabel.Content.ToString();
			byte[] picture = GetPictureBytes();
			double UpDownBox = (double)DecimalUpDownControl.Value;

			if (!double.TryParse(DecimalUpDownControl.Text, out double quantity) || UpDownBox < 0)
			{
				MessageBox.Show("El campo Cantidad es requerido y debe contener solo números positivos.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!double.TryParse(PriceTextBox.Text, out double price))
			{
				MessageBox.Show("El campo Precio es requerido y debe contener solo números.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			string preparation2 = PreparationComboBox.SelectedItem.ToString();
			string active2 = ActiveComboBox.SelectedItem.ToString();

			string restrictions = RestrictionsTextBox.Text;
			bool active;
			bool preparation;
			int idRecipe = -1;

			Recipe selectedRecipe = RecipeComboBox.SelectedItem as Recipe;

			if (selectedRecipe != null)
			{
				idRecipe = selectedRecipe.IdRecipe;
			}

			if (price > 0)
			{
				if (active2 == "System.Windows.Controls.ComboBoxItem: Activo")
				{
					active = true;
				}
				else
				{
					active = false;
				}

				if (preparation2 == "System.Windows.Controls.ComboBoxItem: Sí")
				{
					preparation = true;
				}
				else
				{
					preparation = false;
				}

				Product newProduct = new Product(name, description, productCode, picture, price, preparation, productName, restrictions, idRecipe, active, quantity);

				if (ProductLogic.AddNewProduct(newProduct) == 200)
				{
					MessageBox.Show("Producto agregado correctamente!", "", MessageBoxButton.OK, MessageBoxImage.Information);
					ClearInputFields();
					AddProduct addNewProduct = new AddProduct();
					addNewProduct.ShowDialog();
					Close();
					
				}
				else
				{
					MessageBox.Show("Error al agregar producto!");
				}
			}
			else
			{
				MessageBox.Show("El precio debe ser un número positivo.", "", MessageBoxButton.OK, MessageBoxImage.Information);
			}

		}

		private void DecimalUpDownControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{

		}

		private void SelectImage_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp",
				Title = "Select a Product Image"
			};

			if (openFileDialog.ShowDialog() == true)
			{
				string imagePath = openFileDialog.FileName;
				BitmapImage productPicture = new BitmapImage(new Uri(imagePath));
				ProductImage.Source = productPicture;
			}
		}

		private byte[] GetPictureBytes()
		{
			if (ProductImage.Source != null)
			{
				BitmapSource bitmapSource = (BitmapSource)ProductImage.Source;

				JpegBitmapEncoder encoder = new JpegBitmapEncoder();
				encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

				using (MemoryStream stream = new MemoryStream())
				{
					encoder.Save(stream);
					return stream.ToArray();
				}
			}

			return null;
		}

		private void ClearInputFields()
		{
			ProductNameTextBox.Clear();
			ProductCodeLabel.Content = "";
			DescriptionTextBox.Clear();
			PriceTextBox.Clear();
			PreparationComboBox.SelectedIndex = -1;
			RestrictionsTextBox.Clear();
			ActiveComboBox.SelectedIndex = -1;
			ProductImage.Source = null;
			DecimalUpDownControl.Text = "0";
		}

		private void CancelAddProductButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("¿Está seguro de cancelar el registro del nuevo producto?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

			if (result == MessageBoxResult.Yes)
			{
				Products productW = new Products();
				productW.ShowDialog();
				Close();

			}
			else
			{
			
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Products productW = new Products();
			productW.ShowDialog();
			Close();
		}

		private void RecipeComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{

		}
	}
}
