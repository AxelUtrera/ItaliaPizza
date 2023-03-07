using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        protected int idUser;
        protected string email;
        protected string name;
        protected string lastname;
        protected string phoneNumber;
        protected bool isActive;

        public int IdUser
        {
            get { return idUser; }
            set { idUser = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Lastname
        {
            get { return lastname; }
            set
            {
                lastname = value;
            }
        }
        public string PhoneNumber
        {
            get { return phoneNumber; }
            set
            {
                phoneNumber = value;
            }
        }
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
            }
        }
        public User()
        {

        }


    }
}
