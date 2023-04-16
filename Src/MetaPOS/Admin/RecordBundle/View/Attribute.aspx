<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Attribute.aspx.cs" Inherits="MetaPOS.Admin.RecordBundle.View.Attribute" %>
<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Attributes
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Admin/RecordBundle/Content/record.css?v=0.001?v=0.001") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divHeaderPanel">
                <label class="title"><%=Resources.Language.Title_attribute %></label>
                <a id="btnSaveModal" class="btn btn-info btn-sm btnResize btnAddCustom subbranch-hide" data-toggle="modal" data-backdrop="static"><%=Resources.Language.Btn_attribute_add_attribute %></a>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divSearchPanel">
                <div class="col-md-4 col-sm-4 col-xs-4 gridTitle text-left">
                    <label><%=Resources.Language.Lbl_attribute_attribute_list %></label>
                </div>
                <div class="col-md-8 col-sm-8 col-xs-8 gridTitle text-right form-inline" id="filterPanel">
                    <div class="form-group">
                        <select name="ddlActiveStatus" id="ddlActiveStatus" class="form-control">
                            <option value="1"><%=Resources.Language.Lbl_attribute_active %></option>
                            <option value="0"><%=Resources.Language.Lbl_attribute_non_active %></option>
                        </select>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="card" id="divListPanel">
                <table id="dataListTable" class="table table-striped table-bordered">
                    <thead>
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                    </tr>
                    </thead>
                    <tfoot>
                    <tr>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                        <th></th>
                    </tr>
                    </tfoot>
                </table>
            </div>
        </div>

        <div class="modal fade" id="formModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <h4 class="modal-title" id="myModalLabel"></h4>
                        <label id="lblActionText" class="disNone"></label>
                    </div>
                    <div class="modal-body">
                        <div id="msgOutput" class="text-left"></div>
                        <div class="row">
                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_attribute_field_name %></label>
                                <div class="col-md-8">
                                    <select class="form-control" id="ddlField"></select>
                                </div>
                            </div>

                            <div class="form-group group-section">
                                <label class="col-md-4"><%=Resources.Language.Lbl_attribute_attribute_name %></label>
                                <div class="col-md-8">
                                    <input name="attributeName" type="text" id="attributeName" class="form-control"/>
                                </div>
                            </div>                          
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal" id="btnClose"><%=Resources.Language.Btn_attribute_close %></button>
                        <button type="button" id="btnSave" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_attribute_save %></button>
                        <button type="button" id="btnUpdate" class="btn btn-info btn-sm btnResize btnAddCustom"><%=Resources.Language.Btn_attribute_update %></button>
                    </div>
                </div>
            </div>
        </div>

    </div>
    
    <asp:HiddenField ID="lblHiddenCompanyName" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyAddress" runat="server"/>
    <asp:HiddenField ID="lblHiddenCompanyPhone" runat="server"/>

    <script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-main.js") %>"></script>
    <script src="<%= ResolveUrl("~/Admin/RecordBundle/Script/record-attribute.js?v0.001") %>"></script>

    <script>
        activeModule = "record";

        var ID = "<% =Resources.Language.Lbl_attribute_id %>";
        var Attribute_Name = "<% =Resources.Language.Lbl_attribute_attribute_name %>";
        var Field_Name = "<% =Resources.Language.Lbl_attribute_field_name %>";
        var Action = "<% =Resources.Language.Lbl_attribute_action %>";
        var Add_attribute = "<% =Resources.Language.Lbl_attribute_add_attribute %>";
        var Edit_attribute = "<% =Resources.Language.Lbl_attribute_edit_attribute %>";
        var Select_a_Field = "<% =Resources.Language.Lbl_attribute_select_a_field %>";
    </script>

</asp:Content>



<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Attribute.aspx.cs" Inherits="MetaPOS.Admin.Attribute" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Attribute Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Category.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Attribute</h2>
                <asp:Label ID="lblTest" runat="server"></asp:Label>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12  form-inline">
            <div class="form-group">
                <asp:Panel ID="pnlCat" runat="server" DefaultButton="btnattribute">
                    <div class="form-group">
                        <asp:DropDownList ID="ddlFieldList" runat="server" DataTextField="field" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
                        <asp:SqlDataSource runat="server" ID="dsFieldList" ConnectionString='<%$ ConnectionStrings:dbPOS %>' SelectCommand="SELECT [Id], [field], [visible], [active] FROM [FieldInfo] WHERE [active] = '1' AND [visible] = '1'"></asp:SqlDataSource>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtAttribute" placeholder="Enter attribute" runat="server" CssClass="form-control catTextDownSpace"></asp:TextBox>
                        <asp:Button ID="btnAttribute" runat="server" Text="Add" CssClass="btn btn-info btn-sm btnResize btnAddCustom btnAddOpt" OnClick="btnAttribute_Click" />
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="gridHeader">
                <div class="col-md-4 gridTitle text-left">
                    <label>Attribute List</label>
                </div>
                <div class="col-md-8 gridTitle text-right form-inline">
                    <div class="form-group">
                        <asp:TextBox ID="txtSearchByAttribute" CssClass="search form-control" placeholder="Search..." runat="server" AutoPostBack="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:DropDownList ID="ddlActiveStatus" CssClass="form-control" runat="server" AutoPostBack="true">
                            <asp:ListItem Value="1">Active</asp:ListItem>
                            <asp:ListItem Value="0">Non-Active</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <asp:LinkButton runat="server" ID="btnPrint" CssClass="print" OnClick="btnPrint_Click" Visible="False"><i class="fa fa-print fa-2x"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-12 scroll">
            <asp:Label ID="lblCurrentDate" runat="server" Text="Label" Visible="false"></asp:Label>
            <asp:GridView ID="grdAttributeInfo" 
                          OnRowEditing="grdAttributeInfo_RowEditing" 
                          OnRowUpdated="grdAttributeInfo_RowUpdated" 
                          OnRowUpdating="grdAttributeInfo_RowUpdating" 
                          OnRowDeleting="grdAttributeInfo_RowDeleting" 
                          OnRowDataBound="grdAttributeInfo_RowDataBound"
                          CssClass="mGrid gBox" runat="server" 
                          AutoGenerateColumns="False" DataKeyNames="Id" 
                          DataSourceID="dsgrdAttributeInfo" 
                          EmptyDataText="There are no data records to display." 
                          AllowPaging="True" Pageattribute="10" EnableViewState="false" 
                          onkeydown="return (event.keyCode!=13)">
                <Columns>
                    <asp:TemplateField HeaderText="SL" ShowHeader="true">
                        <ItemTemplate>
                            <%#Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Id" InsertVisible="False" SortExpression="Id" Visible="False">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Id") %>' ID="lblAttId"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Field Name" SortExpression="fieldId">
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("Field") %>' ID="Label1"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Attribute Name" SortExpression="attributeName">
                        <EditItemTemplate>
                            <asp:TextBox runat="server" Text='<%# Bind("attributeName") %>' ID="txtName"></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label runat="server" Text='<%# Bind("attributeName") %>' ID="lblName"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit" ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="btnGrdEdit" runat="server" CssClass="CatDynamicUpCan btnEditOpt" CausesValidation="False" CommandName="Update" Text="Update"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="CatDynamicUpCan" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdEdit" runat="server" CssClass="btn btn-design2 btnEditOpt" CausesValidation="False" CommandName="Edit" Text="<span class='glyphicon glyphicon-pencil'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnGrdDelete" CssClass="btn btn-design2 btnDeleteOpt" OnClientClick=" return confirm('Are you sure you want to delete selected record ?') " runat="server" CausesValidation="False" CommandName="Delete" Text="<span class='glyphicon glyphicon-trash'></span>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle CssClass="pgr" />
                <AlternatingRowStyle CssClass="alt" />
            </asp:GridView>

            <asp:SqlDataSource ID="dsgrdAttributeInfo" runat="server" ConnectionString="<%$ ConnectionStrings:dbPOS %>"
                               CancelSelectOnNullParameter="false"
                               DeleteCommand="DELETE FROM [AttributeInfo] WHERE [Id] = 0"
                               UpdateCommand="UPDATE [AttributeInfo] SET [attributeName] = @attributeName, [updateDate] = @lblCurrentDate WHERE [Id] = @Id">
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                </DeleteParameters>
                <UpdateParameters>
                    <asp:Parameter Name="attributeName" Type="String"></asp:Parameter>
                    <asp:Parameter Name="Id" Type="Int32"></asp:Parameter>
                    <asp:ControlParameter ControlID="lblCurrentDate" Name="lblCurrentDate" PropertyName="Text" Type="DateTime" DefaultValue="1-1-2015" />
                </UpdateParameters>
            </asp:SqlDataSource>
        </div>
    </div>
    
    <script>
        activeModule = "record";
    </script>

</asp:Content>--%>