using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CIV.Classess;

namespace CIV
{
  public partial class frmManageStates : frmBaseForm
  {
    DataGridViewComboBoxColumn cboCountries;
    DataSet _ds = new DataSet();
    DataSet ds = new DataSet();
    DataTable oTable = new DataTable();
    SqlDataAdapter _dataAdapter;

    public frmManageStates(bool isClose)
    {
      InitializeComponent();
      this.Text += " - " + GlobalFn.FormText;
      GetStates();
      BuildGridView();
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
    private void GetCountries()
    {
      try
      {
        _ds = SQL.GetCountries();
      }
      catch (Exception eCount)
      {
        MessageBox.Show("Database error...", GlobalFn.FormText);
        GlobalFn.ProcessException(eCount, "Error in GetCountries method in frmCountries.cs");
        return;
      }
      
    }
    public void GetStates()
    {
      StringBuilder sb = new StringBuilder();
      sb.Append(" select ");
      sb.Append("   state_id, ");
      sb.Append("   name, ");
      sb.Append("   abbr, ");
      sb.Append("   country_id  ");
      sb.Append(" from  ");
      sb.Append("   states  ");
      sb.Append(" order by  ");
      sb.Append("   country_id,name ");

      try
      {
        _dataAdapter = new SqlDataAdapter(sb.ToString(), GlobalFn.GetConnString);
        _dataAdapter.Fill(ds, "states");
        oTable = ds.Tables[0];
      }
      catch (Exception eCount)
      {
        MessageBox.Show("Database error...", GlobalFn.FormText);
        GlobalFn.ProcessException(eCount, "Error in executing query in frmStates.cs");
        return;
      }
    }
    private void BuildGridView()
    {
      DataGridViewTextBoxColumn cs = new DataGridViewTextBoxColumn();
      cboCountries = new DataGridViewComboBoxColumn();

      cs = new DataGridViewTextBoxColumn();
      cs.DataPropertyName = "state_id";
      cs.HeaderText = "State ID";
      cs.ReadOnly = true;
      cs.Width = 10;
      cs.Visible = false;
      dataGridViewStates.Columns.Add(cs);

      cs = new DataGridViewTextBoxColumn();
      cs.DataPropertyName = "name";
      cs.HeaderText = "State";
      cs.Width = 100;
      dataGridViewStates.Columns.Add(cs);

      cs = new DataGridViewTextBoxColumn();
      cs.DataPropertyName = "abbr";
      cs.HeaderText = "State Abbr.";
      cs.Width = 100;
      dataGridViewStates.Columns.Add(cs);

      GetCountries();

      cboCountries.DataSource = _ds.Tables[0];
      cboCountries.DisplayMember = _ds.Tables[0].Columns["country_name"].ColumnName;
      cboCountries.ValueMember = _ds.Tables[0].Columns["country_id"].ColumnName;
      cboCountries.HeaderText = "Countries";
      cboCountries.Width = 125;
      cboCountries.DataPropertyName = oTable.Columns["country_id"].ColumnName;
      cboCountries.Name = oTable.Columns["country_id"].ColumnName;
      dataGridViewStates.Columns.Add(cboCountries);
            
      dataGridViewStates.DataSource = oTable;
      
    }

    private void ButtonCancel_Click(object sender, EventArgs e)
    {
      ds.Clear();
      GetStates();
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
        MessageBox.Show("Please modify existing states or add new states and press update button");
        return;
      }

      SqlConnection oConn = new SqlConnection(GlobalFn.GetConnString);
      _dataAdapter.RowUpdated += new SqlRowUpdatedEventHandler(OnRowUpdated);

      if (modRows.Length > 0)
      {
        try
        {
          oConn.Open();

          //update command
          sb = new StringBuilder();
          sb.Append("	UPDATE	");
          sb.Append("		states	");
          sb.Append("	SET	");
          sb.Append("		name = @name, ");
          sb.Append("   abbr = @abbr, ");
          sb.Append("		country_id = @country_id	");
          sb.Append("	WHERE	");
          sb.Append("		state_id = @state_id	");

          cmd = new SqlCommand(sb.ToString(), oConn);

          cmd.Parameters.Add("@name", SqlDbType.VarChar, 100, "name");
          cmd.Parameters.Add("@abbr", SqlDbType.VarChar, 100, "abbr");
          cmd.Parameters.Add("@country_id", SqlDbType.Int,4, "country_id");
          parm = cmd.Parameters.Add("@state_id", SqlDbType.Int, 4, "state_id");

          parm.SourceVersion = DataRowVersion.Original;

          _dataAdapter.UpdateCommand = cmd;
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
                  msg = msg + row["name"];
                }
              }
            }
            MessageBox.Show(msg, GlobalFn.FormText);
          }
          if (okayFlag)
          {
            _dataAdapter.Update(updTable.Select("", "", DataViewRowState.ModifiedCurrent));
            Application.DoEvents();
            MessageBox.Show("State has been updated!", GlobalFn.FormText);
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
            MessageBox.Show("This State already exists.Please choose different one!", GlobalFn.FormText);
            return;
          }
          else
          {
            MessageBox.Show("Database error...", GlobalFn.FormText);
            GlobalFn.ProcessException(exUpdate, "Error in updating State in frmManageStates.cs");
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
                msg = msg + row["name"];
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
              SQL.AddNewState(row["name"].ToString(), row["abbr"].ToString(),row["country_id"].ToString());
            }
            catch (Exception eCountAdd)
            {
              if (eCountAdd.Message.StartsWith("Violation of UNIQUE KEY constraint"))
              {
                MessageBox.Show("This State already exists.Please choose different one!", GlobalFn.FormText);
                return;
              }
              else
              {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eCountAdd, "Error in adding new state in frmManageStates.cs");
                return;
              }
            }
          }
          MessageBox.Show("State has been added!", GlobalFn.FormText);
        }
        else
        {
          ds.RejectChanges();
        }
      }
      _dataAdapter.RowUpdated -= new SqlRowUpdatedEventHandler(OnRowUpdated);
      ds.Clear();

      GetStates();
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

    private void ButtonClose_Click(object sender, EventArgs e)
    {
      this.Close();
    }
  }

}