﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Controls_Menu" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetMenuId(sender, eventArgs) {
        CheckersWebService.GetMenuId(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnMenuId.ClientID %>').value = result;
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
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetMenuId"
                    Enabled="false" />
                <asp:RequiredFieldValidator ID="ReqVldName" runat="server" 
                    ControlToValidate="TxtName" Display="None" ErrorMessage="Please Enter The Name"></asp:RequiredFieldValidator>
                <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" 
                    Enabled="True" TargetControlID="ReqVldName">
                </Ajax:ValidatorCalloutExtender>
                <asp:Button ID="BtnSearch" runat="server" Text="Search" Visible="False" OnClick="BtnSearch_Click" />
            </td>
        </tr>
        <tr>
            <td>
                Category
            </td>
            <td>
                <asp:DropDownList ID="DdlCategory" runat="server">
                    <asp:ListItem>Restaurant</asp:ListItem>
                    <asp:ListItem>Bar</asp:ListItem>
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
