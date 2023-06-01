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
            cashBoxes=CashBoxLogic.GetCashBoxes();
            Label_Employee.Content = workerLogged.Username + " ; " + workerLogged.WorkerNumber;
            Label_Date.Content = DateTime.Today.ToString("dd/MM/yyyy");
            ComboBox_IdCashbox.ItemsSource=cashBoxes;
        }

        private void ComboBox_IdCashbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_IdCashbox.SelectedItem != null)
            {
                CashBox cashBox = ComboBox_IdCashbox.SelectedItem as CashBox;
                Label_Cash.Content = Label_Cash.Content + cashBox.TotalAmount.ToString();
                UpDown_Amount.MaxValue = cashBox.TotalAmount;
                UpDown_Amount.IsEnabled = true;
            }
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
