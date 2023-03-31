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
        protected string userType;

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

        public string UserType
        {
            get { return userType; }
            set { userType = value; }
        }

        public User()
        {

        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   idUser == user.idUser &&
                   email == user.email &&
                   name == user.name &&
                   lastname == user.lastname &&
                   phoneNumber == user.phoneNumber &&
                   isActive == user.isActive &&
                   userType == user.userType &&
                   IdUser == user.IdUser &&
                   Email == user.Email &&
                   Name == user.Name &&
                   Lastname == user.Lastname &&
                   PhoneNumber == user.PhoneNumber &&
                   IsActive == user.IsActive &&
                   UserType == user.UserType;
        }

        public override int GetHashCode()
        {
            int hashCode = -379080006;
            hashCode = hashCode * -1521134295 + idUser.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(lastname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(phoneNumber);
            hashCode = hashCode * -1521134295 + isActive.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(userType);
            hashCode = hashCode * -1521134295 + IdUser.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lastname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PhoneNumber);
            hashCode = hashCode * -1521134295 + IsActive.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(UserType);
            return hashCode;
        }
    }
}
