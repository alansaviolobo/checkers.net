<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventPackage.ascx.cs"
    Inherits="Controls_EventPackage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetPackageDetails(sender, eventArgs) {
        CheckersWebService.GetPackageDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnPackageId.ClientID %>').value = result['Id'];
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
                <b>Event Details</b>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Event Name
            </td>
            <td>
                <asp:Literal ID="LtrEventName" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Event Organizer
            </td>
            <td>
                <asp:Literal ID="LtrEventOrganizer" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Event Date
            </td>
            <td>
                <asp:Literal ID="LtrEventDate" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                Package Name
            </td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" Width="188px" />
                <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                    ServiceMethod="GetPackageList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetPackageDetails" />
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
                <asp:TextBox ID="TxtQuantity" runat="server" Width="81px" />
                <asp:RegularExpressionValidator ID="RegVldQuantity" runat="server" ControlToValidate="TxtQuantity"
                    Display="None" ErrorMessage="Please Enter A Number" ValidationExpression="^\d{1,10}(\.\d{0,2})?$"></asp:RegularExpressionValidator>
                <Ajax:ValidatorCalloutExtender ID="RegVldQuantityExtender" runat="server" Enabled="True"
                    TargetControlID="RegVldQuantity">
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
                <asp:BoundColumn DataField="Package_Id" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="Package_Name" HeaderText="Name"></asp:BoundColumn>
                <asp:BoundColumn DataField="Package_Type" HeaderText="Type"></asp:BoundColumn>
                <asp:BoundColumn DataField="Package_Cost" HeaderText="Cost"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
            </Columns>
        </asp:DataGrid></p>
</div>
<asp:HiddenField ID="HdnPackageId" runat="server" />
