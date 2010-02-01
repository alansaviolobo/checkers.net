<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Credit.ascx.cs" Inherits="Controls_Credit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript">
    function CallPrint(strid) {
        var strOldOne = document.getElementById(strid);
        var WinPrint = window.open('', '', 'left=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
        WinPrint.document.write("<style rel=\"stylesheet\" type=\"text/css\">body * {margin-left:auto; margin-right:auto;font-family: Arial; font-size: 7px; letter-spacing: 3px; word-spacing: 5px; line-height: 200%;}</style>");
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
                <asp:DataGrid ID="DgBills" runat="server" AutoGenerateColumns="False" 
                    onitemcommand="DgBills_ItemCommand" >
                    <Columns>
                        <asp:BoundColumn DataField="Invoice_Id" HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoice_Amount" HeaderText="Amount"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Invoice_Discount" HeaderText="Discount"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="View" Text="View"></asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
            <td valign="top">
                <strong><u>Receipts</u></strong>&nbsp;
                <asp:Literal ID="LtrReceiptsNumber" runat="server" />
                <br />
                <br />
                <asp:DataGrid ID="DgReceipts" runat="server" AutoGenerateColumns="False" 
                    OnDeleteCommand="DgOrderItems_DeleteCommand" 
                    onitemcommand="DgReceipts_ItemCommand" >
                    <Columns>
                        <asp:BoundColumn DataField="Receipt_Id" HeaderText="No."></asp:BoundColumn>
                        <asp:BoundColumn DataField="Receipt_Amount" HeaderText="Amount"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Receipt_PaymentMode" HeaderText="Payment Mode"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="View" Text="View"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <div id="PrintReceipt" style="display: none; width: 396px">
        <br />
        <br />        
        <div id="BillLogo" style="margin-left: auto; margin-right: auto; text-align: center; width: 396px">
            <img src="Assets/Image/QuatroSmall.png" alt="" /><br />
            <br />
            Along NH-17 Verna By-Pass Road, Manjo Verna, Goa, 403722<br />
            Ph: 0832-2887406
            <br />
            <br />
            <br />
        </div>
        <span style="font-size: 9px; width: 396px; border-bottom: thin dotted black;">Receipt</span>
        <br />
        <br />
        <br />
        <br />
        <strong>Date : </strong>
        <asp:Label ID="LblDate" runat="server" /><br />
        <br />
        <br />
        <br />
        <strong>Paid To : </strong>
        <asp:Label ID="LblPaidTo" runat="server" /><br />
        <strong>Paid By : </strong>
        <asp:Label ID="LblPaidBy" runat="server" /><br />
        <br />
        <br />
        <br />
        <strong>Amount (In Numbers) : </strong>
        <asp:Label ID="LblAmountNumber" runat="server" /><br />
        <br />
        <strong>Amount (In Words) : </strong>
        <asp:Label ID="LblAmountWord" runat="server" /><br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <strong>Signature</strong><br />
        <span id="Ot" runat="server" />
    </div>
</div>
<asp:HiddenField ID="HdnClientId" runat="server" />
