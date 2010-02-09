using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_EventList : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var Event = from E in Checkers.Events
                    from C in Checkers.Contacts
                    where E.Event_Status.Equals(1) && E.Event_Organizer == C.Contact_Id
                    select new { E.Event_Id, E.Event_Name, E.Event_Venue, E.Event_ToTimeStamp, E.Event_FromTimeStamp, C.Contact_Name };
        if (Event.Count() > 0)
        {
            DgEvent.Visible = true;
            DgEvent.DataSource = Event;
            DgEvent.DataBind();
        }
        else
        {
            DgEvent.Visible = false;
            LtrMessage.Text = "No Events Found.";
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 6;

    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action cancelled by the user.";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.EventDelete(int.Parse(HdnEventId.Value));
        LtrMessage.Text = Status == 1 ? "Menu item deleted successfully." : "Error occurred.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
        var Event = from E in Checkers.Events
                    from C in Checkers.Contacts
                    where E.Event_Status.Equals(1) && E.Event_Organizer == C.Contact_Id
                    select new { E, C.Contact_Name };
        if (Event.Count() > 0)
        {
            DgEvent.Visible = true;
            DgEvent.DataSource = Event;
            DgEvent.CurrentPageIndex = 0;
            DgEvent.DataBind();
        }
        else
        {
            DgEvent.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        string[] EventName = TxtName.Text.Split('-');
        Checkers = new CheckersDataContext();
        var Event = from E in Checkers.Events
                    from C in Checkers.Contacts
                    where E.Event_Status.Equals(1) && E.Event_Organizer == C.Contact_Id && E.Event_Name.Equals(EventName[0])
                    select new { E.Event_Id, E.Event_Name, E.Event_Venue, E.Event_ToTimeStamp, E.Event_FromTimeStamp, C.Contact_Name };

        if (Event.Count() > 0)
        {
            DgEvent.Visible = true;
            DgEvent.DataSource = Event;
            DgEvent.DataBind();
        }
        else
        {
            DgEvent.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
    protected void DgEvent_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        HdnEventId.Value = e.Item.Cells[0].Text;
        LtrMessage.Text = "Are you sure, you want to delete item " + e.Item.Cells[1].Text + " ?";
        BtnYes.Visible = true;
        BtnNo.Visible = true;
    }
    protected void DgEvent_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        Response.Redirect("Operation.aspx?Section=Event&Action=Edit&Id=" + e.Item.Cells[0].Text);
    }
    protected void DgEvent_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DgEvent.CurrentPageIndex = e.NewPageIndex;
        DgEvent.DataBind();
    }
    protected void DgEvent_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "Package")
            Response.Redirect("Operation.aspx?Section=EventPackage&EventId=" + e.Item.Cells[0].Text);
        else if (e.CommandName == "Details")
            Response.Redirect("Operation.aspx?Section=EventDetails&EventId=" + e.Item.Cells[0].Text);
    }
}