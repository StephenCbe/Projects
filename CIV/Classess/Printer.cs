using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;   

namespace CIV.Classess
{
    class Printer
    {
        System.IntPtr lhPrinter = new System.IntPtr();
        int pcWritten = 0;

        private String _printText = "";
        public String printText
        {
            get { return _printText; }
            set { _printText = value; }
        }

        public Printer()
        { }

        public void Print()
        {
            PrintDirect.WritePrinter(lhPrinter, _printText, _printText.Length, ref pcWritten);
        }

        public void InitializePrinter(String st1,string printerName)
        {
            DOCINFO di = new DOCINFO();

            // text to print with a form feed character     
            di.pDocName = "Magazine";
            di.pDataType = "RAW";

            // the \x1b means an ascii escape character
            //st1 = "\x1b*c600a6b0P\f";
            //lhPrinter contains the handle for the printer opened
            //If lhPrinter is 0 then an error has occured
            PrintDirect.OpenPrinter(printerName, ref lhPrinter, 0);
            PrintDirect.StartDocPrinter(lhPrinter, 1, ref di);
            PrintDirect.WritePrinter(lhPrinter, st1, st1.Length, ref pcWritten);

        }

        public void PageEnd()
        {
            PrintDirect.EndPagePrinter(lhPrinter);
        }

        public void ClosePrinter()
        {
            PrintDirect.EndDocPrinter(lhPrinter);
            PrintDirect.ClosePrinter(lhPrinter);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct DOCINFO
    {
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDocName;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pOutputFile;
        [MarshalAs(UnmanagedType.LPWStr)]
        public string pDataType;
    }

    public class PrintDirect
    {
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false,
       CallingConvention = CallingConvention.StdCall)]
        public static extern long OpenPrinter(string pPrinterName, ref IntPtr phPrinter, int pDefault);
        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = false,
       CallingConvention = CallingConvention.StdCall)]
        public static extern long StartDocPrinter(IntPtr hPrinter, int Level, ref DOCINFO pDocInfo);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
       CallingConvention = CallingConvention.StdCall)]
        public static extern long StartPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.drv", CharSet = CharSet.Ansi, ExactSpelling = true,
       CallingConvention = CallingConvention.StdCall)]
        public static extern long WritePrinter(IntPtr hPrinter, string data, int buf, ref int pcWritten);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
       CallingConvention = CallingConvention.StdCall)]
        public static extern long EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
       CallingConvention = CallingConvention.StdCall)]
        public static extern long EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.drv", CharSet = CharSet.Unicode, ExactSpelling = true,
       CallingConvention = CallingConvention.StdCall)]
        public static extern long ClosePrinter(IntPtr hPrinter);
    }
}
