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
    public partial class UserInterface : Form
    {
        CatalogueUC catalogueUC;
        ShoppingCartUC shoppingCartUC;
        RegisterUC registerUC;
        LogInUC logInUC;

        Database db;
        User currentUser;

        public UserInterface()
        {
            InitializeComponent();

            currentUser = null;

            db = new Database();

            userLbl.Text = "Current user: Guest";

            catalogueUC = new CatalogueUC(this, db);
            catalogueUC.Dock = DockStyle.Fill;
            userControlPanel.Controls.Add(catalogueUC);

            shoppingCartUC = new ShoppingCartUC(this);
            shoppingCartUC.Name = "shoppingCartUC";
            shoppingCartUC.Dock = DockStyle.Fill;
            userControlPanel.Controls.Add(shoppingCartUC);

            registerUC = new RegisterUC(this, db);
            registerUC.Dock = DockStyle.Fill;
            userControlPanel.Controls.Add(registerUC);

            logInUC = new LogInUC(this, db);
            logInUC.Dock = DockStyle.Fill;
            userControlPanel.Controls.Add(logInUC);

            catalogueUC.Clear();
            catalogueUC.BringToFront();
        }

        public User getCurrentUser()
        {
            return currentUser;
        }
        
        public void changeUser(string email)
        {
            var userInfo = db.readUser(email);
            currentUser = new User(db, userInfo.userId, userInfo.fName, userInfo.lName, userInfo.email, userInfo.password, userInfo.role);
            userLbl.Text = "Current user: " + currentUser.FirstName + " " + currentUser.LastName;
        }

        private void logOut()
        {
            currentUser = null;
            userLbl.Text = "Current user: Guest";
        }

        private void catalogueBtn_Click(object sender, EventArgs e)
        {
            catalogueUC.Clear();
            catalogueUC.BringToFront();
        }

        private void cartBtn_Click(object sender, EventArgs e)
        {
            shoppingCartUC.DisplayItems();
            shoppingCartUC.BringToFront();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            registerUC.BringToFront();
        }

        private void logInBtn_Click(object sender, EventArgs e)
        {
            logInUC.BringToFront();
        }

        private void logOutBtn_Click(object sender, EventArgs e)
        {
            if (currentUser != null)
            {
                logOut();
                catalogueUC.BringToFront();
            }
        }
    }
}
