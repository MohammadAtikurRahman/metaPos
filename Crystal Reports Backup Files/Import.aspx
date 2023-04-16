<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="MetaPOS.Admin.ImportBundle.View.Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Import
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Admin/ImportBundle/Content/import.css") %>"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row ">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title" id="dynamicSectionBreadCrumb" runat="server">Import </label>
                <asp:Label runat="server" ID="lblTest" BackColor="red" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="import-section section">
                <div class="form-inline">
                    <div class="form-group">
                        <asp:FileUpload ID="fileUpload" runat="server" />
                    </div>
                    <asp:Button ID="btnImport" runat="server" CssClass="btn btn-primary btn-custom-uploader" Text="Import" OnClick="btnImport_OnClick" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImport"/>
        </Triggers>
    </asp:UpdatePanel>
    
</asp:Content>
