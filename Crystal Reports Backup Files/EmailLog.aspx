<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmailLog.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.EmailLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Asset/datatable-bootstrap.min.css" rel="stylesheet" />
    <script src="../Js/data-table.min.js"></script>
    <script src="<%= ResolveUrl("~/Admin/PromotionBundle/Script/promotion-email.js") %>"></script>
    <script src="../Js/jquery.min.js"></script>
    <link href="<%= ResolveUrl("~/Admin/PromotionBundle/Content/promotion.css") %>" rel="stylesheet" />

</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title">Email History</label>

            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="card" id="divListPanel">
                <table id="dataListTable" class="table table-striped table-bordered" cellspacing="0" width="100%">
                    <thead>
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                    </tr>
                    </thead>
                </table>
            </div>
        </div>

    </div>

</asp:Content>