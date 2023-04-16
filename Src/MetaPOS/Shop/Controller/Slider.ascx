<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Slider.ascx.cs" Inherits="MetaPOS.Shop.Controller.Slider" %>

<asp:PlaceHolder ID="pnlSlider" runat="server">

    <script type="text/javascript">
        jQuery(function() {
            jQuery('.starbox').each(function() {
                var starbox = jQuery(this);
                starbox.starbox({
                    average: starbox.attr('data-start-value'),
                    changeable: starbox.hasClass('unchangeable') ? false : starbox.hasClass('clickonce') ? 'once' : true,
                    ghosting: starbox.hasClass('ghosting'),
                    autoUpdateAverage: starbox.hasClass('autoupdate'),
                    buttons: starbox.hasClass('smooth') ? false : starbox.attr('data-button-count') || 5,
                    stars: starbox.attr('data-star-count') || 5
                }).bind('starbox-value-changed', function(event, value) {
                    if (starbox.hasClass('random')) {
                        var val = Math.random();
                        starbox.next().text(' ' + val);
                        return val;
                    }
                });
            });
        });
    </script>
    <!---//End-rate---->


    <!-- Carousel
        ================================================== -->
    <div id="wowslider-container1">
        <div class="ws_images customer-slider">
            <ul>
                <asp:Repeater ID="rQuotes" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src='<%# Eval("imgView") %>' runat="server" alt="" class="img-responsive" /></li>
                    </ItemTemplate>
                </asp:Repeater>

            </ul>
        </div>
        <%--<div class="ws_bullets">
            <div>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                       <a href="#" title="01"><img src='<%# Eval("imgView") %>' runat="server" alt="" /> <span runat="server" id="lblCounter">1</span></a>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>--%>
        <div class="ws_script" style="position: absolute; left: -99%"></div>
        <div class="ws_shadow"></div>
    </div>
    <!-- /.carousel -->

    <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>

    <%--<asp:Repeater ID="rQuotes" runat="server">
        <ItemTemplate>
            <img src='<%# Eval("imgView") %>' runat="server" alt="" />
        </ItemTemplate>
    </asp:Repeater>--%>
   
    
    <script src="<%= ResolveUrl("~/Shop/Js/wowslider.js") %>"></script>
    <script src="<%= ResolveUrl("~/Js/wow.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/Js/move-top.js") %>"></script>   
    <script src="<%= ResolveUrl("~/Js/move-top.js") %>"></script>
    <script src="<%= ResolveUrl("~/Js/script.js") %>"></script>

</asp:PlaceHolder>