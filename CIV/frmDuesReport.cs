using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIV.Classess;
using System.Collections;
using System.IO;

namespace CIV
{
    
    public partial class frmDuesReport : frmBaseForm
    {
        Object[] expiryDate = new Object[2];
        ArrayList al = new ArrayList();
        DataTable oTable;
        string subscriberList = "";
        public frmDuesReport()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;


            BindLanguages();
            if (cboMagazine.Items.Count > 0)
                cboMagazine.SelectedIndex = 0;
            if (cboExpiry.Items.Count > 0)
                cboExpiry.SelectedIndex = 0;

            try
            {
                oTable = SQL.DuesReportGetAddressList("-1").Tables[0];
            }
            catch (Exception eDr)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eDr, "Error in Getting Address List in frmDuesReport.cs");
                return;
            }
            if (!oTable.Columns.Contains("seq_ID"))
            {
                DataColumn dc = new DataColumn("seq_ID", Type.GetType("System.Int16"));
                oTable.Columns.Add(dc);
            }
            if (!oTable.Columns.Contains("due"))
            {
                DataColumn dc = new DataColumn("due", Type.GetType("System.Double"));
                oTable.Columns.Add(dc);
            }
            if (!oTable.Columns.Contains("expiry_date"))
            {
                DataColumn dc = new DataColumn("expiry_date", Type.GetType("System.DateTime"));
                oTable.Columns.Add(dc);
            }

            int i = 0;
            foreach (DataRow dr in oTable.Rows)
            {
               dr["seq_ID"] = ++i;
               dr["due"] = 0.0;
               dr["expiry_date"] = "";
            }
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
                GlobalFn.ProcessException(eItems, "Error in Binding Language in frmDuesReport.cs");
                return;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            string dueDate = "";
            string today = DateTime.Today.ToString("yyyyMMdd");
            btnRun.Enabled = false;
            
            bool firstSub = false;
            string expSel = cboExpiry.SelectedItem.ToString();

            int dateDiff=0;
            GlobalFn.StatusDisplayStart("Processing.. Please wait!");
            DataSet ds = SQL.DuesReportGetSubscribers(cboMagazine.SelectedValue.ToString(), "", true);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                expiryDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["Start_Date"]), Convert.ToDouble(dr["Amount_Paid"]),Convert.ToDouble(dr["discount"]),Convert.ToInt32(dr["num_copies"]));
                dueDate = Convert.ToDateTime(expiryDate[0]).ToString("yyyyMMdd");
                dateDiff = SQL.GetDateDiff(dueDate, today);
                if (expSel.Equals("Two Months"))
                {
                    if (dateDiff == 60)
                    {
                        al.Add(dr["subscriber_id"]);
                    }
                    
                }
                else if (expSel.Equals("Expired"))
                {
                    if (dateDiff < 0)
                    {
                        al.Add(dr["subscriber_id"]);
                    }

                }
                else
                {
                    if ((dateDiff < 60) && (dateDiff >= 0))
                    {
                        al.Add(dr["subscriber_id"]);
                    }
                }
            }
            if (al.Count == 0)
            {
                MessageBox.Show("No Records Found!", GlobalFn.FormText);
                return;
            }
            IEnumerator iEnum = al.GetEnumerator();
            while (iEnum.MoveNext())
            {
                if (firstSub)
                    subscriberList = subscriberList + ",";
                subscriberList = subscriberList + iEnum.Current.ToString();
                firstSub = true;
            }
            try
            {
                oTable.Rows.Clear();

                oTable = SQL.DuesReportGetAddressList(subscriberList).Tables[0];
                if (!oTable.Columns.Contains("seq_ID"))
                {
                    DataColumn dc = new DataColumn("seq_ID", Type.GetType("System.Int16"));
                    oTable.Columns.Add(dc);
                }
                if (!oTable.Columns.Contains("due"))
                {
                    DataColumn dc = new DataColumn("due", Type.GetType("System.Double"));
                    oTable.Columns.Add(dc);
                }
                if (!oTable.Columns.Contains("expiry_date"))
                {
                    DataColumn dc = new DataColumn("expiry_date", Type.GetType("System.DateTime"));
                    oTable.Columns.Add(dc);
                }

                int i = 0;
                foreach (DataRow dr in oTable.Rows)
                {
                    dr["seq_ID"] = ++i;
                   expiryDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["Start_Date"]), Convert.ToDouble(dr["Amount_Paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt32(dr["num_copies"]));
                   dr["due"] = Convert.ToDouble(expiryDate[1]);
                   dr["expiry_date"] = Convert.ToDateTime(expiryDate[0]);
                   
                }
                
                dgvSubs.DataSource = oTable;
            }
            catch (Exception eAl)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eAl, "Error in Getting Address List in frmDuesReport.cs");
                btnRun.Enabled = true;
                GlobalFn.StatusDisplayStop();
                return;
            }
            GlobalFn.StatusDisplayStop();
            btnRun.Enabled = true;
        }
        private void BuildGrid()
        {
            try
            {
                dgvSubs.AutoGenerateColumns = false;
                
                DataGridViewTextBoxColumn cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "seq_ID";
                cs.HeaderText = "#.";
                cs.Width = 25;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "mag_name";
                cs.HeaderText = "Magazine";
                cs.Width = 100;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "sub_code";
                cs.HeaderText = "Sub Code";
                cs.Width = 50;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "title";
                cs.HeaderText = "Title";
                cs.Width = 50;
                dgvSubs.Columns.Add(cs);
                
                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "name";
                cs.HeaderText = "Subscriber";
                cs.Width = 150;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "address_line1";
                cs.HeaderText = "Address Line1";
                cs.Width = 200;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "address_line2";
                cs.HeaderText = "Address Line2";
                cs.Width = 200;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "address_line3";
                cs.HeaderText = "Address Line3";
                cs.Width = 200;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "city";
                cs.HeaderText = "City";
                cs.Width = 100;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "district";
                cs.HeaderText = "District";
                cs.Width = 75;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "state";
                cs.HeaderText = "State";
                cs.Width = 100;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "pin_code";
                cs.HeaderText = "Pin Code";
                cs.Width = 75;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "due";
                cs.HeaderText = "Due";
                cs.Width = 75;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "num_copies";
                cs.HeaderText = "# Copies";
                cs.Width = 25;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "start_date";
                cs.HeaderText = "Start Date";
                cs.Width = 100;
                dgvSubs.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "expiry_date";
                cs.HeaderText = "Expiry Date";
                cs.Width = 100;
                dgvSubs.Columns.Add(cs);

                dgvSubs.DataSource = oTable;
            }
            catch (Exception eGrid)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eGrid, "Error in Binding Data Grid in frmDuesPrint.cs");
                return;
            }

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            btnExport.BackColor = Color.Yellow;

            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string reportFilePath = GlobalFn.ReportsDir + "DuesReport_" + currentDate + ".txt";

            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();

            try
            {
                ds = SQL.DuesReportGetAddressList(subscriberList);
            }
            catch (Exception eSR)
            {
                MessageBox.Show("DataBase Error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eSR, "Error occured while executing DuesReportGetAddressList method in NewSubReport.cs");
                TurnOnExportBtn();
                return;
            }
            int iCol = 0;
            for (iCol = 0; iCol < ds.Tables[0].Columns.Count; iCol++)
            {
                if (iCol == ds.Tables[0].Columns.Count - 1)
                {
                    sb.AppendFormat("{0}", ds.Tables[0].Columns[iCol].ColumnName);
                    sb.Append("\r\n");
                }
                else
                    sb.AppendFormat("{0}\t", ds.Tables[0].Columns[iCol].ColumnName);
            }

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\r\n", row["start_date_fmt"], row["civ"], row["jc"], row["kv"]);
            }
            try
            {
                FileStream fs = new FileStream(reportFilePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Write);
                byte[] fsCon = System.Text.Encoding.ASCII.GetBytes(sb.ToString());
                fs.Write(fsCon, 0, fsCon.Length);
                fs.Close();
            }
            catch (Exception efs)
            {
                MessageBox.Show("Error in Exporting ...", GlobalFn.FormText);
                GlobalFn.ProcessException(efs, "Error occured in writing the file in NewSubReport.cs");
                TurnOnExportBtn();
                return;
            }
            lblFilePath.Text = "Report File: " + reportFilePath;
            MessageBox.Show("Export to Text done!", GlobalFn.FormText);
            TurnOnExportBtn();
        }
        private void TurnOnExportBtn()
        {
            btnExport.Enabled = true;
            btnExport.BackColor = Color.Empty;
        }
    }
}