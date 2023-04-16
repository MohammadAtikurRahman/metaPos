<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintQuery.aspx.cs" Inherits="MetaPOS.Admin.Print.PrintQuery" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Invoice Print</title>
        <script type="text/javascript">
            function Print() {
                var dvReport = document.getElementById("dvReport");
                var frame1 = dvReport.getElementsByTagName("iframe")[0];
                if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {
                    frame1.name = frame1.id;
                    window.frames[frame1.id].focus();
                    window.frames[frame1.id].print();
                }
                else {
                    var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                    frameDoc.print();
                }
            }
        </script>  
    </head>

    <body>
        <form id="form1" runat="server">

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xm-12">
                    <a href='javascript:history.go(-1)'>Return to Previous Page</a>
                    <input id="btnPrint" type="button" value="Print" onclick=" Print() " />
                </div>
                <div id="dvReport" class="col-md-12 col-sm-12 col-xm-12">
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" EnableDatabaseLogonPrompt="False" ToolPanelView="None" PrintMode="Pdf" />
                </div>
            </div>

        </form>
    </body>
</html>