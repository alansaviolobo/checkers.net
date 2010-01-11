<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Report.ascx.cs" Inherits="Controls_Report" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Menu Management" />
</div>
<table cellspacing="5" cellpadding="5">
    <tr>
        <td>
            Report Type
        </td>
        <td>
            <asp:DropDownList ID="DdlReportType" runat="server">
                <asp:ListItem>Sales</asp:ListItem>
                <asp:ListItem>Purchase</asp:ListItem>
                <asp:ListItem>PettyCash</asp:ListItem>
                <asp:ListItem>PettyExpense</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            From Date
        </td>
        <td>
            <asp:TextBox ID="TxtFromDate" runat="server" />
            <Ajax:CalendarExtender ID="TxtFromDateCalendarExtender" runat="server" Enabled="True"
                Format="d/M/yyyy" TargetControlID="TxtFromDate" TodaysDateFormat="">
            </Ajax:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td>
            To Date
        </td>
        <td>
            <asp:TextBox ID="TxtToDate" runat="server" />
            <Ajax:CalendarExtender ID="TxtToDateCalendarExtender" runat="server" Enabled="True"
                Format="d/M/yyyy" TargetControlID="TxtToDate" TodaysDateFormat="">
            </Ajax:CalendarExtender>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="text-align: center">
            <asp:Button ID="BtnCreateReport" runat="server" Text="Create" OnClick="BtnCreate_Click" />
        </td>
    </tr>
</table>
