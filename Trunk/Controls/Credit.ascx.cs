using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Credit : System.Web.UI.UserControl
{
    public CheckersDataContext Checkers;
    public static int Id;

    protected void Page_Load(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();

        if (!Page.IsPostBack)
        {
            var Clients = Checkers.Contacts.Where(C => C.Contact_Status.Equals(1) && C.Contact_Credit > 0).Select(C => C);
            foreach (var C in Clients)
                DdlClientName.Items.Add(new ListItem(C.Contact_Name + "- " + C.Contact_OrganizationName, C.Contact_Id.ToString()));
        }
        LtrTotalOutstanding.Text = (from C in Checkers.Contacts
                                    where C.Contact_Status == 1 && C.Contact_Type == "Customer"
                                    select C.Contact_Credit.Value).Sum().ToString();
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 2;
    }
    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        HdnClientId.Value = DdlClientName.SelectedItem.Value;
        FillData();
    }
    protected void BtnPayAmount_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        NumberToEnglish Num = new NumberToEnglish();

        int Status;

        if (decimal.Parse(LtrAmountRemaining.Text) < decimal.Parse(TxtAmount.Text))
        {
            LtrMessage.Text = "Amount Entered Is More Than Remaining Amount.";
        }
        else
        {
            if (HdnClientId.Value != "")
            {
                //Status = Checkers.ReceiptNew(decimal.Parse(TxtAmount.Text), DdlPayBy.SelectedItem.Text, int.Parse(HdnClientId.Value));
                //LtrMessage.Text = Status == 1 ? "Payment Of Amount Rs." + TxtAmount.Text + " Received." : "Error Occurred.";
                //if (Status == 1) Status = Checkers.ActivityNew("Payment Of Amount Rs." + TxtAmount.Text + " Received From " + DdlClientName.SelectedItem.Text, int.Parse(Session["UserId"].ToString()));

                LblDate.Text = DateTime.Today.ToShortDateString();
                LblPaidTo.Text = "Quatro. Verna-Goa.";
                LblPaidBy.Text = DdlClientName.SelectedItem.Text;
                LblAmountNumber.Text = "Rs. " + TxtAmount.Text;
                LblAmountWord.Text = "Rs. " + Num.changeCurrencyToWords(double.Parse(TxtAmount.Text)).ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintReceipt');", true);
            }
            else
                LtrMessage.Text = "Please Select A Client.";
        }

        FillData();
    }

    public void FillData()
    {
        Checkers = new CheckersDataContext();
        decimal BillAmount = 0;

        LtrTotalOutstanding.Text = (from C in Checkers.Contacts
                                    where C.Contact_Status == 1 && C.Contact_Type == "Customer"
                                    select C.Contact_Credit.Value).Sum().ToString();

        if (HdnClientId.Value != "")
        {
            var Bills = Checkers.Invoices.Where(I => I.Invoice_Client == int.Parse(HdnClientId.Value) && I.Invoice_Type == "Bill").Select(I => new { I.Invoice_Id, I.Invoice_Amount, I.Invoice_Tax, I.Invoice_Discount });

            if (Enumerable.Count(Bills) > 0)
            {
                var Source = Checkers.Invoices.Where(I => I.Invoice_Client == int.Parse(HdnClientId.Value) && I.Invoice_Type == "Bill").Select(I => I);
                foreach (var S in Source)
                {
                    BillAmount += (S.Invoice_Amount.Value - S.Invoice_Discount.Value) - S.Invoice_Tax.Value;
                }
                LtrAmountPayable.Text = BillAmount.ToString();
            }

            var Receipts = Checkers.Invoices.Where(I => I.Invoice_Client == int.Parse(HdnClientId.Value) && I.Invoice_Type == "Receipt" && I.Invoice_Status == 0).Select(I => new { I.Invoice_Id, I.Invoice_Amount, I.Invoice_PaymentMode });
            if (Enumerable.Count(Receipts) > 0)
            {
                LtrAmountPaid.Text = Checkers.Invoices.Where(I => I.Invoice_Client == int.Parse(HdnClientId.Value) && I.Invoice_Type == "Receipt" && I.Invoice_Status == 0).Select(I => I.Invoice_Amount.Value).Sum().ToString();
            }

            if (Enumerable.Count(Bills) > 0)
            {
                LtrBillsNumber.Text = "(Total : " + Enumerable.Count(Bills).ToString() + ")";
                DgBills.DataSource = Bills;
                DgBills.DataBind();
            }

            if (Enumerable.Count(Receipts) > 0)
            {
                LtrBillsNumber.Text = "(Total : " + Enumerable.Count(Receipts).ToString() + ")";
                DgReceipts.DataSource = Receipts;
                DgReceipts.DataBind();
            }
            LtrAmountRemaining.Text = (decimal.Parse(LtrAmountPayable.Text) - decimal.Parse(LtrAmountPaid.Text)).ToString();
        }
        else
            LtrMessage.Text = "Please Select A Client.";
    }

    protected void DgOrderItems_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (HdnClientId.Value != "")
        {
            LtrMessage.Text = "Receipt No. " + e.Item.Cells[0].Text + " Will Be Deleted Permenantly. Do You Want To Proceed ? ";
            Id = int.Parse(e.Item.Cells[0].Text);
            BtnYes.Visible = true;
            BtnNo.Visible = true;
        }
        else
        {
            LtrMessage.Text = "Please Select A Client.";
        }
    }
    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.ReceiptDelete(Id, int.Parse(HdnClientId.Value));
        LtrMessage.Text = Status == 1 ? "Receipt No. " + Id + " Successfully Deleted." : "Error Occurred.";
        if (Status == 1) Status = Checkers.ActivityNew("Receipt No. " + Id + " Successfully Deleted", int.Parse(Session["UserId"].ToString()));

        LtrMessage.Text = "Credit Management.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;

        FillData();
    }
    protected void BtnNo_Click(object sender, EventArgs e)
    {
        LtrMessage.Text = "Action Cancelled By The User.";
        BtnYes.Visible = false;
        BtnNo.Visible = false;
    }
}
