﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventoryList.ascx.cs" Inherits="Controls_InventoryList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetInventoryId(sender, eventArgs) {
        CheckersWebService.GetInventoryId(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnInventoryId.ClientID %>').value = result;
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Inventory Management" />
    &nbsp;&nbsp;
    <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
</div>
<div style="float: left; width: auto;">
    <table cellspacing="5" cellpadding="5">
        <tr>
            <td>
                Name
            </td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" />
                <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                    ServiceMethod="GetInventoryList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetInventoryId" />
                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:DataGrid ID="DgInventory" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False"
        OnDeleteCommand="DgInventory_DeleteCommand" 
        OnEditCommand="DgInventory_EditCommand" PageSize="15" 
        onpageindexchanged="DgInventory_PageIndexChanged" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal">
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle Mode="NumericPages" BackColor="#E7E7FF" ForeColor="#4A3C8C" 
            HorizontalAlign="Right" />
        <AlternatingItemStyle BackColor="#F7F7F7" />
        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <Columns>
            <asp:BoundColumn DataField="Inventory_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="Inventory_Name" HeaderText="Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Inventory_BuyingPrice" HeaderText="Price"></asp:BoundColumn>
            <asp:BoundColumn DataField="Inventory_Quantity" 
                HeaderText="Quantity"></asp:BoundColumn>   
            <asp:ButtonColumn CommandName="Delete" Text="Del"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Edit" Text="Edit"></asp:ButtonColumn>
        </Columns>
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    </asp:DataGrid>
</div>
<asp:HiddenField ID="HdnInventoryId" runat="server" />