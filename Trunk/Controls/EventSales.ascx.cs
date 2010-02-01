using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_EventSales : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
    }
    protected void DgContent_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        if (HdnEventId.Value != "")
        {
            Status = Checkers.SalesNew(0, int.Parse(DdlItem.SelectedItem.Value), decimal.Parse(DdlQuantity.SelectedItem.Text), int.Parse(HdnEventId.Value), "Event", 0, DateTime.Parse(Application["SalesSession"].ToString()));
            LtrMessage.Text = Status == 1 ? "Item " + DdlItem.SelectedItem.Text + " Of Quantity " + DdlQuantity.SelectedItem.Text + " Ordered For Event No. " + HdnEventId.Value : "Error Occurred";

            var MenuConverter = Checkers.Converters.Where(C => C.Converter_Menu == int.Parse(DdlItem.SelectedItem.Value) && C.Converter_Status == 1).Select(C => C);
            foreach (var M in MenuConverter)
            {
                decimal Quantity = M.Converter_InventoryQuantity.Value * decimal.Parse(DdlQuantity.SelectedItem.Text);
                Status = Checkers.InventorySubtract(M.Converter_Inventory, Quantity);
            }

            if (Checkers.Inventories.Where(I => I.Inventory_Status == 1 && I.Inventory_Quantity.Value <= I.Inventory_Threshold.Value).Any().Equals(true))
            {
                var Inventory = Checkers.Inventories.Where(I => I.Inventory_Status == 1 && I.Inventory_Quantity.Value <= I.Inventory_Threshold.Value).Select(I => I);
                if (Inventory.Count() > 0)
                    LtrMessage.Text = "Please Check The Inventory Levels.";
            }

            //var MenuType = Checkers.Menus.Where(M => M.Menu_Id == int.Parse(DdlItem.SelectedItem.Value)).Select(M => M.Menu_Category).Single();

            //Status = Checkers.TokenNew(MenuType.ToString() == "Bar" ? "BOT" : "KOT", int.Parse(DdlItem.SelectedItem.Value), decimal.Parse(DdlQuantity.SelectedItem.Text), int.Parse(TableSource().ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

            //LblOt.Text = MenuType.ToString() == "Bar" ? "BOT" : "KOT";

            //Ot.InnerHtml = "<div style=\"width: 396px; font-size: 7px; font-type: Arial\"><br />";
            //Ot.InnerHtml += DateTime.Now.ToString();
            //Ot.InnerHtml += "<br /><br /><strong>Table No: </strong>" + HdnEventId.Value + "<br /><br />";
            //Ot.InnerHtml += "<hr>";
            //Ot.InnerHtml += "<strong>Item: </strong>" + DdlItem.SelectedItem.Text + "<br />";
            //Ot.InnerHtml += "<strong>Quantity: </strong>" + decimal.Parse(DdlQuantity.SelectedItem.Text) + "</div>";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);

            FillData();
        }
        else
            LtrMessage.Text = "Please Select An Table.";
    }

    public void ClearForm()
    {
        DgContent.DataSource = null;
        DgContent.DataBind();
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();

        DdlItem.Items.Clear();
        DdlQuantity.Items.Clear();

        var Item = from I in Checkers.Menus
                   where I.Menu_Status.Equals(1)
                   select I;
        foreach (var I in Item)
            DdlItem.Items.Add(new ListItem(I.Menu_Name, I.Menu_Id.ToString()));

        for (int Quantity = 1; Quantity < 51; Quantity++)
            DdlQuantity.Items.Add(new ListItem(Quantity.ToString()));

        HdnEventSource.Value = Checkers.Sources.Where(S => S.Source_Type == "Event" && S.Source_Number.Value == int.Parse(HdnEventId.Value) && S.Source_Status == 1).Select(S => S).ToString();

        var EventSales = Checkers.Sales.Where(S => S.Sales_Source == int.Parse(HdnEventSource.Value)).Select(S => S);
        DgContent.DataSource = EventSales;
        DgContent.DataBind();
    }
}
