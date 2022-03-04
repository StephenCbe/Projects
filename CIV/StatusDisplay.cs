using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIV.Classess;

namespace CIV
{
    public partial class StatusDisplay : Form
    {
        private DateTime startDate;
        public StatusDisplay(string mainLabel, int sleepInterval)
        {
            InitializeComponent();

            label1.Text = mainLabel;
            this.Show();
            this.Focus();
            this.Update();
            startDate = DateTime.Now;
            while (true)
            {
                ProgressLabelUpdate();
                ClockUpdate();
                FormUpdate();
                System.Threading.Thread.Sleep(sleepInterval);
                if (GlobalFn.StatusDisplayAbort)
                {
                    this.Opacity = 0.0;
                    FormUpdate();
                    System.Threading.Thread.CurrentThread.Abort();
                }
            }
        }
        public void DispMsg(string secondLabel)
        {
            indicatorLabel.Text = secondLabel;
            FormUpdate();
        }
            

        private void timer1_Tick(object sender, EventArgs e)
        {
            ClockUpdate();
            FormUpdate();
        }
        private void ProgressLabelUpdate()
        {
            indicatorLabel.Text = GlobalFn.StatusDisplayProgressLabel;
        }

        private void ClockUpdate()
        {
            TimeSpan ts = DateTime.Now.Subtract(startDate);
            timeExpired.Text = ts.Hours.ToString("00") + ":" + ts.Minutes.ToString("00") + ":" + ts.Seconds.ToString("00");
        }

        private void FormUpdate()
        {
            this.Refresh();
            System.Windows.Forms.Application.DoEvents();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // define a flag in Global Fn. set it false.

            // stop printing  means come out of the loop.

            // Display all the records that matched selection criteria in Datagrid with checkbox checked, so user can uncheck records that have not 
            //been printed.

            // save the printed records means change status to 'P'.
            GlobalFn.CancelPrint = true;
                       
        }
    }
}