using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common : System.Web.UI.MasterPage
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        if (Application["UserId"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            if (!Page.IsPostBack)
                LtrUserLoggedIn.Text = Checkers.Contacts.Where(C => C.Contact_Id == int.Parse(Application["UserId"].ToString())).Select(C => C.Contact_Name).Single();
        }
        BtnStopSales.Visible = Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Status == 1).Select(S => S).Any() == true ? false : true;
    }
    protected void BtnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
    protected void BtnOrderSystem_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Order.aspx");
    }
    protected void BtnStopSales_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("~/SalesStop.aspx");
    }
}
