<%@ Page Title="" Language="C#" MasterPageFile="~/Shop/MasterPage.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MetaPOS.Shop.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <title runat="server">Contact Us</title>

    <script type="text/javascript">
        jQuery(document).ready(function($) {
            $(".scroll").click(function(event) {
                event.preventDefault();
                $('html, body').animate({ scrollTop: $(this.hash).offset().top }, 1000);
            });
        });
    </script>

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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="contentBody" runat="server">



    <!-- contact -->
    <div class="contact">
        <asp:Label ID="lblTest" runat="server" Text=""></asp:Label>
        <div class="container">
            <div class="spec ">
                <h3>Contact Us</h3>
                <div class="ser-t">
                    <b></b>
                    <span><i></i></span>
                    <b class="line"></b>
                </div>
            </div>
            <div class=" contact-w3">
                <div class="col-md-5 contact-right">
                    <asp:PlaceHolder ID="plhGoogleMapIfram" runat="server"></asp:PlaceHolder>
                </div>
                <div class="col-md-7 contact-left">
                    <h4>Contact Information</h4>
                    <ul class="contact-list">
                        <li><i class="fa fa-map-marker" aria-hidden="true"></i>
                            <asp:Label ID="lblShopAddress" runat="server" Text="Label"></asp:Label></li>
                    </ul>
                    <ul class="contact-list">
                        <li><i class="fa fa-envelope" aria-hidden="true"></i><a href="">
                                                                                 <asp:Label ID="lblEmailAddress" runat="server" Text="Label"></asp:Label></a></li>
                        <li><i class="fa fa-phone" aria-hidden="true"></i>
                            <asp:Label ID="lblPhone" runat="server" Text="Label"></asp:Label></li>
                    </ul>
                    <div id="container">
                        <!--Horizontal Tab-->
                        <div id="parentHorizontalTab">
                            <%--<ul class="resp-tabs-list hor_1">
							<li><i class="fa fa-envelope" aria-hidden="true"></i></li>
							<li> <i class="fa fa-map-marker" aria-hidden="true"></i> </span></li>
							<li> <i class="fa fa-phone" aria-hidden="true"></i></li>
						</ul>--%>
                            <div class="resp-tabs-container hor_1">
                                <div>
                                    <asp:TextBox ID="txtName" runat="server" placeholder="Name" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" CssClass="form-control"></asp:TextBox>
                                    <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" placeholder="Message ..." CssClass="form-control"></asp:TextBox>
                                    <asp:Button ID="btnSend" runat="server" Text="Send" CssClass="btn btn-primary" OnClick="btnSend_Click"/>


                                    <%--<form action="#" method="post">
                                        <input type="text" value="Name" name="Name" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Name';}" required="">
                                        <input type="email" value="Email" name="Email" onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Email';}" required="">
                                        <textarea name="Message..." onfocus="this.value = '';" onblur="if (this.value == '') {this.value = 'Message...';}" required="">Message...</textarea>
                                        <input type="submit" value="Submit">
                                    </form>--%>
                                </div>
                                <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                <%--<div>
								<div class="map-grid">
								<h5>Our Branches</h5>
									<ul>
										<li><i class="fa fa-arrow-right" aria-hidden="true"></i>Shop No: 212, Super Market, Rangpur</li>
										<li><i class="fa fa-arrow-right" aria-hidden="true"></i>Shop No: 022, Market Name, Dhaka</li>
										<li><i class="fa fa-arrow-right" aria-hidden="true"></i>Shop No: 212, Market Name, Singapur</li>
										<li><i class="fa fa-arrow-right" aria-hidden="true"></i>Shop No: 212, Market Name, USA</li>
									</ul>
								</div>
							</div>
							<div>
								<div class="map-grid">
									<h5>Contact Me Through</h5>
									<ul>
										<li>Mobile No : +880 1847 108 888</li>
										<li>Office No : 0521-55663</li>
										<li>Home No   : +880 1847 108 888</li>
									</ul>
								</div>
							</div>--%>
                            </div>
                        </div>
                    </div>

                    <!--Plug-in Initialisation-->
                    <script type="text/javascript">
                        $(document).ready(function() {
                            //Horizontal Tab
                            $('#parentHorizontalTab').easyResponsiveTabs({
                                type: 'default', //Types: default, vertical, accordion
                                width: 'auto', //auto or any width like 600px
                                fit: true, // 100% fit in a container
                                tabidentify: 'hor_1', // The tab groups identifier
                                activate: function(event) { // Callback function if tab is switched
                                    var $tab = $(this);
                                    var $info = $('#nested-tabInfo');
                                    var $name = $('span', $info);
                                    $name.text($tab.text());
                                    $info.show();
                                }
                            });

                            // Child Tab
                            $('#ChildVerticalTab_1').easyResponsiveTabs({
                                type: 'vertical',
                                width: 'auto',
                                fit: true,
                                tabidentify: 'ver_1', // The tab groups identifier
                                activetab_bg: '#fff', // background color for active tabs in this group
                                inactive_bg: '#F5F5F5', // background color for inactive tabs in this group
                                active_border_color: '#c1c1c1', // border color for active tabs heads in this group
                                active_content_border_color: '#5AB1D0' // border color for active tabs contect in this group so that it matches the tab head border
                            });

                            //Vertical Tab
                            $('#parentVerticalTab').easyResponsiveTabs({
                                type: 'vertical', //Types: default, vertical, accordion
                                width: 'auto', //auto or any width like 600px
                                fit: true, // 100% fit in a container
                                closed: 'accordion', // Start closed if in accordion view
                                tabidentify: 'hor_1', // The tab groups identifier
                                activate: function(event) { // Callback function if tab is switched
                                    var $tab = $(this);
                                    var $info = $('#nested-tabInfo2');
                                    var $name = $('span', $info);
                                    $name.text($tab.text());
                                    $info.show();
                                }
                            });
                        });
                    </script>

                </div>

                <div class="clearfix"></div>
            </div>
        </div>
    </div>
    <!-- //contact -->

    <!-- tabs -->
    <script src="js/easyResponsiveTabs.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            $('#horizontalTab').easyResponsiveTabs({
                type: 'default', //Types: default, vertical, accordion           
                width: 'auto', //auto or any width like 600px
                fit: true // 100% fit in a container
            });
        });
    </script>
    <!-- //tabs -->

</asp:Content>