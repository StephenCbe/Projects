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
	/// Summary description for frmRenewal.
	/// </summary>
	public class frmRenewal : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblSubcode;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.DateTimePicker dtpBillDate;
		private System.Windows.Forms.TextBox txtDistrict;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cboStates;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtAmount;
		private System.Windows.Forms.Label label_Amount;
		private System.Windows.Forms.Label label_PaymentDt;
		private System.Windows.Forms.TextBox txtBillNo;
		private System.Windows.Forms.Label label_BillNo;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ComboBox cboTitle;
		private System.Windows.Forms.TextBox txtCopies;
		private System.Windows.Forms.TextBox txtRemarks;
		private System.Windows.Forms.TextBox txtPinCode;
		private System.Windows.Forms.TextBox txtAddress3;
		private System.Windows.Forms.TextBox txtAddress2;
		private System.Windows.Forms.TextBox txtAddress1;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.Label label_Copies;
		private System.Windows.Forms.Label label_Remarks;
		private System.Windows.Forms.Label label_PinCode;
		private System.Windows.Forms.Label label_Address3;
		private System.Windows.Forms.Label label_Address2;
		private System.Windows.Forms.Label label_Address1;
		private System.Windows.Forms.Label label_Title;
		private System.Windows.Forms.Label label_LName;
		private System.Windows.Forms.Label label_FName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox gbReceipt;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.GroupBox gb1;
		private System.Windows.Forms.Label lblBalance;
		private System.Windows.Forms.Label lblAmtPaid;
		private System.Windows.Forms.Label lblDueDate;
		private System.Windows.Forms.Label lblStartDate;
		private System.Windows.Forms.Label lblStartDateDisp;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cboStatus;
		private System.Windows.Forms.Label lblMagazine;
		private System.Windows.Forms.Label label_Magazine;
		string subCode="";
		string languageId="";
    private Label label13;
    private ComboBox cboCategory;
    private Label label15;
    private TextBox txtDiscount;
    private ComboBox cboCountries;
    private Label lblCountry;
    private CheckBox chkMakeReceipt;
    private Button ButtonAddState;
    private Button ButtonNewCountry;
    int subId = 0;
    DataTable dtCountries = new DataTable();
        private Label label17;
        private TextBox textBoxMobileNumber;
        DataTable dtStates = new DataTable();

    public frmRenewal(string subscriberId)
    {
	    InitializeComponent();

	    this.Text += " - " + GlobalFn.FormText;
	       		
	    dtpBillDate.CustomFormat = "dd/MM/yyyy";
	    dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
      
      BindCountries();
      if (cboCountries.Items.Count > 0)
      {
        int countIndex = cboCountries.FindString("India");
        if (countIndex > 0)
          cboCountries.SelectedIndex = countIndex;
      }
      
	    BindStates();
	    BindFields(subscriberId);
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

		
		private void BindStates()
		{
		  if ((cboCountries.Items.Count > 0) && (cboCountries.SelectedIndex >= 0))
		  {
			  try
			  {
				  cboStates.ValueMember = "state_ID";
				  cboStates.DisplayMember = "name";
          dtStates = SQL.SubscribersGetStates(cboCountries.SelectedValue.ToString()).Tables[0];
          cboStates.DataSource = dtStates;
			  }
			  catch(Exception eItems)
			  {
				  MessageBox.Show("Database error...",GlobalFn.FormText);
				  GlobalFn.ProcessException(eItems,"Error in Binding states in Renewal.cs");	
				  return;
			  }
			}
		}

    private void BindCountries()
    {
      try
      {
          cboCountries.ValueMember = "Country_ID";
          cboCountries.DisplayMember = "Country_name";
          dtCountries = SQL.SubscribersGetCountries().Tables[0];
          cboCountries.DataSource = dtCountries;
      }
      catch (Exception eItems)
      {
          MessageBox.Show("Database error...", GlobalFn.FormText);
          GlobalFn.ProcessException(eItems, "Error in Binding Countries in Renewal.cs");
          return;
      }
    }
		
		private void BindFields(string subscriberId)
		{
			try
			{
				subId = Convert.ToInt32(subscriberId);
				DataSet ds = SQL.GetSubscriberInfo(subscriberId);
				if (ds.Tables[0].Rows.Count > 0)
				{
					DataRow dr = ds.Tables[0].Rows[0];
					cboTitle.SelectedIndex = cboTitle.FindStringExact(dr["title"].ToString());
					txtLastName.Text= dr["last_name"].ToString();
					txtFirstName.Text = dr["first_name"].ToString();
					txtAddress1.Text = dr["address_line1"].ToString();
					txtAddress2.Text = dr["address_line2"].ToString();
					txtAddress3.Text = dr["address_line3"].ToString();
					subCode = dr["sub_code"].ToString();
					lblSubcode.Text = subCode;
					languageId = dr["language_id"].ToString();
					txtCity.Text = dr["city"].ToString();
					txtDistrict.Text = dr["district"].ToString();
					
					txtPinCode.Text = dr["pin_code"].ToString();
                    textBoxMobileNumber.Text = dr["mobile_number"].ToString();

					txtCopies.Text = dr["num_copies"].ToString();
					txtRemarks.Text = dr["remarks"].ToString();
					lblAmtPaid.Text = dr["amount_paid"].ToString();
					DateTime startDate = Convert.ToDateTime(dr["start_date"]);
					lblStartDateDisp.Text = startDate.ToString("dd/MM/yyyy");
					cboStatus.SelectedIndex = cboStatus.FindStringExact(dr["status"].ToString());
					lblMagazine.Text = dr["mag_name"].ToString();
          txtDiscount.Text = "0.0";// Convert.ToDouble(dr["discount"]).ToString();
					cboCategory.SelectedIndex  = cboCategory.FindStringExact(dr["category_name"].ToString());
          cboCountries.SelectedValue = dr["Country_id"].ToString();
          BindStates();
          cboStates.SelectedValue = dr["state_ID"];
          object[] dueDate = new object[2];
          dueDate = GlobalFn.CalculateDueDate(startDate, Convert.ToDouble(dr["amount_paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt32(dr["num_copies"]));
          lblBalance.Text = dueDate[1].ToString();
          lblDueDate.Text = Convert.ToDateTime(dueDate[0]).ToString("dd/MM/yyyy");
				}
			}
			catch(Exception eItems)
			{
				MessageBox.Show("Database error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eItems,"Error in Binding Items list box in Renewal.cs");	
				return;
			}

		}
        
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.gb1 = new System.Windows.Forms.GroupBox();
            this.ButtonAddState = new System.Windows.Forms.Button();
            this.ButtonNewCountry = new System.Windows.Forms.Button();
            this.chkMakeReceipt = new System.Windows.Forms.CheckBox();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.lblMagazine = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblStartDateDisp = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblDueDate = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lblAmtPaid = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.gbReceipt = new System.Windows.Forms.GroupBox();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.label_BillNo = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label_Amount = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_PaymentDt = new System.Windows.Forms.Label();
            this.label_Magazine = new System.Windows.Forms.Label();
            this.lblSubcode = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCountries = new System.Windows.Forms.ComboBox();
            this.cboStates = new System.Windows.Forms.ComboBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblCountry = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboTitle = new System.Windows.Forms.ComboBox();
            this.txtCopies = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtPinCode = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label_Copies = new System.Windows.Forms.Label();
            this.label_Remarks = new System.Windows.Forms.Label();
            this.label_PinCode = new System.Windows.Forms.Label();
            this.label_Address3 = new System.Windows.Forms.Label();
            this.label_Address2 = new System.Windows.Forms.Label();
            this.label_Address1 = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.label_LName = new System.Windows.Forms.Label();
            this.label_FName = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxMobileNumber = new System.Windows.Forms.TextBox();
            this.gb1.SuspendLayout();
            this.gbReceipt.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb1
            // 
            this.gb1.Controls.Add(this.textBoxMobileNumber);
            this.gb1.Controls.Add(this.label17);
            this.gb1.Controls.Add(this.ButtonAddState);
            this.gb1.Controls.Add(this.ButtonNewCountry);
            this.gb1.Controls.Add(this.chkMakeReceipt);
            this.gb1.Controls.Add(this.cboCategory);
            this.gb1.Controls.Add(this.label15);
            this.gb1.Controls.Add(this.txtDiscount);
            this.gb1.Controls.Add(this.label13);
            this.gb1.Controls.Add(this.lblMagazine);
            this.gb1.Controls.Add(this.cboStatus);
            this.gb1.Controls.Add(this.label4);
            this.gb1.Controls.Add(this.lblStartDateDisp);
            this.gb1.Controls.Add(this.lblStartDate);
            this.gb1.Controls.Add(this.lblDueDate);
            this.gb1.Controls.Add(this.label16);
            this.gb1.Controls.Add(this.lblAmtPaid);
            this.gb1.Controls.Add(this.label14);
            this.gb1.Controls.Add(this.lblBalance);
            this.gb1.Controls.Add(this.label11);
            this.gb1.Controls.Add(this.gbReceipt);
            this.gb1.Controls.Add(this.label_Magazine);
            this.gb1.Controls.Add(this.lblSubcode);
            this.gb1.Controls.Add(this.label10);
            this.gb1.Controls.Add(this.label9);
            this.gb1.Controls.Add(this.label8);
            this.gb1.Controls.Add(this.label6);
            this.gb1.Controls.Add(this.label5);
            this.gb1.Controls.Add(this.txtDistrict);
            this.gb1.Controls.Add(this.label3);
            this.gb1.Controls.Add(this.cboCountries);
            this.gb1.Controls.Add(this.cboStates);
            this.gb1.Controls.Add(this.txtCity);
            this.gb1.Controls.Add(this.lblCountry);
            this.gb1.Controls.Add(this.label2);
            this.gb1.Controls.Add(this.label1);
            this.gb1.Controls.Add(this.btnCancel);
            this.gb1.Controls.Add(this.btnSave);
            this.gb1.Controls.Add(this.cboTitle);
            this.gb1.Controls.Add(this.txtCopies);
            this.gb1.Controls.Add(this.txtRemarks);
            this.gb1.Controls.Add(this.txtPinCode);
            this.gb1.Controls.Add(this.txtAddress3);
            this.gb1.Controls.Add(this.txtAddress2);
            this.gb1.Controls.Add(this.txtAddress1);
            this.gb1.Controls.Add(this.txtLastName);
            this.gb1.Controls.Add(this.txtFirstName);
            this.gb1.Controls.Add(this.label_Copies);
            this.gb1.Controls.Add(this.label_Remarks);
            this.gb1.Controls.Add(this.label_PinCode);
            this.gb1.Controls.Add(this.label_Address3);
            this.gb1.Controls.Add(this.label_Address2);
            this.gb1.Controls.Add(this.label_Address1);
            this.gb1.Controls.Add(this.label_Title);
            this.gb1.Controls.Add(this.label_LName);
            this.gb1.Controls.Add(this.label_FName);
            this.gb1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb1.Location = new System.Drawing.Point(44, 37);
            this.gb1.Name = "gb1";
            this.gb1.Size = new System.Drawing.Size(804, 508);
            this.gb1.TabIndex = 4;
            this.gb1.TabStop = false;
            // 
            // ButtonAddState
            // 
            this.ButtonAddState.Location = new System.Drawing.Point(343, 306);
            this.ButtonAddState.Name = "ButtonAddState";
            this.ButtonAddState.Size = new System.Drawing.Size(118, 25);
            this.ButtonAddState.TabIndex = 70;
            this.ButtonAddState.Text = "Add State";
            this.ButtonAddState.UseVisualStyleBackColor = true;
            this.ButtonAddState.Click += new System.EventHandler(this.ButtonAddState_Click);
            // 
            // ButtonNewCountry
            // 
            this.ButtonNewCountry.Location = new System.Drawing.Point(343, 276);
            this.ButtonNewCountry.Name = "ButtonNewCountry";
            this.ButtonNewCountry.Size = new System.Drawing.Size(118, 25);
            this.ButtonNewCountry.TabIndex = 69;
            this.ButtonNewCountry.Text = "Add Country";
            this.ButtonNewCountry.UseVisualStyleBackColor = true;
            this.ButtonNewCountry.Click += new System.EventHandler(this.ButtonNewCountry_Click);
            // 
            // chkMakeReceipt
            // 
            this.chkMakeReceipt.AutoSize = true;
            this.chkMakeReceipt.ForeColor = System.Drawing.Color.Red;
            this.chkMakeReceipt.Location = new System.Drawing.Point(501, 480);
            this.chkMakeReceipt.Name = "chkMakeReceipt";
            this.chkMakeReceipt.Size = new System.Drawing.Size(190, 21);
            this.chkMakeReceipt.TabIndex = 68;
            this.chkMakeReceipt.Text = "Do Not Make Receipt";
            this.chkMakeReceipt.UseVisualStyleBackColor = true;
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
            this.cboCategory.Location = new System.Drawing.Point(581, 140);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(167, 25);
            this.cboCategory.TabIndex = 15;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(497, 143);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 17);
            this.label15.TabIndex = 66;
            this.label15.Text = "Category:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(581, 111);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(103, 24);
            this.txtDiscount.TabIndex = 14;
            this.txtDiscount.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(500, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(81, 17);
            this.label13.TabIndex = 64;
            this.label13.Text = "Discount:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblMagazine
            // 
            this.lblMagazine.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMagazine.ForeColor = System.Drawing.Color.Red;
            this.lblMagazine.Location = new System.Drawing.Point(102, 28);
            this.lblMagazine.Name = "lblMagazine";
            this.lblMagazine.Size = new System.Drawing.Size(182, 18);
            this.lblMagazine.TabIndex = 63;
            this.lblMagazine.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStatus.Items.AddRange(new object[] {
            "Active",
            "Stopped"});
            this.cboStatus.Location = new System.Drawing.Point(581, 83);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(134, 25);
            this.cboStatus.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(514, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 19);
            this.label4.TabIndex = 61;
            this.label4.Text = "Status:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStartDateDisp
            // 
            this.lblStartDateDisp.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDateDisp.Location = new System.Drawing.Point(581, 280);
            this.lblStartDateDisp.Name = "lblStartDateDisp";
            this.lblStartDateDisp.Size = new System.Drawing.Size(163, 19);
            this.lblStartDateDisp.TabIndex = 60;
            this.lblStartDateDisp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Location = new System.Drawing.Point(485, 280);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(96, 19);
            this.lblStartDate.TabIndex = 59;
            this.lblStartDate.Text = "Start Date:";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDueDate
            // 
            this.lblDueDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDueDate.Location = new System.Drawing.Point(581, 363);
            this.lblDueDate.Name = "lblDueDate";
            this.lblDueDate.Size = new System.Drawing.Size(173, 15);
            this.lblDueDate.TabIndex = 57;
            this.lblDueDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(494, 363);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 15);
            this.label16.TabIndex = 56;
            this.label16.Text = "Due Date:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmtPaid
            // 
            this.lblAmtPaid.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmtPaid.Location = new System.Drawing.Point(581, 308);
            this.lblAmtPaid.Name = "lblAmtPaid";
            this.lblAmtPaid.Size = new System.Drawing.Size(138, 19);
            this.lblAmtPaid.TabIndex = 55;
            this.lblAmtPaid.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(466, 308);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(115, 19);
            this.label14.TabIndex = 54;
            this.label14.Text = "Amount Paid:";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblBalance
            // 
            this.lblBalance.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBalance.Location = new System.Drawing.Point(581, 337);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(115, 18);
            this.lblBalance.TabIndex = 53;
            this.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(504, 336);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 18);
            this.label11.TabIndex = 52;
            this.label11.Text = "Balance:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // gbReceipt
            // 
            this.gbReceipt.Controls.Add(this.dtpBillDate);
            this.gbReceipt.Controls.Add(this.label_BillNo);
            this.gbReceipt.Controls.Add(this.txtAmount);
            this.gbReceipt.Controls.Add(this.txtBillNo);
            this.gbReceipt.Controls.Add(this.label_Amount);
            this.gbReceipt.Controls.Add(this.label7);
            this.gbReceipt.Controls.Add(this.label_PaymentDt);
            this.gbReceipt.Location = new System.Drawing.Point(485, 168);
            this.gbReceipt.Name = "gbReceipt";
            this.gbReceipt.Size = new System.Drawing.Size(243, 93);
            this.gbReceipt.TabIndex = 48;
            this.gbReceipt.TabStop = false;
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBillDate.Location = new System.Drawing.Point(96, 37);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(125, 24);
            this.dtpBillDate.TabIndex = 17;
            // 
            // label_BillNo
            // 
            this.label_BillNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_BillNo.Location = new System.Drawing.Point(29, 10);
            this.label_BillNo.Name = "label_BillNo";
            this.label_BillNo.Size = new System.Drawing.Size(67, 19);
            this.label_BillNo.TabIndex = 23;
            this.label_BillNo.Text = "Bill no:";
            this.label_BillNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(96, 65);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(67, 24);
            this.txtAmount.TabIndex = 18;
            this.txtAmount.Text = "250.0";
            // 
            // txtBillNo
            // 
            this.txtBillNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNo.Location = new System.Drawing.Point(96, 9);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(115, 24);
            this.txtBillNo.TabIndex = 16;
            // 
            // label_Amount
            // 
            this.label_Amount.Location = new System.Drawing.Point(19, 66);
            this.label_Amount.Name = "label_Amount";
            this.label_Amount.Size = new System.Drawing.Size(77, 18);
            this.label_Amount.TabIndex = 26;
            this.label_Amount.Text = "Amount:";
            this.label_Amount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(163, 74);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 9);
            this.label7.TabIndex = 38;
            this.label7.Text = "*";
            // 
            // label_PaymentDt
            // 
            this.label_PaymentDt.Location = new System.Drawing.Point(10, 39);
            this.label_PaymentDt.Name = "label_PaymentDt";
            this.label_PaymentDt.Size = new System.Drawing.Size(86, 19);
            this.label_PaymentDt.TabIndex = 25;
            this.label_PaymentDt.Text = "Bill Date:";
            this.label_PaymentDt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Magazine
            // 
            this.label_Magazine.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Magazine.Location = new System.Drawing.Point(11, 28);
            this.label_Magazine.Name = "label_Magazine";
            this.label_Magazine.Size = new System.Drawing.Size(95, 18);
            this.label_Magazine.TabIndex = 47;
            this.label_Magazine.Text = "Magazine:";
            this.label_Magazine.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // lblSubcode
            // 
            this.lblSubcode.ForeColor = System.Drawing.Color.Blue;
            this.lblSubcode.Location = new System.Drawing.Point(581, 28);
            this.lblSubcode.Name = "lblSubcode";
            this.lblSubcode.Size = new System.Drawing.Size(120, 18);
            this.lblSubcode.TabIndex = 45;
            this.lblSubcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(418, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(163, 18);
            this.label10.TabIndex = 44;
            this.label10.Text = "Subscription Code:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(648, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 19);
            this.label9.TabIndex = 40;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(288, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(19, 18);
            this.label8.TabIndex = 39;
            this.label8.Text = "*";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(374, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 19);
            this.label6.TabIndex = 37;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(307, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(19, 18);
            this.label5.TabIndex = 36;
            this.label5.Text = "*";
            // 
            // txtDistrict
            // 
            this.txtDistrict.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistrict.Location = new System.Drawing.Point(106, 249);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.Size = new System.Drawing.Size(182, 24);
            this.txtDistrict.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(38, 249);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 19);
            this.label3.TabIndex = 33;
            this.label3.Text = "District:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCountries
            // 
            this.cboCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCountries.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCountries.Location = new System.Drawing.Point(106, 277);
            this.cboCountries.Name = "cboCountries";
            this.cboCountries.Size = new System.Drawing.Size(230, 25);
            this.cboCountries.TabIndex = 9;
            this.cboCountries.SelectionChangeCommitted += new System.EventHandler(this.cboCountries_SelectionChangeCommitted);
            // 
            // cboStates
            // 
            this.cboStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStates.Location = new System.Drawing.Point(106, 307);
            this.cboStates.Name = "cboStates";
            this.cboStates.Size = new System.Drawing.Size(230, 25);
            this.cboStates.TabIndex = 9;
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(106, 222);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(182, 24);
            this.txtCity.TabIndex = 7;
            // 
            // lblCountry
            // 
            this.lblCountry.Location = new System.Drawing.Point(13, 277);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(93, 18);
            this.lblCountry.TabIndex = 30;
            this.lblCountry.Text = "Country:";
            this.lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(48, 307);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 18);
            this.label2.TabIndex = 30;
            this.label2.Text = "State:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(58, 222);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 18);
            this.label1.TabIndex = 29;
            this.label1.Text = "City:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(375, 474);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 28);
            this.btnCancel.TabIndex = 20;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(219, 474);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(146, 28);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "&Save and Print";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cboTitle
            // 
            this.cboTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTitle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTitle.Items.AddRange(new object[] {
            "Dr.",
            "Mr.",
            "Mrs.",
            "Ms.",
            "M/s.",
            "Pastor.",
            "Prof.",
            "Rev.",
            "The."});
            this.cboTitle.Location = new System.Drawing.Point(106, 55);
            this.cboTitle.Name = "cboTitle";
            this.cboTitle.Size = new System.Drawing.Size(96, 25);
            this.cboTitle.TabIndex = 1;
            // 
            // txtCopies
            // 
            this.txtCopies.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopies.Location = new System.Drawing.Point(581, 55);
            this.txtCopies.Name = "txtCopies";
            this.txtCopies.Size = new System.Drawing.Size(60, 24);
            this.txtCopies.TabIndex = 12;
            this.txtCopies.Text = "1";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(105, 396);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(259, 68);
            this.txtRemarks.TabIndex = 11;
            // 
            // txtPinCode
            // 
            this.txtPinCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPinCode.Location = new System.Drawing.Point(106, 337);
            this.txtPinCode.MaxLength = 6;
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.Size = new System.Drawing.Size(96, 24);
            this.txtPinCode.TabIndex = 10;
            // 
            // txtAddress3
            // 
            this.txtAddress3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress3.Location = new System.Drawing.Point(106, 194);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(259, 24);
            this.txtAddress3.TabIndex = 6;
            this.txtAddress3.TextChanged += new System.EventHandler(this.txtAddress3_TextChanged);
            // 
            // txtAddress2
            // 
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(106, 166);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(259, 24);
            this.txtAddress2.TabIndex = 5;
            this.txtAddress2.TextChanged += new System.EventHandler(this.txtAddress2_TextChanged);
            // 
            // txtAddress1
            // 
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(106, 138);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(259, 24);
            this.txtAddress1.TabIndex = 4;
            this.txtAddress1.TextChanged += new System.EventHandler(this.txtAddress1_TextChanged);
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(106, 83);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(201, 24);
            this.txtLastName.TabIndex = 2;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(106, 111);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(201, 24);
            this.txtFirstName.TabIndex = 3;
            this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
            // 
            // label_Copies
            // 
            this.label_Copies.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Copies.Location = new System.Drawing.Point(514, 57);
            this.label_Copies.Name = "label_Copies";
            this.label_Copies.Size = new System.Drawing.Size(67, 18);
            this.label_Copies.TabIndex = 9;
            this.label_Copies.Text = "Copies:";
            this.label_Copies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Remarks
            // 
            this.label_Remarks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Remarks.Location = new System.Drawing.Point(18, 396);
            this.label_Remarks.Name = "label_Remarks";
            this.label_Remarks.Size = new System.Drawing.Size(87, 19);
            this.label_Remarks.TabIndex = 7;
            this.label_Remarks.Text = "Remarks:";
            this.label_Remarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_PinCode
            // 
            this.label_PinCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_PinCode.Location = new System.Drawing.Point(29, 337);
            this.label_PinCode.Name = "label_PinCode";
            this.label_PinCode.Size = new System.Drawing.Size(77, 18);
            this.label_PinCode.TabIndex = 6;
            this.label_PinCode.Text = "PinCode:";
            this.label_PinCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Address3
            // 
            this.label_Address3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Address3.Location = new System.Drawing.Point(10, 197);
            this.label_Address3.Name = "label_Address3";
            this.label_Address3.Size = new System.Drawing.Size(96, 19);
            this.label_Address3.TabIndex = 5;
            this.label_Address3.Text = "Address 3:";
            this.label_Address3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Address2
            // 
            this.label_Address2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Address2.Location = new System.Drawing.Point(11, 168);
            this.label_Address2.Name = "label_Address2";
            this.label_Address2.Size = new System.Drawing.Size(95, 21);
            this.label_Address2.TabIndex = 4;
            this.label_Address2.Text = "Address 2:";
            this.label_Address2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_Address1
            // 
            this.label_Address1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Address1.Location = new System.Drawing.Point(7, 141);
            this.label_Address1.Name = "label_Address1";
            this.label_Address1.Size = new System.Drawing.Size(99, 22);
            this.label_Address1.TabIndex = 3;
            this.label_Address1.Text = "Address 1:";
            this.label_Address1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_Title
            // 
            this.label_Title.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Title.Location = new System.Drawing.Point(58, 57);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(48, 18);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "Title:";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_LName
            // 
            this.label_LName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_LName.Location = new System.Drawing.Point(10, 75);
            this.label_LName.Name = "label_LName";
            this.label_LName.Size = new System.Drawing.Size(96, 28);
            this.label_LName.TabIndex = 0;
            this.label_LName.Text = "Last Name:";
            this.label_LName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_FName
            // 
            this.label_FName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_FName.Location = new System.Drawing.Point(4, 112);
            this.label_FName.Name = "label_FName";
            this.label_FName.Size = new System.Drawing.Size(102, 18);
            this.label_FName.TabIndex = 2;
            this.label_FName.Text = "First Name:";
            this.label_FName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(298, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(144, 19);
            this.label12.TabIndex = 5;
            this.label12.Text = "* - Required Field";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(15, 370);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(90, 17);
            this.label17.TabIndex = 71;
            this.label17.Text = "Mobile No:";
            // 
            // textBoxMobileNumber
            // 
            this.textBoxMobileNumber.Location = new System.Drawing.Point(106, 367);
            this.textBoxMobileNumber.MaxLength = 10;
            this.textBoxMobileNumber.Name = "textBoxMobileNumber";
            this.textBoxMobileNumber.Size = new System.Drawing.Size(259, 24);
            this.textBoxMobileNumber.TabIndex = 72;
            // 
            // frmRenewal
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(911, 557);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.gb1);
            this.Name = "frmRenewal";
            this.Text = "Renew Subscription";
            this.gb1.ResumeLayout(false);
            this.gb1.PerformLayout();
            this.gbReceipt.ResumeLayout(false);
            this.gbReceipt.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			Decimal amtPaid = 0;
      decimal discount = 0;
      string category = "";
      string makeReceipt = "Y";

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
    
      if (chkMakeReceipt.Checked)
          makeReceipt = "N";
      else
          makeReceipt = "Y";
			if (txtFirstName.TextLength==0)
			{
				MessageBox.Show("Please enter the name",GlobalFn.FormText);
				//TurnOnSave();
				return;
			}

			if (txtAddress1.TextLength==0)
			{
				MessageBox.Show("Please enter the Address",GlobalFn.FormText);
				//TurnOnSave();
				return;
			}
			
			if (txtAmount.TextLength > 0)
			{
				try
				{
					amtPaid = Convert.ToDecimal(txtAmount.Text);
				}
				catch
				{
					MessageBox.Show("Please enter valid Amount",GlobalFn.FormText,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
					return;
				}
			}
      if (txtDiscount.TextLength > 0)
      {
        try
        {
            discount = Convert.ToDecimal(txtDiscount.Text);
        }
        catch
        {
            MessageBox.Show("Please enter valid Discount", GlobalFn.FormText, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            return;
        }
      }
			if (txtCity.TextLength==0)
			{
				MessageBox.Show("Please enter the City",GlobalFn.FormText);
				//TurnOnSave();
				return;
			}
           
			if (txtCopies.TextLength==0)
			{
				MessageBox.Show("Please enter the Number of Copies",GlobalFn.FormText);
				//TurnOnSave();
				return;
			}

			if (!GlobalFn.IsNumeric(txtCopies.Text))			
			{
				MessageBox.Show("Copies must be a number",GlobalFn.FormText);
				//TurnOnSave();
				return;
			}
      if (txtPinCode.TextLength > 0)
      {
          if (!GlobalFn.IsNumeric(txtPinCode.Text))
          {
              MessageBox.Show("PinCode must be a number", GlobalFn.FormText);
              //TurnOnSave();
              return;
          }
      }
      else
          txtPinCode.Text = "0";
       
      if (textBoxMobileNumber.Text.Length == 0)
      {
        MessageBox.Show("Please enter Mobile Number", GlobalFn.FormText);
        return;
      }
            if (textBoxMobileNumber.Text.Length != 10)
            {
                MessageBox.Show("Please enter a valid Mobile Number", GlobalFn.FormText);
                return;
            }
            if (!GlobalFn.IsNumeric(textBoxMobileNumber.Text))
            {
                MessageBox.Show("Mobile number must be in valid format", GlobalFn.FormText);
                //TurnOnSave();
                return;
            }
       

            if (txtBillNo.TextLength > 0)
      {
          if (!GlobalFn.IsNumeric(txtBillNo.Text))
          {
              MessageBox.Show("Bill Number must be a number", GlobalFn.FormText);
              //TurnOnSave();
              return;
          }
      }
      else
          txtBillNo.Text = "0";
       
			try
			{
			  int rtrn =	SQL.UpdateModifySubscriber(subId,subCode,cboTitle.Text, txtLastName.Text, txtFirstName.Text, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, txtCity.Text, txtDistrict.Text,Convert.ToInt32(cboStates.SelectedValue),Convert.ToInt32(txtPinCode.Text), textBoxMobileNumber.Text, cboCountries.SelectedValue.ToString(), cboStatus.SelectedItem.ToString(),txtRemarks.Text,languageId,amtPaid,Convert.ToInt32(txtCopies.Text),dtpBillDate.Value,Convert.ToInt32(txtBillNo.Text),category,discount,makeReceipt);

        DataSet dsUpdate = SQL.GetSubscriberInfo(subId.ToString());
        if (dsUpdate.Tables[0].Rows.Count > 0)
        {
          DataRow row = dsUpdate.Tables[0].Rows[0];
          object[] dueDate = new object[2];
          dueDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(row["start_date"]), Convert.ToDouble(row["amount_paid"]), Convert.ToDouble(row["discount"]), Convert.ToInt32(row["num_copies"]));
          //lblBalance.Text = dueDate[1].ToString();
          //lblDueDate.Text = Convert.ToDateTime(dueDate[0]).ToString("dd/MM/yyyy");
          string msg = "Subscription has been renewed successfully!\r\n\r\n" +
                       "Amount Paid:" + row["amount_paid"].ToString() + "\r\n\r\n" +
                       "Balance:" + dueDate[1].ToString() + "\r\n\r\n" +
                       "Due Date:" + Convert.ToDateTime(dueDate[0]).ToString("dd/MM/yyyy");
          MessageBox.Show(msg,GlobalFn.FormText,MessageBoxButtons.OK,MessageBoxIcon.Information);

        }

        if (MessageBox.Show("Do you want to Print Receipt?", GlobalFn.FormText, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
            FrmPrintBill frmBill = new FrmPrintBill(subId, dtpBillDate.Value, false);
            frmBill.ShowDialog();
            this.Close();
        }
        else
            this.Close();


      }
			catch(Exception eLang)
			{
				MessageBox.Show("Database error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eLang,"Error in Binding Item Types in NewSubscriptions.cs");
				return;
			}
		}

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAddress1_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress1.TextLength > 30)
            {
                MessageBox.Show("Address1 is " + txtAddress1.TextLength.ToString() + "long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
            }
        }

        private void txtAddress2_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress2.TextLength > 30)
            {
                MessageBox.Show("Address2 is " + txtAddress2.TextLength + " long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
            }
        }

        private void txtAddress3_TextChanged(object sender, EventArgs e)
        {
            if (txtAddress3.TextLength > 30)
            {
                MessageBox.Show("Address3 is " + txtAddress3.TextLength.ToString() +" long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
            }
        }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {
            if (txtFirstName.TextLength > 30)
            {
                MessageBox.Show("First Name is " + txtFirstName.TextLength + " long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
            }
        }
    private void cboCountries_SelectionChangeCommitted(object sender, EventArgs e)
    {
      BindStates();
    }

    private void ButtonNewCountry_Click(object sender, EventArgs e)
    {
      frmManageCountries objNew = new frmManageCountries(true);
      objNew.ShowDialog();
      RefreshCountries();
    }

    private void ButtonAddState_Click(object sender, EventArgs e)
    {
      frmManageStates objNew = new frmManageStates(true);
      objNew.ShowDialog();
      RefreshStates();
    }
    private void RefreshCountries()
    {
      dtCountries.Clear();
      dtCountries = SQL.GetCountries().Tables[0];
      cboCountries.DataSource = dtCountries;
      if (cboCountries.Items.Count > 0)
      {
        int countIndex = cboCountries.FindString("India");
        if (countIndex > 0)
          cboCountries.SelectedIndex = countIndex;
      }

      //BindStates();
    }
    private void RefreshStates()
    {
      dtStates.Clear();
      dtStates = SQL.SubscribersGetStates(cboCountries.SelectedValue.ToString()).Tables[0];
      cboStates.DataSource = dtStates;
    }
	}
}
