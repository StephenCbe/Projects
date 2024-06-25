using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CIV.Classess;

namespace CIV
{
    public partial class LoginForm : Form
    {
        private UserAuthenticator authenticator = new UserAuthenticator();
        public LoginForm()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string username = textBoxUserName.Text;
            string password = textBoxPassword.Text;

            if (authenticator.ValidateUser(username, password))
            {
                MessageBox.Show("Login successful!", GlobalFn.FormText);
                if (!SessionManager.CurrentUser.Role.ToLower().Equals("admin"))
                {
                    MessageBox.Show("You are not authorized to see this Report!", GlobalFn.FormText);
                    frmNewSub newSub = new frmNewSub();
                    newSub.Show();
                }
                else
                {
                    // Open the main form or continue to the application
                    frmRevenueReport mainForm = new frmRevenueReport();
                    mainForm.Show();
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password.", GlobalFn.FormText);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            frmSearch search = new frmSearch();
            search.Show();
            search.Location = this.Location;
            

        }
    }
}
