<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Inventory.ascx.cs" Inherits="Controls_Inventory" %>
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
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetInventoryId"
                    Enabled="false" />
                <asp:RequiredFieldValidator ID="ReqVldName" runat="server" 
                    ControlToValidate="TxtName" Display="None" ErrorMessage="Please Enter A Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" 
                    Enabled="True" TargetControlID="ReqVldName">
                </Ajax:ValidatorCalloutExtender>
                <asp:Button ID="BtnSearch" runat="server" Text="Search" Visible="False" OnClick="BtnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Purchase Unit
            </td>
            <td>
                <asp:DropDownList ID="DdlPurchaseUnit" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DdlPurchaseUnit_SelectedIndexChanged">
                    <asp:ListItem>Kg</asp:ListItem>
                    <asp:ListItem>Bottle</asp:ListItem>
                    <asp:ListItem>Unit</asp:ListItem>
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
                <asp:Literal ID="LtrPurchaseUnit1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Threshold
            </td>
            <td>
                <asp:TextBox ID="TxtThreshold" runat="server" Width="80px" />
                &nbsp;
                <asp:Literal ID="LtrPurchaseUnit2" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <b>1</b>
                <asp:Literal ID="LtrPurchaseUnit3" runat="server"></asp:Literal>
                &nbsp;<b>Of</b>&nbsp;
                <asp:Literal ID="LtrName" runat="server"></asp:Literal>
                &nbsp;<b>Corresponds To</b>
            </td>
        </tr>
        <tr>
            <td>
               
            </td>
            <td>
                <asp:TextBox ID="TxtConversionUnit" runat="server" Width="80px" Height="22px" />
                <asp:RegularExpressionValidator ID="RegVldConversionUnit" runat="server" 
                    ControlToValidate="TxtConversionUnit" Display="None" ValidationExpression="^\d{1,10}(\.\d{0,2})?$" 
                    ErrorMessage="Please Enter A Valid Amount"></asp:RegularExpressionValidator>
                <Ajax:ValidatorCalloutExtender ID="RegVldConversionUnitExtender" runat="server" 
                    Enabled="True" TargetControlID="RegVldConversionUnit">
                </Ajax:ValidatorCalloutExtender>
                &nbsp;
                Units
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
