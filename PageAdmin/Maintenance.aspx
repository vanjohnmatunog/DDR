<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Maintenance.aspx.cs" Inherits="PageAdmin_Maintenance" %>

<%@ Register Src="~/MessageBox/AlertMessage.ascx" TagPrefix="uc1" TagName="AlertMessage" %>

<%@ Register Src="~/EmpSearch/EmpSearch.ascx" TagPrefix="uc1" TagName="EmpSearch" %>

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
    <asp:UpdatePanel ID="uPnlMaintenance" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>            
            <div class="container-fluid">
                    <div class="card border-secondary text-light text-info mt-1">
                        <div class="card-header bg-secondary">
                            <asp:Label ID="lblMaintenanceHeader" runat="server" CssClass="font-weight-bold">
                                L O V &nbsp; M A I N T E N A N C E
                            </asp:Label>
                        </div>
                        <div class="card-body"> 
                            <div class="row small text-secondary">  
                                <div class="form-group col-xl-5 col-lg-5 col-md-5 col-sm-12 input-group text-info small">         
                                    <span class="input-group-text small rounded-0"> Category </span>
                                    <asp:DropDownList ID="ddlCategory" CssClass="form-control text-dark" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                </div>  
                                <div class="form-group col-xl-5 col-lg-4 col-md-4 col-sm-8 input-group text-info small">         
                                    <span class="input-group-text small rounded-0"> Keyword </span>
                                    <asp:TextBox ID="txtKeyword" runat="server" placeholder=" Search...." CssClass="form-control"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-secondary" OnClick="lbtnSearch_Click" >
                                            <i class="fa fa-search"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div> 
                                <div class="form-group col-xl-2 col-lg-3 col-md-3 col-sm-4 input-group text-info small">         
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
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2">
                                    <div class="table-responsive">                          
                                        <asp:GridView ID="grdMasterList" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                            EmptyDataText="There are no data records to display." AllowPaging="True" OnPageIndexChanging="grdMasterList_PageIndexChanging" PageSize="5">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter" />                                                                               
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Category">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCategory" runat="server" CssClass="col-form-label" Text='<%# Eval("Category") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" CssClass="col-form-label" Text='<%# Eval("ItemCode") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescription" runat="server" CssClass="col-form-label" Text='<%# Eval("Description") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Main CTQ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAttribute1" runat="server" CssClass="col-form-label" Text='<%# Eval("Attribute1") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>                                                       
                                                        <div class="chkContainer small text-dark col-form-label text-center">
                                                            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("Active") %>'/>
                                                            <span class="checkmark rounded-bottom rounded-top"></span>
                                                        </div>                                                                                                         
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                    <ItemStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transacted By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUpdatedBy" runat="server" CssClass="col-form-label" Text='<%# Eval("UpdatedBy") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Transacted Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUpdatedDate" runat="server" CssClass="col-form-label" Text='<%# Eval("UpdatedDate", "{0:dd-MMM-yyyy HH:mm}") %>'></asp:Label>                                                    
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                    <HeaderStyle CssClass="alignCenter" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="btn btn-info rounded-left" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("ItemCode") %>' OnCommand="lbtnEdit_Command" >
                                                        &nbsp; <i class="fa fa-pencil-alt"></i> &nbsp; Edit &nbsp;
                                                    </asp:LinkButton>
                                                    <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-danger rounded-left" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("ItemCode") %>' OnCommand="lbtnDelete_Command">
                                                        &nbsp; <i class="fa fa-trash"></i> &nbsp; Delete &nbsp;
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>                                    
                                    </div>
                                </div>
                                <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                    <asp:Label ID="lblCategory" runat="server" CssClass="col-form-label"> Category: </asp:Label>
                                    <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control text-dark" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-lg-3 col-md-4 col-sm-4 input-group-sm mt-2">
                                    <asp:Label ID="lblItem" runat="server" CssClass="col-form-label"> Item: </asp:Label>
                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control text-dark" BackColor="White"></asp:TextBox>
                                    <div id="divEmpNo" runat="server" class="input-group input-group-sm">                            
                                    <asp:TextBox ID="txtEmpNo" runat="server" CssClass="form-control" ReadOnly="true" BackColor="White"></asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="lbtnEmpSearch" runat="server" CssClass="btn btn-secondary" Style="z-index:0;" OnClick="lbtnEmpSearch_Click" >
                                            <i class="fa fa-search"></i>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                                </div>
                                <div class="col-xl-3 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                    <asp:Label ID="lblDescription" runat="server" CssClass="col-form-label"> Description: </asp:Label>
                                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control text-dark" BackColor="White"></asp:TextBox>
                                </div>
                                <div id="divMainCTQ" runat="server" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 input-group-sm mt-2" visible="false">
                                    <asp:Label ID="lblSite" runat="server" CssClass="col-form-label"> Main CTQ: </asp:Label>
                                    <asp:DropDownList ID="ddlMainCTQ" runat="server" CssClass="form-control text-dark"></asp:DropDownList>
                                </div>
                                <div id="divDept" runat="server" class="col-xl-3 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2" visible="false">
                                    <asp:Label ID="lblDept" runat="server" CssClass="col-form-label"> Dept: </asp:Label>
                                    <asp:TextBox ID="txtDept" runat="server" CssClass="form-control text-dark" ReadOnly="true" BackColor="White"></asp:TextBox>
                                </div>
                                <div id="divPJCategory" runat="server" class="col-xl-2 col-lg-3 col-md-4 col-sm-4 input-group-sm mt-2" visible="false">
                                    <asp:Label ID="lblPJCategory" runat="server" CssClass="col-form-label"> Project Category: </asp:Label>
                                    <asp:DropDownList ID="ddlPJCategory" runat="server" CssClass="form-control text-dark"></asp:DropDownList>
                                </div>
                                <div class="col-xl-1 col-lg-1 col-md-2 col-sm-2 input-group-sm mt-2">
                                    <asp:Label ID="lblActive" runat="server" CssClass="col-form-label"> Active? </asp:Label>
                                    <label class="chkContainer small text-dark" style="font-size: 14px; padding-top: 3pt">
                                        <asp:CheckBox ID="chkActive" runat="server"/>
                                        <span class="checkmark rounded-bottom rounded-top"></span>
                                    </label>
                                </div>
                                <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                    <asp:LinkButton ID="lbtnSave" runat="server" CssClass="btn btn-success rounded-left" Visible="false" OnClick="lbtnSave_Click" >
                                        &nbsp; <i class="fa fa-save"></i> &nbsp; Save &nbsp;
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-success rounded-left" Visible="false" OnClick="lbtnUpdate_Click" >
                                        &nbsp; <i class="fa fa-save"></i> &nbsp; Update &nbsp;
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-danger rounded-left" Visible="false" OnClick="lbtnCancel_Click" >
                                        &nbsp; <i class="fa fa-redo "></i> &nbsp; Reset &nbsp;
                                    </asp:LinkButton><asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:HiddenField ID="hdnAction" runat="server" />
                                    <asp:HiddenField ID="hdnUser_EmpNo" runat="server" />
                                    <asp:HiddenField ID="hdnUser_EmpName" runat="server" />
                                    <asp:HiddenField ID="hdnItemID" runat="server" />
                                </div>                                
                            </div>
                        </div>
                    </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <uc1:AlertMessage runat="server" ID="AlertMessage" />
    </div>
    <div>
        <uc1:EmpSearch runat="server" ID="EmpSearch" OnEmployeeSelected="Selected_Employee" />
    </div> 
</asp:Content>

