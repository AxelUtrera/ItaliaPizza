using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ItaliaPizzaServer
{
    internal class Host
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Comunication.Service1)))
            {
                host.Open();
                Console.WriteLine("The server is ready :D");
                Console.ReadLine();
            }
        }
    }
}
