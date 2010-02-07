<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Offer.ascx.cs" Inherits="Controls_Offer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<div id="Message">
    <asp:Literal ID="LtrMessage" runat="server" Text="Offer Management" />
    &nbsp;&nbsp;
    <asp:LinkButton ID="BtnYes" runat="server" OnClick="BtnYes_Click" Visible="false">Yes</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="BtnNo" runat="server" OnClick="BtnNo_Click" Visible="false">No</asp:LinkButton>
</div>
<table cellspacing="5" cellpadding="5">
    <tr>
        <td>
            Name
        </td>
        <td>
            <asp:TextBox ID="TxtName" runat="server" />
            <asp:RequiredFieldValidator ID="ReqName" runat="server" 
                ControlToValidate="TxtName" Display="None" ErrorMessage="Please Enter A Name"></asp:RequiredFieldValidator>
            <Ajax:ValidatorCalloutExtender ID="ReqNameExtender" runat="server" 
                Enabled="True" TargetControlID="ReqName">
            </Ajax:ValidatorCalloutExtender>
            <Ajax:AutoCompleteExtender ID="AutoCompSearch" runat="server" TargetControlID="TxtName"
                ServiceMethod="GetOfferList" CompletionInterval="100" CompletionSetCount="10"
                ServicePath="~/CheckersWebService.asmx" MinimumPrefixLength="1" Enabled="false" />
            <asp:Button ID="BtnSearch" runat="server" Text="Search" Visible="False" OnClick="BtnSearch_Click" />
        </td>
    </tr>
</table>
<div style="float: left; width: auto; margin-top: 15px; border: 1px dotted black;
    padding: 4px;" runat="server" id="PnlRequirement" visible="false">
    <strong>Menu Items Required - </strong>
    <table cellpadding="5" cellspacing="5">
        <tr>
            <td>
                Name
            </td>
            <td>
                Quantity
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="DdlMenuRequiredName" runat="server" />
            </td>
            <td>
                <asp:DropDownList ID="DdlMenuRequiredQuantity" runat="server" />
            </td>
            <td>
                <asp:Button ID="BtnMenuRequired" runat="server" Text="Add" OnClick="BtnMenuRequired_Click" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DgMenuRequired" runat="server" AutoGenerateColumns="False" 
        ondeletecommand="DgMenu_DeleteCommand" BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal">
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" 
            Mode="NumericPages" />
        <AlternatingItemStyle BackColor="#F7F7F7" />
        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <Columns>
            <asp:BoundColumn DataField="Offer_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="Menu_Name" HeaderText="Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Offer_Quantity" HeaderText="Quantity"></asp:BoundColumn>
            <asp:BoundColumn DataField="Offer_Cost" HeaderText="Cost"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
        </Columns>
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    </asp:DataGrid>
</div>
<div style="float: left; margin-left: 20px; margin-top: 15px; border: 1px dotted black;
    padding: 4px;" runat="server" id="PnlOffer" visible="false">
    <strong>Menu Items Offer - </strong>
    <table cellpadding="5" cellspacing="5">
        <tr>
            <td>
                Name
            </td>
            <td>
                Quantity
            </td>
            <td>
                Discount (%)
            </td>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="DdlMenuOfferName" runat="server" />
            </td>
            <td>
                <asp:DropDownList ID="DdlMenuOfferQuantity" runat="server" />
            </td>
            <td>
                <asp:DropDownList ID="DdlMenuOfferDiscount" runat="server" />
            </td>
            <td>
                <asp:Button ID="BtnMenuOffer" runat="server" Text="Add" OnClick="BtnMenuOffer_Click" />
            </td>
        </tr>
    </table>
    <asp:DataGrid ID="DgMenuOffer" runat="server" AutoGenerateColumns="False" 
        ondeletecommand="DgMenu_DeleteCommand" BackColor="White" BorderColor="#E7E7FF" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" 
            Mode="NumericPages" />
        <AlternatingItemStyle BackColor="#F7F7F7" />
        <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <Columns>
            <asp:BoundColumn DataField="Offer_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
            <asp:BoundColumn DataField="Menu_Name" HeaderText="Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="Offer_Quantity" HeaderText="Quantity"></asp:BoundColumn>
            <asp:BoundColumn DataField="Offer_Cost" HeaderText="Cost"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
        </Columns>
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    </asp:DataGrid>
</div>
<div style="clear: both; padding-top: 10px;">
    <asp:Button ID="BtnSubmit" runat="server" OnClick="Submit_Click" />
</div>
<asp:HiddenField ID="HdnOfferName" runat="server" />
<asp:HiddenField ID="HdnOfferId" runat="server" />