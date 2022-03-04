namespace CIV
{
  partial class frmPrintOverseasAddLbls
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
        this.gbOverseasPrint = new System.Windows.Forms.GroupBox();
        this.grpPrintCategory = new System.Windows.Forms.GroupBox();
        this.rbtnIsBulk = new System.Windows.Forms.RadioButton();
        this.rbtnIsGeneral = new System.Windows.Forms.RadioButton();
        this.dtpPostDate = new System.Windows.Forms.DateTimePicker();
        this.label1 = new System.Windows.Forms.Label();
        this.btnPrint = new System.Windows.Forms.Button();
        this.cboPrinters = new System.Windows.Forms.ComboBox();
        this.label8 = new System.Windows.Forms.Label();
        this.cboMagazine = new System.Windows.Forms.ComboBox();
        this.label2 = new System.Windows.Forms.Label();
        this.gbOverseasPrint.SuspendLayout();
        this.grpPrintCategory.SuspendLayout();
        this.SuspendLayout();
        // 
        // gbOverseasPrint
        // 
        this.gbOverseasPrint.Controls.Add(this.grpPrintCategory);
        this.gbOverseasPrint.Controls.Add(this.dtpPostDate);
        this.gbOverseasPrint.Controls.Add(this.label1);
        this.gbOverseasPrint.Controls.Add(this.btnPrint);
        this.gbOverseasPrint.Controls.Add(this.cboPrinters);
        this.gbOverseasPrint.Controls.Add(this.label8);
        this.gbOverseasPrint.Controls.Add(this.cboMagazine);
        this.gbOverseasPrint.Controls.Add(this.label2);
        this.gbOverseasPrint.Location = new System.Drawing.Point(12, -5);
        this.gbOverseasPrint.Name = "gbOverseasPrint";
        this.gbOverseasPrint.Size = new System.Drawing.Size(345, 304);
        this.gbOverseasPrint.TabIndex = 0;
        this.gbOverseasPrint.TabStop = false;
        // 
        // grpPrintCategory
        // 
        this.grpPrintCategory.Controls.Add(this.rbtnIsBulk);
        this.grpPrintCategory.Controls.Add(this.rbtnIsGeneral);
        this.grpPrintCategory.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.grpPrintCategory.Location = new System.Drawing.Point(136, 92);
        this.grpPrintCategory.Name = "grpPrintCategory";
        this.grpPrintCategory.Size = new System.Drawing.Size(150, 87);
        this.grpPrintCategory.TabIndex = 26;
        this.grpPrintCategory.TabStop = false;
        this.grpPrintCategory.Text = "Category";
        // 
        // rbtnIsBulk
        // 
        this.rbtnIsBulk.AutoSize = true;
        this.rbtnIsBulk.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.rbtnIsBulk.Location = new System.Drawing.Point(39, 50);
        this.rbtnIsBulk.Name = "rbtnIsBulk";
        this.rbtnIsBulk.Size = new System.Drawing.Size(53, 17);
        this.rbtnIsBulk.TabIndex = 0;
        this.rbtnIsBulk.Text = "Bulk";
        this.rbtnIsBulk.UseVisualStyleBackColor = true;
        // 
        // rbtnIsGeneral
        // 
        this.rbtnIsGeneral.AutoSize = true;
        this.rbtnIsGeneral.Checked = true;
        this.rbtnIsGeneral.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.rbtnIsGeneral.Location = new System.Drawing.Point(39, 27);
        this.rbtnIsGeneral.Name = "rbtnIsGeneral";
        this.rbtnIsGeneral.Size = new System.Drawing.Size(76, 17);
        this.rbtnIsGeneral.TabIndex = 0;
        this.rbtnIsGeneral.TabStop = true;
        this.rbtnIsGeneral.Text = "General";
        this.rbtnIsGeneral.UseVisualStyleBackColor = true;
        // 
        // dtpPostDate
        // 
        this.dtpPostDate.Location = new System.Drawing.Point(138, 195);
        this.dtpPostDate.Name = "dtpPostDate";
        this.dtpPostDate.Size = new System.Drawing.Size(148, 20);
        this.dtpPostDate.TabIndex = 24;
        this.dtpPostDate.Value = new System.DateTime(2007, 1, 27, 0, 0, 0, 0);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label1.Location = new System.Drawing.Point(18, 199);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(114, 13);
        this.label1.TabIndex = 23;
        this.label1.Text = "Date of Posting :";
        this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // btnPrint
        // 
        this.btnPrint.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.btnPrint.Location = new System.Drawing.Point(136, 247);
        this.btnPrint.Name = "btnPrint";
        this.btnPrint.Size = new System.Drawing.Size(92, 31);
        this.btnPrint.TabIndex = 25;
        this.btnPrint.Text = "Print";
        this.btnPrint.UseVisualStyleBackColor = true;
        this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
        // 
        // cboPrinters
        // 
        this.cboPrinters.FormattingEnabled = true;
        this.cboPrinters.Location = new System.Drawing.Point(136, 22);
        this.cboPrinters.Name = "cboPrinters";
        this.cboPrinters.Size = new System.Drawing.Size(150, 21);
        this.cboPrinters.TabIndex = 22;
        // 
        // label8
        // 
        this.label8.AutoSize = true;
        this.label8.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label8.Location = new System.Drawing.Point(33, 25);
        this.label8.Name = "label8";
        this.label8.Size = new System.Drawing.Size(97, 13);
        this.label8.TabIndex = 21;
        this.label8.Text = "Printer Name:";
        this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // cboMagazine
        // 
        this.cboMagazine.FormattingEnabled = true;
        this.cboMagazine.ItemHeight = 13;
        this.cboMagazine.Location = new System.Drawing.Point(136, 46);
        this.cboMagazine.Name = "cboMagazine";
        this.cboMagazine.Size = new System.Drawing.Size(150, 21);
        this.cboMagazine.TabIndex = 18;
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.label2.Location = new System.Drawing.Point(54, 49);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(76, 13);
        this.label2.TabIndex = 19;
        this.label2.Text = "Magazine :";
        this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // frmPrintOverseasAddLbls
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(367, 310);
        this.Controls.Add(this.gbOverseasPrint);
        this.Name = "frmPrintOverseasAddLbls";
        this.Text = "frmPrintOverseasAddLbls";
        this.gbOverseasPrint.ResumeLayout(false);
        this.gbOverseasPrint.PerformLayout();
        this.grpPrintCategory.ResumeLayout(false);
        this.grpPrintCategory.PerformLayout();
        this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GroupBox gbOverseasPrint;
    private System.Windows.Forms.ComboBox cboPrinters;
      private System.Windows.Forms.Label label8;
    private System.Windows.Forms.ComboBox cboMagazine;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.DateTimePicker dtpPostDate;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button btnPrint;
      private System.Windows.Forms.GroupBox grpPrintCategory;
      private System.Windows.Forms.RadioButton rbtnIsBulk;
      private System.Windows.Forms.RadioButton rbtnIsGeneral;
  }
}