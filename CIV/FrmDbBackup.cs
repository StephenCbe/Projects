using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIV.Classess;

namespace CIV
{
    public partial class FrmDbBackup : frmBaseForm
    {
        public FrmDbBackup()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            txtPath.Text += @"\CivBackup.bak";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialogDbPath.ShowDialog();
            txtPath.Text = folderBrowserDialogDbPath.SelectedPath + @"\CivBackup.bak";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            frmNewSub newForm = new frmNewSub();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SQL.MakeDbBackup(txtPath.Text.Trim());
                MessageBox.Show("BackUp Complete !!!");
                button3_Click(null, null);
            }
            catch (Exception eItems)
            {
                MessageBox.Show("Error: " + eItems.Message);
            }
        }
    }
}