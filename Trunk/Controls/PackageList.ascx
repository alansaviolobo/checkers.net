<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PackageList.ascx.cs" Inherits="Controls_PackageList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetPackageId(sender, eventArgs) {
        CheckersWebService.GetPackageId(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnPackageId.ClientID %>').value = result;
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Package Management." />
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
                    ServiceMethod="GetPackageList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetPackageId"
                    Enabled="false" />
                <asp:Button ID="BtnSearch" Text="Search" runat="server" OnClick="BtnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:DataGrid ID="DgPackage" runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" OnDeleteCommand="DgPackage_DeleteCommand" OnEditCommand="DgPackage_EditCommand"
        PageSize="15" onpageindexchanged="DgPackage_PageIndexChanged" 
        onitemcommand="DgPackage_ItemCommand" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal">
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle Mode="NumericPages" BackColor="#E7E7FF" ForeColor="#4A3C8C" 
            HorizontalAlign="Right" />
        <AlternatingItemStyle BackColor="#F7F7F7" />
        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <Columns>
            <asp:BoundColumn DataField="Package_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="Package_Name" HeaderText="Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Package_Type" HeaderText="Type"></asp:BoundColumn>
            <asp:BoundColumn DataField="Package_Cost" HeaderText="Cost"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="Delete" Text="Del"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Edit" Text="Edit"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Content" Text="Content"></asp:ButtonColumn>
        </Columns>
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    </asp:DataGrid>
</div>
<asp:HiddenField ID="HdnPackageId" runat="server" />
