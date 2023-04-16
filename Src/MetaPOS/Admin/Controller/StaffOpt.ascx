<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StaffOpt.ascx.cs" Inherits="MetaPOS.Admin.Controller.StaffOpt" %>

 <link href="<%= ResolveUrl("~/Admin/RecordBundle/Content/record.css?v=0.013") %>" rel="stylesheet" />

<script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-main.js?v0.013") %>"></script>
<script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-staff.js?v0.013") %>"></script>



<div class="modal fade" id="formModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <asp:Label ID="lblStoreId" runat="server" Text="Label" CssClass="disNone"></asp:Label>
                <h4 class="modal-title" id="myModalLabel"></h4>
                <label id="lblActionText" class="disNone"><%=Resources.Language.Btn_staff_save %></label>
            </div>
            <div class="modal-body">
                <div id="msgOutput" class="text-left"></div>
                <div class="row">
                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4"><%=Resources.Language.Lbl_staff_staff_name %></label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input name="txtName" type="text" id="txtName" class="form-control" />
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4"><%=Resources.Language.Lbl_staff_phone %></label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input name="phone" type="text" id="phone" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group group-section">
                        <div>
                            <label class="col-md-4 col-sm-4 col-xs-4"><%=Resources.Language.Lbl_staff_sex %></label>
                        </div>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <div class="form-group">
                                <select name="staffSexStatus" id="staffSexStatus" class="form-control">
                                    <option value="male"><%=Resources.Language.Lbl_staff_male %></option>
                                    <option value="female"><%=Resources.Language.Lbl_staff_female %></option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4"><%=Resources.Language.Lbl_staff_date_of_birth %></label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input name="birthday" type="text" id="birthday" class="form-control datepickerCSS" value="" />
                        </div>
                    </div>
                    <div class="form-group group-section" id="divDepartment">
                        <label class="col-md-4 col-sm-4 col-xs-4"><%=Resources.Language.Lbl_staff_department %></label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <select id="ddlDepartment" class="form-control">
                                <option value="0"><%=Resources.Language.Lbl_staff_general %></option>
                                <option value="1"><%=Resources.Language.Lbl_staff_marketing %></option>
                            </select>
                        </div>
                    </div>

                    <div class="form-group group-section" id="divStore">
                        <label class="col-md-4 col-sm-4 col-xs-4"><%=Resources.Language.Lbl_staff_store_name %></label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <select id="ddlStore" class="form-control">
                            </select>
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4"><%=Resources.Language.Lbl_staff_address %></label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <textarea id="address" name="address" class="form-control" rows="2"></textarea>
                        </div>
                    </div>

                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal" id="btnClose"><%=Resources.Language.Btn_staff_close %></button>
                <button type="button" id="btnSave" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_staff_save %></button>
                <button type="button" id="btnUpdate" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_staff_update %></button>
            </div>
        </div>
    </div>
</div>
