using System;
using System.Configuration;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace CIV.Classess
{
	/// <summary>
	/// Summary description for GlobalFn.
	/// </summary>
	public class GlobalFn
	{
        public static bool CancelPrint = false;
        private static System.Drawing.Point appLocation = new Point(1, 1);

		public static string GetConnString
		{
			get { return (ConfigurationManager.AppSettings["connString"]); }
		}
        public static System.Drawing.Point AppLocation
        {
            get { return appLocation; }
            set { appLocation = value; }
        }
        public static String GetMachineName()
        {
            String machineName = Environment.MachineName;
            if(machineName.Length > 0)
                return machineName;
            else
            {
                return "Error - not available";
            }
        }
        public static string AppHomePath
        {
            get
            {
                return Application.StartupPath;
            }
        }
        public static string UserHomePath
        {
            get
            {

                if (Environment.OSVersion.ToString().Contains("NT"))
                {
                    return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\CIV\";
                }
                else
                {
                    return AppHomePath + @"\";
                }
            }
                

        }
        public static string LogDir
        {
            get { return UserHomePath + @"Logs\"; }
        }
        public static string ReportsDir
        {
            get { return UserHomePath + @"Reports\"; }
        }
        public static string LogFilePath
        {
            get { return LogDir + DateTime.Now.ToString("yyyyMMdd") + ".log"; }
        }
        public static void LogIt(string msg, string errorOK)
        {
            string currentDate = DateTime.Now.ToString("yyyyMMdd");
            string currentTime = DateTime.Now.ToString("HHmmss.fff");

            StringBuilder sb = new StringBuilder();
            if (msg.Length == 0)
                sb.Append("\r\n\r\n\r\n");
            else
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\r\n", currentDate, currentTime, errorOK, msg);

            lock (typeof(GlobalFn))
            {
                FileStream fs = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                byte[] fsCon = System.Text.Encoding.ASCII.GetBytes(sb.ToString());
                fs.Write(fsCon, 0, fsCon.Length);
                fs.Close();
            }
        }
		public static void ProcessException(Exception ex,string customMessage)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("Base Exception:\r\n{0}\r\n",ex.GetBaseException().ToString());
			sb.AppendFormat("Exception Message:\r\n{0}\r\n",ex.Message);
			sb.AppendFormat("Exception Stack Trace:\r\n{0}\r\n",ex.StackTrace);
			sb.AppendFormat("Exception Source:\r\n{0}\r\n",ex.Source);
			sb.AppendFormat("Custom Message:\r\n{0}\r\n",customMessage);
			sb.AppendFormat("Version:\r\n{0}\r\n",FormText);
			sb.AppendFormat("Date and Time of Error:\r\n{0}\r\n",Convert.ToString(DateTime.Now));

			LogIt(sb.ToString(),"ERR");
		}
		public static void SetCulture(string code) 
		{
			CultureInfo cultureObject = CultureInfo.CreateSpecificCulture(code);
			Thread.CurrentThread.CurrentUICulture = cultureObject;		
			Thread.CurrentThread.CurrentCulture = cultureObject;
		}
		public static string FixQuotes(string inputVal)
		{
			return inputVal.Replace("'","''");
		}
		public static string FormText
		{
			get { return "Magazine " + Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
		}
		public static bool IsNumeric(object num)
		{
			bool rtrn;
			double result;

			rtrn = Double.TryParse(num.ToString(),System.Globalization.NumberStyles.Any,System.Globalization.NumberFormatInfo.InvariantInfo,out result);
			return rtrn;
		}
        public static object[] CalculateDueDate(DateTime startDate, double amountPaid,double discount,int numCopies)
        {
            int startYear = Convert.ToInt32(startDate.ToString("yyyy"));
            DateTime dueDate = startDate;
            object[] returnVal = new object[2];
            //double currentAmount = 0.0; 
            double bal = amountPaid;
            double yearCost = 0.0;
            bool isBalNeg = false;

            while (!isBalNeg)
            {
                yearCost = Convert.ToDouble(SQL.RenewalExpiryDate(dueDate.ToString("yyyy")));
                //yearCost = (yearCost - (yearCost * discount / 100)) * numCopies;
                yearCost = yearCost * numCopies;

                if (yearCost > 0)
                {
                    if (bal >= yearCost)
                    {
                        bal -= yearCost;
                        dueDate = dueDate.AddYears(1);

                    }
                    else
                    {
                        isBalNeg = true;
                    }
                }
                else if (yearCost == 0)
                {
                    throw new Exception("Cost of magazine is not found for year:" + dueDate.ToString("yyyy"));
                    //MessageBox.Show("Cost of magazine is not found for year:" + startYear, GlobalFn.FormText);
                }
            }
            //calculate due;
            DateTime today = DateTime.Today;

            double due = 0.0;
            DateTime finDueDate = dueDate;
            while (finDueDate < today)
            {
                yearCost = Convert.ToDouble(SQL.RenewalExpiryDate(finDueDate.ToString("yyyy")));
                due += yearCost;
                finDueDate = finDueDate.AddYears(1);
            }
            due = bal - due; // deduct the left over balance from due date calculation.

            //lblDueDate.Text = startDate.ToString("dd/MM/" + Convert.ToString(startYear));
            //lblDueDate.Text = dueDate.ToString("dd/MM/yyyy");
            //lblBalance.Text = due.ToString();
            returnVal[0] = dueDate;
            returnVal[1] = due;
            return returnVal;


        }


        # region StatusDisplay

        private static Thread statusDisplayThread;
        private static string statusDisplayMainLabel;
        private static string statusDisplayProgressLabel;
        private static bool statusDisplayAbort;
        private static int statusDisplaySleepInterval;
        private static int statusDisplaySleepIntervalDefault = 100;

        /// <summary>
        /// Use to start a threaded version of Status Display with sleepInterval.
        /// </summary>
        /// <param name="mainLabel">Main label on the form.</param>
        /// <param name="sleepInterval">Time in msec to sleep between clock and progress label updates.</param>
        public static void StatusDisplayStart(string mainLabel, int sleepInterval)
        {
            statusDisplayMainLabel = mainLabel;
            statusDisplaySleepInterval = sleepInterval;
            statusDisplayProgressLabel = "";
            statusDisplayAbort = false;
            statusDisplayThread = new Thread(new ThreadStart(StartStatusDisplay));
            statusDisplayThread.IsBackground = true;
            statusDisplayThread.SetApartmentState(ApartmentState.MTA);
            statusDisplayThread.Name = "StatusDisplay Thread";
            statusDisplayThread.Start();
        }

        /// <summary>
        /// Use to start a threaded version of Status Display with default sleep interval.
        /// </summary>
        /// <param name="mainLabel">Main label on the form.</param>
        public static void StatusDisplayStart(string mainLabel)
        {
            StatusDisplayStart(mainLabel, statusDisplaySleepIntervalDefault);
        }

        /// <summary>
        /// Use to stop the threaded version of Status Display.
        /// </summary>
        public static void StatusDisplayStop()
        {
            statusDisplayAbort = true;
            while (statusDisplayThread.IsAlive)
            {
                System.Threading.Thread.Sleep(statusDisplaySleepInterval);
                System.Diagnostics.Debug.WriteLine("sleeping");
            }
            statusDisplayThread.Abort();
        }

        public static bool StatusDisplayAbort
        {
            get { return statusDisplayAbort; }
        }

        public static string StatusDisplayProgressLabel
        {
            get { return statusDisplayProgressLabel; }
            set { statusDisplayProgressLabel = value; }
        }

        private static void StartStatusDisplay()
        {
            StatusDisplay sd = new StatusDisplay(statusDisplayMainLabel, statusDisplaySleepInterval);
            sd.Show();
        }

        # endregion StatusDisplay
	}
}
