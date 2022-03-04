namespace CIV
{
    partial class frmManageStates
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
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
          System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
          this.dataGridViewStates = new System.Windows.Forms.DataGridView();
          this.ButtonUpdate = new System.Windows.Forms.Button();
          this.ButtonCancel = new System.Windows.Forms.Button();
          this.ButtonClose = new System.Windows.Forms.Button();
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStates)).BeginInit();
          this.SuspendLayout();
          // 
          // dataGridViewStates
          // 
          dataGridViewCellStyle3.BackColor = System.Drawing.Color.SkyBlue;
          dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
          this.dataGridViewStates.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
          dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
          dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
          dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
          dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
          dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
          dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
          this.dataGridViewStates.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
          this.dataGridViewStates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
          this.dataGridViewStates.Location = new System.Drawing.Point(12, 12);
          this.dataGridViewStates.Name = "dataGridViewStates";
          this.dataGridViewStates.Size = new System.Drawing.Size(508, 240);
          this.dataGridViewStates.TabIndex = 0;
          // 
          // ButtonUpdate
          // 
          this.ButtonUpdate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ButtonUpdate.Location = new System.Drawing.Point(147, 271);
          this.ButtonUpdate.Name = "ButtonUpdate";
          this.ButtonUpdate.Size = new System.Drawing.Size(88, 26);
          this.ButtonUpdate.TabIndex = 1;
          this.ButtonUpdate.Text = "Update";
          this.ButtonUpdate.UseVisualStyleBackColor = true;
          this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
          // 
          // ButtonCancel
          // 
          this.ButtonCancel.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ButtonCancel.Location = new System.Drawing.Point(293, 272);
          this.ButtonCancel.Name = "ButtonCancel";
          this.ButtonCancel.Size = new System.Drawing.Size(104, 25);
          this.ButtonCancel.TabIndex = 2;
          this.ButtonCancel.Text = "Cancel";
          this.ButtonCancel.UseVisualStyleBackColor = true;
          this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
          // 
          // ButtonClose
          // 
          this.ButtonClose.Enabled = false;
          this.ButtonClose.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
          this.ButtonClose.Location = new System.Drawing.Point(209, 309);
          this.ButtonClose.Name = "ButtonClose";
          this.ButtonClose.Size = new System.Drawing.Size(117, 33);
          this.ButtonClose.TabIndex = 3;
          this.ButtonClose.Text = "Close";
          this.ButtonClose.UseVisualStyleBackColor = true;
          this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
          // 
          // frmManageStates
          // 
          this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
          this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
          this.ClientSize = new System.Drawing.Size(532, 354);
          this.Controls.Add(this.ButtonClose);
          this.Controls.Add(this.ButtonCancel);
          this.Controls.Add(this.ButtonUpdate);
          this.Controls.Add(this.dataGridViewStates);
          this.Name = "frmManageStates";
          this.Text = "Manage States";
          ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStates)).EndInit();
          this.ResumeLayout(false);

        }

        #endregion

      private System.Windows.Forms.DataGridView dataGridViewStates;
      private System.Windows.Forms.Button ButtonUpdate;
      private System.Windows.Forms.Button ButtonCancel;
      private System.Windows.Forms.Button ButtonClose;
    }
}