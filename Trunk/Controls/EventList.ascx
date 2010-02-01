<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventList.ascx.cs" Inherits="Controls_EventList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetEventDetails(sender, eventArgs) {
        CheckersWebService.GetEventDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnEventId.ClientID %>').value = result['Id'];
    }

    function OnError(result) {
        alert(result);
    }
</script>

<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Event Management" />
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
                    ServiceMethod="GetEventList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetEventDetails" />
                <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" />
            </td>
        </tr>
    </table>
    <br />
    <asp:DataGrid ID="DgEvent" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False"
        OnDeleteCommand="DgEvent_DeleteCommand" 
        OnEditCommand="DgEvent_EditCommand" PageSize="15"
        OnPageIndexChanged="DgEvent_PageIndexChanged" 
        onitemcommand="DgEvent_ItemCommand">
        <PagerStyle Mode="NumericPages" />
        <Columns>
            <asp:BoundColumn DataField="Event_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="Event_Name" HeaderText="Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Contact_Name" HeaderText="Organizer"></asp:BoundColumn>
            <asp:BoundColumn DataField="Event_Venue" HeaderText="Venue"></asp:BoundColumn>
            <asp:BoundColumn DataField="Event_FromTimeStamp" HeaderText="From Date" 
                DataFormatString="{0:d}"></asp:BoundColumn>
            <asp:BoundColumn DataField="Event_ToTimeStamp" HeaderText="To Date" 
                DataFormatString="{0:d}"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="Delete" Text="Del"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Edit" Text="Edit"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Package" Text="Package"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Details" Text="Details"></asp:ButtonColumn>
        </Columns>
    </asp:DataGrid>
</div>
<asp:HiddenField ID="HdnEventId" runat="server" />