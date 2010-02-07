using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing.Printing;
using System.Collections;
using System.Drawing;

public class PrintPage
{
    public static ArrayList PrintContent = new ArrayList();

	public PrintPage()
	{
		
	}

    public void PageContent(ArrayList PrintData)
    {
        PrintDocument Pd = new PrintDocument();
        PrintContent = PrintData;
        Pd.PrinterSettings.PrinterName = "Adobe PDF";
        Pd.PrintPage += new PrintPageEventHandler(PrintPageContent);
        Pd.Print();
    }

    public void PrintPageContent(object sender, PrintPageEventArgs e)
    {
        Single YPos = 40;
        Single LeftMargin = 20;

        var PrintFont = new Font("Arial", 10);
        int ExtraSize = 0;

        foreach (string P in PrintContent)
        {
            e.Graphics.DrawString(P, PrintFont, Brushes.Black, LeftMargin, YPos + ExtraSize, new StringFormat());
            ExtraSize += 30;
        }

    }
}
