<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmailSearch_QA.ascx.cs" Inherits="EmailSearch_QA_EmailSearch_QA" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

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

<asp:UpdatePanel ID="uPnlMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" >
    <ContentTemplate>
        <div id="divMsg" runat="server" class="container-fluid card border-info mt-1" style="max-width:600px !important">
            <div class="modal-header">
                <asp:Label ID="lblTitle" runat="server" Text="Search Employee" CssClass="font-weight-bold"></asp:Label>
                <asp:LinkButton ID="lbntCancel" runat="server" CssClass="close" OnClick="lbntCancel_Click" >&times;</asp:LinkButton>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2">
                        <asp:Label ID="lblKeyword" runat="server" CssClass="small" > Keyword: </asp:Label>
                    </div>
                    <div class="form-group col-xl-7 col-lg-7 col-md-7 col-sm-7 input-group input-group-sm">                                                
                        <asp:TextBox ID="txtKeyword" runat="server" placeholder="Search....." CssClass="form-control"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn btn-info" OnClick="lbtnSearch_Click" >
                                <i class="fa fa-search"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12"></div>
                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                        <div class="table-responsive mh-50">                          
                            <asp:GridView ID="grdEmployee" CssClass="footable" runat="server" AutoGenerateColumns="false" EmptyDataText="There are no data records to display." AllowPaging="True" 
                                AllowSorting="True" PageSize="5" OnPageIndexChanging="grdEmployee_PageIndexChanging"  >
                                <Columns>
                                    <asp:TemplateField HeaderText="#">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                        <HeaderStyle CssClass="alignCenter" />                                                                               
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Employee Email"  >
                                        <ItemTemplate>
                                            <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowHeight alignCenter" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dept">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDept" runat="server" Text='<%# Eval("Dept") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowHeight alignCenter" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Button ID="btnSelect" runat="server" Text="Select" CommandArgument='<%# Eval("EmpNo") %>' CommandName='<%# Eval("Email") %>'
                                                class="btn btn-primary btn-sm w-75" UseSubmitBehavior="false" OnCommand="btnSelect_Command"/>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="rowHeight alignCenter" />
                                    </asp:TemplateField>                         
                                </Columns>
                                <PagerStyle CssClass="rowHeight" />
                            </asp:GridView> 
                        </div>              
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnEmployeeSearch" runat="server"/>
        <asp:HiddenField ID="hdnSelected" runat="server"/>
        <asp:HiddenField ID="hdnTransType" runat="server"/>
        <ajax:ModalPopupExtender ID="mpEmpSearch" runat="server" BackgroundCssClass="msgmodalBackground" TargetControlID="hdnEmployeeSearch" PopupControlID="divMsg"  
            PopupDragHandleControlID="caption" RepositionMode="RepositionOnWindowResize" >
        </ajax:ModalPopupExtender>  
        <script type="text/javascript">
            /* Applied Responsive For Grid View using footable js*/
            $(function () {
                $('[id*=ContentPlaceHolder1_EmailSearch_grdEmployee]').footable();
            });    
        </script>      
    </ContentTemplate>
</asp:UpdatePanel>