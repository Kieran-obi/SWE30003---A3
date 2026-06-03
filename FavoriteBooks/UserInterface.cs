using System;
using System.Collections.Generic;

namespace FavoriteBooks;

public class UserInterface(Database db)
{
    private Database _db = db;

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

        //checks email is not already in use
        var existing = _db.readUser(email);
        if (existing.email != "")
        {
            Console.WriteLine("Error: an account with that email already exists.");
            return null;
        }

        User newUser = new User(firstName, lastName, email, password);
        _db.writeUser(newUser.UserId, newUser.FirstName, newUser.LastName, newUser.Email, password, newUser.Role.ToString());
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
            
            //look up user in the database
            var record = _db.readUser(email);
            if (record.email == "")
            {
                Console.WriteLine("Error: email or password is incorrect");
                attempts++;
                continue;
            }

            //reconstruct user and authenticate
            UserRole role = record.role == "Administrator" ? UserRole.Administrator : UserRole.Customer;
            User user = new User(record.userId, record.fName, record.lName, record.email, record.password, role);

            if (!user.Authenticate(password))
            {
                Console.WriteLine("Error: email or password is incorrect.");
                attempts++;
                continue;
            }

            Console.WriteLine($"Login successful. Welcome back, {user.GetFullName()}!");
            string roleDisplay = user.IsAdmin() ? "Administrator" : "Customer";
            Console.WriteLine($"Role: {roleDisplay}");
            return user;
        }
        Console.WriteLine("Too many failed attempts. Try again later.");
        return null;
    }

    public User? RegisterAdmin(User currentUser)
    {
        if(!currentUser.IsAdmin())
        {
            Console.WriteLine("Error: only administrators can create admin accounts");
            return null;
        }
        Console.WriteLine("\n=== Register Admin ===");

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

        //checks email is not already in use
        var existing = _db.readUser(email);
        if (existing.email != "")
        {
            Console.WriteLine("Error: an account with that email already exists.");
            return null;
        }

        User newAdmin = new User(firstName, lastName, email, password, UserRole.Administrator);
        _db.writeUser(newAdmin.UserId, newAdmin.FirstName, newAdmin.LastName, newAdmin.Email, password, newAdmin.Role.ToString());
        Console.WriteLine($"Admin account created successfully for {newAdmin.GetFullName()}!");
        return newAdmin;
    }

    public void DefaultAdmin(string firstName, string lastName, string email, string password)
    {
        var existing = _db.readUser(email);
        if(existing.email != "") return; 

        User admin = new User(firstName, lastName, email, password, UserRole.Administrator);
        _db.writeUser(admin.UserId, admin.FirstName, admin.LastName, admin.Email, password, admin.Role.ToString());
    }

}