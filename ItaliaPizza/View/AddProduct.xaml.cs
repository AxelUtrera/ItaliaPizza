using System;
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
			string name = ProductNameTextBox.Text;
			string productName = ProductNameTextBox.Text;
			string description = DescriptionTextBox.Text;
			string productCode = ProductCodeLabel.Content.ToString();
			byte[] picture = GetPictureBytes();

			if (!double.TryParse(PriceTextBox.Text, out double price))
			{
				MessageBox.Show("El campo Precio debe contener sólo números positivos.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}

			if (!(price < 0))
			{
				string restrictions = RestrictionsTextBox.Text;
				string preparation2 = PreparationComboBox.SelectedItem?.ToString();
				string active2 = ActiveComboBox.SelectedItem?.ToString();

				if (preparation2 == null || active2 == null)
				{
					MessageBox.Show("Debe seleccionar una opción para los campos Preparación y Estado.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				bool active;
				bool preparation;
				int idRecipe;

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
					idRecipe = new Random().Next(2, 100);
				}
				else
				{
					preparation = false;
					idRecipe = 1;
				}

				Product newProduct = new Product(name, description, productCode, picture, price, preparation, productName, restrictions, idRecipe, active);

				if (string.IsNullOrEmpty(ProductNameTextBox.Text) || string.IsNullOrEmpty(DescriptionTextBox.Text) || string.IsNullOrEmpty(PriceTextBox.Text) || PreparationComboBox.SelectedItem == null || ActiveComboBox.SelectedItem == null)
				{
					MessageBox.Show("Debe completar todos los campos para agregar un nuevo producto.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}

				if (ProductLogic.AddNewProduct(newProduct) == 200 && !(price < 0))
				{
					MessageBox.Show("Producto agregado correctamente!", "", MessageBoxButton.OK, MessageBoxImage.Information);
					ClearInputFields();
					Close();
				}
				else
				{
					MessageBox.Show("Error al agregar producto!");
				}
			}
			else
			{
				Console.WriteLine("Invalid Price.");
				MessageBox.Show("Precio inválido.", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
			ProductCodeLabel.Content = "";
			DescriptionTextBox.Clear();
			PriceTextBox.Clear();
			PreparationComboBox.SelectedIndex = -1;
			RestrictionsTextBox.Clear();
			ActiveComboBox.SelectedIndex = -1;
			ProductImage.Source = null;
		}

		private void CancelAddProductButton_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("¿Está seguro de cancelar el registro del nuevo producto?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

			if (result == MessageBoxResult.Yes)
			{
				Close();
			}
			else
			{

			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
