using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Asset : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Action;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ClearForm();
            Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
            if (Request.QueryString["Id"] != null)
            {
                HdnAssetId.Value = Request.QueryString["Id"].ToString();
                FillData();
            }

            switch (Action)
            {
                case "New": AutoCompSearch.Enabled = false; BtnSubmit.Text = "Add"; break;
                case "Edit": AutoCompSearch.Enabled = true; BtnSubmit.Text = "Update"; break;
                case "Delete": AutoCompSearch.Enabled = true; BtnSubmit.Text = "Delete"; break;
            }
            ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 0;
        }
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;
        if (Action == "Delete")
        {
            Status = Checkers.AssetDelete(int.Parse(HdnAssetId.Value));
            LtrMessage.Text = Status == 1 ? "Asset " + TxtName.Text + " Deleted." : "Error Occurred";
            if (Status == 1) Status = Checkers.ActivityNew("Asset " + TxtName.Text + " Deleted", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
        }
        ClearForm();
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action Cancelled By The User.";
        ClearForm();
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            Status = Checkers.AssetNew(TxtName.Text, int.Parse(TxtQuantity.Text), DateTime.Parse(TxtPurchaseDate.Text), DateTime.Parse(Application["SalesSession"].ToString()));
            LtrMessage.Text = Status == 0 ? "Asset " + TxtName.Text + " Already Exists." : "Asset " + TxtName.Text + " Added.";
            if (Status == 1) Status = Checkers.ActivityNew("Asset " + TxtName.Text + " Added", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

            ClearForm();
        }
        else if (Action == "Edit")
        {
            if (HdnAssetId.Value != "")
            {
                Status = Checkers.AssetEdit(int.Parse(HdnAssetId.Value), TxtName.Text, int.Parse(TxtQuantity.Text), DateTime.Parse(TxtPurchaseDate.Text), DateTime.Parse(Application["SalesSession"].ToString()));
                LtrMessage.Text = Status == 1 ? "Asset " + TxtName.Text + " Updated." : "Asset " + TxtName.Text + " Already Exists.";
                if (Status == 1) Status = Checkers.ActivityNew("Asset " + TxtName.Text + " Updated", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
            }
            else
                LtrMessage.Text = "Please Select An Asset.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnAssetId.Value != "")
            {
                LtrMessage.Text = "Asset " + TxtName.Text + " Will Be Deleted Permenantly. Do You Want To Proceed ? ";
                BtnYes.Visible = true;
                BtnNo.Visible = true;
            }
            else
            {
                LtrMessage.Text = "Please Select An Asset.";
                ClearForm();
            }
        }
    }
    private void ClearForm()
    {
        Checkers = new CheckersDataContext();

        TxtName.Text = "";
        TxtQuantity.Text = "";
        TxtPurchaseDate.Text = "";
        BtnNo.Visible = false;
        BtnYes.Visible = false;
    }
    public void FillData()
    {
        Checkers = new CheckersDataContext();
        var AssetDetails = Checkers.Assets.Where(A => A.Asset_Id == int.Parse(HdnAssetId.Value) && A.Asset_Status == 1).Select(S => S).Single();
        TxtName.Text = AssetDetails.Asset_Name;
        TxtQuantity.Text = AssetDetails.Asset_Quantity.ToString();
        TxtPurchaseDate.Text = AssetDetails.Asset_PurchaseDate.ToString().Substring(0, 10).Replace("-", "/");
    }
}
