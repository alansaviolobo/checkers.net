<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Credit.ascx.cs" Inherits="Controls_Credit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript">
    function CallPrint(strid) {
        var strOldOne = document.getElementById(strid);
        var WinPrint = window.open('', '', 'left=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
        WinPrint.document.write("<style rel=\"stylesheet\" type=\"text/css\">body * {margin-right: auto; margin-left: auto; font-family: calibri; font-size: 14px; }</style>");
        WinPrint.document.write(strOldOne.innerHTML);
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Credit Management" />
    &nbsp;&nbsp;
    <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
</div>
<div>
    <table cellspacing="5" cellpadding="5" width="100%">
        <tr>
            <td valign="middle">
                Client Name :
                <asp:DropDownList ID="DdlClientName" runat="server" />
                <asp:Button ID="BtnSelect" runat="server" Text="Select" OnClick="BtnSelect_Click" />
            </td>
            <td>
                <strong>Total Outstanding Balance (Rs.): </strong>
                <asp:Literal ID="LtrTotalOutstanding" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top">
                <p>
                    <strong><u>Amount Summary</u></strong>
                    <br />
                    <br />
                    Payable (Rs.):
                    <asp:Literal ID="LtrAmountPayable" runat="server" Text="0" />
                    <br />
                    Paid (Rs.):
                    <asp:Literal ID="LtrAmountPaid" runat="server" Text="0" />
                    <br />
                    Remaining (Rs.):
                    <asp:Literal ID="LtrAmountRemaining" runat="server" Text="0" />
                </p>
            </td>
            <td valign="top" style="border: 2px dotted black">
                <p>
                    <strong><u>Pay Amount</u></strong>
                    <br />
                    <br />
                    Amount (Rs.):
                    <asp:TextBox ID="TxtAmount" runat="server" Text="0" />
                    <%--<asp:RegularExpressionValidator ID="RegVldAmount" runat="server" ErrorMessage="Please Enter Correct Number."
                        ControlToValidate="TxtAmount" Display="None" ValidationExpression="\d"></asp:RegularExpressionValidator>
                    <Ajax:ValidatorCalloutExtender ID="RegVldAmountExtender" runat="server" Enabled="True"
                        TargetControlID="RegVldAmount">
                    </Ajax:ValidatorCalloutExtender>--%>
                    <br />
                    Pay By:
                    <asp:DropDownList ID="DdlPayBy" runat="server">
                        <asp:ListItem>Cash</asp:ListItem>
                        <asp:ListItem>Credit Card</asp:ListItem>
                        <asp:ListItem>Cheque</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="BtnPayAmount" Text="Pay" runat="server" OnClick="BtnPayAmount_Click" />
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top">
                <strong><u>Bills</u></strong>&nbsp;
                <asp:Literal ID="LtrBillsNumber" runat="server" />
                <br />
                <br />
                <asp:DataGrid ID="DgBills" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundColumn DataField="Invoice_Id" HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoice_Amount" HeaderText="Amount"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoice_Tax" HeaderText="Tax"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoice_Discount" HeaderText="Discount"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
            <td valign="top">
                <strong><u>Receipts</u></strong>&nbsp;
                <asp:Literal ID="LtrReceiptsNumber" runat="server" />
                <br />
                <br />
                <asp:DataGrid ID="DgReceipts" runat="server" AutoGenerateColumns="False" OnDeleteCommand="DgOrderItems_DeleteCommand">
                    <Columns>
                        <asp:BoundColumn DataField="Invoice_Id" HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoice_Amount" HeaderText="Amount"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoice_PaymentMode" HeaderText="Payment Mode"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <div id="PrintReceipt" style="display: none;">
        <span style="font-size:large;">Receipt</span>
        <hr />
        <strong>Date : </strong>
        <asp:Label ID="LblDate" runat="server" Font-Size="Smaller" /><br />
        <br />
        <strong>Paid To : </strong>
        <asp:Label ID="LblPaidTo" runat="server" Font-Size="Smaller" /><br />
        <strong>Paid By : </strong>
        <asp:Label ID="LblPaidBy" runat="server" Font-Size="Smaller" /><br />
        <br />
        <strong>Amount (In Numbers) : </strong>
        <br />
        <asp:Label ID="LblAmountNumber" runat="server" Font-Size="Smaller" /><br />
        <br />
        <strong>Amount (In Words) : </strong>
        <br />
        <asp:Label ID="LblAmountWord" runat="server" Font-Size="Smaller" /><br />
        <br />
        <br />
        <br />
        <br />
        <strong>Signature</strong><br />
        <span id="Ot" runat="server" />
    </div>
</div>
<asp:HiddenField ID="HdnClientId" runat="server" />
