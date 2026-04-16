<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FeedBack.aspx.cs" Inherits="PortalAdmin_FeedBack" %>

<%@ Register Src="~/MessageBox/AlertMessage.ascx" TagPrefix="uc1" TagName="AlertMessage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script src="../Media/JavaScript/jquery-2.2.4.min.js"  type="text/javascript"></script>
    <script src="../Media/JavaScript/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Media/JavaScript/fontawesome.js" type="text/javascript"></script>
    <script src="../Media/JavaScript/footable.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/footable.min.css" rel="stylesheet" type="text/css" media="screen" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="uPnlMain" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="card">
            <div class="card-header bg-info text-light">
                <h5 class="font-weight-bold">We would like to hear from you... Yes, YOU!</h5>
            </div>
            <div class="card-body">
                <div class="row col-md-12 col-lg-12 col-xl-12 col-sm-12">
                    <h6>Please take some time to show us some love and to give us a feedback.</h6>                
                </div>
                <br />
                <div class="row col-md-12 col-lg-12 col-xl-12 col-sm-12">                
                    <div class="col-md-2 col-lg-2 col-xl-2 col-sm-2">
                        <h6 class="small">Employee Number.</h6>
                    </div>
                    <div class="col-md-4 col-lg-4 col-xl-4 col-sm-4 input-group input-group-sm">
                        <asp:TextBox ID="txtEmpNo" runat="server" placeholder="Optional....." CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row col-md-12 col-lg-12 col-xl-12 col-sm-12">
                    <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12">
                        <h6 class="small">Are you satisfied with your experience on this application?</h6>
                    </div>                
                </div>
                <br />
                <div class="row col-md-12 col-lg-12 col-xl-12 col-sm-12">
                    <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12">
                        <div class="col-md-11 col-lg-11 col-xl-11 col-sm-11">
                            <h6 class="small">
                                <asp:RadioButtonList ID="rdbRatings" runat="server" Width="195px">
                                    <asp:ListItem>Excellent</asp:ListItem>
                                    <asp:ListItem>Good</asp:ListItem>
                                    <asp:ListItem>Average</asp:ListItem>
                                    <asp:ListItem>Needs Improvement</asp:ListItem>
                                </asp:RadioButtonList>  
                            </h6>  
                        </div>
                    </div>                         
                </div>
                <br />
                <div class="row col-md-12 col-lg-12 col-xl-12 col-sm-12">
                    <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12">
                        <h6 class="small">Comments or Suggestions?</h6>
                    </div>                      
                </div>
                <div class="row col-md-10 col-lg-10 col-xl-10 col-sm-10">
                    <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 input-group input-group-sm">
                        <asp:TextBox ID="txtComment" runat="server" Height="82px" TextMode="MultiLine" CssClass="form-control"></asp:TextBox> 
                    
                    </div>                   
                </div>
                <br />
                <div class="row col-md-12 col-lg-12 col-xl-12 col-sm-12">
                <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 input-group input-group-sm">
                    <asp:LinkButton ID="lbtnSubmit" runat="server" CssClass="btn btn-success rounded-left" OnClick="lbtnSubmit_Click"  >
                        <i class="fa fa-envelope"></i> Submit 
                    </asp:LinkButton>
                    <asp:HiddenField ID="hdnAction" runat="server" />
                </div> 
                </div>        
            </div>        
        </div>
        <div>
            <uc1:AlertMessage runat="server" id="AlertMessage" />
        </div>
    </ContentTemplate>
</asp:UpdatePanel>        
</asp:Content>

