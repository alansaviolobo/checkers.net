<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true"
    CodeFile="Report.aspx.cs" Inherits="Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="Content" runat="server" id="CntrlHolder">
        <div id="Message">
            <asp:Literal ID="LtrMessage" runat="server" Text="Menu Management" />
        </div>
        <table cellspacing="5" cellpadding="5">
            <tr>
                <td>
                    From Date
                </td>
                <td>
                    <asp:TextBox ID="TxtFromDate" runat="server" />
                    <asp:RequiredFieldValidator ID="ReqFromDate" runat="server" 
                        ControlToValidate="TxtFromDate" Display="None" 
                        ErrorMessage="Please Select A Date"></asp:RequiredFieldValidator>
                    <Ajax:ValidatorCalloutExtender ID="ReqFromDateExtender" runat="server" 
                        Enabled="True" TargetControlID="ReqFromDate">
                    </Ajax:ValidatorCalloutExtender>
                    <Ajax:CalendarExtender ID="TxtFromDateCalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="TxtFromDate" TodaysDateFormat="">
                    </Ajax:CalendarExtender>
                </td>
                <td>
                    To Date
                </td>
                <td>
                    <asp:TextBox ID="TxtToDate" runat="server" />
                    <asp:RequiredFieldValidator ID="ReqToDate" runat="server" 
                        ControlToValidate="TxtToDate" Display="None" 
                        ErrorMessage="Please Select A Date"></asp:RequiredFieldValidator>
                    <Ajax:ValidatorCalloutExtender ID="ReqToDateExtender" runat="server" 
                        Enabled="True" TargetControlID="ReqToDate">
                    </Ajax:ValidatorCalloutExtender>
                    <Ajax:CalendarExtender ID="TxtToDateCalendarExtender" runat="server" Enabled="True"
                        Format="dd/MM/yyyy" TargetControlID="TxtToDate" TodaysDateFormat="">
                    </Ajax:CalendarExtender>
                </td>
            </tr>
        </table>
        <br />
        <Ajax:TabContainer ID="TabPnlReports" runat="server" ActiveTabIndex="0">
            <Ajax:TabPanel runat="server" HeaderText="General" ID="TabGeneral">
                <ContentTemplate>
                    <asp:Button ID="BtnPettyCash" runat="server" Text="Petty Cash" OnClick="BtnPettyCash_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnPettyExpense" runat="server" Text="Petty Expense" OnClick="BtnPettyExpense_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnPurchaseTotal" runat="server" Text="Purchase Total" OnClick="BtnPurchaseTotal_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnSalesTotal" runat="server" Text="Sales Total" OnClick="BtnSalesTotal_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="BtnReceipts" runat="server" Text="Receipts" OnClick="BtnReceipts_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCancelledTokens" runat="server" Text="Cancelled Tokens" OnClick="BtnCancelledTokens_Click" />
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel runat="server" HeaderText="Item" ID="TabItem">
                <ContentTemplate>
                    Select Item :
                    <asp:DropDownList ID="DdlMenu" runat="server" />
                    &nbsp;&nbsp;
                    <asp:Button ID="BtnSalesItem" runat="server" Text="Sales" OnClick="BtnSalesItem_Click" />
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel runat="server" HeaderText="Category" ID="TabCategory">
                <ContentTemplate>
                    Select Category :
                    <asp:DropDownList ID="DdlCategory" runat="server">
                        <asp:ListItem>Bar</asp:ListItem>
                        <asp:ListItem>Restaurant</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:Button ID="BtnSalesCategory" runat="server" Text="Sales" OnClick="BtnSalesCategory_Click" />
                </ContentTemplate>
            </Ajax:TabPanel>
            <Ajax:TabPanel runat="server" HeaderText="Payment Mode" ID="TabPaymentMode">
                <ContentTemplate>
                    <asp:Button ID="BtnCreditCard" runat="server" Text="Credit Card" 
                        OnClick="BtnCreditCard_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCredit" runat="server" Text="Credit" 
                        OnClick="BtnCredit_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCash" runat="server" Text="Cash" OnClick="BtnCash_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnCheque" runat="server" Text="Cheque" 
                        OnClick="BtnCheque_Click" />&nbsp;&nbsp;
                    <asp:Button ID="BtnZeroBilling" runat="server" Text="Zero Billing" 
                        OnClick="BtnZeroBilling_Click" />
                </ContentTemplate>
            </Ajax:TabPanel>
        </Ajax:TabContainer>
    </div>
</asp:Content>
