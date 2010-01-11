<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DatabaseBackup.ascx.cs"
    Inherits="Controls_DatabaseBackup" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Database Backup." />&nbsp;&nbsp;
</div>
<table cellspacing="5" cellpadding="5">
    <tr>
        <td colspan="2">
            Please Click On The Backup Button To Create A Backup Of Database Checkers.
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnBackup" runat="server" Text="Backup" OnClick="BtnBackup_Click" />
        </td>
    </tr>
</table>
