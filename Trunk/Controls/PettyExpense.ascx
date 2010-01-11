<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PettyExpense.ascx.cs"
    Inherits="Controls_PettyExpense" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Petty Expense." />
</div>
<table cellpadding="5" cellspacing="5">
    <tr>
        <td>
            Amount
        </td>
        <td>
            <asp:TextBox ID="TxtAmount" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Merchandise
        </td>
        <td>
            <asp:TextBox ID="TxtMerchandise" runat="server" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td>
            Quantity
        </td>
        <td>
            <asp:TextBox ID="TxtQuantity" runat="server" />
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
            <asp:Button ID="BtnAdd" runat="server" Text="Add" onclick="BtnAdd_Click" />&nbsp;&nbsp;
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
