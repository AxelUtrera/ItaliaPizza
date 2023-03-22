using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
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
			string productCode = ProductCodeTextBox.Text;
			string productName = ProductNameTextBox.Text;
			string description = DescriptionTextBox.Text;
			double price = Convert.ToDouble(PriceTextBox.Text);
			bool preparation = PreparationComboBox.SelectedItem.ToString() == "Cooked";
			string restrictions = RestrictionsTextBox.Text;
			bool active = ActiveComboBox.SelectedItem.ToString() == "Active";
			byte[] picture = GetPictureBytes();

			Product newProduct = new Product(productName, description, productCode, picture, price, preparation, productName, restrictions, 0, 0, active);

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

		private static Product GetNewProduct(string productName, string description, double price, bool preparation, string restrictions, bool active, byte[] picture)
		{
			return new Product(productName, description, picture, price, preparation, productName, restrictions, 0, 0, active);

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
