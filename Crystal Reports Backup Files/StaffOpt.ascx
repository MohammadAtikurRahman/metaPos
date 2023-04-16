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
                <label id="lblActionText" class="disNone">save</label>
            </div>
            <div class="modal-body">
                <div id="msgOutput" class="text-left"></div>
                <div class="row">
                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4 lang-record-name">Name</label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input name="txtName" type="text" id="txtName" class="form-control" />
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4 lang-record-phone">Phone</label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input name="phone" type="text" id="phone" class="form-control" />
                        </div>
                    </div>
                    <div class="form-group group-section">
                        <div>
                            <label class="col-md-4 col-sm-4 col-xs-4 lang-record-sex">Sex</label>
                        </div>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <div class="form-group">
                                <select name="staffSexStatus" id="staffSexStatus" class="form-control">
                                    <option value="male">Male</option>
                                    <option value="female">Female</option>
                                </select>
                            </div>
                        </div>
                    </div>
                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4 lang-record-date-of-birth">Date of birth</label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input name="birthday" type="text" id="birthday" class="form-control datepickerCSS" value="" />
                        </div>
                    </div>
                    <div class="form-group group-section" id="divDepartment">
                        <label class="col-md-4 col-sm-4 col-xs-4 lang-record-department">Department</label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <select id="ddlDepartment" class="form-control">
                                <option value="0">General</option>
                                <option value="1">Marketing</option>
                            </select>
                        </div>
                    </div>

                    <div class="form-group group-section" id="divStore">
                        <label class="col-md-4 col-sm-4 col-xs-4 lang-record-store">Store</label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <select id="ddlStore" class="form-control">
                            </select>
                        </div>
                    </div>

                    <div class="form-group group-section">
                        <label class="col-md-4 col-sm-4 col-xs-4 lang-record-address">Address</label>
                        <div class="col-md-8 col-sm-8 col-xs-8">
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
