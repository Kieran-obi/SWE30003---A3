using System;
using System.Collections.Generic;

namespace FavoriteBooks;

class Program
{
    static void Main(string[]args)
    {
        UserInterface ui = new UserInterface();

        //successful registration
        Console.WriteLine("-- Test 1: Register a new account --");
        ui.Register();

        //duplicate email
        Console.WriteLine("-- Test 2: Register with same email --");
        ui.Register();

        //blank fields 
        Console.WriteLine("-- Test 3: Register with blank fields --");
        ui.Register();

        //wrong password
        Console.WriteLine("-- Test 4: Login with wrong password --");
        ui.Login();

        //successful login
        Console.WriteLine("-- Test 5: Login with correct details --");
        ui.Login();

    }
}