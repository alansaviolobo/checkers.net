<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order.aspx.cs" Inherits="Order" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Assets/Style/Order.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="FrmOrder" runat="server">
    <asp:ScriptManager ID="ScrMgrOrder" runat="server">
        <Services>
            <asp:ServiceReference Path="~/CheckersWebService.asmx" />
        </Services>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpOrder" runat="server">
        <ContentTemplate>

            <script type="text/javascript">
                function CallPrint(strid) {
                    var strOldOne = document.getElementById(strid);
                    var WinPrint = window.open('', '', 'left=0,top=0,width=1,height=1,toolbar=0,scrollbars=0,status=0');
                    WinPrint.document.write("<link href=\"Assets/Style/Order.css\" rel=\"stylesheet\" type=\"text/css\" />");
                    WinPrint.document.write("<style rel=\"stylesheet\" type=\"text/css\">body * {margin-left:auto; margin-right:auto;font-family: Arial; font-size: 5px; letter-spacing: 3px; word-spacing: 5px; line-height: 200%;}</style>");
                    WinPrint.document.write(strOldOne.innerHTML);
                    WinPrint.document.close();
                    WinPrint.focus();
                    WinPrint.print();
                    WinPrint.close();
                }
                function GetClientDetails(sender, eventArgs) {
                    CheckersWebService.GetClientDetails(sender._element.value, OnSuccess, OnError);
                }

                function OnSuccess(result) {
                    document.getElementById('<%=HdnClientId.ClientID %>').value = result['Id'];
                }

                function OnError(result) {
                    alert(result);
                }

                function ResetDiscount() {
                    document.getElementById('<%=TxtDiscount.ClientID %>').value = 0.0;
                }

                function ResetDiscountOther() {
                    document.getElementById('<%=TxtDiscountBar.ClientID %>').value = 0.0;
                    document.getElementById('<%=TxtDiscountRestaurant.ClientID %>').value = 0.0;
                }

            </script>

            <div id="Information">
                <asp:Literal ID="LtrMessage" runat="server" Text="Welcome to Quatro Order System." />
            </div>
            <div id="Container">
                <div id="List" class="Border" runat="server">
                </div>
                <div id="Operation" class="Border" style="overflow:hidden;">
                    <asp:Menu ID="MenuOrder" runat="server" Orientation="Horizontal" BackColor="#B5C7DE" 
                        ForeColor="#284E98" OnMenuItemClick="MenuOrder_MenuItemClick" 
                        StaticMenuItemStyle-Width="85px" Width="440px">
                        <Items>
                            <asp:MenuItem Text="Item" Value="0" Selected="True"></asp:MenuItem>
                            <asp:MenuItem Text="Bill" Value="1"></asp:MenuItem>
                            <asp:MenuItem Text="OpenMenu" Value="2"></asp:MenuItem>
                            <asp:MenuItem Text="Token" Value="3"></asp:MenuItem>
                            <asp:MenuItem Text="Transfer" Value="4"></asp:MenuItem>
                        </Items>
                        <StaticMenuItemStyle ItemSpacing="0"
                            BorderColor="#507cd1" BorderStyle="dotted" BorderWidth="1" />
                        <StaticSelectedStyle BackColor="#507CD1" ForeColor="White" BorderColor="#507cd1"
                            BorderStyle="solid" BorderWidth="1" />
                        <StaticHoverStyle BackColor="#284E98" ForeColor="White" />
                    </asp:Menu>
                    <br />
                    <asp:MultiView ID="MultiViewOrder" runat="server" ActiveViewIndex="0">
                        <asp:View runat="server" ID="ViewItem">
                            <strong>Table No. </strong>
                            <asp:Literal ID="LtrItemTableNumber" runat="server" />
                            <br />
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        Items
                                    </td>
                                    <td>
                                        Qnty
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="DdlItem" runat="server" Width="100%" />
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlQuantity" runat="server" Width="100%" />
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnOrderItem" runat="server" Text="Order" OnClick="BtnOrderItem_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" valign="baseline">
                                        Offer
                                        <asp:DropDownList ID="DdlOffer" runat="server" Width="100%" />
                                    </td>
                                    <td valign="bottom">
                                        <asp:Button ID="BtnOrderOffer" runat="server" Text="Order" OnClick="BtnOrderOffer_Click" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div class="DataContainer">
                                <asp:DataGrid ID="DgOrderItems" runat="server" AutoGenerateColumns="False" Width="420px"
                                    OnDeleteCommand="DgOrderItems_DeleteCommand" BackColor="White" BorderColor="#E7E7FF"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Sales_Id" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Menu_Name" HeaderText="Menu"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Sales_Quantity" HeaderText="Qnty"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Sales_Cost" HeaderText="Cost"></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                </asp:DataGrid>
                            </div>
                            <br />
                            <asp:Button ID="BtnPrintToken" runat="server" Text="Print Token" OnClick="BtnPrintSelectedToken_Click" />
                        </asp:View>
                        <asp:View ID="ViewBill" runat="server">
                            <strong>Table No. </strong>
                            <asp:Literal ID="LtrBillTableNumber" runat="server" />
                            <br />
                            <br />
                            <table>
                                <tr>
                                    <td>
                                        <strong>Steward:</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlSteward" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>No. Of People:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtNoOfPeople" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Total Items:</strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblTotalItems" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Bar Items:</strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblBarItems" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Restaurant Items:</strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblRestaurantItems" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Total Cost (Rs):</strong>
                                    </td>
                                    <td>
                                        <asp:Label ID="LblTotalCost" runat="server" />
                                    </td>
                                </tr>
                                <tr onclick="ResetDiscountOther();">
                                    <td>
                                        <strong>Discount (%):</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDiscount" runat="server" Text="0" />
                                    </td>
                                </tr>
                                <tr onclick="ResetDiscount();">
                                    <td>
                                        <strong>Disc-Bar (%):</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDiscountBar" runat="server" Text="0" />
                                    </td>
                                </tr>
                                <tr onclick="ResetDiscount();">
                                    <td>
                                        <strong>Disc-Rest. (%):</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtDiscountRestaurant" runat="server" Text="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="BtnBill" runat="server" Text="Print Bill" CssClass="CenterAlign"
                                            OnClick="BtnBill_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="PnlButton" runat="server" Visible="False">
                                            <strong>
                                                <asp:Literal ID="LtrPayBy" runat="server" Text="Pay By" /></strong><br />
                                            <asp:Button ID="BtnCash" runat="server" Text="Cash" CssClass="CenterAlign" OnClick="BtnCash_Click" />
                                            <asp:Button ID="BtnCreditCard" runat="server" Text="Credit Card" CssClass="CenterAlign"
                                                OnClick="BtnCreditCard_Click" />
                                            <asp:Button ID="BtnCredit" runat="server" Text="Credit" CssClass="CenterAlign" OnClick="BtnCredit_Click" />
                                            <asp:Button ID="BtnCheque" runat="server" Text="Cheque" CssClass="CenterAlign" OnClick="BtnCheque_Click" />
                                            <asp:Button ID="BtnZeroBilling" runat="server" Text="Zero Billing" CssClass="CenterAlign"
                                                OnClick="BtnZeroBilling_Click" />
                                            <br />
                                            <asp:Literal ID="LtrClient" Text="Client" runat="server" Visible="False" />
                                            <asp:TextBox ID="TxtClient" runat="server" Visible="False" />
                                            <Ajax:AutoCompleteExtender ID="AutoCompClient" runat="server" TargetControlID="TxtClient"
                                                ServiceMethod="GetClientList" CompletionInterval="100" ServicePath="~/CheckersWebService.asmx"
                                                MinimumPrefixLength="1" OnClientItemSelected="GetClientDetails" DelimiterCharacters=""
                                                Enabled="True" />
                                            <asp:Button ID="BtnCreditOk" runat="server" Text="Ok" OnClick="BtnCreditOk_Click"
                                                Visible="False" />
                                            <asp:Button ID="BtnCancelBill" runat="server" Text="Cancel Bill" CssClass="CenterAlign"
                                                OnClick="BtnCancelBill_Click" />
                                            <p>
                                                <asp:TextBox ID="TxtReason" TextMode="MultiLine" runat="server" Visible="False" />
                                            </p>
                                            <asp:Button ID="BtnOk" runat="server" Text="Ok" OnClick="BtnOk_Click" Visible="False" />
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ViewOpenMenu" runat="server">
                            <table>
                                <tr>
                                    <td>
                                        <strong>Name:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtOpenMenuName" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Cost:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtOpenMenuCost" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Token Section:</strong>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DdlTokenSection" runat="server">
                                            <asp:ListItem Value="Bar">Bar</asp:ListItem>
                                            <asp:ListItem Value="Barbeque">Barbeque</asp:ListItem>
                                            <asp:ListItem Value="Tandoor">Tandoor</asp:ListItem>
                                            <asp:ListItem Value="Restaurant">Restaurant</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <strong>Quantity:</strong>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtOpenMenuQuantity" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Button ID="BtnOrderOpenMenu" runat="server" Text="Order" CssClass="CenterAlign"
                                            OnClick="BtnOrderOpenMenu_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:View>
                        <asp:View ID="ViewToken" runat="server">
                            <div class="DataContainer" style="height: 320px">
                                <asp:DataGrid ID="DgToken" runat="server" AutoGenerateColumns="False" OnDeleteCommand="DgToken_DeleteCommand"
                                    Width="414px" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="3" GridLines="Horizontal">
                                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                                    <SelectedItemStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" Mode="NumericPages" />
                                    <AlternatingItemStyle BackColor="#F7F7F7" />
                                    <ItemStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                                    <Columns>
                                        <asp:TemplateColumn>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="ChkBox" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Token_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Token_Type" HeaderText="Type"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Menu_Name" HeaderText="Name"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Token_Quantity" HeaderText="Qnty"></asp:BoundColumn>
                                        <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                                </asp:DataGrid>
                            </div>
                            <br />
                            <br />
                            Comments :
                            <br />
                            <asp:TextBox ID="TxtComments" runat="server" TextMode="MultiLine" Width="425px" />
                            <br />
                            <asp:Button ID="BtnCheckAll" runat="server" Text="Check All" OnClick="CheckAll_Click" />
                            <asp:Button ID="BtnUnCheckAll" runat="server" Text="UnCheck All" OnClick="UnCheckAll_Click" />
                            <asp:Button ID="BtnPrintSelectedToken" runat="server" Text="Print Selected" OnClick="BtnPrintSelectedToken_Click" />
                            <asp:Button ID="BtnDeleteSelectedToken" runat="server" Text="Delete Selected" OnClick="BtnDeleteSelectedToken_Click" />
                            <br />
                            <br />
                            <asp:Literal ID="LtrTokenCancel" runat="server" Text="Reason :" Visible="False" />
                            <asp:TextBox ID="TxtTokenCancel" runat="server" Width="230px" Visible="False" />
                            <asp:Button ID="BtnTokenCancel" runat="server" Text="Cancel" Visible="False" OnClick="BtnTokenCancel_Click" />
                            <asp:Button ID="BtnTokenReset" runat="server" Text="Reset" Visible="False" OnClick="BtnTokenReset_Click" />
                        </asp:View>
                        <asp:View ID="ViewTableOperation" runat="server">
                            Transfer To Table No.
                            <asp:DropDownList ID="DdlTransfer" runat="server" />
                            &nbsp;
                            <asp:Button ID="BtnTransfer" runat="server" Text="Transfer" OnClick="BtnTransfer_Click" />
                        </asp:View>
                    </asp:MultiView>
                </div>
            </div>
            <asp:HiddenField ID="HdnTableId" runat="server" />
            <asp:HiddenField ID="HdnBillNumber" runat="server" />
            <asp:HiddenField ID="HdnTableSource" runat="server" />
            <asp:HiddenField ID="HdnClientId" runat="server" />
            <div id="PrintBill">
                <br />
                <br />
                <br />
                <br />
                <div class="FullWidth">
                    <div class="Division3">
                        TIN: 30241108428</div>
                    <div class="Division3">
                        CASH / CREDIT MEMO</div>
                    <div class="Division3">
                        Bill No.&nbsp;<asp:Literal ID="LtrBillNo" runat="server" />
                    </div>
                </div>
                <br />
                <br />
                <div id="BillLogo">
                    <img src="Assets/Image/QuatroSmall.png" alt="" /><br />
                    <br />
                    Along NH-17 Verna By-Pass Road, Manjo Verna, Goa, 403722<br />
                    Ph: 0832-2887406
                    <br />
                    <br />
                    <br />
                    <br />
                </div>
                <div class="FullWidth">
                    <div class="Division4">
                        <b>Date</b><br />
                        <asp:Literal ID="LtrDate" runat="server" />
                    </div>
                    <div class="Division4">
                        <b>Time</b><br />
                        <asp:Literal ID="LtrTime" runat="server" />
                    </div>
                    <div class="Division4">
                        <b>Table No.</b><br />
                        <asp:Literal ID="LtrTableNo" runat="server" />
                    </div>
                    <div class="Division4">
                        <b>Steward</b><br />
                        <asp:Literal ID="LtrSteward" runat="server" />
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <table id="TblBill" runat="server" class="FullWidth" style="margin-left: auto; margin-right: auto;">
                    <thead>
                        <tr>
                            <td style="width: 50%">
                                <strong>Item</strong>
                            </td>
                            <td>
                                <strong>Qnty</strong>
                            </td>
                            <td>
                                <strong>Per Unit</strong>
                            </td>
                            <td>
                                <strong>Cost</strong>
                            </td>
                        </tr>
                    </thead>
                </table>
                <br />
                <br />
                <div class="FullWidth" style="text-align: center">
                    Prices inclusive of all the taxes.
                </div>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="LinkAdministration">
        <asp:LinkButton ID="BtnAdministration" runat="server" Text="Go To Administration"
            OnClick="BtnAdministration_Click" Visible="false" />
    </div>
    </form>
</body>
</html>
