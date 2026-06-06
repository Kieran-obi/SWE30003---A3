using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteBooks
{
    public enum UserRole
    {
        Customer,
        Administrator
    }

    public class User
    {
        Database db;
        
        private string _userid;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private ShoppingCart _cart;
        private UserRole _role;

        public User(Database database, string userId, string firstName, string lastName, string email, string password, string role = "Customer")
        {
            db = database;
            
            _userid = userId;
            _firstName = firstName;
            _lastName = lastName;
            _email = email;
            _password = password;
            if (role == "Customer")
            {
                _role = UserRole.Customer;
            }
            if (role == "Administrator")
            {
                _role = UserRole.Administrator;
            }
            _cart = new ShoppingCart(this, db);
        }

        public bool Authenticate(string password)
        {
            return _password == password;
        }

        public bool IsAdmin()
        {
            return _role == UserRole.Administrator;
        }

        public string GetFullName()
        {
            return $"{_firstName} {_lastName}";
        }

        public string UserId
        {
            get { return _userid; }
        }

        public string FirstName
        {
            get { return _firstName; }
        }
        public string LastName
        {
            get { return _lastName; }
        }
        public string Email
        {
            get { return _email; }
        }
        public ShoppingCart GetCart()
        {
            return _cart;
        }
        public UserRole Role
        {
            get { return _role; }
        }
    }
}
