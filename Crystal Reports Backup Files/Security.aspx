<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Security.aspx.cs" Inherits="MetaPOS.Admin.SettingBundle.View.Security" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Account Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/Security.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/Security-responsive.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <h2 class="sectionBreadcrumb lang-setting-security">Security</h2>
            </div>
        </div>
    </div>
    <div class="row headerDesign">
        <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
            <div class="ReturnfieldHeight2 section security">
                <h2 class="sectionHeading lang-setting-edit-user-settings">Edit User Settings</h2>
                <asp:Panel ID="pnlCode" runat="server" DefaultButton="btnUpdateUser">
                    <div class="form-group">
                        <asp:Label ID="lblCode" CssClass="lbl col-sm-4 control-label lang-setting-user-name" runat="server" Text="User Name"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" CssClass="lbl col-sm-4 control-label lang-setting-user-email" runat="server" Text="User Email"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtUserEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnUpdateUser" CssClass="btn btn-info btn-default CRBtnDesign blockDesign btnLimpCustom" runat="server" Text="Save Changes" OnClick="btnUpdateUser_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
            <div class="ReturnfieldHeight2 section security">
                <h2 class="sectionHeading lang-setting-edit-privacy-settings">Edit Privacy Settings</h2>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdatePrivacy">
                    <div class="form-group">
                        <asp:Label ID="Label5" CssClass="lbl col-sm-4 control-label lang-setting-current-password" runat="server" Text="Current Password"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCurrent" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label6" CssClass="lbl col-sm-4 control-label lang-setting-new-password" runat="server" Text="New Password"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtNew" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label7" CssClass="lbl col-sm-4 control-label lang-setting-confirm-password" runat="server" Text="Confirm Password"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtConfirm" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnUpdatePrivacy" CssClass="btn btn-info btn-default CRBtnDesign blockDesign btnLimpCustom" runat="server" Text="Save Changes" OnClick="btnUpdatePrivacy_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

    <script>
        activeModule = "settings";
    </script>

</asp:Content>
