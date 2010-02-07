<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Receipt.ascx.cs" Inherits="Controls_Receipt" %>
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
    <asp:Literal ID="LtrMessage" runat="server" Text="Receipt Management" />
</div>
<table cellspacing="5" cellpadding="5">
    <tr>
        <td>
            Receipt Number
        </td>
        <td>
            <asp:TextBox ID="TxtReceiptNumber" runat="server" />
            <asp:RequiredFieldValidator ID="ReqVldNumber" runat="server" Display="None" 
                ErrorMessage="Please Enter A Receipt Number"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqVldNumberExtender" runat="server" 
                Enabled="True" TargetControlID="ReqVldNumber">
            </Ajax:ValidatorCalloutExtender>
            &nbsp;&nbsp;
            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
        </td>
        <td>
            <input type="submit" onclick="javascript:CallPrint('PrintReceipt');" value="Print" />
        </td>
    </tr>
</table>
<div id="PrintReceipt" style="width: 396px">
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
