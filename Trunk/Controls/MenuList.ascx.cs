using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_MenuList : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var Menu = Checkers.Menus.Where(M => M.Menu_Status == 1).Select(M => M).OrderBy(M => M.Menu_Name);
        if (Menu.Count() > 0)
        {
            DgMenu.Visible = true;
            DgMenu.DataSource = Menu;
            DgMenu.DataBind();
        }
        else
        {
            DgMenu.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 0;
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.MenuDelete(int.Parse(HdnMenuId.Value));
        LtrMessage.Text = Status == 1 ? "Menu item deleted successfully." : "Error occurred.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
        var Menu = Checkers.Menus.Where(M => M.Menu_Status == 1).Select(M => M);
        if (Menu.Count() > 0)
        {
            DgMenu.Visible = true;
            DgMenu.DataSource = Menu;
            DgMenu.CurrentPageIndex = 0;
            DgMenu.DataBind();
        }
        else
        {
            DgMenu.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action cancelled by the user.";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var Menu = Checkers.Menus.Where(M => M.Menu_Status == 1 && M.Menu_Name.Equals(TxtName.Text)).Select(M => M);
        if (Menu.Count() > 0)
        {
            DgMenu.Visible = true;
            DgMenu.DataSource = Menu;
            DgMenu.DataBind();
        }
        else
        {
            DgMenu.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
    protected void DgMenu_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        HdnMenuId.Value = e.Item.Cells[0].Text;
        LtrMessage.Text = "Are you sure, you want to delete item " + e.Item.Cells[1].Text + " ?";
        BtnYes.Visible = true;
        BtnNo.Visible = true;
    }
    protected void DgMenu_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        Response.Redirect("Operation.aspx?Section=Menu&Action=Edit&Id=" + e.Item.Cells[0].Text);
    }
    protected void DgMenu_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DgMenu.CurrentPageIndex = e.NewPageIndex;
        DgMenu.DataBind();
    }
}
