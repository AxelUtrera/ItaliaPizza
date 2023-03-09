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

        public override bool Equals(object obj)
        {
            return obj is Worker worker &&
                   username == worker.username &&
                   nss == worker.nss &&
                   password == worker.password &&
                   rfc == worker.rfc &&
                   role == worker.role &&
                   workerNumber == worker.workerNumber &&
                   idUser == worker.idUser &&
                   Username == worker.Username &&
                   NSS == worker.NSS &&
                   Password == worker.Password &&
                   RFC == worker.RFC &&
                   Role == worker.Role &&
                   WorkerNumber == worker.WorkerNumber &&
                   IdUser == worker.IdUser;
        }

        public override int GetHashCode()
        {
            int hashCode = 1383793456;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(nss);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(rfc);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(role);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(workerNumber);
            hashCode = hashCode * -1521134295 + idUser.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(NSS);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(RFC);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Role);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(WorkerNumber);
            hashCode = hashCode * -1521134295 + IdUser.GetHashCode();
            return hashCode;
        }
    }
}
