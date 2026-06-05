using System;
using System.Collections.Generic;

namespace FavoriteBooks;

public class Order
{
    private int orderId;
    private User user;
    private List<Book> books;
    private List<int> quantities;
    private List<double> prices;

    public Order(User user, List<Book> books, List<int> quantities) {
        this.user = user;
        this.books = new List<Book>(books);
        this.quantities = new List<int>(quantities);

        prices = new List<double>();

        for (int i = 0; i < books.Count; i++) {
            prices.Add(books[i].GetPrice());
        }

    }


    public void WriteOrderInDatabase(Database database) {
        orderId = database.getNextOrderId();
        for (int i = 0; i < books.Count; i++) {
            database.writeOrder(orderId, user.UserId, books[i].GetBookId(), quantities[i]);
        }
    }

    public User GetUser() {
        return user;
    }


    public List<Book> GetBooks() {
        return books;
    }


    public List<int> GetQuantities() {
        return quantities;
    }


    public List<double> GetPrices() {
        return prices;
    }
}