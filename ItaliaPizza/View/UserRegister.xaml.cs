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
using Model;
using Logic;
using System.Text.RegularExpressions;

namespace View
{
    /// <summary>
    /// Lógica de interacción para UserRegister.xaml
    /// </summary>
    public partial class UserRegister : Window
    {
        ResourceManager itemsResource = new ResourceManager("View.Properties.Resources", Assembly.GetExecutingAssembly());
        Regex phoneNumberFormat = new Regex("^[0-9]{10,10}$");
        Regex emailFormat = new Regex("^\\S+@\\S+\\.\\S+$");
        Regex zipCodeFormat = new Regex("^[0-9]{5}(?:-[0-9]{4})?$");
        Regex nssFormat = new Regex("^[0-9]{11,11}$");
        Regex numberFormat = new Regex("^[0-9]{1,5}$");
        Regex rfcFormat = new Regex("^[A-z0-9]{13,13}$");
        Regex usernameFormat = new Regex("^[a-zA-Z0-9]{4,16}$");
        Regex passwordFormat = new Regex("^[0-9]{6,6}$");
        Regex stringFormat = new Regex("^[ a-zA-ZñÑáéíóúÁÉÍÓÚ]{1,30}$");
        Regex streetFormat = new Regex("^.{1,50}$");
        Regex referencesFormat = new Regex("^.{1,100}$");

        public UserRegister()
        {
            InitializeComponent();
            setComboBoxItems();
        }


        private void Button_RegisterUser_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
            if (ValidateFields())
            {
                string name = TextBox_Name.Text;
                string lastname = TextBox_Lastname.Text;
                string email = TextBox_Email.Text;
                string phoneNumber = TextBox_PhoneNumber.Text;

                User newUser = new User()
                {
                    Name = name,
                    Lastname = lastname,
                    Email = email,
                    PhoneNumber = phoneNumber
                };

                string userTypeSelected = ComboBox_UserType.SelectedItem.ToString();

                if (userTypeSelected.Equals(itemsResource.GetString("UserRegister_Worker_UserType")))
                {
                    if (ValidateWorkerFields())
                    {
                        Console.WriteLine("Campos trabajador validos");
                        string nss = TextBox_NSS.Text;
                        string rfc = TextBox_RFC.Text;
                        string workerNumber = TextBox_WorkerNumber.Text;
                        string username = TextBox_Username.Text;
                        string password = PasswordBox_Password.Password;
                        string workerType = ComboBox_WorkerType.SelectedItem.ToString();

                        Worker newWorker = new Worker()
                        {
                            NSS = nss,
                            RFC = rfc,
                            WorkerNumber = workerNumber,
                            Username = username,
                            Password = password,
                            Role = workerType
                        };

                        if (UserLogic.RegisterNewWorker(newUser, newWorker) == 200)
                        {
                            MessageBox.Show(itemsResource.GetString("UserRegister_WorkerSuccesfull_Message"), "", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                        
                    }
                }
                else if (userTypeSelected.Equals(itemsResource.GetString("UserRegister_Customer_UserType")))
                {
                    if (ValidateCustomerFields())
                    {
                        string street = TextBox_Street.Text;
                        string number = TextBox_Number.Text;
                        string city = TextBox_City.Text;
                        string zipCode = TextBox_Zipcode.Text;
                        string neighborhood = TextBox_Colony.Text;
                        string references = TextBox_References.Text;

                        Address newAddress = new Address()
                        {
                            street = street,
                            number = number,
                            city = city,
                            zipcode = zipCode,
                            neighborhood = neighborhood,
                            instructions = references
                        };

                        if (UserLogic.RegisterNewCustomer(newUser, newAddress) == 200)
                        {
                            MessageBox.Show(itemsResource.GetString("UserRegister_CustomerSuccesfull_Message"), "", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                        }
                    }
                }
            }
        }


        private void Button_ModifyUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Actualizado");
        }


        private void Button_CancelRegisterUser_Click(object sender, RoutedEventArgs e)
        {
            var buttonPressed = MessageBox.Show("¿Desea cancelar el registro de usuario?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(buttonPressed == MessageBoxResult.Yes)
            {
                Close();
            }
        }


        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void UserData(object sender, SelectionChangedEventArgs e)
        {
            string userSelected = ComboBox_UserType.SelectedItem.ToString();
            if (userSelected.Equals(itemsResource.GetString("UserRegister_Worker_UserType")))
            {
                WorkerGrid.Visibility = Visibility.Visible;
                CustomerGrid.Visibility = Visibility.Hidden;
            }
            else if (userSelected.Equals(itemsResource.GetString("UserRegister_Customer_UserType")))
            {
                WorkerGrid.Visibility = Visibility.Hidden;
                CustomerGrid.Visibility = Visibility.Visible;
            }
            
        }


        private void setComboBoxItems()
        {
            ComboBox_UserType.ItemsSource = new string[]
            {
                itemsResource.GetString("UserRegister_Worker_UserType"),
                itemsResource.GetString("UserRegister_Customer_UserType")
            };

            ComboBox_WorkerType.ItemsSource = new string[]
            {
                itemsResource.GetString("UserRegister_Cashier_WorkerType"),
                itemsResource.GetString("UserRegister_Waiter_WorkerType"),
                itemsResource.GetString("UserRegister_Supervisor_WorkerType"),
                itemsResource.GetString("UserRegister_Administrator_WorkerType"),
                itemsResource.GetString("UserRegister_Cook_WorkerType")
            };
        }


        private bool ValidateFields()
        {
            bool valid = true;
            if(ComboBox_UserType.SelectedItem == null)
            {
                ComboBox_UserType.BorderThickness = new Thickness(2);
                valid = false;
            }
            if(TextBox_Name.Text.Equals(string.Empty) || !stringFormat.IsMatch(TextBox_Name.Text))
            {
                TextBox_Name.BorderThickness = new Thickness(2);
                valid = false;
            }
            if(TextBox_Lastname.Text.Equals(string.Empty) || !stringFormat.IsMatch(TextBox_Lastname.Text))
            {
                TextBox_Lastname.BorderThickness = new Thickness(2);
                valid = false;
            }
            if(TextBox_PhoneNumber.Text.Equals(string.Empty) || !phoneNumberFormat.IsMatch(TextBox_PhoneNumber.Text))
            {
                TextBox_PhoneNumber.BorderThickness = new Thickness(2);
                valid = false;
            }
            if(TextBox_Email.Text.Equals(string.Empty) || !emailFormat.IsMatch(TextBox_Email.Text))
            {
                TextBox_Email.BorderThickness = new Thickness(2);
                valid = false;
            }

            
            return valid;
        }

        private bool ValidateWorkerFields()
        {
            bool isValid = true;

            if (TextBox_NSS.Text.Equals(string.Empty) || !nssFormat.IsMatch(TextBox_NSS.Text))
            {
                TextBox_NSS.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_RFC.Text.Equals(string.Empty) || !rfcFormat.IsMatch(TextBox_RFC.Text))
            {
                TextBox_RFC.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_WorkerNumber.Text.Equals(string.Empty) || !numberFormat.IsMatch(TextBox_WorkerNumber.Text))
            {
                TextBox_WorkerNumber.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_Username.Text.Equals(string.Empty) || !usernameFormat.IsMatch(TextBox_Username.Text))
            {
                TextBox_Username.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (PasswordBox_Password.Password.Equals(string.Empty) || !passwordFormat.IsMatch(PasswordBox_Password.Password))
            {
                PasswordBox_Password.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if(ComboBox_WorkerType.SelectedItem == null)
            {
                ComboBox_WorkerType.BorderThickness = new Thickness(2);
                isValid = false;
            }

            return isValid;
        }

        private bool ValidateCustomerFields()
        {
            bool isValid = true;

            if(TextBox_Street.Text.Equals(string.Empty) || !streetFormat.IsMatch(TextBox_Street.Text))
            {
                TextBox_Street.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_Colony.Text.Equals(string.Empty) || !stringFormat.IsMatch(TextBox_Colony.Text))
            {
                TextBox_Colony.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_Number.Text.Equals(string.Empty) || !numberFormat.IsMatch(TextBox_Number.Text))
            {
                TextBox_Number.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_Zipcode.Text.Equals(string.Empty) || !zipCodeFormat.IsMatch(TextBox_Zipcode.Text))
            {
                TextBox_Zipcode.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_City.Text.Equals(string.Empty) || !stringFormat.IsMatch(TextBox_City.Text))
            {
                TextBox_City.BorderThickness = new Thickness(2);
                isValid = false;
            }
            if (TextBox_References.Text.Equals(string.Empty) || !referencesFormat.IsMatch(TextBox_References.Text))
            {
                TextBox_References.BorderThickness = new Thickness(2);
                isValid = false;
            }

            return isValid;
        }

        private void ResetFields()
        {
            ComboBox_UserType.BorderThickness = new Thickness(0);
            TextBox_Name.BorderThickness = new Thickness(0);
            TextBox_Lastname.BorderThickness = new Thickness(0);
            TextBox_PhoneNumber.BorderThickness = new Thickness(0);
            TextBox_Email.BorderThickness = new Thickness(0);
            TextBox_NSS.BorderThickness = new Thickness(0);
            TextBox_RFC.BorderThickness = new Thickness(0);
            TextBox_WorkerNumber.BorderThickness = new Thickness(0);
            TextBox_Username.BorderThickness = new Thickness(0);
            PasswordBox_Password.BorderThickness = new Thickness(0);
            TextBox_Street.BorderThickness = new Thickness(0);
            TextBox_Colony.BorderThickness = new Thickness(0);
            TextBox_Number.BorderThickness = new Thickness(0);
            TextBox_Zipcode.BorderThickness = new Thickness(0);
            TextBox_City.BorderThickness = new Thickness(0);
            TextBox_References.BorderThickness = new Thickness(0);
        }


        public void SetModifyUserForm(User userToModify)
        {
            Button_Register.IsEnabled = false;
            Button_Register.Visibility = Visibility.Hidden;

            Button_Modify.IsEnabled = true;
            Button_Modify.Visibility = Visibility.Visible;

            Textbox_IdUser.Text = userToModify.IdUser.ToString();
            TextBox_Name.Text = userToModify.Name;
            TextBox_Lastname.Text = userToModify.Lastname;
            TextBox_PhoneNumber.Text = userToModify.PhoneNumber;
            TextBox_Email.Text = userToModify.Email;

            if (userToModify.UserType.Equals("Trabajador"))
            {
                WorkerGrid.Visibility = Visibility.Visible;
                ComboBox_UserType.IsEnabled= false;
                ComboBox_UserType.SelectedIndex = 0;

                Worker workerInfo = UserLogic.GetWorkerById(Int32.Parse(Textbox_IdUser.Text));

                if (workerInfo != null)
                {
                    TextBox_NSS.Text = workerInfo.NSS;
                    TextBox_RFC.Text = workerInfo.RFC;
                    TextBox_WorkerNumber.Text = workerInfo.WorkerNumber;
                    TextBox_Username.Text = workerInfo.Username;
                    PasswordBox_Password.IsEnabled = false;
                    ComboBox_WorkerType.SelectedIndex = ComboBox_WorkerType.Items.IndexOf(workerInfo.Role);
                }
            }
            else if (userToModify.UserType.Equals("Cliente"))
            {
                CustomerGrid.Visibility = Visibility.Visible;
                ComboBox_UserType.IsEnabled = false;
                ComboBox_UserType.SelectedIndex = 1;

                Address addressInfo = UserLogic.GetAddressByIdUser(Int32.Parse(Textbox_IdUser.Text));

                if (addressInfo != null)
                {
                    TextBox_Street.Text = addressInfo.street;
                }
            }
            
        }

        
    }
}
