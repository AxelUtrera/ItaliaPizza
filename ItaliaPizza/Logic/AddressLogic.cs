using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Model;

namespace Logic
{
    public class AddressLogic
    {
        public static List<Address> GetAllAddresses() 
        {
            List<Address> addressesDataBase = new List<Address>();
            using (ItaliaPizzaEntities dataBase = new ItaliaPizzaEntities())
            {
                var addresses = from address in dataBase.address select address;

                if (addresses != null)
                {
                    foreach (var address in addresses)
                    {
                       Address addressToAdd =  new Address()
                        {
                            idAddress = address.idAddress,
                            city = address.city,
                            idCustomer = address.idCustomer,
                            instructions = address.instructions,
                            neighborhood = address.neighborhood,
                            number = address.number,
                            street = address.street,
                            zipcode = address.zipcode                          
                        };
                        //se recupera el nombre del cliente y el apellido para poder mostrarlo en la tabla.
                        string nameRecover = GetCustomerNameByIdCustmerAddress(addressToAdd.idCustomer).name;
                        string lastNameRecover = GetCustomerNameByIdCustmerAddress(addressToAdd.idCustomer).lastname;
                        addressToAdd.nameCustomer = nameRecover + " " + lastNameRecover;
                        addressesDataBase.Add(addressToAdd);
                    }
                   
                }
            }
            
            return addressesDataBase;
        }

        private static users GetCustomerNameByIdCustmerAddress(int idCustomerAddress)
        {
            users customerDataBase = new users();
            using (ItaliaPizzaEntities dataBase = new ItaliaPizzaEntities())
            {
                var customerRecovered = dataBase.customer.FirstOrDefault(c => c.idCustomer == idCustomerAddress);
                var userRecovered = dataBase.users.FirstOrDefault(u => u.idUser == customerRecovered.idUser);
                if(userRecovered != null)
                {
                    customerDataBase = userRecovered;
                }
            }
            return customerDataBase;
        }

    }
}
