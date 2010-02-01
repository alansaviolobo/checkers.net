using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Converter : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Action;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
            ClearForm();
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 2;
    }
    protected void BtnEnter_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        if (HdnInventoryId.Value != "")
        {
            var Status = Checkers.ConverterNew(int.Parse(HdnMenuId.Value), int.Parse(HdnInventoryId.Value), decimal.Parse(TxtInventoryQuantity.Text), DateTime.Parse(Application["SalesSession"].ToString()));
            LtrMenuName.Text = TxtMenuName.Text;
            GridFill();
        }
        else
            LtrMessage.Text = "Please Select An Inventory Item.";
        ClearForm();
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = 0;

        var MenuConverterList = from C in Checkers.Converters
                                where C.Converter_Menu == int.Parse(HdnMenuId.Value) && C.Converter_Status == 1
                                select C;

        foreach (var M in MenuConverterList)
            Status = Checkers.ConverterDelete(M.Converter_Id);
        LtrMessage.Text = Status == 1 ? "Menu Item " + TxtMenuName.Text + " Deleted." : "Error Occurred";
        if (Status == 1) Status = Checkers.ActivityNew("Menu Item " + TxtMenuName.Text + " Deleted From Conversion", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
        ClearForm();
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action Cancelled By The User.";
        ClearForm();
    }
    public void GridFill()
    {
        Checkers = new CheckersDataContext();
        var MenuConverterValue = (from I in Checkers.Converters
                                  from In in Checkers.Inventories
                                  where I.Converter_Menu == int.Parse(HdnMenuId.Value) && I.Converter_Status == 1 && In.Inventory_Id == I.Converter_Inventory
                                  select new { I.Converter_Id, I.Converter_Menu, I.Converter_InventoryQuantity, In.Inventory_Name });
        if (Enumerable.Count(MenuConverterValue) > 0)
        {
            PnlConversionDetails.Visible = true;
            DgConverterList.Visible = true;
            DgConverterList.DataSource = MenuConverterValue.ToList();
            DgConverterList.DataBind();
            LtrMessage.Text = "Item Inventory Management.";
        }
        else
        {
            PnlConversionDetails.Visible = false;
            LtrMessage.Text = "No Converter Values Present For The Item " + LtrMenuName.Text;
            //TxtInventoryName.Text = "";
            //TxtInventoryQuantity.Text = "";
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        LtrMenuName.Text = TxtMenuName.Text;
        GridFill();
    }
    protected void DgConverterList_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = 0;

        Status = Checkers.ConverterDelete(int.Parse(e.Item.Cells[0].Text));
        if (Status == 1) Status = Checkers.ActivityNew("Inventory Item " + e.Item.Cells[1].Text + " Deleted From Conversion Table Of Menu " + TxtMenuName.Text, int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
        GridFill();
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        if (HdnMenuId.Value != "")
        {
            LtrMessage.Text = "Menu " + TxtMenuName.Text + " Will Be Deleted Permenantly From Converter. Do You Want To Proceed ? ";
            BtnYes.Visible = true;
            BtnNo.Visible = true;
        }
        else
        {
            LtrMessage.Text = "Please Select An Item.";
        }
    }
    public void ClearForm()
    {
        Checkers = new CheckersDataContext();
        TxtMenuName.Text = "";
        TxtInventoryName.Text = "";
        TxtInventoryQuantity.Text = "";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
}