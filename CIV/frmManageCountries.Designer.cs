namespace CIV
{
    partial class frmManageCountries
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
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
          this.dataGridViewCountries = new System.Windows.Forms.DataGridView();
          this.ButtonUpdate = new System.Windows.Forms.Button();
          this.ButtonCancel = new System.Windows.Forms.Button();
          this.ButtonClose = new System.Windows.Forms.Button();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCountries)).BeginInit();
          this.SuspendLayout();
          // 
          // dataGridViewCountries
          // 
          dataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue;
          this.dataGridViewCountries.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
          dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
          dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dataGridViewCountries.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
          this.dataGridViewCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dataGridViewCountries.Location = new System.Drawing.Point(12, 12);
          this.dataGridViewCountries.Name = "dataGridViewCountries";
          this.dataGridViewCountries.Size = new System.Drawing.Size(481, 201);
          this.dataGridViewCountries.TabIndex = 0;
          // 
          // ButtonUpdate
          // 
          this.ButtonUpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ButtonUpdate.Location = new System.Drawing.Point(122, 235);
          this.ButtonUpdate.Name = "ButtonUpdate";
          this.ButtonUpdate.Size = new System.Drawing.Size(75, 25);
          this.ButtonUpdate.TabIndex = 1;
          this.ButtonUpdate.Text = "Update";
          this.ButtonUpdate.UseVisualStyleBackColor = true;
          this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
          // 
          // ButtonCancel
          // 
          this.ButtonCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ButtonCancel.Location = new System.Drawing.Point(233, 235);
          this.ButtonCancel.Name = "ButtonCancel";
          this.ButtonCancel.Size = new System.Drawing.Size(85, 25);
          this.ButtonCancel.TabIndex = 2;
          this.ButtonCancel.Text = "Cancel";
          this.ButtonCancel.UseVisualStyleBackColor = true;
          this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
          // 
          // ButtonClose
          // 
          this.ButtonClose.Enabled = false;
          this.ButtonClose.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ButtonClose.Location = new System.Drawing.Point(176, 266);
          this.ButtonClose.Name = "ButtonClose";
          this.ButtonClose.Size = new System.Drawing.Size(84, 30);
          this.ButtonClose.TabIndex = 3;
          this.ButtonClose.Text = "Close";
          this.ButtonClose.UseVisualStyleBackColor = true;
          this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
          // 
          // frmManageCountries
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(505, 306);
          this.Controls.Add(this.ButtonClose);
          this.Controls.Add(this.ButtonCancel);
          this.Controls.Add(this.ButtonUpdate);
          this.Controls.Add(this.dataGridViewCountries);
          this.Name = "frmManageCountries";
          this.Text = "Manage Countries";
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCountries)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

      private System.Windows.Forms.DataGridView dataGridViewCountries;
      private System.Windows.Forms.Button ButtonUpdate;
      private System.Windows.Forms.Button ButtonCancel;
      private System.Windows.Forms.Button ButtonClose;
    }
}