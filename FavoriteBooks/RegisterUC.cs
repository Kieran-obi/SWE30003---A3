using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FavoriteBooks
{
    public partial class RegisterUC : UserControl
    {
        Database db;
        UserInterface ui;
        string errorMsg;
        bool validReg;

        string fNameInput;
        string lNameInput;
        string emailInput;
        string passwordInput;
        string roleInput;

        public RegisterUC(UserInterface userInterface, Database database)
        {
            InitializeComponent();
            ui = userInterface;
            db = database;
            errorMsg = "";
            validReg = false;
        }

        private bool verifyRegistration()
        {
            validReg = true;
            errorMsg = "";

            fNameInput = firstNameField.Text;
            lNameInput = lastNameField.Text;
            emailInput = emailField.Text;
            passwordInput = passwordField.Text;
            if (custRadBtn.Checked)
            {
                roleInput = "Customer";
            } 
            else if (adminRadBtn.Checked)
            {
                roleInput = "Administrator";
            }

            if (fNameInput == "")
            {
                errorMsg += "Please enter first name\n";
                validReg = false;
            }
            if (lNameInput == "")
            {
                errorMsg += "Please enter last name\n";
                validReg = false;
            }
            if (emailInput == "")
            {
                errorMsg += "Please enter email\n";
                validReg = false;
            } else if (db.readUser(emailInput).userId != "")
            {
                errorMsg += "Email already in use\n";
                validReg = false;
            }
            if (passwordInput == "")
            {
                errorMsg += "Please enter password\n";
                validReg = false;
            }
            return validReg;
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            if (verifyRegistration())
            {
                db.writeUser(Guid.NewGuid().ToString(), fNameInput, lNameInput, emailInput, passwordInput, roleInput);
                errorLbl.ForeColor = Color.Green;
                errorLbl.Text = "New user has been successfully created";
            }
            else
            {
                errorLbl.ForeColor = Color.Red;
                errorLbl.Text = errorMsg;
            }
        }
    }
}
