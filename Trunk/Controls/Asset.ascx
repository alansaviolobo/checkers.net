<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Asset.ascx.cs" Inherits="Controls_Asset" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetAssetDetails(sender, eventArgs) {
        CheckersWebService.GetAssetDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnAssetId.ClientID %>').value = result['Id'];
        document.getElementById('<%=TxtName.ClientID %>').value = result['Name'];
        document.getElementById('<%=TxtQuantity.ClientID %>').value = result['Quantity'];
        document.getElementById('<%=TxtPurchaseDate.ClientID %>').value = result['PurchaseDate'];
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Asset Management" />
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
                    ServiceMethod="GetAssetList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetAssetDetails"
                    Enabled="false" />
                <asp:RequiredFieldValidator ID="ReqVldName" runat="server" ControlToValidate="TxtName"
                    Display="None" ErrorMessage="Please Enter The Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" Enabled="True"
                    TargetControlID="ReqVldName">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                Quantity
            </td>
            <td>
                <asp:TextBox ID="TxtQuantity" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Purchase Date
            </td>
            <td>
                <asp:TextBox ID="TxtPurchaseDate" runat="server" />
                <Ajax:CalendarExtender ID="TxtPurchaseDate_CalendarExtender" runat="server" 
                    Format="d/M/yyyy" Enabled="True" TargetControlID="TxtPurchaseDate">
                </Ajax:CalendarExtender>
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
        <strong># Asset Items :</strong>
        <asp:Literal ID="LtrAssetItems" runat="server" /><br />
        <br />
        <strong># Bar Items :</strong>
        <asp:Literal ID="LtrBarItems" runat="server" /><br />
        <br />
        <strong># Restaurant Items :</strong>
        <asp:Literal ID="LtrRestaurantItems" runat="server" /></p>
</div>
<asp:HiddenField ID="HdnAssetId" runat="server" />
