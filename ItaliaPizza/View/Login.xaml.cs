using Logic;
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
            if(UserLogic.AutenticateUser(TextBox_Username.Text, PasswordBox_PasswordUser.Password) == statusOK)
            {
                Model.Worker worker = UserLogic.GetWorkerByUsername(TextBox_Username.Text); 
                if (worker.Role == "Administrador" && UserLogic.GetUserById(worker.IdUser).IsActive)
                {
                    MainMenu mainMenu = new MainMenu();
                    mainMenu.Show();
                }
                else
                {
                    MessageBox.Show("El usuario o contraseña no son validos, intentelo de nuevo");
                }
                //Agregar codigo de tipo de usuario no admin
            }
            else
            {
                MessageBox.Show("El usuario o contraseña no son validos, intentelo de nuevo");
            }
        }

        

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
