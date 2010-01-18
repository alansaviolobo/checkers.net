using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_PackageList : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        if (!Page.IsPostBack)
        {
            var Menu = Checkers.Packages.Where(P => P.Package_Status == 1).Select(P => P);
            if (Menu.Count() > 0)
            {
                DgPackage.Visible = true;
                DgPackage.DataSource = Menu;
                DgPackage.DataBind();
            }
            else
            {
                DgPackage.Visible = false;
                LtrMessage.Text = "No menu items found.";
            }
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 7;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var Package = Checkers.Packages.Where(P => P.Package_Status == 1 && P.Package_Name.Equals(TxtName.Text)).Select(P => P);
        if (Package.Count() > 0)
        {
            DgPackage.Visible = true;
            DgPackage.DataSource = Package;
            DgPackage.DataBind();
        }
        else
        {
            DgPackage.Visible = false;
            LtrMessage.Text = "No Package items found.";
        }
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.PackageDelete(int.Parse(HdnPackageId.Value));
        LtrMessage.Text = Status == 1 ? "Package item deleted successfully." : "Error occurred.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
        var Package = Checkers.Packages.Where(P => P.Package_Status == 1).Select(P => P);
        if (Package.Count() > 0)
        {
            DgPackage.Visible = true;
            DgPackage.DataSource = Package;
            DgPackage.DataBind();
        }
        else
        {
            DgPackage.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action cancelled by the user.";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    protected void DgPackage_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        HdnPackageId.Value = e.Item.Cells[0].Text;
        LtrMessage.Text = "Are you sure, you want to delete item " + e.Item.Cells[1].Text + " ?";
        BtnYes.Visible = true;
        BtnNo.Visible = true;
    }
    protected void DgPackage_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        Response.Redirect("Operation.aspx?Section=Package&Action=Edit&Id=" + e.Item.Cells[0].Text);
    }
}
