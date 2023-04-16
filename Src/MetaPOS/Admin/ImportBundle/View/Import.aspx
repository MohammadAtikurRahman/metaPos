<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Import.aspx.cs" Inherits="MetaPOS.Admin.ImportBundle.View.Import" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Import
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Admin/ImportBundle/Content/import.css") %>" />

    <script>
        function ShowPopup(title, body) {
            $("#MyPopup .modal-title").html(title);
            $("#MyPopup .modal-body").html(body);
            $("#MyPopup").modal("show");
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row ">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title" id="dynamicSectionBreadCrumb" runat="server"><%=Resources.Language.Title_stockImport %> </label>
                <asp:Label runat="server" ID="lblTest" BackColor="red" Text=""></asp:Label>
                <a href="../../../Sample/sample_stock_import_data.xlsx" class="btn btn-info pull-right btn-import-sample"><%=Resources.Language.Btn_stockImport_download_sample %></a>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="import-section section">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-inline">
                            <div class="form-group">
                                <asp:FileUpload ID="fileUpload" runat="server" />
                            </div>
                            <asp:Button ID="btnImport" runat="server" CssClass="btn btn-primary btn-custom-uploader" Text="<%$Resources:Language, Btn_stockImport_import %>" OnClick="btnImport_OnClick" />
                        </div>

                        <%--<label class="checkbox-inline">
                            <input type="checkbox" value="">Supplier
                        </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" value="">Category
                        </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" value="">Store
                        </label>
                        <label class="checkbox-inline">
                            <input type="checkbox" value="">Unit
                        </label>--%>

                        <br />
                        <br />

                        <div class="form-group" runat="server" id="divSupplier" visible="false">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_stockImport_supplier %> <span class="label-required">*</span></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSupplierList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divCategory">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_stockImport_category %></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlCategoryList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divStore" visible="false">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_stockImport_store %> <span class="label-required">*</span></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divUnit" visible="false">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_stockImport_unit %> <span class="label-required">*</span></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlUnitList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnImport" />
        </Triggers>
    </asp:UpdatePanel>


    <!-- Modal Popup -->
    <div id="MyPopup" class="modal fade" role="dialog">
        <div class="modal-dialog">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        &times;</button>
                    <h4 class="modal-title"></h4>
                </div>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">
                        Close</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal Popup -->
    <script>
           activeModule = "inventory";
    </script>
</asp:Content>
