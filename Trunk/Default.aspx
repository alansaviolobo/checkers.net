<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body
        {
            font-family: Calibri;
            font-size: medium;
        }
        #Header
        {
            text-align: center;
            margin-top: 45px;
        }
        #LoginBox
        {
            width: 200px;
            margin-left: auto;
            margin-right: auto;
            padding: 25px;
            text-align: left;
            border: 1px dotted black;
        }
        #Footer
        {
            clear: both;
            text-align: center;
            border-top: 1px silver solid;
            margin-top: 15px;
            padding: 10px;
            font-size: small;
        }
        .CenterAlign
        {
            text-align: center;
        }
        #Information
        {
        	color: #916096;
            text-transform: capitalize;
            text-align: center;
            padding: 6px;
            font-weight: bold;
        }
    </style>
</head>
<body>
    <form id="FrmDefault" runat="server">
    <div id="Header">
        <img src="Assets/Image/Quatro.png" alt="" />
    </div>
    <br />
    <br />
    <div id="LoginBox">
        <p>
            Username:<br />
            <asp:TextBox ID="TxtUserName" runat="server" Width="200px" /></p>
        <p>
            Password:<br />
            <asp:TextBox ID="TxtPassword" runat="server" Width="200px" TextMode="Password" />
        </p>
        <p class="CenterAlign">
            <asp:Button ID="BtnLogin" runat="server" Text="Login" OnClick="BtnLogin_Click" />
        </p>
    </div>
    <br />
    <br />
    <div id="Information">
        <asp:Literal ID="LtrMessage" runat="server" />
    </div>
    <div id="Footer">
        <img src="Assets/Image/CheckersSmall.png" alt="" /><br />
        Product of Technotrix</div>
    </form>
</body>
</html>
