<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Supplier.aspx.cs" Inherits="MetaPOS.Admin.RecordBundle.View.Supplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Suppliers
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/RecordBundle/Content/record.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title"><%=Resources.Language.Title_supplier %></label>
                <a id="btnSaveModal" class="btn btn-info btn-sm btnResize btnAddCustom subbranch-hide" data-toggle="modal" data-backdrop="static"><%=Resources.Language.Btn_supplier_add_supplier %></a>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divSearchPanel">
                <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_supplier_supplier_list %></label>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-8 gridTitle text-right form-inline" id="filterPanel">
                    <div class="form-group">
                        <select name="ddlActiveStatus" id="ddlActiveStatus" class="form-control">
                            <option value="1"><%=Resources.Language.Lbl_supplier_active %></option>
                            <option value="0"><%=Resources.Language.Lbl_supplier_non_active %></option>
                        </select>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="card" id="divListPanel">
                <table id="dataListTable" class="table table-striped table-bordered">
                    <thead>
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>

                    </tr>
                    </thead>
                    <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>

                    </tr>
                    </tfoot>
                </table>
            </div>
        </div>

        <div class="modal fade" id="formModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel"></h4>
                        <label id="lblActionText" class="disNone"><%=Resources.Language.Btn_supplier_save %></label>
                    </div>
                    <div class="modal-body">
                        <div id="msgOutput" class="text-left"></div>
                        <div class="row">
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_company_name %></label>
                                <div class="col-md-8">
                                    <input name="txtCompany" type="text" id="txtCompany" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_supplier_code %></label>
                                <div class="col-md-8">
                                    <input name="supplierCode" type="text" id="supplierCode" class="form-control"/>
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_contact_name %></label>
                                <div class="col-md-8">
                                    <input name="contactName" type="text" id="contactName" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_contact_designation %></label>
                                <div class="col-md-8">
                                    <input name="contactDesignation" type="text" id="contactDesignation" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_contact_phone %></label>
                                <div class="col-md-8">
                                    <input name="contactPhone" type="text" id="contactPhone" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_contact_email %></label>
                                <div class="col-md-8">
                                    <input name="email" type="text" id="email" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_discount %></label>
                                <div class="col-md-8">
                                    <input name="discount" type="text" id="discount" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_supplier_address %></label>
                                <div class="col-md-8">
                                    <textarea id="address" name="address" class="form-control" rows="2"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="btnClose"><%=Resources.Language.Btn_supplier_close %></button>
                        <button type="button" id="btnSave" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_supplier_save %></button>
                        <button type="button" id="btnUpdate" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_supplier_update %></button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    
    
    <asp:HiddenField ID="lblHiddenCompanyName" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server"/>

    <script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-main.js?v0.002") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-supplier.js?v=0.009") %>"></script>

    <script>
        activeModule = "record";

        var ID = "<% =Resources.Language.Lbl_supplier_id %>";
        var Company_name = "<% =Resources.Language.Lbl_supplier_company_name %>";
        var Supplier_code = "<% =Resources.Language.Lbl_supplier_supplier_code %>";
        var Contact_name = "<% =Resources.Language.Lbl_supplier_contact_name %>";
        var Contact_phone = "<% =Resources.Language.Lbl_supplier_contact_phone %>";
        var Designation = "<% =Resources.Language.Lbl_supplier_contact_designation %>";
        var Email = "<% =Resources.Language.Lbl_supplier_contact_email %>";
        var Discount = "<% =Resources.Language.Lbl_supplier_discount %>";
        var Address = "<% =Resources.Language.Lbl_supplier_address %>";
        var Action = "<% =Resources.Language.Lbl_supplier_action %>";
        var Edit_supplier = "<% =Resources.Language.Lbl_supplier_edit_supplier %>";
        var Add_new_supplier = "<% =Resources.Language.Lbl_supplier_add_new_supplier %>";

    </script>

</asp:Content>
