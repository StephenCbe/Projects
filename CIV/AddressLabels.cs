using System;
using System.Collections;
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
    public partial class frmAddressLabels : frmBaseForm
    {
        DataSet ds;
        Printer pt;
        string st1;
        int pageCount = 0;
        bool isBulk = false;
        bool isFree = false;
      
        String[] numCopies = new string[3];
        String[] dueYears = new string[3];
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
            }
        }

        public frmAddressLabels()
        {
            InitializeComponent();
            this.Text += " - " + GlobalFn.FormText;
            dtpPostDate.CustomFormat = "dd/MM/yyyy";
            dtpPostDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            dtpPostDate.Value = DateTime.Today;
            if (cboPrintCriteria.Items.Count > 0)
                cboPrintCriteria.SelectedIndex = 0;
            BindLanguages();
            if (cboMagazine.Items.Count > 0)
                cboMagazine.SelectedIndex = 0;
            //isBulk = chkIsBulk.Checked;

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
            isBulk = rbtnBulk.Checked;
            isFree = rbtnFree.Checked;

            string sqlScript = "";
            int fromPinCode = 0;
            int toPinCode = 0;
            string fromDistrict = "";
            string toDistrict = "";
            pt = new Printer();
            ArrayList distList = new ArrayList();
            dtpPostDate.Enabled = false;
            StringBuilder sb = new StringBuilder();
            bool isPincode = false;
            ds = new DataSet();

            sb.Append(" and s.country_id in  ");
            sb.Append(" (select country_id from Countries where country_name = 'india') ");
          
            if (isBulk)
              sb.Append(" and s.category = 'B' ");
            else if (isFree)
              sb.Append(" and s.category = 'F' ");
            else
              sb.Append(" and s.category in ('G','S') ");
                      
            String postDate = dtpPostDate.Value.ToString();
            if (cboPrintCriteria.SelectedItem.Equals("Pin Code"))
            {
                isPincode = true;
                // Make sure Pin Codes are entered
                if (txtPinCodeFrom.Text.Length == 0)
                {
                    MessageBox.Show("Please enter Pin Code From",GlobalFn.FormText);
                    return;
                }
                if (txtPinCodeTo.Text.Length == 0)
                {
                    MessageBox.Show("Please enter Pin Code To",GlobalFn.FormText);
                    return;
                }
                if (!(GlobalFn.IsNumeric(txtPinCodeFrom.Text)))
                {
                    MessageBox.Show("Please enter a valid Pin Code",GlobalFn.FormText);
                    return;
                }
                if (!GlobalFn.IsNumeric(txtPinCodeTo.Text))
                {
                    MessageBox.Show("Please enter a valid Pin Code",GlobalFn.FormText);
                    return;
                }
                fromPinCode = Convert.ToInt32(txtPinCodeFrom.Text);
                toPinCode = Convert.ToInt32(txtPinCodeTo.Text);
                sb.Append(" and pin_code between " + fromPinCode +" and " + toPinCode +" order by pin_code");
                sqlScript = sb.ToString();
            }
            else if (cboPrintCriteria.SelectedItem.Equals("District"))
            {
                isPincode = false;
                if (txtDistrictFrom.Text.Length == 0)
                {
                    MessageBox.Show("Please enter District From", GlobalFn.FormText);
                    return;
                }
                if (txtDistrictTo.Text.Length == 0)
                {
                    MessageBox.Show("Please enter District To", GlobalFn.FormText);
                    return;
                }
                fromDistrict = txtDistrictFrom.Text;
                toDistrict = txtDistrictTo.Text;

                if ((fromDistrict.Length == 1) && (toDistrict.Length == 1))
                {
                    ASCIIEncoding ascii = new ASCIIEncoding();

                    Byte[] fromDistAscii = ascii.GetBytes(fromDistrict);
                    Byte[] toDistAscii = ascii.GetBytes(toDistrict);

                    while (fromDistAscii[0] <= toDistAscii[0])
                    {
                        distList.Add(ascii.GetString(fromDistAscii, 0, 1));
                        fromDistAscii[0]++;
                    }

                    IEnumerator iEnum = distList.GetEnumerator();
                    int i = 0;
                    while (iEnum.MoveNext())
                    {
                        if (i == 0)
                            sb.AppendFormat(" and(district like '{0}%'", iEnum.Current.ToString());
                        else
                        {
                            sb.AppendFormat(" or district like '{0}%'", iEnum.Current.ToString());
                        }
                        i++;
                    }
                    sb.Append(") and pin_code = 0 order by district asc");
                    sqlScript = sb.ToString();
                }
                else
                {
                    isPincode = false;
                    sb.Append(" and district like '" + fromDistrict + "%' or district like '" + toDistrict + "%' and pin_code = 0 order by district asc");
                    sqlScript = sb.ToString();
                }
            }
            else
            {
                sqlScript = sb.ToString();
            }
            
            String languageId = cboMagazine.SelectedValue.ToString();
           
            //pd.DefaultPageSettings.PaperSize = customPaperSize;
            GlobalFn.StatusDisplayStart("Printing Record Number:");
            //GlobalFn.StatusDisplayProgressLabel = "10/100";
          
           // GlobalFn.StatusDisplayStop();
            try
            {
                ds = SQL.GetPrintList(languageId, sqlScript, isPincode);
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
                    MessageBox.Show("No records found for your search criteria!", GlobalFn.FormText);
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

        //void printMain(object o, PrintPageEventArgs e)
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
                     if ((dr["category"].ToString().Equals("F")) || (dr["category"].ToString().Equals("B")))
                     {
                         dueYears[i] = "";
                     }
                     else
                     {
                         dueYears[i] = GetDueInYears(Convert.ToDateTime(dr["start_date"]), Convert.ToDouble(dr["amount_paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt16(dr["num_copies"])).ToString();
                         if (dueYears[i].Equals("0"))
                             dueYears[i] = "";
                         else
                             dueYears[i] = " (" + dueYears[i] + ")";
                     }

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

                     if (pin_Code.Length == 6)
                         state[i] = dr["stateAbbr"].ToString().Trim().ToUpper() + " " + pin_Code;
                     else
                         state[i] = dr["state"].ToString().Trim().ToUpper();

                     magazine[i] = dr["mag_name"].ToString().Trim();

                     if (Address2[i].Equals(""))
                     {
                         Address2[i] = dr["address_line3"].ToString().Trim();
                         Address3[i] = city[i];
                         city[i] = state[i];
                         state[i] = "";

                         if (Address2[i].Equals(""))
                         {
                             Address2[i] = Address3[i];
                             Address3[i] = city[i];
                             city[i] = "";
                         }
                     }

                     if (Address3[i].Equals(""))
                     {
                         Address3[i] = city[i];
                         city[i] = state[i];
                         state[i] = "";

                     }
 
                 }
                 else if (i == 1)
                 {
                     if ((dr["category"].ToString().Equals("F")) || (dr["category"].ToString().Equals("B")))
                     {
                         dueYears[i] = "";
                     }
                     else
                     {
                         dueYears[i] = GetDueInYears(Convert.ToDateTime(dr["start_date"]), Convert.ToDouble(dr["amount_paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt16(dr["num_copies"])).ToString();
                         if (dueYears[i].Equals("0"))
                             dueYears[i] = "";
                         else
                             dueYears[i] = " (" + dueYears[i] + ")";
                     }

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

                     if (pin_Code.Length == 6)
                       state[i] = dr["stateAbbr"].ToString().Trim().ToUpper() + " " + pin_Code;
                     else
                       state[i] = dr["state"].ToString().Trim().ToUpper();

                     magazine[i] = dr["mag_name"].ToString().Trim();

                     if (Address2[i].Equals(""))
                     {
                         Address2[i] = dr["address_line3"].ToString().Trim();
                         Address3[i] = city[i];
                         city[i] = state[i];
                         state[i] = "";

                         if (Address2[i].Equals(""))
                         {
                             Address2[i] = Address3[i];
                             Address3[i] = city[i];
                             city[i] = "";
                         }
                     }

                     if (Address3[i].Equals(""))
                     {
                         Address3[i] = city[i];
                         city[i] = state[i];
                         state[i] = "";

                     }
                 }
                 else if (i == 2)
                 {
                     if ((dr["category"].ToString().Equals("F")) || (dr["category"].ToString().Equals("B")))
                     {
                         dueYears[i] = "";
                     }
                     else
                     {
                         dueYears[i] = GetDueInYears(Convert.ToDateTime(dr["start_date"]), Convert.ToDouble(dr["amount_paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt16(dr["num_copies"])).ToString();
                         if (dueYears[i].Equals("0"))
                             dueYears[i] = "";
                         else
                             dueYears[i] = " (" + dueYears[i] + ")";
                     }

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

                     if (pin_Code.Length == 6)
                       state[i] = dr["stateAbbr"].ToString().Trim().ToUpper() + " " + pin_Code;
                     else
                       state[i] = dr["state"].ToString().Trim().ToUpper();

                     magazine[i] = dr["mag_name"].ToString().Trim();

                     if (Address2[i].Equals(""))
                     {
                         Address2[i] = dr["address_line3"].ToString().Trim();
                         Address3[i] = city[i];
                         city[i] = state[i];
                         state[i] = "";

                         if (Address2[i].Equals(""))
                         {
                             Address2[i] = Address3[i];
                             Address3[i] = city[i];
                             city[i] = "";
                         }
                     }

                     if (Address3[i].Equals(""))
                     {
                         Address3[i] = city[i];
                         city[i] = state[i];
                         state[i] = "";

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
        private int GetDueInYears(DateTime startDate, double amountPaid, double discount, int numcopies)
        {
            double numYear = 0.0;
            double dueInYears = 0;
            Object[] expiryDate = new Object[2];
            expiryDate = GlobalFn.CalculateDueDate(startDate, amountPaid, discount, numcopies);

            TimeSpan ts = DateTime.Today.Subtract(Convert.ToDateTime(expiryDate[0]));
            numYear = ts.TotalDays / 365.0;
            dueInYears = Math.Round(numYear);
            if (dueInYears < numYear)
                dueInYears++;
            if (dueInYears < 0)
                dueInYears = 0.0;
            return (int)dueInYears;
        }
        private void PrintPage()
        {
                try 
                {
                    StringBuilder sb = new StringBuilder();

                    //16br at start
                    //if (GlobalFn.GetMachineName().ToUpper().Equals("LAB1"))
                    //    st1 = "  "+ (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10;
                    //else
                      st1 = "  " + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10;
                    sb.Append(st1);

                    if (isBulk)
                    {
                        st1 = "   To Be Delivered At Window";
                        sb.Append(st1);

                        for (int j = st1.Length; j < 48; j++)
                            sb.Append(" ");

                        st1 = "To Be Delivered At Window";
                        sb.Append(st1);
                        
                        for (int j = st1.Length; j < 48; j++)
                            sb.Append(" ");

                        st1 = "To Be Delivered At Window" + (char)10;
                    }

                    else
                        st1 = " " + (char)10;
                        
                    sb.Append(st1);

                    st1 = "" + (char)10;
                    sb.Append(st1);

                    if (!chkBoxPrep.Checked)
                    {
                      st1 = "\x1b" + (char)14 + magazine[0].ToUpper(); 
                      sb.Append(st1);
                      
                      for (int j = st1.Length; j < 26; j++)
                        sb.Append(" ");
                      
                      st1 = magazine[1].ToUpper(); 
                      sb.Append(st1);
                      
                      for (int j = st1.Length; j < 23; j++)
                        sb.Append(" ");
                      
                      st1 = magazine[2].ToUpper() + (char)10;
                      sb.Append(st1);

                      st1 = "      " + "     PERIODICAL     " + "                        " + "       PERIODICAL             " + "                 "
                      + "      PERIODICAL     " + (char)10 + (char)10 + (char)10 ;
                      sb.Append(st1);
                    }
                    else
                    {
                        st1 = " " + (char)10 + (char)10 + (char)10 + (char)10 ;
                        sb.Append(st1);
                        if (pageCount != 0)
                        {
                            st1 = " " + (char)10;
                            sb.Append(st1);
                        }
                          
                    }

                    string noOfCopies = "            No. Copies: ";
                    //42sp  45sp
                    st1 = magazine[0].Remove(1) + "-" + subCode[0] + ((isBulk) ? noOfCopies + numCopies[0] : dueYears[0]);
                    sb.Append(st1);
                    for (int j = st1.Length; j < 48; j++)
                        sb.Append(" ");
                    st1 = magazine[1].Remove(1) + "-" + subCode[1] + ((isBulk) ? noOfCopies + numCopies[1] : dueYears[1]);
                    sb.Append(st1);
                    for (int j = st1.Length; j < 46; j++)
                        sb.Append(" ");
                    st1 = magazine[2].Remove(1) + "-" + subCode[2] + ((isBulk) ? noOfCopies + numCopies[2] : dueYears[2]) + (char)10;
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

                    for (int j = st1.Length; j < 46; j++)
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
                    for (int j = st1.Length; j < 46; j++)
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
                    for (int j = st1.Length; j < 46; j++)
                        sb.Append(" ");
                    st1 = Address2[2] ;
                    st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
                    sb.Append(st1);
                    sb.Append("" + (char)10);


                    st1 =  Address3[0];
                    st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
                    sb.Append(st1);
                    for (int j = st1.Length; j < 48; j++)
                        sb.Append(" ");
                    st1 = Address3[1];
                    st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
                    sb.Append(st1); for (int j = st1.Length; j < 46; j++)
                        sb.Append(" ");
                    st1 = Address3[2];
                    st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
                    sb.Append(st1); 
                    sb.Append("" + (char)10);


                    st1 =  city[0];
                    st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
                    sb.Append(st1); for (int j = st1.Length; j < 48; j++)
                        sb.Append(" ");
                    st1 = city[1];
                    st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
                    sb.Append(st1); for (int j = st1.Length; j < 46; j++)
                        sb.Append(" ");
                    st1 = city[2];
                    st1 = (st1.Length > 33) ? st1.Substring(0, 33) : st1.Substring(0, st1.Length).Trim();
                    sb.Append(st1); 
                    sb.Append("" + (char)10);


                    st1 = state[0];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 48; j++)
                        sb.Append(" ");
                    st1 =  state[1];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 46; j++)
                        sb.Append(" ");
                    st1 = state[2] + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 ;
                    sb.Append(st1);

                    if (!chkBoxPrep.Checked)
                    {
                        // ********* Comment for changes on 25.05.2021  *************
                        
                        if (!magazine[0].ToUpper().Contains("MRITUNJAY"))
                        {
                            st1 = "Licenced to post without prepayment";
                            sb.Append(" " + st1);
                            for (int j = st1.Length; j < 45; j++)
                                sb.Append(" ");
                            sb.Append(st1);
                            for (int j = st1.Length; j < 46; j++)
                                sb.Append(" ");
                            sb.Append(st1 + (char)10);

                            st1 = "posted at Egmore RMS1/Pathrika Channel";
                            sb.Append(st1);
                            for (int j = st1.Length; j < 44; j++)
                                sb.Append(" ");
                            sb.Append(st1);
                            for (int j = st1.Length; j < 46; j++)
                                sb.Append(" ");
                            sb.Append(st1 + (char)10);

                            st1 = "On " + label3.Text;
                            sb.Append("              " + st1);
                            for (int j = st1.Length; j < 44; j++)
                                sb.Append(" ");
                            sb.Append(st1);
                            for (int j = st1.Length; j < 46; j++)
                                sb.Append(" ");
                            sb.Append(st1 + (char)10 + (char)10);
                        }
                        else
                        {
                            sb.Append( " " + (char)10 + (char)10 + (char)10 + (char)10);
                        }
                        
                        
                        // ***** New changes on 25.05.2021 ********//
                        // sb.Append( " " + (char)10 + (char)10 + (char)10 + (char)10);


                        st1 = "If undelivered please return to";
                        sb.Append("   " + st1);
                        for (int j = st1.Length; j < 45; j++)
                            sb.Append(" ");
                        sb.Append(st1);
                        for (int j = st1.Length; j < 46; j++)
                            sb.Append(" ");
                        sb.Append(st1 + (char)10);

                        st1 = magazine[0].ToUpper();
                        sb.Append("          " + st1);
                        for (int j = st1.Length; j < 45; j++)
                            sb.Append(" ");
                        st1 = magazine[1].ToUpper();
                        sb.Append(st1);
                        for (int j = st1.Length; j < 46; j++)
                            sb.Append(" ");
                        st1 = magazine[2].ToUpper();
                        sb.Append(st1 + (char)10);

                        st1 = "9B, N.H.Rd, Chennai 600034";
                        sb.Append("     " + st1);
                        for (int j = st1.Length; j < 45; j++)
                            sb.Append(" ");
                        sb.Append(st1);
                        for (int j = st1.Length; j < 46; j++)
                            sb.Append(" ");
                        sb.Append(st1 + (char)10 + (char)10 + (char)10 + (char)12);
                    }
                    else
                    {
                        st1 = " " + (char)10 + (char)10;
                        sb.Append(st1);
                        
                        st1 = "On " + label3.Text + "    ";
                        sb.Append("      " + st1);
                        for (int j = st1.Length; j < 48; j++)
                            sb.Append(" ");
                        sb.Append(st1);
                        for (int j = st1.Length; j < 46; j++)
                            sb.Append(" ");
                        sb.Append(st1 + (char)10 + (char)10);

                        st1 = " " + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)12;
                        sb.Append(st1); 
                    }

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

        private void dtpPostDate_ValueChanged(object sender, EventArgs e)
        {
            label3.Text = dtpPostDate.Value.ToString("dd/MM/yyyy");
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            label3.Text = dtpPostDate.Value.ToString("dd/MM/yyyy");
        }

        private void cboPrintCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrintCriteria.SelectedItem.Equals("Pin Code"))
            {
                pinCodePanel.Visible = true;
                districtPanel.Visible = false;
            }
            else if (cboPrintCriteria.SelectedItem.Equals("District"))
            {
              pinCodePanel.Visible = false;
              districtPanel.Visible = true;
            }
            else
            {
              pinCodePanel.Visible = false;
              districtPanel.Visible = false;
            }
        }        
      }

}