using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Logic;
using Microsoft.Win32;
using Model;
using System.IO;

namespace View
{
	public partial class AddProduct : Window
	{

		public AddProduct()
		{
			InitializeComponent();
		}

		private void AddProductButton_Click(object sender, RoutedEventArgs e)
		{
			Product newProduct = new Product()
			{
				Name = ProductNameTextBox.Text,
				Description = DescriptionTextBox.Text,
                ProductCode = ProductCodeTextBox.Text,
                Price = Double.Parse(PriceTextBox.Text),
				Preparation = PreparationComboBox.SelectedItem == "Coocked" && PreparationComboBox.SelectedItem != null,
				Active = ActiveComboBox.SelectedItem == "Active" && ActiveComboBox.SelectedItem != null,
				IdRecipe = 1,
				Picture = GetPictureBytes(),
				Restrictions = RestrictionsTextBox.Text,
			};

			if (ProductLogic.AddNewProduct(newProduct) == 200)
			{
				MessageBox.Show("Product added successfully!", "", MessageBoxButton.OK, MessageBoxImage.Information);
				ClearInputFields();
			}
			else
			{
				MessageBox.Show("Error adding product!");
			}
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
			ProductCodeTextBox.Clear();
			DescriptionTextBox.Clear();
			PriceTextBox.Clear();
			PreparationComboBox.SelectedIndex = -1;
			RestrictionsTextBox.Clear();
			ActiveComboBox.SelectedIndex = -1;
			ProductImage.Source = null;
		}

		private void CancelAddProductButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("¿Está seguro de cancelar el registro del nuevo producto?", "", MessageBoxButton.OK, MessageBoxImage.Information);
			Close();
		}
	}
}
