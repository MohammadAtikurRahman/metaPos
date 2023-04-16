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
                <h2 class="sectionBreadcrumb"><%=Resources.Language.Title_security %></h2>
            </div>
        </div>
    </div>
    <div class="row headerDesign">
        
        <div class="col-md-12" id="divSecureAccount" runat="server" visible="false">
            <br />
            <div class="alert alert-warning" role="alert">
                <b>দায়া করে, আপনার ররি আমার হিসাব অ্যাকাউন্ট সিকিউরিটির জন্য পাসওয়ার্ডটি পরিবর্তন করুন।</b>
            </div>
        </div>

        <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">

            <div class="ReturnfieldHeight2 section security">
                <h2 class="sectionHeading"><%=Resources.Language.Lbl_security_edit_privacy_setting %></h2>
                <asp:Panel ID="pnlCode" runat="server" DefaultButton="btnUpdateUser">
                    <div class="form-group">
                        <asp:Label ID="lblCode" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_security_user_name %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtUserName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_security_user_email %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtUserEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnUpdateUser" CssClass="btn btn-info btn-default CRBtnDesign blockDesign btnLimpCustom" runat="server" Text="<%$Resources:Language, Btn_security_save_changes %>" OnClick="btnUpdateUser_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
        <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
            <div class="ReturnfieldHeight2 section security">
                <h2 class="sectionHeading"><%=Resources.Language.Lbl_security_edit_privacy_setting %></h2>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdatePrivacy">
                    <div class="form-group">
                        <asp:Label ID="Label5" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_security_current_password %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCurrent" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label6" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_security_new_password %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtNew" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label7" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_security_confirm_password %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtConfirm" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnUpdatePrivacy" CssClass="btn btn-info btn-default CRBtnDesign blockDesign btnLimpCustom" runat="server" Text="<%$Resources:Language, Btn_security_save_changes%>" OnClick="btnUpdatePrivacy_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>



    <script>
        activeModule = "settings";
    </script>

</asp:Content>
