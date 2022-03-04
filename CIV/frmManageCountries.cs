using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIV.Classess;

namespace CIV
{
    public partial class frmManageCountries : frmBaseForm
    {
      DataSet ds = new DataSet();
      DataTable oTable = new DataTable();
      SqlDataAdapter _dataAdapter;

      public frmManageCountries(bool isClose)
      {
        InitializeComponent();
        this.Text += " - " + GlobalFn.FormText;
        GetCountries();

        BuildGrid();
        if (isClose)
        {
          menuAction.Visible = false;
          menuSearch.Visible = false;
          menuImport.Visible = false;
          menuManage.Visible = false;
          menuPrint.Visible = false;
          menuReports.Visible = false;
          menuExit.Visible = false;
          ButtonClose.Enabled = true;
        }
        else
        {
          menuAction.Visible = true;
          menuSearch.Visible = true;
          menuImport.Visible = true;
          menuManage.Visible = true;
          menuPrint.Visible = true;
          menuReports.Visible = true;
          menuExit.Visible = true;
          ButtonClose.Enabled = false;
        }
      }
      public void GetCountries()
      {
        StringBuilder sb = new StringBuilder();
        sb.Append(" select ");
        sb.Append("   country_id, ");
        sb.Append("   country_name, ");
        sb.Append("   continent,  ");
        sb.Append("   created_on, ");
        sb.Append("   last_mod_on ");
        sb.Append(" from  ");
        sb.Append("   countries ");
        sb.Append(" order by country_name  ");

        try
        {
          _dataAdapter = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
          _dataAdapter.Fill(ds, "countries");
          oTable = ds.Tables[0];
        }
        catch (Exception eCount)
        {
          MessageBox.Show("Database error...", GlobalFn.FormText);
          GlobalFn.ProcessException(eCount, "Error in executing query in frmCountries.cs");
          return;
        }
      
      }

    
      private void BuildGrid()
      {
        try
        {
          
          DataGridViewTextBoxColumn cs = new DataGridViewTextBoxColumn();
          cs.DataPropertyName = "country_id";
          cs.HeaderText = "Country ID";
          cs.Width = 10;
          cs.Visible = false;
          dataGridViewCountries.Columns.Add(cs);

          cs = new DataGridViewTextBoxColumn();
          cs.DataPropertyName = "country_name";
          cs.HeaderText = "Country Name";
          cs.Width = 100;
          dataGridViewCountries.Columns.Add(cs);
          
          cs = new DataGridViewTextBoxColumn();
          cs.DataPropertyName = "continent";
          cs.HeaderText = "Continent";
          cs.Width = 100;
          dataGridViewCountries.Columns.Add(cs);

          cs = new DataGridViewTextBoxColumn();
          cs.DataPropertyName = "created_on";
          cs.HeaderText = "Created On";
          cs.Width = 100;
          dataGridViewCountries.Columns.Add(cs);

          cs = new DataGridViewTextBoxColumn();
          cs.DataPropertyName = "last_mod_on";
          cs.HeaderText = "Last Mod On";
          cs.Width = 100;
          dataGridViewCountries.Columns.Add(cs);

          dataGridViewCountries.DataSource = oTable;
        }
        catch (Exception eGrid)
        {
          MessageBox.Show("Database error...", GlobalFn.FormText);
          GlobalFn.ProcessException(eGrid, "Error in Binding Data Grid in frmCountries.cs");
          return;
        }

      }

      private void ButtonUpdate_Click(object sender, EventArgs e)
      {
        // Create the UpdateCommand.
        SqlParameter parm;
        DataSet dataSetChanged = new DataSet();
        bool okayFlag = true;
        StringBuilder sb = new StringBuilder();
        SqlCommand cmd;
        DataTable updTable = ds.Tables[0];

        DataRow[] modRows = updTable.Select("", "", DataViewRowState.ModifiedCurrent);
        DataRow[] addRows = updTable.Select("", "", DataViewRowState.Added);
        if ((modRows.Length == 0) && (addRows.Length == 0))
        {
          MessageBox.Show("Please modify existing countries or add new countries and press update button");
          return;
        }

        SqlConnection oConn = new SqlConnection(GlobalFn.GetConnString);
        _dataAdapter.RowUpdating += new SqlRowUpdatingEventHandler(OnRowUpdating);
        _dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);
       
        if (modRows.Length > 0)
        {
          try
          {
            oConn.Open();

            //update command
            sb = new StringBuilder();
            sb.Append("	UPDATE	");
            sb.Append("		countries	");
            sb.Append("	SET	");
            sb.Append("		country_name = @country_name, ");
            sb.Append("   continent = @continent, ");
            sb.Append("		last_mod_on = getdate()	");
            sb.Append("	WHERE	");
            sb.Append("		country_id = @country_id	");

            cmd = new SqlCommand(sb.ToString(), oConn);

            cmd.Parameters.Add("@country_name", SqlDbType.VarChar, 50, "country_name");
            cmd.Parameters.Add("@continent", SqlDbType.VarChar, 200, "continent");
            parm = cmd.Parameters.Add("@country_id", SqlDbType.Int, 4, "country_id");
            parm.SourceVersion = DataRowVersion.Original;

            _dataAdapter.UpdateCommand = cmd;

            //modRows = updTable.Select("", "", DataViewRowState.ModifiedCurrent);
                      
            dataSetChanged = ds.GetChanges(DataRowState.Modified);

            if (dataSetChanged.HasErrors)
            {
              okayFlag = false;
              string msg = "Error in row ";
              foreach (DataTable dt in dataSetChanged.Tables)
              {
                if (dt.HasErrors)
                {
                  DataRow[] errRows = dt.GetErrors();
                  foreach (DataRow row in errRows)
                  {
                    msg = msg + row["country_name"];
                  }
                }
              }
              MessageBox.Show(msg, GlobalFn.FormText);
            }
            if (okayFlag)
            {
              _dataAdapter.Update(updTable.Select("", "", DataViewRowState.ModifiedCurrent));
              Application.DoEvents();
              MessageBox.Show("Country has been updated!", GlobalFn.FormText);
            }
            else
            {
              ds.RejectChanges();
            }
          }
          catch (Exception exUpdate)
          {
            if (exUpdate.Message.StartsWith("Violation of UNIQUE KEY constraint"))
            {
              MessageBox.Show("This Country already exists.Please choose different one!", GlobalFn.FormText);
              return;
            }
            else
            {
              MessageBox.Show("Database error...", GlobalFn.FormText);
              GlobalFn.ProcessException(exUpdate, "Error in updating country in frmManageCountries.cs");
              return;
            }
          }
          finally
          {
            oConn.Close();
          }
        }
        //insert command
        modRows = updTable.Select("", "", DataViewRowState.Added);
        if (modRows.Length > 0)
        {
          dataSetChanged = ds.GetChanges(DataRowState.Added);
          if (dataSetChanged.HasErrors)
          {
            okayFlag = false;
            string msg = "Error in row ";
            foreach (DataTable dt in dataSetChanged.Tables)
            {
              if (dt.HasErrors)
              {
                DataRow[] errRows = dt.GetErrors();
                foreach (DataRow row in errRows)
                {
                  msg = msg + row["country_name"];
                }
              }
            }
            MessageBox.Show(msg, GlobalFn.FormText);
          }
          if (okayFlag)
          {
            foreach (DataRow row in modRows)
            {
              try
              {
                SQL.AddNewCountry(row["country_name"].ToString(),row["continent"].ToString());
              }
              catch (Exception eCountAdd)
              {
                if (eCountAdd.Message.StartsWith("Violation of UNIQUE KEY constraint"))
                {
                  MessageBox.Show("This Country already exists.Please choose different one!", GlobalFn.FormText);
                  return;
                }
                else
                {
                  MessageBox.Show("Database error...", GlobalFn.FormText);
                  GlobalFn.ProcessException(eCountAdd, "Error in adding new country in frmManageCountries.cs");
                  return;
                }
              }
            }
            MessageBox.Show("Country has been added!", GlobalFn.FormText);
          }
          else
          {
            ds.RejectChanges();
          }
        }
        _dataAdapter.RowUpdated -= new SqlRowUpdatedEventHandler(OnRowUpdated);
        ds.Clear();

        GetCountries();
      }

      private static void OnRowUpdating(object sender, SqlRowUpdatingEventArgs e)
      {
      }

      private static void OnRowUpdated(object sender, SqlRowUpdatedEventArgs e)
      {
        if (e.Status == UpdateStatus.ErrorsOccurred)
        {
          e.Row.RowError = e.Errors.Message;
          MessageBox.Show("Error: " + e.Row.RowError, GlobalFn.FormText);
          e.Status = UpdateStatus.ErrorsOccurred;
        }
      }

     

      private void ButtonCancel_Click(object sender, EventArgs e)
      {
        ds.Clear();
        GetCountries();
      }

      private void ButtonClose_Click(object sender, EventArgs e)
      {
        this.Close();
      }

    

     
    }
}