<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PettyCash.ascx.cs" Inherits="Controls_PettyCash" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message"> 
    <asp:Literal ID="LtrMessage" runat="server" Text="Petty Cash" />
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
