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
                <label class="title lang-record-supplier">Supplier</label>
                <a id="btnSaveModal" class="btn btn-info btn-sm btnResize btnAddCustom subbranch-hide lang-record-add-supplier" data-toggle="modal" data-backdrop="static">Add Supplier</a>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divSearchPanel">
                <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                    <label class="lang-record-supplier-list">Supplier List</label>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-8 gridTitle text-right form-inline" id="filterPanel">
                    <div class="form-group">
                        <select name="ddlActiveStatus" id="ddlActiveStatus" class="form-control">
                            <option value="1">Active</option>
                            <option value="0">Non-Active</option>
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
                        <label id="lblActionText" class="disNone">save</label>
                    </div>
                    <div class="modal-body">
                        <div id="msgOutput" class="text-left"></div>
                        <div class="row">
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-company-name">Company Name</label>
                                <div class="col-md-8">
                                    <input name="txtCompany" type="text" id="txtCompany" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-supplier-code">Supplier Code</label>
                                <div class="col-md-8">
                                    <input name="supplierCode" type="text" id="supplierCode" class="form-control"/>
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-contact-name">Contact Name</label>
                                <div class="col-md-8">
                                    <input name="contactName" type="text" id="contactName" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-contact-designation">Contact Designation</label>
                                <div class="col-md-8">
                                    <input name="contactDesignation" type="text" id="contactDesignation" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-contact-phone">Contact Phone</label>
                                <div class="col-md-8">
                                    <input name="contactPhone" type="text" id="contactPhone" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-contact-email">Contact Email</label>
                                <div class="col-md-8">
                                    <input name="email" type="text" id="email" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-discount">Discount (in %)</label>
                                <div class="col-md-8">
                                    <input name="discount" type="text" id="discount" class="form-control"/>
                                </div>
                            </div>
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-record-address">Address</label>
                                <div class="col-md-8">
                                    <textarea id="address" name="address" class="form-control" rows="2"></textarea>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="btnClose">Close</button>
                        <button type="button" id="btnSave" class="btn btn-info btn-sm btnResize btnAddCustom">Save</button>
                        <button type="button" id="btnUpdate" class="btn btn-info btn-sm btnResize btnAddCustom">Update</button>
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
    </script>

</asp:Content>
