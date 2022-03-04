using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CIV.Classess;
using System.Drawing.Printing;

namespace CIV
{
    public partial class FrmPrintBill : Form
    {
        int subscriberCode = 0;
        Object[] ExpDt = new Object[2];
        DateTime billDt;
        bool isAddressChange = false;
        public FrmPrintBill(int subID,DateTime billDate,bool addressChange)
        {
            InitializeComponent();
            billDt = billDate;
            subscriberCode = subID;
           
            this.Text += " - " + GlobalFn.FormText;

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
            isAddressChange = addressChange;

        }

        private void FrmPrintBill_Load(object sender, EventArgs e)
        {

            printReceipt();

            
        }

        private void printReceipt()
        {
            try
            {
                DataSet ds = SQL.GetSubscriberInfo(subscriberCode.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = ds.Tables[0].Rows[0];

                    bool isBulk = false;

                    if(dr["category"].ToString().Equals("B"))
                        isBulk = true;

                    DataSet dsReceipt = new DataSet();
                    dsReceipt = SQL.GetReceiptInfo(dr["sub_code"].ToString(), billDt, Convert.ToInt16(dr["language_id"]));
                    if (dsReceipt.Tables[0].Rows.Count > 0 || isAddressChange)
                    {
                        DataRow drReceipt = null;
                        if (!isAddressChange)
                        {
                            drReceipt = dsReceipt.Tables[0].Rows[0];
                        }

                        ExpDt = GlobalFn.CalculateDueDate(Convert.ToDateTime(dr["start_date"]), Convert.ToDouble(dr["amount_paid"]), Convert.ToDouble(dr["discount"]), Convert.ToInt16(dr["num_copies"]));

                        String numCopies = dr["num_copies"].ToString();
                        String title = dr["title"].ToString().Trim().Replace(".", "").ToUpper();
                        String firstName = dr["last_name"].ToString().Trim().ToUpper();
                        String lastName = dr["first_name"].ToString().Trim().ToUpper();
                        String Address1 = dr["address_line1"].ToString().Trim().ToUpper();
                        String Address2 = dr["address_line2"].ToString().Trim().ToUpper();
                        String Address3 = dr["address_line3"].ToString().Trim().ToUpper();
                        String subCode = dr["sub_code"].ToString().Trim().ToUpper();
                        String city = "";// dr["city"].ToString().Trim();
                        //String district = dr["district"].ToString().Trim();
                        String state = "";//dr["state"].ToString().Trim();
                        String pinCode = "";// "PINCODE" + " " + dr["pin_code"].ToString().Trim();
                        String magazine = dr["mag_name"].ToString().Trim().ToUpper();

                        if (dr["district"].ToString().Trim().Length > 0)
                            city = dr["city"].ToString().Trim().ToUpper() + ", " + dr["district"].ToString().Trim().ToUpper();
                        else
                            city = dr["city"].ToString().Trim().ToUpper();

                        pinCode = (dr["pin_code"].ToString().Trim().Equals("0")) ? "" : dr["pin_code"].ToString().Trim().ToUpper();

                        if (pinCode.Length > 0)
                            state = dr["stateAbbr"].ToString().Trim().ToUpper() + " " + pinCode.ToUpper();
                        else
                            state = dr["stateName"].ToString().Trim().ToUpper();

                        if (Address2.Equals(""))
                        {
                            Address2 = dr["address_line3"].ToString().Trim();
                            Address3 = city;
                            city = state;
                            state = "";

                            if (Address2.Equals(""))
                            {
                                Address2 = Address3;
                                Address3 = city;
                                city = "";
                            }
                        }

                        if (Address3.Equals(""))
                        {
                            Address3 = city;
                            city = state;
                            state = "";

                        }

                        Printer pt = new Printer();

                        

                        String initParam = "\x1b" + (char)67 + (char)8 + "\x1b" + (char)80 + "\x1b" + (char)108 + (char)1 + "\x1b" + (char)81 + (char)55 + "\x1b" + (char)32 + (char)1 + "\x1b" + (char)15;
                        pt.InitializePrinter(initParam, cboPrinters.SelectedItem.ToString());
                        StringBuilder sb = new StringBuilder();
                        String st1 = "";

                        if (isAddressChange)
                        {
                            st1 = "\x1b" + (char)14 + "   " + magazine + (char)10;
                            sb.Append(st1);

                            st1 = "                " + dr["mag_name"].ToString() + "  " + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "9B, Nungambakkam High Road, Chennai - 600 034 " + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "\x1b" + (char)14 + "        RECEIPT" + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "Change Of Address: (Sub Code: " + dr["sub_code"].ToString()+ ")" + (char)10 + (char)10;
                            sb.Append(st1);

                            //st1 = ((isBulk) ? "                    No Copies: " + numCopies;
                            //sb.Append(st1);
                            if (!isBulk)
                            {
                              if (title.Length > 0)
                                st1 = title + ". " + lastName + " " + firstName + (char)10;
                              else
                                st1 = lastName + " " + firstName + (char)10;
                            }
                            else
                            {
                              if (title.Length > 0)
                                st1 = title + ". " + lastName + " " + firstName + "           " + "No Copies: " + numCopies + (char)10;
                              else
                                st1 = lastName + " " + firstName + "           " + "No Copies: " + numCopies + (char)10;
                            }

                            sb.Append(st1);

                            st1 = Address1 + (char)10;
                            sb.Append(st1);

                            st1 = Address2 + (char)10;
                            sb.Append(st1);

                            st1 = Address3 + (char)10;
                            sb.Append(st1);

                            st1 = city + (char)10;
                            sb.Append(st1);

                            st1 = state + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "                              For The Editor" + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10 + (char)10;
                        }
                        else
                        {
                            st1 = "\x1b" + (char)14 + "   " + magazine + (char)10;
                            sb.Append(st1);

                            st1 = "                " + dr["Description"].ToString() + "  " + (char)10;
                            sb.Append(st1);

                            st1 = "9B, Nungambakkam High Road, Chennai - 600 034 " + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "\x1b" + (char)14 + "        RECEIPT" + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = " Bill.No: " + drReceipt["BillNo"].ToString() + "             Date: " + Convert.ToDateTime(drReceipt["Date"]).ToString("dd/MM/yyyy") + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "Received With Thanks From" + (char)10;
                            sb.Append(st1);

                            //st1 = ((isBulk) ? "                    No Copies: " + numCopies;
                            //sb.Append(st1);
                            if (!isBulk)
                            {
                              if (title.Length > 0)
                                st1 = title + ". " + lastName + " " + firstName + (char)10;
                              else
                                st1 = lastName + " " + firstName + (char)10;
                            }
                            else
                            {
                              if(title.Length > 0)
                                st1 = title + ". " + lastName + " " + firstName + "           " + "No Copies: " + numCopies + (char)10;
                              else
                                st1 = lastName + " " + firstName + "           " + "No Copies: " + numCopies + (char)10;
                            }

                            sb.Append(st1);

                            st1 = Address1 + (char)10;
                            sb.Append(st1);

                            st1 = Address2 + (char)10;
                            sb.Append(st1);

                            st1 = Address3 + (char)10;
                            sb.Append(st1);

                            st1 = city + (char)10;
                            sb.Append(st1);

                            st1 = state + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "The Sum Of Rs: " + drReceipt["Amount"].ToString() + " " + "towards Sub Code: " + dr["sub_code"].ToString() + (char)10 + "Expiry Date: " + Convert.ToDateTime(ExpDt[0]).ToString("dd/MM/yyyy") + (char)10 + (char)10;
                            sb.Append(st1);

                            st1 = "                              For The Editor" + (char)10 + (char)10 + (char)10 + (char)10 + (char)10;
                            
                        }
                        sb.Append(st1);


                        pt.printText = sb.ToString();
                        pt.Print();

                        pt.PageEnd();
                        pt.ClosePrinter();
                    }
                    else
                    {
                        //MessageBox.Show("error... no Transaction found in tblReceipts", GlobalFn.FormText);
                        return;
                    }

                    //select bill_num as BillNo, amount as Amount, payment_date as Date  from receipts


                }
            }
            catch (Exception eItems)
            {
                MessageBox.Show("Exception in print Receipts");
                GlobalFn.ProcessException(eItems, "Exception in print Receipts");
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReprint_Click(object sender, EventArgs e)
        {
            printReceipt();
        }
    }
}