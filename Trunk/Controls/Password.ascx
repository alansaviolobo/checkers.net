<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Password.ascx.cs" Inherits="Controls_Password" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Change User Password." />
</div>
<table cellpadding="5" cellspacing="5">
    <tr>
        <td>
            UserName
        </td>
        <td>
            <asp:Literal ID="LtrUserName" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Old Password
        </td>
        <td>
            <asp:TextBox ID="TxtOldPassword" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator ID="ReqVldOldPassword" runat="server" 
                Display="None" ErrorMessage="Please Enter The Old Password" 
                ControlToValidate="TxtOldPassword"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqVldOldPasswordExtender" runat="server" 
                Enabled="True" TargetControlID="ReqVldOldPassword">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            New Password
        </td>
        <td>
            <asp:TextBox ID="TxtNewPassword" runat="server" TextMode="Password" />
            <asp:RequiredFieldValidator ID="ReqVldNewPassword" runat="server" 
                Display="None" ErrorMessage="Please Enter A New Password." 
                ControlToValidate="TxtNewPassword"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqVldNewPasswordExtender" runat="server" 
                Enabled="True" TargetControlID="ReqVldNewPassword">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            Retype Password
        </td>
        <td>
            <asp:TextBox ID="TxtRetypePassword" runat="server" TextMode="Password" />
            <asp:CompareValidator ID="CmpVldReTypePassword" runat="server" 
                ControlToCompare="TxtNewPassword" ControlToValidate="TxtReTypePassword" 
                Display="None" ErrorMessage="Passwords Do Not Match"></asp:CompareValidator>
            <Ajax:ValidatorCalloutExtender ID="CmpVldReTypePasswordExtender" runat="server" 
                Enabled="True" TargetControlID="CmpVldReTypePassword">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnChangePassword" Text="ChangePassword" runat="server" onclick="BtnChangePassword_Click" />&nbsp;&nbsp;
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
