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
            TxtName.Text = Request.QueryString["Name"].ToString();
            HdnOfferName.Value = Request.QueryString["Name"].ToString();
            FillData();
        }

        switch (Action)
        {
            case "New": BtnSearch.Visible = false; AutoCompSearch.Enabled = false; BtnSubmit.Text = "Add"; break;
            case "Edit": PnlOffer.Visible = true; PnlRequirement.Visible = true; BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Update"; break;
            case "Delete": PnlOffer.Visible = true; PnlRequirement.Visible = true; BtnSearch.Visible = true; AutoCompSearch.Enabled = true; BtnSubmit.Text = "Delete"; break;
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 1;
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.OfferDelete(HdnOfferName.Value);

        LtrMessage.Text = Status == 1 ? "Offer Successfully Deleted" : "Error Occurred.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
        ClearForm();
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
        HdnOfferName.Value = TxtName.Text;
        FillData();
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (Action == "New")
        {
            TxtName.ReadOnly = true;
            PnlOffer.Visible = true; PnlRequirement.Visible = true;
            LtrMessage.Text = "Please enter items.";
        }
        else if (Action == "Edit")
        {
            if (HdnOfferName.Value != "")
            {
                Status = Checkers.OfferEdit(HdnOfferName.Value, TxtName.Text);
                if (Status == 1) Status = Checkers.ActivityNew("Offer " + TxtName.Text + " Updated", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));
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
            }
        }
    }

    protected void BtnMenuRequired_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (TxtName.Text != "")
        {
            //if (Checkers.Offers.Where(O => O.Offer_Name == TxtName.Text && O.Offer_Status.Equals(1)).Any().Equals(true))
            //    LtrMessage.Text = "Offer name already exists.";
            //else
            //{
            Status = Checkers.OfferNew(TxtName.Text, int.Parse(DdlMenuRequiredName.SelectedItem.Value), int.Parse(DdlMenuRequiredQuantity.SelectedItem.Text), 0, "Key", DateTime.Parse(Application["SalesSession"].ToString()));
            LtrMessage.Text = Status == 1 ? "Item " + DdlMenuRequiredName.Text + " Inserted." : "Error occurred.";
            FillData();
            //}
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
            //if (Checkers.Offers.Where(O => O.Offer_Name == TxtName.Text && O.Offer_Status.Equals(1)).Any().Equals(true))
            //    LtrMessage.Text = "Offer name already exists.";
            //else
            //{
            Status = Checkers.OfferNew(TxtName.Text, int.Parse(DdlMenuOfferName.SelectedItem.Value), int.Parse(DdlMenuOfferQuantity.SelectedItem.Text), int.Parse(DdlMenuOfferDiscount.SelectedItem.Text), "Value", DateTime.Parse(Application["SalesSession"].ToString()));
            LtrMessage.Text = Status == 1 ? "Item " + DdlMenuRequiredName.Text + " Inserted." : "Error occurred.";
            FillData();
            //}
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
        DdlMenuOfferDiscount.Items.Clear();

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

        DgMenuOffer.DataSource = null;
        DgMenuRequired.DataSource = null;
        DgMenuRequired.DataBind();
        DgMenuOffer.DataBind();
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();

        if (TxtName.Text != "")
        {
            var Key = from C in Checkers.Offers
                      from M in Checkers.Menus
                      where C.Offer_Status == 1 && C.Offer_Type == "Key" && C.Offer_Name == TxtName.Text && C.Offer_Menu == M.Menu_Id
                      select new { C.Offer_Id, C.Offer_Cost, C.Offer_Name, C.Offer_Quantity, C.Offer_Type, M.Menu_Name };
            DgMenuRequired.DataSource = Enumerable.Count(Key) > 0 ? Key : null;
            DgMenuRequired.DataBind();

            var Value = from C in Checkers.Offers
                        from M in Checkers.Menus
                        where C.Offer_Status == 1 && C.Offer_Type == "Value" && C.Offer_Name == TxtName.Text && C.Offer_Menu == M.Menu_Id
                        select new { C.Offer_Id, C.Offer_Cost, C.Offer_Name, C.Offer_Quantity, C.Offer_Type, M.Menu_Name };
            DgMenuOffer.DataSource = Enumerable.Count(Value) > 0 ? Value : null;
            DgMenuOffer.DataBind();
        }
        else
            LtrMessage.Text = "No offer selected / entered.";
    }
    protected void DgMenu_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();

        int Status = Checkers.OfferItemDelete(int.Parse(e.Item.Cells[0].Text));
        FillData();
    }
}