<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OfferList.ascx.cs" Inherits="Controls_OfferList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetOfferId(sender, eventArgs) {
        CheckersWebService.GetOfferId(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnOfferName.ClientID %>').value = result;
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Offer Management" />
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
                    ServiceMethod="GetOfferList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetOfferId" />
                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:DataGrid ID="DgOffer" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
        OnDeleteCommand="DgOffer_DeleteCommand" OnEditCommand="DgOffer_EditCommand" PageSize="15"
        OnPageIndexChanged="DgOffer_PageIndexChanged">
        <PagerStyle Mode="NumericPages" />
        <Columns>
            <asp:BoundColumn DataField="Offer_Name" HeaderText="Name"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="Delete" Text="Del"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Edit" Text="Edit"></asp:ButtonColumn>
        </Columns>
    </asp:DataGrid>
</div>
<asp:HiddenField ID="HdnOfferName" runat="server" />