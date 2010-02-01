<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Event.ascx.cs" Inherits="Controls_Event" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

<script type="text/javascript" language="javascript">
    function GetEventDetails(sender, eventArgs) {
        CheckersWebService.GetEventDetails(sender._element.value, OnSuccess, OnError);
    }

    function OnSuccess(result) {
        document.getElementById('<%=HdnEventId.ClientID %>').value = result['Id'];
        document.getElementById('<%=TxtName.ClientID %>').value = result['Name'];
        document.getElementById('<%=DdlOrganizer.ClientID %>').value = result['Organizer'];
        document.getElementById('<%=TxtVenue.ClientID %>').value = result['Venue'];
        document.getElementById('<%=TxtFromDate.ClientID %>').value = result['FromDate'];
        document.getElementById('<%=TxtToDate.ClientID %>').value = result['ToDate'];
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
<div>
    <table cellspacing="5" cellpadding="5">
        <tr>
            <td>
                Name
            </td>
            <td>
                <asp:TextBox ID="TxtName" runat="server" Width="215px" />
                <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                    ServiceMethod="GetEventList" CompletionInterval="100" CompletionSetCount="10"
                    ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" OnClientItemSelected="GetEventDetails"
                    Enabled="false" />
            </td>
        </tr>
        <tr>
            <td>
                Organizer
            </td>
            <td>
                <asp:DropDownList ID="DdlOrganizer" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Venue
            </td>
            <td>
                <asp:TextBox ID="TxtVenue" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                From Date
            </td>
            <td>
                <asp:TextBox ID="TxtFromDate" runat="server" />
                <Ajax:CalendarExtender ID="TxtFromDateCalendarExtender" runat="server" Enabled="True"
                    Format="d/M/yyyy" TargetControlID="TxtFromDate" TodaysDateFormat="">
                </Ajax:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                To Date
            </td>
            <td>
                <asp:TextBox ID="TxtToDate" runat="server" />
                <Ajax:CalendarExtender ID="TxtToDateCalendarExtender" runat="server" Enabled="True"
                    Format="d/M/yyyy" TargetControlID="TxtToDate" TodaysDateFormat="">
                </Ajax:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Button ID="BtnSubmit" runat="server" OnClick="BtnSubmit_Click" />
                </td>
        </tr>
    </table>
</div>
<asp:HiddenField ID="HdnEventId" runat="server" />
