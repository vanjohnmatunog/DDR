<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="carousel1" class="carousel slide" data-ride="carousel">
                <div class="carousel-inner h-50">
                    <div class="carousel-item active">
                        <img src="Media/Images/Slider/DRDHOME.png" class="img-fluid w-100" style="max-height:300px !important;"/>
                        <%--<div class="carousel-caption text-right">
                            <h2 class="text-success font-weight-bold"> Home 1 </h2>
                            <p class="text-success"> Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                            <p><a class="btn btn-info btn-lg" href="#" role="button"> Read more </a></p>
                        </div>--%>
                    </div>
                   <%-- <div class="carousel-item">
                        <img src="Media/Images/Slider/home2.jpg" class="img-fluid w-100" style="max-height:300px !important;"/>--%>
                        <%--<div class="carousel-caption text-right">
                            <h2 class="text-success font-weight-bold"> Home2 </h2>
                            <p class="text-success"> Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                            <p><a class="btn btn-info btn-lg" href="#" role="button"> Read more </a></p>
                        </div>--%>
                   <%-- </div>--%>
                    <%--<div class="carousel-item">
                        <img src="Media/Images/Slider/home3.jpg" class="img-fluid w-100" style="max-height:300px !important;"/>--%>
                        <%--<div class="carousel-caption text-right">
                            <h3 class="text-success font-weight-bold"> Home 3 </h3>
                            <p class="text-success"> Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.</p>
                            <p><a class="btn btn-info btn-lg" href="#" role="button"> Read more </a></p>
                        </div>--%>
                    <%--</div>--%>
                </div>

                <%--<a class="carousel-control-prev" href="#carousel1" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="sr-only"> Previous </span>
                </a>
                <a class="carousel-control-next" href="#carousel1" role="button" data-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="sr-only"> Next </span>
                </a>--%>
            </div>
</asp:Content>

