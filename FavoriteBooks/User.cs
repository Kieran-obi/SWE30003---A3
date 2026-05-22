using System;
using System.Collections.Generic;

namespace FavoriteBooks;

public enum UserRole
{
    Customer,
    Administrator
}
public class User
{
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _password;
    private UserRole _role;

    public User(string firstName, string lastName, string email, string password, UserRole role = UserRole.Customer)
    {
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _password = password;
        _role = role;
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

    public string FirstName
    {
        get {return _firstName;}
    }
    public string LastName
    {
        get {return _lastName;}
    }
    public string Email
    {
        get {return _email;}
    }
    public UserRole Role
    {
        get {return _role;}
    }

}