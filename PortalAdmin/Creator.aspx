<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Creator.aspx.cs" Inherits="PortalAdmin_Creator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
    <script src="../Media/JavaScript/jquery-2.2.4.min.js"  type="text/javascript"></script>
    <script src="../Media/JavaScript/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Media/JavaScript/fontawesome.js" type="text/javascript"></script>
    <script src="../Media/JavaScript/footable.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/footable.min.css" rel="stylesheet" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="card border-info text-light text-info mt-1">
            <div class="card-header bg-info">
                <asp:Label ID="lblOwner" runat="server" CssClass="font-weight-bold">
                    IS3 - PORTAL DEVELOPMENT GROUP
                </asp:Label>
            </div>
            <div class="card-body">
                <div class="row small text-info">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm text-center">
                        <asp:ImageButton id="imgIS3Logo" class="img-fluid w-50" runat="server"></asp:ImageButton>
                    </div>
                   <%-- <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm text-center">
                        <asp:ImageButton id="ImageButton1" class="img-fluid w-50 rounded-top" runat="server" ImageUrl="~/Media/Images/Yna.jpg"></asp:ImageButton>
                    </div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm text-justify">
                        <asp:Label ID="lblDescription" runat="server" CssClass="col-form-label">&emsp;Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin dictum magna a finibus maximus. Nunc feugiat hendrerit scelerisque. Praesent sit amet nibh interdum, iaculis erat eget, hendrerit justo. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Vivamus dignissim aliquam diam, vel commodo justo dignissim sed. Donec ullamcorper blandit urna, laoreet mollis enim lobortis at. Vestibulum quis est nulla. Vestibulum faucibus sed nisi vitae mattis. Donec a pulvinar est.</asp:Label>
                    </div>   --%>                 
                </div>
            </div>
        </div>
    </div>
</asp:Content>

