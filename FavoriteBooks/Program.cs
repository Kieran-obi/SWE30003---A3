using System;
using System.Collections.Generic;

namespace FavoriteBooks;

class Program
{
    static void Main(string[]args)
    {
        Database db = new Database();
        UserInterface ui = new UserInterface(db);

        //default admin
        ui.DefaultAdmin("Admin", "User", "admin@favbooks.com", "admin123");

        //successful registration
        //Console.WriteLine("-- Test 1: Register a new account --");
        //ui.Register();

        //duplicate email
        //Console.WriteLine("-- Test 2: Register with same email --");
        //ui.Register();

        //blank fields 
        //Console.WriteLine("-- Test 3: Register with blank fields --");
        //ui.Register();

        //wrong password
        //Console.WriteLine("-- Test 4: Login with wrong password --");
        //ui.Login();

        //successful customer login
        Console.WriteLine("-- Test 5: Login with correct details --");
        ui.Login();

        //successful admin login
        Console.WriteLine("-- Test 6: Login as admin --");
        User? admin = ui.Login();

        //create new admin account
        //Console.WriteLine("-- Test 7: Admin creates new admin account --");
        //if (admin != null)
        //{
        //    ui.RegisterAdmin(admin);
        //}

        //customer attempts admin login
        Console.WriteLine("-- Test 8: Customer tries to create admin account --");
        User? customer = ui.Login();
        if (customer != null)
        {
            ui.RegisterAdmin(customer);
        }

    }
}