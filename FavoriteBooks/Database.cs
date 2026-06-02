using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FavoriteBooks;

public class Database
{
    public Database()
    {
        if (!File.Exists("Users.txt"))
        {
            File.WriteAllText("Users.txt", null);
        }
        if (!File.Exists("Books.txt"))
        {
            File.WriteAllText("Books.txt", null);
        }
        if (!File.Exists("Carts.txt"))
        {
            File.WriteAllText("Carts.txt", null);
        }
        if (!File.Exists("Orders.txt"))
        {
            File.WriteAllText("Orders.txt", null);
        }
    }

    // email as parameter
    // returns all details of the user with that email
    public (string userId, string fName, string lName, string email, string password, string role) readUser(string email)
    {
        string userIdRet = "";
        string fNameRet = "";
        string lNameRet = "";
        string emailRet = "";
        string passwordRet = "";
        string roleRet = "";

        string usersFilePath = "Users.txt";
        string[] users = File.ReadAllLines(usersFilePath);
        for (int i = 0; i < users.Length; i += 6)
        {
            if (users[i + 3] == email)
            {
                userIdRet = users[i];
                fNameRet = users[i + 1];
                lNameRet = users[i + 2];
                emailRet = users[i + 3];
                passwordRet = users[i + 4];
                roleRet = users[i + 5];
                break;
            }
        }
        return (userIdRet, fNameRet, lNameRet, emailRet, passwordRet, roleRet);
    }

    // Adds a user to the Users table (.txt file)
    public void writeUser(string userId, string fName, string lName, string email, string password, string role)
    {
        string usersFilePath = "Users.txt";
        File.AppendAllText(usersFilePath, userId + "\n");
        File.AppendAllText(usersFilePath, fName + "\n");
        File.AppendAllText(usersFilePath, lName + "\n");
        File.AppendAllText(usersFilePath, email + "\n");
        File.AppendAllText(usersFilePath, password + "\n");
        File.AppendAllText(usersFilePath, role + "\n");
    }

    // bookId, title, or author as parameter (or any combination of the three (or none of them))
    // Returns a list of books with details matching the parameters
    public List<(int bookId, string title, string author, string desc)> readBook(int bookId = 0, string title = "", string author = "")
    {
        List<(int bookId, string title, string author, string desc)> booksRet = new List<(int bookId, string title, string author, string desc)>();

        string booksFilePath = "Books.txt";
        string[] books = File.ReadAllLines(booksFilePath);
        for (int i = 0; i < books.Length; i += 4)
        {
            if ((bookId.ToString() == books[i] || bookId == 0) && (title == books[i + 1] || title == "") && (author == books[i + 2] || author == ""))
            {
                booksRet.Add((Convert.ToInt32(books[i]), books[i + 1], books[i + 2], books[i + 3]));
            }
        }
        return booksRet;
    }

    // Adds a book to the Books table (.txt file)
    public void writeBook(string title, string author, string desc)
    {
        string booksFilePath = "Books.txt";

        int bookId = ((File.ReadLines("Books.txt").Count()) / 4) + 1;

        File.AppendAllText(booksFilePath, bookId.ToString() + "\n");
        File.AppendAllText(booksFilePath, title + "\n");
        File.AppendAllText(booksFilePath, author + "\n");
        File.AppendAllText(booksFilePath, desc + "\n");
    }

    // userId as parameter
    // Returns all items in the shopping cart belonging to that user
    public List<(string userId, int bookId, int quantity)> readCart(string userId)
    {
        List<(string userId, int bookId, int quantity)> cartsRet = new List<(string userId, int bookId, int quantity)>();

        string cartsFilePath = "Carts.txt";
        string[] carts = File.ReadAllLines(cartsFilePath);
        for (int i = 0; i < carts.Length; i += 3)
        {
            if (carts[i] == userId)
            {
                cartsRet.Add((carts[i], Convert.ToInt32(carts[i]), Convert.ToInt32(carts[i])));
            }
        }
        return cartsRet;
    }

    // Adds an item to the Carts table (.txt file)
    public void writeCart(string userId, int bookId, int quantity)
    {
        string cartsFilePath = "Carts.txt";
        bool entryExists = false;

        string[] carts = File.ReadAllLines(cartsFilePath);
        for (int i = 0; i < carts.Length; i += 3)
        {
            if ((carts[i] == userId) && (carts[i + 1] == bookId.ToString()))
            {
                carts[i + 2] = (Convert.ToInt32(carts[i + 2]) + quantity).ToString();
                File.WriteAllLines(cartsFilePath, carts);
                entryExists = true;
                break;
            }
        }

        if (!entryExists)
        {
            File.AppendAllText(cartsFilePath, userId + "\n");
            File.AppendAllText(cartsFilePath, bookId.ToString() + "\n");
            File.AppendAllText(cartsFilePath, quantity.ToString() + "\n");
        }
    }

    // Clears a users cart from the Carts table (.txt file)
    public void clearCart(string userId)
    {
        string cartsFilePath = "Carts.txt";

        string[] carts = File.ReadAllLines(cartsFilePath);
        for (int i = 0; i < carts.Length; i += 3)
        {
            if (carts[i] == userId)
            {
                Array.Clear(carts, i, 1);
            }
        }

        File.WriteAllLines(cartsFilePath, carts);
    }

    // orderId as parameter
    // Returns all items in the order belonging to that order ID
    public List<(int orderId, string userId, int bookId, int quantity)> readCart(int orderId)
    {
        List<(int orderId, string userId, int bookId, int quantity)> ordersRet = new List<(int orderId, string userId, int bookId, int quantity)>();

        string ordersFilePath = "Orders.txt";
        string[] orders = File.ReadAllLines(ordersFilePath);
        for (int i = 0; i < orders.Length; i += 4)
        {
            if (orders[i] == orderId.ToString())
            {
                ordersRet.Add((Convert.ToInt32(orders[i]), orders[i + 1], Convert.ToInt32(orders[i + 2]), Convert.ToInt32(orders[i + 3])));
            }
        }
        return ordersRet;
    }

    // Adds an order to the Carts table (.txt file)
    public void writeOrder(int orderId, string userId, int bookId, int quantity)
    {
        string ordersFilePath = "Orders.txt";
        File.AppendAllText(ordersFilePath, orderId.ToString() + "\n");
        File.AppendAllText(ordersFilePath, userId + "\n");
        File.AppendAllText(ordersFilePath, bookId.ToString() + "\n");
        File.AppendAllText(ordersFilePath, quantity.ToString() + "\n");
    }

    // Gets the next unused order number
    public int getNextOrderId()
    {
        string ordersFilePath = "Orders.txt";
        string[] orders = File.ReadAllLines(ordersFilePath);
        int nextOrderId = 1;

        for (int i = 0; i < orders.Length; i += 4)
        {
            if (Convert.ToInt32(orders[i]) >= nextOrderId)
            {
                nextOrderId = Convert.ToInt32(orders[i]) + 1;
            }
        }

        return nextOrderId;
    }
}
