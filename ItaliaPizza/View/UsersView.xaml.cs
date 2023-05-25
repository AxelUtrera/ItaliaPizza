using Logic;
using Model;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Lógica de interacción para UsersView.xamlS
    /// </summary>
    public partial class UsersView : Window
    {

        public ObservableCollection<User> activeUsers;

        public UsersView()
        {
            InitializeComponent();
            RecoverActiveUsers();

        }

        private void RecoverActiveUsers()
        {
            List<User> users = UserLogic.RecoverActiveUsers();
            activeUsers = new ObservableCollection<User>(users);
            UsersTable.ItemsSource = activeUsers;
        }

        private void Button_ModifyUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersTable.SelectedItem != null)
            {
                var userToModify = UsersTable.SelectedItem as User;
                UserRegister userRegister = new UserRegister();
                userRegister.SetModifyUserForm(userToModify);
                userRegister.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Por favor, Seleccione un usuario", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void Button_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersTable.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Desea eliminar al usuario?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    int userId = ((User)UsersTable.SelectedItem).IdUser;
                    int statusCode = UserLogic.DeleteUser(userId);

                    if (statusCode == 200)
                    {
                        MessageBox.Show("Usuario eliminado correctamente.");
                        RecoverActiveUsers();
                    }
                    else
                    {
                        MessageBox.Show("Ha ocurrido un error al intentar eliminar al usuario, inténtelo de nuevo.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario.", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }


        }

		private void Button_Exit_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}


		private void Textbox_SearchUsers_Input(object sender, TextChangedEventArgs e)
		{
			var searchText = sender as TextBox;
			if (searchText != null)
			{
				var filteredList = activeUsers.Where(x => x.Name.Contains(searchText.Text));
				UsersTable.ItemsSource = null;
				UsersTable.ItemsSource = filteredList;
			}
			else
			{
				UsersTable.ItemsSource = activeUsers;
			}
		}

		private void Button_UserRegister_Click(object sender, RoutedEventArgs e)
		{
			UserRegister userRegister = new UserRegister();
			userRegister.Show();
			Close();
		}

	}
}
