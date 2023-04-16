<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Package.aspx.cs" Inherits="MetaPOS.Admin.PackageBundle.View.Package" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Package Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <%--<link href="../Css/Sale.css" rel="stylesheet" />--%>
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/PackageBundle/Content/package.css") %>" type="text/css" />
    <link rel="stylesheet" href="<%= ResolveUrl("~/Admin/PackageBundle/Content/package-responsive.css") %>" type="text/css" />
    
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
        <div class="row">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="section">
                    <h2 class="sectionBreadcrumb"><%=Resources.Language.Title_package_page %></h2>
                    <asp:Label runat="server" ID="lblTest"></asp:Label>
                </div>
            </div>
        </div>
        <div class="row margin-top-10">
            <div class="col-md-6 col-sm-12 col-xs-12">
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPAdd">
                    <div class="section3 set-height">
                        <asp:TextBox ID="txtBarcodeScaner" runat="server" placeholder="Type product code here" Width="0" BorderStyle="None" OnTextChanged="txtBarcodeScaner_TextChanged"></asp:TextBox>
                        <asp:Panel ID="pnlScan" runat="server" DefaultButton="btnLoadProductDetails">
                            <div class="input-group add-product">
                                <asp:TextBox ID="txtSearchNameCode" CssClass="form-control" runat="server" aria-describedby="btnLoadProductDetails" placeholder="<%$Resources:Language,Lbl_package_search_to_add_product_by_name %>"></asp:TextBox>
                                <asp:HiddenField ID="hfProductDetails" runat="server" />
                                <span class="input-group-btn">
                                    <asp:ImageButton ID="btnLoadProductDetails" runat="server" CssClass="btn btn-default inp-group" OnClick="btnLoadProductDetails_Click" ImageUrl="~/Img/plus.png" />
                                </span>
                                <span class="input-group-btn">
                                    <asp:Button ID="btnPAdd" runat="server" Text="Add Product" OnClick="btnPAdd_Click" Width="0" BorderStyle="None" />
                                </span>
                            </div>
                        </asp:Panel>
                        <div class="staticContentUnit mainItem">
                            <span><%=Resources.Language.Lbl_package_sl%></span>
                            <span><%=Resources.Language.Lbl_package_product_title%></span>
                            <span class="disNone"><%=Resources.Language.Lbl_package_total_qty_min %></span>
                            <span><%=Resources.Language.Lbl_package_price %></span>
                        </div>
                        <div id="divProdPakageList" runat="server" class="mainItemF">
                            <!-- Dynamic content -->
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="col-md-6 col-sm-12 col-xs-12" id="section-print-custom-div">

                <asp:Panel ID="pnlPackage" runat="server" DefaultButton="btnPackageSave">
                    <asp:Panel ID="Panel2" runat="server" DefaultButton="btnUpdate">
                        <div class="section3">
                            <div class="form-horizontal salePadding">
                                <h4><%=Resources.Language.Lbl_package_add_update_package %></h4>
                                <hr />
                                <asp:Label ID="lblPackageId" runat="server" Visible="false"></asp:Label>
                                <div class="form-group">
                                    <asp:Label ID="lblPackageName" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language,Lbl_package_package_name %>"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPackageName" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtPackageName" ID="rvPackageName" ValidationGroup="Save" runat="server" ErrorMessage="* Required" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                
                                <div class="form-group">
                                    <asp:Label ID="Label3" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language,Lbl_package_wholesale_price %>"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtDealerPrice" CssClass="form-control float-number-validate" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ControlToValidate="txtDealerPrice" ID="RegularExpressionValidator1" ValidationGroup="Save" runat="server" ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" Display="Dynamic" ErrorMessage="* Number only" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="lblPrice" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language,Lbl_package_sale_price %>"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="txtPackagePrice" CssClass="form-control float-number-validate" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ControlToValidate="txtPackagePrice" ID="rexPackagePrice" ValidationGroup="Save" runat="server" ValidationExpression="([0-9])[0-9]*[.]?[0-9]*" Display="Dynamic" ErrorMessage="* Number only" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </div>
                                </div>
                                <div class="form-group padding-bottom-75">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnUpdate" runat="server" Text="<%$Resources:Language,Btn_package_update %>" ValidationGroup="Save" CssClass="btn btn-info btn-md btnUpdateCustom  pull-right btnEditOpt" OnClick="btnUpdate_Click" />
                                        <asp:Button ID="btnPackageSave" runat="server" Text="<%$Resources:Language,Btn_package_save%> " ValidationGroup="Save" CssClass="btn btn-success btn-md btnAddCustom btnAddOpt pull-right" OnClick="btnPackageSave_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>

        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="gridHeader">
                    <div class="col-md-4 col-xs-4 gridTitle text-left">
                        <label><%=Resources.Language.Lbl_package_package_list %></label>
                    </div>
                    <div class="col-md-8 col-xs-8 gridTitle text-right-grid form-inline">
                        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btn">
                            <div class="form-group float-left margin-left-7">
                                <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="<%$Resources:Language,Lbl_package_search %>"></asp:TextBox>
                                <asp:Button ID="btn" runat="server" CssClass="disNone" />
                            </div>
                            <div class="form-group float-left margin-left-7">
                                <asp:DropDownList ID="ddlActiveStatus" runat="server" CssClass="form-control" AutoPostBack="true">
                                    <asp:ListItem Value="1" Text="<%$Resources:Language,Lbl_package_active %>">  </asp:ListItem> 
                                    <asp:ListItem Value="0" Text="<%$Resources:Language,Lbl_package_non_active %>"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group float-left">
                                <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="scroll">
                    <asp:GridView ID="grdPackage" runat="server" CssClass="mGrid gBox scrollBar" AutoGenerateColumns="False" EmptyDataText="No data records found." AllowPaging="true"
                                  EnableViewState="false" DataKeyNames="Id" AllowSorting="true"
                                  OnRowDataBound="grdPackage_RowDataBound" PageSize="4"
                                  OnRowDeleting="grdPackage_RowDeleting" OnPageIndexChanging="grdPackage_OnPageIndexChanging"
                                  OnRowEditing="grdPackage_RowEditing" 
                                    OnRowUpdating="grdPackage_RowUpdating"
                                  OnRowUpdated="grdPackage_RowUpdated" 
                                OnDataBound="grdPackage_DataBound">
                        <Columns>
                            <%--<asp:TemplateField HeaderText="SL" ItemStyle-Width="5%">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            
                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_package_id %>" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("Id") %>' ID="lblID"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_package_package_name %>">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("packageName") %>' ID="Label1"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle Width="30%"></ItemStyle>
                            </asp:TemplateField>

                            
                            <asp:TemplateField HeaderText="Product Code" SortExpression="prodCode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("prodCode") %>' ID="lblProductCode"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_package_total_qty_min %>" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="" ID="lblminQty"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_package_empty_qty %>" ItemStyle-Width="15%" SortExpression="emptyQty">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text="" ID="lblEmptyQty"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_package_price %>">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# Bind("price") %>' ID="Label2"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_package_edit %>" ShowHeader="False" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnGrdEdit" runat="server" CssClass="btn btn-design btnEditOpt" CausesValidation="False" CommandName="Edit" Text="<span class='glyphicon glyphicon-pencil'></span>" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="<%$Resources:Language,Lbl_package_delete%>" ShowHeader="False" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnGrdDelete" CssClass="btn btn-design btnDeleteOpt" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle CssClass="pgr" />
                        <AlternatingRowStyle CssClass="alt" />
                    </asp:GridView>
                    
                </div>
            </div>
        </div>
    
    <script>
        activeModule = "inventory";
    </script>
    
    
    <script src="<%= ResolveUrl("~/Admin/PackageBundle/Script/package-search.js?v=0.0004") %>"></script>
    
    <script>

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            var head = document.getElementsByTagName("head")[0];
            var script = document.createElement('script');
            script.src = '/Admin/PackageBundle/Script/package-search.js';
            script.type = 'text/javascript';
            head.appendChild(script);
        });

    </script>

</asp:Content>