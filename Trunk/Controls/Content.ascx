<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Content.ascx.cs" Inherits="Controls_Content" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Content Management" />
</div>
<div style="float: left; width: auto;">
    <table cellspacing="5" cellpadding="5">
        <tr>
            <td>
                Package Name
            </td>
            <td>
                <asp:Literal ID="LtrPackageName" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Item Name
            </td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" />
                <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                    ServiceMethod="GetContentList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetContentId"
                    Enabled="false" />
                <asp:RequiredFieldValidator ID="ReqVldName" runat="server" ControlToValidate="TxtName"
                    Display="None" ErrorMessage="Please Enter The Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" Enabled="True"
                    TargetControlID="ReqVldName">
                </Ajax:ValidatorCalloutExtender>
                <asp:Button ID="BtnSearch" runat="server" Text="Search" Visible="False" OnClick="BtnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Quantity
            </td>
            <td>
                <asp:DropDownList ID="DdlCategory" runat="server">
                    <asp:ListItem>Restaurant</asp:ListItem>
                    <asp:ListItem>Bar</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Unit Price (Rs.)
            </td>
            <td>
                <asp:Literal ID="LtrUnitPrice" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Unit Price (Rs.)
            </td>
            <td>
                <asp:TextBox ID="TxtNewUnitPrice" runat="server" Width="80px" />
                <asp:RegularExpressionValidator ID="RegVldNewUnitPrice" runat="server" ControlToValidate="TxtNewUnitPrice"
                    Display="None" ErrorMessage="Please Enter A Number" ValidationExpression="^\d{1,10}(\.\d{0,2})?$"></asp:RegularExpressionValidator>
                <Ajax:ValidatorCalloutExtender ID="RegVldPriceExtender" runat="server" Enabled="True"
                    TargetControlID="RegVldNewUnitPrice">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="BtnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" />
            </td>
        </tr>
    </table>
</div>
<div style="float: left; width: auto; margin-left: 20%;">
    <p>
        <asp:DataGrid ID="DgContent" runat="server" AutoGenerateColumns="False" OnDeleteCommand="DgOrderItems_DeleteCommand">
            <Columns>
                <asp:BoundColumn DataField="Content_Id" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="Content_Menu" HeaderText="Menu"></asp:BoundColumn>
                <asp:BoundColumn DataField="Receipt_PaymentMode" HeaderText="Payment Mode"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></p>
</div>
