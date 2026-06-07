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
    public partial class CatalogueUC : UserControl
    {
        UserInterface ui;
        Database db;
        Catalogue catalogue;
        List<Book> selectedBooks;
        List<CatBookUC> catBooks;

        public CatalogueUC(UserInterface userInterface, Database database)
        {
            InitializeComponent();
            ui = userInterface;
            db = database;
            catalogue = new Catalogue(db);
            catBooks = new List<CatBookUC>();
        }

        private void searchBooks(string title, string author)
        {
            selectedBooks = catalogue.SearchBooks(title, author);
        }

        private void displayBooks()
        {
            int index = 0;
            while (index < selectedBooks.Count())
            {
                CatBookUC catBookUC = new CatBookUC(ui, db, selectedBooks[index]);
                catBooks.Add(catBookUC);
                catBookUC.Dock = DockStyle.Fill;
                bookTable.Controls.Add(catBookUC, 0, 0);
                index++;
            }
        }

        public void Clear()
        {
            titleField.Text = "";
            authorField.Text = "";
            bookTable.Controls.Clear();
            searchBooks(titleField.Text, authorField.Text);
            displayBooks();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            bookTable.Controls.Clear();
            searchBooks(titleField.Text, authorField.Text);
            displayBooks();
        }
    }
}
