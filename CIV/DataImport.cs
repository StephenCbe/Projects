using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using CIV.Classess;

namespace CIV
{
	/// <summary>
	/// Summary description for DataImport.
	/// </summary>
	public class frmDataImport : frmBaseForm
	{
		private System.Windows.Forms.Button btnImport;
		private System.Windows.Forms.Button btnImportReceipts;
        private ComboBox cboMagazine;
        private Label label_Magazine;
        private ComboBox cboCategory;
        private Label label13;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmDataImport()
		{
			
			InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            BindLanguages();
            if (cboMagazine.Items.Count > 0)
                cboMagazine.SelectedIndex = 0;
            if (cboCategory.Items.Count > 0)
            {
                int selIndex = cboCategory.FindString("General");
                cboCategory.SelectedIndex = selIndex;
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
                GlobalFn.ProcessException(eItems, "Error in Binding Languages in Import.cs");
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
      this.btnImport = new System.Windows.Forms.Button();
      this.btnImportReceipts = new System.Windows.Forms.Button();
      this.cboMagazine = new System.Windows.Forms.ComboBox();
      this.label_Magazine = new System.Windows.Forms.Label();
      this.cboCategory = new System.Windows.Forms.ComboBox();
      this.label13 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // btnImport
      // 
      this.btnImport.Location = new System.Drawing.Point(21, 186);
      this.btnImport.Name = "btnImport";
      this.btnImport.Size = new System.Drawing.Size(116, 32);
      this.btnImport.TabIndex = 0;
      this.btnImport.Text = "Import Subscribers";
      this.btnImport.Click += new System.EventHandler(this.btnImport_Click);
      // 
      // btnImportReceipts
      // 
      this.btnImportReceipts.Location = new System.Drawing.Point(170, 186);
      this.btnImportReceipts.Name = "btnImportReceipts";
      this.btnImportReceipts.Size = new System.Drawing.Size(96, 32);
      this.btnImportReceipts.TabIndex = 1;
      this.btnImportReceipts.Text = "Import Receipts";
      this.btnImportReceipts.Click += new System.EventHandler(this.btnImportReceipts_Click);
      // 
      // cboMagazine
      // 
      this.cboMagazine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cboMagazine.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboMagazine.Location = new System.Drawing.Point(114, 41);
      this.cboMagazine.Name = "cboMagazine";
      this.cboMagazine.Size = new System.Drawing.Size(152, 21);
      this.cboMagazine.TabIndex = 9;
      // 
      // label_Magazine
      // 
      this.label_Magazine.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label_Magazine.Location = new System.Drawing.Point(42, 41);
      this.label_Magazine.Name = "label_Magazine";
      this.label_Magazine.Size = new System.Drawing.Size(72, 16);
      this.label_Magazine.TabIndex = 10;
      this.label_Magazine.Text = "Magazine:";
      this.label_Magazine.TextAlign = System.Drawing.ContentAlignment.BottomRight;
      // 
      // cboCategory
      // 
      this.cboCategory.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cboCategory.FormattingEnabled = true;
      this.cboCategory.Items.AddRange(new object[] {
            "Bulk",
            "Free",
            "General",
            "Student"});
      this.cboCategory.Location = new System.Drawing.Point(114, 68);
      this.cboCategory.Name = "cboCategory";
      this.cboCategory.Size = new System.Drawing.Size(127, 21);
      this.cboCategory.TabIndex = 53;
      // 
      // label13
      // 
      this.label13.AutoSize = true;
      this.label13.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label13.Location = new System.Drawing.Point(45, 71);
      this.label13.Name = "label13";
      this.label13.Size = new System.Drawing.Size(70, 13);
      this.label13.TabIndex = 52;
      this.label13.Text = "Category:";
      this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // frmDataImport
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(322, 254);
      this.Controls.Add(this.cboCategory);
      this.Controls.Add(this.label13);
      this.Controls.Add(this.cboMagazine);
      this.Controls.Add(this.label_Magazine);
      this.Controls.Add(this.btnImportReceipts);
      this.Controls.Add(this.btnImport);
      this.Name = "frmDataImport";
      this.Text = "Data Import";
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion

		private void btnImport_Click(object sender, System.EventArgs e)
		{
            string category = "";
            switch (cboCategory.SelectedItem.ToString())
            {
                case "Bulk":
                    category = "B";
                    break;
                case "Free":
                    category = "F";
                    break;
                case "General":
                    category = "G";
                    break;
                case "Student":
                    category = "S";
                    break;
            }
            decimal discount = 0;
            // Delete from subscribers table.
            string langId = cboMagazine.SelectedValue.ToString();
            //if (MessageBox.Show("Are you sure you want to delete subscribers", GlobalFn.FormText, MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
            //    SQL.DeleteSubscribers(langId);
            //}
            DataSet ds = SQL.ImportSubscribers(langId, category);
			bool isErr = false;
			foreach(DataRow dr in ds.Tables[0].Rows)
			{
				isErr = false;
                DateTime startDate = Convert.ToDateTime(dr["start_date"]);
                DateTime retreatSD = new DateTime(2006, 9, 30);
                DateTime retreatED = new DateTime(2006, 10, 2);
                if (!category.Equals("B"))
                {
                    if ((startDate >= retreatSD) && (startDate <= retreatED) && (Convert.ToDecimal(dr["amount_paid"]) == 60))
                    {
                        category = "S";
                        discount = 20;
                    }
                    else
                    {
                        category = "G";
                        discount = 0;
                    }
                }
               
                    
                 

                
				try
				{
					int subId = SQL.ImportAddNewSubscriber(dr["sub_code"].ToString(),dr["title"].ToString(),dr["last_name"].ToString(),dr["first_name"].ToString(),dr["address_line1"].ToString(),dr["address_line2"].ToString(),dr["address_line3"].ToString(),dr["city"].ToString(),dr["district"].ToString(),"",dr["pin_code"].ToString(),dr["status"].ToString(),dr["remarks"].ToString(),Convert.ToDateTime(dr["start_date"]),Convert.ToDecimal(dr["amount_paid"]),langId,dr["num_copies"].ToString(),category,discount);
					if (subId < 0)
					{
						isErr = true;
					}
				}
				catch(Exception eImport)
				{
                    eImport.ToString();
					//MessageBox.Show(eImport.Message);
					isErr = true;
				}
				if (!isErr)
				{
					try
					{
						//delete the record
						SQL.ImportDeleteRec(dr["subscribers_tmp_id"].ToString());
					}
					catch(Exception eDel)
					{
						MessageBox.Show(eDel.Message);
					}

				}
			}
			MessageBox.Show("Done Importing Subscribers");
		}

		private void btnImportReceipts_Click(object sender, System.EventArgs e)
		{
            char splitter = '/';
            int yearPart = 0;
            int monPart = 0;
            int dayPart = 0;
            string langId = cboMagazine.SelectedValue.ToString();
			DataSet ds = SQL.ImportReceipts();
			foreach(DataRow dr in ds.Tables[0].Rows)
			{			
				try
				{
                    if (dr["subscno"].ToString().Length > 0)
                    {
                        if (dr["bill_date"].ToString().Length > 0)
                        {
                            string[] dateParts = dr["bill_date"].ToString().Split(splitter);
                            if (dateParts.Length > 0)
                            {
                                dayPart = Convert.ToInt32(dateParts[0]);
                                monPart = Convert.ToInt32(dateParts[1]);
                                yearPart = Convert.ToInt32(dateParts[2]);
                            }
                            DateTime billDate = new DateTime(yearPart, monPart, dayPart);
                            SQL.ImportRecepts(dr["SUBSCNO"].ToString(), langId, dr["BILL_AMT"].ToString(), dr["bill_no"].ToString(), billDate.ToString("dd/MM/yyyy"));
                         }
                        
                    }
                    System.Threading.Thread.Sleep(10);
				}
				catch(Exception eImport)
				{
					MessageBox.Show(eImport.Message);
				}
				
			}
			MessageBox.Show("Done Import");
		
		}
	}
}
