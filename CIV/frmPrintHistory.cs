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
    public partial class frmPrintHistory : frmBaseForm
    {
        public frmPrintHistory()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            BindLanguages();
            if (cboMagazine.Items.Count > 0)
                cboMagazine.SelectedIndex = 0;

            BuildGrid();
        }

        private void BindLanguages()
        {
            try
            {
                cboMagazine.ValueMember = "language_id";
                cboMagazine.DisplayMember = "mag_name";
                cboMagazine.DataSource = SQL.SubscribersGetLanguages().Tables[0];
            }
            catch (Exception eItems)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eItems, "Error in Binding Items list box in NewSubscriptions.cs");
                return;
            }
        }

        private void BuildGrid()
        {
            try
            {
                DataGridViewColumn cs;

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "subscriber_print_id";
                cs.HeaderText = "Print ID";
                cs.Width = 100;
                dgvSubPrintHistory.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "print_date";
                cs.HeaderText = "Print Date";
                cs.Width = 150;
                dgvSubPrintHistory.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "machine_name";
                cs.HeaderText = "Machine Name";
                cs.Width = 150;
                dgvSubPrintHistory.Columns.Add(cs);

            }
            catch (Exception eItems)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eItems, "exception in Bind payment history");
                return;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                ds = SQL.GetSubPrintHistory(GlobalFn.FixQuotes(txtSubCode.Text.Trim()), cboMagazine.SelectedValue.ToString());
                dgvSubPrintHistory.DataSource = ds.Tables[0];

                if (ds.Tables[0].Rows.Count == 0)
                    MessageBox.Show("No Records Found!!!",GlobalFn.FormText);
            }
            catch (Exception eItems)
            {
                MessageBox.Show("DatabaseError: " + eItems.Message);
                GlobalFn.ProcessException(eItems, "Error in getSubPrintHistory");
            }
        }
    }
}