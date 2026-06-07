using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FavoriteBooks
{
    public partial class CatBookUC : UserControl
    {
        UserInterface ui;
        Database db;
        Book book;
        User user;
        ShoppingCart cart;

        public CatBookUC(UserInterface userInterface, Database database, Book book)
        {
            InitializeComponent();
            this.ui = userInterface;
            this.db = database;
            this.book = book;
            titleLabel.Text = book.GetTitle();
            authorLabel.Text = book.GetAuthor();
        }

        private void addToCartBtn_Click(object sender, EventArgs e)
        {
            user = ui.getCurrentUser();
            if (user != null)
            {
                cart = user.GetCart();
                cart.AddBook(book, 1);
                cart.SaveCart();
            }
        }
    }
}
