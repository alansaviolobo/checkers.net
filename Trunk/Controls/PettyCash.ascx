<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PettyCash.ascx.cs" Inherits="Controls_PettyCash" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Petty Cash" />
</div>
<table cellpadding="5" cellspacing="5">
    <tr>
        <td>
            Available Amount (Rs.)
        </td>
        <td>
            <asp:Literal ID="LtrAvailableAmount" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Amount (Rs.)
        </td>
        <td>
            <asp:TextBox ID="TxtAmount" runat="server" />
            <asp:RequiredFieldValidator ID="ReqVldAmount" runat="server" ControlToValidate="TxtAmount"
                Display="None" ErrorMessage="Please Enter A Value"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqVldAmountExtender" runat="server" Enabled="True"
                TargetControlID="ReqVldAmount">
            </Ajax:ValidatorCalloutExtender>
            <asp:RegularExpressionValidator ID="RegVldAmount" runat="server" ControlToValidate="TxtAmount"
                Display="None" ErrorMessage="Please Enter A Number" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            <Ajax:ValidatorCalloutExtender ID="RegVldAmountExtender" runat="server" Enabled="True"
                TargetControlID="RegVldAmount">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            Given By
        </td>
        <td>
            <asp:DropDownList ID="DdlGivenBy" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Received By
        </td>
        <td>
            <asp:Literal ID="LtrReceivedBy" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnAdd" runat="server" OnClick="BtnAdd_Click" Text="Add" />&nbsp;&nbsp;
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
