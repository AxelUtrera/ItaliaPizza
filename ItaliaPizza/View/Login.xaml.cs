using Logic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            int statusOK = 200;
            Model.Worker worker = UserLogic.GetWorkerByUsername(TextBox_Username.Text);

            int resultAutenticationUser = UserLogic.AutenticateUser(TextBox_Username.Text, PasswordBox_PasswordUser.Password);

            if (resultAutenticationUser == statusOK && UserLogic.GetUserById(worker.IdUser).IsActive)
            {
                MainMenu.workerLogged = worker;
                MainMenu mainMenu = new MainMenu();
         
                if (worker.Role == "Administrador")
                {
                    mainMenu.ImageOrders.IsEnabled = false;
                    mainMenu.ImageOrders.Opacity = .5;
                    mainMenu.ImageCashBox.IsEnabled = false;
                    mainMenu.ImageCashBox.Opacity = .5;
                }


                if (worker.Role == "Cocinero")
                {
                    mainMenu.ImageCashBox.IsEnabled = false;
                    mainMenu.ImageCashBox.Opacity = .5;
                    mainMenu.ImageSuplier.IsEnabled = false;
                    mainMenu.ImageSuplier.Opacity = .5;
                    mainMenu.ImageUsers.IsEnabled = false;
                    mainMenu.ImageUsers.Opacity = .5;
                    mainMenu.ImageProducts.IsEnabled = false;
                    mainMenu.ImageProducts.Opacity = .5;
                }


                if(worker.Role == "Mesero")
                {
                    mainMenu.ImageProducts.IsEnabled = false;
                    mainMenu.ImageProducts.Opacity = .5;
                    mainMenu.ImageCashBox.IsEnabled = false;
                    mainMenu.ImageCashBox.Opacity = .5;
                    mainMenu.ImageSuplier.IsEnabled = false;
                    mainMenu.ImageSuplier.Opacity = .5;
                    mainMenu.ImageUsers.IsEnabled = false;
                    mainMenu.ImageUsers.Opacity = .5;
                    mainMenu.ImageKitchen.IsEnabled = false;
                    mainMenu.ImageKitchen.Opacity = .5;
                }


                if(worker.Role == "Cajero")
                {
                    mainMenu.ImageProducts.IsEnabled = false;
                    mainMenu.ImageProducts.Opacity = .5;
                    mainMenu.ImageSuplier.IsEnabled = false;
                    mainMenu.ImageSuplier.Opacity = .5;
                    mainMenu.ImageUsers.IsEnabled = false;
                    mainMenu.ImageUsers.Opacity = .5;
                    mainMenu.ImageKitchen.IsEnabled = false;
                    mainMenu.ImageKitchen.Opacity = .5;
                }
                mainMenu.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("El usuario o contraseña no son validos, intentelo de nuevo");
            }
        }

        

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult response = MessageBox.Show( "¿Esta seguro de cerrar el sistema?", "Cerrar sistema", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (response == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }
    }
}
