using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FavoriteBooks
{
    public class ShoppingCart
    {
        private List<Book> books;
        private List<int> quantities;

        List<double> prices = new List<double>();
        List<double> bookPrices = new List<double>();
        double totalPrice;

        private User user;
        private Database db;


        public ShoppingCart(User user, Database database)
        {
            books = new List<Book>();
            quantities = new List<int>();
            this.user = user;
            this.db = database;
            LoadCart();
        }


        public bool AddBook(Book book, int quantity)
        {
            if (book == null || quantity <= 0)
            {
                return false;
            }

            if (book.GetStockQuantity() < quantity)
            {
                Console.WriteLine(book.GetStockQuantity());
                Console.WriteLine(quantity);
                return false;
            }

            bool bookInBooks = false;
            int index = 0;
            foreach (Book bookTemp in books)
            {
                if (book.GetBookId() == bookTemp.GetBookId())
                {
                    bookInBooks = true;
                    break;
                }
                index++;
            }

            if (bookInBooks)
            {
                quantities[index] += quantity;
            }
            else
            {
                books.Add(book);
                quantities.Add(quantity);
            }

            SaveCart();

            return true;
        }


        public bool RemoveBook(Book book)
        {
            int index = books.IndexOf(book);

            if (index < 0)
            {
                return false;
            }

            books.RemoveAt(index);
            quantities.RemoveAt(index);

            return true;
        }


        public void LoadCart()
        {
            books.Clear();
            quantities.Clear();

            var items = db.readCart(user.UserId);

            foreach (var item in items)
            {
                Book book = new Book(db, item.bookId);
                books.Add(book);
                quantities.Add(item.quantity);
            }
        }


        public void SaveCart()
        {
            db.clearCart(user.UserId);

            for (int i = 0; i < books.Count; i++)
            {
                db.writeCart(user.UserId, books[i].GetBookId(), quantities[i]);
            }
        }


        public Order Checkout()
        {
            int orderId = db.getNextOrderId();

            for (int i = 0; i < books.Count; i++)
            {
                db.writeOrder(orderId, user.UserId, books[i].GetBookId(), quantities[i]);
            }

            Order order = new Order(user, books, quantities);

            ClearCart();

            return order;
        }


        public void ClearCart()
        {
            books.Clear();
            quantities.Clear();
        }


        public List<Book> GetBooks()
        {
            return books;
        }

        public List<double> GetPrices()
        {
            if (prices != null)
            {
                prices.Clear();
            }
            
            foreach (Book book in books)
            {
                prices.Add(book.GetPrice());
            }

            return prices;
        }

        public List<int> GetQuantities()
        {
            return quantities;
        }

        public List<double> GetBookTotals()
        {
            if (bookPrices != null)
            {
                bookPrices.Clear();
            }

            prices = GetPrices();

            for (int i = 0;i < GetPrices().Count; i++)
            {
                bookPrices.Add(prices[i] * quantities[i]);
            }

            return bookPrices;
        }

        public double GetCartTotal()
        {
            return GetBookTotals().Sum();
        }
    }
}
