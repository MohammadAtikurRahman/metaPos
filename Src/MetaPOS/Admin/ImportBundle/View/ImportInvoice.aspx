<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportInvoice.aspx.cs" Inherits="MetaPOS.Admin.ImportBundle.View.ImportInvoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Invoice</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <asp:Button runat="server" ID="btnImportInvoice" OnClick="btnImportInvoice_OnClick" CssClass="btn btn-primary btn-lg" Text="Import & Export Invoice"/>
        </form>
    </body>
</html>