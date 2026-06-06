using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteBooks
{
    public class Book
    {
        Database db;
        
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
        public Book(Database database, int bookId)
        {
            this.db = database;

            var bookInfo = db.readBook(bookId)[0];
            
            this.bookId = bookId;
            this.title = bookInfo.title;
            this.author = bookInfo.author;
            this.description = bookInfo.desc;
            this.price = bookInfo.price;
            this.stockQuantity = bookInfo.stockQuantity;
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
            return $"{title} by {author}";
        }
    }
}
