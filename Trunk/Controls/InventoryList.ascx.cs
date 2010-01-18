using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_InventoryList : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        if (!Page.IsPostBack)
        {
            var Inventory = Checkers.Inventories.Where(I => I.Inventory_Status == 1).Select(I => I);
            if (Inventory.Count() > 0)
            {
                DgInventory.Visible = true;
                DgInventory.DataSource = Inventory;
                DgInventory.DataBind();
            }
            else
            {
                DgInventory.Visible = false;
                LtrMessage.Text = "No menu items found.";
            }
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 2;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var Inventory = Checkers.Inventories.Where(I => I.Inventory_Status == 1 && I.Inventory_Name.Equals(TxtName.Text)).Select(I => I);
        if (Inventory.Count() > 0)
        {
            DgInventory.Visible = true;
            DgInventory.DataSource = Inventory;
            DgInventory.DataBind();
        }
        else
        {
            DgInventory.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.InventoryDelete(int.Parse(HdnInventoryId.Value));
        LtrMessage.Text = Status == 1 ? "Inventory item deleted successfully." : "Error occurred.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
        var Inventory = Checkers.Inventories.Where(I => I.Inventory_Status == 1).Select(M => M);
        if (Inventory.Count() > 0)
        {
            DgInventory.Visible = true;
            DgInventory.DataSource = Inventory;
            DgInventory.DataBind();
        }
        else
        {
            DgInventory.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action cancelled by the user.";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    protected void DgInventory_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        Response.Redirect("Operation.aspx?Section=Inventory&Action=Edit&Id=" + e.Item.Cells[0].Text);
    }
    protected void DgInventory_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        HdnInventoryId.Value = e.Item.Cells[0].Text;
        LtrMessage.Text = "Are you sure, you want to delete item " + e.Item.Cells[1].Text + " ?";
        BtnYes.Visible = true;
        BtnNo.Visible = true;
    }
}
