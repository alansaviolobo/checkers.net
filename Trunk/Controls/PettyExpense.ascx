<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PettyExpense.ascx.cs"
    Inherits="Controls_PettyExpense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Petty Expense." />
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
            Amount
        </td>
        <td>
            <asp:TextBox ID="TxtAmount" runat="server" />
            <asp:RequiredFieldValidator ID="ReqVldAmount" runat="server" ControlToValidate="TxtAmount"
                Display="None" ErrorMessage="Please Enter Amount"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqVldAmountExtender" runat="server" 
                Enabled="True" TargetControlID="ReqVldAmount">
            </Ajax:ValidatorCalloutExtender>
            <asp:RegularExpressionValidator ID="RegVldAmount" runat="server" ControlToValidate="TxtAmount"
                Display="None" ErrorMessage="Please Enter A Valid Number" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            <Ajax:ValidatorCalloutExtender ID="RegVldAmountExtender" runat="server" 
                Enabled="True" TargetControlID="RegVldAmount">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            Narration
        </td>
        <td>
            <asp:TextBox ID="TxtNarration" runat="server" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td>
            Quantity
        </td>
        <td>
            <asp:TextBox ID="TxtQuantity" runat="server" Text="0" />
            <asp:RequiredFieldValidator ID="ReqVldQuantity" runat="server" ControlToValidate="TxtQuantity"
                Display="None" ErrorMessage="Please Enter Amount"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqVldQuantityExtender" runat="server" 
                Enabled="True" TargetControlID="ReqVldQuantity">
            </Ajax:ValidatorCalloutExtender>
            <asp:RegularExpressionValidator ID="RegVldQuantity" runat="server" ControlToValidate="TxtQuantity"
                Display="None" ErrorMessage="Please Enter A Valid Number" 
                ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
            <Ajax:ValidatorCalloutExtender ID="RegVldQuantityExtender" runat="server" 
                Enabled="True" TargetControlID="RegVldQuantity">
            </Ajax:ValidatorCalloutExtender>
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
            <asp:Button ID="BtnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" />&nbsp;&nbsp;
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
