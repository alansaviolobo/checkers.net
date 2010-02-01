<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Converter.ascx.cs" Inherits="Controls_Converter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetMenuDetails(sender, eventArgs) {
        CheckersWebService.GetMenuDetails(sender._element.value, OnMenuSuccess, OnError);
    }

    function GetInventoryDetails(sender, eventArgs) {
        CheckersWebService.GetInventoryDetails(sender._element.value, OnInventorySuccess, OnError);
    }

    function OnMenuSuccess(result) {
        document.getElementById('<%=HdnMenuId.ClientID %>').value = result['Id'];
    }

    function OnInventorySuccess(result) {
        document.getElementById('<%=HdnInventoryId.ClientID %>').value = result['Id'];
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
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetMenuDetails" />
                <asp:RequiredFieldValidator ID="ReqVldMenuName" runat="server" ControlToValidate="TxtMenuName"
                    Display="None" ErrorMessage="Please Enter A Menu Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldMenuNameExtender" runat="server" Enabled="True"
                    TargetControlID="ReqVldMenuName">
                </Ajax:ValidatorCalloutExtender>
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
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetInventoryDetails" />
                <asp:RequiredFieldValidator ID="ReqVldInventoryName" runat="server" ControlToValidate="TxtInventoryName"
                    Display="None" ErrorMessage="Please Enter A Inventory Name" Enabled="False"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldInventoryNameExtender" runat="server" Enabled="True"
                    TargetControlID="ReqVldInventoryName">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                Quantity
            </td>
            <td>
                <asp:TextBox ID="TxtInventoryQuantity" runat="server" Text="1" />
                <asp:RequiredFieldValidator ID="ReqVldQuantity" runat="server" 
                    ControlToValidate="TxtInventoryQuantity" Display="None" 
                    ErrorMessage="Please Enter A Valid Number"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldQuantityExtender" runat="server" 
                    Enabled="True" TargetControlID="ReqVldQuantity">
                </Ajax:ValidatorCalloutExtender>
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
<div style="float: left; width: auto; margin-left: 15%;" runat="server" id="PnlConversionDetails"
    visible="false">
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
