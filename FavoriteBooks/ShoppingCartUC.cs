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
    public partial class ShoppingCartUC : UserControl
    {
        UserInterface ui;
        User user;
        ShoppingCart cart;
        List<Book> books;

        string errorMsg;
        
        public ShoppingCartUC(UserInterface userInterface)
        {
            InitializeComponent();
            ui = userInterface;
        }

        public void DisplayItems()
        {
            itemListBox.DataSource = null;
            priceListBox.DataSource = null;
            qtyListBox.DataSource = null;
            itemTotalListBox.DataSource = null;
            cartTotalLbl.Text = "";

            errorLbl.Text = "";

            itemListBox.Items.Clear();
            qtyListBox.Items.Clear();
            
            user = ui.getCurrentUser();
            if (user != null)
            {
                cart = user.GetCart();

                books = cart.GetBooks();
                
                itemListBox.DataSource = cart.GetBooks();
                priceListBox.DataSource = cart.GetPrices();
                qtyListBox.DataSource = cart.GetQuantities();
                itemTotalListBox.DataSource = cart.GetBookTotals();
                cartTotalLbl.Text = cart.GetCartTotal().ToString();
            }
        }

        private bool verifyCart()
        {
            user = ui.getCurrentUser();
            if (user != null) 
            {
                cart = user.GetCart();
            }

            bool validCart = true;
            errorMsg = "";

            if (user == null)
            {
                errorMsg += "Must be signed in to make a purchase\n";
                validCart = false;
            }
            else if (cart.GetBooks().Count() == 0)
            {
                errorMsg += "Cart is empty\n";
                validCart = false;
            }

            return validCart;
        }

        private void checkOutBtn_Click(object sender, EventArgs e)
        {
            errorLbl.ForeColor = Color.Red;
            
            if (verifyCart())
            {
                cart.Checkout();
                DisplayItems();
                errorLbl.ForeColor = Color.Green;
                errorMsg = "Checkout successful";
            }

            errorLbl.Text = errorMsg;
        }
    }
}
