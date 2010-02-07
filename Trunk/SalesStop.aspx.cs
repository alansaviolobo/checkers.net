using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SalesStop : System.Web.UI.Page
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        decimal BarTotal = 0, RestaurantTotal = 0, CreditCard = 0, Credit = 0, Cash = 0, Cheque = 0, Total = 0;

        var InvoiceList = Checkers.ExecuteQuery<Invoice>("select * from invoice where invoice_status != 2 and convert(char(10), invoice_timestamp, 105) = '" + Application["SalesSession"].ToString().Substring(0, 10) + "'");

        foreach (var I in InvoiceList)
        {
            BarTotal += I.Invoice_AmountBar.Value;
            RestaurantTotal += I.Invoice_AmountRestaurant.Value;
            switch (I.Invoice_PaymentMode)
            {
                case "Cheque": Cheque += I.Invoice_Amount.Value; break;
                case "Cash": Cash += I.Invoice_Amount.Value; break;
                case "Credit Card": CreditCard += I.Invoice_Amount.Value; break;
                case "Credit": Credit += I.Invoice_Amount.Value; break;
            }
            Total += I.Invoice_Amount.Value;
        }

        LtrBar.Text = BarTotal.ToString();
        LtrRestaurant.Text = RestaurantTotal.ToString();
        LtrCash.Text = Cash.ToString();
        LtrCheque.Text = Cheque.ToString();
        LtrCredit.Text = Credit.ToString();
        LtrCreditCard.Text = CreditCard.ToString();
        LtrTotalSales.Text = Total.ToString();
    }

    protected void BtnConfirmStop_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        Checkers.MiscellaneousEdit("UserLogged", "0");
        Checkers.MiscellaneousEdit("SalesSession", "0");

        Application.Clear();
        Server.Transfer("~/Default.aspx");
    }

    protected void BtnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Operation.aspx");
    }
}
