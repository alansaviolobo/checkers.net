<%@ Control Language="C#" AutoEventWireup="true" CodeFile="User.ascx.cs" Inherits="Controls_User" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetContactId(sender, eventArgs) {
        CheckersWebService.GetContactId(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnUserId.ClientID %>').value = result;
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="User Management." />
    &nbsp;&nbsp;
    <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
</div>
<table cellpadding="5" cellspacing="5">
    <tr>
        <td>
            Name
        </td>
        <td>
            <asp:TextBox ID="TxtName" runat="server" />
            <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                ServiceMethod="GetUserList" CompletionInterval="100" CompletionSetCount="10"
                ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetContactId" />
            <asp:RequiredFieldValidator ID="ReqVldName" runat="server" ErrorMessage="Please Enter Name."
                ControlToValidate="TxtName" Display="None" />
            <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" Enabled="True"
                TargetControlID="ReqVldName">
            </Ajax:ValidatorCalloutExtender>
            <asp:Button Text="Search" runat="server" ID="BtnSearch" Visible="False" OnClick="BtnSearch_Click" />
        </td>
    </tr>
    <tr>
        <td>
            UserName
        </td>
        <td>
            <asp:TextBox ID="TxtUserName" runat="server" />
            <asp:RequiredFieldValidator ID="ReqVldUserName" runat="server" ErrorMessage="Please Enter UserName."
                ControlToValidate="TxtUserName" Display="None" Enabled="false" />
            <Ajax:ValidatorCalloutExtender ID="ReqVldUserNameExtender" runat="server" Enabled="True"
                TargetControlID="ReqVldUserName">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            Type
        </td>
        <td>
            <asp:DropDownList ID="DdlType" runat="server">
                <asp:ListItem>Administrator</asp:ListItem>
                <asp:ListItem>Waiter</asp:ListItem>
                <asp:ListItem>Manager</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Phone
        </td>
        <td>
            <asp:TextBox ID="TxtPhone" runat="server" />
            <asp:RegularExpressionValidator ID="RegVldPhone" runat="server" ControlToValidate="TxtPhone"
                ErrorMessage="Please Check The Number. Only 10 Digit." Display="None" ValidationExpression="\d{10}" />
            <Ajax:ValidatorCalloutExtender ID="RegVldPhoneExtender" runat="server" Enabled="True"
                TargetControlID="RegVldPhone" />
        </td>
    </tr>
    <tr>
        <td>
            Address
        </td>
        <td>
            <asp:TextBox ID="TxtAddress" runat="server" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            <asp:TextBox ID="TxtEmail" runat="server" />
            <asp:RegularExpressionValidator ID="RegVldEmail" runat="server" ErrorMessage="Email Not In Proper Format."
                ControlToValidate="TxtEmail" Display="None" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
            <Ajax:ValidatorCalloutExtender ID="RegVldEmailExtender" runat="server" Enabled="True"
                TargetControlID="RegVldEmail">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="HdnUserId" runat="server" />
