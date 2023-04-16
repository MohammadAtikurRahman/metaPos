<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmailConfig.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.EmailConfig" %>


<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Configuration Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    
     <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Email Configuration</h2>              
            </div>
        </div>
    </div>

    <div class="row ">


        <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal">
            <div class="ReturnfieldHeight2 section profile">
                <h2 class="sectionHeading">
                    <asp:Label runat="server" Text="" ID="lblOperationTitle"></asp:Label>
                </h2>
                         
                <div class="form-group">
                    <div>
                        <label class="col-sm-2">Medium</label>
                    </div>

                    <div class="col-sm-8">
                        <select name="medium" id="ddlMedium" class="form-control">
                            <option value="none">Select Your Device</option>
                            <option value="elastic">Elastic Email</option>
                            
                        </select>
                    </div>
                     
                </div>
                <div class="form-group">
                    <div>
                        <label class="col-sm-2">Sender Email</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" name="txtSender" id="txtSender" />
                    </div>
                </div>
                <div class="form-group">
                    <div>
                        <label class="col-sm-2">Api Key</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" name="txtApiKey" id="txtApiKey" />
                    </div>
                </div>
                
               
                <div class="form-group ">
                    <div>
                        <label class="col-sm-2">Cost</label>
                    </div>
                    <div class="col-sm-8">
                        <input type="text" class="form-control" name="txtCost" id="txtCost" />
                    </div>
                </div>
               
                <div class="form-group" style="padding-bottom: 25px;">

                    <button type="button" id="btnSaveChanges" class="btn btn-info btn-default CRBtnDesign btnLimpCustom btnLimpCustom">Save Changes</button>

                </div>
        </div>
        </div> 
    </div>
    
    
    <script>
        activeModule = "config";
    </script>
    

    <script src="<%= ResolveUrl("~/Admin/PromotionBundle/Script/email-config.js") %>"></script>

</asp:Content>
