using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Operation : System.Web.UI.Page
{
    public CheckersDataContext Checkers;
    public static string Action, ControlName, Section;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["UserId"] == null)
        //{
        //    Response.Redirect("~/Default.aspx");
        //}
        //else
        //{
            if (!Page.IsPostBack)
            {

                Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
                Section = Request.QueryString["Section"] != null ? Request.QueryString["Section"].ToString() : "Home";

                switch (Action)
                {
                    case "New":
                    case "Edit":
                    case "Delete":
                        ControlName = Section;
                        break;
                    default:
                        ControlName = "New";
                        break;
                }
            }

            CntrlHolder.Controls.Add(LoadControl("~/Controls/"+Section+".ascx"));
        //}
    }
}
