<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="FacebookConfig.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.FacebookConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/promotion.css" rel="stylesheet" />
    <script src="../Script/facebook-config.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
     <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Facebook Configuration</h2>
            </div>
        </div>
    </div>
            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal ">
                    <div class="ReturnfieldHeight2 section profile">
                          <h2 class="sectionHeading">
                    <asp:Label runat="server" Text="" ID="lblOperationTitle"></asp:Label>
                </h2>
                
                         <div class="row">
                         <div class="form-group">
                       <label class="col-md-2 col-sm-2 col-xs-2 col-md-offset-1 label-control">Page Id</label> 
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input type="text" id="txtPageId" class="form-control" value=""/>
                        </div>
                    </div>
                    </div>
                   
                    <div class="row">
                        <div class="form-group">
                       <label class="col-md-offset-2 col-sm-2 col-xs-2 col-md-offset-1 label-control">Access Token</label> 
                        <div class="col-md-8 col-sm-8 col-xs-8">
                            <input type="text" id="txtAccessToken" class="form-control" value=""/>
                        </div>
                    </div>

                    </div>
                    <div class="form-group" style="padding:25px;float:right" >
                        <button type="button" class="btn btn-info " id="btnSaveFacebookConfig">Save Changes</button>
                    </div>
                    </div>
                   
                </div>
            </div>
    
</asp:Content>
