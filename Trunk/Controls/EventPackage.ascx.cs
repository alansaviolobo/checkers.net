using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_EventPackage : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static int EventId;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        if (!Page.IsPostBack)
        {
            EventId = int.Parse(Request.QueryString["EventId"].ToString());
            var EventDetails = Checkers.Events.Where(E => E.Event_Id == EventId).Select(E => E).Single();
            LtrEventName.Text = EventDetails.Event_Name;
            LtrEventOrganizer.Text = Checkers.Contacts.Where(C => C.Contact_Id == EventDetails.Event_Organizer.Value).Select(C => C.Contact_Name).Single();
            LtrEventDate.Text = EventDetails.Event_FromTimeStamp.ToString().Substring(0, 10).Replace("-", "/");
            LtrEventDate.Text += " - " + EventDetails.Event_ToTimeStamp.ToString().Substring(0, 10).Replace("-", "/");

            FillData();
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 6;
    }

    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = 1;
        Status = Checkers.EventPackageNew(EventId, int.Parse(HdnPackageId.Value), int.Parse(TxtQuantity.Text));
        LtrMessage.Text = Status == 1 ? "Package " + TxtName.Text + " Selected For the Event" : "Error Occurred";
        FillData();
        ClearForm();
    }

    protected void DgContent_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = 1;
        Status = Checkers.EventPackageDelete(int.Parse(e.Item.Cells[0].Text));
        LtrMessage.Text = Status == 1 ? "Package Delete From The Event" : "Error Occurred";
        FillData();
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

    public void ClearForm()
    {
        TxtName.Text = "";
        TxtQuantity.Text = "";
    }
}
