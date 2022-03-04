using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using CIV.Classess;
using System.Drawing.Printing;
using System.IO;
using System.Runtime.InteropServices;   


namespace CIV
{
    public partial class frmDuesList : frmBaseForm
    {
        Printer pt = new Printer();

        string st1 = "";
        Object[] expiryDate = new Object[2];
        ArrayList al = new ArrayList();
        string subscriberList = "";
        DataSet ds;
        bool displayDue = true;

        String[] title = new String[4];
        String[] lastName = new String[4];
        String[] firstName = new String[4];
        String[] Address1 = new String[4];
        String[] Address2 = new string[4];
        String[] Address3 = new string[4];
        String[] subCode = new string[4];
        String[] city = new string[4];
        String[] district = new string[4];
        String[] state = new string[4];
        String[] pinCode = new string[4];
        String[] magazine = new string[4];
        String[] due = new string[4];
        String[] dueDate = new string[4];

        public void ResetAddressArray()
        {
            for (int k = 0; k < 4; k++)
            {
                title[k] = "---";
                lastName[k] = "---";
                firstName[k] = "---";
                Address1[k] = "---";
                Address2[k] = "---";
                Address3[k] = "---";
                subCode[k] = "---";
                city[k] = "---";
                district[k] = "---";
                state[k] = "---";
                pinCode[k] = "---";
                magazine[k] = "---";
                due[k] = "---";
                dueDate[k] = "---";
            }
        }

        public frmDuesList()
        {
            InitializeComponent();
            GlobalFn.SetCulture("en-GB");
            this.Text += " - " + GlobalFn.FormText;
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

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            String languageId = cboMagazine.SelectedValue.ToString();
            StringBuilder sb = new StringBuilder();
            String fromDistrict = txtDistrictFrom.Text.Trim();
            String toDistrict = txtDistrictTo.Text.Trim();
            String sqlScript = "";
            ArrayList distList = new ArrayList();
            ArrayList fNameList = new ArrayList();
            int fromPinCode = 0;
            int toPinCode = 0;
            bool isPincode = false;
            String fromFirstName = txtFirstNameFrom.Text;
            String toFirstName = txtFirstNameTo.Text;
            displayDue = true;

            if (rbtnIsGeneral.Checked)
            {
                sb.Append(" and category in ('G','S') ");
            }

            if(rbtnIsBulk.Checked)
            {
                sb.Append(" and category = 'B' ");
            }

            if (ddlCategory.SelectedItem.Equals("Overseas"))
            {
                sb.Append(" and country_id not in  ");
                sb.Append(" (select country_id from Countries where country_name = 'india') ");
                displayDue = false;
            }
            else if (ddlCategory.SelectedItem.Equals("Domestic"))
            {
                sb.Append(" and country_id in  ");
                sb.Append(" (select country_id from Countries where country_name = 'india') ");
            }

            if (cboPrintCriteria.SelectedItem.Equals("Pin Code"))
            {
                isPincode = true;
                // Make sure Pin Codes are entered
                if (txtPinCodeFrom.Text.Length == 0)
                {
                    MessageBox.Show("Please enter Pin Code From", GlobalFn.FormText);
                    return;
                }
                if (txtPinCodeTo.Text.Length == 0)
                {
                    MessageBox.Show("Please enter Pin Code To", GlobalFn.FormText);
                    return;
                }
                if (!(GlobalFn.IsNumeric(txtPinCodeFrom.Text)))
                {
                    MessageBox.Show("Please enter a valid Pin Code", GlobalFn.FormText);
                    return;
                }
                if (!GlobalFn.IsNumeric(txtPinCodeTo.Text))
                {
                    MessageBox.Show("Please enter a valid Pin Code", GlobalFn.FormText);
                    return;
                }
                fromPinCode = Convert.ToInt32(txtPinCodeFrom.Text);
                toPinCode = Convert.ToInt32(txtPinCodeTo.Text);
                sb.Append(" and pin_code between " + fromPinCode + " and " + toPinCode + " order by pin_code");
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
            else if (cboPrintCriteria.SelectedItem.Equals("First Name"))
            {
                isPincode = false;
                if (txtFirstNameFrom.Text.Length == 0)
                {
                    MessageBox.Show("Please enter First Name From", GlobalFn.FormText);
                    return;
                }
                if (txtFirstNameTo.Text.Length == 0)
                {
                    MessageBox.Show("Please enter First Name To", GlobalFn.FormText);
                    return;
                }
                fromFirstName = txtFirstNameFrom.Text;
                toFirstName = txtFirstNameTo.Text;

                if ((fromFirstName.Length == 1) && (toFirstName.Length == 1))
                {
                    ASCIIEncoding ascii = new ASCIIEncoding();

                    Byte[] fromFirstNameAscii = ascii.GetBytes(fromFirstName);
                    Byte[] toFirstNameAscii = ascii.GetBytes(toFirstName);

                    while (fromFirstNameAscii[0] <= toFirstNameAscii[0])
                    {
                        fNameList.Add(ascii.GetString(fromFirstNameAscii, 0, 1));
                        fromFirstNameAscii[0]++;
                    }

                    IEnumerator iEnum = fNameList.GetEnumerator();
                    int i = 0;
                    while (iEnum.MoveNext())
                    {
                        if (i == 0)
                            sb.AppendFormat(" and(first_name like '{0}%'", iEnum.Current.ToString());
                        else
                        {
                            sb.AppendFormat(" or first_name like '{0}%'", iEnum.Current.ToString());
                        }
                        i++;
                    }
                    sb.Append(") and pin_code = 0 order by first_name asc");
                    sqlScript = sb.ToString();
                }
                else
                {
                    isPincode = false;
                    sb.Append(" and first_name like '" + fromFirstName + "%' or first_name like '" + toFirstName + "%' and pin_code = 0 order by first_name asc");
                    sqlScript = sb.ToString();
                }
            }
            else
                sqlScript = sb.ToString();

            try
            {
                //Choose None equivalent... when len(sqlScript) = 0
                string dueDate = "";
                string today = DateTime.Today.ToString("yyyyMMdd");
                bool firstSub = false;
                string expSel = cboExpiry.SelectedItem.ToString();
                int dateDiff = 0;

                GlobalFn.StatusDisplayStart("Processing.. Please wait!");
                DataSet ds = SQL.DuesReportGetSubscribers(languageId, sqlScript, false);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    expiryDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["Start_Date"]), Convert.ToDouble(dr["Amount_Paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt32(dr["num_copies"]));
                    dueDate = Convert.ToDateTime(expiryDate[0]).ToString("yyyyMMdd");
                    dateDiff = SQL.GetDateDiff(dueDate, today);
                    
                    if (expSel.Equals("Two Months"))
                    {
                        if (dateDiff == 60)
                            al.Add(dr["subscriber_id"]);
                    }
                    else if (expSel.Equals("Less than Three Years"))
                    {
                        if ((dateDiff < 0) && (dateDiff >= -1095))
                            al.Add(dr["subscriber_id"]);
                    }
                    else if (expSel.Equals("More than Three Years"))
                    {
                        if (dateDiff < -1095)
                            al.Add(dr["subscriber_id"]);
                    }
                    else if (expSel.Equals("Print All Records"))
                    {
                        al.Add(dr["subscriber_id"]);
                    }
                    else
                    {
                        if ((dateDiff < 60) && (dateDiff >= 0))
                            al.Add(dr["subscriber_id"]);
                    }
                }
                
                if (al.Count == 0)
                {
                    MessageBox.Show("No Records Found!", GlobalFn.FormText);
                    return;
                }
                
                IEnumerator iEnum1 = al.GetEnumerator();
                
                while (iEnum1.MoveNext())
                {
                    if (firstSub)
                        subscriberList = subscriberList + ",";
                    subscriberList = subscriberList + iEnum1.Current.ToString();
                    firstSub = true;
                }
            
                sqlScript = " and subscriber_id in (" + subscriberList + ")";
            }
            catch (Exception eAl)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eAl, "Error in Getting Address List in frmDuesReport.cs");

                GlobalFn.StatusDisplayStop();
                return;
            }

            try
            {
                ds = SQL.DuesPrintList(languageId, sqlScript, isPincode);
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    //insert into print in table.
                    SQL.DuesListInsertDuesPrint(row["subscriber_id"].ToString());
                }
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ResetAddressArray();
                    printMain();
                    frmDuesPrint objPs = new frmDuesPrint(ds);
                    objPs.ShowDialog();
                }
                else
                {
                    MessageBox.Show("No Records found!!", GlobalFn.FormText);
                    return;
                }
            }
            catch (Exception eItems)
            {
                MessageBox.Show("Database error...", GlobalFn.FormText);
                GlobalFn.ProcessException(eItems, "Error in Print.cs");
                return;
            }
        }
       
        void printMain()
        {
            int rowCount = 0;
            String printSettings = "\x1b" + (char)67 + (char)8 + "\x1b" + (char)108 + (char)1 + "\x1b" + (char)81 + (char)200 + "\x1b" + (char)77 + "\x1b" + (char)32 + (char)1;
            pt.InitializePrinter(printSettings,cboPrinters.SelectedItem.ToString()); // enter printer name

            int i = 0;
            int pageRowCount = 0;

            //start by setting margins
            //String pageMargin = "\x1b" + (char)108 + (char)2 + "\x1b" + (char)81 + (char)134;
            int recordCount = ds.Tables[0].Rows.Count;
            while (i < 4 && rowCount < recordCount)
            {
                DataRow dr = ds.Tables[0].Rows[rowCount];

                if (i == 0)
                {
                    expiryDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["Start_Date"]), Convert.ToDouble(dr["Amount_Paid"]),Convert.ToDouble(dr["discount"]),Convert.ToInt32(dr["num_copies"]));

                    dueDate[i] = Convert.ToDateTime(expiryDate[0]).ToString("dd/MM/yyyy");
                    if (Convert.ToInt16(expiryDate[1]) >= 0)
                        due[i] = "";
                    else if (Convert.ToInt16(expiryDate[1]) < 0)
                        due[i] = " Due Rs." + expiryDate[1].ToString().Substring(1, expiryDate[1].ToString().Length - 1);

                    title[0] = dr["title"].ToString().Trim().Replace(".", "");
                    lastName[0] = dr["last_name"].ToString().Trim();
                    lastName[0] = dr["last_name"].ToString().Trim();
                    firstName[0] = dr["first_name"].ToString().Trim();
                    Address1[0] = dr["address_line1"].ToString().Trim();
                    Address2[0] = dr["address_line2"].ToString().Trim();
                    Address3[0] = dr["address_line3"].ToString().Trim();
                    subCode[0] = dr["sub_code"].ToString().Trim();
                    city[0] = dr["city"].ToString().Trim();
                    district[0] = dr["district"].ToString().Trim();
                    state[0] = dr["state"].ToString().Trim();
                    pinCode[0] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                    magazine[0] = dr["mag_name"].ToString().Trim();

                    if (Address2[i].Equals(""))
                    {
                        Address2[i] = dr["address_line3"].ToString().Trim();
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";

                        if (Address2[i].Equals(""))
                        {
                            Address2[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                            Address3[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                            city[i] = pinCode[i] = district[i] = state[i] = "";
                        }
                    }

                    if (Address3[i].Equals(""))
                    {
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";
                    }
                }
                else if (i == 1)
                {
                    expiryDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["Start_Date"]), Convert.ToDouble(dr["Amount_Paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt32(dr["num_copies"]));

                    dueDate[i] = Convert.ToDateTime(expiryDate[0]).ToString("dd/MM/yyyy");
                    if (Convert.ToInt16(expiryDate[1]) >= 0)
                        due[i] = "";
                    else if (Convert.ToInt16(expiryDate[1]) < 0)
                        due[i] = " Due Rs." + expiryDate[1].ToString().Substring(1, expiryDate[1].ToString().Length - 1);

                    title[1] = dr["title"].ToString().Trim().Replace(".", "");
                    lastName[1] = dr["last_name"].ToString().Trim();
                    firstName[1] = dr["first_name"].ToString().Trim();
                    Address1[1] = dr["address_line1"].ToString().Trim();
                    Address2[1] = dr["address_line2"].ToString().Trim();
                    Address3[1] = dr["address_line3"].ToString().Trim();
                    subCode[1] = dr["sub_code"].ToString().Trim();
                    city[1] = dr["city"].ToString().Trim();
                    district[1] = dr["district"].ToString().Trim();
                    state[1] = dr["state"].ToString().Trim();
                    pinCode[1] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                    magazine[1] = dr["mag_name"].ToString().Trim();

                    if (Address2[i].Equals(""))
                    {
                        Address2[i] = dr["address_line3"].ToString().Trim();
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";

                        if (Address2[i].Equals(""))
                        {
                            Address2[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                            Address3[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                            city[i] = pinCode[i] = district[i] = state[i] = "";
                        }
                    }

                    if (Address3[i].Equals(""))
                    {
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";
                    }
                }
                else if (i == 2)
                {
                    expiryDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["Start_Date"]), Convert.ToDouble(dr["Amount_Paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt32(dr["num_copies"]));

                    dueDate[i] = Convert.ToDateTime(expiryDate[0]).ToString("dd/MM/yyyy");
                    if (Convert.ToInt16(expiryDate[1]) >= 0)
                        due[i] = "";
                    else if (Convert.ToInt16(expiryDate[1]) < 0)
                        due[i] = " Due Rs." + expiryDate[1].ToString().Substring(1, expiryDate[1].ToString().Length - 1);

                    title[2] = dr["title"].ToString().Trim().Replace(".", "");
                    lastName[2] = dr["last_name"].ToString().Trim();
                    firstName[2] = dr["first_name"].ToString().Trim();
                    Address1[2] = dr["address_line1"].ToString().Trim();
                    Address2[2] = dr["address_line2"].ToString().Trim();
                    Address3[2] = dr["address_line3"].ToString().Trim();
                    subCode[2] = dr["sub_code"].ToString().Trim();
                    city[2] = dr["city"].ToString().Trim();
                    district[2] = dr["district"].ToString().Trim();
                    state[2] = dr["state"].ToString().Trim();
                    pinCode[2] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                    magazine[2] = dr["mag_name"].ToString().Trim();

                    if (Address2[i].Equals(""))
                    {
                        Address2[i] = dr["address_line3"].ToString().Trim();
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";

                        if (Address2[i].Equals(""))
                        {
                            Address2[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                            Address3[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                            city[i] = pinCode[i] = district[i] = state[i] = "";
                        }
                    }

                    if (Address3[i].Equals(""))
                    {
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";
                    }
                }

                else if (i == 3)
                {
                    expiryDate = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["Start_Date"]), Convert.ToDouble(dr["Amount_Paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt32(dr["num_copies"]));

                    dueDate[i] = Convert.ToDateTime(expiryDate[0]).ToString("dd/MM/yyyy");
                    if (Convert.ToInt16(expiryDate[1]) >= 0)
                        due[i] = "";
                    else if (Convert.ToInt16(expiryDate[1]) < 0)
                        due[i] = " Due Rs." + expiryDate[1].ToString().Substring(1, expiryDate[1].ToString().Length - 1);

                    title[3] = dr["title"].ToString().Trim().Replace(".", "");
                    lastName[3] = dr["last_name"].ToString().Trim();
                    firstName[3] = dr["first_name"].ToString().Trim();
                    Address1[3] = dr["address_line1"].ToString().Trim();
                    Address2[3] = dr["address_line2"].ToString().Trim();
                    Address3[3] = dr["address_line3"].ToString().Trim();
                    subCode[3] = dr["sub_code"].ToString().Trim();
                    city[3] = dr["city"].ToString().Trim();
                    district[3] = dr["district"].ToString().Trim();
                    state[3] = dr["state"].ToString().Trim();
                    pinCode[3] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                    magazine[3] = dr["mag_name"].ToString().Trim();

                    if (Address2[i].Equals(""))
                    {
                        Address2[i] = dr["address_line3"].ToString().Trim();
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";

                        if (Address2[i].Equals(""))
                        {
                            Address2[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                            Address3[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                            city[i] = pinCode[i] = district[i] = state[i] = "";
                        }
                    }

                    if (Address3[i].Equals(""))
                    {
                        Address3[i] = dr["city"].ToString().Trim() + " " + dr["district"].ToString().Trim() + " " + dr["state"].ToString().Trim();
                        city[i] = "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        pinCode[i] = district[i] = state[i] = "";
                    }
                    pageRowCount++;
                    i = -1;
                    PrintPage(pageRowCount);
                    if (pageRowCount > 6)
                    {
                        pageRowCount = 0;
                        pt.PageEnd();
                    }
                    //yet to handle if records in row < 4
                }
                rowCount++;
                i++;

            } // While end.
            if (recordCount % 4 != 0)
            {
              PrintPage(pageRowCount);
            }
            pt.ClosePrinter();
        }

        public void PrintPage(int pageRowCount)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                if (pageRowCount == 1)
                {
                    st1 = " " + (char)10 + (char)10;
                    sb.Append(st1);
                }

                if (displayDue)
                {
                    st1 = magazine[0].Remove(1) + "-" + subCode[0] + "=>Exp.Dt." + dueDate[0] + due[0];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 39; j++)
                        sb.Append(" ");
                    st1 = magazine[1].Remove(1) + "-" + subCode[1] + "=>Exp.Dt." + dueDate[1] + due[1];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 39; j++)
                        sb.Append(" ");
                    st1 = magazine[2].Remove(1) + "-" + subCode[2] + "=>Exp.Dt." + dueDate[2] + due[2];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 39; j++)
                        sb.Append(" ");
                    st1 = magazine[3].Remove(1) + "-" + subCode[3] + "=>Exp.Dt." + dueDate[3] + due[3] + (char)10;
                    sb.Append(st1);
                }
                else
                {
                    st1 = magazine[0].Remove(1) + "-" + subCode[0];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 39; j++)
                        sb.Append(" ");
                    st1 = magazine[1].Remove(1) + "-" + subCode[1];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 39; j++)
                        sb.Append(" ");
                    st1 = magazine[2].Remove(1) + "-" + subCode[2];
                    sb.Append(st1);
                    for (int j = st1.Length; j < 39; j++)
                        sb.Append(" ");
                    st1 = magazine[3].Remove(1) + "-" + subCode[3] + (char)10;
                    sb.Append(st1);
                }

                st1 = title[0] + ". " + lastName[0] + " " + firstName[0];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = title[1] + ". " + lastName[1] + " " + firstName[1];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = title[2] + ". " + lastName[2] + " " + firstName[2];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = title[3] + ". " + lastName[3] + " " + firstName[3] + (char)10;
                sb.Append(st1);

                st1 = Address1[0];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address1[1];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address1[2];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address1[3] + (char)10;
                sb.Append(st1);

                st1 = Address2[0];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address2[1];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address2[2];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address2[3] + (char)10;
                sb.Append(st1);

                st1 = Address3[0];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address3[1];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address3[2];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = Address3[3] + (char)10;
                sb.Append(st1);

                st1 = city[0] + " " + district[0] + " " + state[0];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = city[1] + " " + district[1] + " " + state[1];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = city[2] + " " + district[2] + " " + state[2];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = city[3] + " " + district[3] + " " + state[3] + (char)10;
                sb.Append(st1);

                st1 = pinCode[0];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = pinCode[1];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = pinCode[2];
                sb.Append(st1);
                for (int j = st1.Length; j < 39; j++)
                    sb.Append(" ");
                st1 = pinCode[3] + (char)10 + (char)10 + (char)10;
                sb.Append(st1);

                if (pageRowCount == 7)
                {
                    st1 = " " + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10;
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

        private void cboPrintCriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrintCriteria.SelectedItem.Equals("Pin Code"))
            {
                pinCodePanel.Visible = true;
                districtPanel.Visible = false;
                firstNamePanel.Visible = false;
            }
            else if (cboPrintCriteria.SelectedItem.Equals("District"))
            {
                districtPanel.Visible = true;
                pinCodePanel.Visible = false;
                firstNamePanel.Visible = false;
            }
            else if (cboPrintCriteria.SelectedItem.Equals("First Name"))
            {
                firstNamePanel.Visible = true;
                pinCodePanel.Visible = false;
                districtPanel.Visible = false;
            }
            else
            {
                pinCodePanel.Visible = false;
                districtPanel.Visible = false;
                firstNamePanel.Visible = false;
            }
        }

        private void frmDuesList_Load(object sender, EventArgs e)
        {
            ddlCategory.SelectedIndex = 0;
            cboPrintCriteria.SelectedIndex = 0;
            cboExpiry.SelectedIndex = 0;
        }
    }
}


