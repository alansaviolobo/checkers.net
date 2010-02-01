using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Controls_Invoice : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["Id"] != null)
        {
            TxtBillNumber.Text = Request.QueryString["Id"].ToString();
            FillData();
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        FillData();
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();

        var InvoiceDetails = Checkers.Invoices.Where(I => I.Invoice_Id == int.Parse(TxtBillNumber.Text)).Select(I => I).Single();
        var SalesDetails = from O in Checkers.Sales
                           from M in Checkers.Menus
                           where O.Sales_Source == InvoiceDetails.Invoice_Source && O.Sales_Status != 2 && O.Sales_Menu == M.Menu_Id
                           select new { O.Sales_Id, M.Menu_Name, M.Menu_Category, O.Sales_Quantity, O.Sales_Cost };

        LtrDate.Text = DateTime.Now.ToShortDateString();
        LtrTime.Text = DateTime.Now.ToShortTimeString();
        LtrTableNo.Text = Checkers.Sources.Where(S => S.Source_Id == InvoiceDetails.Invoice_Source).Select(S => S.Source_Number).Single().ToString();
        LtrSteward.Text = Checkers.Contacts.Where(C => C.Contact_Id == InvoiceDetails.Invoice_Steward).Select(C => C.Contact_Name).Single().ToString();
        LtrBillNo.Text = TxtBillNumber.Text;


        foreach (var O in SalesDetails)
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

        TblBillFooterCellKey.InnerHtml = "<strong>S.Total<br />Discount(" + (InvoiceDetails.Invoice_Discount.Value * 100) / InvoiceDetails.Invoice_Amount + ")<br />Total</strong>";
        TblBillFooterCellValue.InnerHtml = "Rs." + InvoiceDetails.Invoice_Amount.Value + "<br />";
        TblBillFooterCellValue.InnerHtml += "Rs." + InvoiceDetails.Invoice_Discount.Value + "<br />";
        TblBillFooterCellValue.InnerHtml += "Rs." + (InvoiceDetails.Invoice_Amount.Value - InvoiceDetails.Invoice_Discount.Value) + "/-";

        TblBillFooter.Cells.Add(TblBillFooterCell1);
        TblBillFooter.Cells.Add(TblBillFooterCellKey);
        TblBillFooter.Cells.Add(TblBillFooterCellValue);
        TblBill.Rows.Add(TblBillFooter);
    }
}
