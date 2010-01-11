using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Menu : System.Web.UI.UserControl
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
            ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 0;
            ClearForm();
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            Status = Checkers.MenuNew(0, TxtName.Text, DdlCategory.SelectedItem.Text, decimal.Parse(TxtPrice.Text));
            LtrMessage.Text = Status == 1 ? "Menu Item " + TxtName.Text + " Added." : "Item Name " + TxtName.Text + " Already Exists.";
            if (Status == 1) Status = Checkers.ActivityNew("Menu Item " + TxtName.Text + " Added", int.Parse(Session["UserId"].ToString()));

            ClearForm();
        }
        else if (Action == "Edit")
        {
            if (HdnMenuId.Value != "")
            {
                Status = Checkers.MenuEdit(int.Parse(HdnMenuId.Value), TxtName.Text, DdlCategory.SelectedItem.Text, decimal.Parse(TxtPrice.Text));
                LtrMessage.Text = Status == 1 ? "Menu Item " + TxtName.Text + " Updated." : "Item Name " + TxtName.Text + " Already Exists.";
                if (Status == 1) Status = Checkers.ActivityNew("Menu Item " + TxtName.Text + " Updated", int.Parse(Session["UserId"].ToString()));
            }
            else
                LtrMessage.Text = "Please Select An Item.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnMenuId.Value != "")
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
            Status = Checkers.MenuDelete(int.Parse(HdnMenuId.Value));
            LtrMessage.Text = Status == 1 ? "Menu Item " + TxtName.Text + " Deleted." : "Error Occurred";
            if (Status == 1) Status = Checkers.ActivityNew("Menu Item " + TxtName.Text + " Deleted", int.Parse(Session["UserId"].ToString()));
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
        TxtPrice.Text = "";
        TxtPrice.Text = "0";
        LtrMenuItems.Text = "";
        LtrBarItems.Text = "";
        LtrRestaurantItems.Text = "";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
        DdlCategory.Items.Clear();

        DdlCategory.Items.Add(new ListItem("Restaurant"));
        DdlCategory.Items.Add(new ListItem("Bar"));

        LtrBarItems.Text = Checkers.SelectItemByType("Bar").ToString();
        LtrRestaurantItems.Text = Checkers.SelectItemByType("Restaurant").ToString();
        LtrMenuItems.Text = (int.Parse(LtrRestaurantItems.Text) + int.Parse(LtrBarItems.Text)).ToString();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var MenuDetails = Checkers.Menus.Where(M => M.Menu_Id == int.Parse(HdnMenuId.Value) && M.Menu_Status == 1).Select(M => M).Single();
        DdlCategory.ClearSelection();
        DdlCategory.Items.FindByText(MenuDetails.Menu_Category.ToString()).Selected = true;
        TxtPrice.Text = MenuDetails.Menu_SellingPrice.ToString();
    }
}
