<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="SMS.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.SMS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    SMS - metaPOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <%--<link href="PromotionBundle/Content/promotion.css" rel="stylesheet" />--%>
    <link href="<%= ResolveUrl("~/Admin/PromotionBundle/Content/promotion.css") %>" rel="stylesheet" />
    <script src="<%= ResolveUrl("~/Admin/PromotionBundle/Script/sms.js") %>"></script>
    <script type="text/javascript" src="~/Js/MaxLength.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <asp:HiddenField ID="getMsgCost" runat="server" />

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divMessageHistory">
                <label class="title">SMS</label>
                <a id="btnDisplayMessageHistory" href="/Admin/PromotionBundle/View/SmsLog.aspx" class="btn btn-success btn-sm btnResize btnAddCustom btn-space">SMS History</a>
                <%--<a id="btnConfig" href="/Admin/PromotionBundle/View/SmsConfig.aspx" class="btn btn-danger btn-sm btnResize btnAddCustom">Sms Config</a>--%>
            </div>

            <div class="sms-tab">
                <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>


                <!-- Tab panes -->
                <div class="tab-content sms-content">
                    <div role="tabpanel" class="tab-pane active" id="Contact">
                        <div class="row">

                            <div class="col-md-6">
                                <div class="gridHeader">
                                    <div class="col-md-4 gridTitle text-left">
                                        <label>Contact List</label>
                                    </div>
                                    <div class="col-md-8 gridTitle text-right form-inline">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged">
                                                <asp:ListItem Value="">All</asp:ListItem>
                                                <asp:ListItem Value="0">Customer</asp:ListItem>
                                                <asp:ListItem Value="1">Staff</asp:ListItem>
                                                <asp:ListItem Value="2">Supplier</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" placeholder="Search..." AutoPostBack="true" OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                                    <ContentTemplate>
                                        <!-- Contact list -->
                                        <div class="ContactList">
                                            <asp:GridView ID="gvContactList"
                                                runat="server"
                                                AutoGenerateColumns="False"
                                                CssClass="mGrid gBox"
                                                DataKeyNames="Id"
                                                OnPageIndexChanging="gvContactList_PageIndexChanging"
                                                EmptyDataText="No data records found."
                                                EnableViewState="false"
                                                ViewStateMode="Enabled">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="10%">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" onclick="Check_Click(this);" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SL" ItemStyle-Width="15%">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" Visible="false"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Name" SortExpression="name">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Bind("name") %>' ID="Label3"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30%"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Phone" SortExpression="phone">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Bind("phone") %>' ID="lblPhone"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="entryDate" SortExpression="entryDate" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Bind("entryDate", "{0:dd-MMM-yyyy}") %>' ID="Label1"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblTotalSelect" runat="server" Text="0"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle Width="15%"></ItemStyle>
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="pgr" />
                                                <AlternatingRowStyle CssClass="alt" />
                                            </asp:GridView>
                                        </div>


                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:PostBackTrigger ControlID="gvContactList" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-6">
                                <!-- SMS Text -->
                                <div class="gridHeader">
                                    <div class="sms-text gridTitle">

                                        <div class="form-group">

                                            <label id="showMedium"></label>
                                            <div class="pull-right">
                                                <span> <b><asp:Label runat="server" ID="showBalance"></asp:Label> BDT</b></span>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <div style="float: left">
                                                <label>Write your message</label>

                                            </div>

                                            <span id="lblCount" style="float: right"></span>
                                            <span id="lblCharacterCount" class="lblForSpace" style="float: right"></span>
                                            <asp:TextBox ID="txtSMSText" runat="server" TextMode="MultiLine" CssClass="form-control " Rows="5"></asp:TextBox>
                                        </div>

                                        <asp:Button runat="server" ID="btnSend" CssClass="btn btn-primary btn-lg pull-right" Text="Send SMS" OnClick="btnSend_Click" OnClientClick=" return initialPageLoad(); " />
                                        <asp:Label runat="server" ID="lblSendCount" Text=""></asp:Label>
                                        <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label>

                                        <span id="checkedPhoneCount" style="float: left" class="lblForSpace"></span>
                                        <%-- <span id="lblShowCost" style="float: left"></span>--%>
                                        <br />
                                        <br />
                                        <div class="pull-left">
                                           
                                            <asp:Label runat="server" ID="lblShowCost" CssClass=""> </asp:Label>
                                        </div>
                                        <br />
                                        <br />
                                        <div class="pull-left">
                                            <b class="sms-rate"><i><span>Rate: 
                                            <asp:Label runat="server" ID="lblSmSRate" CssClass=""> </asp:Label> /sms</span></i></b>
                                        </div>
                                        <br />
                                        <br />
                                        <span id="showErrorMsg" style="float: right"></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div role="tabpanel" class="tab-pane fade" id="History">Slow panda</div>
                </div>

            </div>
        </div>
    </div>
    <script type="text/javascript">

        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type === "checkbox" && objRef !== inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "#3498db";
                        row.style.color = "red";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "#F2F6FF";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }


        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "#3498db";
                row.style.color = "red";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#C2D69B";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }


        //Max count in Message
        //$(window).load(function() {
        //    $(function() {
        //        //Normal Configuration
        //        //$("[id*=txtSMSText]").MaxLength({ MaxLength: 160 });

        //        //Specifying the Character Count control explicitly
        //        $("[id*=txtSMSText]").MaxLength(
        //            {
        //                MaxLength: 1000,
        //                CharacterCountControl: $('#lblCount')
        //            });

        //    });
        //});





    </script>

    <script>
        activeModule = "promotion";
    </script>

</asp:Content>
