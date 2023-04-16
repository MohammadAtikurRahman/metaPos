<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="User.aspx.cs" Inherits="MetaPOS.Admin.SettingBundle.View.User" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Users - metaPOS
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/User.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/SettingBundle/Content/User-responsive.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div style="clear: both"></div>

    <div class="row ">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div id="divHeaderPanel">
                <label class="title" id="dynamicSectionBreadCrumb" runat="server">User</label>
                <asp:Label runat="server" ID="lblTest" BackColor="red"></asp:Label>
                <asp:Button ID="btnAddRole" CssClass="btn btn-info btn-sm btnResize btnAddCustom" CausesValidation="False" runat="server" Text="<%$Resources:Language, Btn_user_add_user %>" OnClick="btnAddRole_Click" />
            </div>
        </div>
    </div>

    

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                    <label id="lblGridTitle" runat="server">User Information</label>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-8 gridTitle text-right form-inline">
                    <div class="form-group float-left">
                        <asp:TextBox ID="txtSearch" CssClass="search form-control" placeholder="<%$Resources:Language, Lbl_user_search %>" runat="server" AutoPostBack="true" OnTextChanged="txtSearchByID_TextChanged"></asp:TextBox>

                    </div>
                    <div class="form-group float-left">
                        <asp:DropDownList ID="ddlActiveStatus" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="txtSearchByID_TextChanged">
                            <asp:ListItem Value="1" Text="<%$Resources:Language, Lbl_user_active %>"></asp:ListItem>
                            <asp:ListItem Value="0" Text="<%$Resources:Language, Lbl_user_non_active %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-12 scroll">
            <asp:GridView ID="grdRoleInfo"
                runat="server"
                CssClass="mGrid gBox"
                OnRowDataBound="grdRoleInfo_RowDataBound"
                OnRowDeleting="grdRoleInfo_RowDeleting"
                OnSelectedIndexChanged="grdRoleInfo_SelectedIndexChanged"
                OnRowEditing="grdRoleInfo_RowEditing"
                AutoGenerateColumns="False"
                DataKeyNames="roleID"
                EmptyDataText="There are no data records to display."
                ViewStateMode="Enabled"
                OnRowCommand="grdRoleInfo_OnRowCommand">
                <Columns>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_id %>" SortExpression="roleID">
                        <EditItemTemplate>
                            <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("roleID") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("roleID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_name %>" SortExpression="title">
                        <ItemTemplate>
                            <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_email %>" SortExpression="email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_type %>" SortExpression="branchType" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBranchType" runat="server" Text='<%# Eval("branchType").ToString() %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_verify %>" SortExpression="verify">
                        <ItemTemplate>
                            <asp:LinkButton runat="server" CssClass="btnVerifyClass" ID="btnVerify" Text='<%# Bind("verify") %>' CommandName="verify" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_access_page %>" SortExpression="title" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAccessPage" runat="server" Text='<%# Bind("accessPage") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Access To" SortExpression="title" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblAccessTo" runat="server" Text='<%# Bind("accessTo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Login" ShowHeader="False" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdLogin" CssClass="btn btn-design" runat="server" CausesValidation="False" CommandName="Login" Text="Login"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_view %>" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdView" CssClass="btn btn-design" runat="server" CausesValidation="False" CommandName="Select" Text="<span class='glyphicon glyphicon-adjust'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Btn_user_edit %>" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdEdit" CssClass="btn btn-design" runat="server" CausesValidation="False" CommandName="Edit" Text="<span class='glyphicon glyphicon-pencil'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_archive %>" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdDelete" CssClass="btn btn-design" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
                <RowStyle Wrap="false" />
                <HeaderStyle Wrap="false" />
            </asp:GridView>
        </div>

        <!-- Verify modal -->
        <asp:Label runat="server" ID="pnlVeridy">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="verifyModal">
                        <asp:Label runat="server" ID="lblEmail"></asp:Label>
                        <div class="modal fade" id="mailVerify" tabindex="-1" role="dialog">
                            <div class="modal-dialog modal-md" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close m-close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>

                                        <h4 class="modal-title">Email Verification</h4>
                                    </div>
                                    <div class="modal-body">
                                        <h4>
                                            <asp:Label runat="server" ID="lblMessage" CssClass="label label-default"></asp:Label></h4>
                                        <div class="form-group verify-title">
                                            <asp:Label runat="server" ID="lblName">Verification Code: </asp:Label>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox runat="server" ID="txtVerify" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Button ID="btnVerify" CssClass="btn btn-info btnAddCustom btnAddOpt" runat="server" Text="Verify" OnClick="btnVerify_OnClick" />
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" id="btnClose" runat="server" class="btn btn-default m-close" data-dissmiss="modal" aria-label="close" onclick="reloadThisPage()">Close</button>
                                    </div>
                                </div>
                                <!-- /.modal-content -->
                            </div>
                            <!-- /.modal-dialog -->
                        </div>
                        <!-- /.modal -->
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnVerify" />
                </Triggers>
            </asp:UpdatePanel>
        </asp:Label>
    </div>


    <div class="modal fade" id="modalDtl" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header ">
                    <button type="button" class="close" data-dismiss="modal" onclick=" reloadThisPage(); ">&times;</button>
                    <h3 class="modal-title">
                        <span class="glyphicon glyphicon-user glyIconPosition"></span>
                        <span>
                            <asp:Label ID="lblModalTitle" runat="server" Text="Label"></asp:Label>
                            <asp:Label ID="lblNewID" runat="server" Text="Label"></asp:Label>
                        </span>
                    </h3>
                </div>
                <div style="clear: both"></div>
                <div class="modal-body">
                    <div class="form-group">

                        <asp:Label runat="server" ID="lblTopId" Visible="false" />
                        <asp:Label runat="server" ID="lblCurrentDate" Visible="false" />
                        <asp:Label runat="server" ID="lblPassword" Visible="false" />
                        <asp:Label runat="server" ID="lblVersion" Visible="false" />
                        <asp:Label runat="server" ID="lblAccessPageList" Visible="false" />
                        <asp:Label runat="server" ID="lblAccessToList" Visible="false" />
                        <asp:Label runat="server" ID="lblSearchBranchId" Visible="false" />
                        <asp:Label runat="server" ID="lblSearchRoleId" Visible="False" />
                        <asp:Label runat="server" ID="lblIsVerify" Visible="False" />
                        <asp:Label runat="server" ID="lblGetEmail" Visible="False" />
                        <asp:Label runat="server" ID="lblEmailUpdate" Visible="False" />
                        <asp:Label runat="server" ID="lblSearchUserRight" Visible="False" />
                        <asp:Label runat="server" ID="lblDomainName" Visible="false" />
                        <asp:Label runat="server" ID="lblIsDomainActive" Visible="false" />
                        <asp:Label runat="server" ID="lblBranchLimit" Visible="false" Text="1" />
                        <asp:Label runat="server" ID="lblUserLimit" Visible="false" Text="1" />
                        <asp:Label runat="server" ID="lblBranchType" Visible="false" />
                        <asp:Label runat="server" ID="lblStoreId" Visible="false" />
                        <asp:Label runat="server" ID="lblRoleId" Visible="false" />
                        <asp:Label runat="server" ID="lblInheritId" Visible="false" />
                        <asp:Label runat="server" ID="lblMonthlyFee" Visible="false" />
                        <asp:Label runat="server" ID="lblExpiryDate" Visible="false" />


                        <asp:DetailsView
                            ID="dtlRoleInfo"
                            runat="server"
                            AllowPaging="false"
                            CssClass="mDtl dtlSize"
                            OnDataBound="dtlRoleInfo_DataBound"
                            AutoGenerateRows="False"
                            DataKeyNames="roleID"
                            onkeydown="return (event.keyCode!=13)">
                            <Fields>
                                <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_id %>">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User ID" SortExpression="roleID">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtRoleID" runat="server" Text='<%# Bind("roleID") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("roleID") %>'></asp:Label>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRoleID" runat="server" Text='<%# Bind("roleID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_title %>" SortExpression="title">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Required!" Display="Dynamic" CssClass="crStyle"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtTitle" runat="server" Text='<%# Bind("title") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="Required!" Display="Dynamic" CssClass="crStyle"></asp:RequiredFieldValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblTitle" runat="server" Text='<%# Bind("title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_email %>" SortExpression="email">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required!" Display="Dynamic" CssClass="crStyle"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cusEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Exists!" OnServerValidate="cusEmail_ServerValidateEditMode" Display="Dynamic" CssClass="crStyle"></asp:CustomValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="Wrong Format!" ValidationExpression="\S+@\S+\.\S{2,3}" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("email") %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Required!" Display="Dynamic" CssClass="crStyle"></asp:RequiredFieldValidator>
                                        <asp:CustomValidator ID="cusEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Exists!" OnServerValidate="cusEmail_ServerValidateInsertMode" Display="Dynamic" CssClass="crStyle"></asp:CustomValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="Wrong Format!" ValidationExpression="\S+@\S+\.\S{2,3}" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_password %>" SortExpression="password">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" value='<%# lblPassword.Text %>' Text='<%# lblPassword.Text %>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required!" Display="Dynamic" CssClass="crStyle"></asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Text=""></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Required!" Display="Dynamic" CssClass="crStyle"></asp:RequiredFieldValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPassword" runat="server" value='<%# lblPassword.Text %>' Text='<%# lblPassword.Text %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_confirm_password %>" SortExpression="password">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" value='<%# lblPassword.Text %>' Text='<%# lblPassword.Text %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" Text=""></asp:TextBox>
                                    </InsertItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Website URL" SortExpression="domainName">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDomainName" runat="server" Text='<%# Bind("domainName") %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ID="rfUrll" ControlToValidate="txtDomainName" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?" ErrorMessage="URL is not validate" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtDomainName" runat="server" Text='<%# Request.Url.Host %>'></asp:TextBox>
                                        <asp:RegularExpressionValidator runat="server" ID="rfUrll" ControlToValidate="txtDomainName" ValidationExpression="(http(s)?://)?([\w-]+\.)+[\w-]+[.com]+(/[/?%&=]*)?" ErrorMessage="URL is not validate" Display="Dynamic"></asp:RegularExpressionValidator>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDomainName" runat="server" Text='<%# Bind("domainName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Activate URL" SortExpression="isDomainActive">
                                    <EditItemTemplate>
                                        <asp:RadioButtonList ID="rbIsDomainActive" CssClass="radioButtonList" runat="server" SelectedValue='<%# bool.Parse(Eval("isDomainActive").ToString()) %>'>
                                            <asp:ListItem Value="True" Text="Enable"></asp:ListItem>
                                            <asp:ListItem Value="False" Text="Disable" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:RadioButtonList ID="rbIsDomainActive" CssClass="radioButtonList" runat="server" SelectMethod='<%#Bind("isDomainActive") %>'>
                                            <asp:ListItem Value="1" Text="Enable" Selected="true"></asp:ListItem>
                                            <asp:ListItem Value="0" Text="Disable" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIsDomainActive" runat="server" Text='<%#Bind("isDomainActive") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shop Type" SortExpression="branchType" Visible="false">
                                    <EditItemTemplate>

                                        <asp:DropDownList ID="ddlBranchType" CssClass="form-control" runat="server" SelectedValue='<%#Eval("branchType") %>'>
                                            <asp:ListItem Value="" Text="None"></asp:ListItem>
                                            <asp:ListItem Value="main" Text="Main"></asp:ListItem>
                                            <asp:ListItem Value="sub" Text="Sub"></asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:Label ID="lblBranchType" runat="server" Text='<%# Eval("branchType").ToString() == "1" ? "Main" : Eval("branchType").ToString() == "2" ? "Sub" : " None" %>' Visible="false"></asp:Label>

                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlBranchType" CssClass="form-control" runat="server" SelectMethod='<%#Bind("branchType") %>'>
                                            <asp:ListItem Value="" Text="None"></asp:ListItem>
                                            <asp:ListItem Value="main" Text="Main"></asp:ListItem>
                                            <asp:ListItem Value="sub" Text="Sub"></asp:ListItem>
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchType" runat="server" Text='<%# Eval("branchType").ToString() %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--SelectMethod='<%#Bind("branchType") %>'  SelectMethod='<%#Bind("inheritId") %>'--%>
                                <asp:TemplateField HeaderText="Shop Inherit" SortExpression="inheritId" Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlInheritId" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlInheritId" CssClass="form-control" runat="server" SelectMethod='<%#Bind("inheritId") %>'>
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblInheritIdt" runat="server" Text='<%# Bind("inheritId") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Branch Limit" SortExpression="branchLimit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBranchLimit" runat="server" Text='<%# Bind("branchLimit") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtBranchLimit" runat="server" Text='1'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchLimit" runat="server" Text='<%# Bind("branchLimit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="User Limit" SortExpression="userLimit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUserLimit" runat="server" Text='<%# Bind("userLimit") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtUserLimit" runat="server" Text='1'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUserLimit" runat="server" Text='<%# Bind("userLimit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_store %>" SortExpression="storeId" Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlStoreId" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:DropDownList ID="ddlStoreId" CssClass="form-control" runat="server" SelectMethod='<%#Bind("storeId") %>'>
                                        </asp:DropDownList>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <%--<asp:Label ID="lblStoreName" runat="server" Text='<%# Bind("warehousename") %>'></asp:Label>--%>
                                        <asp:Label ID="ddlStoreId" runat="server" Text='<%# Bind("storeId") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_user_access_page %>" SortExpression="accessPage" ItemStyle-CssClass="chkDesign" ControlStyle-Width="330px ">
                                    <EditItemTemplate>
                                        <label class="access-title">Inventory</label>
                                        <asp:CheckBox ID="chkPurchase" runat="server" Text="Purchase" />
                                        <asp:CheckBox ID="chkStock" runat="server" Text="Stock" />
                                        <asp:CheckBox ID="chkProduct" runat="server" Text="Product" CssClass="disNone" />
                                        <asp:CheckBox ID="chkPackage" runat="server" Text="Package" />
                                        <asp:CheckBox ID="chkWarranty" runat="server" Text="Warranty" />
                                        <asp:CheckBox ID="chkReturn" runat="server" Text="Return" />
                                        <asp:CheckBox ID="chkDamage" runat="server" Text="Damage" />
                                        <asp:CheckBox ID="chkCancel" runat="server" Text="Cancel" Visible="False" />
                                        <asp:CheckBox ID="chkWarning" runat="server" Text="Warning" />
                                        <asp:CheckBox ID="chkExpiry" runat="server" Text="Expiry" />
                                        <asp:CheckBox ID="chkImport" runat="server" Text="Import" />
                                        <hr />
                                        <label class="access-title">Sales</label>
                                        <asp:CheckBox ID="chkSale" runat="server" Text="Sale" CssClass="disNone" />
                                        <asp:CheckBox ID="chkInvoice" runat="server" Text="Invoice" />
                                        <asp:CheckBox ID="chkCustomer" runat="server" Text="Customer" />
                                        <asp:CheckBox ID="chkQuotation" runat="server" Text="Quotation" />
                                        <asp:CheckBox ID="chkServicing" runat="server" Text="Servicing" />
                                        <asp:CheckBox ID="chkDueReminder" runat="server" Text="DueReminder" />
                                        <asp:CheckBox ID="chkService" runat="server" Text="Service" />
                                        <asp:CheckBox ID="chkToken" runat="server" Text="Token" />
                                        <hr />
                                        <label class="access-title">Accounting</label>
                                        <asp:CheckBox ID="chkSupply" runat="server" Text="Supply" />
                                        <asp:CheckBox ID="chkReceive" runat="server" Text="Receive" />
                                        <asp:CheckBox ID="chkSalary" runat="server" Text="Salary" />
                                        <asp:CheckBox ID="chkExpense" runat="server" Text="Expense" />
                                        <asp:CheckBox ID="chkBanking" runat="server" Text="Banking" />
                                        <hr />
                                        <label class="access-title">Reports</label>
                                        <asp:CheckBox ID="chkDashboard" runat="server" Text="Dashboard" />
                                        <asp:CheckBox ID="chkSlip" runat="server" Text="Slip" />
                                        <asp:CheckBox ID="chkStockReport" runat="server" Text="StockReport" />
                                        <asp:CheckBox ID="chkPurchaseReport" runat="server" Text="PurchaseReport" />
                                        <asp:CheckBox ID="chkInventoryReport" runat="server" Text="InventoryReport" />
                                        <asp:CheckBox ID="chkSupplierCommission" runat="server" Text="SupplierCommission" />
                                        <asp:CheckBox ID="chkTransaction" runat="server" Text="Transaction" />
                                        <asp:CheckBox ID="chkProfitLoss" runat="server" Text="ProfitLoss" />
                                        <asp:CheckBox ID="chkAnalytic" runat="server" Text="Analytic" />
                                        <asp:CheckBox ID="chkSummary" runat="server" Text="Summary" />
                                        <hr />
                                        <label class="access-title">Promotions</label>
                                        <asp:CheckBox ID="chkOffer" runat="server" Text="Offer" />
                                        <asp:CheckBox ID="chkSMS" runat="server" Text="SMS" />
                                        <hr />
                                        <label class="access-title">Website</label>
                                        <asp:CheckBox ID="chkWeb" runat="server" Text="Web" CssClass="disNone" />
                                        <asp:CheckBox ID="chkEcommerce" runat="server" Text="Ecommerce" />
                                        <hr />
                                        <label class="access-title">Records</label>
                                        <asp:CheckBox ID="chkWarehouse" runat="server" Text="Store" />
                                        <asp:CheckBox ID="ckhManufacturer" runat="server" Text="Manufacturer" />
                                        <asp:CheckBox ID="chkLocation" runat="server" Text="Location" />
                                        <asp:CheckBox ID="chkSupplier" runat="server" Text="Supplier" />
                                        <asp:CheckBox ID="chkCategory" runat="server" Text="Category" />
                                        <asp:CheckBox ID="chkUnitMeasurement" runat="server" Text="UnitMeasurement" />
                                        <asp:CheckBox ID="chkField" runat="server" Text="Field" />
                                        <asp:CheckBox ID="chkAttribute" runat="server" Text="Attribute" />
                                        <asp:CheckBox ID="chkBank" runat="server" Text="Bank" />
                                        <asp:CheckBox ID="chkCard" runat="server" Text="Card" />
                                        <asp:CheckBox ID="chkParticular" runat="server" Text="Particular" />
                                        <asp:CheckBox ID="chkStaff" runat="server" Text="Staff" />
                                        <asp:CheckBox ID="chkServiceType" runat="server" Text="ServiceType" />

                                        <hr />
                                        <label class="access-title">Settings</label>
                                        <asp:CheckBox ID="chkSubscription" runat="server" Text="Subscription" />
                                        
                                        <hr />
                                        <label class="access-title">Offline</label>
                                        <asp:CheckBox ID="chkOffline" runat="server" Text="Offline" />

                                        <hr />
                                        <label class="access-title">Operation</label>
                                        <asp:CheckBox ID="chkAdd" runat="server" Text="Add" />
                                        <asp:CheckBox ID="chkEdit" runat="server" Text="Edit" />
                                        <asp:CheckBox ID="chkDelete" runat="server" Text="Delete" />

                                    </EditItemTemplate>

                                    <InsertItemTemplate>
                                        <label class="access-title">Inventory</label>
                                        <asp:CheckBox ID="chkPurchase" runat="server" Text="Purchase" />
                                        <asp:CheckBox ID="chkStock" runat="server" Text="Stock" />
                                        <asp:CheckBox ID="chkProduct" runat="server" Text="Product" CssClass="disNone" />
                                        <asp:CheckBox ID="chkPackage" runat="server" Text="Package" />
                                        <asp:CheckBox ID="chkWarranty" runat="server" Text="Warranty" />
                                        <asp:CheckBox ID="chkReturn" runat="server" Text="Return" />
                                        <asp:CheckBox ID="chkDamage" runat="server" Text="Damage" />
                                        <asp:CheckBox ID="chkCancel" runat="server" Text="Cancel" />
                                        <asp:CheckBox ID="chkWarning" runat="server" Text="Warning" />
                                        <asp:CheckBox ID="chkExpiry" runat="server" Text="Expiry" />
                                        <asp:CheckBox ID="chkImport" runat="server" Text="Import" />
                                        <hr />
                                        <label class="access-title">Sales</label>
                                        <asp:CheckBox ID="chkSale" runat="server" Text="Sale" CssClass="disNone" />
                                        <asp:CheckBox ID="chkInvoice" runat="server" Text="Invoice" />
                                        <asp:CheckBox ID="chkCustomer" runat="server" Text="Customer" />
                                        <asp:CheckBox ID="chkQuotation" runat="server" Text="Quotation" />
                                        <asp:CheckBox ID="chkServicing" runat="server" Text="Servicing" />
                                        <asp:CheckBox ID="chkDueReminder" runat="server" Text="DueReminder" />
                                        <asp:CheckBox ID="chkService" runat="server" Text="Service" />
                                        <asp:CheckBox ID="chkToken" runat="server" Text="Token" />
                                        <hr />
                                        <label class="access-title">Accounting</label>
                                        <asp:CheckBox ID="chkSupply" runat="server" Text="Supply" />
                                        <asp:CheckBox ID="chkReceive" runat="server" Text="Receive" />
                                        <asp:CheckBox ID="chkSalary" runat="server" Text="Salary" />
                                        <asp:CheckBox ID="chkExpense" runat="server" Text="Expense" />
                                        <asp:CheckBox ID="chkBanking" runat="server" Text="Banking" />
                                        <hr />
                                        <label class="access-title">Reports</label>
                                        <asp:CheckBox ID="chkDashboard" runat="server" Text="Dashboard" />
                                        <asp:CheckBox ID="chkSlip" runat="server" Text="Slip" />
                                        <asp:CheckBox ID="chkStockReport" runat="server" Text="StockReport" />
                                        <asp:CheckBox ID="chkPurchaseReport" runat="server" Text="PurchaseReport" />
                                        <asp:CheckBox ID="chkInventoryReport" runat="server" Text="InventoryReport" />
                                        <asp:CheckBox ID="chkSupplierCommission" runat="server" Text="SupplierCommission" />
                                        <asp:CheckBox ID="chkTransaction" runat="server" Text="Transaction" />
                                        <asp:CheckBox ID="chkProfitLoss" runat="server" Text="ProfitLoss" />
                                        <asp:CheckBox ID="chkAnalytic" runat="server" Text="Analytic" />
                                        <asp:CheckBox ID="chkSummary" runat="server" Text="Summary" />
                                        <hr />
                                        <label class="access-title">Promotion</label>
                                        <asp:CheckBox ID="chkOffer" runat="server" Text="Offer" />
                                        <asp:CheckBox ID="chkSMS" runat="server" Text="SMS" />
                                        <hr />
                                        <label class="access-title">Website</label>
                                        <asp:CheckBox ID="chkWeb" runat="server" Text="Web" CssClass="disNone" />
                                        <asp:CheckBox ID="chkEcommerce" runat="server" Text="Ecommerce" />
                                        <hr />
                                        <label class="access-title">Records</label>
                                        <asp:CheckBox ID="chkWarehouse" runat="server" Text="Store" />
                                        <asp:CheckBox ID="ckhManufacturer" runat="server" Text="Manufacturer" />
                                        <asp:CheckBox ID="chkLocation" runat="server" Text="Location" />
                                        <asp:CheckBox ID="chkSupplier" runat="server" Text="Supplier" />
                                        <asp:CheckBox ID="chkCategory" runat="server" Text="Category" />
                                        <asp:CheckBox ID="chkUnitMeasurement" runat="server" Text="UnitMeasurement" />
                                        <asp:CheckBox ID="chkField" runat="server" Text="Field" />
                                        <asp:CheckBox ID="chkAttribute" runat="server" Text="Attribute" />
                                        <asp:CheckBox ID="chkBank" runat="server" Text="Bank" />
                                        <asp:CheckBox ID="chkCard" runat="server" Text="Card" />
                                        <asp:CheckBox ID="chkParticular" runat="server" Text="Particular" />
                                        <asp:CheckBox ID="chkStaff" runat="server" Text="Staff" />
                                        <asp:CheckBox ID="chkServiceType" runat="server" Text="ServiceType" />

                                        <hr />
                                        <label class="access-title">Setting</label>
                                        <asp:CheckBox ID="chkSubscription" runat="server" Text="Subscription" />
                                        
                                        <hr />
                                        <label class="access-title">Offline</label>
                                        <asp:CheckBox ID="chkOffline" runat="server" Text="Offline" />

                                        <hr />
                                        <label class="access-title">Operations</label>
                                        <asp:CheckBox ID="chkAdd" runat="server" Text="Add" />
                                        <asp:CheckBox ID="chkEdit" runat="server" Text="Edit" />
                                        <asp:CheckBox ID="chkDelete" runat="server" Text="Delete" />


                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccessPage" runat="server" Text='<%# Bind("accessPage") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Access To" SortExpression="accessTo" ItemStyle-CssClass="chkDesign" ControlStyle-Width="330px ">
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkDisplayBuyPrice" runat="server" Text="DisplayBuyPrice" />
                                        <asp:CheckBox ID="chkDisplayWholesalePrice" runat="server" Text="DisplayWholesalePrice" />
                                        <asp:CheckBox ID="chkDisplaySalePrice" runat="server" Text="DisplaySalePrice" />
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:CheckBox ID="chkDisplayBuyPrice" runat="server" Text="DisplayBuyPrice" />
                                        <asp:CheckBox ID="chkDisplayWholesalePrice" runat="server" Text="DisplayWholesalePrice" />
                                        <asp:CheckBox ID="chkDisplaySalePrice" runat="server" Text="DisplaySalePrice" />
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccessTo" runat="server" Text='<%# Bind("accessTo") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Entry Date" SortExpression="entryDate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd/MMM/yyyy}") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd/MMM/yyyy}") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update Date" SortExpression="updateDate" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtUpdateDate" runat="server" Text='<%# Bind("updateDate", "{0:dd/MMM/yyyy}") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtUpdateDate" runat="server" Text='<%# Bind("updateDate", "{0:dd/MMM/yyyy}") %>'></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblUpdateDate" runat="server" Text='<%# Bind("updateDate", "{0:dd/MMM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Monthly Fee" SortExpression="monthlyFee" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtMonthlyFee" runat="server" Text='<%# Bind("monthlyFee") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtMonthlyFee" runat="server" Text=''></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonthlyFee" runat="server" Text='<%# Bind("monthlyFee") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Expiry Date" SortExpression="expiryDate" Visible="False">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtExpiryDate" CssClass=" datepickerCSS form-control" runat="server" Text='<%# Bind("expiryDate","{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="datepickerCSS" Text=''></asp:TextBox>
                                    </InsertItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpiryDate" runat="server" Text='<%# Bind("expiryDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField ShowHeader="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="lnkBtnUpdate" runat="server" CausesValidation="True" CssClass="btn btn-default btn-sm btnSizeF" CommandName="Update" Text="Save1"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnCancel" data-dismiss="modal" OnClientClick=" reloadThisPage(); " runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" CssClass="btn btn-default btn-sm btnSize"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <InsertItemTemplate>
                                        <asp:LinkButton ID="lnkBtnInsert" runat="server" CausesValidation="True" CssClass="btn btn-default btn-sm btnSizeF" CommandName="Insert" Text="Save"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnCancel" data-dismiss="modal" OnClientClick=" reloadThisPage(); " runat="server" CausesValidation="False" CssClass="btn btn-default btn-sm btnSize" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                                    </InsertItemTemplate>
                                    <%--<ItemTemplate>
                                        <asp:LinkButton ID="lnkBtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" CssClass="disNone"></asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnNew" runat="server" CausesValidation="False" CommandName="New" Text="New"  CssClass="disNone"></asp:LinkButton>
                                    </ItemTemplate>--%>
                                </asp:TemplateField>

                            </Fields>
                            <PagerStyle CssClass="pgr " />
                            <AlternatingRowStyle CssClass="alt" />
                        </asp:DetailsView>

                       <%-- <asp:SqlDataSource ID="dtsRoleInfoDtl" runat="server" ConnectionString="<%$ ConnectionStrings:dbPOS %>"
                            InsertCommand="INSERT INTO [RoleInfo] ([roleID], [title], [password], [email], [branchId], [accessPage], [accessTo], [entryDate], [updateDate], [groupId], [userRight], [version] ,[domainName],[isDomainActive],[branchLimit],[userLimit],[branchType],[inheritId],[storeId],[monthlyFee],[ExpiryDate]) VALUES (@lblTopId, @title, @lblPassword, @email, @lblSearchBranchId, @lblAccessPageList, @lblAccessToList, @lblCurrentDate, @lblCurrentDate, @lblSearchRoleId, @lblSearchUserRight, @lblVersion, @lblDomainName,@lblIsDomainActive,@lblBranchLimit, @lblUserLimit,@lblBranchType,@lblInheritId,@lblStoreId,@lblMonthlyFee,@lblExpiryDate)"
                            SelectCommand="SELECT * FROM [RoleInfo] ORDER BY [roleID] DESC"
                            UpdateCommand="UPDATE [RoleInfo] SET [title] = @title, [password] = @lblPassword, [email] = @lblEmailUpdate, [accessPage] = @lblAccessPageList, [accessTo] = @lblAccessToList, [updateDate] = @lblCurrentDate,verify=@lblIsVerify, domainName=@lblDomainName,isDomainActive=@lblIsDomainActive,branchLimit=@lblBranchLimit, userLimit=@lblUserLimit,branchType=@lblBranchType,inheritId=@lblInheritId,storeId=@lblStoreId,monthlyFee=@lblMonthlyFee,ExpiryDate=@lblExpiryDate WHERE [roleID] = @roleID">
                            <InsertParameters>
                                <asp:Parameter Name="title" Type="String" />
                                <asp:Parameter Name="password" Type="String" />
                                <asp:Parameter Name="descr" Type="String" />
                                <asp:Parameter Name="email" Type="String" />
                                <asp:Parameter Name="accessPage" Type="String" />
                                <asp:Parameter Name="accessTo" Type="String" />
                                <asp:Parameter Name="entryDate" Type="DateTime" />
                                <asp:Parameter Name="updateDate" Type="DateTime" />
                                <asp:ControlParameter ControlID="lblTopId" Name="lblTopId" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblCurrentDate" Name="lblCurrentDate" PropertyName="Text" Type="DateTime" DefaultValue="1-1-2015" />
                                <asp:ControlParameter ControlID="lblPassword" Name="lblPassword" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblAccessPageList" Name="lblAccessPageList" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblAccessToList" Name="lblAccessToList" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchBranchId" Name="lblSearchBranchId" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchRoleId" Name="lblSearchRoleId" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblSearchUserRight" Name="lblSearchUserRight" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblVersion" Name="lblVersion" PropertyName="Text" Type="Decimal" />
                                <asp:ControlParameter ControlID="lblDomainName" Name="lblDomainName" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblIsDomainActive" Name="lblIsDomainActive" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblBranchLimit" Name="lblBranchLimit" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblUserLimit" Name="lblUserLimit" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblBranchType" Name="lblBranchType" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblInheritId" Name="lblInheritId" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblStoreId" Name="lblStoreId" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblMonthlyFee" Name="lblMonthlyFee" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblExpiryDate" Name="lblExpiryDate" PropertyName="Text" Type="String" />
                            </InsertParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="title" Type="String" />
                                <asp:Parameter Name="password" Type="String" />
                                <asp:Parameter Name="descr" Type="String" />
                                <asp:Parameter Name="accessPage" Type="String" />
                                <asp:Parameter Name="accessTo" Type="String" />
                                <asp:Parameter Name="entryDate" Type="DateTime" />
                                <asp:Parameter Name="updateDate" Type="DateTime" />
                                <asp:Parameter Name="roleID" Type="Int32" />
                                <asp:ControlParameter ControlID="lblCurrentDate" Name="lblCurrentDate" PropertyName="Text" Type="DateTime" DefaultValue="1-1-2015" />
                                <asp:ControlParameter ControlID="lblPassword" Name="lblPassword" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblAccessPageList" Name="lblAccessPageList" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblAccessToList" Name="lblAccessToList" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblEmailUpdate" Name="lblEmailUpdate" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblIsVerify" Name="lblIsVerify" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblDomainName" Name="lblDomainName" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblBranchLimit" Name="lblBranchLimit" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblUserLimit" Name="lblUserLimit" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblIsDomainActive" Name="lblIsDomainActive" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblBranchType" Name="lblBranchType" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblInheritId" Name="lblInheritId" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblStoreId" Name="lblStoreId" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblMonthlyFee" Name="lblMonthlyFee" PropertyName="Text" Type="String" />
                                <asp:ControlParameter ControlID="lblExpiryDate" Name="lblExpiryDate" PropertyName="Text" Type="String" />
                            </UpdateParameters>
                        </asp:SqlDataSource>--%>
                        <%--InsertCommand="INSERT INTO [RoleInfo] ([roleID], [title], [password], [email], [branchId], [accessPage], [accessTo], [entryDate], [updateDate], [groupId], [userRight], [version] ,[domainName],[isDomainActive],[branchLimit],[userLimit]) VALUES (@lblTopId, @title, @lblPassword, @email, @lblSearchBranchId, @lblAccessPageList, @lblAccessToList, @lblCurrentDate, @lblCurrentDate, @lblSearchRoleId, @lblSearchUserRight, @lblVersion, @lblDomainName,@lblIsDomainActive,@lblBranchLimit, @lblUserLimit)"--%>

                    </div>
                </div>
                <div style="clear: both"></div>
                <div class="modal-footer" runat="server" id="modalFooter">
                    <button type="submit" class="btn btn-lg btnCustomize" data-dismiss="modal"><%=Resources.Language.Btn_user_close %></button>
                </div>
            </div>
        </div>
    </div>

    <script>

        $(document).ready(function () {

        });


        $('#contentBody_dtlRoleInfo_ddlBranchType').on("change", function () {
          
        });


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        function EndRequestHandler(sender, args) {
            if (args.get_error() != undefined) {
                args.set_errorHandled(true);
            }
        }

    </script>


    <script>
        activeModule = "settings";
    </script>

</asp:Content>
