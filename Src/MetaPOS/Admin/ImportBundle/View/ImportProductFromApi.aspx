<%@ Page Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="ImportProductFromApi.aspx.cs" Inherits="MetaPOS.Admin.ImportBundle.View.ImportProductFromApi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Import Product from Master Database
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="<%= ResolveUrl("~/Admin/ImportBundle/Content/importProduct.css") %>" />

    <script>
        function ShowPopup(title, body) {
            $("#MyPopup .modal-title").html(title);
            $("#MyPopup .modal-body").html(body);
            $("#MyPopup").modal("show");
        }

        function CheckGrdSelectAll(oCheckbox) {
            var i;
            var grdImportStock = document.getElementById("<%=grdProductDataFilter.ClientID %>");
            for (i = 1; i < grdImportStock.rows.length; i++) {
                grdImportStock.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row ">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title" id="dynamicSectionBreadCrumb" runat="server"><%=Resources.Language.Title_masterDB_import_product_from_master_database %></label>
                <asp:Label runat="server" ID="lblTest" BackColor="red" Text=""></asp:Label>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="import-section section">
                <div class="row">
                    <div class="col-md-6">

                        <div class="form-group" runat="server" id="divCategory">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_masterDB_category %></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlCategoryList" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCategoryList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divSubCategory">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_masterDB_sub_category %></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSubCategoryList" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSubCategoryList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divChildCategory" visible="false">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_masterDB_child_category %></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlChildCategoryList" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlChildCategoryList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divChildCategoryTwo" visible="false">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_masterDB_child_category %></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlChildCategoryListTwo" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="form-group" runat="server" id="divCompany">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_masterDB_company %></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlCompanyList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="form-group" runat="server" id="divBrand">
                            <div class="row">
                                <label class="col-md-4"><%=Resources.Language.Lbl_masterDB_brand %></label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlBrandList" runat="server" CssClass="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>



                        <div class="form-inline">
                            <asp:Button ID="btnApplyFilter" runat="server" CssClass="btn btn-primary btn-custom-uploader" Text="<%$Resources:Language, Btn_masterDB_apply_filter %>" OnClick="btnApplyFilter_onclick" />
                        </div>
                    </div>

                    <hr />

                    <div class="col-md-12">
                        <asp:Panel ID="panelProductData" runat="server" Visible="false">
                            <%--<div class="header-load-panel">
                                <h3 class="product-data-title">Product Data</h3>
                                <asp:Button runat="server" ID="btnImportProductData" CssClass="btn btn-info btn-import-product" Text="Start Import" OnClick="btnImportProductData_Click" />
                            </div>--%>
                            <div class="row">
                                <div class="col-md-2">
                                    <h3 class="product-data-title"><%=Resources.Language.Lbl_masterDB_product_data %></h3>
                                </div>
                                 <div class="col-md-2">  
                                    <asp:DropDownList ID="ddlStore" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                                <div class="col-md-3">  
                                    <asp:DropDownList ID="ddlSupplier" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                               <div class="col-md-3">  
                                    <asp:DropDownList ID="ddlCategory" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                               <div class="col-md-2">
                                    <asp:Button runat="server" ID="btnImportProductData" CssClass="btn btn-info btn-import-product" Text="<%$Resources:Language, Btn_masterDB_start_import %>" OnClick="btnImportProductData_Click" />
                               </div>
                            </div>

                            <div class="panel-body-import">
                                <asp:GridView ID="grdProductDataFilter" runat="server" DataKeyNames="Id" ViewStateMode="Enabled" OnRowDataBound="grdProductDataFilter_RowDataBound" CssClass="mGrid gBox scrollBar">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <input id="chkGrdSelectAll" type="checkbox" onclick="CheckGrdSelectAll(this)" runat="server" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox runat="server" ID="chkGrdSelect" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </div>

                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnApplyFilter" />
        </Triggers>
    </asp:UpdatePanel>

    <script>
        activeModule = "inventory";
    </script>
</asp:Content>

