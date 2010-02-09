using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_EventDetails : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static int EventId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        if (!Page.IsPostBack)
        {
            Checkers = new CheckersDataContext();

            if (!Page.IsPostBack)
            {
                EventId = int.Parse(Request.QueryString["EventId"].ToString());
                var EventDetails = Checkers.Events.Where(E => E.Event_Id == EventId).Select(E => E).Single();
                LtrEventName.Text = EventDetails.Event_Name;
                LtrEventOrganizer.Text = Checkers.Contacts.Where(C => C.Contact_Id == EventDetails.Event_Organizer.Value).Select(C => C.Contact_Name).Single();
                LtrEventToDate.Text = EventDetails.Event_ToTimeStamp.ToString().Substring(0, 10).Replace("-", "/");
                LtrEventFromDate.Text = EventDetails.Event_FromTimeStamp.ToString().Substring(0, 10).Replace("-", "/");
                LtrEventVenue.Text = EventDetails.Event_Venue;
                LtrEventCost.Text = EventDetails.Event_Cost.ToString();

                FillData();
            }
            ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 6;
        }
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();

        var EventPackageItems = from E in Checkers.EventPackages
                                from P in Checkers.Packages
                                where E.EventPackage_Event == EventId && E.EventPackage_Package == P.Package_Id && E.EventPackage_Status == 1
                                select new { E.EventPackage_Id, P.Package_Name, E.EventPackage_Quantity, E.EventPackage_Cost };

        if (EventPackageItems.Count() > 0)
            DgContent.DataSource = EventPackageItems;
        else
            DgContent.DataSource = null;

        DgContent.DataBind();
    }
}
