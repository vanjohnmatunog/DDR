<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AlertMessage.ascx.cs" Inherits="MessageBox_AlertMessage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajax" %>

<%--<link href="../Media/CSS/AlertMessage.css" rel="stylesheet" type="text/css" />
<link href="../Media/CSS/FontAwesome.css" rel="stylesheet" type="text/css" />--%>

<asp:UpdatePanel ID="uPnlMain" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true" >
    <ContentTemplate>
        <div id="divMsg" runat="server" class="container-fluid card border-info mt-1" style="max-width:450px !important">
            <div class="modal-header">
                <asp:Label ID="lblTitle" runat="server" Text="Title" CssClass="font-weight-bold"></asp:Label>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-xl-2 col-lg-2 col-md-2 col-sm-2 align-self-center">
                        <asp:Label ID="lblImage" runat="server" CssClass="fa fa-question-circle fa-2x text-info"></asp:Label>
                    </div>
                    <div class="col-xl-10 col-lg-10 col-md-10 col-sm-10 align-self-center">
                        <div class="row">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <asp:Label ID="lblMessage" runat="server" Text="Message"></asp:Label>
                            </div>
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12">
                                <asp:Label ID="lblRemarks" runat="server" Text="Remarks" CssClass="small font-weight-bold"></asp:Label>
                                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>                
                    </div>
                </div>
            </div>
            <div class="modal-footer">                
                <asp:LinkButton ID="lbtnAccept" runat="server" CssClass="btn btn-success rounded-left" UseSubmitBehavior="False" OnClick="lbtnAccept_Click" ></asp:LinkButton> 
                <asp:LinkButton ID="lbtnDecline" runat="server" CssClass="btn btn-danger rounded-left" UseSubmitBehavior="False" OnClick="lbtnDecline_Click" >
                    <i class="fa fa-thumbs-down"></i> NO 
                </asp:LinkButton>      
            </div>
        </div>
        <asp:HiddenField ID="hdnMsgBox" runat="server"/>
        <ajax:ModalPopupExtender ID="mpMsgBox" runat="server" BackgroundCssClass="msgmodalBackground"
            TargetControlID="hdnMsgBox" PopupControlID="divMsg"  PopupDragHandleControlID="caption" RepositionMode="RepositionOnWindowResize" >
        </ajax:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>
