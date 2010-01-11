using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Event : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Action;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
            switch (Action)
            {
                case "New": BtnSearch.Visible = false; AutoCompSearch.Enabled = false; BtnSubmit.Text = "Add"; break;
                case "Edit": BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Update"; break;
                case "Delete": BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Delete"; break;
            }
            ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 5;
            ClearForm();
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            Status = Checkers.EventNew(TxtName.Text, DateTime.Parse(TxtFromDate.Text), DateTime.Parse(TxtToDate.Text), int.Parse(DdlOrganizer.SelectedItem.Value), TxtVenue.Text);
            LtrMessage.Text = Status == 1 ? "Event Item " + TxtName.Text + " Added." : "Item Name " + TxtName.Text + " Already Exists.";
            if (Status == 1) Status = Checkers.ActivityNew("Event Item " + TxtName.Text + " Added", int.Parse(Session["UserId"].ToString()));

            ClearForm();
        }
        else if (Action == "Edit")
        {
            if (HdnEventId.Value != "")
            {
                Status = Checkers.EventEdit(int.Parse(HdnEventId.Value), TxtName.Text, DateTime.Parse(TxtFromDate.Text), DateTime.Parse(TxtToDate.Text), int.Parse(DdlOrganizer.SelectedItem.Value), TxtVenue.Text);
                LtrMessage.Text = Status == 1 ? "Event Item " + TxtName.Text + " Updated." : "Item Name " + TxtName.Text + " Already Exists.";
                if (Status == 1) Status = Checkers.ActivityNew("Event Item " + TxtName.Text + " Updated", int.Parse(Session["UserId"].ToString()));
            }
            else
                LtrMessage.Text = "Please Select An Item.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnEventId.Value != "")
            {
                LtrMessage.Text = "Item " + TxtName.Text + " Will Be Deleted Permenantly. Do You Want To Proceed ? ";
                BtnYes.Visible = true;
                BtnNo.Visible = true;
            }
            else
            {
                LtrMessage.Text = "Please Select An Item.";
                ClearForm();
            }
        }
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;
        if (Action == "Delete")
        {
            Status = Checkers.EventDelete(int.Parse(HdnEventId.Value));
            LtrMessage.Text = Status == 1 ? "Event Item " + TxtName.Text + " Deleted." : "Error Occurred";
            if (Status == 1) Status = Checkers.ActivityNew("Event Item " + TxtName.Text + " Deleted", int.Parse(Session["UserId"].ToString()));
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
        TxtVenue.Text = "";
        TxtFromDate.Text = "";
        TxtToDate.Text = "";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
        DdlOrganizer.Items.Clear();

        var Organizer = Checkers.Contacts.Where(C => C.Contact_Type == "Customer" && C.Contact_Status.Equals(1)).Select(C => C);

        foreach (var O in Organizer)
            DdlOrganizer.Items.Add(new ListItem(O.Contact_Name + " (" + O.Contact_OrganizationName + ")", O.Contact_Id.ToString()));
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var EventDetails = Checkers.Events.Where(M => M.Event_Id == int.Parse(HdnEventId.Value) && M.Event_Status == 1).Select(M => M).Single();
        DdlOrganizer.ClearSelection();
        DdlOrganizer.Items.FindByValue(EventDetails.Event_Organizer.ToString()).Selected = true;
        TxtVenue.Text = EventDetails.Event_Venue;
        TxtName.Text = EventDetails.Event_Name;
        TxtFromDate.Text = EventDetails.Event_FromTimeStamp.ToString();
        TxtToDate.Text = EventDetails.Event_ToTimeStamp.ToString();
    }
}