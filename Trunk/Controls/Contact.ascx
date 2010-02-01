<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Contact.ascx.cs" Inherits="Controls_Contact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetContactDetails(sender, eventArgs) {
        CheckersWebService.GetContactDetails(sender._element.value, OnSuccess, OnError);
    }

    function GetOrganizationDetails(sender, eventArgs) {
        CheckersWebService.GetOrganizationDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnContactId.ClientID %>').value = result['Id'];
        document.getElementById('<%=TxtPersonalName.ClientID %>').value = result['Name'];
        document.getElementById('<%=TxtPersonalPhone.ClientID %>').value = result['Phone'];
        document.getElementById('<%=TxtPersonalAddress.ClientID %>').value = result['Address'];
        document.getElementById('<%=TxtPersonalEmail.ClientID %>').value = result['Email'];
        document.getElementById('<%=TxtOrganizationName.ClientID %>').value = result['OrganizationName'];
        document.getElementById('<%=TxtOrganizationPhone.ClientID %>').value = result['OrganizationPhone'];
        document.getElementById('<%=TxtOrganizationAddress.ClientID %>').value = result['OrganizationAddress'];
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Contact Management." />
    &nbsp;&nbsp;
    <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
</div>
<table cellpadding="5" cellspacing="5">
    <tr>
        <td colspan="2">
            <strong>Personal</strong>
        </td>
    </tr>
    <tr>
        <td>
            Name
        </td>
        <td>
            <asp:TextBox ID="TxtPersonalName" runat="server" />
            <Ajax:AutoCompleteExtender ID="AutoCompSearchPersonalName" runat="server" TargetControlID="TxtPersonalName"
                ServiceMethod="GetContactList" CompletionInterval="100" CompletionSetCount="10"
                ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetContactDetails" />
            <asp:RequiredFieldValidator ID="ReqVldPersonalName" runat="server" ErrorMessage="Please Enter Name."
                ControlToValidate="TxtPersonalName" Display="None" Enabled="true" />
            <Ajax:ValidatorCalloutExtender ID="ReqVldPersonalNameExtender" runat="server" Enabled="True"
                TargetControlID="ReqVldPersonalName">
            </Ajax:ValidatorCalloutExtender>
        </td>
    </tr>
    <tr>
        <td>
            Phone
        </td>
        <td>
            <asp:TextBox ID="TxtPersonalPhone" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Address
        </td>
        <td>
            <asp:TextBox ID="TxtPersonalAddress" runat="server" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td>
            Email
        </td>
        <td>
            <asp:TextBox ID="TxtPersonalEmail" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <strong>Organization</strong>
        </td>
    </tr>
    <tr>
        <td>
            Organization Name
        </td>
        <td>
            <asp:TextBox ID="TxtOrganizationName" runat="server" />
            <Ajax:AutoCompleteExtender ID="AutoCompSearchOrganizationName" runat="server" TargetControlID="TxtOrganizationName"
                ServiceMethod="GetOrganizationList" CompletionInterval="100" CompletionSetCount="10"
                ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetOrganizationDetails" />
        </td>
    </tr>
    <tr>
        <td>
            Organization Phone
        </td>
        <td>
            <asp:TextBox ID="TxtOrganizationPhone" runat="server" />
        </td>
    </tr>
    <tr>
        <td>
            Organization Address
        </td>
        <td>
            <asp:TextBox ID="TxtOrganizationAddress" runat="server" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="HdnContactId" runat="server" />
