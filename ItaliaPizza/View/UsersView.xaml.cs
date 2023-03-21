﻿using Logic;
using Model;
using Syncfusion.SfSkinManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            if(UsersTable.SelectedItem != null)
            {
                Console.WriteLine("Modificando Usuario");
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
                Console.WriteLine("Eliminando Usuario");
            }
            else
            {
                MessageBox.Show("Por favor, Seleccione un usuario", "", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Textbox_SearchUsers_Input(object sender, TextChangedEventArgs e)
        {
            var searchText = sender as TextBox;
            if(searchText != null)
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
    }
}