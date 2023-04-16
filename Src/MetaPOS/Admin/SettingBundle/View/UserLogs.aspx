<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="UserLogs.aspx.cs" Inherits="MetaPOS.Admin.SettingBundle.View.UserLogs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    User logs
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row ">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title" id="dynamicSectionBreadCrumb" runat="server">User Logs</label>
                <asp:Label runat="server" ID="lblTest" BackColor="red" Text=""></asp:Label>
            </div>
        </div>
    </div>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="section">
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>--%>
    
    <div class="row">
        <div class="col-md-12 section">
            <div class="col-md-6">
            <div class="form-group" runat="server" id="divUserLogs">
                <div class="row">
                    <label class="col-md-4">User:</label>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlUserLogsList" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUserLogsList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        </div>
    </div> 
    

    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-xs-4 gridTitle text-left">
                    <label class="lang-inventory-stock-report"><%=Resources.Language.Lbl_return_stock_report %></label>
                </div>
                <div class="col-xs-8 gridTitle text-right form-inline">
                    <div class="form-group">
                        <%--<asp:LinkButton runat="server" ID="btnPrint" ValidationGroup="validGroup" CssClass="print" OnClick="btnPrint_Click"><i class="fa fa-print fa-2x"></i></asp:LinkButton>--%>
                    </div>

                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:GridView ID="grdUserLogsStatus" 
                ShowFooter="true" AllowPaging="true" PageSize="10" runat="server"
                CssClass="mGrid gBox scrollBar" AutoGenerateColumns="False" DataKeyNames="Id"
                EmptyDataText="There are no data records to display." ViewStateMode="Enabled">
                <Columns>
                    <asp:TemplateField HeaderText="Id" SortExpression="Id" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblID" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="<%$Resources:Language, Lbl_return_sl %>">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="roleId" SortExpression="roleId" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblRoleId" runat="server" Text='<%# Bind("roleId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="branchId" SortExpression="branchId" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblBranchId" runat="server" Text='<%# Bind("branchId") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="userRight" SortExpression="userRight" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblUserRight" runat="server" Text='<%# Bind("userRight") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email" SortExpression="email" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("email") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Ip Address" SortExpression="ipAddress">
                        <ItemTemplate>
                            <asp:Label ID="lblIpAddress" runat="server" Text='<%# Bind("ipAddress") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Login Date" SortExpression="loginDate">
                        <ItemTemplate>
                            <asp:Label ID="lblLoginDate" runat="server" Text='<%# Bind("loginDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>
        </div>
    </div>
    <script>
        activeModule = "settings";
    </script>
</asp:Content>