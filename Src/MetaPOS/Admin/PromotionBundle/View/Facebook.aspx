<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Admin/MasterPage.Master" AutoEventWireup="true" CodeBehind="Facebook.aspx.cs" Inherits="MetaPOS.Admin.PromotionBundle.View.Facebook" %>
<asp:Content ID="Content1" ContentPlaceHolderID="id" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <link href="../Content/facebook.css" rel="stylesheet" />
   <script>
       
   </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="contentBody" runat="server">   
 
   
     <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
          <div class="section">
                <label class="title">Facebook</label>
                <a href="/Admin/PromotionBundle/View/FacebookConfig.aspx" id="btnFacebookConfig" class="btn btn-info btn-sm btnCustoms btn-space">Facebook Config</a>
                 <a href="#" id="btnFacebookHistory" class="btn btn-info btn-sm btnCustoms">Facebook History</a>
            </div>
            </div>
            <div class="col-md-6 col-sm-6 col-xs-6">
                
            <div class="form-group">
                <div>
                    <label>Write your facebook post:</label>
                </div>
                
                <textarea id="txtPostContent" runat="server" aria-multiline="true" class="form-control" rows="6"></textarea>
            </div>
               <%-- <div class="form-group">
                     <span class="col-md-4 label-control">Upload Image</span><br/>
                    <img src="" id="postImage" width="250" style="display:none;"/>
                  
                    <asp:FileUpload ID="imageUpload" runat="server" />
                </div>   
                --%>
                <div class="form-group" style="padding-bottom: 25px;">

                    <%--<button type="button" id="btnSaveChanges" class="btn btn-info btn-default">Post Facebook</button>--%>
                    <asp:Button runat="server" ID="btnPostFacebook" Text="Post Facebook" CssClass="btn btn-info btn-default" OnClick="btn_PostFacebook"/>
                </div>
                <asp:Label ID ="lblText" runat="server"></asp:Label>
        </div>
            <div class="col-md-6 col-sm-6 col-xs-6">
        
                   <div id="fb-root"></div>
<div id="fb-root"></div>
<script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = 'https://connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.12&appId=586426288385653&autoLogAppEvents=1';
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
    
   <div class="fb-page" data-href="https://www.facebook.com/TestBot-154593141889378/" data-tabs="timeline" data-width="500" data-height="500" data-small-header="false" data-adapt-container-width="true" data-hide-cover="false" data-show-facepile="true"><blockquote cite="https://www.facebook.com/TestBot-154593141889378/" class="fb-xfbml-parse-ignore"><a href="https://www.facebook.com/TestBot-154593141889378/">Facebook Page Loading...</a></blockquote></div>
      </div>
    </div>
 

</asp:Content>


