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
    public partial class frmDeleteSubscriber : frmBaseForm
    {
        DataTable oTable;
        public frmDeleteSubscriber()
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
                oTable = SQL.DeleteSubscriberGetRec("-1", cboMagazine.SelectedValue.ToString()).Tables[0];
                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = oTable.TableName;
                ts.HeaderFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                ts.AlternatingBackColor = Color.SkyBlue;

                GridColumnStylesCollection ColumnStyles = ts.GridColumnStyles;

                DataGridColumnStyle cs = new DataGridTextBoxColumn();

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "sub_code";
                cs.HeaderText = "Sub. Code";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "sub_name";
                cs.HeaderText = "Name";
                cs.Width = 150;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "address_line1";
                cs.HeaderText = "Address 1";
                cs.Width = 150;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "city";
                cs.HeaderText = "City";
                cs.Width = 150;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "status";
                cs.HeaderText = "Status";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "amount_paid";
                cs.HeaderText = "Amount Paid";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                GridTableStylesCollection tableStyles = dgDeleteSub.TableStyles;
                tableStyles.Add(ts);

                dgDeleteSub.DataSource = oTable;
            }
            catch (Exception eItems)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eItems, "exception in Bind Delete Subscriber");
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
                oTable = SQL.DeleteSubscriberGetRec(txtSubCode.Text, cboMagazine.SelectedValue.ToString()).Tables[0];
                dgDeleteSub.DataSource = oTable;
                if (oTable.Rows.Count == 0)
                {
                    MessageBox.Show("No Records Found!", GlobalFn.FormText,MessageBoxButtons.OK);
                    return;
                }
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
                GlobalFn.ProcessException(eItems, "Error in Binding Items list box in Delete Subscriber.cs");
                return;
            }
        }

        private void dgDeleteSub_MouseUp(object sender, MouseEventArgs e)
        {
            System.Drawing.Point pt = new Point(e.X, e.Y);
            DataGrid.HitTestInfo hti = dgDeleteSub.HitTest(pt);

            if (hti.Type == DataGrid.HitTestType.Cell)
            {
                if (hti.Column == 0)
                {
                    if (MessageBox.Show("Do you really want to delete this Subscriber?", GlobalFn.FormText, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            int rtrn = SQL.DeleteSubDelete(txtSubCode.Text, cboMagazine.SelectedValue.ToString());
                            if (rtrn == -2)
                            {
                                MessageBox.Show("This subscriber is active and cannot be deleted!", GlobalFn.FormText);
                                return;
                            }
                            MessageBox.Show("Subscriber has been deleted successfully!", GlobalFn.FormText);

                        }
                        catch (Exception eBill)
                        {
                            MessageBox.Show("Database error...", GlobalFn.FormText);
                            GlobalFn.ProcessException(eBill, "Error in deleting subscriber in DeleteSubscriber.cs");
                            return;
                        }

                    }
                }
            }
        }

        
    }
}