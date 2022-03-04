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
	/// Summary description for Form1.
	/// </summary>
	public class frmBaseForm : System.Windows.Forms.Form
	{
    private System.Windows.Forms.MainMenu mainMenu1;
        protected MenuItem menuAction;
		private System.Windows.Forms.MenuItem menuNew;
    protected System.Windows.Forms.MenuItem menuExit;
    protected MenuItem menuSearch;
    private System.Windows.Forms.MenuItem menuRenew;
    protected MenuItem menuImport;
    protected MenuItem menuPrint;
    protected MenuItem menuReports;
    private MenuItem menuPrintAddress;
    private MenuItem menuPrintDues;
    private MenuItem menuPayHistory;
    private MenuItem menuRevenue;
    private MenuItem menuNewSubRep;
    private MenuItem menuMagCost;
    private MenuItem menuDuesReport;
    private MenuItem menuItem3;
    private MenuItem menuItem5;
    protected MenuItem menuManage;
    private MenuItem menuCountries;
    private MenuItem menuStates;
    private MenuItem menuItem1;
    private IContainer components;

		public frmBaseForm()
		{
			InitializeComponent();
      GlobalFn.SetCulture("en-GB");
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuAction = new System.Windows.Forms.MenuItem();
            this.menuNew = new System.Windows.Forms.MenuItem();
            this.menuRenew = new System.Windows.Forms.MenuItem();
            this.menuSearch = new System.Windows.Forms.MenuItem();
            this.menuImport = new System.Windows.Forms.MenuItem();
            this.menuManage = new System.Windows.Forms.MenuItem();
            this.menuCountries = new System.Windows.Forms.MenuItem();
            this.menuStates = new System.Windows.Forms.MenuItem();
            this.menuPrint = new System.Windows.Forms.MenuItem();
            this.menuPrintAddress = new System.Windows.Forms.MenuItem();
            this.menuPrintDues = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuReports = new System.Windows.Forms.MenuItem();
            this.menuMagCost = new System.Windows.Forms.MenuItem();
            this.menuPayHistory = new System.Windows.Forms.MenuItem();
            this.menuRevenue = new System.Windows.Forms.MenuItem();
            this.menuNewSubRep = new System.Windows.Forms.MenuItem();
            this.menuDuesReport = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuExit = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuAction,
            this.menuSearch,
            this.menuImport,
            this.menuManage,
            this.menuPrint,
            this.menuReports,
            this.menuExit});
            // 
            // menuAction
            // 
            this.menuAction.Index = 0;
            this.menuAction.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuNew,
            this.menuRenew});
            this.menuAction.Text = "Action";
            // 
            // menuNew
            // 
            this.menuNew.Index = 0;
            this.menuNew.Text = "New";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            // 
            // menuRenew
            // 
            this.menuRenew.Index = 1;
            this.menuRenew.Text = "Renew";
            this.menuRenew.Click += new System.EventHandler(this.menuRenew_Click);
            // 
            // menuSearch
            // 
            this.menuSearch.Index = 1;
            this.menuSearch.Text = "Search";
            this.menuSearch.Click += new System.EventHandler(this.menuSearch_Click);
            // 
            // menuImport
            // 
            this.menuImport.Index = 2;
            this.menuImport.Text = "Import";
            this.menuImport.Click += new System.EventHandler(this.menuImport_Click);
            // 
            // menuManage
            // 
            this.menuManage.Index = 3;
            this.menuManage.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuCountries,
            this.menuStates});
            this.menuManage.Text = "Manage";
            // 
            // menuCountries
            // 
            this.menuCountries.Index = 0;
            this.menuCountries.Text = "Countries";
            this.menuCountries.Click += new System.EventHandler(this.menuCountries_Click);
            // 
            // menuStates
            // 
            this.menuStates.Index = 1;
            this.menuStates.Text = "States";
            this.menuStates.Click += new System.EventHandler(this.menuStates_Click);
            // 
            // menuPrint
            // 
            this.menuPrint.Index = 4;
            this.menuPrint.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuPrintAddress,
            this.menuPrintDues,
            this.menuItem1});
            this.menuPrint.Text = "&Print";
            // 
            // menuPrintAddress
            // 
            this.menuPrintAddress.Index = 0;
            this.menuPrintAddress.Text = "&Address Labels";
            this.menuPrintAddress.Click += new System.EventHandler(this.menuPrintAddress_Click);
            // 
            // menuPrintDues
            // 
            this.menuPrintDues.Index = 1;
            this.menuPrintDues.Text = "&Dues list";
            this.menuPrintDues.Click += new System.EventHandler(this.menuPrintDues_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 2;
            this.menuItem1.Text = "&Overseas Address Labels";
            this.menuItem1.Click += new System.EventHandler(this.overseasAddress_Click);
            // 
            // menuReports
            // 
            this.menuReports.Index = 5;
            this.menuReports.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuMagCost,
            this.menuPayHistory,
            this.menuRevenue,
            this.menuNewSubRep,
            this.menuDuesReport,
            this.menuItem3,
            this.menuItem5});
            this.menuReports.Text = "Reports";
            // 
            // menuMagCost
            // 
            this.menuMagCost.Index = 0;
            this.menuMagCost.Text = "Magazine Cost";
            this.menuMagCost.Click += new System.EventHandler(this.menuMagCost_Click);
            // 
            // menuPayHistory
            // 
            this.menuPayHistory.Index = 1;
            this.menuPayHistory.Text = "Payment History";
            this.menuPayHistory.Click += new System.EventHandler(this.menuPayHistory_Click);
            // 
            // menuRevenue
            // 
            this.menuRevenue.Index = 2;
            this.menuRevenue.Text = "Revenue Report";
            this.menuRevenue.Click += new System.EventHandler(this.menuRevenue_Click);
            // 
            // menuNewSubRep
            // 
            this.menuNewSubRep.Index = 3;
            this.menuNewSubRep.Text = "New Subscription Report";
            this.menuNewSubRep.Click += new System.EventHandler(this.menuNewSubRep_Click);
            // 
            // menuDuesReport
            // 
            this.menuDuesReport.Index = 4;
            this.menuDuesReport.Text = "Dues Report";
            this.menuDuesReport.Click += new System.EventHandler(this.menuDuesReport_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 5;
            this.menuItem3.Text = "BackUp Database";
            this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 6;
            this.menuItem5.Text = "Subscriber Print History";
            this.menuItem5.Click += new System.EventHandler(this.menuItem5_Click);
            // 
            // menuExit
            // 
            this.menuExit.Index = 6;
            this.menuExit.Text = "Exit";
            this.menuExit.Click += new System.EventHandler(this.menuExit_Click);
            // 
            // frmBaseForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(520, 246);
            this.Menu = this.mainMenu1;
            this.Name = "frmBaseForm";
            this.ResumeLayout(false);

		}
		#endregion

    protected void NewSub()
    {
        menuNew_Click(null,null);
    }
		private void menuNew_Click(object sender, System.EventArgs e)
		{
			this.Close();
			frmNewSub newForm = new frmNewSub();
			newForm.Show();
            newForm.Location = this.Location;
		}

		private void menuExit_Click(object sender, System.EventArgs e)
		{
			Application.Exit();
		}

		private void menuSearch_Click(object sender, System.EventArgs e)
		{
			this.Close();
			frmSearch newForm = new frmSearch();
			newForm.Show();
            newForm.Location = this.Location;
		}

		private void menuRenew_Click(object sender, System.EventArgs e)
		{
			this.Close();
			frmSearch newForm = new frmSearch();
			newForm.Show();
            newForm.Location = this.Location;
		}

		private void menuImport_Click(object sender, System.EventArgs e)
		{
			this.Close();
			frmDataImport newForm = new frmDataImport();
			newForm.Show();
            newForm.Location = this.Location;
		}

        private void menuPrintAddress_Click(object sender, EventArgs e)
        {
            this.Close();
            frmAddressLabels newForm = new frmAddressLabels();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuPrintDues_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDuesList newForm = new frmDuesList();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuPayHistory_Click(object sender, EventArgs e)
        {
            this.Close();
            frmPaymentHistory newForm = new frmPaymentHistory();
            newForm.Show();
            newForm.Location = this.Location;
 
        }
        private void menuRevenue_Click(object sender, EventArgs e)
        {
            this.Close();
            frmRevenueReport newForm = new frmRevenueReport();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuNewSubRep_Click(object sender, EventArgs e)
        {
            this.Close();
            frmNewSubReport newForm = new frmNewSubReport();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuMagCost_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMagazineCost newForm = new frmMagazineCost();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuDuesReport_Click(object sender, EventArgs e)
        {
            this.Close();
            frmDuesReport newForm = new frmDuesReport();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            this.Close();
            FrmDbBackup newForm = new FrmDbBackup();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            this.Close();
            frmPrintHistory newForm = new frmPrintHistory();
            newForm.Show();
            newForm.Location = this.Location;
        }

        private void menuCountries_Click(object sender, EventArgs e)
        {
          this.Close();
          frmManageCountries newForm = new frmManageCountries(false);
          newForm.Show();
          newForm.Location = this.Location;
        }

        private void menuStates_Click(object sender, EventArgs e)
        {
          this.Close();
          frmManageStates newForm = new frmManageStates(false);
          newForm.Show();
          newForm.Location = this.Location;
        }

    private void overseasAddress_Click(object sender, EventArgs e)
    {
      this.Close();
      frmPrintOverseasAddLbls newForm = new frmPrintOverseasAddLbls();
      newForm.Show();
      newForm.Location = this.Location;
    }

       
	}
}
