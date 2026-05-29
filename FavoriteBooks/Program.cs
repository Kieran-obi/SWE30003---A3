using System;
using System.Collections.Generic;

namespace FavoriteBooks;

class Program
{
    static void Main(string[]args)
    {
        //create new customer
        User customer = new User("Kieran", "O'Brien", "kob@gmail.com", "password123");
        Console.WriteLine("=== New Customer ===");
        Console.WriteLine("Name: " + customer.GetFullName());
        Console.WriteLine("Email: " + customer.Email);
        Console.WriteLine(customer.GetCart() != null ? "cart created": "no cart");
        Console.WriteLine("Is Admin: " + customer.IsAdmin());

        Console.WriteLine("\n=== Authentication ===");
        Console.WriteLine("Correct password: " + customer.Authenticate("password123"));   //should be True
        Console.WriteLine("Wrong password: "   + customer.Authenticate("wrongpassword")); //should be False

        User admin = new User("Boom", "Shakalaka", "bs@email.com", "admin", UserRole.Administrator);

        Console.WriteLine("\n=== New Administrator ===");
        Console.WriteLine("Name: " + admin.GetFullName());
        Console.WriteLine("Is Admin: " + admin.IsAdmin()); // should be True

    }
}