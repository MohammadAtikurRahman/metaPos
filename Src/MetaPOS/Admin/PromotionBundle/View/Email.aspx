<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Email.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.Email" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Email - metaPOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
   
    <link href="<%= ResolveUrl("~/Admin/PromotionBundle/Content/promotion.css") %>" rel="stylesheet" />
    <%--<script type="text/javascript" src="../Js/MaxLength.min.js"></script>
    <script src="http://cdn.tinymce.com/4/tinymce.min.js"></script>--%>
   <%-- <script src="../Js/tinymce.min.js"></script>--%>
   <%-- <script> tinymce.init({ selector: 'textarea' }); </script>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">

    <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <div id="divMessageHistory" class="btn-space">
                <label class="title">Email</label>
                <a id="btnDisplayMessageHistory" href="PromotionBundle/View/EmailLog.aspx" class="btn btn-success btn-sm btnResize btnAddCustom btn-space">Email History</a>
                <a id="btnEmailConfigSetting" href="PromotionBundle/View/EmailConfig.aspx" class="btn btn-danger btn-sm btnResize btnAddCustom ">Set Config</a>
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
                                        <label>Contact</label>
                                    </div>
                                    <div class="col-md-8 gridTitle text-right form-inline">
                                        <div class="form-group">
                                            <asp:DropDownList ID="ddlSearch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSearch_SelectedIndexChanged" >
                                                <asp:ListItem Value="">All</asp:ListItem>
                                                <asp:ListItem Value="0">Customer</asp:ListItem>                                                
                                                <asp:ListItem Value="1">Supplier</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtSearch" CssClass="form-control" runat="server" placeholder="Search..." AutoPostBack="true"  OnTextChanged="txtSearch_TextChanged"></asp:TextBox>
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

                                                    <asp:TemplateField HeaderText="Email" SortExpression="email">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" Text='<%# Bind("email") %>' ID="lblEmail"></asp:Label>
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
                                <div class="sms-text">
                        
                                 
                                 <div class="form-group">
                                        <label>Write your Email</label>
                                        <span id="lblCount" style="float: right"></span>
                                     

                                    
                                     <div id="subjectEmail">
                                        
                                         <asp:TextBox ID="emailSubject" runat="server" TextMode="MultiLine" Class="form-control aoT" Rows="2" placeholder="Subject" 
                                                      AutoComplete="off" spellcheck="true"> </asp:TextBox>
                                     </div>
                                        <asp:TextBox ID="txtEmailText" runat="server" TextMode="MultiLine" Class="form-control" Rows="8" style="direction: ltr; min-height: 90px;" placeholder="Email body"></asp:TextBox>
                                       
                                    </div>

                                    <asp:Button runat="server" ID="btnSend" CssClass="btn btn-primary btn-lg pull-right" Text="Send Email" OnClick="btnSend_Click" />

                                    <asp:Label runat="server" ID="lblSendCount" Text=""></asp:Label>
                                    <asp:Label runat="server" ID="lblMessage" Text=""></asp:Label>
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
                if (inputList[i].type === "checkbox" && objRef != inputList[i]) {
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
                        if (row.rowIndex % 2 === 0) {
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
        $(window).load(function () {
            $(function () {
                //Normal Configuration
                //$("[id*=txtSMSText]").MaxLength({ MaxLength: 160 });

                //Specifying the Character Count control explicitly
                $("[id*=txtSMSText]").MaxLength(
                    {
                        MaxLength: 160,
                        CharacterCountControl: $('#lblCount')
                    });

            });
        });

    </script>
   <%-- <script>

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function (s, e) {
            var script = document.createElement('script');
            script.src ='"<%= ResolveUrl("~/Js/tinymce.min.js") %>"';
            script.type = 'text/javascript';
            var head = document.getElementsByTagName("head")[0];
            head.appendChild(script);
        });
    </script>--%>
    <script>
        activeModule = "promotion";
    </script>

</asp:Content>