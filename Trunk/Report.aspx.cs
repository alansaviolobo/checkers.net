using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class Report : System.Web.UI.Page
{
    public CheckersDataContext Checkers;
    public static string FromDate, ToDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        DdlMenu.Items.Clear();
        Checkers = new CheckersDataContext();
        var Items = Checkers.Menus.Where(M => M.Menu_Status.Equals(1)).Select(M => M);
        foreach (var M in Items)
            DdlMenu.Items.Add(new ListItem(M.Menu_Name, M.Menu_Id.ToString()));
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 8;
    }

    public void ReportHeader(string ReportType)
    {
        Checkers = new CheckersDataContext();

        FromDate = TxtFromDate.Text;
        ToDate = TxtToDate.Text;

        string attachment = "attachment; filename=" + ReportType + " (" + FromDate + "-" + ToDate + ").csv";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("Pragma", "public");
    }

    public void ReportColumns(string ReportType, string Columns)
    {
        HttpContext.Current.Response.Write("Quatro. Restaurant n Pub");
        HttpContext.Current.Response.Write(Environment.NewLine);
        HttpContext.Current.Response.Write("Report Type- " + ReportType);
        HttpContext.Current.Response.Write(Environment.NewLine);
        HttpContext.Current.Response.Write("From- " + FromDate);
        HttpContext.Current.Response.Write(Environment.NewLine);
        HttpContext.Current.Response.Write("To- " + ToDate);
        HttpContext.Current.Response.Write(Environment.NewLine);
        HttpContext.Current.Response.Write(Environment.NewLine);
        HttpContext.Current.Response.Write(Columns);
        HttpContext.Current.Response.Write(Environment.NewLine);
    }

    protected void BtnPettyCash_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("PettyCash");
        ReportColumns("PettyCash", ",Amount(Rs.), Given By, Received By, Date");

        var PettyCash = Checkers.ExecuteQuery<PettyCash>("select * from pettycash where (convert(char(10), pettycash_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var PC in PettyCash)
        {
            StringBuilder StrData = new StringBuilder();
            var ContactDetails = Checkers.Contacts.Select(C => C);
            AddComma("", StrData);
            AddComma(PC.PettyCash_Amount.ToString(), StrData);
            AddComma(Checkers.Contacts.Where(C => C.Contact_Id == PC.PettyCash_GivenBy).Select(C => C.Contact_Name).Single().ToString(), StrData);
            AddComma(Checkers.Contacts.Where(C => C.Contact_Id == PC.PettyCash_ReceivedBy).Select(C => C.Contact_Name).Single().ToString(), StrData);
            AddComma(PC.PettyCash_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnPettyExpense_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("PettyExpense");
        ReportColumns("PettyExpense", ",Merchandise, Quantity, Amount, Received By, Date");

        var PettyExpense = Checkers.ExecuteQuery<PettyExpense>("select * from pettyexpense where (convert(char(10), pettyexpense_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var PE in PettyExpense)
        {
            StringBuilder StrData = new StringBuilder();
            var ContactDetails = Checkers.Contacts.Select(C => C);
            AddComma("", StrData);
            AddComma(PE.PettyExpense_Narration.ToString(), StrData);
            AddComma(PE.PettyExpense_Quantity.ToString(), StrData);
            AddComma(PE.PettyExpense_Amount.ToString(), StrData);
            AddComma(Checkers.Contacts.Where(C => C.Contact_Id == PE.PettyExpense_ReceivedBy).Select(C => C.Contact_Name).Single().ToString(), StrData);
            AddComma(PE.PettyExpense_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnPurchaseTotal_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Purchase Total");
        ReportColumns("Purchase Total", ",Item, Quantity, Cost(Rs.), Date");

        var Purchase = Checkers.ExecuteQuery<Purchase>("select * from purchase where (convert(char(10), purchase_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var P in Purchase)
        {
            StringBuilder StrData = new StringBuilder();
            var InventoryDetails = Checkers.Inventories.Where(I => I.Inventory_Id == P.Purchase_Inventory).Select(I => I).Single();
            AddComma("", StrData);
            AddComma(InventoryDetails.Inventory_Name.ToString(), StrData);
            AddComma(P.Purchase_Quantity.ToString(), StrData);
            AddComma((InventoryDetails.Inventory_BuyingPrice * P.Purchase_Quantity).ToString(), StrData);
            AddComma(P.Purchase_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnSalesTotal_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Total");
        ReportColumns("Sales Total", ",Invoice No., Amount, Amount Bar, Amount Restaurant, Discount, Discount Bar, Discount Restaurant, Payment Mode, Date");

        var Invoice = Checkers.ExecuteQuery<Invoice>("select * from invoice where invoice_status = 0 and (convert(char(10), invoice_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var I in Invoice)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(I.Invoice_Id.ToString(), StrData);
            AddComma(I.Invoice_Amount.ToString(), StrData);
            AddComma(I.Invoice_AmountBar.ToString(), StrData);
            AddComma(I.Invoice_AmountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_Discount.ToString(), StrData);
            AddComma(I.Invoice_DiscountBar.ToString(), StrData);
            AddComma(I.Invoice_DiscountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_PaymentMode.ToString(), StrData);
            AddComma(I.Invoice_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnReceipts_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Receipts");
        ReportColumns("Receipts", ",Receipt No., Amount, Payment Mode, Client, Date");

        var Receipt = Checkers.ExecuteQuery<Receipt>("select * from receipt where receipt_status = 0 and (convert(char(10), receipt_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var R in Receipt)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(R.Receipt_Id.ToString(), StrData);
            AddComma(R.Receipt_Amount.ToString(), StrData);
            AddComma(R.Receipt_PaymentMode.ToString(), StrData);
            var Client = Checkers.Contacts.Where(C => C.Contact_Id == R.Receipt_Client).Select(C => C.Contact_Name).Single();
            AddComma(Client, StrData);
            AddComma(R.Receipt_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnCancelledTokens_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Cancelled Tokens");
        ReportColumns("Cancelled Tokens", ",Token No., Type, Menu, Quantity, Invoice No., Reason, Date");

        var Token = Checkers.ExecuteQuery<Token>("select * from token where token_status = 2 and (convert(char(10), token_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var T in Token)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(T.Token_Id.ToString(), StrData);
            AddComma(T.Token_Type.ToString(), StrData);
            var Menu = Checkers.Menus.Where(M => M.Menu_Id == T.Token_Menu).Select(M => M.Menu_Name).Single();
            AddComma(Menu.ToString(), StrData);
            AddComma(T.Token_Quantity.ToString(), StrData);
            var Invoice = "";
            if (Checkers.Invoices.Where(I => I.Invoice_Source.Value == T.Token_Source.Value).Select(I => I).Any())
                Invoice = Checkers.Invoices.Where(I => I.Invoice_Source.Value == T.Token_Source.Value).Select(I => I.Invoice_Id.ToString()).Single();
            else Invoice = "Nill";
            AddComma(Invoice.ToString(), StrData);
            AddComma(T.Token_Reason.ToString(), StrData);
            AddComma(T.Token_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnSalesItem_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Item - " + DdlMenu.SelectedItem.Text);
        ReportColumns("Sales Item - " + DdlMenu.SelectedItem.Text, ",Name, Quantity, Invoice No., Date");

        var Sale = Checkers.ExecuteQuery<Sale>("select * from sales where sales_menu = " + DdlMenu.SelectedItem.Value + " and sales_status = 0 and (convert(char(10), sales_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var S in Sale)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            var Menu = Checkers.Menus.Where(M => M.Menu_Id == S.Sales_Menu).Select(M => M.Menu_Name).Single();
            AddComma(Menu.ToString(), StrData);
            AddComma(S.Sales_Quantity.ToString(), StrData);
            var Invoice = "";
            if (Checkers.Invoices.Where(I => I.Invoice_Source == S.Sales_Source).Select(I => I).Any())
                Invoice = Checkers.Invoices.Where(I => I.Invoice_Source == S.Sales_Source && I.Invoice_Status == 0).Select(I => I.Invoice_Id.ToString()).Single();
            else Invoice = "Nill";
            AddComma(Invoice.ToString(), StrData);
            AddComma(S.Sales_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnSalesCategory_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Category - " + DdlCategory.SelectedItem.Text);
        ReportColumns("Sales Category - " + DdlCategory.SelectedItem.Text, ",Name, Quantity, Invoice No., Date");

        var SaleList = Checkers.ExecuteQuery<Sale>("select * from sales where sales_status = 0 and (convert(char(10), sales_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");
        var MenuList = Checkers.Menus.Where(M => M.Menu_Category == DdlCategory.SelectedItem.Text && M.Menu_Status == 1).Select(M => M);

        foreach (var S in SaleList)
        {
            if (Checkers.Menus.Where(M => M.Menu_Status == 1 && M.Menu_Id == S.Sales_Menu.Value && M.Menu_Category == DdlCategory.SelectedItem.Text).Select(M => M).Any())
            {
                var MenuItem = Checkers.Menus.Where(M => M.Menu_Status == 1 && M.Menu_Id == S.Sales_Menu.Value && M.Menu_Category == DdlCategory.SelectedItem.Text).Select(M => M).Single();
                StringBuilder StrData = new StringBuilder();
                AddComma("", StrData);
                AddComma(MenuItem.Menu_Name.ToString(), StrData);
                AddComma(S.Sales_Quantity.ToString(), StrData);
                var Invoice = Checkers.Invoices.Where(I => I.Invoice_Source == S.Sales_Source && I.Invoice_Status == 0).Select(I => I.Invoice_Id).SingleOrDefault();
                AddComma(Invoice.ToString(), StrData);
                AddComma(S.Sales_TimeStamp.ToString(), StrData);
                HttpContext.Current.Response.Write(StrData.ToString());
                HttpContext.Current.Response.Write(Environment.NewLine);
            }
        }

        HttpContext.Current.Response.End();
    }

    public void AddComma(string Value, StringBuilder StrData)
    {
        StrData.Append(Value.Replace(',', ' '));
        StrData.Append(", ");
    }

    protected void BtnCreditCard_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Total");
        ReportColumns("Sales Total", ",Invoice No., Amount, Amount Bar, Amount Restaurant, Discount, Discount Bar, Discount Restaurant, Payment Mode, Date");

        var Invoice = Checkers.ExecuteQuery<Invoice>("select * from invoice where invoice_paymentmode = 'Credit Card' and invoice_status = 0 and (convert(char(10), invoice_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var I in Invoice)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(I.Invoice_Id.ToString(), StrData);
            AddComma(I.Invoice_Amount.ToString(), StrData);
            AddComma(I.Invoice_AmountBar.ToString(), StrData);
            AddComma(I.Invoice_AmountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_Discount.ToString(), StrData);
            AddComma(I.Invoice_DiscountBar.ToString(), StrData);
            AddComma(I.Invoice_DiscountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_PaymentMode.ToString(), StrData);
            AddComma(I.Invoice_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnCredit_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Total");
        ReportColumns("Sales Total", ",Invoice No., Amount, Amount Bar, Amount Restaurant, Discount, Discount Bar, Discount Restaurant, Payment Mode, Date");

        var Invoice = Checkers.ExecuteQuery<Invoice>("select * from invoice where invoice_paymentmode = 'Credit' and invoice_status = 0 and (convert(char(10), invoice_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var I in Invoice)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(I.Invoice_Id.ToString(), StrData);
            AddComma(I.Invoice_Amount.ToString(), StrData);
            AddComma(I.Invoice_AmountBar.ToString(), StrData);
            AddComma(I.Invoice_AmountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_Discount.ToString(), StrData);
            AddComma(I.Invoice_DiscountBar.ToString(), StrData);
            AddComma(I.Invoice_DiscountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_PaymentMode.ToString(), StrData);
            AddComma(I.Invoice_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnCash_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Total");
        ReportColumns("Sales Total", ",Invoice No., Amount, Amount Bar, Amount Restaurant, Discount, Discount Bar, Discount Restaurant, Payment Mode, Date");

        var Invoice = Checkers.ExecuteQuery<Invoice>("select * from invoice where invoice_paymentmode = 'Cash' and invoice_status = 0 and (convert(char(10), invoice_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var I in Invoice)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(I.Invoice_Id.ToString(), StrData);
            AddComma(I.Invoice_Amount.ToString(), StrData);
            AddComma(I.Invoice_AmountBar.ToString(), StrData);
            AddComma(I.Invoice_AmountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_Discount.ToString(), StrData);
            AddComma(I.Invoice_DiscountBar.ToString(), StrData);
            AddComma(I.Invoice_DiscountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_PaymentMode.ToString(), StrData);
            AddComma(I.Invoice_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnCheque_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Total");
        ReportColumns("Sales Total", ",Invoice No., Amount, Amount Bar, Amount Restaurant, Discount, Discount Bar, Discount Restaurant, Payment Mode, Date");

        var Invoice = Checkers.ExecuteQuery<Invoice>("select * from invoice where invoice_paymentmode = 'Cheque' and invoice_status = 0 and (convert(char(10), invoice_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var I in Invoice)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(I.Invoice_Id.ToString(), StrData);
            AddComma(I.Invoice_Amount.ToString(), StrData);
            AddComma(I.Invoice_AmountBar.ToString(), StrData);
            AddComma(I.Invoice_AmountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_Discount.ToString(), StrData);
            AddComma(I.Invoice_DiscountBar.ToString(), StrData);
            AddComma(I.Invoice_DiscountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_PaymentMode.ToString(), StrData);
            AddComma(I.Invoice_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }

    protected void BtnZeroBilling_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportHeader("Sales Total");
        ReportColumns("Sales Total", ",Invoice No., Amount, Amount Bar, Amount Restaurant, Discount, Discount Bar, Discount Restaurant, Payment Mode, Date");

        var Invoice = Checkers.ExecuteQuery<Invoice>("select * from invoice where invoice_paymentmode = 'Zero Billing' and invoice_status = 0 and (convert(char(10), invoice_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')");

        foreach (var I in Invoice)
        {
            StringBuilder StrData = new StringBuilder();
            AddComma("", StrData);
            AddComma(I.Invoice_Id.ToString(), StrData);
            AddComma(I.Invoice_Amount.ToString(), StrData);
            AddComma(I.Invoice_AmountBar.ToString(), StrData);
            AddComma(I.Invoice_AmountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_Discount.ToString(), StrData);
            AddComma(I.Invoice_DiscountBar.ToString(), StrData);
            AddComma(I.Invoice_DiscountRestaurant.ToString(), StrData);
            AddComma(I.Invoice_PaymentMode.ToString(), StrData);
            AddComma(I.Invoice_TimeStamp.ToString(), StrData);
            HttpContext.Current.Response.Write(StrData.ToString());
            HttpContext.Current.Response.Write(Environment.NewLine);
        }

        HttpContext.Current.Response.End();
    }
}