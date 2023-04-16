<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="MetaPOS.Admin.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Error Page - metaPOS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row pagenotfound">
        <div class="col-md-3 col-sm-3 col-xs-3"></div>
        <div class="col-md-6 col-sm-6 col-xs-6 text-center">
            <img src="../img/404.png" alt="404" class="img-responsive error_image"/>
            <h1 class="error_heading " >Page Not Found</h1>
            <img src="../img/ops.png" alt="404" class="img-responsive ops_images shake"/>
            <a  href='javascript:history.go(-1)' class="btn_back_home">Back To Previous Page</a>
            <a  href='login' class="btn_back_home">Goto To Login page</a>
            <h4 class="copyright">Copyright (c) <a href="http://www.metakave.com.bd" target="_blank">MetaKave</a> All Rights Reserved.</h4>
        </div>
        <div class="col-md-3 col-sm-3 col-xs-3"></div>
    </div>
</asp:Content>