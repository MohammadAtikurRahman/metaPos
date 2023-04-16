<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" enableEventValidation="false" CodeBehind="Sync.aspx.cs" Inherits="MetaPOS.Admin.SyncBundle.View.Sync" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Sync - metaPOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SyncBundle/Content/Sync.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Sync</h2>
            </div>
        </div>
        <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
    </div>


    <div class="sync-section section">
        <h3>Sync Data Branch To Branch </h3>
        <hr />
        <div class="row">
            <div class="col-md-4">
                
                <div class="form-group">
                    <label>From</label>
                    <asp:DropDownList ID="ddlSyncFrom" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="col-md-2 sync-label">

                <label class="label label-info">Sync</label>
            </div>
            <div class="col-md-4">
                <div class="form-group">
                    <label>To</label>
                    <asp:DropDownList ID="ddlSyncTo" runat="server" CssClass="form-control"></asp:DropDownList>

                </div>
            </div>

            <div class="col-md-2  sync-label">
                <input type="button" class="btn btn-primary" id="btnSync" value="Sync Now"/>
                <asp:Button ID="btnSyncBackend" runat="server" OnClick="btnSyncBackend_OnClick" Text="Sync" Visible="false" />
            </div>

        </div>
    </div>
    
    <script>
        activeModule = "config";
    </script>
    
    <script src="<%= ResolveUrl("~/Admin/SyncBundle/Script/sync-main.js?v0.002") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SyncBundle/Script/sync-loader.js?v0.002") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/SyncBundle/Script/sync-upsert.js?v0.002") %>"></script>

</asp:Content>
