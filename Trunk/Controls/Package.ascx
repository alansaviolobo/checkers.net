<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Package.ascx.cs" Inherits="Controls_Package" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetPackageId(sender, eventArgs) {
        CheckersWebService.GetPackageId(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnPackageId.ClientID %>').value = result;
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
            <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                ServiceMethod="GetPackageList" CompletionInterval="100" CompletionSetCount="10"
                ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetPackageId"
                Enabled="false" />
            <asp:Button ID="BtnSearch" Text="Search" runat="server" OnClick="BtnSearch_Click" />
        </td>
    </tr>
    <tr>
        <td>
            Type
        </td>
        <td>
            <asp:DropDownList ID="DdlType" runat="server" />
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
            <input type="reset" id="BtnReset" />
        </td>
    </tr>
</table>
<asp:HiddenField ID="HdnPackageId" runat="server" />
