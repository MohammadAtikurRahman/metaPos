<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/MasterPage.Master" CodeBehind="SmsConfig.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.SmsConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Configuration Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">SMS Configuration</h2>
            </div>
        </div>
    </div>

    <div class="row ">

        <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal" id="SmsConfig">
            <div class="ReturnfieldHeight2 section profile">
                <h2 class="sectionHeading">
                    <asp:Label runat="server" Text="" ID="lblOperationTitle"></asp:Label>
                </h2>


                <div class="form-group">
                    <div>
                        <label class="col-sm-2">Group</label>
                    </div>
                    <div class="col-sm-8">
                        <select name="group" id="ddlGroup" class="form-control">
                        </select>
                    </div>
                </div>

                <div class="form-group">
                    <div>
                        <label class="col-sm-2">Medium</label>
                    </div>

                    <div class="col-sm-8">
                        <select name="medium" id="ddlMedium" class="form-control">
                            <option value='0'>Select Your Device</option>
                            <option value="infobip">Infobip</option>
                            <option value="elitbuzz">Elitbuzz</option>
                            <option value="modem">Modem</option>
                        </select>
                    </div>
                </div>

                <div id="divMedium">
                    <div class="form-group" id="selectApiKey">
                        <div>
                            <label class="col-sm-2">Api Key</label>
                        </div>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" name="txtApiKey" id="txtApiKey" />
                        </div>
                    </div>

                    <div class="form-group" id="selectSenderId">
                        <div>
                            <label class="col-sm-2">Sender Id</label>
                        </div>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" name="txtSenderId" id="txtSenderId" />
                        </div>
                    </div>

                    <div class="form-group" id="selectUsername">
                        <div>
                            <label class="col-sm-2">User Name</label>
                        </div>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" name="txtUsername" id="txtUsername" />
                        </div>
                    </div>

                    <div class="form-group" id="selectPassword">
                        <div>
                            <label class="col-sm-2">Password</label>
                        </div>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" name="txtPassword" id="txtPassword" />
                        </div>
                    </div>
                    
                    <div class="form-group">
                        <div>
                            <label class="col-sm-2">Balance</label>
                        </div>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" name="txtSmsCost" id="txtSmsBalance" />
                        </div>
                    </div>

                    <div class="form-group">
                        <div>
                            <label class="col-sm-2">Cost</label>
                        </div>
                        <div class="col-sm-8">
                            <input type="text" class="form-control" name="txtSmsCost" id="txtSmsCost" />
                        </div>
                    </div>

                </div>

                <div id="showMessage">
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

    <script src="<%= ResolveUrl("~/Admin/PromotionBundle/Script/sms-config.js?v=0.033") %>"></script>
</asp:Content>
