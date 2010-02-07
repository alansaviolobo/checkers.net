<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventSales.ascx.cs" Inherits="Controls_EventSales" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>

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
                Menu
            </td>
            <td>
                <asp:DropDownList ID="DdlItem" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                Quantity
            </td>
            <td>
                <asp:DropDownList ID="DdlQuantity" runat="server" />
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
        <asp:DataGrid ID="DgContent" runat="server" AutoGenerateColumns="False" 
            OnDeleteCommand="DgContent_DeleteCommand" BackColor="White" 
            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
            GridLines="Horizontal">
            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
            <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" 
                Mode="NumericPages" />
            <AlternatingItemStyle BackColor="#F7F7F7" />
            <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
            <Columns>
                <asp:BoundColumn DataField="Menu_Id" Visible="false"></asp:BoundColumn>
                <asp:BoundColumn DataField="Menu_Name" HeaderText="Name"></asp:BoundColumn>
                <asp:BoundColumn DataField="Menu_Quantity" HeaderText="Quantity"></asp:BoundColumn>
                <asp:BoundColumn DataField="Menu_Cost" HeaderText="Cost"></asp:BoundColumn>
                <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
            </Columns>
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        </asp:DataGrid></p>
</div>
<asp:HiddenField ID="HdnEventId" runat="server" />
<asp:HiddenField ID="HdnEventSource" runat="server" />