using Logic;
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
    /// Lógica de interacción para TakeOutCash.xaml
    /// </summary>
    public partial class TakeOutCash : Window
    {
        public static Worker workerLogged;
        public List<CashBox> cashBoxes;
        public TakeOutCash()
        {
            cashBoxes = new List<CashBox>();
            InitializeComponent();
            ConfigurateWindow();
        }
        public void ConfigurateWindow()
        {
            string nameWorker = UserLogic.GetUserById(workerLogged.IdUser).Name;
            cashBoxes = CashBoxLogic.GetCashBoxes();
            Label_Employee.Content = nameWorker + " ; " + workerLogged.WorkerNumber;
            Label_Date.Content = DateTime.Today.ToString("dd/MM/yyyy");            
            UpDown_Amount.Value = 0;
            Label_Cash2.Content = string.Empty;
            ComboBox_IdCashbox.ItemsSource = cashBoxes;
        }

        private void ComboBox_IdCashbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_IdCashbox.SelectedItem != null)
            {
                CashBox cashBox = ComboBox_IdCashbox.SelectedItem as CashBox;
                Label_Cash2.Content = cashBox.TotalAmount.ToString();
                UpDown_Amount.MaxValue = cashBox.TotalAmount;
                UpDown_Amount.IsEnabled = true;
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            switch (ValidateText())
            {
                case  1:
                    TextRange textRange = new TextRange(RichTextBox_Reason.Document.ContentStart,
                    RichTextBox_Reason.Document.ContentEnd);
                    Transactions transactions = new Transactions()
                    {
                        Reason = textRange.Text,
                        CashBox = ComboBox_IdCashbox.SelectedItem as CashBox,
                        Amount = (int)UpDown_Amount.Value,
                        Worker = workerLogged.Username
                    };
                    CashBox cashBox = ComboBox_IdCashbox.SelectedItem as CashBox;
                    cashBox.Outcomes += (int)UpDown_Amount.Value;
                    cashBox.TotalAmount = cashBox.TotalAmount - (int)UpDown_Amount.Value;
                    if (Logic.TransactionsLogic.takeOutCash(transactions) && CashBoxLogic.UpdateCashBox(cashBox))
                    {
                        MessageBox.Show("Operacion realizada con Exito", "Operacion Exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Operacion fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                    ConfigurateWindow();
                   
                    break;
                case 0:
                    MessageBox.Show("Por favor, llena correctamente los campos", "Campos invalidos", MessageBoxButton.OK, MessageBoxImage.Error);

                    break;

                case 2:
                    MessageBox.Show("Por favor, asegurate de no dejar campos vacios", "Campos vacios", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
            
        }
        private int ValidateText()
        {
            TextRange textRange = new TextRange(RichTextBox_Reason.Document.ContentStart,
               RichTextBox_Reason.Document.ContentEnd);
            int result = 0;
            if (!string.IsNullOrWhiteSpace(textRange.Text)&&ComboBox_IdCashbox.SelectedItem!=null&&UpDown_Amount.Value!=0)
            {
                Regex regex = new Regex(@"^(?=.?[#?!@$%^&-])");
                Match match = regex.Match(textRange.Text);
                if (!match.Success)
                {
                    result = 1;
                }
                else
                {
                    result = 0;
                }
            }
            else
            {
                result = 2;
            }
            return result;
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            CashBoxMenu.workerLogged = workerLogged;
            CashBoxMenu cashBoxMenuWindow = new CashBoxMenu();
            Close();
            cashBoxMenuWindow.ShowDialog();
        }
    }
}
