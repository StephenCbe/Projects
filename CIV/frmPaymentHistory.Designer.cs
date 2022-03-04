namespace CIV
{
    partial class frmPaymentHistory
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblSubData = new System.Windows.Forms.Label();
            this.dgPaymentHistory = new System.Windows.Forms.DataGrid();
            this.cboMagazine = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaymentHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblSubData);
            this.groupBox1.Controls.Add(this.dgPaymentHistory);
            this.groupBox1.Controls.Add(this.cboMagazine);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtSubCode);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSubmit);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(554, 407);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " ";
            // 
            // lblSubData
            // 
            this.lblSubData.AutoSize = true;
            this.lblSubData.Location = new System.Drawing.Point(6, 114);
            this.lblSubData.Name = "lblSubData";
            this.lblSubData.Size = new System.Drawing.Size(12, 16);
            this.lblSubData.TabIndex = 6;
            this.lblSubData.Text = " ";
            // 
            // dgPaymentHistory
            // 
            this.dgPaymentHistory.AlternatingBackColor = System.Drawing.Color.SkyBlue;
            this.dgPaymentHistory.CausesValidation = false;
            this.dgPaymentHistory.DataMember = "";
            this.dgPaymentHistory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgPaymentHistory.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dgPaymentHistory.Location = new System.Drawing.Point(9, 142);
            this.dgPaymentHistory.Name = "dgPaymentHistory";
            this.dgPaymentHistory.ReadOnly = true;
            this.dgPaymentHistory.Size = new System.Drawing.Size(541, 259);
            this.dgPaymentHistory.TabIndex = 5;
            this.dgPaymentHistory.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgPaymentHistory_MouseUp);
            // 
            // cboMagazine
            // 
            this.cboMagazine.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboMagazine.FormattingEnabled = true;
            this.cboMagazine.Location = new System.Drawing.Point(240, 16);
            this.cboMagazine.Name = "cboMagazine";
            this.cboMagazine.Size = new System.Drawing.Size(197, 24);
            this.cboMagazine.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(152, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 3;
            this.label2.Tag = "";
            this.label2.Text = "Magazine:";
            // 
            // txtSubCode
            // 
            this.txtSubCode.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubCode.Location = new System.Drawing.Point(240, 46);
            this.txtSubCode.Name = "txtSubCode";
            this.txtSubCode.Size = new System.Drawing.Size(116, 23);
            this.txtSubCode.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(90, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Subscription Code:";
            // 
            // btnSubmit
            // 
            this.btnSubmit.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.Location = new System.Drawing.Point(240, 75);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(96, 30);
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "&Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // frmPaymentHistory
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 448);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmPaymentHistory";
            this.Text = "frmPaymentHistory";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPaymentHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboMagazine;
        private System.Windows.Forms.Label lblSubData;
        private System.Windows.Forms.DataGrid dgPaymentHistory;
    }
}