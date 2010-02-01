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
        Checkers = new CheckersDataContext();
        if ((Checkers.Miscellaneous.Where(M => M.Miscellaneous_Key == "SalesSession").Select(M => M.Miscellaneous_Value).Single()).ToString().Equals("0"))
        {
            RdoLstSalesType.Items.FindByText("Continue").Enabled = false;
            RdoLstSalesType.Items.FindByText("New").Selected = true;
        }
        else
        {
            RdoLstSalesType.Items.FindByText("Continue").Selected = true;
            RdoLstSalesType.Items.FindByText("New").Enabled = false;
        }
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
                if (RdoLstSalesType.SelectedItem.Text == "Continue")
                    Application["SalesSession"] = Checkers.Miscellaneous.Where(M => M.Miscellaneous_Key == "SalesSession").Select(M => M.Miscellaneous_Value).Single();
                else
                {
                    Checkers.MiscellaneousEdit("SalesSession", DateTime.Now.ToString());
                    Application["SalesSession"] = DateTime.Now.ToString();
                }

                int UserId = Checkers.Contacts.Where(U => U.Contact_UserName == TxtUserName.Text).Select(U => U.Contact_Id).Single();
                Checkers.MiscellaneousEdit("UserLogged", UserId.ToString());
                Application["UserId"] = UserId;

                Server.Transfer("~/Operation.aspx");
            }
            else
                LtrMessage.Text = "Please Check Username And Password";
        }
        else
            LtrMessage.Text = "Please Enter UserName And Password";
    }
}
