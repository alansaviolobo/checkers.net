<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventDetails.ascx.cs"
    Inherits="Controls_EventDetails" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Event Details" />
</div>
<div style="float: left; width: auto;">
    <h2>
        Event Details</h2>
    <table cellspacing="5" cellpadding="5">
        <tr>
            <td>
                Name
            </td>
            <td>
                <asp:Literal ID="LtrEventName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Organizer
            </td>
            <td>
                <asp:Literal ID="LtrEventOrganizer" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Venue
            </td>
            <td>
                <asp:Literal ID="LtrEventVenue" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                From Date
            </td>
            <td>
                <asp:Literal ID="LtrEventFromDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                To Date
            </td>
            <td>
                <asp:Literal ID="LtrEventToDate" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td>
                Cost
            </td>
            <td>
                <asp:Literal ID="LtrEventCost" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center">
            </td>
        </tr>
    </table>
</div>
<div style="float: left; width: auto; margin-left: 7%;">
    <h2>
        Package Details</h2>
    <p>
        <asp:DataGrid ID="DgContent" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundColumn DataField="EventPackage_Id" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="Package_Name" HeaderText="Name"></asp:BoundColumn>
                <asp:BoundColumn DataField="EventPackage_Quantity" HeaderText="Quantity"></asp:BoundColumn>
                <asp:BoundColumn DataField="EventPackage_Cost" HeaderText="Cost"></asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        </asp:DataGrid></p>
</div>
