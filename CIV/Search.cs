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
	/// Summary description for frmSearch.
	/// </summary>
	public class frmSearch : frmBaseForm
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtSubcode;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.TextBox txtPinCode;
		private System.Windows.Forms.Button btnFind;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.DataGrid dgSearch;
		private System.Windows.Forms.ComboBox cboMagazine;
        private TextBox txtAddress1;
        private Label label7;
        private TextBox txtBillNum;
        private Label label9;
        private ComboBox cboStatus;
        private Label label8;
		DataTable oTable=new DataTable("foundSubscribers");

		public frmSearch()
		{
			InitializeComponent();
      this.Text += " - " + GlobalFn.FormText;
                 
			BindLanguages();
			if (cboMagazine.Items.Count > 0)
				cboMagazine.SelectedIndex=0;
      if (cboStatus.Items.Count > 0)
      {
          int index = cboStatus.FindString("Choose All");
          cboStatus.SelectedIndex = index;
      }
			BuildGrid();
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBillNum = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cboMagazine = new System.Windows.Forms.ComboBox();
            this.dgSearch = new System.Windows.Forms.DataGrid();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtPinCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSubcode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBillNum);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cboStatus);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtAddress1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cboMagazine);
            this.groupBox1.Controls.Add(this.dgSearch);
            this.groupBox1.Controls.Add(this.btnFind);
            this.groupBox1.Controls.Add(this.txtPinCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtCity);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtSubcode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(704, 487);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // txtBillNum
            // 
            this.txtBillNum.Location = new System.Drawing.Point(415, 100);
            this.txtBillNum.Name = "txtBillNum";
            this.txtBillNum.Size = new System.Drawing.Size(111, 20);
            this.txtBillNum.TabIndex = 65;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(351, 103);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 64;
            this.label9.Text = "Bill Num:";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Items.AddRange(new object[] {
            "Choose All",
            "Active",
            "Stopped"});
            this.cboStatus.Location = new System.Drawing.Point(415, 75);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(112, 21);
            this.cboStatus.TabIndex = 62;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(359, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 16);
            this.label8.TabIndex = 63;
            this.label8.Text = "Status:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddress1
            // 
            this.txtAddress1.Location = new System.Drawing.Point(415, 49);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(247, 20);
            this.txtAddress1.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(343, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Address1:";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = " Magazine Name:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboMagazine
            // 
            this.cboMagazine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMagazine.Location = new System.Drawing.Point(144, 24);
            this.cboMagazine.Name = "cboMagazine";
            this.cboMagazine.Size = new System.Drawing.Size(152, 21);
            this.cboMagazine.TabIndex = 1;
            // 
            // dgSearch
            // 
            this.dgSearch.CaptionFont = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgSearch.CaptionText = "Please click on subscriber code to renew subscription.";
            this.dgSearch.DataMember = "";
            this.dgSearch.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgSearch.Location = new System.Drawing.Point(16, 184);
            this.dgSearch.Name = "dgSearch";
            this.dgSearch.ReadOnly = true;
            this.dgSearch.Size = new System.Drawing.Size(682, 297);
            this.dgSearch.TabIndex = 9;
            this.dgSearch.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgSearch_MouseUp);
            // 
            // btnFind
            // 
            this.btnFind.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFind.Location = new System.Drawing.Point(301, 154);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(88, 24);
            this.btnFind.TabIndex = 7;
            this.btnFind.Text = "&Find";
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtPinCode
            // 
            this.txtPinCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPinCode.Location = new System.Drawing.Point(415, 22);
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.Size = new System.Drawing.Size(88, 21);
            this.txtPinCode.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(324, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "Pin Code:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(144, 100);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(152, 21);
            this.txtCity.TabIndex = 4;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(144, 72);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(168, 21);
            this.txtName.TabIndex = 3;
            // 
            // txtSubcode
            // 
            this.txtSubcode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubcode.Location = new System.Drawing.Point(144, 48);
            this.txtSubcode.Name = "txtSubcode";
            this.txtSubcode.Size = new System.Drawing.Size(80, 21);
            this.txtSubcode.TabIndex = 2;
            this.txtSubcode.TextChanged += new System.EventHandler(this.txtSubcode_TextChanged);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(96, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(104, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "City:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Subscription Code:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(88, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(535, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "Enter the Search Criteria ( Either Subscription Code OR Name, City and PinCode)";
            // 
            // frmSearch
            // 
            this.AcceptButton = this.btnFind;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(720, 535);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Name = "frmSearch";
            this.Text = "Search Subscriber";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSearch)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		

		private void btnFind_Click(object sender, System.EventArgs e)
		{
			string subCode = txtSubcode.Text;
			string subName = txtName.Text;
			string city = txtCity.Text;
			string pinCode = txtPinCode.Text;
      string address1 = txtAddress1.Text;
      string status = "";
      switch (cboStatus.SelectedItem.ToString())
      {
          case "Active":
              status = "A";
              break;
          case "Stopped":
              status = "X";
              break;
          case "Choose All":
              status = "-1";
              break;

      }
          
      string billNum = txtBillNum.Text;
			if (pinCode.Length > 0)
			{
				if (!GlobalFn.IsNumeric(pinCode))			
				{
					MessageBox.Show("PinCode must be a number");
					
					return;
				}
			}
      if (billNum.Length > 0)
      {
          if (!GlobalFn.IsNumeric(billNum))
          {
              MessageBox.Show("Bill Num must be a number");
              return;
          }
      }
        //if ((subCode.Length == 0) && (subName.Length == 0) && (city.Length == 0) && (pinCode.Length == 0) && (address1.Length == 0))
        //{
        //    MessageBox.Show("Please enter Subscription Code or Name or City or Pincode or Address of the Subscriber",GlobalFn.FormText);
        //    return;
        //}
            int i = 0;
			try
			{
                if (txtBillNum.TextLength == 0)
                    oTable = SQL.FindSubscribers(subCode, subName, city, pinCode, cboMagazine.SelectedValue.ToString(), address1,status).Tables[0];
                else
                {
                    oTable = SQL.FindSubscribers(cboMagazine.SelectedValue.ToString(), txtBillNum.Text, status).Tables[0];
                }

                if ((oTable.Rows.Count == 1) && (subCode.Length > 0) && (subName.Length == 0) && (city.Length == 0) && (pinCode.Length == 0))
                {
                  if (MessageBox.Show("Do you renew this Subscription?", GlobalFn.FormText, MessageBoxButtons.YesNo) == DialogResult.Yes)
                  {
                    frmRenewal objRenew = new frmRenewal(oTable.Rows[0]["subscriber_id"].ToString());
                    objRenew.ShowDialog();
                  }
                  else
                  {
                    frmEditSubscription objRenew = new frmEditSubscription(oTable.Rows[0]["subscriber_id"].ToString());
                    objRenew.ShowDialog();
                  }
                }
                else
                {
                    if (!oTable.Columns.Contains("row_cnt"))
                    {
                        DataColumn dc = new DataColumn("row_cnt", Type.GetType("System.Int16"));
                       
                        oTable.Columns.Add(dc);
                    }
                    foreach (DataRow dr in oTable.Rows)
                    {
                        dr["row_cnt"] = ++i;
                    }
                    if (!oTable.Columns.Contains("sub_renew"))
                    {
                      DataColumn dc = new DataColumn("sub_renew", Type.GetType("System.String"));

                      oTable.Columns.Add(dc);
                    }
                    foreach (DataRow dr in oTable.Rows)
                    {
                      dr["sub_renew"] = "Renewal";
                    }

                    if (!oTable.Columns.Contains("sub_ca"))
                    {
                      DataColumn dc = new DataColumn("sub_ca", Type.GetType("System.String"));

                      oTable.Columns.Add(dc);
                    }
                    foreach (DataRow dr in oTable.Rows)
                    {
                      dr["sub_ca"] = "Address Change";
                    }

                    dgSearch.DataSource = oTable;

                }
				if (oTable.Rows.Count == 0)
				{
					MessageBox.Show("Your search criteria yielded no results!",GlobalFn.FormText,MessageBoxButtons.OK);
					return;
				}
			}
			catch(Exception eLang)
			{
				MessageBox.Show("Database error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eLang,"Error in Binding Item Types in FrmSearch.cs");
				return;
			}
		
		}

		private void BuildGrid()
		{
            int j = 0;
			try
			{
				oTable = SQL.FindSubscribers("-1","","","",cboMagazine.SelectedValue.ToString(),"","-1").Tables[0];
        if (!oTable.Columns.Contains("row_cnt"))
        {
            DataColumn dc = new DataColumn("row_cnt", Type.GetType("System.Int16"));

            oTable.Columns.Add(dc);
        }
        foreach (DataRow dr in oTable.Rows)
        {
            dr["row_cnt"] = ++j;
        }
        if (!oTable.Columns.Contains("sub_renew"))
        {
          DataColumn dc = new DataColumn("sub_renew", Type.GetType("System.String"));

          oTable.Columns.Add(dc);
        }
        foreach (DataRow dr in oTable.Rows)
        {
          dr["sub_renew"] = "Renewal";
        }

        if (!oTable.Columns.Contains("sub_ca"))
        {
          DataColumn dc = new DataColumn("sub_ca", Type.GetType("System.String"));

          oTable.Columns.Add(dc);
        }
        foreach (DataRow dr in oTable.Rows)
        {
          dr["sub_ca"] = "Address Change";
        }

				DataGridTableStyle ts = new DataGridTableStyle();
				ts.MappingName = oTable.TableName;
				ts.HeaderFont = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((System.Byte)(0)));
				ts.AlternatingBackColor = Color.SkyBlue;

				GridColumnStylesCollection  ColumnStyles = ts.GridColumnStyles;

				DataGridColumnStyle cs = new DataGridTextBoxColumn();
			
				cs = new DataGridTextBoxColumn();
				cs.MappingName = "subscriber_id";
				cs.HeaderText = "Subscriber ID" ;
				cs.Width = 0;
				ColumnStyles.Add(cs);

        cs = new DataGridTextBoxColumn();
        cs.MappingName = "row_cnt";
        cs.HeaderText = "Num. Rows";
        cs.Width = 50;
        ColumnStyles.Add(cs);

        cs = new DataGridTextBoxColumn();
        cs.MappingName = "sub_renew";
        cs.HeaderText = "Renewal";
        cs.Width = 100;
        ColumnStyles.Add(cs);

        cs = new DataGridTextBoxColumn();
        cs.MappingName = "sub_ca";
        cs.HeaderText = "Address Change";
        cs.Width = 100;
        ColumnStyles.Add(cs);

				cs = new DataGridTextBoxColumn();
				cs.MappingName = "sub_code";
				cs.HeaderText = "Subscriber Code" ;
				cs.Width = 50;
				ColumnStyles.Add(cs);

				cs = new DataGridTextBoxColumn();
				cs.MappingName = "last_name";
				cs.HeaderText = "Last Name" ;
				cs.Width = 75;
				ColumnStyles.Add(cs);

				cs = new DataGridTextBoxColumn();
				cs.MappingName = "first_name";
				cs.HeaderText = "First Name" ;
				cs.Width = 150;
				ColumnStyles.Add(cs);
				
				cs = new DataGridTextBoxColumn();
				cs.MappingName = "address_line1";
				cs.HeaderText = "Address Line1" ;
				cs.Width = 200;
				ColumnStyles.Add(cs);

				cs = new DataGridTextBoxColumn();
				cs.MappingName = "city";
				cs.HeaderText = "City" ;
				cs.Width = 100;
				ColumnStyles.Add(cs);

				cs = new DataGridTextBoxColumn();
				cs.MappingName = "district";
				cs.HeaderText = "District" ;
				cs.Width = 75;
				ColumnStyles.Add(cs);

				cs = new DataGridTextBoxColumn();
				cs.MappingName = "state_name";
				cs.HeaderText = "State" ;
				cs.Width = 50;
				ColumnStyles.Add(cs);

				cs = new DataGridTextBoxColumn();
				cs.MappingName = "pin_code";
				cs.HeaderText = "Pin Code" ;
				cs.Width = 75;
				ColumnStyles.Add(cs);

        cs = new DataGridTextBoxColumn();
        cs.MappingName = "status";
        cs.HeaderText = "Status";
        cs.Width = 100;
        ColumnStyles.Add(cs);

				GridTableStylesCollection tableStyles = dgSearch.TableStyles;  
				tableStyles.Add(ts);

				dgSearch.DataSource = oTable;
			}
			catch(Exception eGrid)
			{
				MessageBox.Show("Database error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eGrid,"Error in Binding Data Grid in FrmSearch.cs");
				return;
			}

		}

		private void dgSearch_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Drawing.Point pt = new Point(e.X,e.Y);
			DataGrid.HitTestInfo hti = dgSearch.HitTest(pt);

			if (hti.Type == DataGrid.HitTestType.Cell)
			{
				if (hti.Column == 2)
				{
					frmRenewal objRenew = new frmRenewal(dgSearch[dgSearch.CurrentRowIndex,0].ToString());
					objRenew.ShowDialog();
				}
        else if (hti.Column == 3)
        {
          frmEditSubscription objEdit = new frmEditSubscription(dgSearch[dgSearch.CurrentRowIndex, 0].ToString());
          objEdit.ShowDialog();
        }
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
			catch(Exception eItems)
			{
				MessageBox.Show("Database error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eItems,"Error in Binding Items list box in NewSubscriptions.cs");	
				return;
			}
		}

        private void txtSubcode_TextChanged(object sender, EventArgs e)
        {
          
        }
	
		/*private void dgShipDetails_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			System.Drawing.Point pt = new Point(e.X,e.Y);
			DataGrid.HitTestInfo hti = dgShipDetails.HitTest(pt);
			

			if (hti.Type == DataGrid.HitTestType.Cell)
			{
				
					if (hti.Column == 0)
					{
						if (MessageBox.Show("Do you really want to delete this record?",GlobalFn.FormText,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
						{
							dgShipDetails.CurrentCell = new DataGridCell(hti.Row,hti.Column);
							dgShipDetails.Select(hti.Row);
							if (existShipId.Length == 0) //new one
							{
								DataRow row = oShipDet.Rows.Find(dgShipDetails[dgShipDetails.CurrentRowIndex,0]);
								oShipDet.Rows.Remove(row);
								dgShipDetails.DataSource = oShipDet;
							}
							else
							{
								DeleteShipmentDetail(existShipId,dgShipDetails[dgShipDetails.CurrentRowIndex,0].ToString());
							}//else\
						}
					}//if (hti.Column == 0)
				
			}//if (hti.Type == DataGrid.HitTestType.Cell)
		}
		 */
		
	}
}
