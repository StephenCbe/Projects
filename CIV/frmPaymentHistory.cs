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
    public partial class frmPaymentHistory : frmBaseForm
    {
        DataTable oTable;
        public frmPaymentHistory()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            BindLanguages();
            BuildGrid();
        }
        private void BuildGrid()
        {
            try
            {
                oTable = SQL.GetPaymentHistory("-1", cboMagazine.SelectedValue.ToString()).Tables[0];
                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = oTable.TableName;
                ts.HeaderFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                ts.AlternatingBackColor = Color.SkyBlue;

                GridColumnStylesCollection ColumnStyles = ts.GridColumnStyles;

                DataGridColumnStyle cs = new DataGridTextBoxColumn();

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "receipt_id";
                cs.HeaderText = "Receipt ID";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "payment_date";
                cs.HeaderText = "Payment Date";
                cs.Width = 150;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "bill_num";
                cs.HeaderText = "Bill No.";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "amount";
                cs.HeaderText = "Amount" + "    |";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "created_on";
                cs.HeaderText = "Created On";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "last_mod_on";
                cs.HeaderText = "Last Mod On";
                cs.Width = 100;
                ColumnStyles.Add(cs);


                GridTableStylesCollection tableStyles = dgPaymentHistory.TableStyles;
                tableStyles.Add(ts);

                dgPaymentHistory.DataSource = oTable;
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
            if (txtSubCode.TextLength == 0)
            {
                MessageBox.Show("Please enter subscription Code", GlobalFn.FormText);
                return;
            }
            try
            {
                oTable = SQL.GetPaymentHistory(txtSubCode.Text, cboMagazine.SelectedValue.ToString()).Tables[0];
                dgPaymentHistory.DataSource = oTable;
            }
            catch (Exception eItems)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eItems, "exception in Bind payment history");
                return;
            }
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

        private void dgPaymentHistory_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            System.Drawing.Point pt = new Point(e.X, e.Y);
            DataGrid.HitTestInfo hti = dgPaymentHistory.HitTest(pt);

            //if (hti.Type == DataGrid.HitTestType.Cell)
            //{
            //    if (hti.Column == 2)
            //    {
            //        frmRenewal objRenew = new frmRenewal(dgSearch[dgSearch.CurrentRowIndex,0].ToString());
            //        objRenew.ShowDialog();
            //    }
            //}

            if (hti.Type == DataGrid.HitTestType.Cell)
            {
                if (hti.Column == 0)
                {
                    string receiptId = dgPaymentHistory[dgPaymentHistory.CurrentRowIndex, 0].ToString();

                    if (MessageBox.Show("Do you really want to delete this Bill with receiptID: " + receiptId + " ?", GlobalFn.FormText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            //SQL.PaymentHistoryBillCancel(receiptId);
                            SQL.CancelReceipt(Convert.ToInt32(receiptId));
                        }
                        catch (Exception eBill)
                        {
                            MessageBox.Show("Database error...", GlobalFn.FormText);
                            GlobalFn.ProcessException(eBill, "Error in cancelling bill in PaymentHistory.cs");
                            return;
                        }
                        try
                        {
                            oTable = SQL.GetPaymentHistory(txtSubCode.Text, cboMagazine.SelectedValue.ToString()).Tables[0];
                            dgPaymentHistory.DataSource = oTable;
                        }
                        catch (Exception eItems)
                        {
                            MessageBox.Show("Database error...", GlobalFn.FormText);
                            GlobalFn.ProcessException(eItems, "exception in Bind payment history");
                            return;
                        }
                    }
                }
            }
        }
     
       
    }
}