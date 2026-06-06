using System;
using System.Collections.Generic;
namespace FavoriteBooks;

public class Book
{
    private int bookId;
    private string title;
    private string author;
    private string description;
    private double price;
    private int stockQuantity;

    public Book(int bookId, string title, string author, string description, double price, int stockQuantity)
    {
        this.bookId = bookId;
        this.title = title;
        this.author = author;
        this.description = description;
        this.price = price;
        this.stockQuantity = stockQuantity;
    }

    // Constructor used by ShoppingCart when loading from database
    public Book(int bookId)
    {
        this.bookId = bookId;
        this.title = "";
        this.author = "";
        this.description = "";
        this.price = 0.0;
        this.stockQuantity = 0;
    }

    public int GetBookId()
    {
        return bookId;
    }

    public string GetTitle()
    {
        return title;
    }

    public string GetAuthor()
    {
        return author;
    }

    public string GetDescription()
    {
        return description;
    }

    public double GetPrice()
    {
        return price;
    }

    public int GetStockQuantity()
    {
        return stockQuantity;
    }

    public override string ToString()
    {
        return $"[{bookId}] {title} by {author} - ${price:F2} ({stockQuantity} in stock)\n    {description}";
    }
}