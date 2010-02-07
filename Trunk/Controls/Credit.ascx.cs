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
        if (Checkers.Contacts.Where(C => C.Contact_Type == "Customer").Any().Equals(true))
            LtrTotalOutstanding.Text = (from C in Checkers.Contacts
                                        where C.Contact_Status == 1 && C.Contact_Type == "Customer"
                                        select C.Contact_Credit.Value).Sum().ToString();
        else
            LtrTotalOutstanding.Text = "0";
        ((AjaxControlToolkit.Accordion)Page.Master.FindControl("AccMenu")).SelectedIndex = 3;
    }

    protected void BtnSelect_Click(object sender, EventArgs e)
    {
        if (DdlClientName.Items.Count > 0)
        {
            HdnClientId.Value = DdlClientName.SelectedItem.Value;
            FillData();
        }
        else
            LtrMessage.Text = "No Credit Clients Exists.";
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
                Status = Checkers.ReceiptNew(decimal.Parse(TxtAmount.Text), DdlPayBy.SelectedItem.Text, int.Parse(HdnClientId.Value), DateTime.Parse(Application["SalesSession"].ToString()));
                LtrMessage.Text = Status == 1 ? "Payment Of Amount Rs." + TxtAmount.Text + " Received." : "Error Occurred.";
                if (Status == 1) Status = Checkers.ActivityNew("Payment Of Amount Rs." + TxtAmount.Text + " Received From " + DdlClientName.SelectedItem.Text, int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

                LblDate.Text = DateTime.Today.ToShortDateString();
                LblPaidTo.Text = "Quatro. Verna-Goa.";
                LblPaidBy.Text = DdlClientName.SelectedItem.Text;
                LblAmountNumber.Text = "Rs." + TxtAmount.Text + "/-";
                LblAmountWord.Text = "Rs." + Num.changeCurrencyToWords(double.Parse(TxtAmount.Text)).ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Print", "javascript:CallPrint('PrintReceipt');", true);
            }
            else
                LtrMessage.Text = "Please Select A Client.";
        }

        FillData();
    }

    public void FillData()
    {
        ClearForm();
        Checkers = new CheckersDataContext();
        decimal BillAmount = 0;

        LtrTotalOutstanding.Text = (from C in Checkers.Contacts
                                    where C.Contact_Status == 1 && C.Contact_Type == "Customer"
                                    select C.Contact_Credit.Value).Sum().ToString();

        if (HdnClientId.Value != "")
        {
            var Bills = Checkers.Invoices.Where(I => I.Invoice_Client == int.Parse(HdnClientId.Value)).Select(I => new { I.Invoice_Id, I.Invoice_Amount, I.Invoice_Discount });

            if (Enumerable.Count(Bills) > 0)
            {
                var Source = Checkers.Invoices.Where(I => I.Invoice_Client == int.Parse(HdnClientId.Value)).Select(I => I);
                foreach (var S in Source)
                {
                    BillAmount += (S.Invoice_Amount.Value - S.Invoice_Discount.Value);// -S.Invoice_Tax.Value;
                }
                LtrAmountPayable.Text = BillAmount.ToString();
            }

            var Receipts = Checkers.Receipts.Where(R => R.Receipt_Client == int.Parse(HdnClientId.Value) && R.Receipt_Status == 0).Select(R => new { R.Receipt_Id, R.Receipt_Amount, R.Receipt_PaymentMode });
            if (Enumerable.Count(Receipts) > 0)
            {
                LtrAmountPaid.Text = Checkers.Receipts.Where(R => R.Receipt_Client == int.Parse(HdnClientId.Value) && R.Receipt_Status == 0).Select(R => R.Receipt_Amount.Value).Sum().ToString();
                LtrReceiptsNumber.Text = "(Total : " + Enumerable.Count(Receipts).ToString() + ")";
                DgReceipts.DataSource = Receipts;
                DgReceipts.DataBind();
            }
            else
            {
                DgReceipts.DataSource = null;
                DgReceipts.DataBind();
            }

            if (Enumerable.Count(Bills) > 0)
            {
                LtrBillsNumber.Text = "(Total : " + Enumerable.Count(Bills).ToString() + ")";
                DgBills.DataSource = Bills;
                DgBills.DataBind();
            }
            else
            {
                DgBills.DataSource = null;
                DgBills.DataBind();
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
        FillData();
    }

    protected void BtnYes_Click(object sender, EventArgs e)
    {
        Checkers = new CheckersDataContext();
        int Status;

        Status = Checkers.ReceiptDelete(Id, int.Parse(HdnClientId.Value));
        LtrMessage.Text = Status == 1 ? "Receipt No. " + Id + " Successfully Deleted." : "Error Occurred.";
        if (Status == 1) Status = Checkers.ActivityNew("Receipt No. " + Id + " Successfully Deleted", int.Parse(Application["UserId"].ToString()), DateTime.Parse(Application["SalesSession"].ToString()));

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

    public void ClearForm()
    {
        LtrAmountPaid.Text = "0";
        LtrAmountPayable.Text = "0";
        LtrAmountRemaining.Text = "0";
        LtrBillsNumber.Text = "(Total : 0)";
        LtrReceiptsNumber.Text = "(Total : 0)";
        LtrTotalOutstanding.Text = "0";
    }

    protected void DgBills_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "View")
            Response.Redirect("Operation.aspx?Section=Invoice&Id=" + e.Item.Cells[0].Text);
    }
    protected void DgReceipts_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName == "View")
            Response.Redirect("Operation.aspx?Section=Receipt&Id=" + e.Item.Cells[0].Text);
    }
}
