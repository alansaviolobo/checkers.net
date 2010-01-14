using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

public partial class Report : System.Web.UI.Page
{
    public CheckersDataContext Checkers;
    public static string ReportType, FromDate, ToDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 8;
    }

    protected void BtnCreate_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        ReportType = DdlReportType.SelectedItem.Text;
        FromDate = TxtFromDate.Text;
        ToDate = TxtToDate.Text;

        string attachment = "attachment; filename=" + ReportType + " (" + FromDate + "-" + ToDate + ").csv";
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", attachment);
        HttpContext.Current.Response.ContentType = "text/csv";
        HttpContext.Current.Response.AddHeader("Pragma", "public");

        ColumnName();
        FillData();

        HttpContext.Current.Response.End();
    }

    public void ColumnName()
    {
        string Columns = "Quatro. Restaurant n Pub";
        HttpContext.Current.Response.Write(Columns);
        HttpContext.Current.Response.Write(Environment.NewLine);
        Columns = "Report- " + ReportType + ", From- " + FromDate + ", To- " + ToDate;
        HttpContext.Current.Response.Write(Columns);
        HttpContext.Current.Response.Write(Environment.NewLine);
        HttpContext.Current.Response.Write(Environment.NewLine);

        switch (ReportType)
        {
            case "Sales":
                Columns = "Invoice No., Amount, Discount, Tax, Payment Mode, Restaurant, Bar, Date";
                break;
            case "Purchase":
                Columns = "Item, Quantity, Cost(Rs.), Date";
                break;
            case "PettyCash":
                Columns = "Amount(Rs.), Given By, Received By, Date";
                break;
            case "PettyExpense":
                Columns = "Merchandise, Quantity, Amount, Received By, Date";
                break;
        }

        HttpContext.Current.Response.Write(Columns);
        HttpContext.Current.Response.Write(Environment.NewLine);
    }

    public void FillData()
    {
        StringBuilder StrData;
        string SqlStatement = "";
        Checkers = new CheckersDataContext();

        switch (ReportType)
        {
            case "Sales":
                SqlStatement = "select * from invoice where invoice_status = 0 and (convert(char(10), invoice_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')";
                var Invoice = Checkers.ExecuteQuery<Invoice>(SqlStatement);
                decimal BarItems = 0, RestaurantItems = 0;

                foreach (var I in Invoice)
                {
                    StrData = new StringBuilder();
                    AddComma(I.Invoice_Id.ToString(), StrData);
                    AddComma(I.Invoice_Amount.ToString(), StrData);
                    AddComma(I.Invoice_Discount.ToString(), StrData);
                    AddComma(I.Invoice_Tax.ToString(), StrData);
                    AddComma(I.Invoice_PaymentMode.ToString(), StrData);
                    var Sales = Checkers.Sales.Where(S => S.Sales_Source == I.Invoice_Source && S.Sales_Status == 0).Select(S => S);
                    foreach (var S in Sales)
                    {
                        if (Checkers.Menus.Where(M => M.Menu_Id == S.Sales_Menu && M.Menu_Status == 1).Select(M => M.Menu_Category).Single().Equals("Bar"))
                            BarItems += S.Sales_Cost.Value;
                        else
                            RestaurantItems += S.Sales_Cost.Value;
                    }
                    AddComma(RestaurantItems.ToString(), StrData);
                    AddComma(BarItems.ToString(), StrData);
                    AddComma(I.Invoice_TimeStamp.ToString(), StrData);
                    HttpContext.Current.Response.Write(StrData.ToString());
                    HttpContext.Current.Response.Write(Environment.NewLine);
                }
                break;
            case "Purchase":
                SqlStatement = "select * from purchase where (convert(char(10), purchase_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')";
                var Purchase = Checkers.ExecuteQuery<Purchase>(SqlStatement);

                foreach (var P in Purchase)
                {
                    StrData = new StringBuilder();
                    var InventoryDetails = Checkers.Inventories.Where(I => I.Inventory_Id == P.Purchase_Inventory).Select(I => I).Single();
                    AddComma(InventoryDetails.Inventory_Name.ToString(), StrData);
                    AddComma(P.Purchase_Quantity.ToString(), StrData);
                    AddComma((InventoryDetails.Inventory_BuyingPrice * P.Purchase_Quantity).ToString(), StrData);
                    AddComma(P.Purchase_TimeStamp.ToString(), StrData);
                    HttpContext.Current.Response.Write(StrData.ToString());
                    HttpContext.Current.Response.Write(Environment.NewLine);
                }
                break;
            case "PettyCash":
                SqlStatement = "select * from pettycash where (convert(char(10), pettycash_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')";
                var PettyCash = Checkers.ExecuteQuery<PettyCash>(SqlStatement);

                foreach (var PC in PettyCash)
                {
                    StrData = new StringBuilder();
                    var ContactDetails = Checkers.Contacts.Select(C => C);
                    AddComma(PC.PettyCash_Amount.ToString(), StrData);
                    AddComma(Checkers.Contacts.Where(C => C.Contact_Id == PC.PettyCash_GivenBy).Select(C => C.Contact_Name).Single().ToString(), StrData);
                    AddComma(Checkers.Contacts.Where(C => C.Contact_Id == PC.PettyCash_ReceivedBy).Select(C => C.Contact_Name).Single().ToString(), StrData);
                    AddComma(PC.PettyCash_TimeStamp.ToString(), StrData);
                    HttpContext.Current.Response.Write(StrData.ToString());
                    HttpContext.Current.Response.Write(Environment.NewLine);
                }
                break;
            case "PettyExpense":
                SqlStatement = "select * from pettyexpense where (convert(char(10), pettyexpense_timestamp, 103) between '" + FromDate + "' and '" + ToDate + "')";
                var PettyExpense = Checkers.ExecuteQuery<PettyExpense>(SqlStatement);

                foreach (var PE in PettyExpense)
                {
                    StrData = new StringBuilder();
                    var ContactDetails = Checkers.Contacts.Select(C => C);
                    AddComma(PE.PettyExpense_Merchandise.ToString(), StrData);
                    AddComma(PE.PettyExpense_Quantity.ToString(), StrData);
                    AddComma(PE.PettyExpense_Amount.ToString(), StrData);
                    AddComma(Checkers.Contacts.Where(C => C.Contact_Id == PE.PettyExpense_ReceivedBy).Select(C => C.Contact_Name).Single().ToString(), StrData);
                    AddComma(PE.PettyExpense_TimeStamp.ToString(), StrData);
                    HttpContext.Current.Response.Write(StrData.ToString());
                    HttpContext.Current.Response.Write(Environment.NewLine);
                }
                break;
        }

    }

    public void AddComma(string Value, StringBuilder StrData)
    {
        StrData.Append(Value.Replace(',', ' '));
        StrData.Append(", ");
    }
}
