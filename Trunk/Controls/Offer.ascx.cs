using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Offer : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static string Action;
    public static int MaximumQuantity = 51;
    public static int MaximumDiscount = 105;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        ClearForm();
        Action = Request.QueryString["Action"] != null ? Request.QueryString["Action"].ToString() : "Add";
        if (Request.QueryString["Name"] != null)
        {
            HdnOfferName.Value = Request.QueryString["Id"].ToString();
            FillData();
        }

        switch (Action)
        {
            case "New": BtnSearch.Visible = false; AutoCompSearch.Enabled = false; BtnSubmit.Text = "Add"; break;
            case "Edit": BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Update"; break;
            case "Delete": BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Delete"; break;
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 0;
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
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
        FillData();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            if (HdnOfferName.Value != "")
                LtrMessage.Text = "Offer " + TxtName.Text + " Inserted.";
            else
                LtrMessage.Text = "Please select an offer.";
            ClearForm();
        }
        else if (Action == "Edit")
        {
            if (HdnOfferName.Value != "")
            {
                Status = Checkers.OfferEdit(HdnOfferName.Value, TxtName.Text);
                if (Status == 1) Status = Checkers.ActivityNew("Offer " + TxtName.Text + " Updated", int.Parse(Session["UserId"].ToString()));
            }
            else
                LtrMessage.Text = "Please Select An Offer.";

            ClearForm();
        }
        else if (Action == "Delete")
        {
            if (HdnOfferName.Value != "")
            {
                LtrMessage.Text = "Offer " + TxtName.Text + " Will Be Deleted Permenantly. Do You Want To Proceed ? ";
                BtnYes.Visible = true;
                BtnNo.Visible = true;
            }
            else
            {
                LtrMessage.Text = "Please Select An Offer.";
                ClearForm();
            }
        }
    }

    protected void BtnMenuRequired_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (TxtName.Text != "")
        {
            Status = Checkers.OfferNew(TxtName.Text, int.Parse(DdlMenuRequiredName.SelectedItem.Value), int.Parse(DdlMenuRequiredQuantity.SelectedItem.Text), "Key");
            LtrMessage.Text = Status == 1 ? "Item " + DdlMenuRequiredName.Text + " Inserted." : "Error occurred.";
            FillData();
        }
        else
            LtrMessage.Text = "Please enter a offer name.";
    }

    protected void BtnMenuOffer_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (TxtName.Text != "")
        {
            Status = Checkers.OfferNew(TxtName.Text, int.Parse(DdlMenuRequiredName.SelectedItem.Value), int.Parse(DdlMenuRequiredQuantity.SelectedItem.Text), "Value");
            LtrMessage.Text = Status == 1 ? "Item " + DdlMenuRequiredName.Text + " Inserted." : "Error occurred.";
            FillData();
        }
        else
            LtrMessage.Text = "Please enter a offer name.";
    }

    public void ClearForm()
    {
        TxtName.Text = "";
        DdlMenuRequiredName.Items.Clear();
        DdlMenuRequiredQuantity.Items.Clear();
        DdlMenuOfferName.Items.Clear();
        DdlMenuOfferQuantity.Items.Clear();

        var Item = from I in Checkers.Menus
                   where I.Menu_Status.Equals(1)
                   select I;

        foreach (var I in Item)
        {
            DdlMenuRequiredName.Items.Add(new ListItem(I.Menu_Name, I.Menu_Id.ToString()));
            DdlMenuOfferName.Items.Add(new ListItem(I.Menu_Name, I.Menu_Id.ToString()));
        }

        for (int Quantity = 1; Quantity < MaximumQuantity; Quantity++)
        {
            DdlMenuRequiredQuantity.Items.Add(new ListItem(Quantity.ToString()));
            DdlMenuOfferQuantity.Items.Add(new ListItem(Quantity.ToString()));
        }

        for (int Discount = 5; Discount < MaximumDiscount; Discount += 5)
        {
            DdlMenuOfferDiscount.Items.Add(new ListItem(Discount.ToString()));
        }
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();

        if (TxtName.Text != "")
        {
            var Key = Checkers.Offers.Where(O => O.Offer_Status == 1 && O.Offer_Type == "Key").Select(O => O);
            DgMenuRequired.DataSource = Key;
            DgMenuRequired.DataBind();

            var Value = Checkers.Offers.Where(O => O.Offer_Status == 1 && O.Offer_Type == "Value").Select(O => O);
            DgMenuOffer.DataSource = Value;
            DgMenuOffer.DataBind();
        }
        else
            LtrMessage.Text = "No offer selected / entered.";
    }
}