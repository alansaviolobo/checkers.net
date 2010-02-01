<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true"
    CodeFile="Database.aspx.cs" Inherits="Database" EnableViewState="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="Content" runat="server" id="CntrlHolder">
        <div id="Message">
            <asp:Literal ID="LtrMessage" runat="server" Text="Database Operation." />&nbsp;&nbsp;
            <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
            &nbsp;
            <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
        </div>
        <table cellspacing="5" cellpadding="5">
            <tr>
                <td colspan="2">
                    <strong>Database Backup</strong>
                </td>
            </tr>
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
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <strong>Database Restore</strong>
                </td>
            </tr>
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
    </div>
</asp:Content>
