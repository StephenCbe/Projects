using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CIV.Classess;
using System.Data;

namespace CIV
{
	/// <summary>
	/// Summary description for ExpiryReport.
	/// </summary>
	public class frmExpiryReport : System.Windows.Forms.Form
	{
        private System.Windows.Forms.GroupBox gbExpiry;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private DataGrid dgExpiry;
        DataTable oTable = new DataTable("expiryList");

		public frmExpiryReport()
		{
			InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            
            BuildGrid();
		}

        private void BuildGrid()
        {
            try
            {
                oTable = SQL.FindSubscribers("-1", "", "", "", "1","","-1").Tables[0];

                DataGridTableStyle ts = new DataGridTableStyle();
                ts.MappingName = oTable.TableName;
                ts.HeaderFont = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
                ts.AlternatingBackColor = Color.SkyBlue;

                GridColumnStylesCollection ColumnStyles = ts.GridColumnStyles;

                DataGridColumnStyle cs = new DataGridTextBoxColumn();

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "subscriber_id";
                cs.HeaderText = "Subscriber ID";
                cs.Width = 0;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "sub_code";
                cs.HeaderText = "Subscriber Code";
                cs.Width = 50;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "last_name";
                cs.HeaderText = "Last Name";
                cs.Width = 75;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "first_name";
                cs.HeaderText = "First Name";
                cs.Width = 150;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "address_line1";
                cs.HeaderText = "Address Line1";
                cs.Width = 200;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "city";
                cs.HeaderText = "City";
                cs.Width = 100;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "district";
                cs.HeaderText = "District";
                cs.Width = 75;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "state_name";
                cs.HeaderText = "State";
                cs.Width = 50;
                ColumnStyles.Add(cs);

                cs = new DataGridTextBoxColumn();
                cs.MappingName = "pin_code";
                cs.HeaderText = "Pin Code";
                cs.Width = 75;
                ColumnStyles.Add(cs);

                GridTableStylesCollection tableStyles = dgExpiry.TableStyles;
                tableStyles.Add(ts);

                dgExpiry.DataSource = oTable;
            }
            catch (Exception eGrid)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eGrid, "Error in Binding Data Grid in ExpiryReport.cs");
                return;
            }

        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gbExpiry = new System.Windows.Forms.GroupBox();
            this.dgExpiry = new System.Windows.Forms.DataGrid();
            this.gbExpiry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgExpiry)).BeginInit();
            this.SuspendLayout();
            // 
            // gbExpiry
            // 
            this.gbExpiry.Controls.Add(this.dgExpiry);
            this.gbExpiry.Location = new System.Drawing.Point(12, 54);
            this.gbExpiry.Name = "gbExpiry";
            this.gbExpiry.Size = new System.Drawing.Size(576, 398);
            this.gbExpiry.TabIndex = 0;
            this.gbExpiry.TabStop = false;
            this.gbExpiry.Text = " ";
            // 
            // dgExpiry
            // 
            this.dgExpiry.DataMember = "";
            this.dgExpiry.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgExpiry.Location = new System.Drawing.Point(17, 87);
            this.dgExpiry.Name = "dgExpiry";
            this.dgExpiry.ReadOnly = true;
            this.dgExpiry.Size = new System.Drawing.Size(538, 294);
            this.dgExpiry.TabIndex = 10;
            // 
            // frmExpiryReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(600, 494);
            this.Controls.Add(this.gbExpiry);
            this.Name = "frmExpiryReport";
            this.Text = "Subscription Dues Report";
            this.gbExpiry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgExpiry)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
	}
}
