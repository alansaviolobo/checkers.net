<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Content.ascx.cs" Inherits="Controls_Content" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetMenuDetails(sender, eventArgs) {
        CheckersWebService.GetMenuDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnMenuId.ClientID %>').value = result['Id'];
        document.getElementById('<%=LblUnitPrice.ClientID %>').innerHTML = result['SellingPrice'];
        document.getElementById('<%=TxtSpecialUnitPrice.ClientID %>').value = result['SellingPrice'];
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Content Management" />
</div>
<div style="float: left; width: auto;">
    <table cellspacing="5" cellpadding="5">
        <tr>
            <td>
                Package Name
            </td>
            <td>
                <asp:Literal ID="LtrPackageName" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Item Name
            </td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" Width="188px" />
                <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                    ServiceMethod="GetMenuList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetMenuDetails" />
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
                <asp:RegularExpressionValidator ID="RegVldQuantity" runat="server" ControlToValidate="TxtQuantity"
                    Display="None" ErrorMessage="Please Enter A Number" ValidationExpression="^\d{1,10}(\.\d{0,2})?$"></asp:RegularExpressionValidator>
                <Ajax:ValidatorCalloutExtender ID="RegVldQuantityExtender" runat="server"
                    Enabled="True" TargetControlID="RegVldQuantity">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                Unit Price (Rs.)
            </td>
            <td>
                <asp:Label ID="LblUnitPrice" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Special Unit Price (Rs.)
            </td>
            <td>
                <asp:TextBox ID="TxtSpecialUnitPrice" runat="server" Width="80px" />
                <asp:RegularExpressionValidator ID="RegVldSpecialUnitPrice" runat="server" ControlToValidate="TxtSpecialUnitPrice"
                    Display="None" ErrorMessage="Please Enter A Number" ValidationExpression="^\d{1,10}(\.\d{0,2})?$"></asp:RegularExpressionValidator>
                <Ajax:ValidatorCalloutExtender ID="RegVldSpecialUnitPriceExtender" runat="server"
                    Enabled="True" TargetControlID="RegVldSpecialUnitPrice">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="BtnAdd" runat="server" Text="Add" OnClick="BtnAdd_Click" />
            </td>
        </tr>
    </table>
</div>
<div style="float: left; width: auto; margin-left: 7%;">
    <p>
        <asp:DataGrid ID="DgContent" runat="server" AutoGenerateColumns="False" OnDeleteCommand="DgContent_DeleteCommand">
            <Columns>
                <asp:BoundColumn DataField="Content_Id" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="Menu_Name" HeaderText="Menu"></asp:BoundColumn>
                <asp:BoundColumn DataField="Content_Quantity" HeaderText="Quantity"></asp:BoundColumn>
                <asp:BoundColumn DataField="Content_UnitPrice" HeaderText="Unit Price"></asp:BoundColumn>
                <asp:BoundColumn DataField="Content_Cost" HeaderText="Cost"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></p>
</div>
<asp:HiddenField ID="HdnMenuId" runat="server" />
