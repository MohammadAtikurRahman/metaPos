<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Staff.aspx.cs" Inherits="MetaPOS.Admin.RecordBundle.View.Staff" %>

<%@ Register Src="~/Admin/Controller/StaffOpt.ascx" TagPrefix="uc1" TagName="StaffOpt" %>


<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Staff List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/RecordBundle/Content/record.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title"><%=Resources.Language.Title_staff %></label>
                <a id="btnSaveModal" class="btn btn-info btn-sm btnResize btnAddCustom subbranch-hide" data-toggle="modal" data-backdrop="static"><%=Resources.Language.Btn_staff_add_staff %></a>
                <asp:Label ID="lblUserRight" runat="server" Text="Label" CssClass="disNone"></asp:Label>
                
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divSearchPanel">
                <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_staff_list %></label>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-8 gridTitle text-right form-inline" id="filterPanel">
                    <div class="form-group">
                        <select name="ddlActiveStatus" id="ddlActiveStatus" class="form-control">
                            <option value="1"><%=Resources.Language.Lbl_staff_active %></option>
                            <option value="0"><%=Resources.Language.Lbl_staff_non_active %></option>
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
                    </tr>
                    </tfoot>
                </table>
            </div>
        </div>
        

        <uc1:StaffOpt runat="server" id="StaffOpt" />


    </div>
    
    <asp:HiddenField ID="lblHiddenCompanyName" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server"/>

    <script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-main.js?v0.006") %>"></script>


    <script>
        activeModule = "record";

        var ID = "<% =Resources.Language.Lbl_staff_id %>";
        var StaffName = "<% =Resources.Language.Lbl_staff_staff_name %>";
        var Phone = "<% =Resources.Language.Lbl_staff_phone %>";
        var Gender = "<% =Resources.Language.Lbl_staff_gender %>";
        var Address = "<% =Resources.Language.Lbl_staff_address %>";
        var Store_name = "<% =Resources.Language.Lbl_staff_store_name %>";
        var Action = "<% =Resources.Language.Lbl_staff_action %>";
        var Add_staff = "<% =Resources.Language.Lbl_staff_add_staff %>";
        var Edit_staff = "<% =Resources.Language.Lbl_staff_edit_staff %>";

    </script>

</asp:Content>