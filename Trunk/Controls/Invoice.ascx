<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Invoice.ascx.cs" Inherits="Controls_Invoice" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<script type="text/javascript">
    function CallPrint(strid) {
        var strOldOne = document.getElementById(strid);
        var WinPrint = window.open('', '', 'left=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
        WinPrint.document.write("<link href=\"Assets/Style/Order.css\" rel=\"stylesheet\" type=\"text/css\" />");
        WinPrint.document.write("<style rel=\"stylesheet\" type=\"text/css\">body * {margin-left:auto; margin-right:auto;font-family: Arial; font-size: 5px; letter-spacing: 3px; word-spacing: 5px; line-height: 200%;}</style>");
        WinPrint.document.write(strOldOne.innerHTML);
        WinPrint.document.close();
        WinPrint.focus();
        WinPrint.print();
        WinPrint.close();
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Invoice Management" />
</div>
<table cellspacing="5" cellpadding="5">
    <tr>
        <td>
            Bill Number
        </td>
        <td>
            <asp:TextBox ID="TxtBillNumber" runat="server" />
            <asp:RequiredFieldValidator ID="ReqVldNumber" runat="server" Display="None" 
                ErrorMessage="Please Enter A Bill Number" 
                ControlToValidate="TxtBillNumber"></asp:RequiredFieldValidator>
            <cc1:ValidatorCalloutExtender ID="ReqVldNumberExtender" runat="server" 
                Enabled="True" TargetControlID="ReqVldNumber">
            </cc1:ValidatorCalloutExtender>
            &nbsp;&nbsp;
            <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
        </td>
        <td>
        <input type="submit" onclick="javascript:CallPrint('PrintBill');" value="Print" />
        </td>
    </tr>
</table>
<div id="PrintBill" style="display: inherit">
    <br />
    <br />
    <br />
    <br />
    <div class="FullWidth">
        <div class="Division3">
            TIN: 30241108428</div>
        <div class="Division3">
            CASH / CREDIT MEMO</div>
        <div class="Division3">
            Bill No.&nbsp;<asp:Literal ID="LtrBillNo" runat="server" />
        </div>
    </div>
    <br />
    <br />
    <div id="BillLogo">
        <img src="Assets/Image/QuatroSmall.png" alt="" /><br />
        <br />
        Along NH-17 Verna By-Pass Road, Manjo Verna, Goa, 403722<br />
        Ph: 0832-2887406
        <br />
        <br />
        <br />
        <br />
    </div>
    <div class="FullWidth">
        <div class="Division4">
            <b>Date</b><br />
            <asp:Literal ID="LtrDate" runat="server" />
        </div>
        <div class="Division4">
            <b>Time</b><br />
            <asp:Literal ID="LtrTime" runat="server" />
        </div>
        <div class="Division4">
            <b>Table No.</b><br />
            <asp:Literal ID="LtrTableNo" runat="server" />
        </div>
        <div class="Division4">
            <b>Steward</b><br />
            <asp:Literal ID="LtrSteward" runat="server" />
        </div>
    </div>
    <br />
    <br />
    <br />
    <br />
    <table id="TblBill" runat="server" class="FullWidth" style="margin-left: auto; margin-right: auto;">
        <thead>
            <tr>
                <td style="width: 50%">
                    <strong>Item</strong>
                </td>
                <td>
                    <strong>Qnty</strong>
                </td>
                <td>
                    <strong>Per Unit</strong>
                </td>
                <td>
                    <strong>Cost</strong>
                </td>
            </tr>
        </thead>
    </table>
    <br />
    <br />
    <div class="FullWidth" style="text-align: center">
        Prices inclusive of all the taxes.
    </div>
    <br />
    <br />
</div>
