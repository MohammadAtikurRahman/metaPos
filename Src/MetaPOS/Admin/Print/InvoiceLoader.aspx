<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceLoader.aspx.cs" Inherits="MetaPOS.Admin.Print.InvoiceLoader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <link href="../../Css/bootstrap.min.css" rel="stylesheet" />
        <link href="../../Css/Invoice-print.css?v=0.002" rel="stylesheet" />

        <script src="../../Js/jquery.min.js"></script>
        <script src="../../Js/main.js"></script>


    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div id="dvReport" runat="server">
                </div>
            </div>
        </form>

        <script>
            
            var inFormOrLink;
            $('a').on('click', function () { inFormOrLink = true; });
            $('form').on('submit', function () { inFormOrLink = true; });

            $(window).on("beforeunload", function() {
                return inFormOrLink ? "Do you really want to close?" : null;
            });
       
        </script>

    </body>
</html>