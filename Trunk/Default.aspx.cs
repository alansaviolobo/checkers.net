using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void BtnLogin_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        if (TxtUserName.Text != "" && TxtPassword.Text != "")
        {
            bool User = (from U in Checkers.Contacts
                         where U.Contact_UserName == TxtUserName.Text && U.Contact_Password == TxtPassword.Text && U.Contact_Status == 1
                         select U).Any();

            if (User == true)
            {
                Session["UserId"] = Checkers.Contacts.Where(U => U.Contact_UserName == TxtUserName.Text).Select(U => U.Contact_Id).Single();
                Session.Timeout = 60;

                Server.Transfer("~/Order.aspx");
            }
            else
                LtrMessage.Text = "Please Check Username And Password";
        }
        else
            LtrMessage.Text = "Please Enter UserName And Password";
    }
}
