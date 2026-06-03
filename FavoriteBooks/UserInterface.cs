using System;
using System.Collections.Generic;

namespace FavoriteBooks;

public class UserInterface
{
    private List<User> _users = new List<User>();

    public User? Register()
    {
        Console.WriteLine("\n=== Register ===");

        Console.Write("First name: ");
        string firstName = Console.ReadLine() ?? "";

        Console.Write("Last name: ");
        string lastName = Console.ReadLine() ?? "";

        Console.Write("Email: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Password: ");
        string password = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(firstName) ||
        string.IsNullOrWhiteSpace(lastName)  ||
        string.IsNullOrWhiteSpace(email)     ||
        string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Error: all fields are required.");
            return null;
        }

        if (FindByEmail(email) != null)
        {
            Console.WriteLine("Error: an account with that email already exists.");
            return null;
        }

        User newUser = new User(firstName, lastName, email, password);
        _users.Add(newUser);
        Console.WriteLine($"Account created successfully. Welcome, {newUser.GetFullName()}!");
        return newUser;
    }

    public User? Login()
    {
        Console.WriteLine("\n=== Login ===");
        int attempts = 0;
        while (attempts < 3)
        {

            Console.Write("Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Error: email and password are required.");
                attempts++;
                continue;
            }

            User? user = FindByEmail(email);

            if (user == null || !user.Authenticate(password))
            {
                Console.WriteLine("Error: email or password is incorrect.");
                attempts++;
                continue;
            }

            Console.WriteLine($"Login successful. Welcome back, {user.GetFullName()}!");
            string role = user.IsAdmin() ? "Administrator" : "Customer";
            Console.WriteLine($"Role: {role}");
        }
        Console.WriteLine("Too many failed attempts. Try again later.");
        return null;
    }

    private User? FindByEmail(string email)
    {
        foreach (User user in _users)
        {
            if (user.Email == email)
            {
                return user;
            }
        }
        return null;
    }
}