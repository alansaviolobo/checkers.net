﻿using System;
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
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 2;
    }
    protected void BtnPurchase_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.PurchaseNew(int.Parse(HdnInventoryId.Value), decimal.Parse(TxtPurchaseQuantity.Text), DateTime.Parse(Application["SalesSession"].ToString()));
        LtrMessage.Text = Status == 1 ? "Quantity " + TxtPurchaseQuantity.Text + " Purchased For Item " + TxtName.Text + "." : "Error Occured.";
        if (Status == 1) Status = Checkers.ActivityNew("Quantity " + TxtPurchaseQuantity.Text + " Purchased For Item " + TxtName.Text, int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

        ClearForm();
    }
    private void ClearForm()
    {
        Checkers = new CheckersDataContext();

        TxtName.Text = "";
        TxtExistingQuantity.Text = "";
        TxtPurchaseQuantity.Text = "0";
        LblPurchaseUnit1.Text = "";
        LblPurchaseUnit2.Text = "";
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        if (HdnInventoryId.Value != "")
        {
            var IngrdientData = (from I in Checkers.Inventories where I.Inventory_Id == int.Parse(HdnInventoryId.Value) select I).Single();

            TxtExistingQuantity.Text = (IngrdientData.Inventory_Quantity / (IngrdientData.Inventory_ConversionUnit == 0 ? 1 : IngrdientData.Inventory_ConversionUnit)).ToString();
            LblPurchaseUnit1.Text = IngrdientData.Inventory_PurchaseUnit;
            LblPurchaseUnit2.Text = IngrdientData.Inventory_PurchaseUnit;
        }
        else
        {
            LtrMessage.Text = "Please Select An Inventory.";
        }
    }
}
