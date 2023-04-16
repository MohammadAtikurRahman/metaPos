<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Web.aspx.cs" Inherits="MetaPOS.Admin.ShopBundle.View.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Website - metaPOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Web.css" rel="stylesheet" type="text/css" />
    <script src="../Js/jquery.MultiFile.js"></script>

    <script>
        function pageLoad() {
            //location.reload(true); 
        }
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Website Information</h2>
                <asp:Label runat="server" ID="lblTest" />
            </div>
        </div>
    </div>

    <div class="row ">
        <asp:Panel ID="pnlBranch" runat="server" DefaultButton="btnUpdateWebInfo">

            <div class="col-md-12 col-sm-12 col-xs-12 form-horizontal">
                <div class="ReturnfieldHeight2 section profile">
                    <h2 class="sectionHeading">
                        <asp:Label runat="server" Text="" ID="lblOperationTitle">Update Website Info</asp:Label>
                    </h2>
                    <div class="form-group" runat="server">
                        <asp:Label ID="lblNameTitle" CssClass="lbl col-sm-2 control-label" runat="server" Text="Website Name"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtWebName" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label1" CssClass="lbl col-sm-2 control-label" runat="server" Text="Slogan"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtWebSlogan" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label2" CssClass="lbl col-sm-2 control-label" runat="server" Text="Contact"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtContact" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label4" CssClass="lbl col-sm-2 control-label" runat="server" Text="Email"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label3" CssClass="lbl col-sm-2 control-label" runat="server" Text="Address"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label6" CssClass="lbl col-sm-2 control-label" runat="server" Text="About Shop"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtAbout" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="6"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label7" CssClass="lbl col-sm-2 control-label" runat="server" Text="Google Map Link"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtMapShareLink" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label9" CssClass="lbl col-sm-2 control-label" runat="server" Text="Display Featured Poroduct"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlFeaturedProduct" runat="server" CssClass="form-control">
                                <asp:ListItem Value="4" Selected="True">4</asp:ListItem>
                                <asp:ListItem Value="8">8</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>

                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" runat="server">
                        <asp:Label ID="Label8" CssClass="lbl col-sm-2 control-label" runat="server" Text="Display New Poroduct"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlNewProduct" runat="server" CssClass="form-control">
                                <asp:ListItem Value="4">4</asp:ListItem>
                                <asp:ListItem Value="8" Selected="True">8</asp:ListItem>
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="12">12</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnUpdateWebInfo" CssClass="btn btn-info btn-default CRBtnDesign btnLimpCustom btnLimpCustom" runat="server" Text="Save Changes" OnClick="btnUpdateWebInfo_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
         
        <asp:Panel ID="pnlSliderOption" runat="server" DefaultButton="btnUpload">
            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
                <div class="ReturnfieldHeight2 section profile">
                    <h2 class="sectionHeading">
                        <asp:Label runat="server" Text="" ID="Label5">New Images Upload </asp:Label>
                        <hr />

                        <asp:FileUpload ID="file_upload" class="multi" runat="server" />
                    </h2>
                    <%--<asp:Button ID="btnUpload" runat="server" Text="Upload"
                        OnClick="btnUpload_Click" CssClass="btn btn-primary" />--%>

                    <%--<span class="btn-file">
                        <asp:FileUpload ID="FileUpload1" runat="server" AllowMultiple="true" />
                    </span>--%>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-info btn-default  btnLimpCustom btnLimpCustom" Text="Upload" OnClick="btnUpload_Click" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnUpload" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-6 col-sm-12 col-xs-12">
                <div class="ReturnfieldHeight2 section profile">
                    <h2 class="sectionHeading">
                        <asp:Label runat="server" Text="" ID="Label10">Current Slider Images List </asp:Label>
                    </h2>
                    <asp:GridView ID="gvGalleryList" CssClass="mGrid gBox" runat="server" OnSelectedIndexChanging="gvGalleryList_SelectedIndexChanging" OnSelectedIndexChanged="gvGalleryList_SelectedIndexChanged" OnRowCommand="gvGalleryList_RowCommand" AutoGenerateColumns="false" EnableViewState="false">
                        <Columns>
                            <asp:TemplateField HeaderText="Image">
                                <ItemTemplate>
                                    <asp:Image runat="server" ImageUrl='<%# Eval("ImgData") %>' ID="ImgSlider" CssClass="img-slider-box"></asp:Image>
                                    <asp:Label runat="server" Text='<%# Eval("ImgData") %>' ID="lblImgSlider" Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete" ItemStyle-Width="20%">
                                <ItemTemplate>
                                    <asp:ImageButton runat="server" ImageUrl="~/Img/cancel.png" CssClass="btn-block btn-cancel-slider" ID="btnCancel" AlternateText="Cancel" CommandName="Select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataTemplate>
                            There are no Images.
                        </EmptyDataTemplate>
                    </asp:GridView>

                </div>
            </div>
        </asp:Panel>


    </div>
    
     <script>
         activeModule = "website";
    </script>
</asp:Content>