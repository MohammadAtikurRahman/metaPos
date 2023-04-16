<%@ Page Title="" Language="C#" MasterPageFile="~/Site/Shared/_Layout.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="MetaPOS.Site.Views.Contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Title" runat="server">
    Contact | metaPOS
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="<%= ResolveUrl("~/Site/Content/asset/css/contact.css") %>" rel="stylesheet" />
    <link href="<%= ResolveUrl("~/Site/Content/bootstrap-sweetalert/sweet-alert.css") %>" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/sweetalert/1.1.3/sweetalert.min.js"></script>


    Run code
    

    <style>
        form label.error, form input.submit {
            color: red;
        }

        span.required {
            color: #F44336;
            margin-left: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Body" runat="server">


    <section id="breadcrumb" class="breadcrumb">
        <div class="container">
            <div class="row text-center">
                <div class="col-sm-12">
                    <h1>আমাদের সাথে যোগাযোগ করুন</h1>
                </div>
            </div>
        </div>
    </section>
    <section class="contact">
        <div class="container">
            <div class="row">
                <div class="col-sm-2"></div>
                <div class="col-sm-8">
                    <div class="contact-info">
                        <p class="contact-alert text-center">
                            আমাদের স্বয়ং সম্পূর্ণ পজ সফটওয়্যারটির মাধ্যমে আপনার ব্যবসা কিভাবে
                        লাভবান হতে পারে তা জানতে এবং ফ্রি ডেমো পাওয়ার জন্য আমাদের সাথে যোগাযোগ করুন।
                        <br>
                            আজই কল করুন: ০১৭২৩ ৩৯৩ ৪৫৬, ০১৭২৫ ২০৯ ৩০৯, ০১৭ ৩৮ ১৬৯ ৭০৯
                        </p>
                        <p class="contact-alert text-center">
                            অথবা নীচের ফর্মটি পূরণ করলে আমাদের সেলস / মার্কেটিং টিম থেকে
                        আপনার সাথে দ্রুত যোগাযোগ করা হবে।
                        </p>
                    </div>
                    <br>
                    <br>
                    <br> 
                    <div class="form-group">
                        <label class="col-sm-3 control-label text-right" for="txtName">আপনার নাম<span class="required">*</span></label>
                        <div class="col-sm-9">
                            <input type="text" class="form-control" id="txtName" placeholder="আপনার নাম">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3 text-right" for="txtMobile">মোবাইল নং<span class="required">*</span></label>
                        <div class="col-sm-9">
                            <input type="text" maxlength="11" class="form-control" id="txtMobile" placeholder="e.g ০১৭০০০০০০০০">
                        </div>
                    </div>
                    <%-- 
                     <div class="form-group">
                        <label class="control-label col-sm-3" for="txtEmail">ইমেইল<span class="required">*</span></label>
                        <div class="col-sm-9">
                            <input type="email" class="form-control" id="txtEmail" placeholder="ইমেইল" required>
                        </div>
                    </div>
                        --%>
                    <div class="form-group">
                        <label class="control-label col-sm-3 text-right" for="txtCompany">কোম্পানি নাম<span class="required">*</span></label>
                        <div class="col-sm-9">
                            <input type="text" class="form-control" id="txtCompany" placeholder="কোম্পানি নাম">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3 text-right">বিষয়<span class="required">*</span></label>
                        <div class="col-sm-9">
                            <asp:DropDownList ID="ddlSubject" CssClass="form-control" runat="server">
                                <asp:ListItem Value="0">--- বিষয় নির্বাচন ---</asp:ListItem>
                                <asp:ListItem Value="1">সাধারণ প্রশ্ন</asp:ListItem>
                                <asp:ListItem Value="2">ফিচার সমূহ</asp:ListItem>
                                <asp:ListItem Value="3">প্রাইসিং</asp:ListItem>
                                <asp:ListItem Value="4">ডেমো দেখতে চাই</asp:ListItem>
                                <asp:ListItem Value="5">অন্যান্য</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-sm-3 text-right" for="txtQus">প্রশ্ন সমূহ<span class="required">*</span></label>
                        <div class="col-sm-9">
                            <textarea class="form-control" rows="3" id="txtQus" name="message"></textarea>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="col-sm-offset-3 col-sm-9">
                            <button class="btn btn-success btn-xl" id="btnSend" type="submit">পাঠিয়ে দিন </button>
                        </div>
                    </div>
                </div>
                <div class="col-sm-2"></div>
            </div>
        </div>
    </section>
    <section class="address">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 text-center">
                    <div class="section_heading">
                        <h1>সরাসরি আমাদের অফিসে যোগাযোগ করুন</h1>
                        <h2>আমাদের যেকোনো অফিসে যোগাযোগ করতে পারেন</h2>
                    </div>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>প্রধান অফিস</h1>
                            <h2>এপার্টমেন্ট ৪০২, বাসা ২৪, সেকশন ৬, ব্লক ডি, এভিনিউ ৫, মিরপুর, ঢাকা ১২১৬</h2>
                        </div>
                        <p class="call_num">(+৮৮০) ০১৬৭০ ৭২৭৩০২ </p>
                    </div>
                </div>
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>রংপুর অফিস</h1>
                            <h2>বাসা ১৫১(২য় তলা), কলেজ রোড (পিডিবি অফিস এর পাশে)</h2>
                        </div>
                        <p class="call_num">(+৮৮০) ১৭২৩ ৩৯৩ ৪৫৬ </p>
                    </div>
                </div>
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>বগুড়া অফিস</h1>
                            <h2>সাতমাথা বগুড়া </h2>
                        </div>
                        <p class="call_num">(+৮৮০) ১৭৯৬ ১৪৩ ৪০৮ </p>
                    </div>
                </div>
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>ঝিনাইদহ অফিস</h1>
                            <h2>কলেজ হোস্টেল মার্কেট, চুয়াডাঙ্গা বাস স্ট্যান্ড, মাওনালা ভাসানী সড়ক, ঝিনাইদহ</h2>
                        </div>
                        <p class="call_num">(+৮৮০) ১৭১১ ১১১ ৫৮৫ </p>
                    </div>
                </div>
            </div>
            <div class="row text-center">
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>জামালপুর অফিস</h1>
                            <h2>তাড়াতাড়ি আসবে</h2>
                        </div>
                        <p class="call_num">(+৮৮০) ০১৬৭০ ৭২৭৩০২ </p>
                    </div>
                </div>
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>সিলেট অফিস</h1>
                            <h2>তাড়াতাড়ি আসবে</h2>
                        </div>
                        <p class="call_num">(+৮৮০) ১৭২৩ ৩৯৩ ৪৫৬ </p>
                    </div>
                </div>
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>খুলনা অফিস</h1>
                            <h2>তাড়াতাড়ি আসবে</h2>
                        </div>
                        <p class="call_num">(+৮৮০) ১৭৯৬ ১৪৩ ৪০৮ </p>
                    </div>
                </div>
                <div class="col-sm-3 branch_box">
                    <div class="branch_content">
                        <div class="branch_address">
                            <h1>চট্টগ্রাম অফিস</h1>
                            <h2>তাড়াতাড়ি আসবে</h2>
                        </div>
                        <p class="call_num">(+৮৮০) ১৭১১ ১১১ ৫৮৫ </p>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Modal HTML -->
    <div id="myModal" class="modal fade">
        <div class="modal-dialog modal-confirm">
            <div class="modal-content">
                <div class="modal-header">
                    <div class="icon-box">
                        <i class="material-icons">&#xE876;</i>
                    </div>
                    <h4 class="modal-title">ধন্যবাদ </h4>
                </div>
                <div class="modal-body">
                    <p class="text-center">আমাদের মেইল করার জন্য আপনাকে ধন্যবাদ, আমরা খুব দ্রত আপনার বিষয়টি পর্যবেক্ষণ করব। আশা করি আমাদের ফোনের আপেক্ষা করবেন </p>
                </div>
                <div class="modal-footer">
                    <button class="btn btn-success btn-block" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="subjectText" runat="server"/>
</asp:Content>


<asp:Content ID="Content4" ContentPlaceHolderID="Footer" runat="server">

    <script src="<%= ResolveUrl("~/Site/Scripts/jquery.validate.min.js") %>"></script>
    <script src="<%= ResolveUrl("~/Site/Content/asset/js/contact.js?v=0.010") %>"></script>

</asp:Content>
