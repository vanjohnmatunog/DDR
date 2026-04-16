<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Registration.aspx.cs" Inherits="PageTransaction_Registration" %>

<%@ Register Src="~/MessageBox/AlertMessage.ascx" TagPrefix="uc1" TagName="AlertMessage" %>

<%@ Register Src="~/EmpSearch/EmpSearch.ascx" TagPrefix="uc1" TagName="EmpSearch" %>

<%@ Register Src="~/EmailSearch_QA/EmailSearch_QA.ascx" TagPrefix="uc1" TagName="EmailSearch_QA" %>

<%@ Register Src="~/EmailSearch_MPD/EmailSearch_MPD.ascx" TagPrefix="uc1" TagName="EmailSearch_MPD" %>

<%@ Register Src="~/EmailSearch_CopyTo/EmailSearch_CopyTo.ascx" TagPrefix="uc1" TagName="EmailSearch_CopyTo" %>

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
         /* The container */
        .chkContainer {
            display: block;
            position: relative;
            padding-left: 40px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: medium;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
            top: 3px;
            left: 0px;
            width: 264px;
            height: 24px;
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
    <div id="divStatusMenu">
    <asp:UpdatePanel ID="upnlStatusMenu" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="container-fluid">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

    <div id="divDamageInfo" runat="server">
    <asp:UpdatePanel ID="uPnlDamageInfo" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>            
            <div class="container-fluid" id="divDamageInfo_Header" runat="server">
                <div class="card border-secondary text-light text-info mt-1">
                    <div class="card-header bg-secondary" data-toggle="collapse" data-target="#divDamageInfo_Body">
                        <asp:Label ID="lblPCInfoHeader" runat="server" CssClass="font-weight-bold">
                            I. D A M A G E &nbsp; D E T A I L S
                        </asp:Label> 
                    </div>
                    <div id="divDamageInfo_Body" class="card-body collapse show py-0"> 
                        <div class="row small text-secondary">   
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblDDRNo" runat="server" CssClass="col-form-label text-dark"> DDR NO. : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtDDRNo" runat="server" CssClass="form-control font-weight-bold text-center" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                            </div>
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblStatus" runat="server" CssClass="col-form-label text-dark"> Status : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtStatus" runat="server" CssClass="form-control font-weight-bold text-center" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-4">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:Label ID="lblReqInfo" runat="server" CssClass="font-weight-bold large text-primary"> Requestor Information : </asp:Label>
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-1">
                                <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                    <asp:Label ID="lblIssuedBY" runat="server" CssClass="col-form-label text-dark"> Issued By : </asp:Label>
                                </div> 
                                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                     <asp:TextBox ID="txtIssuedBy" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White"></asp:TextBox>
                               </div> 
                               <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                     <asp:Label ID="lblIssuedDate" runat="server" CssClass="col-form-label text-dark"> Issued Date : </asp:Label>
                               </div>
                               <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                   <asp:TextBox ID="txtIssuedDate" runat="server" CssClass="form-control text-dark text-center" ReadOnly="true" BackColor="White"></asp:TextBox>
                               </div> 
                        </div>
                        <div class="row small text-secondary mt-1">
                               <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                     <asp:Label ID="lblAffecBU" runat="server" CssClass="col-form-label text-dark"> Affected BU : </asp:Label>
                               </div>
                               <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                   <asp:DropDownList ID="ddlAffecBU" CssClass="form-control text-dark alignCenter" runat="server"></asp:DropDownList>
                               </div> 
                               <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                   <asp:Label ID="lblLocal" runat="server" CssClass="col-form-label text-dark"> Local : </asp:Label>
                               </div> 
                               <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                   <asp:TextBox ID="txtLocal" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" EnableViewState="False" autocomplete="off" MaxLength="4" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                   <script type="text/javascript">
                                       function isNumberKey(evt) {
                                           var charCode = (evt.which) ? evt.which : event.keyCode;
                                           if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                                               return false;
                                           }
                                           return true;
                                       }
                                   </script>

                               </div>
                        </div>
                        <div class="row small text-secondary mt-3">
                        </div>
                        <div class="row small text-secondary mt-3">
                             <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                 <asp:Label ID="lblNoD" runat="server" CssClass="col-form-label text-dark"> Nature of Damage : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 ">
                                <div id="divNatureOfDamage" runat="server" class="input-group input-group-sm text-dark">
                                    <asp:RadioButtonList ID="rBtnNature" runat="server"  OnSelectedIndexChanged="rBtnNature_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Selected="false" Value="Dent">&nbsp; Dent </asp:ListItem> 
                                    <asp:ListItem Selected="false" Value="Hole">&nbsp; Hole </asp:ListItem>
                                    <asp:ListItem Selected="false" Value="LooseVacuum">&nbsp; Loose vacuum</asp:ListItem>
                                    <asp:ListItem Selected="false" Value="Torn">&nbsp; Torn </asp:ListItem> 
                                    <asp:ListItem Selected="false" Value="Wet">&nbsp; Wet </asp:ListItem>
                                    <asp:ListItem Selected="false" Value="Others">&nbsp; Others </asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="txtView" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" Visible="False"></asp:TextBox>
                                    <div class="row small text-secondary mt-5">
                                     <asp:Label ID="Label4" runat="server" Text="row1" Visible="False"></asp:Label>
                                    </div>
                                   <div class="row small text-secondary mt-5">
                                       
                                        <div class="row small text-secondary mt-5">
                                            <div class="input-group-append mt-5" style="margin-left:35px;">
                                                <div class=" row small text-secondary rowHeight " >
                                                    <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" Visible="False"></asp:TextBox>
                                                </div>
                                            </div>   
                                        </div>  
                                   </div>
                                </div>
                            </div> 
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                 <asp:Label ID="lblDamageNoteAt" runat="server" CssClass="col-form-label text-dark"> Damage Noted At : </asp:Label>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                 <asp:DropDownList ID="ddlDamageNoted" CssClass="form-control text-dark alignCenter" runat="server"></asp:DropDownList>
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-3">
                               <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                    <asp:Label ID="lblInvoiceNo" runat="server" CssClass="col-form-label text-dark"> Invoice No. : </asp:Label>
                                </div> 
                                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                    <asp:TextBox ID="txtInvoiceNo" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" EnableViewState="False" autocomplete="off"></asp:TextBox>
                                </div>
                                <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                    <asp:Label ID="lblItemCode" runat="server" CssClass="col-form-label text-dark"> Item Code : </asp:Label>
                                </div> 
                                <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" EnableViewState="False" autocomplete="off"></asp:TextBox>
                                </div>
                        </div>
                        <div class="row small text-secondary mt-1"> 
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblReceiveDate" runat="server" CssClass="col-form-label text-dark"> Receiving Date : </asp:Label>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="form-control form-control-sm datepicker alignCenter text-dark" placeholder="Receiving date" EnableViewState="False" autocomplete="off" OnTextChanged="txtReceiveDate_TextChanged" AutoPostBack="True"></asp:TextBox>
         
                            </div>
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblDesc" runat="server" CssClass="col-form-label text-dark"> Description : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" MaxLength="50" EnableViewState="False" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row small text-secondary mt-1">
                             <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                 <asp:Label ID="lblForwarder" runat="server" CssClass="col-form-label text-dark"> Forwarder : </asp:Label>
                             </div> 
                             <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                 <%--<asp:TextBox ID="txtForwarder" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White"></asp:TextBox>--%>
                                 <asp:DropDownList ID="ddlForwarder" CssClass="form-control text-dark alignCenter" runat="server"></asp:DropDownList>
                             </div>
                             <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblInvoiceQty" runat="server" CssClass="col-form-label text-dark"> Invoice Quantity : </asp:Label>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtInvoiceQty" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" EnableViewState="False" autocomplete="off" MaxLength="9" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                <script type="text/javascript">
                                    function isNumberKey(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode;
                                        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                                            return false;
                                        }
                                        return true;
                                    }
                                </script>

                            </div>
                        </div>
                        <div class="row small text-secondary mt-1">
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblAwhNo" runat="server" CssClass="col-form-label text-dark"> BL/AWH No/Peza# : </asp:Label>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtBANo" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" EnableViewState="False" autocomplete="off"></asp:TextBox>
                            </div>
                             <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                 <asp:Label ID="lblQty" runat="server" CssClass="col-form-label text-dark"> Quantity Affected : </asp:Label>
                             </div> 
                             <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                 <asp:TextBox ID="txtQtyAffected" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" EnableViewState="False" autocomplete="off" MaxLength="10" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                 <script type="text/javascript">
                                        function isNumberKey(evt) {
                                            var charCode = (evt.which) ? evt.which : event.keyCode;
                                            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                                                return false;
                                            }
                                            return true;
                                        }
                                 </script>

                             </div>
                        </div>
                        <div class="row small text-secondary mt-1">  
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblSupplier" runat="server" CssClass="col-form-label text-dark"> Supplier : </asp:Label>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                 <asp:DropDownList ID="ddlSupplier" CssClass="form-control text-dark alignCenter" runat="server" ></asp:DropDownList>
                            </div>
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblNoOfBox" runat="server" CssClass="col-form-label text-dark"> No. of box/pallet affected : </asp:Label>
                            </div>
                            <div class="col-xl-4 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtNoOfBox" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" EnableViewState="False" autocomplete="off" MaxLength="3" onkeypress="return isNumberKey(event)"></asp:TextBox>

                                <script type="text/javascript">
                                    function isNumberKey(evt) {
                                        var charCode = (evt.which) ? evt.which : event.keyCode;
                                        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                                            return false;
                                        }
                                        return true;
                                    }
                                </script>

                            </div>
                        </div>
                        <div class="row small text-secondary mt-1 mb-2 ">  
                            <div class="col-xl-2 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-3">
                                <asp:Label ID="lblRemarks" runat="server" CssClass="col-form-label text-dark"> Remarks : </asp:Label>
                            </div>
                            <div class="col-xl-8 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-2">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control text-dark" BackColor="White" TextMode="MultiLine" Height="60" EnableViewState="False" autocomplete="off" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                         <div class="row small text-secondary mt-5">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:Label ID="Label44" runat="server" CssClass="font-weight-bold large text-primary"> ATTACHMENT: </asp:Label>
                            </div>
                            <div id="divUploadAttach" runat="server" class="col-xl-8 col-lg-8 col-md-12 col-sm-12 input-group-sm mt-2">
                                <div class="input-group input-group-sm">
                                    <div class="input-group-append">
                                        <span class="input-group-text bg-secondary text-light"> Select File </span>
                                    </div>
                                    <div class="custom-file">
                                        <asp:FileUpload ID="fuAttachment" runat="server" class="custom-file-input" />
                                        <asp:Label CssClass="custom-file-label" ID="lblFilePath" AssociatedControlID="fuAttachment" runat="server"> Choose file </asp:Label>
                                        <%--<label class="custom-file-label" for="fuAttachment">Choose file</label>--%>
                                    </div> &nbsp;&nbsp;
                                    <%--<asp:FileUpload ID="fuAttachment" runat="server" CssClass="w-50"/> &nbsp;--%>
                                    <asp:LinkButton ID="lbtnUpload" runat="server" CssClass="btn btn-primary rounded-left" OnCommand="lbtnUpload_Command">
                                        &nbsp; <i class="fas fa-upload"></i> &nbsp; Upload &nbsp;
                                    </asp:LinkButton>                                            
                                </div>  
                            </div>                            
                        </div>                                           
                        <div class="row small text-secondary">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:Label ID="Label47" runat="server" CssClass="font-weight-bold large">   </asp:Label>
                             </div> 
                            <div class="col-xl-10 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">
                                <div class="row medium text-secondary"> 
                                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                            <asp:Label ID="Label29" runat="server" CssClass="col-form-label"> List of Attachments: </asp:Label>
                                            <asp:Label ID="Label43" runat="server" CssClass="col-form-label text-warning font-italic small" Text=" (Note: All changes on list of attachments is automatically saved to database.): "></asp:Label>
                                    </div>
                                    <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvAttachment" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                                EmptyDataText="There are no data records to display." AllowSorting="True" AllowPaging="true" >
                                                <Columns>
                                                    <asp:TemplateField HeaderText="No.">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex + 1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />                                                                               
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Filename">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFilename" runat="server" CssClass="col-form-label" Text='<%# Eval("FileName_Orig") %>'></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transacted By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreatedBy_EmpName" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedBy_EmpName") %>'></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Transacted Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCreatedDate" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedDate", "{0:dd-MMM-yyyy HH:mm}") %>'></asp:Label>                                                    
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                        <HeaderStyle CssClass="alignCenter" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtnOpen" runat="server" CssClass="btn btn-link text-success" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("FilePath") %>' OnCommand="lbtnOpen_Command">
                                                                <i class="fas fa-folder-open"></i> Open
                                                            </asp:LinkButton>
                                                            <asp:LinkButton ID="lbtnDelete" runat="server" CssClass="btn btn-link text-danger" CommandArgument='<%# Eval("ID") %>' CommandName='<%# Eval("FilePath") %>' OnCommand="lbtnDeleteAttachment_Command" >
                                                                <i class="fas fa-trash"></i> Delete
                                                            </asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                            
                                                </Columns>
                                            </asp:GridView>                               
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row small text-secondary mt-1 mb-2 ">  
                            <div class="col-xl-6 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-4 text-danger">
                                <asp:Label ID="lblNote" runat="server" CssClass="col-form-label"> NOTE : ALL FIELDS ARE REQUIRED.</asp:Label>
                            </div>
                           <%-- <div class="col-xl-8 col-lg-4 col-md-6 col-sm-6 input-group-sm mt-4 text-danger">
                                <asp:Label ID="lblNotes" runat="server" CssClass="col-form-label"> ALL FIELDS ARE REQUIRED.  </asp:Label>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnUpload" />
            <asp:PostBackTrigger ControlID="gvAttachment" />
            <%--<asp:PostBackTrigger ControlID="lblFilePath" />--%>
        </Triggers>
    </asp:UpdatePanel>
    
</div>
    <div id="divDocReceiver" runat="server">
        <asp:UpdatePanel ID="uPnlDocReceiver" runat="server" UpdateMode="Conditional" >
            <ContentTemplate> 
                 <div class="container-fluid" id="divDocReceiver_Header" runat="server">
                <div class="card border-secondary text-light text-info mt-1">
                    <div class="card-header bg-secondary" data-toggle="collapse" data-target="#divDocReceiver_Body">
                        <asp:Label ID="Label2" runat="server" CssClass="font-weight-bold">
                            II. D O C U M E N T &nbsp; R E C E I V E R
                        </asp:Label> 
                    </div>
                    <div id="divDocReceiver_Body" class="card-body collapse show py-0"> 
                        <div class="row small text-secondary mt-3">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblSendToQA" runat="server" CssClass="font-weight-bold large text-dark"> Sent To QA : </asp:Label>
                            </div> 
                            <div class="col-xl-8 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">                         
                                 <asp:TextBox ID="txtSendToQA" runat="server" CssClass="form-control text-dark" BackColor="White" TextMode="MultiLine" Height="60" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row small text-secondary">
                            <div class="col-xl-12 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:LinkButton ID="lBtnAdd_sendToQA" runat="server" CssClass="btn btn-success rounded-left" OnClick="lBtnAdd_sendToQA_Click" AutoPostBack="True">
                                &nbsp; <i class="fa fa-user-plus"></i> Add 
                                </asp:LinkButton> &nbsp; &nbsp;
                                <asp:LinkButton ID="lBtnRemove_sendToQA" runat="server" CssClass="btn btn-danger rounded-left" OnClick="lBtnRemove_sendToQA_Click" AutoPostBack="True">
                                &nbsp; <i class="fa fa-trash-alt"></i> Remove 
                                </asp:LinkButton> 
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-3">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblSendToMPD" runat="server" CssClass="font-weight-bold large text-dark"> Sent To Purchasing/Planning : </asp:Label>
                            </div> 
                            <div class="col-xl-8 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">                         
                                 <asp:TextBox ID="txtSendToMPD" runat="server" CssClass="form-control text-dark" BackColor="White" TextMode="MultiLine" Height="60" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row small text-secondary">
                            <div class="col-xl-12 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:LinkButton ID="lBtnAdd_sendToMPD" runat="server" CssClass="btn btn-success rounded-left" OnClick="lBtnAdd_sendToMPD_Click" AutoPostBack="True">
                                &nbsp; <i class="fa fa-user-plus"></i> Add 
                                </asp:LinkButton> &nbsp; &nbsp;
                                <asp:LinkButton ID="lBtnRemove_sendToMPD" runat="server" CssClass="btn btn-danger rounded-left" OnClick="lBtnRemove_sendToMPD_Click" AutoPostBack="True">
                                &nbsp; <i class="fa fa-trash-alt"></i> Remove 
                                </asp:LinkButton> 
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-3">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblCopyTo" runat="server" CssClass="font-weight-bold large text-dark"> Copy To : </asp:Label>
                            </div> 
                            <div class="col-xl-8 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">                         
                                 <asp:TextBox ID="txtCopyTo" runat="server" CssClass="form-control text-dark" BackColor="White" TextMode="MultiLine" Height="60" ReadOnly="True"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row small text-secondary mb-2">
                            <div class="col-xl-12 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:LinkButton ID="lBtnAdd_copyTo" runat="server" CssClass="btn btn-success rounded-left" OnClick="lBtnAdd_copyTo_Click" AutoPostBack="True">
                                &nbsp; <i class="fa fa-user-plus"></i> Add 
                                </asp:LinkButton> &nbsp; &nbsp;
                                <asp:LinkButton ID="lBtnRemove_copyTo" runat="server" CssClass="btn btn-danger rounded-left" OnClick="lBtnRemove_copyTo_Click" AutoPostBack="True">
                                &nbsp; <i class="fa fa-trash-alt"></i> Remove 
                                </asp:LinkButton> 
                            </div> 
                        </div>

                        
                       
                        </div>
                 </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>       
    </div>
    <div id="divApprover" runat="server">
        <asp:UpdatePanel ID="uPnlApprover" runat="server" UpdateMode="Conditional" >
            <ContentTemplate> 
                 <div class="container-fluid" id="divApprover_Header" runat="server">
                <div class="card border-secondary text-light text-info mt-1">
                    <div class="card-header bg-secondary" data-toggle="collapse" data-target="#divApprover_Body">
                        <asp:Label ID="Label8" runat="server" CssClass="font-weight-bold">
                            III. A P P R O V E R
                        </asp:Label> 
                    </div>
                    <div id="divApprover_Body" class="card-body collapse show py-0"> 
                        <div class="row small text-secondary text-center mt-3">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                            </div> 
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-right">
                                <asp:Label ID="lblApprovedBy" runat="server" CssClass="font-weight-bold large text-dark"> Approved by : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-left">
                                <asp:DropDownList ID="ddlApprover" CssClass="form-control text-dark alignCenter" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlApprover_SelectedIndexChanged" ></asp:DropDownList>
                                <asp:TextBox ID="txtApprover" runat="server" CssClass="form-control text-dark alignCenter" BackColor="White" ReadOnly="True" Visible="False"></asp:TextBox>
                            </div>   
                        </div>
                        <div class="row small text-secondary">
                            <div class="col-xl-12 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:Label ID="lblReqStat" runat="server" CssClass="font-weight-bold large" Visible="false"> Date Approved : </asp:Label>
                            </div>   
                        </div>
                        <%--<div class="row small text-secondary">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2">
                            </div> 
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-right">
                            </div> 
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-right">
                                <asp:Label ID="lblDateApp" runat="server" CssClass="font-weight-bold large text-danger" Visible="false"> Date Approved : </asp:Label>
                                <asp:Label ID="lblDateDen" runat="server" CssClass="font-weight-bold large text-danger" Visible="false"> Date Denied : </asp:Label>
                            </div>   
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-left">
                                <asp:Label ID="lblDateApproved" runat="server" CssClass="font-weight-bold large text-danger" Visible="false"> [Date Approved] </asp:Label>
                                <asp:Label ID="lblDateDenied" runat="server" CssClass="font-weight-bold large text-danger" Visible="false"> [Date Denied] </asp:Label>
                            </div>  
                        </div>--%>
                        <div class="row input-group input-group-sm mt-2 mb-2">
                             <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 text-center"> 
                                 <asp:LinkButton ID="lbtnApprove_App" runat="server" CssClass="btn btn-success rounded-left" Visible="true" OnClick="lbtnApprove_App_Click">
                                    &nbsp; <i class="fa fa-thumbs-up"></i> &nbsp; Approve &nbsp;
                                 </asp:LinkButton>
                                 <asp:LinkButton ID="lbtnDeny_App" runat="server" CssClass="btn btn-danger rounded-left" Visible="true" OnClick="lbtnDeny_App_Click">
                                    &nbsp; <i class="fa fa-thumbs-down"></i> &nbsp; Deny &nbsp;
                                 </asp:LinkButton> 
                                 <asp:LinkButton ID="lbtnSubmit_App" runat="server" CssClass="btn btn-success rounded-left" Visible="true" OnClick="lbtnSubmit_App_Click">
                                    &nbsp; <i class="fa fa-save"></i> &nbsp; Submit &nbsp;
                                 </asp:LinkButton>
                                 <asp:LinkButton ID="lbtnSave_App" runat="server" CssClass="btn btn-info rounded-left" Visible="true" OnClick="lbtnSave_App_Click" AutoPostBack="True">
                                    &nbsp; <i class="fa fa-save"></i> &nbsp; Save &nbsp;
                                 </asp:LinkButton>  
                                 <asp:LinkButton ID="lbtnUpdate_App" runat="server" CssClass="btn btn-info rounded-left" Visible="false" AutoPostBack="True" OnClick="lbtnUpdate_App_Click">
                                    &nbsp; <i class="fa fa-save"></i> &nbsp; Update &nbsp;
                                 </asp:LinkButton> 
                                 <asp:LinkButton ID="lbtnEdit" runat="server" CssClass="btn btn-success rounded-left" Visible="false" AutoPostBack="True" OnClick="lbtnEdit_Click"  >
                                     &nbsp; <i class="fa fa-edit"></i> &nbsp; Edit &nbsp;
                                 </asp:LinkButton> 
                                 <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-danger rounded-left" Visible="false" OnClick="lbtnCancel_Click">
                                     &nbsp; <i class="fa fa-times"></i> &nbsp; Cancel Edit &nbsp;
                                 </asp:LinkButton>
                                 <asp:LinkButton ID="lbtnReassign" runat="server" CssClass="btn btn-success rounded-left" Visible="false" AutoPostBack="True" OnClick="lbtnReassign_Click">
                                     &nbsp; <i class="fa fa-user"></i> &nbsp; Re-assign &nbsp;
                                 </asp:LinkButton> 
                                 <asp:LinkButton ID="lbtnCancelReassign" runat="server" CssClass="btn btn-danger rounded-left" Visible="false" OnClick="lbtnCancelReassign_Click">
                                     &nbsp; <i class="fa fa-times"></i> &nbsp; Cancel Re-assign &nbsp;
                                 </asp:LinkButton>
                                 
                             </div> 
                        </div>
                     </div>
                 </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>       
    </div>
    <div id="divConcern" runat="server">
        <asp:UpdatePanel ID="uPnlConcern" runat="server" UpdateMode="Conditional" >
            <ContentTemplate> 
                 <div class="container-fluid" id="divConcern_Header" runat="server">
                <div class="card border-secondary text-light text-info mt-1">
                    <div class="card-header bg-secondary" data-toggle="collapse" data-target="#divConcern_Body">
                        <asp:Label ID="Label9" runat="server" CssClass="font-weight-bold">
                            IV. C O N C E R N E D &nbsp; B U &nbsp; R E M A R K S 
                        </asp:Label> 
                    </div>
                    <div id="divConcern_Body" class="card-body collapse show py-0"> 
                        <div class="row small text-secondary mt-1">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:Label ID="lblSQA" runat="server" CssClass="font-weight-bold large text-primary"> For QA Remarks: </asp:Label>
                            </div>
                        </div>
                        <div class="row small text-secondary mt-2">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblDispositionSQA" runat="server" CssClass="font-weight-bold large text-dark"> Disposition : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:DropDownList ID="ddlDisposition_SQA" CssClass="form-control alignCenter" runat="server" ></asp:DropDownList>
                            </div> 
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblDate_SQA" runat="server" CssClass="font-weight-bold large text-dark"> Date : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:TextBox ID="txtBURemarksDate_SQA" runat="server" CssClass="form-control text-dark text-center alignCenter" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-2 mb-2">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:Label ID="lblComments_SQA" runat="server" CssClass="font-weight-bold large text-dark"> Comment(s) : </asp:Label>
                            </div> 
                            <div class="col-xl-8 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:TextBox ID="txtComments_SQA" runat="server" CssClass="form-control text-dark" BackColor="White" TextMode="MultiLine" Height="60"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row input-group input-group-sm mt-2">
                             <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 text-center"> 
                                 <asp:LinkButton ID="lbtnSubmit_SQA" runat="server" CssClass="btn btn-success rounded-left" OnClick="lbtnSubmit_SQA_Click">
                                    &nbsp; <i class="fa fa-save"></i> &nbsp; Submit &nbsp;
                                 </asp:LinkButton>
                             </div> 
                        </div>

                         <div class="row small text-secondary">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:Label ID="lblMPD" runat="server" CssClass="font-weight-bold large text-primary" Visible="False"> For MPD Remarks: </asp:Label>
                            </div>
                        </div>
                        <div class="row small text-secondary mt-2">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblDispositionMPD" runat="server" CssClass="font-weight-bold large text-dark" Visible="False"> Disposition : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:DropDownList ID="ddlDisposition_MPD" CssClass="form-control alignCenter" runat="server" Visible="False" ></asp:DropDownList>
                            </div> 
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblDate_MPD" runat="server" CssClass="font-weight-bold large text-dark" Visible="False"> Date : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:TextBox ID="txtBURemarksDate_MPD" runat="server" CssClass="form-control text-dark text-center alignCenter" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --" Visible="False"></asp:TextBox>
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-2 mb-2">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:Label ID="lblComments_MPD" runat="server" CssClass="font-weight-bold large text-dark" Visible="False"> Comment(s) : </asp:Label>
                            </div> 
                            <div class="col-xl-8 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:TextBox ID="txtComments_MPD" runat="server" CssClass="form-control text-dark" BackColor="White" TextMode="MultiLine" Height="60" Visible="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row input-group input-group-sm mt-2 mb-2">
                             <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 text-center"> 
                                 <asp:LinkButton ID="lbtnSubmit_MPD" runat="server" CssClass="btn btn-success rounded-left" Visible="False" OnClick="lbtnSubmit_MPD_Click">
                                    &nbsp; <i class="fa fa-save"></i> &nbsp; Submit &nbsp;
                                 </asp:LinkButton>
                             </div> 
                        </div>

                     </div>
                 </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>       
    </div>
    <div id="divFinalDis" runat="server">
        <asp:UpdatePanel ID="uPnlFinalDis" runat="server" UpdateMode="Conditional" >
            <ContentTemplate> 
                 <div class="container-fluid" id="divFinalDis_Header" runat="server">
                <div class="card border-secondary text-light text-info mt-1">
                    <div class="card-header bg-secondary" data-toggle="collapse" data-target="#divFinalDis_Body">
                        <asp:Label ID="Label15" runat="server" CssClass="font-weight-bold">
                            V. F I N A L &nbsp; D I S P O S I T I O N 
                        </asp:Label> 
                    </div>
                    <div id="divFinalDis_Body" class="card-body collapse show py-0"> 
                        <div class="row small text-secondary mt-3">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblFinalDisposition" runat="server" CssClass="font-weight-bold large text-dark"> Disposition : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:DropDownList ID="ddlFinalDisposition" CssClass="form-control alignCenter" runat="server" ></asp:DropDownList>
                            </div> 
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-3 text-center">
                                <asp:Label ID="lblFinalDate" runat="server" CssClass="font-weight-bold large text-dark"> Date : </asp:Label>
                            </div> 
                            <div class="col-xl-4 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:TextBox ID="txtFinalDate" runat="server" CssClass="form-control text-dark text-center alignCenter" ReadOnly="true" BackColor="White" placeholder="-- Auto Generated --"></asp:TextBox>
                            </div> 
                        </div>
                        <div class="row small text-secondary mt-2 mb-2">
                            <div class="col-xl-2 col-lg-2 col-md-12 col-sm-12 input-group-sm mt-2 text-center">
                                <asp:Label ID="lblFinalComments" runat="server" CssClass="font-weight-bold large text-dark"> Comment(s) : </asp:Label>
                            </div> 
                            <div class="col-xl-8 col-lg-10 col-md-12 col-sm-12 input-group-sm mt-2">
                                <asp:TextBox ID="txtFinalComments" runat="server" CssClass="form-control text-dark" BackColor="White" TextMode="MultiLine" Height="60"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row input-group input-group-sm mt-2 mb-2">
                             <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 text-center"> 
                                 <asp:LinkButton ID="lbtnSubmit_Final" runat="server" CssClass="btn btn-success rounded-left" Visible="true" OnClick="lbtnSubmit_Final_Click">
                                    &nbsp; <i class="fa fa-save"></i> &nbsp; Submit &nbsp;
                                 </asp:LinkButton>
                             </div> 
                        </div>
                     </div>
                 </div>
            </div>
            </ContentTemplate>
        </asp:UpdatePanel>       
    </div>
     <asp:UpdatePanel ID="upnlLogs" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="container-fluid" id="divLogs" runat="server">
                <div class="card border-secondary text-light text-info mt-1">
                    <div class="card-header bg-secondary">
                        <asp:Label ID="lblLogsHeader" runat="server" CssClass="font-weight-bold">
                            L O G S
                        </asp:Label>
                    </div>
                    <div id="divLogs_Body" class="card-body">                
                        <div class="row small text-info">
                            <div class="col-xl-12 col-lg-12 col-md-12 col-sm-12 input-group-sm">
                                <div class="table-responsive">                          
                                    <asp:GridView ID="gvHistoryLogs" CssClass="footable" runat="server" AutoGenerateColumns="false" 
                                        EmptyDataText="There are no data records to display." PageSize="5" AllowSorting="True" AllowPaging="True" OnPageIndexChanging="gvHistoryLogs_PageIndexChanging" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="No.">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />                                                                               
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Action Taken">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActionTaken" runat="server" CssClass="col-form-label" Text='<%# Eval("ActionTaken") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>   
                                           <%-- <asp:TemplateField HeaderText="Details">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDetails" runat="server" CssClass="col-form-label" Text='<%# Eval("Details") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField> --%>                                                                             
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRemarks" runat="server" CssClass="col-form-label" Text='<%# Eval("Remarks") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transacted By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedBy_EmpName" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedBy") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Transacted Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCreatedDate" runat="server" CssClass="col-form-label" Text='<%# Eval("CreatedDate", "{0:dd-MMM-yyyy HH:mm}") %>'></asp:Label>                                                    
                                                </ItemTemplate>
                                                <ItemStyle CssClass="rowHeight alignCenter"/> 
                                                <HeaderStyle CssClass="alignCenter" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>                                    
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


    <div id="divActionButton">
        <asp:UpdatePanel ID="upnlActionButton" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <div class="container-fluid">
                    <div class="row input-group input-group-sm mt-2 mb-2">
                        <div class="col-md-12 col-lg-12 col-xl-12 col-sm-12 text-center"> 
                            <asp:HiddenField ID="hdnRefID" runat="server" />     
                            <asp:HiddenField ID="hdnAction" runat="server" />
                            <asp:HiddenField ID="hdnUserEmpNo" runat="server" />
                            <asp:HiddenField ID="hdnUserEmpName" runat="server" />
                            <asp:HiddenField ID="hdnUserDepCode" runat="server" />
                            <asp:HiddenField ID="hdnUserDepartment" runat="server" />
                            <asp:HiddenField ID="hdnUserDept" runat="server" />
                            <asp:HiddenField ID="hdnUserSecCode" runat="server" />
                            <asp:HiddenField ID="hdnUserType" runat="server" />
                            <asp:HiddenField ID="hdnReqStat" runat="server" />
                            <asp:HiddenField ID="hdnAttachmentID" runat="server" />
                            <asp:HiddenField ID="hdnAttachmentPath" runat="server" />       
                            <asp:HiddenField ID="hdnEmpNoQA" runat="server" /> 
                            <asp:HiddenField ID="hdnEmpNameQA" runat="server" /> 
                            <asp:HiddenField ID="hdnDeptQA" runat="server" /> 
                            <asp:HiddenField ID="hdnEmpNoMPD" runat="server" /> 
                            <asp:HiddenField ID="hdnEmpNameMPD" runat="server" /> 
                            <asp:HiddenField ID="hdnDeptMPD" runat="server" /> 
                            <asp:HiddenField ID="hdnEmpNoCopy" runat="server" /> 
                            <asp:HiddenField ID="hdnEmpNameCopy" runat="server" /> 
                            <asp:HiddenField ID="hdnDeptCopy" runat="server" /> 
                            <asp:HiddenField ID="hdnLink" runat="server" /> 
                            <asp:HiddenField ID="hdnAppEmail" runat="server" /> 
                            <asp:HiddenField ID="hdnAppEmpNo" runat="server" /> 
                            <asp:HiddenField ID="hdnDateApproved" runat="server" /> 
                            <asp:HiddenField ID="hdnDateDenied" runat="server" /> 
                            <asp:HiddenField ID="hdnAdminButton" runat="server" /> 
                            <asp:HiddenField ID="hdnSQAEmail" runat="server" /> 
                            <asp:HiddenField ID="hdnMPDEmail" runat="server" />  
                        </div> 
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div>
        <uc1:EmailSearch_QA runat="server" ID="EmailSearch_QA" OnEmployeeSelected="Selected_Employee_QA"/>
    </div>
     <div>
        <uc1:EmailSearch_MPD runat="server" ID="EmailSearch_MPD" OnEmployeeSelected="Selected_Employee_MPD"/>
    </div>
     <div>
        <uc1:EmailSearch_CopyTo runat="server" ID="EmailSearch_CopyTo" OnEmployeeSelected="Selected_Employee_CopyTo"/>
    </div>
    <div>
        <uc1:AlertMessage runat="server" ID="AlertMessage" /> 
    </div> 
    <div>
        <uc1:EmpSearch runat="server" ID="EmpSearch" />
    </div> 
     

</asp:Content>

