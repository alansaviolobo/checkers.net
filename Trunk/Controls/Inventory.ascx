<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Inventory.ascx.cs" Inherits="Controls_Inventory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetInventoryDetails(sender, eventArgs) {
        CheckersWebService.GetInventoryDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=DdlPurchaseUnit.ClientID %>').value = result['PurchaseUnit'];
        document.getElementById('<%=TxtBuyingPrice.ClientID %>').value = result['BuyingPrice'];
        document.getElementById('<%=TxtThreshold.ClientID %>').value = result['Threshold'];
        document.getElementById('<%=TxtConversionUnit.ClientID %>').value = result['ConversionUnit'];
        document.getElementById('<%=LblPurchaseUnit1.ClientID %>').value = result['PurchaseUnit'];
        document.getElementById('<%=LblPurchaseUnit2.ClientID %>').value = result['PurchaseUnit'];
        document.getElementById('<%=LblPurchaseUnit3.ClientID %>').value = result['PurchaseUnit'];
        document.getElementById('<%=LblName.ClientID %>').value = result['Name'];
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
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetInventoryDetails"
                    Enabled="false" />
                <asp:RequiredFieldValidator ID="ReqVldName" runat="server" ControlToValidate="TxtName"
                    Display="None" ErrorMessage="Please Enter A Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" Enabled="True"
                    TargetControlID="ReqVldName">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                Purchase Unit
            </td>
            <td>
                <asp:DropDownList ID="DdlPurchaseUnit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlPurchaseUnit_SelectedIndexChanged">
                    <asp:ListItem Value="Kg">Kg</asp:ListItem>
                    <asp:ListItem Value="Bottle">Bottle</asp:ListItem>
                    <asp:ListItem Value="Unit">Unit</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Buying Price (Rs.)
            </td>
            <td>
                <asp:TextBox ID="TxtBuyingPrice" runat="server" Width="80px" />
                &nbsp;
                <asp:Label ID="LblPurchaseUnit1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Threshold
            </td>
            <td>
                <asp:TextBox ID="TxtThreshold" runat="server" Width="80px" />
                &nbsp;
                <asp:Label ID="LblPurchaseUnit2" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <b>1</b>
                <asp:Label ID="LblPurchaseUnit3" runat="server" />
                &nbsp;<b>Of</b>&nbsp;
                <asp:Label ID="LblName" runat="server" />
                &nbsp;<b>Corresponds To</b>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:TextBox ID="TxtConversionUnit" runat="server" Width="80px" Height="22px" />
                <asp:RegularExpressionValidator ID="RegVldConversionUnit" runat="server" ControlToValidate="TxtConversionUnit"
                    Display="None" ValidationExpression="^\d{1,10}(\.\d{0,2})?$" ErrorMessage="Please Enter A Valid Amount"></asp:RegularExpressionValidator>
                <Ajax:ValidatorCalloutExtender ID="RegVldConversionUnitExtender" runat="server" Enabled="True"
                    TargetControlID="RegVldConversionUnit">
                </Ajax:ValidatorCalloutExtender>
                &nbsp; Units
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" />
            </td>
        </tr>
    </table>
</div>
<div style="float: left; width: auto; margin-left: 20%; margin-top: 2%;">
    <p>
        <strong># Inventory Items :</strong>
        <asp:Literal ID="LtrInventoryItems" runat="server" /></p>
</div>
<asp:HiddenField ID="HdnInventoryId" runat="server" />
