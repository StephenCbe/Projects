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
  public partial class frmEditSubscription : Form
  {
    int subId = 0;
    string subCode = "";
    string languageId = "";
    DataTable dtCountries = new DataTable();
    DataTable dtStates = new DataTable();
    public frmEditSubscription(string subscriberId)
    {
      InitializeComponent();
      this.Text += " - " + GlobalFn.FormText;
      
      BindCountries();
      if (cboCountries.Items.Count > 0)
      {
        int countIndex = cboCountries.FindString("India");
        if (countIndex > 0)
          cboCountries.SelectedIndex = countIndex;
      }

      BindStates();
      BindFields(subscriberId);
    }
    private void BindStates()
    {
      if ((cboCountries.Items.Count > 0) && (cboCountries.SelectedIndex >= 0))
      {
        try
        {
          cboStates.ValueMember = "state_ID";
          cboStates.DisplayMember = "name";
          dtStates = SQL.SubscribersGetStates(cboCountries.SelectedValue.ToString()).Tables[0];
          cboStates.DataSource = dtStates;
        }
        catch (Exception eItems)
        {
          MessageBox.Show("Database error...", GlobalFn.FormText);
          GlobalFn.ProcessException(eItems, "Error in Binding States in frmEditSubscriptions.cs");
          return;
        }
      }
    }

    private void BindCountries()
    {
      try
      {
        cboCountries.ValueMember = "Country_ID";
        cboCountries.DisplayMember = "Country_name";
        dtCountries = SQL.SubscribersGetCountries().Tables[0];
        cboCountries.DataSource = dtCountries;
      }
      catch (Exception eItems)
      {
        MessageBox.Show("Database error...", GlobalFn.FormText);
        GlobalFn.ProcessException(eItems, "Error in Binding Countries in frmEditSubscriptions.cs");
        return;
      }
    }

    private void BindFields(string subscriberId)
    {
      try
      {
        subId = Convert.ToInt32(subscriberId);
        DataSet ds = SQL.GetSubscriberInfo(subscriberId);
        if (ds.Tables[0].Rows.Count > 0)
        {
          DataRow dr = ds.Tables[0].Rows[0];
          cboTitle.SelectedIndex = cboTitle.FindStringExact(dr["title"].ToString());
          txtLastName.Text = dr["last_name"].ToString();
          txtFirstName.Text = dr["first_name"].ToString();
          txtAddress1.Text = dr["address_line1"].ToString();
          txtAddress2.Text = dr["address_line2"].ToString();
          txtAddress3.Text = dr["address_line3"].ToString();
          subCode = dr["sub_code"].ToString();
          lblSubcode.Text = subCode;
          languageId = dr["language_id"].ToString();
          txtCity.Text = dr["city"].ToString();
          txtDistrict.Text = dr["district"].ToString();
            
          txtPinCode.Text = dr["pin_code"].ToString();
          textBoxMobileNumber.Text = dr["mobile_number"].ToString();
          txtCopies.Text = dr["num_copies"].ToString();
          txtRemarks.Text = dr["remarks"].ToString();
          lblAmtPaid.Text = dr["amount_paid"].ToString();
          DateTime startDate = Convert.ToDateTime(dr["start_date"]);
          lblStartDateDisp.Text = startDate.ToString("dd/MM/yyyy");
          cboStatus.SelectedIndex = cboStatus.FindStringExact(dr["status"].ToString());
          lblMagazine.Text = dr["mag_name"].ToString();
          txtDiscount.Text = "0.0";// Convert.ToDouble(dr["discount"]).ToString();
          cboCategory.SelectedIndex = cboCategory.FindStringExact(dr["category_name"].ToString());
          cboCountries.SelectedValue = dr["Country_id"].ToString();
          BindStates();
          cboStates.SelectedValue = dr["state_ID"];
          object[] dueDate = new object[2];
          dueDate = GlobalFn.CalculateDueDate(startDate, Convert.ToDouble(dr["amount_paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt32(dr["num_copies"]));
          lblBalance.Text = dueDate[1].ToString();
          lblDueDate.Text = Convert.ToDateTime(dueDate[0]).ToString("dd/MM/yyyy");
        }
      }
      catch (Exception eItems)
      {
        MessageBox.Show("Database error...", GlobalFn.FormText);
        GlobalFn.ProcessException(eItems, "Error in BindFields method in frmEditSubscription.cs");
        return;
      }

    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      string category = "";
      decimal discount = 0;
      switch (cboCategory.SelectedItem.ToString())
      {
        case "Bulk":
          category = "B";
          break;
        case "Free":
          category = "F";
          break;
        case "General":
          category = "G";
          break;
        case "Student":
          category = "S";
          break;
      }

      if (txtFirstName.TextLength == 0)
      {
        MessageBox.Show("Please enter the name", GlobalFn.FormText);
        //TurnOnSave();
        return;
      }
      if (txtAddress1.TextLength == 0)
      {
        MessageBox.Show("Please enter the Address", GlobalFn.FormText);
        //TurnOnSave();
        return;
      }
      if (txtCity.TextLength == 0)
      {
        MessageBox.Show("Please enter the City", GlobalFn.FormText);
        //TurnOnSave();
        return;
      }

      if (txtCopies.TextLength == 0)
      {
        MessageBox.Show("Please enter the Number of Copies", GlobalFn.FormText);
        //TurnOnSave();
        return;
      }

      if (!GlobalFn.IsNumeric(txtCopies.Text))
      {
        MessageBox.Show("Copies must be a number", GlobalFn.FormText);
        //TurnOnSave();
        return;
      }
      if (txtPinCode.TextLength > 0)
      {
        if (!GlobalFn.IsNumeric(txtPinCode.Text))
        {
          MessageBox.Show("PinCode must be a number", GlobalFn.FormText);
          //TurnOnSave();
          return;
        }
      }
      else
        txtPinCode.Text = "0";

            if (textBoxMobileNumber.Text.Length == 0)
            {
                MessageBox.Show("Please enter Mobile Number", GlobalFn.FormText);
                return;
            }
            if (textBoxMobileNumber.Text.Length != 10)
            {
                MessageBox.Show("Please enter a valid Mobile Number", GlobalFn.FormText);
                return;
            }
            if (!GlobalFn.IsNumeric(textBoxMobileNumber.Text))
            {
                MessageBox.Show("Mobile number must be in valid format", GlobalFn.FormText);
                //TurnOnSave();
                return;
            }

            try
            {
        int rtrn = SQL.EditSubscriber(subId, subCode, cboTitle.Text, txtLastName.Text, txtFirstName.Text, txtAddress1.Text, txtAddress2.Text, txtAddress3.Text, txtCity.Text, txtDistrict.Text, Convert.ToInt32(cboStates.SelectedValue), Convert.ToInt32(txtPinCode.Text), textBoxMobileNumber.Text, cboCountries.SelectedValue.ToString(), cboStatus.SelectedItem.ToString(), txtRemarks.Text, languageId, Convert.ToInt32(txtCopies.Text),category, discount);
        string msg = "Subscription has been modified successfully!";
        MessageBox.Show(msg, GlobalFn.FormText, MessageBoxButtons.OK, MessageBoxIcon.Information);
        if (MessageBox.Show("Do you want to Print Receipt?", GlobalFn.FormText, MessageBoxButtons.YesNo) == DialogResult.Yes)
        {
          FrmPrintBill frmBill = new FrmPrintBill(subId, new DateTime(), true);
          frmBill.ShowDialog();
          this.Close();
        }
        else
          this.Close();
      }
      catch (Exception eLang)
      {
        MessageBox.Show("Database error...", GlobalFn.FormText);
        GlobalFn.ProcessException(eLang, "Error in Updating Subscription in frmEditSubscription.cs");
        return;
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void txtAddress1_TextChanged(object sender, EventArgs e)
    {
      if (txtAddress1.TextLength > 30)
      {
        MessageBox.Show("Address1 is " + txtAddress1.TextLength.ToString() + "long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
      }
    }

    private void txtAddress2_TextChanged(object sender, EventArgs e)
    {
      if (txtAddress2.TextLength > 30)
      {
        MessageBox.Show("Address2 is " + txtAddress2.TextLength + " long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
      }
    }

    private void txtAddress3_TextChanged(object sender, EventArgs e)
    {
      if (txtAddress3.TextLength > 30)
      {
        MessageBox.Show("Address3 is " + txtAddress3.TextLength.ToString() + " long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
      }
    }

    private void txtFirstName_TextChanged(object sender, EventArgs e)
    {
      if (txtFirstName.TextLength > 30)
      {
        MessageBox.Show("First Name is " + txtFirstName.TextLength + " long. Limit it to less than or equal to 30 characters", GlobalFn.FormText);
      }
    }

    private void cboCountries_SelectionChangeCommitted(object sender, EventArgs e)
    {
      BindStates();
    }

    private void ButtonNewCountry_Click(object sender, EventArgs e)
    {
      frmManageCountries objNew = new frmManageCountries(true);
      objNew.ShowDialog();
      RefreshCountries();
    }
    private void RefreshCountries()
    {
      dtCountries.Clear();
      dtCountries = SQL.GetCountries().Tables[0];
      cboCountries.DataSource = dtCountries;
      if (cboCountries.Items.Count > 0)
      {
        int countIndex = cboCountries.FindString("India");
        if (countIndex > 0)
          cboCountries.SelectedIndex = countIndex;
      }

      //BindStates();
    }
    private void RefreshStates()
    {
      dtStates.Clear();
      dtStates = SQL.SubscribersGetStates(cboCountries.SelectedValue.ToString()).Tables[0];
      cboStates.DataSource = dtStates;
    }
    private void ButtonAddState_Click(object sender, EventArgs e)
    {
      frmManageStates objNew = new frmManageStates(true);
      objNew.ShowDialog();
      RefreshStates();
    }
  }
}