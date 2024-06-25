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
    public partial class NewUserForm : frmBaseForm
    {
        public NewUserForm()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxUserName.Text.Length==0)
            {
                MessageBox.Show("Please enter user name", GlobalFn.FormText);
                return;
            }
            if (textBoxPassword.TextLength == 0) 
            {
                MessageBox.Show("Please enter password", GlobalFn.FormText);
                return;
            }
            if((!radioButtonAdmin.Checked) && (!radioButtonUser.Checked))
            {
                MessageBox.Show("Please select Role: Admin/User", GlobalFn.FormText);
                return;
            }
            string role = string.Empty;
            if (radioButtonAdmin.Checked) {
                role = "Admin";
            }
            if (radioButtonUser.Checked) {
                role = "User";
            }
            UserAuthenticator authenticator = new UserAuthenticator();
            authenticator.CreateUser(textBoxUserName.Text, textBoxPassword.Text, textBoxEmail.Text, role);
            MessageBox.Show("New User has been Added Successfully", GlobalFn.FormText);
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
