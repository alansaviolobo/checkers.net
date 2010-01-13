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

public partial class Order : System.Web.UI.Page
{
    #region Parameters

    public CheckersDataContext Checkers;
    public static int NumberOfTables = 40;
    public static int MaximumQuantity = 50;

    #endregion

    #region Form Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
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

                var Item = from I in Checkers.Menus
                           where I.Menu_Status.Equals(1)
                           select I;
                foreach (var I in Item)
                    DdlItem.Items.Add(new ListItem(I.Menu_Name, I.Menu_Id.ToString()));

                for (int Quantity = 1; Quantity < MaximumQuantity; Quantity++)
                    DdlQuantity.Items.Add(new ListItem(Quantity.ToString()));


                if (Checkers.Contacts.Where(C => C.Contact_Id.Equals(int.Parse(Session["UserId"].ToString()))).Select(C => C.Contact_Type).Single().Equals("Administrator"))
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

        //if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)).Select(S => S).Any().Equals(true))
        //    HdnTableSource.Value = (from S in Checkers.Sources
        //                            where S.Source_Type == "Table" && S.Source_Number.Value == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)
        //                            select S.Source_Id).Single().ToString();
        //else
        //    HdnTableSource.Value = "";

        BillNumber();
        ClearForm();

        LtrBillTableNumber.Text = HdnTableId.Value;
        LtrItemTableNumber.Text = HdnTableId.Value;

        if (ImgBtnTable.ToolTip == "Table Empty")
        {
            BtnBill.Text = "Print Bill";
            TxtDiscount.ReadOnly = false;
            TxtTax.ReadOnly = false;
        }
        else if (ImgBtnTable.ToolTip == "Table Busy")
        {
            BtnBill.Text = "Print Bill";
            TxtDiscount.ReadOnly = false;
            TxtTax.ReadOnly = false;
        }
        else if (ImgBtnTable.ToolTip == "Table Billed")
        {
            BtnBill.Text = "RePrint Bill";
            TxtDiscount.ReadOnly = true;
            TxtTax.ReadOnly = true;
            PnlButton.Visible = true;
        }

        ShowOrders();
        OrderInformation();
        FillOt();
    }

    protected void BtnOrder_Click(object sender, EventArgs e)
    {
        if (HdnTableId.Value != "")
        {
            Checkers = new CheckersDataContext();
            int Status;

            if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Status == 2).Any().Equals(true))
                LtrMessage.Text = "Table " + HdnTableId.Value + " Already Billed. Please Close The Bill.";
            else
            {
                Status = Checkers.SalesNew(0, int.Parse(DdlItem.SelectedItem.Value), decimal.Parse(DdlQuantity.SelectedItem.Text), int.Parse(HdnTableId.Value), "Table", 0);
                LtrMessage.Text = Status == 1 ? "Item " + DdlItem.SelectedItem.Text + " Of Quantity " + DdlQuantity.SelectedItem.Text + " Ordered For Table No. " + HdnTableId.Value : "Error Occurred";

                var MenuConverter = Checkers.Converters.Where(C => C.Converter_Menu == int.Parse(DdlItem.SelectedItem.Value) && C.Converter_Status == 1).Select(C => C);
                foreach (var M in MenuConverter)
                {
                    decimal Quantity = M.Converter_InventoryQuantity.Value * decimal.Parse(DdlQuantity.SelectedItem.Text);
                    Status = Checkers.InventorySubtract(M.Converter_Inventory, Quantity);
                }

                var MenuType = Checkers.Menus.Where(M => M.Menu_Id == int.Parse(DdlItem.SelectedItem.Value)).Select(M => M.Menu_Category).Single();

                Status = Checkers.TokenNew(MenuType.ToString() == "Bar" ? "BOT" : "KOT", int.Parse(DdlItem.SelectedItem.Value), decimal.Parse(DdlQuantity.SelectedItem.Text), int.Parse(TableSource().ToString()));

                LblOt.Text = MenuType.ToString() == "Bar" ? "BOT" : "KOT";

                Ot.InnerHtml = "<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />";
                Ot.InnerHtml += "<hr>";
                Ot.InnerHtml += "<strong>Item: </strong>" + DdlItem.SelectedItem.Text + "<br />";
                Ot.InnerHtml += "<strong>Quantity: </strong>" + decimal.Parse(DdlQuantity.SelectedItem.Text);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);

                ShowOrders();
                OrderInformation();
            }
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

        Status = Checkers.SalesDelete(SalesId, Menu, decimal.Parse(e.Item.Cells[2].Text), int.Parse(HdnTableId.Value), "Table");
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
                decimal Tax = (decimal.Parse(LblTotalCost.Text) * decimal.Parse(TxtTax.Text)) / 100;
                decimal Total = (SubTotal - Discount) + Tax;

                if (BtnBill.Text == "Print Bill")
                {
                    Status = Checkers.InvoiceNew(0, SubTotal, Discount, Tax, int.Parse(TableSource()), null);
                    LtrMessage.Text = Status == 1 ? "Bill Printed For Table No. " + HdnTableId.Value + "." : "Error Occurred.";
                    BtnBill.Text = "RePrint Bill";
                }
                else
                    LtrMessage.Text = "Bill Re Printed For Table No. " + HdnTableId.Value;

                LtrDate.Text = DateTime.Now.ToShortDateString();
                LtrTime.Text = DateTime.Now.ToShortTimeString();
                LtrTableNo.Text = HdnTableId.Value;

                var OrderList = from O in Checkers.Sales
                                from M in Checkers.Menus
                                where O.Sales_Source == (from S in Checkers.Sources
                                                         where S.Source_Number == int.Parse(HdnTableId.Value) && S.Source_Status == 2
                                                         select S.Source_Id).Single() && O.Sales_Status == 1
                                                         && O.Sales_Menu == M.Menu_Id
                                select new { O.Sales_Id, M.Menu_Name, O.Sales_Quantity, O.Sales_Cost };

                BillNumber();

                LtrBillNo.Text = HdnBillNumber.Value;

                foreach (var O in OrderList)
                {
                    HtmlTableRow TblBillRow = new HtmlTableRow();
                    HtmlTableCell TblBillCellItem = new HtmlTableCell();
                    HtmlTableCell TblBillCellQnty = new HtmlTableCell();
                    HtmlTableCell TblBillCellCost = new HtmlTableCell();
                    TblBillCellItem.InnerText = O.Menu_Name;
                    TblBillCellQnty.InnerText = O.Sales_Quantity.ToString();
                    TblBillCellCost.InnerText = O.Sales_Cost.ToString();
                    TblBillRow.Cells.Add(TblBillCellItem);
                    TblBillRow.Cells.Add(TblBillCellQnty);
                    TblBillRow.Cells.Add(TblBillCellCost);
                    TblBill.Rows.Add(TblBillRow);
                }

                HtmlTableRow TblBillFooter = new HtmlTableRow();
                HtmlTableCell TblBillFooterCell1 = new HtmlTableCell();
                HtmlTableCell TblBillFooterCellKey = new HtmlTableCell();
                HtmlTableCell TblBillFooterCellValue = new HtmlTableCell();

                TblBillFooterCell1.InnerHtml = "<div style=\"width: 50%; float: left; text-align: center\">Cashier</div><div style=\"width: 50%; float: right; text-align: center\">Customer</div>";
                TblBillFooterCell1.VAlign = "bottom";
                TblBillFooterCellKey.InnerHtml = "<strong>S.Total<br />Tax<br />Discount<br />Total</strong>";
                TblBillFooterCellValue.InnerHtml = "Rs. " + SubTotal + "<br />";
                TblBillFooterCellValue.InnerHtml += "Rs. " + Tax + "<br />";
                TblBillFooterCellValue.InnerHtml += "Rs. " + Discount + "<br />";
                TblBillFooterCellValue.InnerHtml += "Rs. " + Total;

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
        Checkers = new CheckersDataContext();

        if (DdlToken.Items.Count > 0)
        {
            var Token = Checkers.Tokens.Where(T => T.Token_Id == int.Parse(DdlToken.SelectedItem.Value) && T.Token_Status == 1).Select(T => T).Single();

            var MenuType = Checkers.Menus.Where(M => M.Menu_Id == Token.Token_Menu).Select(M => M.Menu_Category);

            LblOt.Text = MenuType.ToString() == "Bar" ? "BOT" : "KOT";

            Ot.InnerHtml = "<br /><br /><strong>Table No: </strong>" + HdnTableId.Value + "<br /><br />";
            Ot.InnerHtml += "<hr>";
            Ot.InnerHtml += "<strong>Item: </strong>" + Checkers.Menus.Where(M => M.Menu_Id == Token.Token_Menu).Select(M => M.Menu_Name).Single() + "<br />";
            Ot.InnerHtml += "<strong>Quantity: </strong>" + Token.Token_Quantity.ToString();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintOt');", true);
        }
        else
        {
            LtrMessage.Text = "No Items For Table No. " + HdnTableId.Value + ".";
        }
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

        Status = Checkers.InvoiceCloseCredit(int.Parse(HdnBillNumber.Value), int.Parse(HdnClientId.Value), int.Parse(HdnTableSource.Value));
        LtrMessage.Text = Status == 1 ? "Bill " + HdnBillNumber.Value + " Closed." : "Error Occurred.";

        FillTables();
        ClearForm();

        LtrClient.Visible = false;
        TxtClient.Visible = false;
        BtnCreditOk.Visible = false;
    }

    protected void BtnCredit_Click(object sender, EventArgs e)
    {
        LtrClient.Visible = true;
        TxtClient.Visible = true;
        BtnCreditOk.Visible = true;
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
        DdlToken.Items.Clear();
        int TableSource1 = 0;

        if (Checkers.Sources.Where(S => S.Source_Type == "Table" && S.Source_Number == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)).Select(S => S).Any().Equals(true))
        {
            TableSource1 = (from S in Checkers.Sources
                            where S.Source_Type == "Table" && S.Source_Number.Value == int.Parse(HdnTableId.Value) && (S.Source_Status == 1 || S.Source_Status == 2)
                            select S.Source_Id).Single();

            if (Checkers.Tokens.Where(T => T.Token_Status == 1 && T.Token_Source == int.Parse(TableSource())).Select(T => T).Any().Equals(true))
            {
                var Token = from T in Checkers.Tokens
                            where T.Token_Status == 1 && T.Token_Source == int.Parse(TableSource())
                            orderby T.Token_Id descending
                            select T;

                foreach (var T in Token)
                    DdlToken.Items.Add(new ListItem((Checkers.Menus.Where(M => M.Menu_Id == T.Token_Menu).Select(M => M.Menu_Name).Single()) + " (" + T.Token_Quantity.ToString() + " nos.)", T.Token_Id.ToString()));
            }
            else
                DdlToken.Items.Clear();
        }
        else
            DdlToken.Items.Clear();

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
                        select new { O.Sales_Id, M.Menu_Name, O.Sales_Quantity };

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
            TxtTax.Text = BillNumber.Invoice_Tax.ToString();
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
        TxtTax.Text = "0.0";
        TxtReason.Text = "";
        TxtClient.Text = "";
        PnlButton.Visible = false;
        DgOrderItems.DataSource = null;
        DgOrderItems.DataBind();
    }

    #endregion
}