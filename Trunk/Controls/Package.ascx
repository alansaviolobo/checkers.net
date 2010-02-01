<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Package.ascx.cs" Inherits="Controls_Package" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetPackageDetails(sender, eventArgs) {
        CheckersWebService.GetPackageDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnPackageId.ClientID %>').value = result['Id'];
        document.getElementById('<%=DdlType.ClientID %>').value = result['Type'];
        document.getElementById('<%=TxtComments.ClientID %>').value = result['Comments'];       
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Package Management." />
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
            <asp:RequiredFieldValidator ID="ReqVldName" runat="server" ControlToValidate="TxtName"
                Display="None" ErrorMessage="Please Enter A Name"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqVldNameExtender" runat="server" Enabled="True"
                TargetControlID="ReqVldName">
            </Ajax:ValidatorCalloutExtender>
            <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                ServiceMethod="GetPackageList" CompletionInterval="100" CompletionSetCount="10"
                ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetPackageDetails"
                Enabled="false" />
        </td>
    </tr>
    <tr>
        <td>
            Type
        </td>
        <td>
            <asp:DropDownList ID="DdlType" runat="server">
                <asp:ListItem Value="Birthday">Birthday</asp:ListItem>
                <asp:ListItem Value="Anniversary">Anniversary</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            Comments
        </td>
        <td>
            <asp:TextBox ID="TxtComments" runat="server" TextMode="MultiLine" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" />&nbsp;&nbsp;
        </td>
    </tr>
</table>
<asp:HiddenField ID="HdnPackageId" runat="server" />
