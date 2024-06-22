using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIV.Classess;
using System.IO;

namespace CIV
{
    public partial class frmRevenueReport : frmBaseForm
    {
        public frmRevenueReport()
        {
            InitializeComponent();

            if (!SessionManager.CurrentUser.IsAuthenticated)
            {
                MessageBox.Show("User is not authenticated.");
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("User is authenticated.");
                //lblUsername.Text = "Welcome, " + SessionManager.CurrentUser.Username;
                // Additional setup based on the user's role
            }

            this.Text += " - " + GlobalFn.FormText;
            dtpStart.CustomFormat = "dd/MM/yyyy";
            dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

            dtpEnd.CustomFormat = "dd/MM/yyyy";
            dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        
            BindLanguages();
            BuildGrid();
        }

        private void BindLanguages()
        {
            try
            {
                cboMagazine.ValueMember = "language_id";
                cboMagazine.DisplayMember = "mag_name";
                cboMagazine.DataSource = SQL.RevenueReportGetLanguages().Tables[0];
            }
            catch (Exception eItems)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eItems, "Error in Binding languages in frmRevenueReport.cs");
                return;
            }
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            dgvReceipts.AutoGenerateColumns = false;
            btnRun.Enabled = false;
            btnRun.BackColor = Color.Yellow;
           
            if (rdoLocal.Checked)
                machingType = "Local";
            if (rdoAll.Checked)
                machingType = "All";
            try
            {
                DataTable oTable = SQL.RevenueReportGetRecs(dtpStart.Value, dtpEnd.Value, cboMagazine.SelectedValue.ToString(), machingType).Tables[0];
                dgvReceipts.DataSource = oTable;
                TurnOnRuntBtn();
            }
            catch (Exception eRpt)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eRpt, "Error in executing SQL.RevenueReportGetRecs in frmRevenueReport.cs");
                TurnOnRuntBtn();
                return;
            }
        }
        string machingType = "";     
        private void BuildGrid()
        {
            dgvReceipts.AutoGenerateColumns = false;
            btnRun.Enabled = false;
            btnRun.BackColor = Color.Yellow;
           
            if (rdoLocal.Checked)
                machingType = "Local";
            if (rdoAll.Checked)
                machingType = "All";
            try
            {
                DataGridViewTextBoxColumn cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "payment_date_fmt";
                cs.HeaderText = "Date";
                cs.Width = 50;
                dgvReceipts.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "civ";
                cs.HeaderText = "Christ is Victor";
                cs.Width = 150;
                dgvReceipts.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "jc";
                cs.HeaderText = "Jeya Christu";
                cs.Width = 150;
                dgvReceipts.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "kv";
                cs.HeaderText = "Kreestu Vijayamu";
                cs.Width = 150;
                dgvReceipts.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "mjk";
                cs.HeaderText = "Mritunjay Khrist";
                cs.Width = 150;
                dgvReceipts.Columns.Add(cs);

                DataTable oTable = SQL.RevenueReportGetRecs(dtpStart.Value, dtpEnd.Value, cboMagazine.SelectedValue.ToString(), machingType).Tables[0];
                dgvReceipts.DataSource = oTable;
                TurnOnRuntBtn();
            }
            catch (Exception eRpt)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eRpt, "Error in executing SQL.RevenueReportGetRecs in frmRevenueReport.cs");
                TurnOnRuntBtn();
                return;
            }
        }
        private void TurnOnRuntBtn()
        {
            btnRun.Enabled = true;
            btnRun.BackColor = Color.Empty;
        }
      
        private void TurnOnExportBtn()
		{
			btnExportToExcel.Enabled = true ;
			btnExportToExcel.BackColor = Color.Empty; 
		}
        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            btnExportToExcel.Enabled = false ;
			btnExportToExcel.BackColor = Color.Yellow;

			string currentDate = DateTime.Now .ToString("yyyyMMdd");
			string reportFilePath = GlobalFn.ReportsDir + "revenue_"+ currentDate + ".txt";
			
			DataSet ds = new DataSet();
			StringBuilder sb = new StringBuilder();

			try
			{
				ds = SQL.RevenueReportGetRecs(dtpStart.Value, dtpEnd.Value, cboMagazine.SelectedValue.ToString(),machingType);
			}
			catch(Exception eSR)
			{
				MessageBox.Show("DataBase Error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eSR,"Error occured while executing RevenueReportGetRecs method in RevenueReport.cs");
				TurnOnExportBtn();
				return;
			}
			int iCol = 0;
			for(iCol = 0; iCol < ds.Tables[0].Columns.Count; iCol++)
			{
				if(iCol == ds.Tables[0].Columns.Count - 1)
				{
					sb.AppendFormat("{0}",ds.Tables[0].Columns[iCol].ColumnName);
					sb.Append("\r\n");
				}
				else
					sb.AppendFormat("{0}\t",ds.Tables[0].Columns[iCol].ColumnName);
			}
			
			foreach (DataRow row in ds.Tables[0].Rows)
			{
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\r\n", row["payment_date_fmt"], row["civ"],row["jc"],row["kv"]);
			}
			try
			{
				FileStream fs = new FileStream(reportFilePath,FileMode.Create,FileAccess.ReadWrite,FileShare.Write);
				byte[] fsCon = System.Text.Encoding.ASCII.GetBytes(sb.ToString());
				fs.Write(fsCon,0,fsCon.Length);
				fs.Close();
			}
			catch(Exception efs)
			{
				MessageBox.Show("Error in Exporting ...",GlobalFn.FormText);
				GlobalFn.ProcessException(efs,"Error occured in writing the file in RevenueReport.cs");
				TurnOnExportBtn();
				return;
			}
			lblFilePath.Text = "Report File: " + reportFilePath;
			MessageBox.Show("Exported to Excel done!",GlobalFn.FormText);
			TurnOnExportBtn();
		}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        
    }
}