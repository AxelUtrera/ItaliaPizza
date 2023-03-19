using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
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

        public static List<User> RecoverActiveUsers()
        {
            List<User> usersObtained = new List<User>();

            using (var database = new ItaliaPizzaEntities())
            {
                var activeUsers = database.users.Where(x => x.active.Equals(true)).ToList();

                foreach (var user in activeUsers)
                {
                    User recoverUser = new User()
                    {
                        IdUser = user.idUser,
                        Name = user.name,
                        Lastname = user.lastname,
                        PhoneNumber = user.phoneNumber,
                        Email = user.email,
                        IsActive = user.active,
                        UserType = "Todavia Falta"
                    };

                    usersObtained.Add(recoverUser);
                }
            }
            return usersObtained;
        }
    }
}
