using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CIV.Classess;
using System.IO;
namespace CIV
{
	/// <summary>
	/// Summary description for Main.
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		public frmMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(104, 31);
            this.Name = "frmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new frmMain());
		}

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MessageBox.Show("An unexpected error occured.\r\n\r\nThis Application will be closed.", GlobalFn.FormText);
            GlobalFn.ProcessException(e.Exception, "Error in the Application");
            Application.Exit();
        }
		private void Main_Load(object sender, System.EventArgs e)
		{
            if (!Directory.Exists(GlobalFn.LogDir))
                Directory.CreateDirectory(GlobalFn.LogDir);

            if (!Directory.Exists(GlobalFn.ReportsDir))
                Directory.CreateDirectory(GlobalFn.ReportsDir);

            frmNewSub objNew = new frmNewSub();
            objNew.Show();
            
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.Hide();
            timer1.Stop();

		}
	}
}
