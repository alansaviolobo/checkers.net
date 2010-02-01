using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_PettyCash : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        var RecievedBy = from R in Checkers.Contacts
                         where R.Contact_Type != "Customer" && R.Contact_Status.Equals(1) && R.Contact_Name != "Administrator"
                         select R;

        foreach (var R in RecievedBy)
        {
            if (R.Contact_Id == int.Parse(Application["UserId"].ToString()))
                LtrReceivedBy.Text = R.Contact_Name;

            DdlGivenBy.Items.Add(new ListItem(R.Contact_Name, R.Contact_Id.ToString()));
        }

        FillData();

        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 5;
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.PettyCashNew(decimal.Parse(TxtAmount.Text), int.Parse(DdlGivenBy.SelectedItem.Value), int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

        var CashBalance = (from C in Checkers.PettyCashes select C.PettyCash_Balance).ToList().Last();
        LtrMessage.Text = Status == 1 ? "Amount " + TxtAmount.Text + " Added. Total Balance " + CashBalance.ToString() : "Error Occurred.";
        if (Status == 1) Status = Checkers.ActivityNew("PettyCash Of Amount " + TxtAmount.Text + " Added", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
        FillData();
    }

    public void FillData()
    {
        if (Checkers.PettyCashes.Any() == true)
            LtrAvailableAmount.Text = (from C in Checkers.PettyCashes select C.PettyCash_Balance).ToList().Last().Value.ToString();
        else
            LtrAvailableAmount.Text = "0";
    }
}
