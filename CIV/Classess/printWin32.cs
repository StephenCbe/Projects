using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CIV.Classess
{
    class printWin32
    {
        printWin32(String printText)
        {
            if (!File.Exists(GlobalFn.AppHomePath + @"\temp\print.txt"))
                File.Create(GlobalFn.AppHomePath + @"\temp\print.txt");

            ASCIIEncoding ae = new ASCIIEncoding();

            byte[] b1 = new byte[200];

            b1 = ae.GetBytes(printText);

            FileStream fs = new FileStream(GlobalFn.AppHomePath + @"\temp\print.txt", FileMode.Truncate);
            fs.Write(b1, 0, printText.Length);
            fs.Close();
            fs.Dispose();

        }

    }
}
