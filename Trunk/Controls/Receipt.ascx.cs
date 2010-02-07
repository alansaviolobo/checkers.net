using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Receipt : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Id"] != null)
        {
            TxtReceiptNumber.Text = Request.QueryString["Id"].ToString();
            FillData();
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 8;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();
        NumberToEnglish Num = new NumberToEnglish();

        var Receipts = Checkers.Receipts.Where(R => R.Receipt_Id == int.Parse(TxtReceiptNumber.Text)).Select(R => R).Single();
        LblDate.Text = Receipts.Receipt_TimeStamp.ToString().Substring(0, 10);
        LblPaidTo.Text = "Quatro. Verna-Goa.";
        LblPaidBy.Text = Checkers.Contacts.Where(C => C.Contact_Id == Receipts.Receipt_Client.Value).Select(C => C.Contact_Name).Single();
        LblAmountNumber.Text = "Rs." + Receipts.Receipt_Amount.ToString() + "/-";
        LblAmountWord.Text = "Rs." + Num.changeCurrencyToWords(double.Parse(Receipts.Receipt_Amount.Value.ToString())).ToString();
    }
}
