<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Receive.aspx.cs" Inherits="MetaPOS.Admin.AccountBundle.View.Receive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Receive Page
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Transaction.css" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Admin/AccountBundle/Content/Receive-responsive.css?v=0.001") %>" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12 margin-top-10">
            <div class="section">
                <h2 class="sectionBreadcrumb"><%=Resources.Language.Title_receive %></h2>
            </div>
        </div>
        <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
    </div>
    <div class="row headerDesign">
        <asp:Panel ID="pnlCashIn" runat="server" DefaultButton="btnCashInAdd">
            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading"><%=Resources.Language.Lbl_receive_add_receive %></h2>
                    <div class="form-group disNone" id="cashintype" runat="server">
                        <asp:Label ID="lblCashInType" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_particular %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashInType" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlCashInType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="Receive">Receive</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group" id="divCashInTypeSup" runat="server">
                        <asp:Label ID="lblCashInTypeSup" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_particular %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashInTypeSup" CssClass="form-control" runat="server" ></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashInTypeStaff" runat="server" visible="false">
                        <asp:Label ID="lblCashInTypeStaff" CssClass="lbl col-sm-4 control-label lang-accounting-staff" runat="server" Text="Staff"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashInTypeStaff" CssClass="form-control" runat="server" OnTextChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashInMainDescr" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_description %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashInMainDescr" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashInAmount" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_amount %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashInAmount" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCashInAmount" runat="server" ControlToValidate="txtCashInAmount" ErrorMessage="Invalid!" ValidationExpression="^[\-]?[0-9]{1,8}([\.][0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label3" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_recived_date %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtRecivedDate" CssClass="form-control datepickerCSS" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="padding-bottom-74">
                        <asp:Button ID="btnCashInAdd" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddCustom btnAddOpt" runat="server" Text="<%$Resources:Language, Btn_receive_add %>" OnClick="btnCashInAdd_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="cashClear"></div>
        <asp:Panel ID="pnlCashOut" runat="server" DefaultButton="btnCashOutAdd" Visible="false">
            <div class="col-md-0 col-sm-0 col-xs-0 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading"><%=Resources.Language.Lbl_receive_add_expense %> </h2>
                    <div class="form-group">
                        <asp:Label ID="lblCashOutType" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_particular %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutType" CssClass=" form-control" runat="server" OnSelectedIndexChanged="ddlCashOutType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutTypeSup" runat="server" visible="false">
                        <asp:Label ID="lblCashOutTypeSup" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_supplier %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutTypeSup" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutTypeStaff" runat="server" visible="false">
                        <asp:Label ID="lblCashOutTypeStaff" CssClass="lbl col-sm-4 control-label lang-accounting-staff" runat="server" Text="Staff"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashOutTypeStaff" CssClass="form-control" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divCashOutDescriptions" runat="server" visible="true">
                        <asp:Label ID="lblCashOutMainDescr" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_description %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashOutMainDescr" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashOutAmount" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_amount %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtCashOutAmount" CssClass=" form-control" runat="server"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="revCashOutAmount" runat="server" ControlToValidate="txtCashOutAmount" ErrorMessage="Invalid!" ValidationExpression="^[\-]?[0-9]{1,8}([\.][0-9]{1,2})?$" Display="Dynamic" CssClass="crStyle"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnCashOutAdd" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnAddCustom btnAddOpt" runat="server" Text="<%$Resources:Language, Btn_receive_add %>" OnClick="btnCashOutAdd_Click" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="cashClear"></div>
        <asp:Panel ID="pnlSearch" runat="server" DefaultButton="btnCashSearch">
            <div class="col-md-6 col-sm-12 col-xs-12 form-horizontal">
                <div class="stockfieldHeight singleCashReportField section">
                    <h2 class="sectionHeading"><%=Resources.Language.Lbl_receive_search %></h2>
                    
                    <div class="form-group" id="divStoreList" runat="server">
                        <asp:Label ID="Label2" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_store %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlStoreList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlStoreList_OnSelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group"  runat="server" id="divUserList">
                        <asp:Label ID="Label9" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_user %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList runat="server" ID="ddlUserList" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlUserList_OnSelectedIndexChanged"/>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCashSearchType" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_particular %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlCashSearchType" CssClass=" form-control" runat="server" OnSelectedIndexChanged="ddlCashSearchType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divSearchSup" runat="server" visible="false">
                        <asp:Label ID="lblSearchSup" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_supplier %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSearchSup" CssClass="form-control" runat="server" OnSelectedIndexChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group" id="divSearchStaff" runat="server" visible="false">
                        <asp:Label ID="lblSearchStaff" CssClass="lbl col-sm-4 control-label" runat="server" Text="Staff"></asp:Label>
                        <div class="col-sm-8">
                            <asp:DropDownList ID="ddlSearchStaff" CssClass="form-control" runat="server" OnSelectedIndexChanged="btnCashSearch_Click" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSearchDateFrom" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_from %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSearchDateFrom" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblSearchDateTo" CssClass="lbl col-sm-4 control-label" runat="server" Text="<%$Resources:Language, Lbl_receive_to %>"></asp:Label>
                        <div class="col-sm-8">
                            <asp:TextBox ID="txtSearchDateTo" CssClass="form-control datepickerCSS" runat="server" AutoPostBack="true" OnTextChanged="btnCashSearch_Click"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group padding-bottom-6">
                        <asp:CheckBox runat="server" ID="chkAnyDate" Text="<%$Resources:Language, Lbl_receive_show_all_track_received_amount %>" OnCheckedChanged="chkAnyDate_OnCheckedChanged" CssClass="chk-supplier-received" AutoPostBack="True" />
                        <asp:Button ID="btnCashSearch" CssClass="btn btn-info btn-sm CRBtnDesign btnResize btnSearchCustom" runat="server" Text="<%$Resources:Language, Btn_receive_search %>" OnClick="btnCashSearch_Click" />
                    </div>


                </div>
            </div>
        </asp:Panel>
        <div class="cashClear"></div>
    </div>
    <div style="clear: both"></div>
    <div class="row gridTopButton">
        <div class="btn-group btnGroupDesignLeft" role="group">
            <asp:Button ID="btnAddPaticular" CssClass="btn btn-info btnAddCustom btnAddOpt" runat="server" Text="<%$Resources:Language, Btn_receive_add_particular %>" OnClick="btnAddPaticular_Click" />
        </div>
        <%--<div class="btn-group btnGroupDesign" role="group">
            <asp:Button ID="btnPrint" CssClass="btn btn-primary btnPrintCustom" runat="server" Text="Print" OnClick="btnPrint_Click" />
        </div>--%>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-md-3 col-sm-4 col-xs-3 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_receive_receive_report %></label>
                </div>
                <div class="col-md-9 col-sm-8 col-xs-9 gridTitle text-right form-inline">
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblTrackAmt" CssClass="control-label text-capitalize text-primary msg-report"></asp:Label>
                        <asp:Label runat="server" ID="lblTotalReceive" CssClass="control-label text-capitalize text-primary msg-report"></asp:Label>
                        <asp:Label runat="server" ID="lblBalance" CssClass="control-label text-capitalize text-primary msg-report"></asp:Label>
                    </div>
                    <div class="form-group float-left">
                        <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="grdCashReportInfo" OnRowDataBound="grdCashReportInfo_RowDataBound"
                OnRowDeleted="grdCashReportInfo_RowDeleted" OnRowDeleting="grdCashReportInfo_RowDeleting"
                CssClass="mGrid gBox scrollBar" ShowFooter="true" AllowPaging="True" PageSize="10" runat="server"
                AutoGenerateColumns="False" DataKeyNames="Id" OnPageIndexChanging="grdCashReportInfo_OnPageIndexChanging"
                EmptyDataText="There are no data records to display." ViewStateMode="Enabled"
                RowStyle-Wrap="true" AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblId" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_sl %>">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_source %>" SortExpression="cashType">
                        <ItemTemplate>
                            <asp:Label ID="lblCashType" runat="server" Text='<%# Bind("cashType") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_particular %>" SortExpression="descr">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Particular") %>'></asp:Label>
                            <asp:Label ID="lblDescr" runat="server" Text='<%# Bind("CustomerName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_details %>" SortExpression="mainDescr" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lblMainDescr" runat="server" Text='<%# Bind("mainDescr") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblMainDescr" runat="server" Text="<%$Resources:Language, Lbl_receive_total %>"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_received_amt %>" SortExpression="cashIn">
                        <ItemTemplate>
                            <asp:Label ID="lblCashIn" runat="server" Text='<%# Bind("cashIn") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalCashIn" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_track_amt %>" SortExpression="trackAmt" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblTrackAmt" runat="server" Text='<%# Bind("trackAmt") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalTrackAmt" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash-Out" SortExpression="cashOut" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCashOut" runat="server" Text='<%# Bind("cashOut") %>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalCashOut" runat="server" Text=""></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Cash-In-Hand" SortExpression="cashInHand" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCashInHand" runat="server" Text='<%# Bind("cashInHand") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_date %>" SortExpression="entryDate" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_receive_store %>" SortExpression="storeName">
                        <ItemTemplate>
                            <asp:Label ID="lblStoreName" runat="server" Text='<%# Bind("storeName") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ShowHeader="False" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDelete" CssClass="btn  btn-design btnDeleteOpt" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
        </div>
    </div>

    <script>
        activeModule = "accounting";
    </script>

</asp:Content>
