using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_OfferList : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        var Offer = Checkers.Offers.Where(O => O.Offer_Status == 1 && O.Offer_Type == "Key").Select(O => O);
        if (Offer.Count() > 0)
        {
            DgOffer.Visible = true;
            DgOffer.DataSource = Offer;
            DgOffer.DataBind();
        }
        else
        {
            DgOffer.Visible = false;
            LtrMessage.Text = "No offers found.";
        }
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 1;
    }
    protected void DgOffer_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        HdnOfferName.Value = e.Item.Cells[0].Text;
        LtrMessage.Text = "Are you sure, you want to delete item " + e.Item.Cells[0].Text + " ?";
        BtnYes.Visible = true;
        BtnNo.Visible = true;
    }
    protected void DgOffer_EditCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        Response.Redirect("Operation.aspx?Section=Offer&Action=Edit&Name=" + e.Item.Cells[0].Text);
    }
    protected void DgOffer_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        DgOffer.CurrentPageIndex = e.NewPageIndex;
        DgOffer.DataBind();
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.OfferDelete(HdnOfferName.Value);
        LtrMessage.Text = Status == 1 ? "Offer item deleted successfully." : "Error occurred.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
        var Offer = Checkers.Offers.Where(O => O.Offer_Status == 1).Select(O => O);
        if (Offer.Count() > 0)
        {
            DgOffer.Visible = true;
            DgOffer.DataSource = Offer;
            DgOffer.CurrentPageIndex = 0;
            DgOffer.DataBind();
        }
        else
        {
            DgOffer.Visible = false;
            LtrMessage.Text = "No Offer items found.";
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
        var Offer = Checkers.Offers.Where(O => O.Offer_Status == 1 && O.Offer_Name.Equals(TxtName.Text)).Select(O => O);
        if (Offer.Count() > 0)
        {
            DgOffer.Visible = true;
            DgOffer.DataSource = Offer;
            DgOffer.DataBind();
        }
        else
        {
            DgOffer.Visible = false;
            LtrMessage.Text = "No menu items found.";
        }
    }
}
