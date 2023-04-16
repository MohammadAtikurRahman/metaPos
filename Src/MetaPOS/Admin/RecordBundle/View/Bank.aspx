<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Bank.aspx.cs" Inherits="MetaPOS.Admin.RecordBundle.View.Bank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Bank Name Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/RecordBundle/Content/record.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title"><%=Resources.Language.Title_bank_name %></label>
                <a id="btnSaveModal" class="btn btn-info btn-sm btnResize btnAddCustom subbranch-hide" data-toggle="modal" data-backdrop="static"><%=Resources.Language.Btn_bank_add_bank %></a>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divSearchPanel">
                <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_bank_bank_name_list %></label>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-8 gridTitle text-right form-inline" id="filterPanel">
                    <div class="form-group">
                        <select name="ddlActiveStatus" id="ddlActiveStatus" class="form-control">
                            <option value="1"><%=Resources.Language.Lbl_bank_active %></option>
                            <option value="0"><%=Resources.Language.Lbl_bank_non_active %></option>
                        </select>
                    </div>
                </div>
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

                    </tr>
                    </thead>
                    <tfoot>
                    <tr>
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
                        <label id="lblActionText" class="disNone"></label>
                    </div>
                    <div class="modal-body">
                        <div id="msgOutput" class="text-left"></div>
                        <div class="row">
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_banking_bank_name %></label>
                                <div class="col-md-8">
                                    <input name="txtName" type="text" id="txtName" class="form-control"/>
                                </div>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="btnClose"><%=Resources.Language.Btn_bank_close %></button>
                        <button type="button" id="btnSave" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_bank_save %></button>
                        <button type="button" id="btnUpdate" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_bank_update %></button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    
    <asp:HiddenField ID="lblHiddenCompanyName" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server"/>

    <script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-main.js") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-bank.js?v0.001") %>"></script>

    <script>
        activeModule = "record";

        var ID = "<% =Resources.Language.Lbl_bank_id %>";
        var Bank_name = "<% =Resources.Language.Lbl_bank_bank_name %>";
        var Action = "<% =Resources.Language.Lbl_bank_action %>";
        var Add_bank = "<% =Resources.Language.Lbl_bank_add_bank %>";
        var Edit_bank_name = "<% =Resources.Language.Lbl_bank_edit_bank_name %>";

    </script>

</asp:Content>
