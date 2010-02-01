<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuList.ascx.cs" Inherits="Controls_MenuList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetMenuId(sender, eventArgs) {
        CheckersWebService.GetMenuId(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnMenuId.ClientID %>').value = result;
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Menu Management" />
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
                    ServiceMethod="GetMenuList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetMenuId" />
                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:DataGrid ID="DgMenu" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        OnDeleteCommand="DgMenu_DeleteCommand" OnEditCommand="DgMenu_EditCommand" PageSize="15"
        OnPageIndexChanged="DgMenu_PageIndexChanged">
        <PagerStyle Mode="NumericPages" />
        <Columns>
            <asp:BoundColumn DataField="Menu_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="Menu_Name" HeaderText="Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Menu_Category" HeaderText="Category"></asp:BoundColumn>
            <asp:BoundColumn DataField="Menu_SellingPrice" HeaderText="Price"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="Delete" Text="Del"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Edit" Text="Edit"></asp:ButtonColumn>
        </Columns>
    </asp:DataGrid>
</div>
<asp:HiddenField ID="HdnMenuId" runat="server" />
