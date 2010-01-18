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
                function GetClientId(sender, eventArgs) {
                    CheckersWebService.GetClientId(sender._element.value, OnSuccess, OnError);
                }

                function OnSuccess(result) {
                    document.getElementById('<%=HdnClientId.ClientID %>').value = result;
                }

                function OnError(result) {
                    alert(result);
                }
            </script>

            <div id="Information">
                <asp:Literal ID="LtrMessage" runat="server" Text="Welcome to Quatro Order System." />
            </div>
            <div id="Container">
                <div id="List" class="Border" runat="server">
                </div>
                <div id="Operation" class="Border">
                    <Ajax:TabContainer ID="TabOrder" runat="server" ActiveTabIndex="0">
                        <Ajax:TabPanel runat="server" ID="TabItem">
                            <HeaderTemplate>
                                Items
                            </HeaderTemplate>
                            <ContentTemplate>
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
                                            Quantity
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DdlItem" runat="server" />
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DdlQuantity" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnOrderItem" runat="server" Text="Order" OnClick="BtnOrderItem_Click" />
                                        </td>
                                    </tr>
                                    <%--<tr>
                                        <td colspan="2" valign="baseline">
                                            Offer
                                            <asp:DropDownList ID="DdlOffer" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnOrderOffer" runat="server" Text="Order" OnClick="BtnOrderOffer_Click" />
                                        </td>
                                    </tr>--%>
                                </table>
                                <br />
                                <div id="DataContainer">
                                    <asp:DataGrid ID="DgOrderItems" runat="server" AutoGenerateColumns="False" Width="290px"
                                        OnDeleteCommand="DgOrderItems_DeleteCommand">
                                        <Columns>
                                            <asp:BoundColumn DataField="Sales_Id" HeaderText="Id" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Menu_Name" HeaderText="Menu"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Sales_Quantity" HeaderText="Quantity"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Sales_Cost" HeaderText="Cost"></asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Delete" Text="X"></asp:ButtonColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                                <br />
                                <br />
                                <asp:DropDownList ID="DdlToken" runat="server" Width="235px" />
                                <asp:Button ID="BtnPrintOt" runat="server" Text="Print OT" CssClass="CenterAlign"
                                    OnClick="BtnPrintOt_Click" />
                            </ContentTemplate>
                        </Ajax:TabPanel>
                        <Ajax:TabPanel runat="server" ID="TabBill">
                            <HeaderTemplate>
                                Bill
                            </HeaderTemplate>
                            <ContentTemplate>
                                <strong>Table No. </strong>
                                <asp:Literal ID="LtrBillTableNumber" runat="server" />
                                <br />
                                <br />
                                <table>
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
                                    <tr>
                                        <td>
                                            <strong>Tax (%):</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtTax" runat="server" Text="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <strong>Discount (%):</strong>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TxtDiscount" runat="server" Text="0" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BtnBill" runat="server" Text="Bill" CssClass="CenterAlign" OnClick="BtnBill_Click" />
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
                                                    MinimumPrefixLength="1" OnClientItemSelected="GetClientId" DelimiterCharacters=""
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
                            </ContentTemplate>
                        </Ajax:TabPanel>
                    </Ajax:TabContainer>
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
                    </div>
                </div>
                <br />
                <br />
                <br />
                <br />
                <table id="TblBill" runat="server" class="FullWidth" style="margin-left:auto; margin-right: auto;">
                    <thead>
                        <tr>
                            <td style="width: 60%">
                                <strong>Item</strong>
                            </td>
                            <td>
                                <strong>Qnty</strong>
                            </td>
                            <td>
                                <strong>Cost</strong>
                            </td>
                        </tr>
                    </thead>
                </table>
                <br />
                <br />
            </div>
            <div id="PrintOt">
            <br />
            <br /><br />
                <asp:Label ID="LblOt" runat="server" CssClass="OtTitle" />
                <span id="Ot" runat="server" />
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
