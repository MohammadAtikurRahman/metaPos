<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Version.aspx.cs" Inherits="MetaPOS.Admin.SettingBundle.View.Version" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Version Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/SettingBundle/Content/version.css") %>" type="text/css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="version-history section2 ">
        <div class="row">
            <div class="col-md-4 col-sm-6">
                <h3>Current Version:
                    <asp:Label runat="server" ID="lblVersion" CssClass="label label-info"></asp:Label></h3>
                <p>
                    Last updated at
                    <asp:Label runat="server" ID="lblVersionDate" CssClass="label label-info"></asp:Label>
                </p>

            </div>
            <div class="col-md-8 col-sm-6">
                <%--<div class="metapos-img">
                    <img src="../Img/metapos.png" class="img-responsive" alt="MetaPOS" />
                </div>--%>
            </div>
        </div>
    </div>
    <br />
    <div class="update-history">
        <div class="row">
            <div class="col-md-12 col-sm-12">
            </div>
        </div>

        <div class="row">
            <div class="v-history">
                <div class="col-md-12 col-sm-12">
                    <h4>Version History: </h4>
                    <hr class="v-horizontal" />
                    <h4 class="v-title"><b>Version 3.0 – 05 Novembor, 2016</b></h4>
                    <table class="table table-striped v-table">
                        <tr>
                            <td>- Email Very function for user.</td>
                        </tr>
                        <tr>
                            <td>- Invoice have now to print Company logo.</td>
                        </tr>
                        <tr>
                            <td>- Email Very function for user.</td>
                        </tr>
                        <tr>
                            <td>- Invoice have now to print Company logo.</td>
                        </tr>
                    </table>
                </div>
            </div>

            <div class="v-history">
                <div class="col-md-12 col-sm-12">
                    <h4 class="v-title"><b>Version 2.9 – 05 October, 2016</b></h4>
                    <table class="table table-striped v-table">
                        <tr>
                            <td>- Email Very function for user.</td>
                        </tr>
                        <tr>
                            <td>- Invoice have now to print Company logo.</td>
                        </tr>
                        <tr>
                            <td>- Email Very function for user.</td>
                        </tr>
                        <tr>
                            <td>- Invoice have now to print Company logo.</td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>

    </div>


    <script src="<%= ResolveUrl("~/Admin/SettingBundle/Script/version.js") %>"></script>

    <script>
        activeModule = "public";
    </script>
</asp:Content>

