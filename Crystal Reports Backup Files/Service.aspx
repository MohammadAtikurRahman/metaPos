<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Service.aspx.cs" Inherits="MetaPOS.Admin.SaleBundle.View.Service" %>

<%@ Import Namespace="System.ComponentModel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Service
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SaleBundle/Content/service.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/RecordBundle/Content/record.css?v=0.001") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SaleBundle/Content/Service-responsive.css?v=0.000") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">

            <div id="divHeaderPanel">
                <label class="title lang-service-title">Service</label>
                <a id="btnSaveModal" class="btn btn-info btn-sm btnResize btnAddCustom subbranch-hide lang-add-service" data-toggle="modal" data-toggle="modal" data-target="#addServiceFormModal">Add Service</a>
            </div>

        </div>


        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divSearchPanel">
                <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                    <label class="lang-service-list">Service List</label>
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
                            <th></th>
                            <th></th>
                            <th></th>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>


        <!-- Add Service Modal -->
        <div class="modal fade" id="addServiceFormModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <label id="lblId" class="disNone"></label>
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title lang-add-service" id="myModalLabel">Add Service</h4>
                    </div>
                    <div class="modal-body">

                        <span id="msgOutput"></span>

                        <div class="row">
                            <div class="form-group group-section">
                                <label class="col-md-4 lang-service-type">Type</label>
                                <div class="col-md-8">
                                    <select id="ddlServiceType" class="form-control"></select>
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <label class="col-md-4 lang-service-name">Name</label>
                                <div class="col-md-8">
                                    <input type="text" id="txtServiceName" class="form-control" placeholder="Service name" />
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <label class="col-md-4 lang-service-wholesale-price">Wholesale Price</label>
                                <div class="col-md-8">
                                    <input type="text" id="txtWholePrice" class="form-control float-number-validate" placeholder="Wholesale Price" />
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <label class="col-md-4 lang-service-retail-price">Retail Price</label>
                                <div class="col-md-8">
                                    <input type="text" id="txtRetailPrice" class="form-control float-number-validate" placeholder="Retail Price" />
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default btn-close" data-dismiss="modal">Close</button>
                        <button type="button" id="btnSave" class="btn btn-primary btn-save">Save</button>
                        <button type="button" id="btnUpdate" class="btn btn-primary btn-update">Update</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    
    <asp:HiddenField ID="lblHiddenCompanyName" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server"/>


    <script type="text/javascript" src="<%= ResolveUrl("~/Admin/SaleBundle/Script/service-upsert.js?v0.006") %>"></script>
    <script type="text/javascript" src="<%= ResolveUrl("~/Admin/SaleBundle/Script/service.js?v0.006") %>"></script>


    <script>
        activeModule = "sale";
    </script>

</asp:Content>
