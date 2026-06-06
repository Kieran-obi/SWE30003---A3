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
    public partial class LogInUC : UserControl
    {
        Database db;
        UserInterface ui;
        string errorMsg;
        bool validLogin;

        string emailInput;
        string passwordInput;

        public LogInUC(UserInterface userInterface, Database database)
        {
            InitializeComponent();
            ui = userInterface;
            db = database;
        }

        private bool verifyLogin()
        {
            validLogin = true;
            errorMsg = "";

            emailInput = emailField.Text;
            passwordInput = passwordField.Text;
            if (emailInput == "")
            {
                errorMsg += "Please enter email\n";
                validLogin = false;
            }
            else if (db.readUser(emailInput).userId == "")
            {
                errorMsg += "Email not registered\n";
                validLogin = false;
            }
            else if (passwordInput == "")
            {
                errorMsg += "Please enter password\n";
                validLogin = false;
            }
            else if (db.readUser(emailInput).password != passwordInput)
            {
                errorMsg += "Incorrect password\n";
                validLogin = false;
            }
            return validLogin;
        }

        private void logInBtn_Click(object sender, EventArgs e)
        {
            if (verifyLogin())
            {
                ui.changeUser(emailInput);
                errorLbl.ForeColor = Color.Green;
                errorLbl.Text = "User logged in";
            }
            else
            {
                errorLbl.ForeColor = Color.Red;
                errorLbl.Text = errorMsg;
            }
        }
    }
}
