<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="MetaPOS.Controller.Footer" %>

<asp:PlaceHolder ID="PlaceHolder1" runat="server">

    <!--footer-->
    <div class="footer" runat="server" id="FooterControl">
        <div class="container">
            <div class="clearfix"></div>
            <div class="footer-bottom" runat="server" visible="false">
                <div class=" address">
                    <div class="col-md-4 fo-grid1">
                        <p><i class="fa fa-home" aria-hidden="true"></i> <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label></p>
                    </div>
                    <div class="col-md-4 fo-grid1">
                        <p><i class="fa fa-phone" aria-hidden="true"></i> <asp:Label ID="lblContact" runat="server" Text="Label"></asp:Label></p>
                    </div>
                    <div class="col-md-4 fo-grid1">
                        <p><i class="fa fa-envelope-o" aria-hidden="true"></i><asp:Label ID="lblEmailAddress" runat="server" Text="Label"></asp:Label></p>
                    </div>
                    <div class="clearfix"></div>

                </div>
            </div>
            <div class="copy-right">
                <p>&copy; <span runat="server" id="copyRightYear"></span> <asp:Label ID="lblCompanyName" runat="server" Text="Label"></asp:Label>. All Rights Reserved</p>
                <p class="text-center">Developed by  <a href="http://metakave.com.bd/" target="_blank"><span style="color: #FAB005;">MetaKave Bangladesh</span></a></p>
            </div>
        </div>
    </div>
    <!-- //footer-->
</asp:PlaceHolder>