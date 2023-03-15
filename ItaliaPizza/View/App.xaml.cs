using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace View
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTMwNjgzOEAzMjMwMmUzNDJlMzBZc0pWMUNwWGZhU0JmUjR3c0k2OUJBMjZSWkFPNjN3RFlBVU5hdnhvcU9vPQ==");
        }
    }
}
