<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Unauthorized.aspx.cs" Inherits="PortalAdmin_Unauthorized" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Unauthorized Access</title>
    <meta http-equiv="X-UA-Compatible" content="IE=7,8,9,10,edge" />

    <script src="../Media/JavaScript/RightClick.js" type="text/javascript"></script>
    <script src="../Media/JavaScript/jquery-2.2.4.min.js"  type="text/javascript"></script>
    <script src="../Media/JavaScript/popper.min.js" type="text/javascript"></script>
    <script src="../Media/JavaScript/fontawesome.js" type="text/javascript"></script>
    <script src="../Media/JavaScript/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../Media/CSS/FontAwesome.css" rel="stylesheet" type="text/css" />
    <link href="../Media/CSS/Default.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">    
    <div class="container-fluid h-100 align-content-center">
        <div class="row">
            <div class="col-md-12 text-center" >
                <br />
                <br />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Media/Images/unauthorized.png" />
            </div>
            <br />
            <div class="col-md-12 text-center" >
                <br />
                <h3 class="font-weight-bold">YOU ARE NOT AUTHORIZED TO ACCESS THIS PAGE</h3>
            </div>
            <div class="col-md-12 text-center" >
                <h6>Please coordinate with the Process Owner</h6>
            </div>            
            <div class="col-md-12 text-center" >
                <br />
                <asp:Button ID="Button1" runat="server" Text="System Information" CssClass="btn btn-danger"/>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
