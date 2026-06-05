using System;
using System.Collections.Generic;

namespace FavoriteBooks;

public class ShoppingCart {
    private List<Book> books;
    private List<int> quantities;

    private User user;


    public ShoppingCart(User user) {
        books = new List<Book>();
        quantities = new List<int>();
        this.user = user;
    }


    public bool AddBook(Book book, int quantity) {
        if (book == null || quantity <= 0) {
            return false;
        }

        if (book.GetStockQuantity() < quantity) {
            return false;
        }

        int index = books.IndexOf(book);

        if (index >= 0) {
            quantities[index] += quantity;
        }
        else {
            books.Add(book);
            quantities.Add(quantity);
        }

        return true;
    }


    public bool RemoveBook(Book book) {
        int index = books.IndexOf(book);

        if (index < 0) {
            return false;
        }

        books.RemoveAt(index);
        quantities.RemoveAt(index);

        return true;
    }


    public void LoadCart(Database database) {
        books.Clear();
        quantities.Clear();

        var items = database.readCart(user.UserId);

        foreach (var item in items) {
            Book book = new Book(item.bookId);
            books.Add(book);
            quantities.Add(item.quantity);
        }
    }


    public void SaveCart(Database database) {
        database.clearCart(user.UserId);

        for (int i = 0; i < books.Count; i++) {
            database.writeCart(user.UserId, books[i].GetBookId(), quantities[i]);
        }
    }


    public Order Checkout() {
        Order order = new Order(user, books, quantities);

        ClearCart();

        return order;
    }


    public void ClearCart() {
        books.Clear();
        quantities.Clear();
    }


    public List<Book> GetBooks() {
        return books;
    }


    public List<int> GetQuantities() {
        return quantities;
    }
}