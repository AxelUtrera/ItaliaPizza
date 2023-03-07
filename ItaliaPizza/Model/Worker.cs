using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Worker
    {
        private string username;
        private string nss;
        private string password;
        private string rfc;
        private string role;
        private string workerNumber;
        private int idUser;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string NSS
        {
            get { return nss; }
            set { nss = value; }
        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
            }
        }

        public string RFC
        {
            get { return rfc; }
            set { rfc = value; }
        }

        public string Role
        {
            get { return role; }
            set
            {
                role = value;
            }
        }

        public string WorkerNumber
        {
            get { return workerNumber; }
            set
            {
                workerNumber = value;
            }
        }

        public int IdUser
        {
            set { idUser = value; }
            get { return idUser; }
        }

    }
}
