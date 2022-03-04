using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;



namespace CIV
{
    public class PrintReports
    {

        Font f,f1;
        FontStyle style1 = FontStyle.Regular;
        FontStyle style2 = FontStyle.Bold; 
     
        //StreamReader sr;

        public void abc()
        {

            //sr = new StreamReader("a.txt");
            f = new Font("Courier New", 20, style2);
            f1 = new Font("Courier New", 12, style1);

            PrintDocument pd = new PrintDocument();

            pd.PrintPage += new PrintPageEventHandler(printMain);

            pd.Print();

        }

        void printMain(object o, PrintPageEventArgs e)
        {

            //float lpp = e.MarginBounds.Height / f.GetHeight(e.Graphics);

            //int c = 0;

            String s = "Christ is victor";

            //while (c < lpp && ((s = sr.ReadLine()) != null))
            //{

                float y = e.MarginBounds.Top;

                e.Graphics.DrawString(s, f, Brushes.Black, e.MarginBounds.Left+60, y);
                 //c++;
                 e.Graphics.DrawString("PERIODICAL", f1, Brushes.Black, e.MarginBounds.Left+150, y=y+30);
                // c++;
                 e.Graphics.DrawString("X102", f1, Brushes.Black, e.MarginBounds.Left, y+40);
                 //c++;
                e.Graphics.DrawString("MR XX XXXXXX", f1, Brushes.Black, e.MarginBounds.Left, y+55);
                //c++;
                e.Graphics.DrawString("NO 20 XX STREET", f1, Brushes.Black, e.MarginBounds.Left, y+70);
                //c++;
                e.Graphics.DrawString("XXXXX DIST", f1, Brushes.Black, e.MarginBounds.Left, y+85);
                //c++;
                e.Graphics.DrawString("XXXXXXXXX 600200", f1, Brushes.Black, e.MarginBounds.Left, y+100);
                //c++;
                e.Graphics.DrawString("XX XX X X", f1, Brushes.Black, e.MarginBounds.Left, y+115);
                y += 100;
                //c++;
                e.Graphics.DrawString("licenced to post without prepayment", f1, Brushes.Black, e.MarginBounds.Left+7, y+215);
                //c++;
                e.Graphics.DrawString("posted at Egmore RMS1/pathrika channel", f1, Brushes.Black, e.MarginBounds.Left, y+230);
                //c++;
                e.Graphics.DrawString("On XX-XX-XXXX", f1, Brushes.Black, e.MarginBounds.Left+100, y+245);
                //c++;
                e.Graphics.DrawString("If undelivered please return to:", f1, Brushes.Black, e.MarginBounds.Left+25, y+290);
                //c++;
                e.Graphics.DrawString(s, f1, Brushes.Black, e.MarginBounds.Left+55, y+305);
                //c++;
                e.Graphics.DrawString("9B, N.H. Road, Chennai 600 034", f1, Brushes.Black, e.MarginBounds.Left+34, y+320);
                //c++;


            //}

            //if (s != null)

            //    e.HasMorePages = true;

            //else

                e.HasMorePages = false;

        }

    }
}
