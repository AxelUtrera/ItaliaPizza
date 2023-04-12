using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class UserLogic
    {
        public static int AutenticateUser(string username, string password)
        {
            int operationResult = 500;
            string passwordHash = Encription.ToSHA2Hash(password);

            using (var dataBase = new ItaliaPizzaEntities())
            {
                try
                {
                    var userFound = (from w in dataBase.worker
                                     where
                                     w.username.Equals(username)
                                     && w.password.Equals(passwordHash)
                                     select w).Count();

                    if (userFound > 0)
                    {
                        operationResult = 200;
                    }
                }catch (ArgumentNullException ex)
                {
                    operationResult = 400;
                }
                
                
                return operationResult;
            }
        }

        public static Model.Worker GetWorkerByUsername(string username)
        {
            Model.Worker workerFounded = new Worker();

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {
                    worker worker = database.worker.Find(username);

                    if (worker != null)
                    {
                        workerFounded.IdUser = worker.idUser;
                        workerFounded.WorkerNumber = worker.workerNumber;
                        workerFounded.NSS = worker.nss;
                        workerFounded.Username = worker.username;
                        workerFounded.Password = worker.password;
                        workerFounded.Role = worker.role;
                        workerFounded.RFC = worker.rfc;
                    }
                }
            }
            catch(ArgumentException ex)
            {

            }
            return workerFounded;
            
        }



        public static User GetUserById(int idUser)
        {
            User userFound = new User();

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {
                    users user = database.users.Find(idUser);

                    if (user != null)
                    {
                        userFound.IdUser = user.idUser;
                        userFound.Email = user.email;
                        userFound.Name = user.name;
                        userFound.Lastname = user.lastname;
                        userFound.PhoneNumber = user.phoneNumber;
                        userFound.IsActive = user.active;
                    }
                }
            }
            catch (ArgumentException ex)
            {

            }
            return userFound;

        }


        public static Model.Worker GetWorkerById(int idUser)
        {
            Model.Worker workerFounded = new Worker();

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {

                    worker worker = database.worker.Where(x => x.idUser == idUser).First();

                    if (worker != null)
                    {
                        workerFounded.WorkerNumber = worker.workerNumber;
                        workerFounded.NSS = worker.nss;
                        workerFounded.Username = worker.username;
                        workerFounded.Password = worker.password;
                        workerFounded.Role = worker.role;
                        workerFounded.RFC = worker.rfc;
                    }
                }
            }
            catch (ArgumentException ex)
            {

            }

            return workerFounded;

        }


        public static Model.Address GetAddressByIdUser(int idUser)
        {
            Model.Address addressObtained = new Address();

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {
                    int idCustomer = (from Customer in database.customer where Customer.idUser == idUser select Customer.idCustomer).First();

                    if (idCustomer != 0)
                    {
                        address address = database.address.Where(x => x.idCustomer == idCustomer).First();

                        addressObtained.idAddress = address.idAddress;
                        addressObtained.number = address.number;
                        addressObtained.zipcode = address.zipcode;
                        addressObtained.city = address.city;
                        addressObtained.street = address.street;
                        addressObtained.neighborhood = address.neighborhood;
                        addressObtained.instructions = address.instructions;
                    }
                }
            }
            catch (ArgumentException e)
            {

            }

            return addressObtained;
        }

        public static int RegisterNewWorker(User user, Worker worker)
        {
            int statusCode = 500;

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {
                    var newUser = database.users.Add(new users()
                    {
                        name = user.Name,
                        lastname = user.Lastname,
                        email = user.Email,
                        phoneNumber = user.PhoneNumber,
                        active = true
                    });
                    database.SaveChanges();

                    var idUser = (from Users in database.users where Users.phoneNumber.Equals(user.PhoneNumber) select Users.idUser).First();
                    if (idUser != 0)
                    {
                        var newWorker = database.worker.Add(new worker() 
                        {
                            username = worker.Username,
                            nss = worker.NSS,
                            password = Encription.ToSHA2Hash(worker.Password),
                            rfc = worker.RFC,
                            role = worker.Role,
                            workerNumber = worker.WorkerNumber,
                            idUser = idUser
                        });
                        database.SaveChanges();

                        if(newWorker != null)
                        {
                            statusCode = 200;
                        }
                    }
                }
            }
            catch(ArgumentException ex)
            {

            }
            return statusCode;
        }


        public static int RegisterNewCustomer(User user, Address address)
        {
            int statusCode = 500;

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {
                    var newUser = database.users.Add(new users()
                    {
                        name = user.Name,
                        lastname = user.Lastname,
                        email = user.Email,
                        phoneNumber = user.PhoneNumber,
                        active = true
                    });
                    database.SaveChanges();
                    if (newUser != null)
                    {
                        statusCode = 200;
                    } 
                    else
                    {
                        statusCode = 500;
                    }
                    
                    var idUser = (from Users in database.users where Users.phoneNumber.Equals(user.PhoneNumber) select Users.idUser).First();

                    if(idUser != 0)
                    {
                        var newCustomer = database.customer.Add(new customer()
                        {
                            idUser = idUser
                        });
                        database.SaveChanges();
                        if (newCustomer != null)
                        {
                            statusCode = 200;
                        }
                        else
                        {
                            statusCode = 500;
                        }
                    }

                    var idCustomer = (from Customer in database.customer where Customer.idUser.Equals(idUser) select Customer.idCustomer).First();
                    if(idCustomer != 0)
                    {
                        var newAddress = database.address.Add(new address()
                        {
                            idCustomer = idCustomer,
                            street = address.street,
                            number = address.number,
                            city = address.city,
                            zipcode = address.zipcode,
                            neighborhood = address.neighborhood,
                            instructions = address.instructions
                        });
                        database.SaveChanges();
                        if (newAddress != null)
                        {
                            statusCode = 200;
                        }
                        else
                        {
                            statusCode = 500;
                        }
                    }
                }
            }
            catch(ArgumentException ex)
            {
                statusCode = 500;
            }

            return statusCode;
        }


        public static int ModifyWorker(User updatedUser, Worker updatedWorker)
        {
            int statusCode = 500;
            int resultObtained;

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {
                    var modifyUser = database.users.First(i => i.idUser == updatedUser.IdUser);
                    if (modifyUser != null)
                    {
                        modifyUser.email = updatedUser.Email;
                        modifyUser.name = updatedUser.Name;
                        modifyUser.lastname = updatedUser.Lastname;
                        modifyUser.phoneNumber = updatedUser.PhoneNumber;
                    }
                    resultObtained = database.SaveChanges();
                    if (resultObtained != 0)
                    {
                        statusCode = 200;
                    }

                    var modifyWorker = database.worker.First(i => i.idUser == updatedUser.IdUser);
                    if(modifyWorker != null)
                    {
                        modifyWorker.username = updatedWorker.Username;
                        modifyWorker.nss = updatedWorker.NSS;
                        modifyWorker.rfc = updatedWorker.RFC;
                        modifyWorker.role = updatedWorker.Role;
                        modifyWorker.workerNumber = updatedWorker.WorkerNumber;
                    }
                    resultObtained = database.SaveChanges();
                    if (resultObtained != 0)
                    {
                        statusCode = 200;
                    }
                }
            }
            catch (ArgumentException)
            {
                statusCode = 500;
            }

            return statusCode;
        }


        public static int ModifyCustomer(User updatedUser, Address updatedAddress)
        {
            int statusCode = 500;
            int resultObtained;

            try
            {
                using (var database = new ItaliaPizzaEntities())
                {
                    var idCustomer = (from Customer in database.customer where Customer.idUser.Equals(updatedUser.IdUser) select Customer.idCustomer).First();

                    var modifyUser = database.users.First(i => i.idUser == updatedUser.IdUser);
                    if (modifyUser != null)
                    {
                        modifyUser.email = updatedUser.Email;
                        modifyUser.name = updatedUser.Name;
                        modifyUser.lastname = updatedUser.Lastname;
                        modifyUser.phoneNumber = updatedUser.PhoneNumber;
                    }
                    resultObtained = database.SaveChanges();
                    if (resultObtained != 0)
                    {
                        statusCode = 200;
                    }

                    var modifyAddress = database.address.First(i => i.idCustomer == idCustomer);
                    if (modifyAddress != null)
                    {
                        modifyAddress.street = updatedAddress.street;
                        modifyAddress.number = updatedAddress.number;
                        modifyAddress.city = updatedAddress.city;
                        modifyAddress.zipcode = updatedAddress.zipcode;
                        modifyAddress.neighborhood = updatedAddress.neighborhood;
                        modifyAddress.instructions = updatedAddress.instructions;
                    }

                    resultObtained = database.SaveChanges();
                    if (resultObtained != 0)
                    {
                        statusCode = 200;
                    }
                }
            }
            catch(ArgumentException e)
            {
                statusCode = 500;
            }

            return statusCode;
        }



        public static List<User> RecoverActiveUsers()
        {
            List<User> usersObtained = new List<User>();

            using (var database = new ItaliaPizzaEntities())
            {
                var activeUsers = database.users.Where(x => x.active.Equals(true)).ToList();

                foreach (var user in activeUsers)
                {
                    string _userType;

                    var userType = database.worker.Where(x => x.idUser == user.idUser).Count();
                    if(userType > 0)
                    {
                        _userType = "Trabajador";
                    }
                    else
                    {
                        _userType = "Cliente";
                    }

                    User recoverUser = new User()
                    {
                        IdUser = user.idUser,
                        Name = user.name,
                        Lastname = user.lastname,
                        PhoneNumber = user.phoneNumber,
                        Email = user.email,
                        IsActive = user.active,
                        UserType = _userType
                    };

                    usersObtained.Add(recoverUser);
                }
            }
            return usersObtained;
        }

        public static int DeleteUser(int userId)
		{
			int statusCode = 500;

			try
			{
				using (var database = new ItaliaPizzaEntities())
				{
					var userToDelete = database.users.FirstOrDefault(u => u.idUser == userId);

					if (userToDelete != null)
					{
						userToDelete.active = false;
						database.SaveChanges();
						statusCode = 200;
					}
				}
			}
			catch (ArgumentException ex)
			{
				statusCode = 500;
			}

			return statusCode;
		}
	}
}
