using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIV.Classess;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;

namespace CIV
{
  public partial class frmPrintOverseasAddLbls : frmBaseForm
  {
    DataSet ds;
    Printer pt;
    string st1;
    int pageCount = 0;
    bool isBulk = false;

    String[] numCopies = new string[3];
    String[] title = new String[3];
    String[] lastName = new String[3];
    String[] firstName = new String[3];
    String[] Address1 = new String[3];
    String[] Address2 = new string[3];
    String[] Address3 = new string[3];
    String[] subCode = new string[3];
    String[] city = new string[3];
    String[] state = new string[3];
    String[] magazine = new string[3];
    String[] country = new string[3];
    String[] dueYears = new string[3];

    public void ResetAddressArray()
    {
      for (int k = 0; k < 3; k++)
      {
        numCopies[k] = "---";
        title[k] = "---";
        lastName[k] = "---";
        firstName[k] = "---";
        Address1[k] = "---";
        Address2[k] = "---";
        Address3[k] = "---";
        subCode[k] = "---";
        city[k] = "---";
        state[k] = "---";
        magazine[k] = "---";
        country[k] = "---";
        dueYears[k] = "";
      }
    }

    public frmPrintOverseasAddLbls()
    {
        InitializeComponent();
        this.Text += " - " + GlobalFn.FormText;
        dtpPostDate.CustomFormat = "dd/MM/yyyy";
        dtpPostDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
        dtpPostDate.Value = DateTime.Today;
        BindLanguages();
        if (cboMagazine.Items.Count > 0)
            cboMagazine.SelectedIndex = 0;
            
        PrintDocument prtdoc = new PrintDocument();
        string strDefaultPrinter = prtdoc.PrinterSettings.PrinterName;
        foreach (String strPrinter in PrinterSettings.InstalledPrinters)
        {
            cboPrinters.Items.Add(strPrinter);
            if (strPrinter == strDefaultPrinter)
            {
                cboPrinters.SelectedIndex = cboPrinters.Items.IndexOf(strPrinter);
            }
        }
    }

    private void BindLanguages()
    {
      try
      {
        cboMagazine.ValueMember = "language_id";
        cboMagazine.DisplayMember = "mag_name";
        cboMagazine.DataSource = SQL.SubscribersGetLanguages().Tables[0];
      }
      catch (Exception eItems)
      {
        MessageBox.Show("Database error...", GlobalFn.FormText);
        GlobalFn.ProcessException(eItems, "Error in Binding Languages combo box in Reports.cs");
        return;
      }
    }
    
    private void btnPrint_Click(object sender, EventArgs e)
    {
        isBulk = rbtnIsBulk.Checked;
        pt = new Printer();
        dtpPostDate.Enabled = false;
        StringBuilder sb = new StringBuilder();
        String sqlScript = "";
        
        if(isBulk)
          sb.Append(" and category = 'B'");
        else
          sb.Append(" and category in ('G','S') ");
          
        sb.Append(" and s.country_id not in ");
        sb.Append(" (select country_id from Countries where country_name = 'india') ");

        sqlScript = sb.ToString();
        
        String postDate = dtpPostDate.Value.ToString();
        String languageId = cboMagazine.SelectedValue.ToString();
        GlobalFn.StatusDisplayStart("Printing Record Number:");

        try
        {
          ds = SQL.GetOverseasPrintList(languageId, sqlScript);
          foreach (DataRow row in ds.Tables[0].Rows)
          {
            //insert into print in table.
            SQL.AddressLabelsInsertSubPrint(row["subscriber_id"].ToString());

          }
          if (ds.Tables[0].Rows.Count > 0)
          {
            ResetAddressArray();
            printMain();
          }
          else
          {
            MessageBox.Show("No records found!", GlobalFn.FormText);
            GlobalFn.StatusDisplayStop();
            return;
          }
        }
        catch (Exception eItems)
        {
          GlobalFn.StatusDisplayStop();
          MessageBox.Show("Database error...", GlobalFn.FormText);
          GlobalFn.ProcessException(eItems, "Error in Print.cs");
          return;
        }
    }
    
    void printMain()
    {
        string pin_Code = "";
        pageCount = 0;
        int rowCount = 0;
        String printerSettings = "\x1b" + (char)67 + (char)8 + "\x1b" + (char)80 + "\x1b" + (char)108 + (char)1 + "\x1b" + (char)81 + (char)129;
        pt.InitializePrinter(printerSettings,cboPrinters.SelectedItem.ToString());

        int i=0;
        
        int recordCount = ds.Tables[0].Rows.Count;
        while (i < 3 && rowCount < recordCount) 
        {
          GlobalFn.StatusDisplayProgressLabel = rowCount.ToString() + "/" + recordCount.ToString();
          if (GlobalFn.CancelPrint)
          {
            if (MessageBox.Show("Do you really want to cancel printing?", GlobalFn.FormText, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
              break;
            }
            else
              GlobalFn.CancelPrint = false;
          }
          System.Threading.Thread.Sleep(1);
          DataRow dr = ds.Tables[0].Rows[rowCount];

          if (i == 0)
          {
            numCopies[i] = dr["num_copies"].ToString();
            title[i] = dr["title"].ToString().Replace(".", "").Trim();
            lastName[i] = dr["last_name"].ToString().Trim();
            lastName[i] = dr["last_name"].ToString().Trim();
            firstName[i] = dr["first_name"].ToString().Trim();
            Address1[i] = dr["address_line1"].ToString().Trim();
            Address2[i] = dr["address_line2"].ToString().Trim();
            Address3[i] = dr["address_line3"].ToString().Trim();
            subCode[i] = dr["sub_code"].ToString().Trim();

            if (dr["district"].ToString().Trim().Length > 0)
              city[i] = dr["city"].ToString().Trim() + ", " + dr["district"].ToString().Trim();
            else
              city[i] = dr["city"].ToString().Trim();

            pin_Code = (dr["pin_code"].ToString().Trim().Equals("0")) ? "" : dr["pin_code"].ToString().Trim();

            if (pin_Code.Length > 0)
            {
              if (dr["stateAbbr"].ToString().Trim().Length > 0)
                state[i] = dr["stateAbbr"].ToString().Trim().ToUpper() + " " + pin_Code;
              else
                state[i] = dr["state"].ToString().Trim().ToUpper() + " " + pin_Code;
            }
            else
              state[i] = dr["state"].ToString().Trim().ToUpper();

            country[i] = dr["country"].ToString().Trim().ToUpper();
            magazine[i] = dr["mag_name"].ToString().Trim();

            if (Address2[i].Equals(""))
            {
              Address2[i] = dr["address_line3"].ToString().Trim();
              Address3[i] = city[i];
              city[i] = state[i];
              state[i] = country[i];
              country[i] = "";

              if (Address2[i].Equals(""))
              {
                Address2[i] = Address3[i];
                Address3[i] = city[i];
                city[i] = state[i];
                state[i] = "";
              }
            }

            if (Address3[i].Equals(""))
            {
              Address3[i] = city[i];
              city[i] = state[i];
              state[i] = country[i];
              country[i] = "";
            }

          }
          else if (i == 1)
          {
            numCopies[i] = dr["num_copies"].ToString();
            title[i] = dr["title"].ToString().Replace(".", "").Trim();
            lastName[i] = dr["last_name"].ToString().Trim();
            firstName[i] = dr["first_name"].ToString().Trim();
            Address1[i] = dr["address_line1"].ToString().Trim();
            Address2[i] = dr["address_line2"].ToString().Trim();
            Address3[i] = dr["address_line3"].ToString().Trim();
            subCode[i] = dr["sub_code"].ToString().Trim();

            if (dr["district"].ToString().Trim().Length > 0)
              city[i] = dr["city"].ToString().Trim() + ", " + dr["district"].ToString().Trim();
            else
              city[i] = dr["city"].ToString().Trim();

            pin_Code = (dr["pin_code"].ToString().Trim().Equals("0")) ? "" : dr["pin_code"].ToString().Trim();

            if (pin_Code.Length > 0)
            {
              if(dr["stateAbbr"].ToString().Trim().Length > 0)
                state[i] = dr["stateAbbr"].ToString().Trim().ToUpper() + " " + pin_Code;
              else
                state[i] = dr["state"].ToString().Trim().ToUpper() + " " + pin_Code;
            }
            else
              state[i] = dr["state"].ToString().Trim().ToUpper();

            country[i] = dr["country"].ToString().Trim().ToUpper();
            magazine[i] = dr["mag_name"].ToString().Trim();

            if (Address2[i].Equals(""))
            {
              Address2[i] = dr["address_line3"].ToString().Trim();
              Address3[i] = city[i];
              city[i] = state[i];
              state[i] = country[i];
              country[i] = "";
              
              if (Address2[i].Equals(""))
              {
                Address2[i] = Address3[i];
                Address3[i] = city[i];
                city[i] = state[i];
                state[i] = "";
              }
            }

            if (Address3[i].Equals(""))
            {
              Address3[i] = city[i];
              city[i] = state[i];
              state[i] = country[i];
              country[i] = "";
            }
          }
          else if (i == 2)
          {
            numCopies[i] = dr["num_copies"].ToString();
            title[i] = dr["title"].ToString().Replace(".", "").Trim();
            lastName[i] = dr["last_name"].ToString().Trim();
            firstName[i] = dr["first_name"].ToString().Trim();
            Address1[i] = dr["address_line1"].ToString().Trim();
            Address2[i] = dr["address_line2"].ToString().Trim();
            Address3[i] = dr["address_line3"].ToString().Trim();
            subCode[i] = dr["sub_code"].ToString().Trim();

            if (dr["district"].ToString().Trim().Length > 0)
              city[i] = dr["city"].ToString().Trim() + ", " + dr["district"].ToString().Trim();
            else
              city[i] = dr["city"].ToString().Trim();

            pin_Code = (dr["pin_code"].ToString().Trim().Equals("0")) ? "" : dr["pin_code"].ToString().Trim();

            if (pin_Code.Length > 0)
            {
              if (dr["stateAbbr"].ToString().Trim().Length > 0)
                state[i] = dr["stateAbbr"].ToString().Trim().ToUpper() + " " + pin_Code;
              else
                state[i] = dr["state"].ToString().Trim().ToUpper() + " " + pin_Code;
            }
            else
              state[i] = dr["state"].ToString().Trim().ToUpper();

            country[i] = dr["country"].ToString().Trim().ToUpper();
            magazine[i] = dr["mag_name"].ToString().Trim();

            if (Address2[i].Equals(""))
            {
              Address2[i] = dr["address_line3"].ToString().Trim();
              Address3[i] = city[i];
              city[i] = state[i];
              state[i] = country[i];
              country[i] = "";

              if (Address2[i].Equals(""))
              {
                Address2[i] = Address3[i];
                Address3[i] = city[i];
                city[i] = state[i];
                state[i] = "";
              }
            }

            if (Address3[i].Equals(""))
            {
              Address3[i] = city[i];
              city[i] = state[i];
              state[i] = country[i];
              country[i] = "";
            }
            PrintPage();
            pageCount++;
            i = -1;
          }
          rowCount++;
          i++;
        }

        if (recordCount % 3 != 0)
        {
          if (!GlobalFn.CancelPrint)
            PrintPage();
        }
        
        pt.ClosePrinter();
        GlobalFn.StatusDisplayStop();
        frmPrintStatus objPs = new frmPrintStatus(ds);
        objPs.ShowDialog();

    }
    
     private void PrintPage()
    {
        try 
        {
          StringBuilder sb = new StringBuilder();

          st1 = "  " + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10;
          sb.Append(st1);

          st1 = "\x1b" + (char)14 + "    AIR MAIL";
          sb.Append(st1);

          for (int j = st1.Length; j < 30; j++)
            sb.Append(" ");

          st1 = "AIR MAIL";
          sb.Append(st1);

          for (int j = st1.Length; j < 24; j++)
            sb.Append(" ");

          st1 = "AIR MAIL" + (char)10;
          sb.Append(st1);

          st1 = "" + (char)10;
          sb.Append(st1);

          st1 = "\x1b" + (char)14 + magazine[0].ToUpper(); 
          sb.Append(st1);

          for (int j = st1.Length; j < 26; j++)
              sb.Append(" ");

          st1 = magazine[1].ToUpper();
          sb.Append(st1);

          for (int j = st1.Length; j < 24; j++)
            sb.Append(" ");

          st1 = magazine[2].ToUpper() + (char)10;
          sb.Append(st1);
          st1 = "           PERIODICAL";                                      
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
              sb.Append(" ");

          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");
            
          sb.Append(st1 + (char)10 + (char)10 + (char)10);
            
          st1 = magazine[0].Remove(1) + "-" + subCode[0] + ((isBulk) ? "            No. Copies: " + numCopies[0] : dueYears[0]);
            sb.Append(st1);
            
          for (int j = st1.Length; j < 48; j++)
              sb.Append(" ");

          st1 = magazine[1].Remove(1) + "-" + subCode[1] + ((isBulk) ? "            No. Copies: " + numCopies[1] : dueYears[1]);
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");
          
          st1 = magazine[2].Remove(1) + "-" + subCode[2] + ((isBulk) ? "            No. Copies: " + numCopies[2] : dueYears[2]) + (char)10;
          sb.Append(st1);

          if (title[0].Length > 0)
          {
            if (lastName[0].Length > 0)
              st1 = title[0] + ". " + lastName[0] + " " + firstName[0];
            else
              st1 = title[0] + ". " + firstName[0];
          }
          else
          {
            if (lastName[0].Length > 0)
              st1 = lastName[0] + " " + firstName[0];
            else
              st1 = firstName[0];
          }
          sb.Append(st1);
            
          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          if (title[1].Length > 0)
          {
            if (lastName[1].Length > 0)
              st1 = title[1] + ". " + lastName[1] + " " + firstName[1];
            else
              st1 = title[1] + ". " + firstName[1];
          }
          else
          {
            if (lastName[1].Length > 0)
              st1 = lastName[1] + " " + firstName[1];
            else
              st1 = firstName[1];
          } 
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          if (title[2].Length > 0)
          {
            if (lastName[2].Length > 0)
              st1 = title[2] + ". " + lastName[2] + " " + firstName[2] + (char)10;
            else
              st1 = title[2] + ". " + firstName[2] + (char)10;
          }
          else
          {
            if (lastName[2].Length > 0)
              st1 = lastName[2] + " " + firstName[2] + (char)10;
            else
              st1 = firstName[2] + (char)10;
          } 
          sb.Append(st1);

          st1 = Address1[0];
          sb.Append(st1);
          
          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          st1 = Address1[1];
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
          sb.Append(" ");
          
          st1 = Address1[2] + (char)10;
          sb.Append(st1);
          
          st1 = Address2[0];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1);
          
          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          st1 = Address2[1];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");
          
          st1 = Address2[2];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1);
          sb.Append("" + (char)10);


          st1 = Address3[0];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1);
          
          for (int j = st1.Length; j < 48; j++)
          sb.Append(" ");

          st1 = Address3[1];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");
          
          st1 = Address3[2];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1); 
          sb.Append("" + (char)10);

          st1 = city[0];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1);
          
          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          st1 = city[1];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");
          
          st1 = city[2];
          st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
          sb.Append(st1); 
          sb.Append("" + (char)10);

          st1 = state[0];
          sb.Append(st1);
          
          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          st1 = state[1];
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");
          
          st1 = state[2] + (char)10;
          sb.Append(st1);

          st1 = country[0];
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          st1 = country[1];
          sb.Append(st1);

          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");

          st1 = country[2] + (char)10 + (char)10 + (char)10 + (char)10;
          sb.Append(st1);

          sb.Append(" " + (char)10 + (char)10 + (char)10 + (char)10);

          st1 = "If undelivered please return to";
          
          sb.Append(st1);
          for (int j = st1.Length; j < 48; j++)
              sb.Append(" ");

          sb.Append(st1);
          for (int j = st1.Length; j < 48; j++)
              sb.Append(" ");

          sb.Append(st1 + (char)10);

          st1 = magazine[0].ToUpper();
          
          sb.Append("       " + st1);
          for (int j = st1.Length; j < 48; j++)
              sb.Append(" ");

          st1 = magazine[1].ToUpper();
          sb.Append(st1);
          for (int j = st1.Length; j < 48; j++)
            sb.Append(" ");
          
          st1 = magazine[2].ToUpper();
          sb.Append(st1 + (char)10);

          st1 = "9B, N.H.Road, Chennai 600 034";
          sb.Append(" " + st1);
          for (int j = st1.Length; j < 46; j++)
              sb.Append(" ");
            
          sb.Append("  " + st1);
          for (int j = st1.Length; j < 48; j++)
              sb.Append(" ");

            sb.Append(st1 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)12);

          pt.printText = sb.ToString();
          pt.Print();
          ResetAddressArray();
        }
        catch (Exception ex)
        {
          MessageBox.Show(ex.Message);
        }
        pt.PageEnd();
    }
  }
}