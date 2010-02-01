<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Purchase.ascx.cs" Inherits="Controls_Purchase" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetInventoryDetails(sender, eventArgs) {
        CheckersWebService.GetInventoryDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnInventoryId.ClientID %>').value = result['Id'];
        document.getElementById('<%=TxtExistingQuantity.ClientID %>').value = result['Quantity'] / result['ConversionUnit'];
        document.getElementById('<%=LblPurchaseUnit2.ClientID %>').value = result['PurchaseUnit'] + "(s)";
        document.getElementById('<%=LblPurchaseUnit2.ClientID %>').value = result['PurchaseUnit'] + "(s)";
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
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetInventoryDetails" />
                <asp:RequiredFieldValidator ID="ReqVldName" runat="server" 
                    ControlToValidate="TxtName" Display="None" ErrorMessage="Please Enter A Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" 
                    Enabled="True" TargetControlID="ReqVldName">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                Existing Quantity
            </td>
            <td>
                <asp:TextBox ID="TxtExistingQuantity" runat="server" ReadOnly="true" />
                &nbsp;
                <asp:Label ID="LblPurchaseUnit1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Purchase Quantity
            </td>
            <td>
                <asp:TextBox ID="TxtPurchaseQuantity" runat="server" />
                &nbsp;
                <asp:Label ID="LblPurchaseUnit2" runat="server" />
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
