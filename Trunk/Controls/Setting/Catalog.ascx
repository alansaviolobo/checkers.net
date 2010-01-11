<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Catalog.ascx.cs" Inherits="Controls_Setting_Catalog" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" />
</div>
<div id="Search">
    Search : <asp:TextBox ID="TxtSearch" runat="server" />
</div>
<table cellpadding="5" cellspacing="5">
    <tr>
        <td>
            Name
        </td>
        <td>
            <asp:TextBox ID="TxtName" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Type
        </td>
        <td>
            <asp:TextBox ID="TxtType" runat="server" />
        </td>
    </tr>    
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnSubmit" runat="server" />&nbsp;&nbsp;
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
