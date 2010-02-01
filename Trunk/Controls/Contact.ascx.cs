using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Contact : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Action;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClearForm();
            Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
            switch (Action)
            {
                case "New": AutoCompSearchPersonalName.Enabled = false; AutoCompSearchOrganizationName.Enabled = false;
                    BtnSubmit.Text = "Add";
                    break;
                case "Edit": AutoCompSearchPersonalName.Enabled = true; AutoCompSearchOrganizationName.Enabled = true;
                    BtnSubmit.Text = "Update";
                    break;
                case "Delete": AutoCompSearchPersonalName.Enabled = true; AutoCompSearchOrganizationName.Enabled = true;
                    BtnSubmit.Text = "Delete";
                    break;
            }
            if (Request.QueryString["Id"] != null)
            {
                HdnContactId.Value = Request.QueryString["Id"].ToString();
                FillData();
            }
            ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 3;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            if (Checkers.Contacts.Where(C => C.Contact_Name == TxtPersonalName.Text && C.Contact_OrganizationName == TxtOrganizationName.Text).Any().Equals(false))
            {
                Status = Checkers.ContactNew(0, TxtPersonalName.Text, null, null, "Customer", TxtPersonalPhone.Text, TxtPersonalAddress.Text, TxtPersonalEmail.Text, TxtOrganizationName.Text, TxtOrganizationAddress.Text, TxtOrganizationPhone.Text, DateTime.Parse(Application["SalesSession"].ToString()));
                LtrMessage.Text = Status == 1 ? "Contact " + TxtPersonalName.Text + " Added." : "Contact With Phone Number " + TxtPersonalPhone.Text + " Already Exists.";
                if (Status == 1) Status = Checkers.ActivityNew("Contact " + TxtPersonalName.Text + " Added", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
                ClearForm();
            }
            else
                LtrMessage.Text = TxtPersonalName.Text + " For Organization " + TxtOrganizationName.Text + " Already Exists.";
        }
        else if (Action == "Edit")
        {
            if (HdnContactId.Value != "")
            {
                if (Checkers.Contacts.Where(C => C.Contact_Name == TxtPersonalName.Text && C.Contact_OrganizationName == TxtOrganizationName.Text && C.Contact_Id != int.Parse(HdnContactId.Value)).Any().Equals(false))
                {
                    Status = Checkers.ContactEdit(int.Parse(HdnContactId.Value), TxtPersonalName.Text, null, "checkers", "Customer", TxtPersonalPhone.Text, TxtPersonalAddress.Text, TxtPersonalEmail.Text, TxtOrganizationName.Text, TxtOrganizationAddress.Text, TxtOrganizationPhone.Text);
                    LtrMessage.Text = Status == 1 ? "Contact " + TxtPersonalName.Text + " Updated." : "Contact With Phone Number " + TxtPersonalPhone.Text + " Already Exists.";
                    if (Status == 1) Status = Checkers.ActivityNew("Contact " + TxtPersonalName.Text + " Updated", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
                }
                else
                    LtrMessage.Text = TxtPersonalName.Text + " For Organization " + TxtOrganizationName.Text + " Already Exists.";
            }
            else
                LtrMessage.Text = "Please Select An User.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnContactId.Value != null)
            {
                LtrMessage.Text = "User " + TxtPersonalName.Text + " Of Organization " + TxtOrganizationName.Text + " Will Be Deleted Permenantly. Do You Want To Proceed ? ";
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
            Status = Checkers.ContactDelete(int.Parse(HdnContactId.Value));
            LtrMessage.Text = Status == 1 ? "Contact " + TxtPersonalName.Text + " Of Organization " + TxtOrganizationName.Text + " Deleted." : "Contact With Phone Number " + TxtPersonalPhone.Text + " Already Exists.";
            if (Status == 1) Status = Checkers.ActivityNew("Contact " + TxtPersonalName.Text + " Deleted", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
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

        TxtPersonalName.Text = "";
        TxtPersonalPhone.Text = "";
        TxtPersonalAddress.Text = "";
        TxtPersonalEmail.Text = "";
        TxtOrganizationName.Text = "";
        TxtOrganizationPhone.Text = "";
        TxtOrganizationAddress.Text = "";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    protected void Search()
    {
        Checkers = new CheckersDataContext();
        var Users = (from U in Checkers.Contacts
                     where U.Contact_Id == int.Parse(HdnContactId.Value)
                     select U).Single();
        TxtPersonalName.Text = Users.Contact_Name;
        TxtPersonalPhone.Text = Users.Contact_Phone;
        TxtPersonalAddress.Text = Users.Contact_Address;
        TxtPersonalEmail.Text = Users.Contact_Email;
        TxtOrganizationName.Text = Users.Contact_OrganizationName;
        TxtOrganizationPhone.Text = Users.Contact_OrganizationPhone;
        TxtOrganizationAddress.Text = Users.Contact_OrganizationAddress;

        ReqVldPersonalName.Enabled = true;
    }
    public void FillData()
    {
        AutoCompSearchOrganizationName.Enabled = false;
        Search();
    }
}
