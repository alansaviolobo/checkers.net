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
    }
    protected void BtnLogout_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Response.Redirect("~/Default.aspx");
    }
    protected void BtnOrderSystem_Click(object sender, ImageClickEventArgs e)
    {
        Checkers = new CheckersDataContext();

        bool Menu = Checkers.Menus.Where(M => M.Menu_Status == 1).Select(M => M).Any();
        bool User = Checkers.Contacts.Where(C => C.Contact_Status == 1 && C.Contact_Type == "Steward").Select(C => C).Any();

        if (Menu == false || User == false) ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:alert('Please Enter Stewards and Menu Items, before proceeding.');", true);
        else Response.Redirect("~/Order.aspx");
    }
    protected void BtnStopSales_Click(object sender, ImageClickEventArgs e)
    {
        Checkers = new CheckersDataContext();

        bool TableStatus = Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Status == 1).Select(S => S).Any();

        if (TableStatus == true) ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:alert('Please check the table status in the order page.');", true);
        else Server.Transfer("~/SalesStop.aspx");
    }
}
