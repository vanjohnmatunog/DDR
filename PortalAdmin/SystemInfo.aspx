<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SystemInfo.aspx.cs" Inherits="PortalAdmin_SystemInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script src="../Media/JavaScript/jquery-2.2.4.min.js"  type="text/javascript"></script>
    <script src="../Media/JavaScript/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Media/JavaScript/fontawesome.js" type="text/javascript"></script>
    <script src="../Media/JavaScript/footable.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/footable.min.css" rel="stylesheet" type="text/css" media="screen" />
   
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="card border-info text-light text-info mt-1">
            <div class="card-header bg-info">
                <asp:Label ID="lblOwner" runat="server" CssClass="font-weight-bold">
                    PROCESS OWNER
                </asp:Label>
            </div>
            <div class="card-body">
                <div class="row small text-info">
                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 input-group-sm ">
                        <asp:Label ID="lblPrimaryOwner" runat="server" CssClass="col-form-label font-weight-bold"> Primary Owner: </asp:Label>
                        <asp:TextBox ID="txtPrimaryOwner" runat="server" CssClass="form-control text-info"></asp:TextBox>
                    </div>
                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-6 input-group-sm">
                        <asp:Label ID="lblSubOwner" runat="server" CssClass="col-form-label font-weight-bold"> Secondary Owner: </asp:Label>
                        <asp:TextBox ID="txtSubOwner" runat="server" CssClass="form-control text-info"></asp:TextBox>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="lblDepartment" runat="server" CssClass="col-form-label font-weight-bold"> Department: </asp:Label>
                        <asp:TextBox ID="txtDepartment" runat="server" CssClass="form-control text-info"></asp:TextBox>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="lblSection" runat="server" CssClass="col-form-label font-weight-bold"> Section: </asp:Label>
                        <asp:TextBox ID="txtSection" runat="server" CssClass="form-control text-info"></asp:TextBox>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="lblLocalNo" runat="server" CssClass="col-form-label font-weight-bold"> Local No: </asp:Label>
                        <asp:TextBox ID="txtLocalNo" runat="server" CssClass="form-control text-info"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="card border-success text-light text-success mt-1">
            <div class="card-header bg-success">
                <asp:Label ID="lblSystem" runat="server" CssClass="font-weight-bold">
                    SYSTEM DETAILS
                </asp:Label>
            </div>
            <div class="card-body">
                <div class="row small text-success">
                    <div class="col-xl-3 col-lg-3 col-md-3 col-sm-3 input-group-sm">
                        <asp:Label ID="lblAppID" runat="server" CssClass="col-form-label font-weight-bold"> Application ID: </asp:Label>
                        <asp:TextBox ID="txtAppID" runat="server" CssClass="form-control text-success"></asp:TextBox>
                    </div>
                    <div class="col-xl-9 col-lg-9 col-md-9 col-sm-9 input-group-sm">
                        <asp:Label ID="lblSysName" runat="server" CssClass="col-form-label font-weight-bold"> System Name: </asp:Label>
                        <asp:TextBox ID="txtSysName" runat="server" CssClass="form-control text-success"></asp:TextBox>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="lblDBName" runat="server" CssClass="col-form-label font-weight-bold"> Database Name: </asp:Label>
                        <asp:TextBox ID="txtDBName" runat="server" CssClass="form-control text-success"></asp:TextBox>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="lblDBServer" runat="server" CssClass="col-form-label font-weight-bold"> Database Server: </asp:Label>
                        <asp:TextBox ID="txtDBServer" runat="server" CssClass="form-control text-success"></asp:TextBox>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="lblAppServer" runat="server" CssClass="col-form-label font-weight-bold"> Application Server: </asp:Label>
                        <asp:TextBox ID="txtAppServer" runat="server" CssClass="form-control text-success"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="card border-secondary text-light text-secondary mt-1">
            <div class="card-header bg-secondary">
                <asp:Label ID="Label6" runat="server" CssClass="font-weight-bold">
                    SUPPORT
                </asp:Label>
            </div>
            <div class="card-body">
                <div class="row small text-secondary">                    
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-2 input-group-sm"></div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-8 input-group-sm text-center">
                        <asp:Label ID="lblSupport" runat="server" CssClass="col-form-label font-weight-bold align-middle"> Support In-Charge: </asp:Label>
                        <asp:TextBox ID="txtSupport" runat="server" CssClass="form-control text-secondary text-center"></asp:TextBox>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-2 input-group-sm"></div>
                </div>
            </div>
        </div>        
        <div class="card border-danger text-light text-danger mt-1">
            <div class="card-header bg-danger">
                <asp:Label ID="Label1" runat="server" CssClass="font-weight-bold">
                    REFERENCE
                </asp:Label>
            </div>
            <div class="card-body">                
                <div class="row small text-danger">
                    <div class="col-xl-3 col-lg-3 col-md-3 col-sm-2 input-group-sm"></div>
                    <div class="col-xl-6 col-lg-6 col-md-6 col-sm-8 input-group-sm text-center">
                        <div class="form-group">
                        <asp:Label ID="Label2" runat="server" CssClass="col-form-label font-weight-bold"> Upload File: </asp:Label>
                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                    <div class="col-xl-3 col-lg-3 col-md-3 col-sm-2 input-group-sm"></div>
                </div>
            </div>
        </div>        
        <div class="card border-warning text-light text-warning mt-1">
            <div class="card-header bg-warning">
                <asp:Label ID="Label3" runat="server" CssClass="font-weight-bold">
                    STATISTICS
                </asp:Label>
            </div>
            <div class="card-body">
                <div class="row small text-danger">
                    <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 input-group-sm"></div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm text-center">
                                                 
                    </div>
                </div>
            </div>
        </div>
    </div>    
</asp:Content>

