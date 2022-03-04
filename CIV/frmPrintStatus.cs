using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CIV.Classess;


namespace CIV
{

    public partial class frmPrintStatus : Form
    {
        int maxSeq;
        DataTable oTable;
        public frmPrintStatus(DataSet ds)
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            oTable = ds.Tables[0];

            toolTip1.InitialDelay = 50;
            toolTip1.AutoPopDelay = 10000;
            toolTip1.ReshowDelay = 10;
            toolTip1.Active = false;
            
            if (!oTable.Columns.Contains("is_printed"))
            {
                DataColumn dc = new DataColumn("is_printed", Type.GetType("System.Boolean"));
                oTable.Columns.Add(dc);
            }

            if (!oTable.Columns.Contains("seq_ID"))
            {
                DataColumn dc = new DataColumn("seq_ID", Type.GetType("System.Int16"));
                //dc.Unique = true;
                oTable.Columns.Add(dc);
            }
            
            int i = 0;
            foreach (DataRow dr in oTable.Rows)
            {
                dr["is_Printed"] = true;
                dr["seq_ID"] = ++i;

            }
            maxSeq = i;

            BuildGrid();
        }

        private void BuildGrid()
        {
            try
            {
                DataGridViewTextBoxColumn cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "seq_ID";
                cs.HeaderText = "Sequence ID";
                cs.Width = 50;
                dgvPrint.Columns.Add(cs);

                DataGridViewCheckBoxColumn cb = new DataGridViewCheckBoxColumn();
                cb.DataPropertyName = "is_Printed";
                cb.HeaderText = "Printed";
                cb.Width = 50;
                dgvPrint.Columns.Add(cb);
                    
                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "mag_name";
                cs.HeaderText = "Magazine";
                cs.Width = 100;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "sub_code";
                cs.HeaderText = "Subscriber Code";
                cs.Width = 50;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "last_name";
                cs.HeaderText = "Last Name";
                cs.Width = 75;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "first_name";
                cs.HeaderText = "First Name";
                cs.Width = 150;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "address_line1";
                cs.HeaderText = "Address Line1";
                cs.Width = 200;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "address_line2";
                cs.HeaderText = "Address Line2";
                cs.Width = 200;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "city";
                cs.HeaderText = "City";
                cs.Width = 100;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "district";
                cs.HeaderText = "District";
                cs.Width = 75;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "state";
                cs.HeaderText = "State";
                cs.Width = 50;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "pin_code";
                cs.HeaderText = "Pin Code";
                cs.Width = 75;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "subscriber_id";
                cs.HeaderText = "Subscriber ID";
                cs.Visible = false;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "address_line3";
                cs.Visible = false;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "language_id";
                cs.Visible = false;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "title";
                cs.Visible = false;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "start_date";
                cs.Visible = false;
                dgvPrint.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "amount_paid";
                cs.Visible = false;
                dgvPrint.Columns.Add(cs);

                dgvPrint.DataSource = oTable;
            }
            catch (Exception eGrid)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eGrid, "Error in Binding Data Grid in frmPrintStatus.cs");
                return;
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isPrinted = false;
            btnSave.Enabled = false;
            foreach (DataRow row in oTable.Rows)
            {
                try
                {
                    isPrinted = Convert.ToBoolean(row["is_printed"]);
                }
                catch (Exception ePrint)
                {
                    MessageBox.Show(ePrint.Message);
                    return;
                }
                if (isPrinted)
                {
                    // update status in Subscriber print table
                    try
                    {
                        SQL.PrintStatusUpdateStaus(row["subscriber_id"].ToString());
                    }
                    catch (Exception eUpd)
                    {
                        MessageBox.Show(eUpd.Message, GlobalFn.FormText);
                        GlobalFn.ProcessException(eUpd, "Error in Updating Subscriber print in frmPrintStatus.cs");
                        btnSave.Enabled = true;
                    }
                }
            }
            DeletePrintStatus();
            frmPrintStatus.ActiveForm.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //delete all records that have status 0?
            DeletePrintStatus();
            frmPrintStatus.ActiveForm.Close();
        }
        private void DeletePrintStatus()
        {
            try
            {
                SQL.PrintStatusDeleteRec();
            }
            catch (Exception eDel)
            {
                MessageBox.Show(eDel.Message, GlobalFn.FormText);
                GlobalFn.ProcessException(eDel, "Error in deleting Subscriber print in frmPrintStatus.cs");
                return;
            }
        }

        private void btnUncheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalFn.IsNumeric(txtFrom.Text) || !GlobalFn.IsNumeric(txtTo.Text))
                {
                    MessageBox.Show("Error!!... uncheck from must be a Number",GlobalFn.FormText);
                    return;
                }

                int uncheckFrom = Convert.ToInt16(txtFrom.Text);
                int uncheckTo = Convert.ToInt16(txtTo.Text);

                if ((uncheckFrom > uncheckTo) || (uncheckFrom > maxSeq || uncheckTo > maxSeq))
                {
                    MessageBox.Show("Error!!... uncheck from Invalid", GlobalFn.FormText);
                    return;
                }

                foreach (DataRow dr in oTable.Rows)
                {
                    if (Convert.ToInt16(dr["seq_ID"]) - uncheckFrom >= 0 && Convert.ToInt16(dr["seq_ID"]) - uncheckTo <= 0)
                    {
                        dr["is_Printed"] = false;
                    }
                }
                dgvPrint.DataSource = oTable;
            }

            catch (Exception ex)
            {
                GlobalFn.ProcessException(ex, "Exception in Uncheck isPrinted");
                return;
            }
        }

        private void txtFrom_MouseMove(object sender, MouseEventArgs e)
        {            
            toolTip1.SetToolTip(txtFrom, "Enter the Sequence ID range to uncheck.");
            toolTip1.Active = true;
        }

    }

}