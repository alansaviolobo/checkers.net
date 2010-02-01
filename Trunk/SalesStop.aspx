<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesStop.aspx.cs" Inherits="SalesStop"
    EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sales Stop</title>
    <style type="text/css">
        *
        {
            font-family: Calibri;
            font-size: 20px;
            margin-left: auto;
            margin-right: auto;
        }
    </style>
</head>
<body>
    <form id="FrmSalesStop" runat="server">
    <div style="width: 700px;">
        <p>
            Sales Ended For : &nbsp;&nbsp;
            <%=Application["SalesSession"].ToString() %>
        </p>
        <p>
            Total Sales For The Day : &nbsp;&nbsp; <strong>
                <asp:Literal ID="LtrTotalSales" runat="server" /></strong>
        </p>
        <br />
        <div style="width: 50%; float: left;">
            <p>
                <u>Sales By Food Type</u></p>
            <div style="width: 30%; float: left; text-align: right; padding: 5px;">
                Bar :
                <br />
                <br />
                Restaurant :
                <br />
                <br />
            </div>
            <div style="width: 60%; float: left; text-align: left; padding: 5px;">
                <strong>
                    <asp:Literal ID="LtrBar" runat="server" /></strong><br />
                <br />
                <strong>
                    <asp:Literal ID="LtrRestaurant" runat="server" /></strong>
            </div>
        </div>
        <div style="width: 50%; float: right;">
            <p>
                <u>Sales By Payment Type</u>
            </p>
            <div style="width: 30%; float: left; text-align: right; padding: 5px;">
                Credit Card:
                <br />
                <br />
                Credit :
                <br />
                <br />
                Cheque :
                <br />
                <br />
                Cash :
            </div>
            <div style="width: 60%; float: left; text-align: left; padding: 5px;">
                <strong>
                    <asp:Literal ID="LtrCreditCard" runat="server" /></strong><br />
                <br />
                <strong>
                    <asp:Literal ID="LtrCredit" runat="server" /></strong><br />
                <br />
                <strong>
                    <asp:Literal ID="LtrCash" runat="server" /></strong><br />
                <br />
                <strong>
                    <asp:Literal ID="LtrCheque" runat="server" /></strong>
            </div>
        </div>
    </div>
    <div style="margin-right: auto; margin-left: auto; width: 680px; text-align:center; padding: 10px; clear:both">
        <asp:LinkButton ID="BtnOk" Text="Ok" runat="server" onclick="BtnOk_Click" />
    </div>
    </form>
</body>
</html>
