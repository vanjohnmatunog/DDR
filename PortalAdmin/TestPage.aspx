<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestPage.aspx.cs" Inherits="PortalAdmin_TestPage" %>

<%@ Register Src="~/EmpSearch/EmpSearch.ascx" TagPrefix="uc1" TagName="EmpSearch" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

  
    
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
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="uPnlMain" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
     <div class="container-fluid">
        <div class="card border-info text-light text-info mt-1">
            <div class="card-header bg-info">
                <asp:Label ID="lblTestPage" runat="server" CssClass="font-weight-bold">
                    TEST PAGE
                </asp:Label>
            </div>
            <div class="card-body">                
                <div class="row small text-info">
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="Label1" runat="server" Text=" Employee No: "></asp:Label>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm">
                        <asp:Label ID="Label3" runat="server" Text=" Employee Name: "></asp:Label>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm"></div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm ">
                        <div class="form-group">
                            <div class="input-group input-group-sm">                                                        
                                <asp:TextBox ID="txtEmpNo" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="input-group-append">
                                    <asp:LinkButton ID="lbtnSearchEmp" runat="server" CssClass="btn btn-info rounded-right" OnClick="lbtnSearchEmp_Click" >
                                        <i class="fa fa-search"></i>
                                    </asp:LinkButton>
                                </div>
                                <asp:HiddenField ID="hdnAction" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-4 col-lg-4 col-md-4 col-sm-4 input-group-sm ">
                        <div class="form-group">
                            <div class="input-group input-group-sm">                                                        
                                <asp:TextBox ID="txtEmpName" runat="server" CssClass="form-control"></asp:TextBox>                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
         <div class="card border-info text-light text-info mt-1">
            <div class="card-header bg-info">
                <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold">
                    TEST PAGE
                </asp:Label>
            </div>
            <div class="card-body">                
                <div class="row small text-info">
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>  
                                <div class="table-responsive">                          
                                    <asp:GridView ID="grdTest" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                        EmptyDataText="There are no data records to display." AllowSorting="True" 
                                        OnPageIndexChanging="grdTest_PageIndexChanging">
                                        <Columns>
                                            <asp:BoundField DataField="Id" HeaderText="Id" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="Country" HeaderText="Country" />
                                            <asp:BoundField DataField="ID" HeaderText="Salary" DataFormatString="{0:N}" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("Id") %>'
                                                        class="btn btn-primary" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>'
                                                        class="btn btn-danger" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>                                    
                                 </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div>
        <uc1:EmpSearch runat="server" ID="EmpSearch" OnEmployeeSelected="Selected_Employee" />
    </div>
    <script type="text/javascript">
        /* Applied Responsive For Grid View using footable js*/
        $(function () {
            $('[id*=ContentPlaceHolder1_grdTest]').footable();
        });    
    </script>
    </ContentTemplate>    
</asp:UpdatePanel>
</asp:Content>

