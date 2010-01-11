﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Converter.ascx.cs" Inherits="Controls_Converter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function DisplayMenuDetails(sender, eventArgs) {
        CheckersWebService.GetMenuDetails(sender._element.value, OnMenuSuccess, OnError);
    }

    function DisplayInventoryDetails(sender, eventArgs) {
        CheckersWebService.GetInventoryDetails(sender._element.value, OnInventorySuccess, OnError);
    }

    function OnMenuSuccess(result) {
        document.getElementById('<%=HdnMenuId.ClientID %>').value = result;
    }

    function OnInventorySuccess(result) {
        document.getElementById('<%=HdnInventoryId.ClientID %>').value = result;
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Item Inventory Management." />
    &nbsp;&nbsp;
    <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
</div>
<div style="float: left; width: auto;">
    <table cellspacing="5" cellpadding="5">
        <tr>
            <td>
                Menu Name
            </td>
            <td>
                <asp:TextBox ID="TxtMenuName" runat="server" />
                <Ajax:AutoCompleteExtender ID="AutoCompMenuSearch" runat="server" TargetControlID="TxtMenuName"
                    ServiceMethod="GetMenuList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="DisplayMenuDetails" />
                <asp:Button ID="BtnSearch" runat="server" Text="Search" Visible="True" OnClick="BtnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Ingredient Name
            </td>
            <td>
                <asp:TextBox ID="TxtInventoryName" runat="server" />
                <Ajax:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="TxtInventoryName"
                    ServiceMethod="GetInventoryList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="DisplayInventoryDetails" />
            </td>
        </tr>
        <tr>
            <td>
                Quantity
            </td>
            <td>
                <asp:TextBox ID="TxtInventoryQuantity" runat="server" />
                <asp:Button ID="BtnEnter" Text="Enter" runat="server" OnClick="BtnEnter_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="BtnDelete" Text="Delete" runat="server" OnClick="BtnDelete_Click"
                    Visible="false" />
            </td>
        </tr>
    </table>
</div>
<div style="float: left; width: auto; margin-left: 15%;">
    <p>
        <b>1 Unit&nbsp;<asp:Literal ID="LtrMenuName" runat="server" />&nbsp;Contains :</b><br />
        <br />
        <asp:DataGrid ID="DgConverterList" runat="server" AutoGenerateColumns="False" OnDeleteCommand="DgConverterList_DeleteCommand"
            Visible="False">
            <Columns>
                <asp:BoundColumn DataField="Converter_Id" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="Inventory_Name" HeaderText="Ingredient"></asp:BoundColumn>
                <asp:BoundColumn DataField="Converter_InventoryQuantity" HeaderText="Quantity"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></p>
</div>
<asp:HiddenField ID="HdnConverterId" runat="server" />
<asp:HiddenField ID="HdnMenuId" runat="server" />
<asp:HiddenField ID="HdnInventoryId" runat="server" />