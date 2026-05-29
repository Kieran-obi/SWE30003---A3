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
    private readonly string _userid;
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _password;
    private ShoppingCart _cart;
    private UserRole _role;

    public User(string firstName, string lastName, string email, string password, UserRole role = UserRole.Customer)
    {
        _userid = Guid.NewGuid().ToString();
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
        _password = password;
        _role = role;
        _cart = new ShoppingCart(this);
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
        get {return _userid;}
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
    public ShoppingCart GetCart()
    {
        return _cart;
    }
    public UserRole Role
    {
        get {return _role;}
    }

}