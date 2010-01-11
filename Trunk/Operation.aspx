<%@ Page Title="" Language="C#" MasterPageFile="~/Common.master" AutoEventWireup="true"
    CodeFile="Operation.aspx.cs" Inherits="Operation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <asp:UpdatePanel runat="server" ID="UpMenu">
        <ContentTemplate>
            <div class="Content" runat="server" id="CntrlHolder">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
