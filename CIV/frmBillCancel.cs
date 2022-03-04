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
    public partial class frmBillCancel : Form
    {
        public frmBillCancel()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
           
        }
    }
}