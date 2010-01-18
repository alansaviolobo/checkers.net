﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Inventory : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Action;

    protected void Page_Load(object sender, EventArgs e)
    {
        ClearForm();
        if (!Page.IsPostBack)
        {
            Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
            if (Request.QueryString["Id"] != null)
            {
                HdnInventoryId.Value = Request.QueryString["Id"].ToString();
                FillData();
            }
            switch (Action)
            {
                case "New": BtnSearch.Visible = false; AutoCompSearch.Enabled = false; BtnSubmit.Text = "Add"; break;
                case "Edit": BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Update"; break;
                case "Delete": BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Delete"; break;
            }
            ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 2;
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        TxtThreshold.Text = TxtThreshold.Text == "" ? "1" : TxtThreshold.Text;
        TxtConversionUnit.Text = TxtConversionUnit.Text == "" ? "1" : TxtConversionUnit.Text;
        TxtBuyingPrice.Text = TxtBuyingPrice.Text == "" ? "1" : TxtBuyingPrice.Text;

        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            Status = Checkers.InventoryNew(TxtName.Text, decimal.Parse(TxtBuyingPrice.Text), decimal.Parse(TxtThreshold.Text), DdlPurchaseUnit.SelectedItem.Text, decimal.Parse(TxtConversionUnit.Text));
            LtrMessage.Text = Status == 1 ? "Inventory Item " + TxtName.Text + " Added." : "Item Name " + TxtName.Text + " Already Exists.";
            if (Status == 1) Status = Checkers.ActivityNew("Inventory Item " + TxtName.Text + " Added", int.Parse(Session["UserId"].ToString()));

            ClearForm();
        }
        else if (Action == "Edit")
        {
            if (HdnInventoryId.Value != "")
            {
                Status = Checkers.InventoryEdit(int.Parse(HdnInventoryId.Value), TxtName.Text, decimal.Parse(TxtBuyingPrice.Text), decimal.Parse(TxtThreshold.Text), DdlPurchaseUnit.SelectedItem.Text, decimal.Parse(TxtConversionUnit.Text));
                LtrMessage.Text = Status == 1 ? "Inventory Item " + TxtName.Text + " Updated." : "Item Name " + TxtName.Text + " Already Exists.";
                if (Status == 1) Status = Checkers.ActivityNew("Inventory Item " + TxtName.Text + " Updated", int.Parse(Session["UserId"].ToString()));
            }
            else
                LtrMessage.Text = "Please Select An Item.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnInventoryId.Value != "")
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
            Status = Checkers.InventoryDelete(int.Parse(HdnInventoryId.Value));
            LtrMessage.Text = Status == 1 ? "Inventory Item " + TxtName.Text + " Deleted." : "Error Occurred";
            if (Status == 1) Status = Checkers.ActivityNew("Inventory Item " + TxtName.Text + " Deleted", int.Parse(Session["UserId"].ToString()));
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
        TxtBuyingPrice.Text = "";
        TxtBuyingPrice.Text = "0";
        TxtThreshold.Text = "0";
        TxtConversionUnit.Text = "1";
        LtrInventoryItems.Text = "";
        LtrPurchaseUnit1.Text = "Per Kg";
        LtrPurchaseUnit2.Text = "Kg(s)";
        LtrPurchaseUnit3.Text = "Kg";
        BtnNo.Visible = false;
        BtnYes.Visible = false;

        LtrInventoryItems.Text = Checkers.Inventories.Where(I => I.Inventory_Status == 1).Select(I => I).Count().ToString();
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }
    protected void DdlPurchaseUnit_SelectedIndexChanged(object sender, EventArgs e)
    {
        LtrPurchaseUnit1.Text = "Per " + DdlPurchaseUnit.SelectedItem.Text;
        LtrPurchaseUnit2.Text = DdlPurchaseUnit.SelectedItem.Text + "(s)";
        LtrPurchaseUnit3.Text = DdlPurchaseUnit.SelectedItem.Text;
        LtrName.Text = TxtName.Text;
    }
    public void FillData()
    {
        Checkers = new CheckersDataContext();
        var InventoryDetails = Checkers.Inventories.Where(M => M.Inventory_Id == int.Parse(HdnInventoryId.Value) && M.Inventory_Status == 1).Select(M => M).Single();
        TxtName.Text = InventoryDetails.Inventory_Name;
        DdlPurchaseUnit.ClearSelection();
        DdlPurchaseUnit.Items.FindByText(InventoryDetails.Inventory_PurchaseUnit.ToString()).Selected = true;
        TxtBuyingPrice.Text = InventoryDetails.Inventory_BuyingPrice.ToString();
        TxtThreshold.Text = InventoryDetails.Inventory_Threshold.ToString();
        TxtConversionUnit.Text = InventoryDetails.Inventory_ConversionUnit.ToString();
        LtrName.Text = InventoryDetails.Inventory_Name.ToString();
        LtrPurchaseUnit1.Text = "Per " + InventoryDetails.Inventory_PurchaseUnit.ToString();
        LtrPurchaseUnit2.Text = InventoryDetails.Inventory_PurchaseUnit.ToString() + "(s)";
        LtrPurchaseUnit3.Text = InventoryDetails.Inventory_PurchaseUnit.ToString();
    }
}
