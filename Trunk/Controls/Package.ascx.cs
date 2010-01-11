using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Package : System.Web.UI.UserControl
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
            ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 6;
            ClearForm();
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            Status = Checkers.PackageNew(TxtName.Text, DdlType.SelectedItem.Text, TxtComments.Text);
            LtrMessage.Text = Status == 1 ? "Package " + TxtName.Text + " Added." : "Package Name " + TxtName.Text + " Already Exists.";
            if (Status == 1) Status = Checkers.ActivityNew("Package " + TxtName.Text + " Added", int.Parse(Session["UserId"].ToString()));

            ClearForm();
        }
        else if (Action == "Edit")
        {
            if (HdnPackageId.Value != null)
            {
                Status = Checkers.PackageEdit(int.Parse(HdnPackageId.Value), TxtName.Text, DdlType.SelectedItem.Text, TxtComments.Text);
                LtrMessage.Text = Status == 1 ? "Package " + TxtName.Text + " Updated." : "Package Name " + TxtName.Text + " Already Exists.";
                if (Status == 1) Status = Checkers.ActivityNew("Package " + TxtName.Text + " Updated", int.Parse(Session["UserId"].ToString()));
            }
            else
                LtrMessage.Text = "Please Select An Item.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnPackageId.Value != null)
            {
                LtrMessage.Text = "Package " + TxtName.Text + " Will Be Deleted Permenantly. Do You Want To Proceed ? ";
                BtnYes.Visible = true;
                BtnNo.Visible = true;
            }
            else
            {
                LtrMessage.Text = "Please Select An Package.";
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
            Status = Checkers.PackageDelete(int.Parse(HdnPackageId.Value));
            LtrMessage.Text = Status == 1 ? "Package Name " + TxtName.Text + " Deleted." : "Error Occurred";
            if (Status == 1) Status = Checkers.ActivityNew("Package " + TxtName.Text + " Deleted", int.Parse(Session["UserId"].ToString()));
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
        TxtComments.Text = "";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
        DdlType.Items.Clear();
        DdlType.Items.Add(new ListItem("Birthday"));
        DdlType.Items.Add(new ListItem("Anniversary"));
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        var Package = Checkers.Packages.Where(P => P.Package_Id == int.Parse(HdnPackageId.Value)).Select(P => P).Single();
        TxtName.Text = Package.Package_Name;
        TxtComments.Text = Package.Package_Comments;
        DdlType.ClearSelection();
        DdlType.Items.FindByText(Package.Package_Type).Selected = true;
    }
}
