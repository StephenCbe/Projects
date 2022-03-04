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
    public partial class frmMagazineCost : frmBaseForm
    {
        
        public frmMagazineCost()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            BuildGrid();

        }
        private void BuildGrid()
        {
            try
            {
                DataTable oTable = SQL.MagazineCost().Tables[0];
                DataGridViewTextBoxColumn cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "year_part";
                cs.HeaderText = "Year";
                cs.Width = 100;
                dgvCost.Columns.Add(cs);

                cs = new DataGridViewTextBoxColumn();
                cs.DataPropertyName = "cost";
                cs.HeaderText = "Cost";
                cs.Width = 150;
                dgvCost.Columns.Add(cs);


                dgvCost.DataSource = oTable;
            }
            catch (Exception eGrid)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eGrid, "Error in Binding Data Grid in frmMagazineCost.cs");
                return;
            }

        }
    }
}