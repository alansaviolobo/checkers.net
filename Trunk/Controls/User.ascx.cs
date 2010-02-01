using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_User : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Action;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClearForm();
            Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
            if (Request.QueryString["Id"] != null)
            {
                HdnUserId.Value = Request.QueryString["Id"].ToString();
                FillData();
            }
            switch (Action)
            {
                case "New": AutoCompSearch.Enabled = false; BtnSubmit.Text = "Add"; break;
                case "Edit": AutoCompSearch.Enabled = true; BtnSubmit.Text = "Update"; break;
                case "Delete": AutoCompSearch.Enabled = true; BtnSubmit.Text = "Delete"; break;
            }
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 4;
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            Status = Checkers.ContactNew(0, TxtName.Text, TxtUserName.Text, "checkers", DdlType.SelectedItem.Text, TxtPhone.Text, TxtAddress.Text, TxtEmail.Text, null, null, null, DateTime.Parse(Application["SalesSession"].ToString()));
            LtrMessage.Text = Status == 1 ? "User " + TxtName.Text + " Added." : "User Name " + TxtName.Text + " Already Exists.";
            if (Status == 1) Status = Checkers.ActivityNew("User " + TxtName.Text + " Added With Default Password - checkers", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

            ClearForm();
        }
        else if (Action == "Edit")
        {
            if (HdnUserId.Value != "")
            {
                Status = Checkers.ContactEdit(int.Parse(HdnUserId.Value), TxtName.Text, TxtUserName.Text, "checkers", DdlType.SelectedItem.Text, TxtPhone.Text, TxtAddress.Text, TxtEmail.Text, null, null, null);
                LtrMessage.Text = Status == 1 ? "User " + TxtName.Text + " Updated." : "User Name " + TxtName.Text + " Already Exists.";
                if (Status == 1) Status = Checkers.ActivityNew("User " + TxtName.Text + " Updated", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
            }
            else
                LtrMessage.Text = "Please Select An User.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnUserId.Value != null)
            {
                LtrMessage.Text = "User " + TxtName.Text + " Will Be Deleted Permenantly. Do You Want To Proceed ? ";
                BtnYes.Visible = true;
                BtnNo.Visible = true;
            }
            else
            {
                LtrMessage.Text = "Please Select An User.";
                ClearForm();
            }
        }
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = 0;
        if (Action == "Delete")
        {
            Status = Checkers.ContactDelete(int.Parse(HdnUserId.Value));
            LtrMessage.Text = Status == 1 ? "User " + TxtName.Text + " Deleted." : "User Name " + TxtName.Text + " Already Exists.";
            if (Status == 1) Status = Checkers.ActivityNew("User " + TxtName.Text + " Deleted", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
        }
        ClearForm();
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action Cancelled By The User.";
        ClearForm();
    }
    private void ClearForm()
    {
        Checkers = new CheckersDataContext();

        TxtName.Text = "";
        TxtUserName.Text = "";
        TxtPhone.Text = "";
        TxtAddress.Text = "";
        TxtEmail.Text = "";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    public void FillData()
    {
        Checkers = new CheckersDataContext();
        var Users = (from U in Checkers.Contacts
                     where U.Contact_Id == int.Parse(HdnUserId.Value)
                     select U).Single();
        TxtName.Text = Users.Contact_Name;
        TxtUserName.Text = Users.Contact_UserName;
        TxtPhone.Text = Users.Contact_Phone;
        TxtAddress.Text = Users.Contact_Address;
        TxtEmail.Text = Users.Contact_Email;

        DdlType.Items.FindByText(Users.Contact_Type).Selected = true;

        ReqVldUserName.Enabled = true;
    }
}
