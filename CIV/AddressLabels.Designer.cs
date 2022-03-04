namespace CIV
{
    partial class frmAddressLabels
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblPrintCriteria = new System.Windows.Forms.Label();
            this.grpPrint = new System.Windows.Forms.GroupBox();
            this.grpCategory = new System.Windows.Forms.GroupBox();
            this.rbtnGeneral = new System.Windows.Forms.RadioButton();
            this.rbtnFree = new System.Windows.Forms.RadioButton();
            this.rbtnBulk = new System.Windows.Forms.RadioButton();
            this.cboPrinters = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.districtPanel = new System.Windows.Forms.Panel();
            this.txtDistrictTo = new System.Windows.Forms.TextBox();
            this.txtDistrictFrom = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pinCodePanel = new System.Windows.Forms.Panel();
            this.txtPinCodeFrom = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPinCodeTo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.chkBoxPrep = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboMagazine = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpPostDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.cboPrintCriteria = new System.Windows.Forms.ComboBox();
            this.grpPrint.SuspendLayout();
            this.grpCategory.SuspendLayout();
            this.districtPanel.SuspendLayout();
            this.pinCodePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(140, 354);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(101, 31);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // lblPrintCriteria
            // 
            this.lblPrintCriteria.AutoSize = true;
            this.lblPrintCriteria.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrintCriteria.Location = new System.Drawing.Point(73, 170);
            this.lblPrintCriteria.Name = "lblPrintCriteria";
            this.lblPrintCriteria.Size = new System.Drawing.Size(64, 13);
            this.lblPrintCriteria.TabIndex = 1;
            this.lblPrintCriteria.Text = "Criteria :";
            this.lblPrintCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpPrint
            // 
            this.grpPrint.Controls.Add(this.grpCategory);
            this.grpPrint.Controls.Add(this.cboPrinters);
            this.grpPrint.Controls.Add(this.label8);
            this.grpPrint.Controls.Add(this.districtPanel);
            this.grpPrint.Controls.Add(this.pinCodePanel);
            this.grpPrint.Controls.Add(this.chkBoxPrep);
            this.grpPrint.Controls.Add(this.label3);
            this.grpPrint.Controls.Add(this.cboMagazine);
            this.grpPrint.Controls.Add(this.label2);
            this.grpPrint.Controls.Add(this.dtpPostDate);
            this.grpPrint.Controls.Add(this.label1);
            this.grpPrint.Controls.Add(this.cboPrintCriteria);
            this.grpPrint.Controls.Add(this.lblPrintCriteria);
            this.grpPrint.Controls.Add(this.btnPrint);
            this.grpPrint.Location = new System.Drawing.Point(12, 4);
            this.grpPrint.Name = "grpPrint";
            this.grpPrint.Size = new System.Drawing.Size(378, 397);
            this.grpPrint.TabIndex = 2;
            this.grpPrint.TabStop = false;
            // 
            // grpCategory
            // 
            this.grpCategory.Controls.Add(this.rbtnGeneral);
            this.grpCategory.Controls.Add(this.rbtnFree);
            this.grpCategory.Controls.Add(this.rbtnBulk);
            this.grpCategory.Location = new System.Drawing.Point(143, 65);
            this.grpCategory.Name = "grpCategory";
            this.grpCategory.Size = new System.Drawing.Size(149, 96);
            this.grpCategory.TabIndex = 18;
            this.grpCategory.TabStop = false;
            this.grpCategory.Text = "Category";
            // 
            // rbtnGeneral
            // 
            this.rbtnGeneral.AutoSize = true;
            this.rbtnGeneral.Checked = true;
            this.rbtnGeneral.Location = new System.Drawing.Point(30, 21);
            this.rbtnGeneral.Name = "rbtnGeneral";
            this.rbtnGeneral.Size = new System.Drawing.Size(62, 17);
            this.rbtnGeneral.TabIndex = 2;
            this.rbtnGeneral.TabStop = true;
            this.rbtnGeneral.Text = "General";
            this.rbtnGeneral.UseVisualStyleBackColor = true;
            // 
            // rbtnFree
            // 
            this.rbtnFree.AutoSize = true;
            this.rbtnFree.Location = new System.Drawing.Point(30, 67);
            this.rbtnFree.Name = "rbtnFree";
            this.rbtnFree.Size = new System.Drawing.Size(46, 17);
            this.rbtnFree.TabIndex = 1;
            this.rbtnFree.Text = "Free";
            this.rbtnFree.UseVisualStyleBackColor = true;
            // 
            // rbtnBulk
            // 
            this.rbtnBulk.AutoSize = true;
            this.rbtnBulk.Location = new System.Drawing.Point(30, 44);
            this.rbtnBulk.Name = "rbtnBulk";
            this.rbtnBulk.Size = new System.Drawing.Size(46, 17);
            this.rbtnBulk.TabIndex = 0;
            this.rbtnBulk.Text = "Bulk";
            this.rbtnBulk.UseVisualStyleBackColor = true;
            // 
            // cboPrinters
            // 
            this.cboPrinters.FormattingEnabled = true;
            this.cboPrinters.Location = new System.Drawing.Point(143, 17);
            this.cboPrinters.Name = "cboPrinters";
            this.cboPrinters.Size = new System.Drawing.Size(150, 21);
            this.cboPrinters.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(97, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Printer Name:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // districtPanel
            // 
            this.districtPanel.Controls.Add(this.txtDistrictTo);
            this.districtPanel.Controls.Add(this.txtDistrictFrom);
            this.districtPanel.Controls.Add(this.label6);
            this.districtPanel.Controls.Add(this.label5);
            this.districtPanel.Location = new System.Drawing.Point(35, 235);
            this.districtPanel.Name = "districtPanel";
            this.districtPanel.Size = new System.Drawing.Size(302, 34);
            this.districtPanel.TabIndex = 14;
            this.districtPanel.Visible = false;
            // 
            // txtDistrictTo
            // 
            this.txtDistrictTo.Location = new System.Drawing.Point(214, 7);
            this.txtDistrictTo.Name = "txtDistrictTo";
            this.txtDistrictTo.Size = new System.Drawing.Size(78, 21);
            this.txtDistrictTo.TabIndex = 6;
            // 
            // txtDistrictFrom
            // 
            this.txtDistrictFrom.Location = new System.Drawing.Point(108, 6);
            this.txtDistrictFrom.Name = "txtDistrictFrom";
            this.txtDistrictFrom.Size = new System.Drawing.Size(72, 21);
            this.txtDistrictFrom.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(186, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(27, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "To:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(10, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "District From:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pinCodePanel
            // 
            this.pinCodePanel.Controls.Add(this.txtPinCodeFrom);
            this.pinCodePanel.Controls.Add(this.label7);
            this.pinCodePanel.Controls.Add(this.txtPinCodeTo);
            this.pinCodePanel.Controls.Add(this.label4);
            this.pinCodePanel.Location = new System.Drawing.Point(35, 199);
            this.pinCodePanel.Name = "pinCodePanel";
            this.pinCodePanel.Size = new System.Drawing.Size(302, 37);
            this.pinCodePanel.TabIndex = 13;
            this.pinCodePanel.Visible = false;
            // 
            // txtPinCodeFrom
            // 
            this.txtPinCodeFrom.Location = new System.Drawing.Point(108, 9);
            this.txtPinCodeFrom.Name = "txtPinCodeFrom";
            this.txtPinCodeFrom.Size = new System.Drawing.Size(72, 21);
            this.txtPinCodeFrom.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(105, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Pin Code From:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPinCodeTo
            // 
            this.txtPinCodeTo.Location = new System.Drawing.Point(214, 9);
            this.txtPinCodeTo.Name = "txtPinCodeTo";
            this.txtPinCodeTo.Size = new System.Drawing.Size(78, 21);
            this.txtPinCodeTo.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(186, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "To:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkBoxPrep
            // 
            this.chkBoxPrep.AutoSize = true;
            this.chkBoxPrep.Checked = true;
            this.chkBoxPrep.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkBoxPrep.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkBoxPrep.Location = new System.Drawing.Point(143, 328);
            this.chkBoxPrep.Name = "chkBoxPrep";
            this.chkBoxPrep.Size = new System.Drawing.Size(145, 20);
            this.chkBoxPrep.TabIndex = 8;
            this.chkBoxPrep.Text = " Use Pre-Printed";
            this.chkBoxPrep.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(143, 239);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = " ";
            this.label3.Visible = false;
            // 
            // cboMagazine
            // 
            this.cboMagazine.FormattingEnabled = true;
            this.cboMagazine.ItemHeight = 13;
            this.cboMagazine.Location = new System.Drawing.Point(143, 41);
            this.cboMagazine.Name = "cboMagazine";
            this.cboMagazine.Size = new System.Drawing.Size(150, 21);
            this.cboMagazine.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(61, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Magazine :";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPostDate
            // 
            this.dtpPostDate.Location = new System.Drawing.Point(143, 292);
            this.dtpPostDate.Name = "dtpPostDate";
            this.dtpPostDate.Size = new System.Drawing.Size(150, 21);
            this.dtpPostDate.TabIndex = 7;
            this.dtpPostDate.Value = new System.DateTime(2007, 1, 27, 0, 0, 0, 0);
            this.dtpPostDate.ValueChanged += new System.EventHandler(this.dtpPostDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 296);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Date of Posting :";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPrintCriteria
            // 
            this.cboPrintCriteria.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboPrintCriteria.FormattingEnabled = true;
            this.cboPrintCriteria.Items.AddRange(new object[] {
            "Choose None",
            "Pin Code",
            "District"});
            this.cboPrintCriteria.Location = new System.Drawing.Point(143, 167);
            this.cboPrintCriteria.Name = "cboPrintCriteria";
            this.cboPrintCriteria.Size = new System.Drawing.Size(149, 21);
            this.cboPrintCriteria.TabIndex = 2;
            this.cboPrintCriteria.SelectedIndexChanged += new System.EventHandler(this.cboPrintCriteria_SelectedIndexChanged);
            // 
            // frmAddressLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 409);
            this.Controls.Add(this.grpPrint);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frmAddressLabels";
            this.Text = "Print - Address Labels";
            this.Load += new System.EventHandler(this.frmPrint_Load);
            this.grpPrint.ResumeLayout(false);
            this.grpPrint.PerformLayout();
            this.grpCategory.ResumeLayout(false);
            this.grpCategory.PerformLayout();
            this.districtPanel.ResumeLayout(false);
            this.districtPanel.PerformLayout();
            this.pinCodePanel.ResumeLayout(false);
            this.pinCodePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblPrintCriteria;
        private System.Windows.Forms.GroupBox grpPrint;
        private System.Windows.Forms.DateTimePicker dtpPostDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboPrintCriteria;
        private System.Windows.Forms.ComboBox cboMagazine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkBoxPrep;
        private System.Windows.Forms.Panel pinCodePanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPinCodeTo;
        private System.Windows.Forms.Panel districtPanel;
        private System.Windows.Forms.TextBox txtDistrictTo;
        private System.Windows.Forms.TextBox txtDistrictFrom;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPinCodeFrom;
      private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPrinters;
        private System.Windows.Forms.Label label8;
      private System.Windows.Forms.GroupBox grpCategory;
      private System.Windows.Forms.RadioButton rbtnFree;
      private System.Windows.Forms.RadioButton rbtnBulk;
      private System.Windows.Forms.RadioButton rbtnGeneral;
    }
}