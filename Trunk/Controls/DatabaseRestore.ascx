<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseRestore.ascx.cs"
    Inherits="Controls_DatabaseRestore" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Database Restore." />&nbsp;&nbsp;
    <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
</div>
<table cellpadding="5" cellspacing="5">
    <tr>
        <td>
            Please Select The Backup File
        </td>
        <td>
            <asp:FileUpload ID="FuBackup" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnRestore" runat="server" Text="Restore" OnClick="BtnRestore_Click" />
        </td>
    </tr>
</table>
