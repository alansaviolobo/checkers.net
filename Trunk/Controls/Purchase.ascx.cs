using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Purchase : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Operation;

    protected void Page_Load(object sender, EventArgs e)
    {
        ClearForm();
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 1;
    }
    protected void BtnPurchase_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.PurchaseNew(int.Parse(HdnInventoryId.Value), decimal.Parse(TxtPurchaseQuantity.Text));
        LtrMessage.Text = Status == 1 ? "Quantity " + TxtPurchaseQuantity.Text + " Purchased For Item " + TxtName.Text + "." : "Error Occured.";
        if (Status == 1) Status = Checkers.ActivityNew("Quantity " + TxtPurchaseQuantity.Text + " Purchased For Item " + TxtName.Text, int.Parse(Session["UserId"].ToString()));
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 1;
        ClearForm();
    }
    private void ClearForm()
    {
        Checkers = new CheckersDataContext();

        TxtName.Text = "";
        TxtExistingQuantity.Text = "";
        TxtPurchaseQuantity.Text = "";
        TxtPurchaseQuantity.Text = "0";
        LtrPurchaseUnit1.Text = "";
        LtrPurchaseUnit2.Text = "";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        if (HdnInventoryId.Value != "")
        {
            var IngrdientData = (from I in Checkers.Inventories where I.Inventory_Id == int.Parse(HdnInventoryId.Value) select I).Single();

            TxtExistingQuantity.Text = (IngrdientData.Inventory_Quantity / IngrdientData.Inventory_ConversionUnit).ToString();
            LtrPurchaseUnit1.Text = IngrdientData.Inventory_PurchaseUnit;
            LtrPurchaseUnit2.Text = IngrdientData.Inventory_PurchaseUnit;
        }
        else
        {
            LtrMessage.Text = "Please Select An Inventory.";
        }
    }
}
