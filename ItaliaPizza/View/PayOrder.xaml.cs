using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class PayOrder : Window
    {
        public static Order payingOrder = new Order();

        public PayOrder()
        {
            InitializeComponent();
            SetOrderInfo();
            SetOrderProducts();
        }

        private void Button_Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_PayOrder_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatePrice())
            {
                if (OrderLogic.ChangeOrderStatus(payingOrder.idOrder) == 200)
                {
                    CalculateChange();
                    CloseWindowAndUpdateTable();
                }
            }
            else
            {
                MessageBox.Show("Ingrese un precio valido", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void SetOrderInfo()
        {
            TextBox_OrderNumber.Text = payingOrder.idOrder.ToString();
            TextBox_Client.Text = payingOrder.nameCustomer.ToString();
            TextBox_TypeOrder.Text = payingOrder.typeOrder.ToString();
            TextBox_Date.Text = payingOrder.date;
            TextBox_Total.Text = payingOrder.total.ToString();
        }


        private void SetOrderProducts()
        {
            List<Product> products = OrderLogic.GetInfoProductById(payingOrder.idOrder);
            ObservableCollection<Product> infoProduct = new ObservableCollection<Product>(products);
            ItemsTable.ItemsSource = infoProduct;
        }

        private bool ValidatePrice()
        {
            bool isValid = true;
            string regexPattern = @"^\d+(\.\d{2})?$";
            double pay = Double.Parse(TextBox_Pay.Text);

            if(!Regex.IsMatch(TextBox_Pay.Text, regexPattern))
            {
                isValid = false;
            }
            if(pay < payingOrder.total)
            {
                isValid = false;
            }
            return isValid;
        }


        private void CalculateChange()
        {
            double total = payingOrder.total;
            double pay = Double.Parse(TextBox_Pay.Text);
            double change = pay - total;

            MessageBox.Show("El cambio es de: $" + change, "Orden Pagada", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void CloseWindowAndUpdateTable()
        {
            if(PendingPayOrders.instance != null)
            {
                PendingPayOrders.instance.RecoverPayPendingOrders();
            }

            this.Close();
        }
    }
}
