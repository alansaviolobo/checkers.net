using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class Order : System.Web.UI.Page
{
    #region Parameters

    public CheckersDataContext Checkers;
    public static int NumberOfTables = 40;
    public static int MaximumQuantity = 51;

    #endregion

    #region Form Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Application["UserId"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            Checkers = new CheckersDataContext();

            if (!Page.IsPostBack)
            {
                DdlItem.Items.Clear();
                DdlQuantity.Items.Clear();
                DdlSteward.Items.Clear();
                DdlOffer.Items.Clear();

                var Item = from I in Checkers.Menus
                           where I.Menu_Status.Equals(1)
                           select I;
                foreach (var I in Item)
                    DdlItem.Items.Add(new ListItem(I.Menu_Name, I.Menu_Id.ToString()));

                for (int Quantity = 1; Quantity < MaximumQuantity; Quantity++)
                    DdlQuantity.Items.Add(new ListItem(Quantity.ToString()));

                var Steward = from S in Checkers.Contacts
                              where S.Contact_Type == "Steward" && S.Contact_Status.Equals(1)
                              select S;

                foreach (var S in Steward)
                    DdlSteward.Items.Add(new ListItem(S.Contact_Name, S.Contact_Id.ToString()));

                var Offer = (from O in Checkers.Offers
                             where O.Offer_Status == 1 && O.Offer_Type == "Key"
                             select O).Distinct();

                foreach (var O in Offer)
                    DdlOffer.Items.Add(new ListItem(O.Offer_Name));

                if (Checkers.Contacts.Where(C => C.Contact_Id.Equals(int.Parse(Application["UserId"].ToString()))).Select(C => C.Contact_Type).Single().Equals("Administrator"))
                    BtnAdministration.Visible = true;
            }
            FillTables();
        }
    }

    public void BtnTable_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ImgBtnTable = (ImageButton)sender;

        HdnTableId.Value = ImgBtnTable.ID;
        HdnTableSource.Value = TableSource();

        BillNumber();
        ClearForm();

        LtrBillTableNumber.Text = HdnTableId.Value;
        LtrItemTableNumber.Text = HdnTableId.Value;

        if (ImgBtnTable.ToolTip == "Table Empty")
        {
            BtnBill.Text = "Print Bill";
            TxtDiscount.ReadOnly = false;
            TxtDiscountBar.ReadOnly = false;
            TxtDiscountRestaurant.ReadOnly = false;
            TxtNoOfPeople.ReadOnly = false;
            DdlSteward.Enabled = true;
        }
        else if (ImgBtnTable.ToolTip == "Table Busy")
        {
            BtnBill.Text = "Print Bill";
            TxtDiscount.ReadOnly = false;
            TxtDiscountBar.ReadOnly = false;
            TxtDiscountRestaurant.ReadOnly = false;
            TxtNoOfPeople.ReadOnly = false;
            DdlSteward.Enabled = true;
        }
        else if (ImgBtnTable.ToolTip == "Table Billed")
        {
            BtnBill.Text = "RePrint Bill";
            TxtDiscount.ReadOnly = true;
            TxtDiscountBar.ReadOnly = true;
            TxtDiscountRestaurant.ReadOnly = true;
            TxtNoOfPeople.ReadOnly = true;
            PnlButton.Visible = true;
            DdlSteward.Enabled = false;
        }

        ShowOrders();
        OrderInformation();
        FillOt();
    }

    protected void BtnOrderItem_Click(object sender, EventArgs e)
    {
        if (HdnTableId.Value != "")
        {
            Checkers = new CheckersDataContext();
            int Status;

            if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Status == 2).Any().Equals(true))
                LtrMessage.Text = "Table " + HdnTableId.Value + " Already Billed. Please Close The Bill.";
            else
            {
                Status = Checkers.SalesNew(0, int.Parse(DdlItem.SelectedItem.Value), decimal.Parse(DdlQuantity.SelectedItem.Text), int.Parse(HdnTableId.Value), "Table", 0, DateTime.Parse(Application["SalesSession"].ToString()));
                LtrMessage.Text = Status == 1 ? "Item " + DdlItem.SelectedItem.Text + " Of Quantity " + DdlQuantity.SelectedItem.Text + " Ordered For Table No. " + HdnTableId.Value : "Error Occurred";

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

                var MenuToken = Checkers.Menus.Where(M => M.Menu_Id == int.Parse(DdlItem.SelectedItem.Value)).Select(M => M.Menu_TokenSection).Single();

                Status = Checkers.TokenNew(MenuToken.ToString() + " Token", int.Parse(DdlItem.SelectedItem.Value), decimal.Parse(DdlQuantity.SelectedItem.Text), int.Parse(TableSource().ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

                //LblOt.Text = MenuToken.ToString() + " Token";

                //Ot.InnerHtml = "<div style=\"width: 396px; font-size: 7px; font-type: Arial\"><br />";
                //Ot.InnerHtml += DateTime.Now.ToString();
                //Ot.InnerHtml += "<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />";
                //Ot.InnerHtml += "<hr>";
                //Ot.InnerHtml += "<strong>Item: </strong>" + DdlItem.SelectedItem.Text + "<br />";
                //Ot.InnerHtml += "<strong>Quantity: </strong>" + decimal.Parse(DdlQuantity.SelectedItem.Text) + "</div>";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);

                ShowOrders();
                OrderInformation();
                
                DdlItem.SelectedIndex = 0;
                DdlQuantity.SelectedIndex = 0;
            }
        }
        else
            LtrMessage.Text = "Please Select An Table.";

        FillTables();
    }

    protected void BtnOrderOffer_Click(object sender, EventArgs e)
    {
        if (HdnTableId.Value != "")
        {
            Checkers = new CheckersDataContext();
            int Status;

            if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Status == 2).Any().Equals(true))
                LtrMessage.Text = "Table " + HdnTableId.Value + " Already Billed. Please Close The Bill.";
            else
            {
                var OfferItems = Checkers.Offers.Where(O => O.Offer_Status == 1 && O.Offer_Name == DdlOffer.SelectedItem.Text).Select(O => O);
                foreach (var O in OfferItems)
                {
                    Status = Checkers.SalesOffer(0, O.Offer_Menu, 1, int.Parse(HdnTableId.Value), "Table", O.Offer_Cost, 0, DateTime.Parse(Application["SalesSession"].ToString()));
                    LtrMessage.Text = Status == 1 ? "Offer " + DdlOffer.Text + " Ordered Successfully." : "Error Occurred";

                    var MenuConverter = Checkers.Converters.Where(C => C.Converter_Menu == O.Offer_Menu && C.Converter_Status == 1).Select(C => C);
                    foreach (var M in MenuConverter)
                    {
                        Status = Checkers.InventorySubtract(M.Converter_Inventory, M.Converter_InventoryQuantity.Value);
                    }
                    var MenuToken = Checkers.Menus.Where(M => M.Menu_Id == O.Offer_Menu).Select(M => M.Menu_TokenSection).Single();

                    string TokenTitle = MenuToken.ToString() + " Token";

                    Status = Checkers.TokenNew(TokenTitle, O.Offer_Menu, O.Offer_Quantity, int.Parse(TableSource().ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

                }
                //LblOt.Text = MenuCategory;

                //Ot.InnerHtml = "<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />";
                //Ot.InnerHtml += "<hr>";
                //Ot.InnerHtml += "<strong>Item: </strong>" + DdlItem.SelectedItem.Text + "<br />";
                //Ot.InnerHtml += "<strong>Quantity: </strong>" + decimal.Parse(DdlQuantity.SelectedItem.Text);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);

                ShowOrders();
                OrderInformation();

                DdlItem.SelectedIndex = 0;
                DdlQuantity.SelectedIndex = 0;
            }
        }
        else
            LtrMessage.Text = "Please Select An Table.";

        FillTables();
    }

    protected void BtnOrderOpenMenu_Click(object sender, EventArgs e)
    {
        if (HdnTableId.Value != "")
        {
            Checkers = new CheckersDataContext();
            int Status, MenuId;

            MenuId = Checkers.MenuNew(0, TxtOpenMenuName.Text, "Open Menu", DdlTokenSection.SelectedItem.Text, decimal.Parse(TxtOpenMenuCost.Text), DateTime.Parse(Application["SalesSession"].ToString()));

            if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Status == 2).Any().Equals(true))
                LtrMessage.Text = "Table " + HdnTableId.Value + " Already Billed. Please Close The Bill.";
            else
            {
                Status = Checkers.SalesNew(0, MenuId, decimal.Parse(TxtOpenMenuQuantity.Text), int.Parse(HdnTableId.Value), "Table", 0, DateTime.Parse(Application["SalesSession"].ToString()));
                LtrMessage.Text = Status == 1 ? "Item " + TxtOpenMenuName.Text + " Of Quantity " + TxtOpenMenuQuantity.Text + " Ordered For Table No. " + HdnTableId.Value : "Error Occurred";

                var MenuToken = Checkers.Menus.Where(M => M.Menu_Id == MenuId).Select(M => M.Menu_TokenSection).Single();

                Status = Checkers.TokenNew(MenuToken + " Token", MenuId, decimal.Parse(TxtOpenMenuQuantity.Text), int.Parse(TableSource().ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

                //LblOt.Text = MenuToken + " Token";

                //Ot.InnerHtml = "<div style=\"width: 396px; font-size: 7px; font-type: Arial\"><br />";
                //Ot.InnerHtml += DateTime.Now.ToString();
                //Ot.InnerHtml += "<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />";
                //Ot.InnerHtml += "<hr>";
                //Ot.InnerHtml += "<strong>Item: </strong>" + TxtOpenMenuName.Text + "<br />";
                //Ot.InnerHtml += "<strong>Quantity: </strong>" + decimal.Parse(TxtOpenMenuQuantity.Text) + "</div>";
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);

                ShowOrders();
                OrderInformation();

                DdlItem.SelectedIndex = 0;
                DdlQuantity.SelectedIndex = 0;
            }

            Status = Checkers.MenuDelete(MenuId);
        }
        else
            LtrMessage.Text = "Please Select An Table.";

        FillTables();
    }

    protected void DgOrderItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;
        int SalesId = int.Parse(e.Item.Cells[0].Text); ;
        int Menu = (from S in Checkers.Sales where S.Sales_Id == SalesId select S.Sales_Menu.Value).Single();

        Status = Checkers.SalesDelete(SalesId, Menu, decimal.Parse(e.Item.Cells[2].Text), int.Parse(HdnTableId.Value), "Table", DateTime.Parse(Application["SalesSession"].ToString()));
        LtrMessage.Text = Status == 1 ? "Item " + e.Item.Cells[1].Text + " For Table No. " + HdnTableId.Value + " Deleted" : "Error Occurred";

        var MenuConverter = Checkers.Converters.Where(C => C.Converter_Menu == Menu && C.Converter_Status == 1).Select(C => C);
        foreach (var M in MenuConverter)
        {
            decimal Quantity = M.Converter_InventoryQuantity.Value * decimal.Parse(e.Item.Cells[2].Text);
            Status = Checkers.InventoryAdd(M.Converter_Inventory, Quantity);
        }

        FillOt();
        ShowOrders();
        OrderInformation();
    }

    protected void BtnBill_Click(object sender, EventArgs e)
    {
        if (HdnTableId.Value == "")
            LtrMessage.Text = "Please Select A Table.";
        else
        {
            if (LblTotalCost.Text == "")
                LtrMessage.Text = "Bill Cannot Be Printed. There are No Orders To This Table.";
            else
            {
                Checkers = new CheckersDataContext();
                int Status;
                decimal SubTotal = decimal.Parse(LblTotalCost.Text);
                decimal Discount = (decimal.Parse(LblTotalCost.Text) * decimal.Parse(TxtDiscount.Text)) / 100;

                if (BtnBill.Text == "Print Bill")
                {
                    Status = Checkers.InvoiceNew(0, SubTotal, Discount, 0, 0, 0, 0, int.Parse(TableSource()), null, int.Parse(DdlSteward.SelectedItem.Value), int.Parse(TxtNoOfPeople.Text), DateTime.Parse(Application["SalesSession"].ToString()));
                    //Status = 1;
                    LtrMessage.Text = Status == 1 ? "Bill Printed For Table No. " + HdnTableId.Value + "." : "Error Occurred.";
                }
                else
                    LtrMessage.Text = "Bill Re Printed For Table No. " + HdnTableId.Value;

                LtrDate.Text = DateTime.Now.ToShortDateString();
                LtrTime.Text = DateTime.Now.ToShortTimeString();
                LtrTableNo.Text = HdnTableId.Value;
                LtrSteward.Text = DdlSteward.SelectedItem.Text;

                var OrderList = from O in Checkers.Sales
                                from M in Checkers.Menus
                                where O.Sales_Source == (from S in Checkers.Sources
                                                         where S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Status == 2
                                                         select S.Source_Id).Single() && O.Sales_Status == 1
                                                         && O.Sales_Menu == M.Menu_Id
                                select new { O.Sales_Id, M.Menu_Name, M.Menu_Category, O.Sales_Quantity, O.Sales_Cost };

                BillNumber();

                LtrBillNo.Text = HdnBillNumber.Value;

                decimal AmountBar = 0;
                decimal AmountRestaurant = 0;

                foreach (var O in OrderList)
                {
                    HtmlTableRow TblBillRow = new HtmlTableRow();
                    HtmlTableCell TblBillCellItem = new HtmlTableCell();
                    HtmlTableCell TblBillCellQnty = new HtmlTableCell();
                    HtmlTableCell TblBillCellPerUnit = new HtmlTableCell();
                    HtmlTableCell TblBillCellCost = new HtmlTableCell();
                    TblBillCellItem.InnerText = O.Menu_Name;
                    TblBillCellQnty.InnerText = O.Sales_Quantity.ToString();
                    TblBillCellPerUnit.InnerText = (O.Sales_Cost / O.Sales_Quantity).ToString();
                    TblBillCellCost.InnerText = O.Sales_Cost.ToString();
                    TblBillRow.Cells.Add(TblBillCellItem);
                    TblBillRow.Cells.Add(TblBillCellQnty);
                    TblBillRow.Cells.Add(TblBillCellPerUnit);
                    TblBillRow.Cells.Add(TblBillCellCost);
                    TblBill.Rows.Add(TblBillRow);

                    if (O.Menu_Category == "Restaurant") AmountRestaurant += O.Sales_Cost.Value;
                    else AmountBar += O.Sales_Cost.Value;
                }

                decimal DiscountBar = (AmountBar * decimal.Parse(TxtDiscountBar.Text)) / 100;
                decimal DiscountRestaurant = (AmountRestaurant * decimal.Parse(TxtDiscountRestaurant.Text)) / 100;
                decimal TotalBar = AmountBar - DiscountBar;
                decimal TotalRestaurant = AmountRestaurant - DiscountRestaurant;
                SubTotal = TotalBar + TotalRestaurant;
                decimal Total = Math.Round(SubTotal - Discount);

                if (BtnBill.Text == "Print Bill")
                {
                    Status = Checkers.InvoiceEdit(int.Parse(HdnBillNumber.Value), AmountBar, AmountRestaurant, DiscountBar, DiscountRestaurant);
                    BtnBill.Text = "RePrint Bill";
                }

                HtmlTableRow TblBillFooter = new HtmlTableRow();
                HtmlTableCell TblBillFooterCell1 = new HtmlTableCell();
                HtmlTableCell TblBillFooterCellKey = new HtmlTableCell();
                HtmlTableCell TblBillFooterCellValue = new HtmlTableCell();
                TblBillFooterCell1.ColSpan = 3;
                TblBillFooterCell1.InnerHtml = "&nbsp;";
                TblBillFooter.Cells.Add(TblBillFooterCell1);
                TblBill.Rows.Add(TblBillFooter);

                TblBillFooter = new HtmlTableRow();
                TblBillFooterCell1 = new HtmlTableCell();

                TblBillFooterCell1.InnerHtml = "<div style=\"width: 50%; float: left; text-align: center\">Cashier</div><div style=\"width: 50%; float: left; text-align: center\">Customer</div>";
                TblBillFooterCell1.VAlign = "bottom";
                TblBillFooterCellKey.InnerHtml = "<strong>S.Total<br />Discount(" + decimal.Parse(TxtDiscount.Text) + ")<br />Total</strong>";
                TblBillFooterCellValue.InnerHtml = "Rs." + Math.Round(SubTotal, 2) + "<br />";
                TblBillFooterCellValue.InnerHtml += "Rs." + Math.Round(Discount, 2) + "<br />";
                TblBillFooterCellValue.InnerHtml += "Rs." + Math.Round(Total, 2) + "/-";

                TblBillFooter.Cells.Add(TblBillFooterCell1);
                TblBillFooter.Cells.Add(TblBillFooterCellKey);
                TblBillFooter.Cells.Add(TblBillFooterCellValue);
                TblBill.Rows.Add(TblBillFooter);

                FillTables();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintBill');", true);
            }
        }
    }

    protected void BtnAdministration_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Operation.aspx");
    }

    protected void BtnCancelBill_Click(object sender, EventArgs e)
    {
        TxtReason.Visible = true;
        BtnOk.Visible = true;
        LtrMessage.Text = "Please Record The Reason For Bill Cancellation.";
    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.InvoiceDelete(int.Parse(HdnBillNumber.Value), int.Parse(HdnTableSource.Value), TxtReason.Text);
        LtrMessage.Text = Status == 1 ? "Bill " + HdnBillNumber.Value + " Cancelled." : "Error Occurred.";

        TxtReason.Visible = false;
        BtnOk.Visible = false;

        FillTables();
    }

    protected void BtnCash_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.InvoiceClose(int.Parse(HdnBillNumber.Value), int.Parse(HdnTableSource.Value), "Cash");
        LtrMessage.Text = Status == 1 ? "Bill " + HdnBillNumber.Value + " Closed." : "Error Occurred.";

        FillTables();
        ClearForm();
    }

    protected void BtnCheque_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.InvoiceClose(int.Parse(HdnBillNumber.Value), int.Parse(HdnTableSource.Value), "Cheque");
        LtrMessage.Text = Status == 1 ? "Bill " + HdnBillNumber.Value + " Closed." : "Error Occurred.";

        FillTables();
        ClearForm();
    }

    protected void BtnCreditCard_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.InvoiceClose(int.Parse(HdnBillNumber.Value), int.Parse(HdnTableSource.Value), "Credit Card");
        LtrMessage.Text = Status == 1 ? "Bill " + HdnBillNumber.Value + " Closed." : "Error Occurred.";

        FillTables();
        ClearForm();
    }

    protected void BtnPrintOt_Click(object sender, EventArgs e)
    {
        //Checkers = new CheckersDataContext();

        //if (DdlToken.Items.Count > 0)
        //{
        //    var Token = Checkers.Tokens.Where(T => T.Token_Id == int.Parse(DdlToken.SelectedItem.Value) && T.Token_Status == 1).Select(T => T).Single();

        //    var MenuType = Checkers.Menus.Where(M => M.Menu_Id == Token.Token_Menu).Select(M => M.Menu_Category);

        //    LblOt.Text = MenuType.ToString() == "Bar" ? "BOT" : "KOT";

        //    Ot.InnerHtml = "<div style=\"width: 396px; font-size: 7px; font-type: Arial\"><br />";
        //    Ot.InnerHtml += DateTime.Now.ToString();
        //    Ot.InnerHtml += "<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />";
        //    Ot.InnerHtml += "<hr>";
        //    Ot.InnerHtml += "<strong>Item: </strong>" + Checkers.Menus.Where(M => M.Menu_Id == Token.Token_Menu).Select(M => M.Menu_Name).Single() + "<br />";
        //    Ot.InnerHtml += "<strong>Quantity: </strong>" + Token.Token_Quantity.ToString();
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);
        //}
        //else
        //{
        //    LtrMessage.Text = "No Items For Table No. " + HdnTableId.Value + ".";
        //}
    }

    protected void BtnZeroBilling_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.InvoiceCloseZeroBilling(int.Parse(HdnBillNumber.Value), int.Parse(HdnTableSource.Value));
        LtrMessage.Text = Status == 1 ? "Bill " + HdnBillNumber.Value + " Closed." : "Error Occurred.";

        FillTables();
        ClearForm();
    }

    protected void BtnCreditOk_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;
        if (HdnClientId.Value != "")
        {
            Status = Checkers.InvoiceCloseCredit(int.Parse(HdnBillNumber.Value), int.Parse(HdnClientId.Value), int.Parse(HdnTableSource.Value));
            LtrMessage.Text = Status == 1 ? "Bill " + HdnBillNumber.Value + " Closed." : "Error Occurred.";

            FillTables();
            ClearForm();

            LtrClient.Visible = false;
            TxtClient.Visible = false;
            BtnCreditOk.Visible = false;
        }
        else
            LtrMessage.Text = "No Contact Found With The Name " + TxtClient.Text;
    }

    protected void BtnCredit_Click(object sender, EventArgs e)
    {
        LtrClient.Visible = true;
        TxtClient.Visible = true;
        BtnCreditOk.Visible = true;
    }

    protected void DgToken_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        LtrMessage.Text = "Please Enter A Reason For Token Cancellation.";
        LtrTokenCancel.Visible = true;
        BtnTokenCancel.Visible = true;
        TxtTokenCancel.Visible = true;
        BtnTokenReset.Visible = true;
        Context.Items["TokenId"] = e.Item.Cells[1].Text;
    }

    protected void BtnTokenCancel_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = 0;
        int i;

        if (Context.Items["TokenId"] != null)
        {
            Status = Checkers.TokenDelete(int.Parse(Context.Items["TokenId"].ToString()), TxtTokenCancel.Text);
            LtrMessage.Text = Status == 1 ? "Token " + Context.Items["TokenId"].ToString() + " Canceled" : "Error Occurred.";
            Session.Clear();
        }
        else
        {
            for (i = 0; i < DgToken.Items.Count; i++)
            {
                if (((System.Web.UI.WebControls.CheckBox)DgToken.Items[i].Cells[0].FindControl("ChkBox")).Checked == true)
                {
                    Status = Checkers.TokenDelete(int.Parse(DgToken.Items[i].Cells[1].Text), TxtTokenCancel.Text);
                }
            }
            LtrMessage.Text = Status == 1 ? "Tokens Canceled" : "Error Occurred.";
        }
        LtrTokenCancel.Visible = false;
        TxtTokenCancel.Visible = false;
        BtnTokenCancel.Visible = false;
        BtnTokenReset.Visible = false;
        FillOt();
    }

    protected void BtnTokenReset_Click(object sender, EventArgs e)
    {
        LtrTokenCancel.Visible = false;
        TxtTokenCancel.Visible = false;
        BtnTokenCancel.Visible = false;
        BtnTokenReset.Visible = false;
        Context.Items.Clear();
    }

    protected void BtnTransfer_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status = Checkers.TransferTable(int.Parse(LtrBillTableNumber.Text), int.Parse(DdlTransfer.SelectedItem.Text));
        LtrMessage.Text = Status == 1 ? "Table No. " + LtrBillTableNumber.Text + " Transfered To Table No. " + DdlTransfer.SelectedItem.Text : "Error Occurred.";

        FillTables();
    }

    protected void BtnPrintSelectedToken_Click(object sender, EventArgs e)
    {
        StringBuilder Bar = new StringBuilder();
        StringBuilder Barbeque = new StringBuilder();
        StringBuilder Tandoor = new StringBuilder();
        StringBuilder Restaurant = new StringBuilder();
        StringBuilder PrintData = new StringBuilder();

        for (int i = 0; i < DgToken.Items.Count; i++)
        {
            if (((System.Web.UI.WebControls.CheckBox)DgToken.Items[i].Cells[0].FindControl("ChkBox")).Checked == true)
            {
                switch (DgToken.Items[i].Cells[2].Text)
                {
                    case "Barbeque Token":
                        Barbeque.Append(DgToken.Items[i].Cells[3].Text + "&nbsp;&nbsp;(" + DgToken.Items[i].Cells[4].Text + ")<br />");
                        break;
                    case "Bar Token":
                        Bar.Append(DgToken.Items[i].Cells[3].Text + "&nbsp;&nbsp;(" + DgToken.Items[i].Cells[4].Text + ")<br />");
                        break;
                    case "Restaurant Token":
                        Restaurant.Append(DgToken.Items[i].Cells[3].Text + "&nbsp;&nbsp;(" + DgToken.Items[i].Cells[4].Text + ")<br />");
                        break;
                    case "Tandoor Token":
                        Tandoor.Append(DgToken.Items[i].Cells[3].Text + "&nbsp;&nbsp;(" + DgToken.Items[i].Cells[4].Text + ")<br />");
                        break;
                }
                ((System.Web.UI.WebControls.CheckBox)DgToken.Items[i].Cells[0].FindControl("ChkBox")).Checked = false;
            }
        }

        if (Barbeque.Length > 0)
        {
            PrintData.Append("<br /><br /><br /><br /><div class=\"OtTitle\">Barbeque Token</div>");
            PrintData.Append("<div style=\"width: 396px; font-size: 9px; font-type: Arial\"><br />");
            PrintData.Append(DateTime.Now.ToString());
            PrintData.Append("<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />");
            PrintData.Append("<hr>");
            PrintData.Append(Barbeque);
            if (TxtComments.Text != "") Ot.InnerHtml += "<strong>Comments: </strong><b />" + TxtComments.Text;
            PrintData.Append("</div>");
            Ot.InnerHtml = PrintData.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);
        }

        if (Bar.Length > 0)
        {
            PrintData.Append("<br /><br /><br /><br /><div class=\"OtTitle\">Bar Token</div>");
            PrintData.Append("<div style=\"width: 396px; font-size: 9px; font-type: Arial\"><br />");
            PrintData.Append(DateTime.Now.ToString());
            PrintData.Append("<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />");
            PrintData.Append("<hr>");
            PrintData.Append(Bar);
            if (TxtComments.Text != "") Ot.InnerHtml += "<strong>Comments: </strong><b />" + TxtComments.Text;
            PrintData.Append("</div>");
            Ot.InnerHtml = PrintData.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);
        }

        if (Tandoor.Length > 0)
        {
            PrintData.Append("<br /><br /><br /><br /><div class=\"OtTitle\">Tandoor Token</div>");
            PrintData.Append("<div style=\"width: 396px; font-size: 9px; font-type: Arial\"><br />");
            PrintData.Append(DateTime.Now.ToString());
            PrintData.Append("<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />");
            PrintData.Append("<hr>");
            PrintData.Append(Tandoor);
            if (TxtComments.Text != "") Ot.InnerHtml += "<strong>Comments: </strong><b />" + TxtComments.Text;
            PrintData.Append("</div>");
            Ot.InnerHtml = PrintData.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);
        }

        if (Restaurant.Length > 0)
        {
            PrintData.Append("<br /><br /><br /><br /><div class=\"OtTitle\">Restaurant Token</div>");
            PrintData.Append("<div style=\"width: 396px; font-size: 9px; font-type: Arial\"><br />");
            PrintData.Append(DateTime.Now.ToString());
            PrintData.Append("<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />");
            PrintData.Append("<hr>");
            PrintData.Append(Restaurant);
            if (TxtComments.Text != "") Ot.InnerHtml += "<strong>Comments: </strong><b />" + TxtComments.Text;
            PrintData.Append("</div>");
            Ot.InnerHtml = PrintData.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);
        }
    }

    protected void BtnDeleteSelectedToken_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Please Enter A Reason For Token Cancellation.";
        LtrTokenCancel.Visible = true;
        BtnTokenCancel.Visible = true;
        TxtTokenCancel.Visible = true;
        BtnTokenReset.Visible = true;
    }

    protected void CheckAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < DgToken.Items.Count; i++)
        {
            ((System.Web.UI.WebControls.CheckBox)DgToken.Items[i].Cells[0].FindControl("ChkBox")).Checked = true;
        }
    }

    protected void UnCheckAll_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < DgToken.Items.Count; i++)
        {
            ((System.Web.UI.WebControls.CheckBox)DgToken.Items[i].Cells[0].FindControl("ChkBox")).Checked = false;
        }
    }

    #endregion

    #region Custom Functions

    public string TableSource()
    {
        if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)).Select(S => S).Any().Equals(true))
            return (from S in Checkers.Sources
                    where S.Source_Type == "Table" && S.Source_Number.Value == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)
                    select S.Source_Id).Single().ToString();
        else
            return null;
    }

    public void FillOt()
    {
        Checkers = new CheckersDataContext();
        DgToken.DataSource = null;
        int TableSource1 = 0;

        if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)).Select(S => S).Any().Equals(true))
        {
            TableSource1 = (from S in Checkers.Sources
                            where S.Source_Type == "Table" && S.Source_Number.Value == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)
                            select S.Source_Id).Single();

            if (Checkers.Tokens.Where(T => T.Token_Status == 1 && T.Token_Source == int.Parse(TableSource())).Select(T => T).Any().Equals(true))
            {
                var Token = from T in Checkers.Tokens
                            from M in Checkers.Menus
                            where T.Token_Status == 1 && T.Token_Source == int.Parse(TableSource()) && T.Token_Menu == M.Menu_Id
                            orderby T.Token_Id descending
                            select new { T.Token_Id, T.Token_Quantity, T.Token_Type, M.Menu_Name };

                DgToken.DataSource = Token;
            }
            else
                DgToken.DataSource = null;
        }
        else
            DgToken.DataSource = null;

        DgToken.DataBind();
    }

    public void BillNumber()
    {
        if (HdnTableSource.Value != "")
            if (Checkers.Invoices.Where(I => I.Invoice_Status == 1 && I.Invoice_Source == int.Parse(HdnTableSource.Value)).Select(I => I.Invoice_Id).Any().Equals(true))
            {
                HdnBillNumber.Value = (from I in Checkers.Invoices
                                       where I.Invoice_Status == 1 && I.Invoice_Source == int.Parse(HdnTableSource.Value)
                                       select I.Invoice_Id).Single().ToString();

                PnlButton.Visible = true;
            }
            else
            {
                PnlButton.Visible = false;
            }
    }

    public void FillTables()
    {
        DdlTransfer.Items.Clear();
        List.Controls.Clear();
        ImageButton[] ImgBtnList = new ImageButton[NumberOfTables];
        int Number = 1;

        for (int Table = 0; Table < NumberOfTables; Table++)
        {
            ImageButton ImgBtnTable = new ImageButton();
            ImgBtnTable.ID = Number.ToString();
            ImgBtnTable.BorderWidth = 15;
            ImgBtnTable.BorderColor = Color.White;
            if (Checkers.Sources.Where(T => T.Source_Number == Number).Any() == true)
            {
                var TableStatus = (from S in Checkers.Sources
                                   where S.Source_Type == "Table" && S.Source_Number == Number
                                   orderby S.Source_Id descending
                                   select S.Source_Status).First();

                if (TableStatus == 0)
                {
                    ImgBtnTable.ImageUrl = "~/Assets/Image/TableGreen.png";
                    ImgBtnTable.ToolTip = "Table Empty";
                }
                if (TableStatus == 1)
                {
                    ImgBtnTable.ImageUrl = "~/Assets/Image/TableOrange.png";
                    ImgBtnTable.ToolTip = "Table Busy";
                    DdlTransfer.Items.Remove("");
                }
                else if (TableStatus == 2)
                {
                    ImgBtnTable.ImageUrl = "~/Assets/Image/TableRed.png";
                    ImgBtnTable.ToolTip = "Table Billed";
                }
            }
            else
            {
                ImgBtnTable.ImageUrl = "~/Assets/Image/TableGreen.png";
                ImgBtnTable.ToolTip = "Table Empty";
                DdlTransfer.Items.Add(new ListItem(Number.ToString()));
            }
            ImgBtnList[Table] = ImgBtnTable;
            ImgBtnList[Table].Click += new ImageClickEventHandler(BtnTable_Click);
            List.Controls.Add(ImgBtnTable);
            Number++;
        }
    }

    public void ShowOrders()
    {
        Checkers = new CheckersDataContext();

        var OrderList = from O in Checkers.Sales
                        from M in Checkers.Menus
                        where O.Sales_Source == (from S in Checkers.Sources
                                                 where S.Source_Number == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)
                                                 select S.Source_Id).Single() && O.Sales_Status == 1
                                                 && O.Sales_Menu == M.Menu_Id
                        select new { O.Sales_Id, M.Menu_Name, O.Sales_Quantity, O.Sales_Cost };

        if (Enumerable.Count(OrderList) > 0)
        {
            DgOrderItems.Visible = true;
            DgOrderItems.DataSource = OrderList;
            DgOrderItems.DataBind();
            FillOt();
        }
        else
            DgOrderItems.Visible = false;
    }

    public void OrderInformation()
    {
        Checkers = new CheckersDataContext();

        if ((from S in Checkers.Sources where S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Type == "Table" && (S.Source_Status == 1 || S.Source_Status == 2) select S).Any().Equals(true))
            LblTotalCost.Text = (from S in Checkers.Sources
                                 where S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Type == "Table" && (S.Source_Status == 1 || S.Source_Status == 2)
                                 select S.Source_AmountPayable).Single().ToString();

        if (DgOrderItems.Items.Count > 0)
        {
            LblTotalItems.Text = DgOrderItems.Items.Count.ToString();
            MenuType();
        }
        else
        {
            LblTotalItems.Text = "";
        }

        if (HdnBillNumber.Value != "")
        {
            var BillNumber = Checkers.Invoices.Where(I => I.Invoice_Id == int.Parse(HdnBillNumber.Value)).Select(I => I).Single();
            TxtDiscount.Text = BillNumber.Invoice_Discount.ToString();
            TxtDiscountBar.Text = BillNumber.Invoice_DiscountBar.ToString();
            TxtDiscountRestaurant.Text = BillNumber.Invoice_DiscountRestaurant.ToString();
            TxtNoOfPeople.Text = BillNumber.Invoice_NoOfPeople.ToString();
            DdlSteward.ClearSelection();
            DdlSteward.Items.FindByValue(BillNumber.Invoice_Steward.ToString());
        }
    }

    public void MenuType()
    {
        int RestaurantItems = 0, BarItems = 0;
        for (int Id = 0; Id < DgOrderItems.Items.Count; Id++)
        {
            int MenuId = Checkers.Sales.Where(S => S.Sales_Id == int.Parse(DgOrderItems.Items[Id].Cells[0].Text)).Select(S => S.Sales_Menu.Value).Single();
            string MenuType = Checkers.Menus.Where(M => M.Menu_Id == MenuId).Select(M => M.Menu_Category).Single();
            if (MenuType == "Restaurant")
                RestaurantItems++;
            else
                BarItems++;

            LblBarItems.Text = BarItems.ToString();
            LblRestaurantItems.Text = RestaurantItems.ToString();
        }
    }

    public void ClearForm()
    {
        LblBarItems.Text = "";
        LblTotalItems.Text = "";
        LblRestaurantItems.Text = "";
        LblTotalCost.Text = "";
        TxtDiscount.Text = "0.0";
        TxtDiscountBar.Text = "0.0";
        TxtDiscountRestaurant.Text = "0.0";
        TxtNoOfPeople.Text = "0";
        TxtReason.Text = "";
        TxtClient.Text = "";
        TxtOpenMenuCost.Text = "0.0";
        TxtOpenMenuQuantity.Text = "0.0";
        TxtOpenMenuName.Text = "";
        TxtTokenCancel.Text = "";
        TxtComments.Text = "";
        PnlButton.Visible = false;
        DgOrderItems.DataSource = null;
        DgOrderItems.DataBind();
    }

    #endregion
}