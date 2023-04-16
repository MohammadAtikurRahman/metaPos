<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.Master" CodeBehind="SMSConfig.aspx.cs" Inherits="MetaPOS.Admin.SMSBundle.View.SMSConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Configure SMS - metaPOS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/SMSBundle/Content/sms-config.css" rel="stylesheet" type="text/css"/>
    <link href="<%= ResolveUrl("~/Admin/SMSBundle/Content/sms-config-responsive.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <h2 class="sectionBreadcrumb">SMS Configuration</h2>
            </div>
        </div>
    </div>

    <div class="row ">

        <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal" id="SmsConfig">
            
            <div class="ReturnfieldHeight2 section profile sms-config">
                
                <h2 class="sectionHeading">
                    <asp:Label runat="server" Text="" ID="lblOperationTitle"></asp:Label>
                </h2>

                <%--<div class="form-group">
                    <div>
                        <label class="col-sm-2">Company</label>
                    </div>
                    <div class="col-sm-8">
                        <select name="group" id="ddlBranch" class="form-control">
                        </select>
                    </div>
                </div>--%>

                <div class="form-group" id="selectUsername">
                    <div>
                        <label class="col-sm-2">User Name</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group" id="selectPassword">
                    <div>
                        <label class="col-sm-2">Password</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtPassword" CssClass="form-control" Type="password" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                 <div class="form-group" id="selectApiKey">
                    <div>
                        <label class="col-sm-2">Api Key</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtApiKey" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                
                 <div class="form-group" id="selectSenderKey">
                    <div>
                        <label class="col-sm-2">Sender Key</label>
                    </div>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txtSenderKey" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                
                
                <div id="showMessage">
                </div>
                
                
                <div class="form-group">
                  
                    <div class="col-sm-10">
                        <button type="button" id="btnSaveChanges" class="btn btn-info btn-default btnSaveChangsCustom btnLimpCustom">Save Changes</button>
                    </div>
                    <div class="col-sm-2">
                    
                    </div>
                </div>

            </div>

        </div>
    </div>
    
    <script src="<%= ResolveUrl("~/Admin/SMSBundle/Script/sms-config.js?v=.006") %>"></script>

    <asp:HiddenField ID="lblHiddenSMSConfig" runat="server"/>

    <script>
        activeModule = "settings";
    </script>

    

</asp:Content>
