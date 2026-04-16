<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewRecords.aspx.cs" Inherits="PageView_ViewRecords" %>

<%@ Register Src="~/MessageBox/AlertMessage.ascx" TagPrefix="uc1" TagName="AlertMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--  <script src="../Media/JavaScript/jquery-2.2.4.min.js"  type="text/javascript"></script>
    <script src="../Media/JavaScript/bootstrap.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script src="../Media/JavaScript/fontawesome.js" type="text/javascript"></script>
    <script src="../Media/JavaScript/footable.min.js" type="text/javascript"></script>
    <link href="../Media/CSS/footable.min.css" rel="stylesheet" type="text/css" media="screen" />--%>

    <style type="text/css">
        .rowHeight
        {
            padding: 2px !important;
            margin: auto !important;
            /*line-height: 0px !important;*/        
        }
        .alignRight
        {
            text-align: right !important;      
        }
        .alignCenter
        {
            text-align: center !important;        
        }
        /* The container */
        .chkContainer {
            display: block;
            position: relative;
            padding-left: 40px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 18px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

        /* Hide the browser's default checkbox */
        .chkContainer input {
            position: absolute;
            opacity: 0;
            cursor: pointer;
        }

        /* Create a custom checkbox */
        .checkmark {
            position: absolute;
            top: 3px;
            left: 0px;
            height: 22px;
            width: 22px;
            background-color: #ccc;
        }

        /* On mouse-over, add a grey background color */
        .chkContainer:hover input ~ .checkmark {
            background-color: #aaa;
        }

        /* When the checkbox is checked, add a blue background */
        .chkContainer input:checked ~ .checkmark {
            background-color: #555;
        }

        /* Create the checkmark/indicator (hidden when not checked) */
        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the checkmark when checked */
        .chkContainer input:checked ~ .checkmark:after {
            display: block;
        }

        /* Style the checkmark/indicator */
        .chkContainer .checkmark:after {
            left: 8px;
            top: 3px;
            width: 7px;
            height: 14px;
            border: solid white;
            border-width: 0 3px 3px 0;
            -webkit-transform: rotate(45deg);
            -ms-transform: rotate(45deg);
            transform: rotate(45deg);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="upnlRecords" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>            
            <div class="container-fluid">
                    <div class="card border-secondary text-light text-info mt-1">
                        <div class="card-header bg-secondary">
                            <asp:Label ID="lblRecordsHeader" runat="server" CssClass="font-weight-bold">
                                V I E W &nbsp; R E C O R D S
                            </asp:Label>
                        </div>
                        <div class="card-body"> 
                            <div class="row small text-secondary">                                 
                                <div class="form-group col-xl-4 col-lg-4 col-md-4 col-sm-12 input-group text-info small">         
                                    <span class="input-group-text small rounded-0"> <asp:Label ID="lblSelectedCategory" runat="server" Text="Status"></asp:Label> </span>
                                    <asp:DropDownList ID="ddlStatus" CssClass="form-control text-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlParameter_SelectedIndexChanged"></asp:DropDownList>
                                </div>  
                                <div class="form-group col-xl-5 col-lg-5 col-md-5 col-sm-12 input-group text-info small">         
                                    <span class="input-group-text small rounded-0"> Keyword </span>
                                    <asp:TextBox ID="txtKeyword" runat="server" placeholder=" Search...." CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-secondary" OnClick="lbtnSearch_Click" >
                                            <i class="fa fa-search"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div> 
                                <div class="form-group col-xl-3 col-lg-3 col-md-3 col-sm-12 input-group text-info small">         
                                    <span class="input-group-text small rounded-0"> Page Size </span>
                                    <asp:DropDownList ID="ddlPageSize" CssClass="form-control text-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                    </asp:DropDownList>
                                </div>   
                            </div>
                            <div class="row small text-secondary">  
                                <div class="form-group col-xl-4 col-lg-5 col-md-5 col-sm-12 input-group text-info small">         
                                    <span class="input-group-text small rounded-0"> From Date </span>
                                    <asp:TextBox ID="txtFromDate" runat="server" placeholder=" From Date" CssClass="form-control form-control-sm datepicker alignCenter" ></asp:TextBox>
                                    <div class="input-group-append">
                                    </div>
                                </div> 
                            </div>
                            <div class="row small text-secondary">  
                                <div class="form-group col-xl-4 col-lg-5 col-md-5 col-sm-12 input-group text-info small">         
                                    <span class="input-group-text small rounded-0">&nbsp; To Date &nbsp;&nbsp;</span>
                                    <asp:TextBox ID="txtToDate" runat="server" placeholder=" To Date" CssClass="form-control form-control-sm datepicker alignCenter"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="lbtnSearchByDate" runat="server" CssClass="btn btn-secondary" OnClick="lbtnSearchByDate_Click">
                                            <i class="fa fa-search"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div> 
                            </div>

                            <div class="row small text-secondary">                                    
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2">
                                    <div class="table-responsive">                          
                                        <asp:GridView ID="gvRecords" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                            EmptyDataText="There are no data records to display." AllowPaging="True" PageSize="5" OnPageIndexChanging="grdRecords_PageIndexChanging">
                                            <Columns>                                                
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter" />                                                                               
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="View">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lbtnView" runat="server" CssClass="btn btn-link small" CommandArgument='<%# Eval("RefID") %>' CommandName='<%# Eval("RefID") %>' OnCommand="lbtnView_Command">
                                                            <i class="fa fa-info-circle"></i>
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Control Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblControlNo" runat="server" CssClass="col-form-label" Text='<%# Eval("DrNo") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssuedDate" runat="server" CssClass="col-form-label" Text='<%# Eval("IssuedDate") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Issued By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssuedBy" runat="server" CssClass="col-form-label" Text='<%# Eval("IssuedBy") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BU">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBU" runat="server" CssClass="col-form-label" Text='<%# Eval("AffectedBU") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Received Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReceivedDate" runat="server" CssClass="col-form-label" Text='<%# Eval("ReceiveDate") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supplier">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSupplier" runat="server" CssClass="col-form-label" Text='<%# Eval("Supplier") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Forwarder">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblForwarder" runat="server" CssClass="col-form-label" Text='<%# Eval("Forwader") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BL/AWB No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBLNo" runat="server" CssClass="col-form-label" Text='<%# Eval("BlAwhNo") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" CssClass="col-form-label" Text='<%# Eval("ItemCode") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>  
                                                <asp:TemplateField HeaderText="Invoice No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceNo" runat="server" CssClass="col-form-label" Text='<%# Eval("InvoiceNo") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField> 
                                            </Columns>
                                        </asp:GridView>                                    
                                    </div>
                                </div>   
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2">
                                    <asp:Label ID="lblCount" runat="server" CssClass="col-form-label" Text="Label"></asp:Label>
                                </div>                             
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                    <asp:HiddenField ID="hdnAction" runat="server" />
                                    <asp:HiddenField ID="hdnUser_EmpNo" runat="server" />
                                    <asp:HiddenField ID="hdnUser_EmpName" runat="server" />
                                    <asp:HiddenField ID="hdnItemID" runat="server" />
                                    <asp:HiddenField ID="hdnUserType" runat="server" />
                                    <asp:HiddenField ID="hdnKeyword" runat="server" />
                                    <asp:HiddenField ID="hdnUserDepCode" runat="server" />
                                </div> 
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2 text-right">
                                    <asp:LinkButton ID="lbtnExport" runat="server" CssClass="btn btn-success rounded-left" Visible="false" OnClick="lbtnExport_Click">
                                        &nbsp; <i class="fas fa-download"></i> &nbsp; Export to Excel &nbsp;
                                    </asp:LinkButton>
                                </div>                                        
                            </div>
                           <%-- <div class="form-group" id="divDataTable" runat="server">
                                <div class="card border border-secondary">
                                    <div class="card-header bg-dark text-white font-weight-bold"><span> DataTables Example</span></div>
                                    <div class="card-body">
                                        <div style="overflow-x: auto;">
                                            <div class="table-reponsive">
                                                <asp:GridView ID="gvDataTables" runat="server" AutoGenerateColumns="false" CssClass="dataTable order-column nowrap hover table table-bordered ">
                                                    <Columns>
                                                        <asp:BoundField DataField="EmpNo" HeaderText="EMPLOYEE #" />
                                                        <asp:BoundField DataField="EmpName" HeaderText="FULL NAME" />
                                                        <asp:BoundField DataField="EmpStatus" HeaderText="EMP STATUS" />
                                                        <asp:BoundField DataField="LaborType" HeaderText="LABOR TYPE" />
                                                        <asp:BoundField DataField="Position" HeaderText="POSITION" />
                                                        <asp:BoundField DataField="GAIASecCode" HeaderText="GAIA SECCODE" />
                                                        <asp:BoundField DataField="Section" HeaderText="SECTION" />
                                                        <asp:BoundField DataField="DepCode" HeaderText="DEPCODE" />
                                                        <asp:BoundField DataField="Dept" HeaderText="DEPT" />
                                                        <asp:BoundField DataField="Site" HeaderText="SITE" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnExport" />
        </Triggers>
    </asp:UpdatePanel>
    <div>
        <uc1:AlertMessage runat="server" ID="AlertMessage" />
    </div>
</asp:Content>

