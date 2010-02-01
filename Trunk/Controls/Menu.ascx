<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Controls_Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetMenuDetails(sender, eventArgs) {
        CheckersWebService.GetMenuDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnMenuId.ClientID %>').value = result['Id'];
        document.getElementById('<%=TxtName.ClientID %>').value = result['Name'];
        document.getElementById('<%=DdlCategory.ClientID %>').value = result['Category'];
        document.getElementById('<%=DdlTokenSection.ClientID %>').value = result['TokenSection'];
        document.getElementById('<%=TxtPrice.ClientID %>').value = result['SellingPrice'];
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
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetMenuDetails"
                    Enabled="false" />
                <asp:RequiredFieldValidator ID="ReqVldName" runat="server" 
                    ControlToValidate="TxtName" Display="None" ErrorMessage="Please Enter The Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" 
                    Enabled="True" TargetControlID="ReqVldName">
                </Ajax:ValidatorCalloutExtender>
            </td>
        </tr>
        <tr>
            <td>
                Category
            </td>
            <td>
                <asp:DropDownList ID="DdlCategory" runat="server">
                    <asp:ListItem Value="Bar">Bar</asp:ListItem>
                    <asp:ListItem Value="Beverage">Beverage</asp:ListItem>
                    <asp:ListItem Value="Restaurant">Restaurant</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Token Section</td>
            <td>
                <asp:DropDownList ID="DdlTokenSection" runat="server">
                    <asp:ListItem Value="Bar">Bar</asp:ListItem>
                    <asp:ListItem Value="Barbeque">Barbeque</asp:ListItem>
                    <asp:ListItem Value="Tandoor">Tandoor</asp:ListItem>
                    <asp:ListItem Value="Restaurant">Restaurant</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                Price (Rs.)
            </td>
            <td>
                <asp:TextBox ID="TxtPrice" runat="server" Width="80px" />
                <asp:RegularExpressionValidator ID="RegVldPrice" runat="server" 
                    ControlToValidate="TxtPrice" Display="None" 
                    ErrorMessage="Please Enter A Number" ValidationExpression="^\d{1,10}(\.\d{0,2})?$"></asp:RegularExpressionValidator>
                <Ajax:ValidatorCalloutExtender ID="RegVldPriceExtender" runat="server" 
                    Enabled="True" TargetControlID="RegVldPrice">
                </Ajax:ValidatorCalloutExtender>
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
        <strong># Menu Items :</strong>
        <asp:Literal ID="LtrMenuItems" runat="server" /><br />
        <br />
        <strong># Bar Items :</strong>
        <asp:Literal ID="LtrBarItems" runat="server" /><br />
        <br />
        <strong># Restaurant Items :</strong>
        <asp:Literal ID="LtrRestaurantItems" runat="server" /></p>
</div>
<asp:HiddenField ID="HdnMenuId" runat="server" />