using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteBooks
{
    internal class Catalogue
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

        // Returns books matching the given title and author
        public List<Book> SearchBooks(string title, string author)
        {
            return ConvertToBooks(_db.readBook(title: title, author: author));
        }

        // Returns a single book by ID, or null if not found
        public Book GetBookById(int bookId)
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
            _db.writeBook(title, author, description, price, stockQuantity);
        }

        // Converts database tuples into Book objects
        private List<Book> ConvertToBooks(List<(int bookId, string title, string author, string desc, double price, int stockQuantity)> raw)
        {
            List<Book> books = new List<Book>();
            foreach (var entry in raw)
            {
                books.Add(new Book(entry.bookId, entry.title, entry.author, entry.desc, entry.price, entry.stockQuantity));
            }
            return books;
        }
    }
}
