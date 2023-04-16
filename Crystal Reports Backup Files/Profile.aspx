<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="MetaPOS.Admin.ProfileBundle.Views.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Profile
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <script src="http://cdn.tinymce.com/4/tinymce.min.js"></script>
    <script src="/Admin/ProfileBundle/Script/tinymce.min.js"></script>
    <script src="/Admin/ProfileBundle/Script/ProfileUpsert.js"></script>
    <script src="/Admin/ProfileBundle/Script/loadData.js?v=0.001"></script>
    <link href="/Admin/ProfileBundle/Content/profile.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/ProfileBundle/Content/Profile-responsive.css") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">


    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb lang-setting-profile">Profile</h2>
                <asp:Label ID="lblTest" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <!-- tinymceTextArea -->
    <div class="section profile-section ">
        <div class="row">
            
            <div class="col-md-12">
                <div class="section">
                    <h3 class="sectionBreadcrumb lang-setting-update-your-profile">Update Your Profile</h3>
                    <asp:Label ID="lblStoreId" runat="server" Text="" CssClass="disNone"></asp:Label>
                </div>
            </div>
            <hr/>

            <div class="col-md-12 form-group " id="divStore">
                <label class="col-md-4 lang-setting-store-name">Store Name</label>
                <div class="col-md-8">
                    <asp:DropDownList ID="ddlStore" runat="server" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-company-name">Company Name</label>
                <div class="col-md-8">
                    <input type="text" id="txtCompany" class="form-control" />
                </div>
            </div>

            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-phone">Phone</label>
                <div class="col-md-8">
                    <input type="text" id="txtPhone" class="form-control" />
                </div>
            </div>


            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-mobile">Mobile</label>
                <div class="col-md-8">
                    <input type="text" id="txtMobile" class="form-control" />
                </div>
            </div>


            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-business-owner-number">Business Owner Number</label>
                <div class="col-md-8">
                    <input type="text" id="txtBusinessOwnerNumber" class="form-control" />
                </div>
            </div>

            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-vat-reg-no">Vat Reg No.</label>
                <div class="col-md-8">
                    <input type="text" id="txtVat" class="form-control" />
                </div>
            </div>

            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-tax-id-no">Tax Id No.</label>
                <div class="col-md-8">
                    <input type="text" id="txtTax" class="form-control" />
                </div>
            </div>

            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-url-path">Url Path</label>
                <div class="col-md-8">
                    <input type="text" id="txtUrlPath" class="form-control" />
                </div>
            </div>

            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-invoice-header">Invoice Header</label>
                <div class="col-md-8">
                    <textarea id="txtInvoiceHeader" class="form-control"></textarea>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <label class="col-md-4 lang-setting-invoice-footer">Invoice Footer</label>
                <div class="col-md-8">
                    <textarea id="txtInvoiceFooter" class="form-control"></textarea>
                </div>
            </div>

            <div class="col-md-12 form-group">
                <input type="button" id="btnSaveProfile" class="btn btn-primary float-right" value="Update" />
            </div>
            
            
            <div class="col-md-12">
                <div id="msgOutput" class="text-left"></div>
            </div>

        </div>
    </div>
    
    
    
    
    <div class="col-md-12 col-sm-12 col-xs-12  form-horizontal">
                <div class="ReturnfieldHeight2 section profileRight">
                    <h2 class="sectionHeading">Edit Company Logo</h2>
                    <asp:Panel ID="Panel1" runat="server" DefaultButton="btnUpdateImage">
                        <div class="form-group">
                            <asp:Label ID="Label1" CssClass="lbl col-sm-2 control-label" runat="server" Text="Current Logo"></asp:Label>
                            <div class="col-sm-8">
                                <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Img/logo-100x100.png" Width="180px" Height="180px" BorderColor="WhiteSmoke" BorderWidth="1px" /><br />
                                <asp:Label ID="lblLogoSize" runat="server" Text="[Default 100x100 px ]" ForeColor="YellowGreen"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="Label5" CssClass="lbl col-sm-2 control-label" runat="server" Text="Upload New"></asp:Label>
                            <div class="col-sm-8">
                                <asp:FileUpload ID="fulogo" runat="server" CssClass="" AlternateText="You cannot upload files." />

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="fulogo" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg)$" runat="server" ErrorMessage="NB: File name and extension should be logo.png or .jpg" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="col-sm-8 col-md-offset-2">
                                            <asp:Button ID="btnUpdateImage" CssClass="btn btn-info btn-default CRBtnDesign btnLimpCustom" runat="server" Text="Save Changes" OnClick="btnUpdateImage_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpdateImage" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                </div>
            </div>


            <!-- SMS Config -->
            <div class="col-md-12 col-sm-12 col-xs-12  form-horizontal">
                <div class="ReturnfieldHeight2 section profileRight">
                    <h2 class="sectionHeading">Send Invoice Template</h2>
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnSaveSmsTemplate">

                        <div class="form-group">
                            <asp:Label ID="Label13" CssClass="lbl col-sm-2 control-label" runat="server" Text="Message Text:"></asp:Label>
                            <div class="col-sm-8">
                                <asp:TextBox runat="server" ID="txtInvoiceSmsTemplate" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                <p>[N.B:@customer = Customer Name, @billNo= Bill NO, @paid = Paid Amount, @due = Due Amount, @cartAmt = Cart Amount, @grandAmt = Grand Amount]</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="col-sm-8 col-md-offset-2">
                                            <asp:Button ID="btnSaveSmsTemplate" CssClass="btn btn-info btn-default CRBtnDesign btnLimpCustom" runat="server" Text="Save Changes" OnClick="btnSaveSmsTemplate_OnClick" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSaveSmsTemplate" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                </div>
            </div>


            <div class="col-md-12 col-sm-12 col-xs-12  form-horizontal">
                <div class="ReturnfieldHeight2 section profileRight">
                    <h2 class="sectionHeading">Send Installament Template</h2>
                    <asp:Panel ID="Panel3" runat="server" DefaultButton="btnSaveInstallantTemplate">

                        <div class="form-group">
                            <asp:Label ID="Label12" CssClass="lbl col-sm-2 control-label" runat="server" Text="Message Text:"></asp:Label>
                            <div class="col-sm-8">
                                <asp:TextBox runat="server" ID="txtInstallmentTemplate" CssClass="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                <p>[N.B:@customer = Customer Name, @invoiceNo= Invoice NO, @nextPayDate = Next pay date, @installmentAmt = Installment Amount]</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <div class="col-sm-8 col-md-offset-2">
                                            <asp:Button ID="btnSaveInstallantTemplate" CssClass="btn btn-info btn-default CRBtnDesign btnLimpCustom" runat="server" Text="Save Changes" OnClick="btnSaveInstallantTemplate_OnClick" />

                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSaveInstallantTemplate" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                </div>
            </div>

</asp:Content>
