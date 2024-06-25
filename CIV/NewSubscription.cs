using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CIV.Classess;
using System.Data;
using System.Text;

namespace CIV
{
	/// <summary>
	/// Summary description for Form2.
	/// </summary>
	public class frmNewSub : frmBaseForm
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label_LName;
		private System.Windows.Forms.Label label_Title;
		private System.Windows.Forms.Label label_FName;
		private System.Windows.Forms.ComboBox cboTitle;
		private System.Windows.Forms.ComboBox cboMagazine;
		private System.Windows.Forms.Label label_Copies;
		private System.Windows.Forms.Label label_Magazine;
		private System.Windows.Forms.Label label_Remarks;
		private System.Windows.Forms.Label label_PinCode;
		private System.Windows.Forms.Label label_Address3;
		private System.Windows.Forms.Label label_Address2;
		private System.Windows.Forms.Label label_Address1;
		private System.Windows.Forms.Label label_BillNo;
		private System.Windows.Forms.Label label_PaymentDt;
		private System.Windows.Forms.Label label_Amount;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cboStates;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtDistrict;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.TextBox txtAmount;
		private System.Windows.Forms.TextBox txtBillNo;
		private System.Windows.Forms.TextBox txtCopies;
		private System.Windows.Forms.TextBox txtRemarks;
		private System.Windows.Forms.TextBox txtPinCode;
		private System.Windows.Forms.TextBox txtAddress3;
		private System.Windows.Forms.TextBox txtAddress2;
		private System.Windows.Forms.TextBox txtAddress1;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.DateTimePicker dtpBillDate;
		private System.Windows.Forms.Label label10;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblSubcode;
    private RadioButton rdoUnAvail;
    private RadioButton rdoAvail;
    private Label label11;
    private TextBox txtSubCode;
    private TextBox txtDiscount;
    private Label label14;
    private ComboBox cboCategory;
    private Label label13;
    private DateTimePicker dtpStartDate;
    private Label label15;
    private CheckBox chkMakeReceipt;
    private ComboBox cboCountries;
    private Label label16;
    private Button ButtonAddState;
    private Button ButtonNewCountry;
		string subCode="";
    DataTable dtCountries = new DataTable();
        private TextBox textBoxMobileNumber;
        private Label label17;
        DataTable dtStates = new DataTable();
		public frmNewSub()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            if (SessionManager.CurrentUser != null && SessionManager.CurrentUser.IsAuthenticated)
            {
                if (SessionManager.CurrentUser.Role.ToLower() == "admin")
                {
                    chkMakeReceipt.Visible = true;
                }
                else
                {
                    chkMakeReceipt.Visible = false;
                }
            }
            else
            {
                chkMakeReceipt.Visible = false;
            }
            this.Text += " - " + GlobalFn.FormText;
					
			dtpBillDate.CustomFormat = "dd/MM/yyyy";
			dtpBillDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

      dtpStartDate.CustomFormat = "dd/MM/yyyy";
      dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;

			BindStates();
			if (cboTitle.Items.Count > 0)
				cboTitle.SelectedIndex=0;
			
      BindLanguages();
			if (cboMagazine.Items.Count > 0)
			  cboMagazine.SelectedIndex=0;

      BindCountries();
      if (cboCountries.Items.Count > 0)
      {
        int countIndex  = cboCountries.FindString("India");
        if (countIndex > 0)
          cboCountries.SelectedIndex = countIndex;
      }

      if (cboCategory.Items.Count > 0)
      {
        int selIndex = cboCategory.FindString("General");
        if (selIndex > 0)
            cboCategory.SelectedIndex = selIndex;
      }
    }

		private void BindStates()
		{
      if ((cboCountries.Items.Count > 0) && (cboCountries.SelectedIndex >= 0))
      {
        try
        {
          cboStates.ValueMember = "state_ID";
          cboStates.DisplayMember = "name";
          dtStates =  SQL.SubscribersGetStates(cboCountries.SelectedValue.ToString()).Tables[0];
          cboStates.DataSource = dtStates;
        }
        catch (Exception eItems)
        {
          MessageBox.Show("Database error...", GlobalFn.FormText);
          GlobalFn.ProcessException(eItems, "Error in Binding Items list box in NewSubscriptions.cs");
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
          GlobalFn.ProcessException(eItems, "Error in Binding Countries drop down in NewSubscriptions.cs");
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
			catch(Exception eItems)
			{
				MessageBox.Show("Database error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eItems,"Error in Binding Items list box in NewSubscriptions.cs");	
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
            this.label_LName = new System.Windows.Forms.Label();
            this.label_Title = new System.Windows.Forms.Label();
            this.label_FName = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxMobileNumber = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.ButtonAddState = new System.Windows.Forms.Button();
            this.ButtonNewCountry = new System.Windows.Forms.Button();
            this.chkMakeReceipt = new System.Windows.Forms.CheckBox();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.label15 = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSubCode = new System.Windows.Forms.TextBox();
            this.rdoUnAvail = new System.Windows.Forms.RadioButton();
            this.rdoAvail = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.lblSubcode = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dtpBillDate = new System.Windows.Forms.DateTimePicker();
            this.txtDistrict = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboCountries = new System.Windows.Forms.ComboBox();
            this.cboStates = new System.Windows.Forms.ComboBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label_Amount = new System.Windows.Forms.Label();
            this.label_PaymentDt = new System.Windows.Forms.Label();
            this.txtBillNo = new System.Windows.Forms.TextBox();
            this.label_BillNo = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.cboTitle = new System.Windows.Forms.ComboBox();
            this.cboMagazine = new System.Windows.Forms.ComboBox();
            this.txtCopies = new System.Windows.Forms.TextBox();
            this.txtRemarks = new System.Windows.Forms.TextBox();
            this.txtPinCode = new System.Windows.Forms.TextBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.txtAddress1 = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.label_Copies = new System.Windows.Forms.Label();
            this.label_Magazine = new System.Windows.Forms.Label();
            this.label_Remarks = new System.Windows.Forms.Label();
            this.label_PinCode = new System.Windows.Forms.Label();
            this.label_Address3 = new System.Windows.Forms.Label();
            this.label_Address2 = new System.Windows.Forms.Label();
            this.label_Address1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_LName
            // 
            this.label_LName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_LName.Location = new System.Drawing.Point(53, 128);
            this.label_LName.Name = "label_LName";
            this.label_LName.Size = new System.Drawing.Size(80, 16);
            this.label_LName.TabIndex = 0;
            this.label_LName.Text = "Last Name:";
            this.label_LName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_Title
            // 
            this.label_Title.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Title.Location = new System.Drawing.Point(93, 104);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(40, 16);
            this.label_Title.TabIndex = 1;
            this.label_Title.Text = "Title:";
            this.label_Title.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_FName
            // 
            this.label_FName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_FName.Location = new System.Drawing.Point(51, 151);
            this.label_FName.Name = "label_FName";
            this.label_FName.Size = new System.Drawing.Size(82, 16);
            this.label_FName.TabIndex = 2;
            this.label_FName.Text = "First Name:";
            this.label_FName.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxMobileNumber);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.ButtonAddState);
            this.groupBox1.Controls.Add(this.ButtonNewCountry);
            this.groupBox1.Controls.Add(this.chkMakeReceipt);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txtDiscount);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.cboCategory);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtSubCode);
            this.groupBox1.Controls.Add(this.rdoUnAvail);
            this.groupBox1.Controls.Add(this.rdoAvail);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblSubcode);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dtpBillDate);
            this.groupBox1.Controls.Add(this.txtDistrict);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cboCountries);
            this.groupBox1.Controls.Add(this.cboStates);
            this.groupBox1.Controls.Add(this.txtCity);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label_Amount);
            this.groupBox1.Controls.Add(this.label_PaymentDt);
            this.groupBox1.Controls.Add(this.txtBillNo);
            this.groupBox1.Controls.Add(this.label_BillNo);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.cboTitle);
            this.groupBox1.Controls.Add(this.cboMagazine);
            this.groupBox1.Controls.Add(this.txtCopies);
            this.groupBox1.Controls.Add(this.txtRemarks);
            this.groupBox1.Controls.Add(this.txtPinCode);
            this.groupBox1.Controls.Add(this.txtAddress3);
            this.groupBox1.Controls.Add(this.txtAddress2);
            this.groupBox1.Controls.Add(this.txtAddress1);
            this.groupBox1.Controls.Add(this.txtLastName);
            this.groupBox1.Controls.Add(this.txtFirstName);
            this.groupBox1.Controls.Add(this.label_Copies);
            this.groupBox1.Controls.Add(this.label_Magazine);
            this.groupBox1.Controls.Add(this.label_Remarks);
            this.groupBox1.Controls.Add(this.label_PinCode);
            this.groupBox1.Controls.Add(this.label_Address3);
            this.groupBox1.Controls.Add(this.label_Address2);
            this.groupBox1.Controls.Add(this.label_Address1);
            this.groupBox1.Controls.Add(this.label_Title);
            this.groupBox1.Controls.Add(this.label_LName);
            this.groupBox1.Controls.Add(this.label_FName);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(8, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(729, 369);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // textBoxMobileNumber
            // 
            this.textBoxMobileNumber.Location = new System.Drawing.Point(432, 106);
            this.textBoxMobileNumber.MaxLength = 10;
            this.textBoxMobileNumber.Name = "textBoxMobileNumber";
            this.textBoxMobileNumber.Size = new System.Drawing.Size(204, 21);
            this.textBoxMobileNumber.TabIndex = 14;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(325, 107);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(108, 13);
            this.label17.TabIndex = 59;
            this.label17.Text = "Mobile Number:";
            // 
            // ButtonAddState
            // 
            this.ButtonAddState.Location = new System.Drawing.Point(625, 50);
            this.ButtonAddState.Name = "ButtonAddState";
            this.ButtonAddState.Size = new System.Drawing.Size(98, 22);
            this.ButtonAddState.TabIndex = 58;
            this.ButtonAddState.Text = "Add State";
            this.ButtonAddState.UseVisualStyleBackColor = true;
            this.ButtonAddState.Click += new System.EventHandler(this.ButtonAddState_Click);
            // 
            // ButtonNewCountry
            // 
            this.ButtonNewCountry.Location = new System.Drawing.Point(625, 25);
            this.ButtonNewCountry.Name = "ButtonNewCountry";
            this.ButtonNewCountry.Size = new System.Drawing.Size(98, 22);
            this.ButtonNewCountry.TabIndex = 57;
            this.ButtonNewCountry.Text = "Add Country";
            this.ButtonNewCountry.UseVisualStyleBackColor = true;
            this.ButtonNewCountry.Click += new System.EventHandler(this.ButtonNewCountry_Click);
            // 
            // chkMakeReceipt
            // 
            this.chkMakeReceipt.AutoSize = true;
            this.chkMakeReceipt.ForeColor = System.Drawing.Color.Red;
            this.chkMakeReceipt.Location = new System.Drawing.Point(534, 338);
            this.chkMakeReceipt.Name = "chkMakeReceipt";
            this.chkMakeReceipt.Size = new System.Drawing.Size(159, 17);
            this.chkMakeReceipt.TabIndex = 24;
            this.chkMakeReceipt.Text = "Do Not Make Receipt";
            this.chkMakeReceipt.UseVisualStyleBackColor = true;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Location = new System.Drawing.Point(432, 203);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(103, 21);
            this.dtpStartDate.TabIndex = 18;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(355, 206);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 13);
            this.label15.TabIndex = 56;
            this.label15.Text = "Start Date:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiscount.Location = new System.Drawing.Point(432, 125);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(56, 21);
            this.txtDiscount.TabIndex = 15;
            this.txtDiscount.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(366, 127);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 52;
            this.label14.Text = "Discount:";
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
            this.cboCategory.Location = new System.Drawing.Point(133, 55);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(127, 21);
            this.cboCategory.TabIndex = 2;
            this.cboCategory.SelectedIndexChanged += new System.EventHandler(this.cboCategory_SelectedIndexChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(64, 58);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(70, 13);
            this.label13.TabIndex = 50;
            this.label13.Text = "Category:";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubCode
            // 
            this.txtSubCode.Location = new System.Drawing.Point(133, 79);
            this.txtSubCode.Name = "txtSubCode";
            this.txtSubCode.Size = new System.Drawing.Size(103, 21);
            this.txtSubCode.TabIndex = 3;
            this.txtSubCode.Visible = false;
            // 
            // rdoUnAvail
            // 
            this.rdoUnAvail.AutoSize = true;
            this.rdoUnAvail.Checked = true;
            this.rdoUnAvail.Location = new System.Drawing.Point(225, 11);
            this.rdoUnAvail.Name = "rdoUnAvail";
            this.rdoUnAvail.Size = new System.Drawing.Size(102, 17);
            this.rdoUnAvail.TabIndex = 48;
            this.rdoUnAvail.TabStop = true;
            this.rdoUnAvail.Text = "Unavailable";
            this.rdoUnAvail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoUnAvail.UseVisualStyleBackColor = true;
            this.rdoUnAvail.CheckedChanged += new System.EventHandler(this.rdoUnAvail_CheckedChanged);
            // 
            // rdoAvail
            // 
            this.rdoAvail.AutoSize = true;
            this.rdoAvail.Location = new System.Drawing.Point(133, 11);
            this.rdoAvail.Name = "rdoAvail";
            this.rdoAvail.Size = new System.Drawing.Size(86, 17);
            this.rdoAvail.TabIndex = 47;
            this.rdoAvail.Text = "Available";
            this.rdoAvail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rdoAvail.UseVisualStyleBackColor = true;
            this.rdoAvail.CheckedChanged += new System.EventHandler(this.rdoAvail_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(62, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(71, 13);
            this.label11.TabIndex = 46;
            this.label11.Text = "Sub Code:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubcode
            // 
            this.lblSubcode.Location = new System.Drawing.Point(136, 80);
            this.lblSubcode.Name = "lblSubcode";
            this.lblSubcode.Size = new System.Drawing.Size(100, 16);
            this.lblSubcode.TabIndex = 45;
            this.lblSubcode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(5, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(129, 16);
            this.label10.TabIndex = 44;
            this.label10.Text = "Subscription Code:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(519, 82);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 13);
            this.label12.TabIndex = 43;
            this.label12.Text = "*";
            // 
            // label9
            // 
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(489, 135);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 16);
            this.label9.TabIndex = 40;
            this.label9.Text = "*";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(285, 245);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(16, 16);
            this.label8.TabIndex = 39;
            this.label8.Text = "*";
            // 
            // label7
            // 
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(489, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 16);
            this.label7.TabIndex = 38;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(349, 178);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 16);
            this.label6.TabIndex = 37;
            this.label6.Text = "*";
            // 
            // label5
            // 
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(301, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(16, 16);
            this.label5.TabIndex = 36;
            this.label5.Text = "*";
            // 
            // dtpBillDate
            // 
            this.dtpBillDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpBillDate.Location = new System.Drawing.Point(432, 250);
            this.dtpBillDate.Name = "dtpBillDate";
            this.dtpBillDate.Size = new System.Drawing.Size(104, 21);
            this.dtpBillDate.TabIndex = 20;
            // 
            // txtDistrict
            // 
            this.txtDistrict.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistrict.Location = new System.Drawing.Point(133, 273);
            this.txtDistrict.Name = "txtDistrict";
            this.txtDistrict.Size = new System.Drawing.Size(152, 21);
            this.txtDistrict.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(73, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 33;
            this.label3.Text = "District:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCountries
            // 
            this.cboCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCountries.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCountries.Location = new System.Drawing.Point(433, 25);
            this.cboCountries.Name = "cboCountries";
            this.cboCountries.Size = new System.Drawing.Size(192, 21);
            this.cboCountries.TabIndex = 14;
            this.cboCountries.SelectedIndexChanged += new System.EventHandler(this.cboCountries_SelectedIndexChanged);
            // 
            // cboStates
            // 
            this.cboStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStates.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboStates.Location = new System.Drawing.Point(433, 52);
            this.cboStates.Name = "cboStates";
            this.cboStates.Size = new System.Drawing.Size(192, 21);
            this.cboStates.TabIndex = 12;
            // 
            // txtCity
            // 
            this.txtCity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCity.Location = new System.Drawing.Point(133, 248);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(152, 21);
            this.txtCity.TabIndex = 10;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(367, 26);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(66, 16);
            this.label16.TabIndex = 30;
            this.label16.Text = "Country:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(385, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "State:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(93, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "City:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAmount.Location = new System.Drawing.Point(432, 275);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(56, 21);
            this.txtAmount.TabIndex = 21;
            this.txtAmount.Tag = "";
            this.txtAmount.Text = "250.00";
            // 
            // label_Amount
            // 
            this.label_Amount.Location = new System.Drawing.Point(368, 276);
            this.label_Amount.Name = "label_Amount";
            this.label_Amount.Size = new System.Drawing.Size(64, 16);
            this.label_Amount.TabIndex = 26;
            this.label_Amount.Text = "Amount:";
            this.label_Amount.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_PaymentDt
            // 
            this.label_PaymentDt.Location = new System.Drawing.Point(360, 253);
            this.label_PaymentDt.Name = "label_PaymentDt";
            this.label_PaymentDt.Size = new System.Drawing.Size(72, 16);
            this.label_PaymentDt.TabIndex = 25;
            this.label_PaymentDt.Text = "Bill Date:";
            this.label_PaymentDt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBillNo
            // 
            this.txtBillNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBillNo.Location = new System.Drawing.Point(432, 227);
            this.txtBillNo.Name = "txtBillNo";
            this.txtBillNo.Size = new System.Drawing.Size(96, 21);
            this.txtBillNo.TabIndex = 19;
            // 
            // label_BillNo
            // 
            this.label_BillNo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_BillNo.Location = new System.Drawing.Point(376, 228);
            this.label_BillNo.Name = "label_BillNo";
            this.label_BillNo.Size = new System.Drawing.Size(56, 16);
            this.label_BillNo.TabIndex = 23;
            this.label_BillNo.Text = "Bill no:";
            this.label_BillNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(352, 310);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(152, 24);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(197, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(147, 24);
            this.btnSave.TabIndex = 22;
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
            "The.",
            "",
            ""});
            this.cboTitle.Location = new System.Drawing.Point(133, 103);
            this.cboTitle.Name = "cboTitle";
            this.cboTitle.Size = new System.Drawing.Size(80, 21);
            this.cboTitle.TabIndex = 4;
            // 
            // cboMagazine
            // 
            this.cboMagazine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMagazine.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMagazine.Location = new System.Drawing.Point(133, 32);
            this.cboMagazine.Name = "cboMagazine";
            this.cboMagazine.Size = new System.Drawing.Size(152, 21);
            this.cboMagazine.TabIndex = 1;
            this.cboMagazine.SelectedIndexChanged += new System.EventHandler(this.cboMagazine_SelectedIndexChanged);
            // 
            // txtCopies
            // 
            this.txtCopies.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCopies.Location = new System.Drawing.Point(432, 152);
            this.txtCopies.Name = "txtCopies";
            this.txtCopies.Size = new System.Drawing.Size(56, 21);
            this.txtCopies.TabIndex = 16;
            this.txtCopies.Text = "1";
            // 
            // txtRemarks
            // 
            this.txtRemarks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRemarks.Location = new System.Drawing.Point(432, 178);
            this.txtRemarks.Multiline = true;
            this.txtRemarks.Name = "txtRemarks";
            this.txtRemarks.Size = new System.Drawing.Size(216, 20);
            this.txtRemarks.TabIndex = 17;
            // 
            // txtPinCode
            // 
            this.txtPinCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPinCode.Location = new System.Drawing.Point(433, 79);
            this.txtPinCode.MaxLength = 6;
            this.txtPinCode.Name = "txtPinCode";
            this.txtPinCode.Size = new System.Drawing.Size(80, 21);
            this.txtPinCode.TabIndex = 13;
            this.txtPinCode.Text = "0";
            // 
            // txtAddress3
            // 
            this.txtAddress3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress3.Location = new System.Drawing.Point(133, 223);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(216, 21);
            this.txtAddress3.TabIndex = 9;
            // 
            // txtAddress2
            // 
            this.txtAddress2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress2.Location = new System.Drawing.Point(133, 199);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(216, 21);
            this.txtAddress2.TabIndex = 8;
            // 
            // txtAddress1
            // 
            this.txtAddress1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress1.Location = new System.Drawing.Point(133, 175);
            this.txtAddress1.Name = "txtAddress1";
            this.txtAddress1.Size = new System.Drawing.Size(216, 21);
            this.txtAddress1.TabIndex = 7;
            // 
            // txtLastName
            // 
            this.txtLastName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLastName.Location = new System.Drawing.Point(133, 127);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(168, 21);
            this.txtLastName.TabIndex = 5;
            // 
            // txtFirstName
            // 
            this.txtFirstName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFirstName.Location = new System.Drawing.Point(133, 151);
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(168, 21);
            this.txtFirstName.TabIndex = 6;
            this.txtFirstName.TextChanged += new System.EventHandler(this.txtFirstName_TextChanged);
            // 
            // label_Copies
            // 
            this.label_Copies.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Copies.Location = new System.Drawing.Point(371, 152);
            this.label_Copies.Name = "label_Copies";
            this.label_Copies.Size = new System.Drawing.Size(61, 15);
            this.label_Copies.TabIndex = 9;
            this.label_Copies.Text = "Copies:";
            this.label_Copies.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Magazine
            // 
            this.label_Magazine.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Magazine.Location = new System.Drawing.Point(61, 33);
            this.label_Magazine.Name = "label_Magazine";
            this.label_Magazine.Size = new System.Drawing.Size(72, 16);
            this.label_Magazine.TabIndex = 8;
            this.label_Magazine.Text = "Magazine:";
            this.label_Magazine.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_Remarks
            // 
            this.label_Remarks.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Remarks.Location = new System.Drawing.Point(361, 179);
            this.label_Remarks.Name = "label_Remarks";
            this.label_Remarks.Size = new System.Drawing.Size(72, 16);
            this.label_Remarks.TabIndex = 7;
            this.label_Remarks.Text = "Remarks:";
            this.label_Remarks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_PinCode
            // 
            this.label_PinCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_PinCode.Location = new System.Drawing.Point(364, 79);
            this.label_PinCode.Name = "label_PinCode";
            this.label_PinCode.Size = new System.Drawing.Size(69, 16);
            this.label_PinCode.TabIndex = 6;
            this.label_PinCode.Text = "PinCode:";
            this.label_PinCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Address3
            // 
            this.label_Address3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Address3.Location = new System.Drawing.Point(53, 223);
            this.label_Address3.Name = "label_Address3";
            this.label_Address3.Size = new System.Drawing.Size(80, 16);
            this.label_Address3.TabIndex = 5;
            this.label_Address3.Text = "Address 3:";
            this.label_Address3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_Address2
            // 
            this.label_Address2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Address2.Location = new System.Drawing.Point(54, 199);
            this.label_Address2.Name = "label_Address2";
            this.label_Address2.Size = new System.Drawing.Size(79, 16);
            this.label_Address2.TabIndex = 4;
            this.label_Address2.Text = "Address 2:";
            this.label_Address2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label_Address1
            // 
            this.label_Address1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Address1.Location = new System.Drawing.Point(56, 175);
            this.label_Address1.Name = "label_Address1";
            this.label_Address1.Size = new System.Drawing.Size(77, 16);
            this.label_Address1.TabIndex = 3;
            this.label_Address1.Text = "Address 1:";
            this.label_Address1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(248, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "* - Required Field";
            // 
            // frmNewSub
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(900, 438);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmNewSub";
            this.Text = "New Subscription";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void btnSave_Click(object sender, System.EventArgs e)
		{
      string category = "";
      string langId = cboMagazine.SelectedValue.ToString();
      string makeReceipt="Y";
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
			if (txtFirstName.TextLength==0)
			{
				MessageBox.Show("Please enter the name",GlobalFn.FormText);
				//TurnOnSave();
				return;
			}

			if (txtAddress1.TextLength==0)
			{
				MessageBox.Show("Please enter the Address");
				//TurnOnSave();
				return;
			}
			if (txtAmount.TextLength==0)
			{
				MessageBox.Show("Please enter the Amount");
				//TurnOnSave();
				return;
			}
			if (txtAmount.TextLength > 0)
			{
			  try
			  {
				  Convert.ToDecimal(txtAmount.Text);
			  }
			  catch
			  {
				  MessageBox.Show("Please valid Amount",GlobalFn.FormText,MessageBoxButtons.OKCancel,MessageBoxIcon.Warning);
				  return;
			  }
			}
			if (txtCity.TextLength==0)
			{
				MessageBox.Show("Please enter the City");
				//TurnOnSave();
				return;
			}
			if (txtCopies.TextLength==0)
			{
				MessageBox.Show("Please enter the Number of Copies");
				//TurnOnSave();
				return;
			}

			if (!GlobalFn.IsNumeric(txtCopies.Text))			
			{
				MessageBox.Show("Copies must be a number");
				//TurnOnSave();
				return;
			}

			if (!GlobalFn.IsNumeric(txtPinCode.Text))			
			{
				MessageBox.Show("Pin Code must be a number");
				//TurnOnSave();
				return;
			}
            if (textBoxMobileNumber.Text.Length == 0)
            {
                MessageBox.Show("Mobile Number must be in valid format", GlobalFn.FormText);
                //TurnOnSave();
                return;
            }
            if (textBoxMobileNumber.Text.Length != 10)
            {
                MessageBox.Show("Please enter a valid Mobile Number!", GlobalFn.FormText);
                //TurnOnSave();
                return;
            }
            if (!GlobalFn.IsNumeric(textBoxMobileNumber.Text))
            {
                MessageBox.Show("Mobile Number must be a number", GlobalFn.FormText);
                //TurnOnSave();
                return;
            }
            if (rdoAvail.Checked)
      {
        if (txtSubCode.Text.Length == 0)
        {
            MessageBox.Show("Please enter Subscription Code");
            return;
        }
        subCode = txtSubCode.Text;
      }
      // check dupe sub record.
      try
      {
        DataSet dsDupe = SQL.NewSubscriberDupeRec(subCode, langId);
        if (dsDupe.Tables[0].Rows.Count > 0)
        {
          MessageBox.Show("This subscriber code exists for this magazine.", GlobalFn.FormText);
          return;
        }
      }
      catch (Exception eDupe)
      {
        MessageBox.Show("Database error...", GlobalFn.FormText);
        GlobalFn.ProcessException(eDupe, "Error in getting dupe recs in NewSubscriptions.cs");
        return;
      }
      if (chkMakeReceipt.Checked)
          makeReceipt = "N";
      else
          makeReceipt = "Y";
			try
			{
			  int subID = SQL.AddNewSubscriber(subCode,cboTitle.SelectedItem.ToString(),txtLastName.Text,txtFirstName.Text,txtAddress1.Text,txtAddress2.Text,txtAddress3.Text,txtCity.Text,txtDistrict.Text,cboStates.SelectedValue.ToString(),txtPinCode.Text, textBoxMobileNumber.Text, cboCountries.SelectedValue.ToString(),"A",txtRemarks.Text,dtpStartDate.Value,dtpBillDate.Value,txtAmount.Text,langId,txtCopies.Text,txtBillNo.Text,category,Convert.ToDouble(txtDiscount.Text) 
                 , makeReceipt);
        // Get an automatic receipt id and display it.
        try
        {
          string receiptId = SQL.NewSubscriptionReceiptId(subCode, langId);
          MessageBox.Show("New Subscription has been saved!\r\n\r\n Receipt ID: " + receiptId, GlobalFn.FormText, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception eRecpt)
        {
          MessageBox.Show("Database error...", GlobalFn.FormText);
          GlobalFn.ProcessException(eRecpt, "Error in retrieving Automatic Receipt Id in NewSubscriptions.cs");
          return;
        }
        if (MessageBox.Show("Do you want to Print Receipt?", GlobalFn.FormText, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FrmPrintBill frmBill = new FrmPrintBill(subID, dtpBillDate.Value,false);
          frmBill.ShowDialog();
        }
        this.NewSub();
			}
			catch(Exception eLang)
			{
				MessageBox.Show("Database error...",GlobalFn.FormText);
				GlobalFn.ProcessException(eLang,"Error in saving new subscription in NewSubscriptions.cs");
				return;
			}
				
		}

    private bool IsFirstLetterValid(String fLetter)
    {
      ASCIIEncoding ascii = new ASCIIEncoding();
      Byte[] driveASCII = ascii.GetBytes(fLetter);

      if (driveASCII[0] >= 65 && driveASCII[0] <= 90)
          return true;
      else
          return false;
    }
		private void txtFirstName_TextChanged(object sender, System.EventArgs e)
		{
			int maxCode = 0;
			string firstName = txtFirstName.Text.Trim();
			string category = cboCategory.SelectedItem.ToString();
      bool isBulk = false;
			if (firstName.Length == 0)
				return;
      if (rdoUnAvail.Checked)
      {
        string fLetter = "";
        if (category.Equals("Bulk"))
        {
            fLetter = "BL";
            isBulk = true;
        }
        else
            fLetter = firstName.Substring(0, 1).ToUpper();
        if (!IsFirstLetterValid(fLetter))
        {
            MessageBox.Show("Please enter valid First name", GlobalFn.FormText);
            txtFirstName.Text = "";
            return;
        }
        DataSet ds = SQL.NewSubsciberGetSubCode(fLetter,cboMagazine.SelectedValue.ToString(),isBulk);

        if (ds.Tables[0].Rows[0]["max_code"] == DBNull.Value)
        {
          if (isBulk)
              subCode = fLetter + "001";
          else
              subCode = fLetter + "0001";
        }
        else
        {
          if (!GlobalFn.IsNumeric(ds.Tables[0].Rows[0]["max_code"]))
          {
              MessageBox.Show("Corrupted Sub Code in the data. Do not enter this subscription!",GlobalFn.FormText,MessageBoxButtons.OK,MessageBoxIcon.Error);
              return;
          }
          maxCode = Convert.ToInt32(ds.Tables[0].Rows[0]["max_code"]);

          maxCode = maxCode + 1;
          if (maxCode < 10)
          {
            if (isBulk)
                subCode = fLetter + "00" + maxCode;
            else
                subCode = fLetter + "000" + maxCode;
          }
          else if (maxCode < 100)
          {
            if (isBulk)
                subCode = fLetter + "0" + maxCode;
            else
                subCode = fLetter + "00" + maxCode;
          }
          else if (maxCode < 1000)
          {
            if (isBulk)
                subCode = fLetter + maxCode;
            else
                subCode = fLetter + "0" + maxCode;
          }
          else
            subCode = fLetter + maxCode;
         }
         lblSubcode.Text = subCode;
      }
    }

    private void rdoUnAvail_CheckedChanged(object sender, EventArgs e)
    {
        lblSubcode.Visible = true;
        txtSubCode.Visible = false;
        txtFirstName_TextChanged(null, null);
    }

    private void rdoAvail_CheckedChanged(object sender, EventArgs e)
    {
        lblSubcode.Visible = false;
        txtSubCode.Visible = true;
    }

    private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
      txtDiscount.Text = "0";
      if (txtFirstName.TextLength > 0)
          txtFirstName_TextChanged(null, null);

      if (cboCategory.Text.Equals("Free"))
      {
          txtDiscount.Text = "100";
      }
      else if (cboCategory.Text.Equals("Student"))
          txtDiscount.Text = "20";
 
    }

    private void cboMagazine_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtFirstName.TextLength > 0)
            txtFirstName_TextChanged(null, null);
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
        this.NewSub();
    }

     

    private void cboCountries_SelectedIndexChanged(object sender, EventArgs e)
    {
      BindStates();
    }

    private void ButtonNewCountry_Click(object sender, EventArgs e)
    {
      frmManageCountries objNew = new frmManageCountries(true);
      objNew.ShowDialog();
      RefreshCountries();
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
    private void ButtonAddState_Click(object sender, EventArgs e)
    {
      frmManageStates objNew = new frmManageStates(true);
      objNew.ShowDialog();
      RefreshStates();
    }
    private void RefreshStates()
    {
      dtStates.Clear();
      dtStates = SQL.SubscribersGetStates(cboCountries.SelectedValue.ToString()).Tables[0];
      cboStates.DataSource = dtStates;
    }
	}

		

		
}

