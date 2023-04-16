<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Docs.aspx.cs" Inherits="MetaPOS.Admin.Docs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
    Docs Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Css/Docs.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">
    <div class="row">
        <div style="clear: both"></div>
        <div class="col-md-12 col-sm-12 col-xm-12">
            <div class="section">
                <h2 class="sectionBreadcrumb">Documentation</h2>
            </div>
        </div>
    </div>

    <div class="panel-group" id="accordion">
        <div class="panel panel-default ">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseOne">
                        <span class="glyphicon glyphicon-minus"></span>
                        Sales
                    </a>
                </h4>
            </div>
            <div id="collapseOne" class="panel-collapse collapse in">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b>১)</b>&nbsp;কোন পন্য Sale করতে, Barcode Reader দিয়ে পন্যটি Scan/Read করলে Sale পেজের বাম দিকে পন্যটির/একই পন্যের তথ্য পাওয়া যায়।</li>
                            <li><b>২)</b>&nbsp;সেখান থেকে প্রয়োজন অনুযায়ী পন্যের Qty পরিবর্তন করা যাবে এবং Customer Name, Phone Number, Discount, Vat ইত্যাদি ঘর পূরন করে Save বাটনে ক্লিক করলে Sale কৃত পন্যটি System এ Save হবে এবং Customer Bill কপি Print হবে।</li>
                            <li><b>৩)</b>&nbsp;একই পন্য বার বার Scan/Read করলে পন্যের Qty এক এক করে বাড়তে থাকবে।</li>
                            <li><b>৪)</b>&nbsp;Customer Bill Copy এর Bill Number টি দিয়ে উপরের Search বক্সটি পূরন করে Sale কৃত পন্যের পরিমান Update করা সম্ভব। Update করতে না চাইলে Cancel বাটনে ক্লিক করে Cancel করা সম্ভব।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div> 
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo">
                        <span class="glyphicon glyphicon-plus"></span>
                        Stock
                    </a>
                </h4>
            </div>
            <div id="collapseTwo" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b><u>Add Stock</u></b></li>
                            <li><b>১)</b>&nbsp;নতুন কোন পন্য Add করতে চাইলে পন্যের Supplier, Category, Quantity, Buying Price, Selling Price ও Size এর ঘরগুলো পূরন করে Add বাটনে ক্লিক করুন।</li>
                            
                            <li><b><u>Search By Code</u></b></li>
                            <li><b>১)</b>&nbsp;Stock এ আছে এমন নির্দিষ্ট কোন পন্য খুঁজে বের করতে চাইলে পন্যের কোডটি Search By Code Option এ গিয়ে Code এর বক্সে লিখে Search বাটনে ক্লিক করুন।</li>
                            <li><b>২)</b>&nbsp;Supplier/Category অনুযায়ী পন্য খুঁজতে চাইলে Supplier/Category সিলেক্ট করে Search বাটনে ক্লিক করুন।</li>
                            
                            <li><b><u>Search By Date</u></b></li>
                            <li><b>১)</b>&nbsp;Supplier, Category অনুযায়ী একটি নির্দিষ্ট তারিখ হতে অন্য তারিখের পন্য খুঁজতে Search By Date এর Supplier, Category, From(date), To(date) এর ঘরগুলো পূরন করে Search বাটনে ক্লিক করুন।</li>
                            <li><b><u>Report Generator</u></b></li>
                            <li><b>১)</b>&nbsp;একটি নির্দিষ্ট তারিখ হতে অন্য তারিখের পন্যের রিপোর্ট পেতে From(date), To(date) এর ঘর পুরন করে Print Button এ ক্লিক করুন।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseThree">
                        <span class="glyphicon glyphicon-plus"></span>
                        Return
                    </a>
                </h4>
            </div>
            <div id="collapseThree" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b><u>Back To Return</u></b></li>
                            <li><b>১)</b>&nbsp;Stock Page এর কোন পন্য ফেরত দিতে কিংবা পন্যের পরিমান কমাতে Return Page এর Back To Return অপশনে যান।</li>
                            <li><b>২)</b>&nbsp;উক্ত অপশনের Product Code ঘরটি পূরন করে যতটি পন্য ফেরত দিতে চান তা Qty বক্সকে লিখে Back To Return বাটনে ক্লিক করুন। এবং নিচের View এ কতটি পন্য ফেরত দিলেন তা দেখতে পারবেন।</li>
                            <li><b><u>Search By Date</u></b></li>
                            <li><b>১)</b>&nbsp;উপরে ডানদিকের Search By Date অপশনের ঘরগুলো প্রয়োজন অনুযায়ী পূরন করে Returned পন্য দেখতে ও Print বাটনে ক্লিক করে Report Print করতে পারবেন।</li>
                            <li><b><u>Grid View</u></b></li>
                            <li><b>১)</b>&nbsp;একই Date এ ফেরত/Returned পন্য ডান দিকের Retrived বাটনে ক্লিক করে পুনরায় Stock Page এ পূর্বের পরিমানের সাথে যোগ করতে পারবেন। উল্লেখ্য ১ দিনের বেশি পুর্বের Date এর কোন পন্য Retrive করা সম্ভব নয়।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseFour">
                        <span class="glyphicon glyphicon-plus"></span>
                        Cancel
                    </a>
                </h4>
            </div>
            <div id="collapseFour" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b>১)</b>&nbsp;Stock Page এর কোন পন্য Delete করলে তা Permanently Delete হয় না । Delete কৃত পন্যগুলো Cancel পেজ এ জমা হয়।</li>
                            <li><b>২)</b>&nbsp;ভুলে কোন পন্য Delete করে থাকলে Cancel পেজের ডানদিকের Retrive বাটনে ক্লিক করে উক্ত পন্য পুনরায় Stock Page এ নিয়ে আসা যায়। ডানদিকের View বাটনে ক্লিক করে পন্যের যাবতীয় তথ্য দেখা যায়।</li>
                            <li><b>৩)</b>&nbsp;একই Date এর Delete কৃত পন্যসমূহ শুধুমাত্র Retrive করা সম্ভব।</li>
                            <li><b>৪)</b>&nbsp;উপরের বাটন সমুহ হতে Supplier/Category নাম দিয়ে Date অনুযায়ী Delete পন্যসমূহ Search করা সম্ভব।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseFive">
                        <span class="glyphicon glyphicon-plus"></span>
                        Cash
                    </a>
                </h4>
            </div>
            <div id="collapseFive" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b><u>Cash In</u></b></li>
                            <li><b>১)</b>&nbsp;Account এ টাকা যোগ করতে চাইলে Cash In ব্লকের Cash Type, Amount, Description ঘর পূরণ করে Add বাটনে ক্লিক করুন।</li>

                            <li><b><u>Cash Out</u></b></li>
                            <li><b>১)</b>&nbsp;Account হতে টাকা খরচ হলে Cash Out ব্লকের Cash Type, Amount, Description ঘর পূরণ করে Add বাটনে ক্লিক করুন।</li>

                            <li><b><u>Search</u></b></li>
                            <li><b>১)</b>&nbsp;নির্দিষ্ট Cash Type খুঁজতে Cash Type, From(date), To(date) ঘর পূরন করে Search বাটনে ক্লিক করুন।</li>

                            <li><b><u>Report Generator</u></b></li>
                            <li><b>১)</b>&nbsp;Report প্রিন্ট করতে চাইলে উক্ত ঘরগুলো পূরন করে Print বাটনে ক্লিক করুন।</li>

                            <li><b><u>Grid View</u></b></li>
                            <li><b>১)</b>&nbsp;শুধুমাত্র ১ নং SL এর Cash Type টি ডিলিট করা সম্ভব। ডিলিট করতে চাইলে ১ নং SL এর ডিলিট বাটনে ক্লিক করুন।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseEight">
                        <span class="glyphicon glyphicon-plus"></span>
                        Customer
                    </a>
                </h4>
            </div>
            <div id="collapseEight" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b><u>Search</u></b></li>
                            <li><b>১)</b>&nbsp;কোন Customer কে খুঁজতে Customer এর ID, Name ও Phone Number উপরের Search ঘরে দিন। অথবা আপনি এর যে কোন একটি দিয়েও Search করতে পারবেন।</li>
                            <li><b><u>Grid View</u></b></li>
                            <li><b>১)</b>&nbsp;Sale পেজ হতে Customer, Customer পেজে Automatically Add হয়। নির্দিষ্ট কোন Customer এর বিস্তারিত তথ্য জানতে View, Customer এর তথ্য Edit করতে চাইলে Edit আর কোন Customer কে Delete করতে চাইলে Delete বাটনে ক্লিক করুন। </li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseNine">
                        <span class="glyphicon glyphicon-plus"></span>
                        Supplier
                    </a>
                </h4>
            </div>
            <div id="collapseNine" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b><u>Button ও Search</u></b></li>
                            <li><b>১)</b>&nbsp;কোন Supplier এর যাবতীয় তথ্য সংরক্ষণ করতে Add Supplier বাটনে ক্লিক করুন। তারপর Supplier এর তথ্যের ঘরগুলো পূরন করে Insert বাটনে ক্লিক করুন।</li>
                            <li><b>২)</b>&nbsp;উপরে ডান দিকের Search বক্স থেকে ID, Company Name এবং Phone Number দিয়ে নির্দিষ্ট Supplier কে খুঁজে বের করতে পারবেন। অথবা আপনি এর যে কোন একটি দিয়েও Search করতে পারবেন।</li>
                            <li><b><u>Grid View</u></b></li>
                            <li><b>১)</b>&nbsp; ডান দিকের View, Edit ও Delete বাটনে ক্লিক করে Supplier এর পূর্নতথ্য দেখতে, Edit করতে ও Delete করতে পারবেন।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseTen">
                        <span class="glyphicon glyphicon-plus"></span>
                        Staff
                    </a>
                </h4>
            </div>
            <div id="collapseTen" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b><u>Button ও Search</u></b></li>
                            <li><b>১)</b>&nbsp;কোন Staff এর যাবতীয় তথ্য সংরক্ষণ করতে Add Staff বাটনে ক্লিক করুন। তারপর Staff এর তথ্যের ঘরগুলো পূরন করে Insert বাটনে ক্লিক করুন। তথ্য সংরক্ষণ করতে না চাইলে Cancel বাটনে ক্লিক করুন।</li>
                            <li><b>২)</b>&nbsp;উপরে ডান দিকের Search বক্স থেকে ID, Staff Name এবং Phone Number দিয়ে নির্দিষ্ট Staff কে খুঁজে বের করতে পারবেন। অথবা আপনি এর যে কোন একটি দিয়েও Search করতে পারবেন।</li>
                            <li><b><u>Grid View</u></b></li>
                            <li><b>১)</b>&nbsp; ডান দিকের View, Edit ও Delete বাটনে ক্লিক করে Staff এর পূর্নতথ্য দেখতে, Edit করতে ও Delete করতে পারবেন।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default">
            <div class="panel-heading">
                <h4 class="panel-title">
                    <a class="accordion-toggle" data-toggle="collapse" data-parent="#accordion" href="#collapseTwelve">
                        <span class="glyphicon glyphicon-plus"></span>
                        Setting
                    </a>
                </h4>
            </div>
            <div id="collapseTwelve" class="panel-collapse collapse">
                <div class="panel-body">                
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <ul class="list-unstyled">
                            <li><b><u>Category</u></b></li>
                            <li><b>১)</b>&nbsp;কোন পন্যের Category Add করতে চাইলে Category অংশে "Enter Category" বক্সে পন্যের নাম লিখে Add বাটনে ক্লিক করুন।</li>
                            <li><b>১)</b>&nbsp;ডান দিকের Edit এবং Delete বাটন দিয়ে কোন পন্যের নাম পরিবর্তন এবং Delete করতে পারবেন।</li>
                            <li><b><u>Cash Type</u></b></li>
                            <li><b>১)</b>&nbsp;কোন পন্যের Cash Type Add করতে চাইলে Cash Type অংশে "Enter Cash Type" বক্সে Cash Type এর নাম লিখে Add বাটনে ক্লিক করুন।</li>
                            <li><b>১)</b>&nbsp;ডান দিকের Edit এবং Delete বাটন দিয়ে কোন Cash Type নাম পরিবর্তন এবং Delete করতে পারবেন।</li>
                        </ul>
                    </div>
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <video width="100%" height="80%" controls>                           
                            <source src="../Video/TestVedio.mp4" type="video/mp4" />
                            <source src="../Video/TestVedio.ogg" type="video/ogg" />
                        </video>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <script>

        $('.collapse').on('shown.bs.collapse', function() {
            $(this).parent().find(".glyphicon-plus").removeClass("glyphicon-plus").addClass("glyphicon-minus");
        }).on('hidden.bs.collapse', function() {
            $(this).parent().find(".glyphicon-minus").removeClass("glyphicon-minus").addClass("glyphicon-plus");
        });
    </script>
       
    <script>
        activeModule = "public";
    </script>

</asp:Content>