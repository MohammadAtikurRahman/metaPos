<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoadQuery.aspx.cs" Inherits="MetaPOS.Admin.Print.LoadQuery" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Print() {
            var dvReport = document.getElementById("dvReport");
            var frame1 = dvReport.getElementsByTagName("iframe")[0];
            if (navigator.appName.indexOf("Internet Explorer") != -1 || navigator.appVersion.indexOf("Trident") != -1) {
                frame1.name = frame1.id;
                window.frames[frame1.id].focus();
                window.frames[frame1.id].print();
                window.frames[frame1.id].close();
            }
            else {
                var frameDoc = frame1.contentWindow ? frame1.contentWindow : frame1.contentDocument.document ? frame1.contentDocument.document : frame1.contentDocument;
                frameDoc.print();
                frameDoc.close();
            }
        }
    </script>
    <style>
        div.barcode-header{
        text-align:center !important;
        }
        .codeHeader1 {
            text-align: center !important;
        }
        .price1{
            text-align:center !important;
        }
        div#codeHeader1 {
            text-align: center !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="printPageHeader">
                <a href='javascript:history.go(-1)' class="printPageHeaderArrow"><i class="fa fa-arrow-left"></i></a>
                <input id="btnPrint" type="button" value="Print Your Report" onclick=" Print() " class="btn btn-warning btn-sm printBtnDesign btnPrintCustom"/>
            </div>
        </div>
        <div id="dvReport" class="col-md-12 col-sm-12 col-xs-12">
            <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" Width="100%" AutoDataBind="false" HasPrintButton="True"
                                    EnableDatabaseLogonPrompt="false" EnableParameterPrompt="True" HasCrystalLogo="False" 
                                    HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" PrintMode="ActiveX" ToolPanelView="None" 
                                    EnableDrillDown="True" HasSearchButton="True" HasExportButton="False" HasDrillUpButton="False" ShowPrintButton="Print"/>            
        </div>
    </div>

    <script>
        $(window).on("load", function () {
            activeModule = "sale";
        });

    </script>

</asp:Content>