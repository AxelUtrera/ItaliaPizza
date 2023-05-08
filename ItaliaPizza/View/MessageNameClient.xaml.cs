using System.Windows;

namespace View
{
    /// <summary>
    /// Lógica de interacción para MessageNameClient.xaml
    /// </summary>
    public partial class MessageNameClient : Window
    {
        public string nameClient = "";

        public MessageNameClient()
        {
            InitializeComponent();
        }
        

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
        }


        public void Button_Accept_Click(object sender, RoutedEventArgs e)
        {
            nameClient = InputTextBox.Text;
            this.Close();
        }

    }
}
