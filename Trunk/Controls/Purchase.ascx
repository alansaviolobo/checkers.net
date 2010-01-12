<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchase.ascx.cs" Inherits="Controls_Purchase" %>
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
    <asp:Literal ID="LtrMessage" runat="server" Text="Inventory Items Purchase." />
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
        <tr>
            <td>
                Existing Quantity
            </td>
            <td>
                <asp:TextBox ID="TxtExistingQuantity" runat="server" ReadOnly="true" />
            </td>
        </tr>
        <tr>
            <td>
                Purchase Quantity
            </td>
            <td>
                <asp:TextBox ID="TxtPurchaseQuantity" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="BtnPurchase" runat="server" OnClick="BtnPurchase_Click" Text="Purchase" />
            </td>
        </tr>
    </table>
</div>
<div style="float: left; width: auto; margin-left: 20%; margin-top: 2%;">
    <p>
    </p>
</div>
<asp:HiddenField ID="HdnInventoryId" runat="server" />
