using System;
using System.Collections.Generic;
namespace FavoriteBooks;

public class Catalogue
{
    private Database _db;

    public Catalogue(Database database)
    {
        _db = database;
    }

    // Returns all books in the catalogue
    public List<Book> GetAllBooks()
    {
        return ConvertToBooks(_db.readBook());
    }

    // Returns books matching the given title
    public List<Book> SearchByTitle(string title)
    {
        return ConvertToBooks(_db.readBook(title: title));
    }

    // Returns books matching the given author
    public List<Book> SearchByAuthor(string author)
    {
        return ConvertToBooks(_db.readBook(author: author));
    }

    // Returns a single book by ID, or null if not found
    public Book? GetBookById(int bookId)
    {
        List<Book> results = ConvertToBooks(_db.readBook(bookId: bookId));
        if (results.Count > 0)
        {
            return results[0];
        }
        return null;
    }

    // Adds a new book to the catalogue
    public void AddBook(string title, string author, string description, double price, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Error: title cannot be empty.");
            return;
        }
        if (string.IsNullOrWhiteSpace(author))
        {
            Console.WriteLine("Error: author cannot be empty.");
            return;
        }
        _db.writeBook(title, author, description);
    }

    // Converts database tuples into Book objects
    private List<Book> ConvertToBooks(List<(int bookId, string title, string author, string desc)> raw)
    {
        List<Book> books = new List<Book>();
        foreach (var entry in raw)
        {
            books.Add(new Book(entry.bookId, entry.title, entry.author, entry.desc, 0.0, 0));
        }
        return books;
    }
}