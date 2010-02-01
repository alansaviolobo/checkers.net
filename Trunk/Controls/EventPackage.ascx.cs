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
        if (!Page.IsPostBack)
        {
            EventId = int.Parse(Request.QueryString["EventId"].ToString());
            var EventDetails = Checkers.Events.Where(E => E.Event_Id == EventId).Select(E => E);
        }
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
    }
    protected void DgContent_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
    }
}
