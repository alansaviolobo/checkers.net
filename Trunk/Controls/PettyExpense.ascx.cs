using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_PettyExpense : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        var RecievedBy = from R in Checkers.Contacts
                         where R.Contact_Type != "Customer" && R.Contact_Status.Equals(1) 
                         select R;

        foreach (var R in RecievedBy)
        {
            if (R.Contact_Id == int.Parse(Session["UserId"].ToString()))
                LtrReceivedBy.Text = R.Contact_Name;
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 5;
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if ((from C in Checkers.PettyCashes select C.PettyCash_Balance).Any() == true)
        {
            var CashBalance = (from C in Checkers.PettyCashes select C.PettyCash_Balance).ToList().Last();

            if (decimal.Parse(CashBalance.ToString()) < decimal.Parse(TxtAmount.Text))
                LtrMessage.Text = "No Enough Balance Exists. ";
            else
            {
                Status = Checkers.PettyExpenseNew(decimal.Parse(TxtAmount.Text), TxtMerchandise.Text, decimal.Parse(TxtQuantity.Text), int.Parse(Session["UserId"].ToString()));
                LtrMessage.Text = Status == 1 ? "Expense Amount " + TxtAmount.Text + " Added. " : "Error Occurred.";
                if (Status == 1) Status = Checkers.ActivityNew("PettyExpense Of Amount " + TxtAmount.Text + " Added For Merchandise " + TxtMerchandise.Text + " Of Quantity " + TxtQuantity.Text, int.Parse(Session["UserId"].ToString()));
            }

            CashBalance = (from C in Checkers.PettyCashes select C.PettyCash_Balance).ToList().Last();

            LtrMessage.Text += "Total Balance " + CashBalance.ToString();
        }
        else
            LtrMessage.Text = "No Cash Exists.";
    }
}
