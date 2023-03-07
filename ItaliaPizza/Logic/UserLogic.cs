using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Net;
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
                catch (EntityException ex)
                {
                    operationResult = 401;
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
            catch(EntityException ex)
            {

            }

            return workerFounded;
            
        }


        
    }


    
}
