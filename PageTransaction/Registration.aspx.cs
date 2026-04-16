using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;

public partial class PageTransaction_Registration : System.Web.UI.Page
{
    i_Employee emp = new cl_Employee();
    i_DataLayer dl = new cl_DataLayer();
    cl_DR_DMGTransactionObject dmg = new cl_DR_DMGTransactionObject();
    cl_DR_BURemarksObject bu = new cl_DR_BURemarksObject();
    cl_DR_FinalDispositionObject fnl = new cl_DR_FinalDispositionObject();
    cl_BP_PJMemberObject pmo = new cl_BP_PJMemberObject();

    cl_DataTransferObject dto = new cl_DataTransferObject();

    private static Boolean isAdmin = false;
    private static Boolean isLeader = false;
    private static Boolean isApprover = false;
    private static Boolean isRequestor = false;
    private static Boolean isSQA = false;
    private static Boolean isMPD = false;

    private const string UPDATE_APPROVER_EVENT = "UPDATE_APPROVER_EVENT";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            hdnRefID.Value = (!string.IsNullOrEmpty(Request.QueryString["RefID"])) ? Request.QueryString["RefID"] : "0";
            if (emp.HasRows)
            {
                hdnUserEmpNo.Value = emp.EmpNo;
                hdnUserEmpName.Value = emp.EmpName;
                hdnUserDepCode.Value = emp.DepCode;
                hdnUserDepartment.Value = emp.Department;
                hdnUserDept.Value = emp.Dept;
                hdnUserSecCode.Value = emp.SecCode;
                uPnlDamageInfo.Update();
                isAdmin = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is admin", "Administrator", emp.EmpNo, "", ""));
                isLeader = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is leader", "Warehouse Leader", emp.EmpNo, "", ""));
                isRequestor = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is requestor", "Requestor", emp.EmpNo, "", ""));
                isApprover = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is approver", "", emp.EmpNo, "", hdnUserDepCode.Value));

                if (!isAdmin)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "MenuHide('five-ddheader');", true);
                }
                if (!isRequestor && !isAdmin && !isApprover && !isLeader)
                {
                    if (hdnRefID.Value == "0")
                    {
                        Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
                    }
                    else
                    {
                        divLogs.Visible = true;
                        Load_ListofValues();
                        Load_Attachment();
                        Load_Information("per refid", hdnRefID.Value);
                        if (hdnReqStat.Value.ToLower() == "for mpd remarks")
                        {
                            LoadRemarks_SQA("get sqa remarks", hdnRefID.Value);

                            ddlDisposition_SQA.Enabled = false;
                            txtComments_SQA.Enabled = false;
                            lbtnSubmit_SQA.Visible = false;

                            divFinalDis.Visible = false;

                        }
                        else if (hdnReqStat.Value.ToLower() == "for final disposition")
                        {
                            LoadRemarks_SQA("get sqa remarks", hdnRefID.Value);
                            if (ddlDisposition_SQA.SelectedValue.ToLower() == "reject")
                            {
                                LoadRemarks_MPD("get mpd remarks", hdnRefID.Value);

                                lblDispositionMPD.Visible = true;
                                ddlDisposition_MPD.Visible = true;
                                lblDate_MPD.Visible = true;
                                txtBURemarksDate_MPD.Visible = true;
                                lblComments_MPD.Visible = true;
                                txtComments_MPD.Visible = true;

                            }
                        }
                        else if (hdnReqStat.Value.ToLower() == "closed")
                        {
                            LoadRemarks_SQA("get sqa remarks", hdnRefID.Value);
                            LoadFinalDisposition("get final disposition", hdnRefID.Value);
                            if (ddlDisposition_SQA.SelectedValue.ToLower() == "reject")
                            {
                                LoadRemarks_MPD("get mpd remarks", hdnRefID.Value);
                            }
                        }
                    }
                    
                }
            }
            else
            {
                Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
            }
            emp.Dispose();

            //hdnRefID.Value = (!string.IsNullOrEmpty(Request.QueryString["RefID"])) ? Request.QueryString["RefID"] : "0";
            Load_ListofValues();
            SaveButtonOnly();
            GetLink("get link");
            if (hdnRefID.Value == "0")
            {
                if (hdnRefID.Value == "0") hdnRefID.Value = TIPIS3WebAdmin.TIPDataStandard.Generation.GetReferenceNumber();
                txtIssuedBy.Text = hdnUserEmpName.Value;
                txtIssuedDate.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                Set_Control_Visibility();
                divLogs.Visible = false;
            }
            else
            {
                divLogs.Visible = true;
                Load_Attachment();
                Load_Information("per refid", hdnRefID.Value);
                if (hdnReqStat.Value.ToLower() == "for mpd remarks")
                {
                    LoadRemarks_SQA("get sqa remarks", hdnRefID.Value);
                }
                else if (hdnReqStat.Value.ToLower() == "for final disposition")
                {
                    LoadRemarks_SQA("get sqa remarks", hdnRefID.Value);
                    if (ddlDisposition_SQA.SelectedValue.ToLower() == "reject")
                    {
                        LoadRemarks_MPD("get mpd remarks", hdnRefID.Value);
                    }
                }
                else if (hdnReqStat.Value.ToLower() == "closed")
                {
                    LoadRemarks_SQA("get sqa remarks", hdnRefID.Value);
                    LoadFinalDisposition("get final disposition", hdnRefID.Value);
                    if (ddlDisposition_SQA.SelectedValue.ToLower() == "reject")
                    {
                        LoadRemarks_MPD("get mpd remarks", hdnRefID.Value);
                    }
                }
                //Show edit button if ADMIN
                if (isAdmin || isLeader)
                {
                    if (txtStatus.Text.ToLower() == "open")
                    {
                        lbtnEdit.Visible = true;
                        lbtnUpdate_App.Visible = false;
                    }
                    else if (txtStatus.Text.ToLower() == "closed")
                    {
                        lbtnEdit.Visible = false;
                    }
                }

                //Show Re-assign button if ADMIN
                if (isAdmin || isLeader) 
                {
                    if (hdnReqStat.Value.ToLower() == "for submission" || hdnReqStat.Value.ToLower() == "awaiting approval" || hdnReqStat.Value.ToLower() == "for sqa remarks" || hdnReqStat.Value.ToLower() == "for mpd remarks")

                    {
                        lbtnReassign.Visible = true;
                        lbtnUpdate_App.Visible = false;
                    }
                    //else if (hdnReqStat.Value.ToLower() == "awaiting approval" && isAdmin)
                    //{
                    //    lbtnReassign.Visible = true;
                    //    lbtnUpdate_App.Visible = false;
                    //}

                }


                SetButtonsVisibility();
                Load_DR_HistoryLogs(hdnRefID.Value);
            }

        }
        else
        {
            if (gvAttachment.Rows.Count > 0)
            {
                gvAttachment.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            
            if (gvHistoryLogs.Rows.Count > 0)
            {
                gvHistoryLogs.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }


    }
    #region Fill all dropdownlist thru maintenance
    //Fill all dropdownlist
    private void Load_ListofValues()
    {
        // Affected BU
        cl_Common.fill_DropDownList(ddlAffecBU, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get affectedBU ", "", "", ""), "ItemCode", "Description", "-- Please Select --");
        //Forwader
        cl_Common.fill_DropDownList(ddlForwarder, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get forwarder ", "", "", ""), "ItemCode", "Description", "-- Please Select --");
        // Supplier
        cl_Common.fill_DropDownList(ddlSupplier, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get supplier", "", "", ""), "ItemCode", "Description", "-- Please Select --");
        // Damage Noted At
        cl_Common.fill_DropDownList(ddlDamageNoted, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get damage noted", "", "", ""), "ItemCode", "Description", "-- Please Select --");
        // Approver
        cl_Common.getApprovers(hdnUserDepCode.Value, ddlApprover, "LastEmpName", "EmpNo");
        // SQA remarks
        cl_Common.fill_DropDownList(ddlDisposition_SQA, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get sqa remarks", "", "", ""), "ItemCode", "Description", "-- Please Select --");
        // MPD remarks
        cl_Common.fill_DropDownList(ddlDisposition_MPD, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get mpd remarks", "", "", ""), "ItemCode", "Description", "-- Please Select --");
        // Final disposition
        cl_Common.fill_DropDownList(ddlFinalDisposition, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get final disposition", "", "", ""), "ItemCode", "Description", "-- Please Select --");

    }
    #endregion

    protected override void OnInit(EventArgs e)
    {
        //find the button control within the user control
        LinkButton btnAccept = (LinkButton)AlertMessage.FindControl("lbtnAccept");
        LinkButton btnDecline = (LinkButton)AlertMessage.FindControl("lbtnDecline");
        btnAccept.Click += new EventHandler(btnAccept_Click);
        btnDecline.Click += new EventHandler(btnDecline_Click);
        base.OnInit(e);
    }
    void btnAccept_Click(object sender, EventArgs e)
    {
        TextBox txtRemarks_Save = (TextBox)AlertMessage.FindControl("txtRemarks");
        switch (hdnAction.Value.ToLower())
        {
            case "refresh":
                Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
                hdnAction.Value = string.Empty;
                break;
            case "redirect":
                Response.Redirect("Registration.aspx");
                break;
            case "alert":
                hdnAction.Value = string.Empty;
                break;
            case "update damage report":
                if (isAdmin)
                {
                    if (string.IsNullOrEmpty(txtRemarks_Save.Text))
                    {
                        AlertMessage.Show("warning", "Warning:", "Please provide remarks.");
                        hdnAction.Value = "alert";
                    }
                    else
                    {
                        Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                    }
                }
                else 
                {
                    Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                }
                break;
            case "update by admin":
                if (isAdmin)
                {
                    if (string.IsNullOrEmpty(txtRemarks_Save.Text))
                    {
                        AlertMessage.Show("warning", "Warning:", "Please provide remarks.");
                        hdnAction.Value = "alert";
                    }
                    else
                    {
                        Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                    }
                }
                else
                {
                    Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                }
                break;
            case "reassign sqa":
                if (isAdmin)
                {
                    if (string.IsNullOrEmpty(txtRemarks_Save.Text))
                    {
                        AlertMessage.Show("warning", "Warning:", "Please provide remarks.");
                        hdnAction.Value = "alert";
                    }
                    else
                    {
                        Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                    }
                }
                else
                {
                    Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                }
                break;
            case "reassign mpd":
                if (isAdmin)
                {
                    if (string.IsNullOrEmpty(txtRemarks_Save.Text))
                    {
                        AlertMessage.Show("warning", "Warning:", "Please provide remarks.");
                        hdnAction.Value = "alert";
                    }
                    else
                    {
                        Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                    }
                }
                else
                {
                    Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                }
                break;
            case "reassign":
                if (isAdmin)
                {
                    if (string.IsNullOrEmpty(txtRemarks_Save.Text))
                    {
                        AlertMessage.Show("warning", "Warning:", "Please provide remarks.");
                        hdnAction.Value = "alert";
                    }
                    else
                    {
                        Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                    }
                }
                else
                {
                    Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                }
                break;
            case "submit damage report":
                Update_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "select approver":
                ddlApprover.SelectedIndex = 0;
                hdnAction.Value = string.Empty;
                break;
            case "delete attachment":
                Delete_Attachment(txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "approve damage report":
                Approve_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "deny damage report":
                if (string.IsNullOrEmpty(txtRemarks_Save.Text))
                {
                    AlertMessage.Show("warning", "Warning:", "Please provide remarks.");
                    hdnAction.Value = "alert";
                }
                else Approve_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "submit sqa remarks":
                Submit_Remarks_SQA(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "submit mpd remarks":
                Submit_Remarks_MPD(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            case "submit final disposition":
                Submit_Remarks_Final(hdnAction.Value.ToLower(), hdnUserEmpName.Value.Trim(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
            default:
                Save_Request(hdnAction.Value.ToLower(), txtRemarks_Save.Text.Trim().Replace("'", "`"));
                break;
        }

    }
    private void Save_Request(string TransType, string txtRemarks_Save)
    {
        string rDate = txtReceiveDate.Text;
        DateTime receiveDate = DateTime.ParseExact(rDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        dmg.RefID = hdnRefID.Value;
        dmg.IssuedBy = hdnUserEmpName.Value;
        dmg.Local = txtLocal.Text;
        dmg.AffectedBU = ddlAffecBU.SelectedValue.ToString();
        dmg.NatureOfDiscrepancy = txtView.Text;
        dmg.DamageNote = ddlDamageNoted.SelectedValue.ToString();
        dmg.InvoiceNo = txtInvoiceNo.Text;
        dmg.ItemCode = txtItemCode.Text;
        dmg.Supplier = ddlSupplier.SelectedValue.ToString();
        dmg.Description = txtDescription.Text;
        dmg.Forwarder = ddlForwarder.SelectedValue.ToString();
        dmg.InvoiceQty = txtInvoiceQty.Text;
        dmg.BlAwhNo = txtBANo.Text;
        dmg.QtyAffected = txtQtyAffected.Text;
        dmg.ReceivedDate = receiveDate;
        dmg.NoOfBoxAffected = txtNoOfBox.Text;
        dmg.Remarks = txtRemarks.Text;
        dmg.SendToQA = txtSendToQA.Text;
        dmg.SendToMPD = txtSendToMPD.Text;
        dmg.CopyTo = txtCopyTo.Text;
        dmg.ApprovedBy = ddlApprover.SelectedValue.ToString();
        dmg.ApproverName = ddlApprover.SelectedItem.ToString();
        dmg.Attributes1 = txtOthers.Text;
        dmg.Attributes2 = "";
        dmg.Attributes3 = txtRemarks_Save;
        dmg.EmpNoQA = hdnEmpNoQA.Value;
        dmg.EmpNameQA = hdnEmpNameQA.Value;
        dmg.DeptQA = hdnDeptQA.Value;
        dmg.EmpNoMPD = hdnEmpNoMPD.Value;
        dmg.EmpNameMPD = hdnEmpNameMPD.Value;
        dmg.DeptMPD = hdnDeptMPD.Value;
        dmg.EmpNoCopy = hdnEmpNoCopy.Value;
        dmg.EmpNameCopy = hdnEmpNameCopy.Value;
        dmg.DeptCopy = hdnDeptCopy.Value;


        Save_Transaction(TransType);
    }

    private void Save_Transaction(string TransType)
    {
        string result = dl.Insert_DR_DMGTransaction(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dmg, TransType);
        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {

                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
            else
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "alert";
            }
            uPnlApprover.Update();
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Please check input details then try again!");
            hdnAction.Value = "alert";
        }
    }

    private void Update_Request(string TransType, string txtRemarks_Save)
    {
        string rDate = txtReceiveDate.Text;
        DateTime receiveDate = DateTime.ParseExact(rDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);

        dmg.RefID = hdnRefID.Value;
        dmg.IssuedBy = txtIssuedBy.Text;
        dmg.Local = txtLocal.Text;
        dmg.AffectedBU = ddlAffecBU.SelectedValue.ToString();
        dmg.NatureOfDiscrepancy = txtView.Text;
        dmg.DamageNote = ddlDamageNoted.SelectedValue.ToString();
        dmg.InvoiceNo = txtInvoiceNo.Text;
        dmg.ItemCode = txtItemCode.Text;
        dmg.Supplier = ddlSupplier.SelectedValue.ToString();
        dmg.Description = txtDescription.Text;
        dmg.Forwarder = ddlForwarder.SelectedValue.ToString();
        dmg.InvoiceQty = txtInvoiceQty.Text;
        dmg.BlAwhNo = txtBANo.Text;
        dmg.QtyAffected = txtQtyAffected.Text;
        dmg.ReceivedDate = receiveDate;
        dmg.NoOfBoxAffected = txtNoOfBox.Text;
        dmg.Remarks = txtRemarks.Text;
        dmg.SendToQA = txtSendToQA.Text;
        dmg.SendToMPD = txtSendToMPD.Text;
        dmg.CopyTo = txtCopyTo.Text;
        dmg.ApprovedBy = ddlApprover.SelectedValue.ToString();
        dmg.ApproverName = ddlApprover.SelectedItem.ToString();
        dmg.Attributes1 = txtOthers.Text;
        dmg.Attributes2 = "";
        dmg.Attributes3 = txtRemarks_Save;
        dmg.EmpNoQA = hdnEmpNoQA.Value;
        dmg.EmpNameQA = hdnEmpNameQA.Value;
        dmg.DeptQA = hdnDeptQA.Value;
        dmg.EmpNoMPD = hdnEmpNoMPD.Value;
        dmg.EmpNameMPD = hdnEmpNameMPD.Value;
        dmg.DeptMPD = hdnDeptMPD.Value;
        dmg.EmpNoCopy = hdnEmpNoCopy.Value;
        dmg.EmpNameCopy = hdnEmpNameCopy.Value;
        dmg.DeptCopy = hdnDeptCopy.Value;
        dmg.UserName = hdnUserEmpName.Value;
        dmg.isAdmin = isAdmin;
        dmg.isLeader = isLeader;


        Update_Transaction(TransType);
    }
    private void Update_Transaction(string TransType)
    {
        string result = dl.Update_DR_DMGTransaction(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dmg, TransType);
        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {

                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
            else
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "alert";
            }
            uPnlApprover.Update();
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Please check input details then try again!");
            hdnAction.Value = "alert";
        }
    }
    private void Delete_Attachment(string Remarks)
    {
        try
        {
            dto.ID = hdnAttachmentID.Value;
            dto.RefID = hdnRefID.Value;
            dto.FileName_Orig = "";
            dto.FileName_New = "";
            dto.FilePath = hdnAttachmentPath.Value;
            dto.ActionRemarks = Remarks;
            dto.CreatedBy_EmpNo = hdnUserEmpNo.Value;
            dto.CreatedBy_EmpName = hdnUserEmpName.Value;
            dto.Attribute1 = "-";
            dto.Attribute2 = "-";

            string result = dl.Insert_DR_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dto, "delete");
            if (result.ToLower() == "request timeout")
            {
                AlertMessage.Show("Warning", "Information:", "Request Timeout: Please try again!");
                hdnAction.Value = "alert";
            }
            else
            {
                string filePath = hdnAttachmentPath.Value;
                File.Delete(filePath);
                AlertMessage.Show("Warning", "Information:", "Attachment has been deleted!");
                hdnAction.Value = "alert";
            }
            Load_Attachment();
            //Load_Project_HistoryLogs(hdnRefID.Value);
        }
        catch (Exception ex)
        {
            //
        }
    }

    private void Approve_Request(string TransType, string Remarks)
    {
        string result = dl.Approve_DR_Request(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), TransType, hdnRefID.Value, Remarks, hdnUserEmpName.Value);
        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
            else
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "alert";
            }
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Please check input details then try again!");
            hdnAction.Value = "alert";
        }

    }

    private void Submit_Remarks_SQA(string TransType, string txtRemarks_Save)
    {
        bu.Disposition = ddlDisposition_SQA.SelectedValue.ToString();
        bu.Comment = txtComments_SQA.Text;

        Submit_BU_Remarks(TransType, txtRemarks_Save);
    }
    private void Submit_Remarks_MPD(string TransType, string txtRemarks_Save)
    {
        bu.Disposition = ddlDisposition_MPD.SelectedValue.ToString();
        bu.Comment = txtComments_MPD.Text;

        Submit_BU_Remarks(TransType, txtRemarks_Save);
    }
    private void Submit_Remarks_Final(string TransType, string userEmpName, string txtRemarks_Save)
    {
        fnl.Disposition = ddlFinalDisposition.SelectedValue.ToString();
        fnl.Comment = txtFinalComments.Text;

        Submit_FinalDisposition(TransType, userEmpName, txtRemarks_Save);
    }
    private void Submit_BU_Remarks(string TransType, string Remarks)
    {
        string result = dl.Submit_BU_Remarks(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), bu, TransType, hdnRefID.Value, Remarks);
        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
            else
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "alert";
            }
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Please check input details then try again!");
            hdnAction.Value = "alert";
        }

    }
    private void Submit_FinalDisposition(string TransType, string userEmpName, string Remarks)
    {
        string result = dl.Submit_FinalDisposition(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), fnl, TransType, hdnRefID.Value, userEmpName, Remarks);
        if (result.ToLower() != "request timeout")
        {
            if (result.ToLower().Contains("completed"))
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "refresh";
            }
            else
            {
                AlertMessage.Show("Information", "Information:", result);
                hdnAction.Value = "alert";
            }
        }
        else
        {
            AlertMessage.Show("Warning", "Information:", "Transaction Failed: Please check input details then try again!");
            hdnAction.Value = "alert";
        }

    }
    void btnDecline_Click(object sender, EventArgs e)
    {
        hdnAction.Value = "";
        upnlActionButton.Update();
    }


    #region Methods

    //Get Link from table DR Link
    private void GetLink(string transType)
    {
        DataTable dtLink = new DataTable();
        dtLink = dl.get_DR_Link(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType);
        if (dtLink != null && dtLink.Rows.Count > 0)
        {
            hdnLink.Value = !string.IsNullOrEmpty(dtLink.Rows[0]["Link"].ToString()) ? dtLink.Rows[0]["Link"].ToString() : "";
        }
    }
    //Save buttons
    private void ButtonsAfterSave()
    {
        lbtnApprove_App.Visible = false;
        lbtnDeny_App.Visible = false;
        lbtnSubmit_App.Visible = true;
        lbtnSave_App.Visible = false;
        if (isAdmin) 
        {
            lbtnUpdate_App.Visible = false;
        }
        else if (!isAdmin)
        {
            lbtnUpdate_App.Visible = true;
        }
    }

    private void AdminAndApproverButton()
    {
        lbtnApprove_App.Visible = true;
        lbtnDeny_App.Visible = true;
        lbtnUpdate_App.Visible = false;
        lbtnSubmit_App.Visible = false;
        lbtnSave_App.Visible = false;
    }
    private void RequestorButton()
    {
        lbtnApprove_App.Visible = false;
        lbtnDeny_App.Visible = false;
        lbtnUpdate_App.Visible = false;
        lbtnSubmit_App.Visible = false;
        lbtnSave_App.Visible = false;
    }

    //Submit buttons
    private void SetControls()
    {
        if (isAdmin)
        {
            if (hdnReqStat.Value.ToLower() == "for submission")
            {
                lblReqStat.Text = hdnReqStat.Value;
                lblReqStat.ForeColor = System.Drawing.Color.DarkOrange;
                lblReqStat.Visible = true;

                txtApprover.Visible = true;
                ddlApprover.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "awaiting approval")
            {
                lblReqStat.Text = hdnReqStat.Value;
                lblReqStat.ForeColor = System.Drawing.Color.DarkOrange;
                lblReqStat.Visible = true;

                txtApprover.Visible = true;
                ddlApprover.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "denied")
            {
                lblReqStat.Text = "Date Denied: " + hdnDateDenied.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Red;
                lblReqStat.Visible = true;
            }
            else if (hdnReqStat.Value.ToLower() == "for sqa remarks")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;
            }
            else if (hdnReqStat.Value.ToLower() == "for mpd remarks")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;

                ddlDisposition_SQA.Enabled = false;
                txtComments_SQA.Enabled = false;
                txtBURemarksDate_SQA.Enabled = false;
                lbtnSubmit_SQA.Visible = false;

                lblMPD.Visible = true;
                lblDispositionMPD.Visible = true;
                ddlDisposition_MPD.Visible = true;
                lblDate_MPD.Visible = true;
                txtBURemarksDate_MPD.Visible = true;
                lblComments_MPD.Visible = true;
                txtComments_MPD.Visible = true;
                lbtnSubmit_MPD.Visible = true;
            }

            else if (hdnReqStat.Value.ToLower() == "for final disposition")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;
                if (txtComments_MPD.Text == "")
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    txtBURemarksDate_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = false;
                    lblDispositionMPD.Visible = false;
                    ddlDisposition_MPD.Visible = false;
                    lblDate_MPD.Visible = false;
                    txtBURemarksDate_MPD.Visible = false;
                    lblComments_MPD.Visible = false;
                    txtComments_MPD.Visible = false;
                    lbtnSubmit_MPD.Visible = false;
                }
                else 
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    txtBURemarksDate_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = true;
                    lblDispositionMPD.Visible = true;
                    ddlDisposition_MPD.Visible = true;
                    lblDate_MPD.Visible = true;
                    txtBURemarksDate_MPD.Visible = true;
                    lblComments_MPD.Visible = true;
                    txtComments_MPD.Visible = true;
                    lbtnSubmit_MPD.Visible = true;

                    ddlDisposition_MPD.Enabled = false;
                    txtComments_MPD.Enabled = false;
                    txtBURemarksDate_MPD.Enabled = false;
                    lbtnSubmit_MPD.Visible = false;
                }

            }
            else if (hdnReqStat.Value.ToLower() == "closed")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;
                if (txtComments_MPD.Text == "")
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = false;
                    lblDispositionMPD.Visible = false;
                    ddlDisposition_MPD.Visible = false;
                    lblDate_MPD.Visible = false;
                    txtBURemarksDate_MPD.Visible = false;
                    lblComments_MPD.Visible = false;
                    txtComments_MPD.Visible = false;
                    lbtnSubmit_MPD.Visible = false;

                    //Final Disposition
                    ddlFinalDisposition.Enabled = false;
                    txtFinalComments.Enabled = false;
                    lbtnSubmit_Final.Visible = false;
                }
                else
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    txtBURemarksDate_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = true;
                    lblDispositionMPD.Visible = true;
                    ddlDisposition_MPD.Visible = true;
                    lblDate_MPD.Visible = true;
                    txtBURemarksDate_MPD.Visible = true;
                    lblComments_MPD.Visible = true;
                    txtComments_MPD.Visible = true;
                    lbtnSubmit_MPD.Visible = true;

                    ddlDisposition_MPD.Enabled = false;
                    txtComments_MPD.Enabled = false;
                    txtBURemarksDate_MPD.Enabled = false;
                    lbtnSubmit_MPD.Visible = false;

                    //Final Disposition
                    ddlFinalDisposition.Enabled = false;
                    txtFinalComments.Enabled = false;
                    txtFinalDate.Enabled = false;
                    lbtnSubmit_Final.Visible = false;
                }

            }

            RequestorButton();

            lBtnAdd_sendToQA.Visible = false;
            lBtnRemove_sendToQA.Visible = false;
            lBtnAdd_sendToMPD.Visible = false;
            lBtnRemove_sendToMPD.Visible = false;
            lBtnAdd_copyTo.Visible = false;
            lBtnRemove_copyTo.Visible = false;

            //Attachment 
            divUploadAttach.Visible = false;
            LinkButton lbtnDelete = new LinkButton();
            foreach (GridViewRow row in gvAttachment.Rows)
            {
                lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
                lbtnDelete.Visible = false;
            }

            //Text fields
            ddlAffecBU.Enabled = false;
            txtLocal.Enabled = false;
            rBtnNature.Enabled = false;
            txtOthers.Enabled = false;
            ddlDamageNoted.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            ddlSupplier.Enabled = false;
            txtDescription.Enabled = false;
            ddlForwarder.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtBANo.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtNoOfBox.Enabled = false;
            txtRemarks.Enabled = false;
            //ddlApprover.Enabled = false;
            ddlApprover.Visible = false;
            txtApprover.Visible = true;

            Set_Control_Visibility();
        }
        else if (isLeader)
        {
            if (hdnReqStat.Value.ToLower() == "for submission")
            {
                lblReqStat.Text = hdnReqStat.Value;
                lblReqStat.ForeColor = System.Drawing.Color.DarkOrange;
                lblReqStat.Visible = true;

                txtApprover.Visible = true;
                ddlApprover.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "awaiting approval")
            {
                lblReqStat.Text = hdnReqStat.Value;
                lblReqStat.ForeColor = System.Drawing.Color.DarkOrange;
                lblReqStat.Visible = true;

                txtApprover.Visible = true;
                ddlApprover.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "denied")
            {
                lblReqStat.Text = "Date Denied: " + hdnDateDenied.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Red;
                lblReqStat.Visible = true;
            }
            else if (hdnReqStat.Value.ToLower() == "for sqa remarks")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;

                ddlDisposition_SQA.Enabled = false;
                txtComments_SQA.Enabled = false;
                txtBURemarksDate_SQA.Enabled = false;
                lbtnSubmit_SQA.Visible = false;

            }
            else if (hdnReqStat.Value.ToLower() == "for mpd remarks")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;

                ddlDisposition_SQA.Enabled = false;
                txtComments_SQA.Enabled = false;
                txtBURemarksDate_SQA.Enabled = false;
                lbtnSubmit_SQA.Visible = false;

                lblMPD.Visible = true;
                lblDispositionMPD.Visible = true;
                ddlDisposition_MPD.Visible = true;
                lblDate_MPD.Visible = true;
                txtBURemarksDate_MPD.Visible = true;
                lblComments_MPD.Visible = true;
                txtComments_MPD.Visible = true;
                lbtnSubmit_MPD.Visible = true;

                ddlDisposition_MPD.Enabled = false;
                txtComments_MPD.Enabled = false;
                txtBURemarksDate_MPD.Enabled = false;
                lbtnSubmit_MPD.Visible = false;
            }

            else if (hdnReqStat.Value.ToLower() == "for final disposition")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;

                //ddlFinalDisposition.Enabled = false;
                //txtFinalComments.Enabled = false;
                //txtFinalDate.Enabled = false;
                //lbtnSubmit_Final.Visible = false;

                if (txtComments_MPD.Text == "")
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    txtBURemarksDate_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = false;
                    lblDispositionMPD.Visible = false;
                    ddlDisposition_MPD.Visible = false;
                    lblDate_MPD.Visible = false;
                    txtBURemarksDate_MPD.Visible = false;
                    lblComments_MPD.Visible = false;
                    txtComments_MPD.Visible = false;
                    lbtnSubmit_MPD.Visible = false;
                }
                else
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    txtBURemarksDate_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = true;
                    lblDispositionMPD.Visible = true;
                    ddlDisposition_MPD.Visible = true;
                    lblDate_MPD.Visible = true;
                    txtBURemarksDate_MPD.Visible = true;
                    lblComments_MPD.Visible = true;
                    txtComments_MPD.Visible = true;
                    lbtnSubmit_MPD.Visible = true;

                    ddlDisposition_MPD.Enabled = false;
                    txtComments_MPD.Enabled = false;
                    txtBURemarksDate_MPD.Enabled = false;
                    lbtnSubmit_MPD.Visible = false;
                }

            }
            else if (hdnReqStat.Value.ToLower() == "closed")
            {
                lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                lblReqStat.ForeColor = System.Drawing.Color.Green;
                lblReqStat.Visible = true;
                if (txtComments_MPD.Text == "")
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = false;
                    lblDispositionMPD.Visible = false;
                    ddlDisposition_MPD.Visible = false;
                    lblDate_MPD.Visible = false;
                    txtBURemarksDate_MPD.Visible = false;
                    lblComments_MPD.Visible = false;
                    txtComments_MPD.Visible = false;
                    lbtnSubmit_MPD.Visible = false;

                    //Final Disposition
                    ddlFinalDisposition.Enabled = false;
                    txtFinalComments.Enabled = false;
                    lbtnSubmit_Final.Visible = false;
                }
                else
                {
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    txtBURemarksDate_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    //MPD
                    lblMPD.Visible = true;
                    lblDispositionMPD.Visible = true;
                    ddlDisposition_MPD.Visible = true;
                    lblDate_MPD.Visible = true;
                    txtBURemarksDate_MPD.Visible = true;
                    lblComments_MPD.Visible = true;
                    txtComments_MPD.Visible = true;
                    lbtnSubmit_MPD.Visible = true;

                    ddlDisposition_MPD.Enabled = false;
                    txtComments_MPD.Enabled = false;
                    txtBURemarksDate_MPD.Enabled = false;
                    lbtnSubmit_MPD.Visible = false;

                    //Final Disposition
                    ddlFinalDisposition.Enabled = false;
                    txtFinalComments.Enabled = false;
                    txtFinalDate.Enabled = false;
                    lbtnSubmit_Final.Visible = false;
                }

            }

            RequestorButton();

            lBtnAdd_sendToQA.Visible = false;
            lBtnRemove_sendToQA.Visible = false;
            lBtnAdd_sendToMPD.Visible = false;
            lBtnRemove_sendToMPD.Visible = false;
            lBtnAdd_copyTo.Visible = false;
            lBtnRemove_copyTo.Visible = false;

            //Attachment 
            divUploadAttach.Visible = false;
            LinkButton lbtnDelete = new LinkButton();
            foreach (GridViewRow row in gvAttachment.Rows)
            {
                lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
                lbtnDelete.Visible = false;
            }

            //Text fields
            ddlAffecBU.Enabled = false;
            txtLocal.Enabled = false;
            rBtnNature.Enabled = false;
            txtOthers.Enabled = false;
            ddlDamageNoted.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            ddlSupplier.Enabled = false;
            txtDescription.Enabled = false;
            ddlForwarder.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtBANo.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtNoOfBox.Enabled = false;
            txtRemarks.Enabled = false;
            //ddlApprover.Enabled = false;
            ddlApprover.Visible = false;
            txtApprover.Visible = true;

            Set_Control_Visibility();
        }
        else
        {
            if (hdnReqStat.Value.ToLower() == "for submission")
            {
                lblReqStat.Text = hdnReqStat.Value;
                lblReqStat.ForeColor = System.Drawing.Color.DarkOrange;
                lblReqStat.Visible = true;

                if (hdnUserEmpName.Value.ToLower() == txtIssuedBy.Text.ToLower())
                {
                    ddlAffecBU.Enabled = true;
                    txtLocal.Enabled = true;
                    rBtnNature.Enabled = true;
                    txtOthers.Enabled = true;
                    ddlDamageNoted.Enabled = true;
                    txtInvoiceNo.Enabled = true;
                    txtItemCode.Enabled = true;
                    txtReceiveDate.Enabled = true;
                    txtDescription.Enabled = true;
                    ddlForwarder.Enabled = true;
                    txtInvoiceQty.Enabled = true;
                    txtBANo.Enabled = true;
                    txtQtyAffected.Enabled = true;
                    ddlSupplier.Enabled = true;
                    txtNoOfBox.Enabled = true;
                    txtRemarks.Enabled = true;
                }
                else if (hdnUserEmpName.Value.ToLower() != txtIssuedBy.Text.ToLower())
                {
                    //txtbox
                    ddlAffecBU.Enabled = false;
                    txtLocal.Enabled = false;
                    rBtnNature.Enabled = false;
                    txtOthers.Enabled = false;
                    ddlDamageNoted.Enabled = false;
                    txtInvoiceNo.Enabled = false;
                    txtItemCode.Enabled = false;
                    txtReceiveDate.Enabled = false;
                    txtDescription.Enabled = false;
                    ddlForwarder.Enabled = false;
                    txtInvoiceQty.Enabled = false;
                    txtBANo.Enabled = false;
                    txtQtyAffected.Enabled = false;
                    ddlSupplier.Enabled = false;
                    txtNoOfBox.Enabled = false;
                    txtRemarks.Enabled = false;
                    ddlApprover.Visible = false;
                    txtApprover.Visible = true;

                    //Buttons
                    lBtnAdd_sendToQA.Visible = false;
                    lBtnRemove_sendToQA.Visible = false;
                    lBtnAdd_sendToMPD.Visible = false;
                    lBtnRemove_sendToMPD.Visible = false;
                    lBtnAdd_copyTo.Visible = false;
                    lBtnRemove_copyTo.Visible = false;

                    //Attachment 
                    divUploadAttach.Visible = false;
                    LinkButton lbtnDelete = new LinkButton();
                    foreach (GridViewRow row in gvAttachment.Rows)
                    {
                        lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
                        lbtnDelete.Visible = false;
                    }
                }



                }
            
            else
            {
                if (hdnReqStat.Value.ToLower() == "awaiting approval")
                {
                    lblReqStat.Text = hdnReqStat.Value;
                    lblReqStat.ForeColor = System.Drawing.Color.DarkOrange;
                    lblReqStat.Visible = true;
                }
                else if (hdnReqStat.Value.ToLower() == "denied")
                {
                    lblReqStat.Text = "Date Denied: " + hdnDateDenied.Value;
                    lblReqStat.ForeColor = System.Drawing.Color.Red;
                    lblReqStat.Visible = true;
                }
                else if (hdnReqStat.Value.ToLower() == "for sqa remarks")
                {
                    lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                    lblReqStat.ForeColor = System.Drawing.Color.Green;
                    lblReqStat.Visible = true;
                }
                else if (hdnReqStat.Value.ToLower() == "for mpd remarks")
                {
                    lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                    lblReqStat.ForeColor = System.Drawing.Color.Green;
                    lblReqStat.Visible = true;
                    if (isSQA || isRequestor || isApprover) 
                    {
                        //SQA
                        ddlDisposition_SQA.Enabled = false;
                        txtComments_SQA.Enabled = false;
                        txtBURemarksDate_SQA.Enabled = false;
                        lbtnSubmit_SQA.Visible = false;
                    }
                    else if (isMPD)
                    {
                        ddlDisposition_SQA.Enabled = false;
                        txtComments_SQA.Enabled = false;
                        txtBURemarksDate_SQA.Enabled = false;
                        lbtnSubmit_SQA.Visible = false;

                        lblMPD.Visible = true;
                        lblDispositionMPD.Visible = true;
                        ddlDisposition_MPD.Visible = true;
                        lblDate_MPD.Visible = true;
                        txtBURemarksDate_MPD.Visible = true;
                        lblComments_MPD.Visible = true;
                        txtComments_MPD.Visible = true;
                        lbtnSubmit_MPD.Visible = true;
                    }
                    
                }
                else if (hdnReqStat.Value.ToLower() == "for final disposition")
                {
                    lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                    lblReqStat.ForeColor = System.Drawing.Color.Green;
                    lblReqStat.Visible = true;
                    //SQA
                    ddlDisposition_SQA.Enabled = false;
                    txtComments_SQA.Enabled = false;
                    txtBURemarksDate_SQA.Enabled = false;
                    lbtnSubmit_SQA.Visible = false;

                    if (isMPD || isSQA || isApprover || isRequestor)
                    {
                        if (txtComments_MPD.Text == "")
                        {
                            //SQA
                            ddlDisposition_SQA.Enabled = false;
                            txtComments_SQA.Enabled = false;
                            txtBURemarksDate_SQA.Enabled = false;
                            lbtnSubmit_SQA.Visible = false;

                            //MPD
                            lblMPD.Visible = false;
                            lblDispositionMPD.Visible = false;
                            ddlDisposition_MPD.Visible = false;
                            lblDate_MPD.Visible = false;
                            txtBURemarksDate_MPD.Visible = false;
                            lblComments_MPD.Visible = false;
                            txtComments_MPD.Visible = false;
                            lbtnSubmit_MPD.Visible = false;
                        }
                        else
                        {
                            //SQA
                            ddlDisposition_SQA.Enabled = false;
                            txtComments_SQA.Enabled = false;
                            txtBURemarksDate_SQA.Enabled = false;
                            lbtnSubmit_SQA.Visible = false;

                            //MPD
                            lblMPD.Visible = true;
                            lblDispositionMPD.Visible = true;
                            ddlDisposition_MPD.Visible = true;
                            lblDate_MPD.Visible = true;
                            txtBURemarksDate_MPD.Visible = true;
                            lblComments_MPD.Visible = true;
                            txtComments_MPD.Visible = true;
                            lbtnSubmit_MPD.Visible = true;

                            ddlDisposition_MPD.Enabled = false;
                            txtComments_MPD.Enabled = false;
                            txtBURemarksDate_MPD.Enabled = false;
                            lbtnSubmit_MPD.Visible = false;
                        }
                    }


                }
                else if (hdnReqStat.Value.ToLower() == "closed")
                {
                    lblReqStat.Text = "Date Approved: " + hdnDateApproved.Value;
                    lblReqStat.ForeColor = System.Drawing.Color.Green;
                    lblReqStat.Visible = true;
                    if (txtComments_MPD.Text == "")
                    {
                        //SQA
                        ddlDisposition_SQA.Enabled = false;
                        txtComments_SQA.Enabled = false;
                        lbtnSubmit_SQA.Visible = false;

                        //MPD
                        lblMPD.Visible = false;
                        lblDispositionMPD.Visible = false;
                        ddlDisposition_MPD.Visible = false;
                        lblDate_MPD.Visible = false;
                        txtBURemarksDate_MPD.Visible = false;
                        lblComments_MPD.Visible = false;
                        txtComments_MPD.Visible = false;
                        lbtnSubmit_MPD.Visible = false;

                        //Final Disposition
                        ddlFinalDisposition.Enabled = false;
                        txtFinalComments.Enabled = false;
                        lbtnSubmit_Final.Visible = false;
                    }
                    else
                    {
                        //SQA
                        ddlDisposition_SQA.Enabled = false;
                        txtComments_SQA.Enabled = false;
                        txtBURemarksDate_SQA.Enabled = false;
                        lbtnSubmit_SQA.Visible = false;

                        //MPD
                        lblMPD.Visible = true;
                        lblDispositionMPD.Visible = true;
                        ddlDisposition_MPD.Visible = true;
                        lblDate_MPD.Visible = true;
                        txtBURemarksDate_MPD.Visible = true;
                        lblComments_MPD.Visible = true;
                        txtComments_MPD.Visible = true;
                        lbtnSubmit_MPD.Visible = true;

                        ddlDisposition_MPD.Enabled = false;
                        txtComments_MPD.Enabled = false;
                        txtBURemarksDate_MPD.Enabled = false;
                        lbtnSubmit_MPD.Visible = false;

                        //Final Disposition
                        ddlFinalDisposition.Enabled = false;
                        txtFinalComments.Enabled = false;
                        txtFinalDate.Enabled = false;
                        lbtnSubmit_Final.Visible = false;
                    }

                }

                RequestorButton();

                //Concern BU Buttons
                lBtnAdd_sendToQA.Visible = false;
                lBtnRemove_sendToQA.Visible = false;
                lBtnAdd_sendToMPD.Visible = false;
                lBtnRemove_sendToMPD.Visible = false;
                lBtnAdd_copyTo.Visible = false;
                lBtnRemove_copyTo.Visible = false;

                //Attachment 
                divUploadAttach.Visible = false;
                LinkButton lbtnDelete = new LinkButton();
                foreach (GridViewRow row in gvAttachment.Rows)
                {
                    lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
                    lbtnDelete.Visible = false;
                }

                //Text fields
                ddlAffecBU.Enabled = false;
                txtLocal.Enabled = false;
                rBtnNature.Enabled = false;
                txtOthers.Enabled = false;
                ddlDamageNoted.Enabled = false;
                txtInvoiceNo.Enabled = false;
                txtItemCode.Enabled = false;
                ddlSupplier.Enabled = false;
                txtDescription.Enabled = false;
                ddlForwarder.Enabled = false;
                txtInvoiceQty.Enabled = false;
                txtBANo.Enabled = false;
                txtQtyAffected.Enabled = false;
                txtReceiveDate.Enabled = false;
                txtNoOfBox.Enabled = false;
                txtRemarks.Enabled = false;
                //ddlApprover.Enabled = false;
                ddlApprover.Visible = false;
                txtApprover.Visible = true;
            }
        }
    }

    //Open status textbox
    private void OpenStatus()
    {
        txtStatus.BackColor = System.Drawing.Color.Green;
        txtStatus.BorderColor = System.Drawing.Color.Green;
        txtStatus.ForeColor = System.Drawing.Color.White;
    }
    //close status textbox
    private void CloseStatus()
    {
        txtStatus.BackColor = System.Drawing.Color.Red;
        txtStatus.BorderColor = System.Drawing.Color.Red;
        txtStatus.ForeColor = System.Drawing.Color.White;
    }
    //DR No. textbox
    private void DrNoStyle()
    {
        txtDDRNo.ReadOnly = true;
        txtDDRNo.BackColor = System.Drawing.Color.Green;
        txtDDRNo.BorderColor = System.Drawing.Color.Green;
        txtDDRNo.ForeColor = System.Drawing.Color.White;
    }
    //Buttons for requestor
    private void SaveButtonOnly()
    {
        lbtnApprove_App.Visible = false;
        lbtnDeny_App.Visible = false;
        lbtnUpdate_App.Visible = false;
        lbtnSubmit_App.Visible = false;
        lbtnSave_App.Visible = true;
    }
    private void Load_Information(string transType, string keyword)
    {
        DataTable dtDMGTransaction = new DataTable();
        dtDMGTransaction = dl.get_DR_DMGTransaction(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, keyword);
        if (dtDMGTransaction != null && dtDMGTransaction.Rows.Count > 0)
        {
            txtDDRNo.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["DrNo"].ToString()) ? dtDMGTransaction.Rows[0]["DrNo"].ToString() : "";
            DrNoStyle();
            txtIssuedBy.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["IssuedBy"].ToString()) ? dtDMGTransaction.Rows[0]["IssuedBy"].ToString() : "";
            txtIssuedDate.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["IssuedDate"].ToString()) ? dtDMGTransaction.Rows[0]["IssuedDate"].ToString() : "";
            ddlAffecBU.SelectedValue = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["AffectedBU"].ToString()) ? dtDMGTransaction.Rows[0]["AffectedBU"].ToString() : "";
            txtLocal.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Local"].ToString()) ? dtDMGTransaction.Rows[0]["Local"].ToString() : "";
            ddlDamageNoted.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["DamageNote"].ToString()) ? dtDMGTransaction.Rows[0]["DamageNote"].ToString() : "";
            txtInvoiceNo.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["InvoiceNo"].ToString()) ? dtDMGTransaction.Rows[0]["InvoiceNo"].ToString() : "";
            txtItemCode.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["ItemCode"].ToString()) ? dtDMGTransaction.Rows[0]["ItemCode"].ToString() : "";
            ddlSupplier.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Supplier"].ToString()) ? dtDMGTransaction.Rows[0]["Supplier"].ToString() : "";
            txtDescription.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Description"].ToString()) ? dtDMGTransaction.Rows[0]["Description"].ToString() : "";
            ddlForwarder.SelectedValue = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Forwader"].ToString()) ? dtDMGTransaction.Rows[0]["Forwader"].ToString() : "";
            txtInvoiceQty.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["InvoiceQty"].ToString()) ? dtDMGTransaction.Rows[0]["InvoiceQty"].ToString() : "";
            txtBANo.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["BlAwhNo"].ToString()) ? dtDMGTransaction.Rows[0]["BlAwhNo"].ToString() : "";
            txtQtyAffected.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["QtyAffected"].ToString()) ? dtDMGTransaction.Rows[0]["QtyAffected"].ToString() : "";
            txtReceiveDate.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["ReceiveDate"].ToString()) ? dtDMGTransaction.Rows[0]["ReceiveDate"].ToString() : "";
            txtNoOfBox.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["NoOfBoxAffected"].ToString()) ? dtDMGTransaction.Rows[0]["NoOfBoxAffected"].ToString() : "";
            txtRemarks.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Remarks"].ToString()) ? dtDMGTransaction.Rows[0]["Remarks"].ToString() : "";
            txtSendToQA.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["SendToQA"].ToString()) ? dtDMGTransaction.Rows[0]["SendToQA"].ToString() : "";
            txtSendToMPD.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["SendToMPD"].ToString()) ? dtDMGTransaction.Rows[0]["SendToMPD"].ToString() : "";
            txtCopyTo.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["CopyTo"].ToString()) ? dtDMGTransaction.Rows[0]["CopyTo"].ToString() : "";
            ddlApprover.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["AppEmpno"].ToString()) ? dtDMGTransaction.Rows[0]["AppEmpno"].ToString() : "";
            txtApprover.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Approver"].ToString()) ? dtDMGTransaction.Rows[0]["Approver"].ToString() : "";
            txtView.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["NatureOfDiscrepancy"].ToString()) ? dtDMGTransaction.Rows[0]["NatureOfDiscrepancy"].ToString() : "";

            txtStatus.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Status"].ToString()) ? dtDMGTransaction.Rows[0]["Status"].ToString() : "";
            if (txtStatus.Text.ToLower() == "open")
            {
                OpenStatus();
            }
            else if (txtStatus.Text.ToLower() == "closed")
            {
                CloseStatus();
            }
            hdnDateApproved.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["ApprovedDate"].ToString()) ? dtDMGTransaction.Rows[0]["ApprovedDate"].ToString() : "";
            hdnDateDenied.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["DeniedDate"].ToString()) ? dtDMGTransaction.Rows[0]["DeniedDate"].ToString() : "";
            hdnReqStat.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["RequestStatus"].ToString()) ? dtDMGTransaction.Rows[0]["RequestStatus"].ToString() : "";
            hdnEmpNoQA.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["QAEmpNo"].ToString()) ? dtDMGTransaction.Rows[0]["QAEmpNo"].ToString() : "";
            hdnEmpNameQA.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["QAEmpName"].ToString()) ? dtDMGTransaction.Rows[0]["QAEmpName"].ToString() : "";
            hdnDeptQA.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["QAEmpDept"].ToString()) ? dtDMGTransaction.Rows[0]["QAEmpDept"].ToString() : "";
            hdnEmpNoMPD.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["MPDEmpNo"].ToString()) ? dtDMGTransaction.Rows[0]["MPDEmpNo"].ToString() : "";
            hdnEmpNameMPD.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["MPDEmpName"].ToString()) ? dtDMGTransaction.Rows[0]["MPDEmpName"].ToString() : "";
            hdnDeptMPD.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["MPDEmpDept"].ToString()) ? dtDMGTransaction.Rows[0]["MPDEmpDept"].ToString() : "";
            hdnEmpNoCopy.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["CopyEmpNo"].ToString()) ? dtDMGTransaction.Rows[0]["CopyEmpNo"].ToString() : "";
            hdnEmpNameCopy.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["CopyEmpName"].ToString()) ? dtDMGTransaction.Rows[0]["CopyEmpName"].ToString() : "";
            hdnDeptCopy.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["CopyEmpDept"].ToString()) ? dtDMGTransaction.Rows[0]["CopyEmpDept"].ToString() : "";

            hdnSQAEmail.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["SendToQA"].ToString()) ? dtDMGTransaction.Rows[0]["SendToQA"].ToString() : "";
            hdnMPDEmail.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["SendToMPD"].ToString()) ? dtDMGTransaction.Rows[0]["SendToMPD"].ToString() : "";
            hdnAppEmpNo.Value = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["AppEmpno"].ToString()) ? dtDMGTransaction.Rows[0]["AppEmpno"].ToString() : "";

            if (txtView.Text == "Dent")
            {
                rBtnNature.SelectedValue = "Dent";
            }
            if (txtView.Text == "Hole")
            {
                rBtnNature.SelectedValue = "Hole";
            }
            if (txtView.Text == "Loose vacuum")
            {
                rBtnNature.SelectedValue = "LooseVacuum";
            }
            if (txtView.Text == "Torn")
            {
                rBtnNature.SelectedValue = "Torn";
            }
            if (txtView.Text == "Wet")
            {
                rBtnNature.SelectedValue = "Wet";
            }
            if (txtView.Text == "Others")
            {
                rBtnNature.SelectedValue = "Others";
                txtOthers.Text = !string.IsNullOrEmpty(dtDMGTransaction.Rows[0]["Attributes1"].ToString()) ? dtDMGTransaction.Rows[0]["Attributes1"].ToString() : "";
                txtOthers.Visible = true;
            }

        }
        else
        {
            AlertMessage.Show("warning", "Information:", "Records not found.");
            hdnAction.Value = "redirect";
        }

    }
    private void LoadRemarks_SQA(string transType, string keyword)
    {
        DataTable dtSQA = new DataTable();
        dtSQA = dl.get_DR_Remarks(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, keyword);
        if (dtSQA != null && dtSQA.Rows.Count > 0)
        {
            ddlDisposition_SQA.SelectedValue = !string.IsNullOrEmpty(dtSQA.Rows[0]["Disposition"].ToString()) ? dtSQA.Rows[0]["Disposition"].ToString() : "";
            txtBURemarksDate_SQA.Text = !string.IsNullOrEmpty(dtSQA.Rows[0]["Date"].ToString()) ? dtSQA.Rows[0]["Date"].ToString() : "";
            txtComments_SQA.Text = !string.IsNullOrEmpty(dtSQA.Rows[0]["Comment"].ToString()) ? dtSQA.Rows[0]["Comment"].ToString() : "";

        }
        else
        {
            AlertMessage.Show("warning", "Information:", "Records not found.");
            hdnAction.Value = "redirect";
        }

    }
    private void LoadRemarks_MPD(string transType, string keyword)
    {
        DataTable dtSQA = new DataTable();
        dtSQA = dl.get_DR_Remarks(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, keyword);
        if (dtSQA != null && dtSQA.Rows.Count > 0)
        {
            ddlDisposition_MPD.SelectedValue = !string.IsNullOrEmpty(dtSQA.Rows[0]["Disposition"].ToString()) ? dtSQA.Rows[0]["Disposition"].ToString() : "";
            txtBURemarksDate_MPD.Text = !string.IsNullOrEmpty(dtSQA.Rows[0]["Date"].ToString()) ? dtSQA.Rows[0]["Date"].ToString() : "";
            txtComments_MPD.Text = !string.IsNullOrEmpty(dtSQA.Rows[0]["Comment"].ToString()) ? dtSQA.Rows[0]["Comment"].ToString() : "";

        }
        else
        {
            AlertMessage.Show("warning", "Information:", "Records not found.");
            hdnAction.Value = "redirect";
        }

    }
    private void LoadFinalDisposition(string transType, string keyword)
    {
        DataTable dtSQA = new DataTable();
        dtSQA = dl.get_DR_Remarks(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, keyword);
        if (dtSQA != null && dtSQA.Rows.Count > 0)
        {
            ddlFinalDisposition.SelectedValue = !string.IsNullOrEmpty(dtSQA.Rows[0]["Disposition"].ToString()) ? dtSQA.Rows[0]["Disposition"].ToString() : "";
            txtFinalDate.Text = !string.IsNullOrEmpty(dtSQA.Rows[0]["Date"].ToString()) ? dtSQA.Rows[0]["Date"].ToString() : "";
            txtFinalComments.Text = !string.IsNullOrEmpty(dtSQA.Rows[0]["Comment"].ToString()) ? dtSQA.Rows[0]["Comment"].ToString() : "";

        }
        else
        {
            AlertMessage.Show("warning", "Information:", "Records not found.");
            hdnAction.Value = "redirect";
        }

    }

    //Load Attachments
    private void Load_Attachment()
    {
        DataTable dtAttachment = new DataTable();
        dtAttachment = dl.get_DR_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per refid", hdnRefID.Value);
        gvAttachment.DataSource = dtAttachment;
        gvAttachment.DataBind();

        if (gvAttachment.Rows.Count > 0)
        {
            gvAttachment.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        Load_DR_HistoryLogs(hdnRefID.Value);

    }
    private void Load_DR_HistoryLogs(string keyword)
    {
        DataTable dtDRLogs = new DataTable();
        dtDRLogs = dl.get_DR_HistoryLogs(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), keyword);
        gvHistoryLogs.DataSource = dtDRLogs;
        gvHistoryLogs.DataBind();
        if (gvHistoryLogs.Rows.Count > 0)
        {
            gvHistoryLogs.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        upnlLogs.Update();
    }
    private void SetButtonsVisibility()
    {
        isApprover = hdnUserEmpNo.Value == ddlApprover.SelectedValue;
        isSQA = hdnUserEmpName.Value == hdnEmpNameQA.Value;
        isMPD = hdnUserEmpName.Value == hdnEmpNameMPD.Value;
        SetControls();
        Set_Control_Visibility();
        if (!isSQA)
        {
            ddlDisposition_SQA.Enabled = false;
            txtComments_SQA.Enabled = false;
            lbtnSubmit_SQA.Visible = false;
        }
        if (!isMPD) 
        {
            ddlDisposition_MPD.Enabled = false;
            txtComments_MPD.Enabled = false;
            lbtnSubmit_MPD.Visible = false;
        }
        if (hdnReqStat.Value.ToLower() == "for submission")
        {
            ButtonsAfterSave();
            if (hdnUserEmpName.Value.ToLower() == txtIssuedBy.Text.ToLower()) 
            {
                lbtnSubmit_App.Visible = true;
                lbtnUpdate_App.Visible = true;
            }
            else 
            {
                lbtnSubmit_App.Visible = false;
                lbtnUpdate_App.Visible = false;
            }
        }
        else if (hdnReqStat.Value.ToLower() == "awaiting approval")
        {
            if (isAdmin || isApprover)
            {
                AdminAndApproverButton();
            }
            else
            {
                RequestorButton();
            }
        }
        else if (hdnReqStat.Value.ToLower() == "for mpd remarks")
        {
            if (ddlDisposition_SQA.SelectedValue.ToLower() == "reject")
            {
                ddlDisposition_SQA.BackColor = System.Drawing.Color.Red;
                ddlDisposition_SQA.ForeColor = System.Drawing.Color.White;
                ddlDisposition_SQA.Font.Bold = true;
            }
        }
        else if (hdnReqStat.Value.ToLower() == "for final disposition")
        {
            if (ddlDisposition_SQA.SelectedValue.ToLower() == "good")
            {
                ddlDisposition_SQA.BackColor = System.Drawing.Color.Green;
                ddlDisposition_SQA.ForeColor = System.Drawing.Color.White;
                ddlDisposition_SQA.Font.Bold = true;
            }
            else if (ddlDisposition_SQA.SelectedValue.ToLower() == "reject")
            {
                ddlDisposition_SQA.BackColor = System.Drawing.Color.Red;
                ddlDisposition_SQA.ForeColor = System.Drawing.Color.White;
                ddlDisposition_SQA.Font.Bold = true;
            }
            ddlDisposition_MPD.BackColor = System.Drawing.Color.Red;
            ddlDisposition_MPD.ForeColor = System.Drawing.Color.White;
            ddlDisposition_MPD.Font.Bold = true;
        }
        else if (hdnReqStat.Value.ToLower() == "closed")
        {
            if (ddlDisposition_SQA.SelectedValue.ToLower() == "good")
            {
                ddlDisposition_SQA.BackColor = System.Drawing.Color.Green;
                ddlDisposition_SQA.ForeColor = System.Drawing.Color.White;
                ddlDisposition_SQA.Font.Bold = true;
            }
            else if (ddlDisposition_SQA.SelectedValue.ToLower() == "reject")
            {
                ddlDisposition_SQA.BackColor = System.Drawing.Color.Red;
                ddlDisposition_SQA.ForeColor = System.Drawing.Color.White;
                ddlDisposition_SQA.Font.Bold = true;
            }
            ddlDisposition_MPD.BackColor = System.Drawing.Color.Red;
            ddlDisposition_MPD.ForeColor = System.Drawing.Color.White;
            ddlDisposition_MPD.Font.Bold = true;

            ddlFinalDisposition.BackColor = System.Drawing.Color.Red;
            ddlFinalDisposition.ForeColor = System.Drawing.Color.White;
            ddlFinalDisposition.Font.Bold = true;
        }
    }
    private void Set_Control_Visibility()
    {
        if (isApprover && hdnReqStat.Value.ToLower() == "for sqa remarks" || hdnReqStat.Value.ToLower() == "denied")
        {
            divConcern.Visible = false;
            divFinalDis.Visible = false;
        }
        else if (isSQA && hdnReqStat.Value.ToLower() == "for sqa remarks")
        {
            divConcern.Visible = true;
            divFinalDis.Visible = false;
        }
        else if (hdnReqStat.Value.ToLower() == "for mpd remarks")
        {
            if (isRequestor || isApprover || isSQA || isMPD)
            {
                divConcern.Visible = true;
                divFinalDis.Visible = false;
            }
        }
        else if (hdnReqStat.Value.ToLower() == "for final disposition")
        {
            if (isSQA || isMPD)
            {
                divConcern.Visible = true;
                divFinalDis.Visible = false;
            }
            else if (isApprover)
            {
                divConcern.Visible = true;
                divFinalDis.Visible = true;
            }
            else if (hdnUserEmpName.Value.ToLower() == txtIssuedBy.Text.ToLower())
            {
                divConcern.Visible = true;
                divFinalDis.Visible = true;
            }
            else if (hdnUserEmpName.Value.ToLower() != txtIssuedBy.Text.ToLower())
            {
                divConcern.Visible = true;
                divFinalDis.Visible = false;
            }

        }
        else if (hdnReqStat.Value.ToLower() == "closed")
        {
            divConcern.Visible = true;
            divFinalDis.Visible = true;
        }
        else
        {
            divConcern.Visible = false;
            divFinalDis.Visible = false;
        }
        
        if (isAdmin)
        {
            if (hdnReqStat.Value.ToLower() == "denied")
            {
                divConcern.Visible = false;
                divFinalDis.Visible = false;
            }
            else 
            {
                divConcern.Visible = true;
                divFinalDis.Visible = true;
            }
            
        }
        else if (isLeader)
        {
            if (hdnReqStat.Value.ToLower() == "denied")
            {
                divConcern.Visible = false;
                divFinalDis.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "for submission") 
            {
                divConcern.Visible = false;
                divFinalDis.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "awaiting approval")
            {
                divConcern.Visible = false;
                divFinalDis.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "for sqa remarks" || hdnReqStat.Value.ToLower() == "for mpd remarks")
            {
                divConcern.Visible = true;
                divFinalDis.Visible = false;
            }
            else if (hdnReqStat.Value.ToLower() == "for final disposition")
            {
                divConcern.Visible = true;
                divFinalDis.Visible = true;
            }

        }
    }
    #endregion
    #region Nature of damage radio button
    //Nature of Damage Radiobuttons
    protected void rBtnNature_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rBtnNature.SelectedValue == "Dent")
        {
            txtView.Text = "Dent";
            txtOthers.Visible = false;
        }
        if (rBtnNature.SelectedValue == "Hole")
        {
            txtView.Text = "Hole";
            txtOthers.Visible = false;
        }
        if (rBtnNature.SelectedValue == "LooseVacuum")
        {
            txtView.Text = "Loose vacuum";
            txtOthers.Visible = false;
        }
        if (rBtnNature.SelectedValue == "Torn")
        {
            txtView.Text = "Torn";
            txtOthers.Visible = false;
        }
        if (rBtnNature.SelectedValue == "Wet")
        {
            txtView.Text = "Wet";
            txtOthers.Visible = false;
        }
        if (rBtnNature.SelectedValue == "Others")
        {
            txtView.Text = "Others";
            txtOthers.Visible = true;
            txtOthers.Text = string.Empty;
            txtOthers.Focus();
        }
    }
    #endregion

    #region Selection Methods and Buttons
    protected void lbtnEmpSearch_SendTo_Click(object sender, EventArgs e)
    {
        EmpSearch.Show("with username");

    }
    protected void Selected_Employee_QA(string TransType, string EmpNo, string EmpEmail)
    {
        cl_Employee emp = new cl_Employee();
        emp.Select_EmployeeMaster_Email(TransType, EmpNo, EmpEmail);

        txtSendToQA.Text = emp.Email1 + "; ";
        hdnEmpNoQA.Value = EmpNo;
        hdnEmpNameQA.Value = emp.EmpName;
        hdnDeptQA.Value = emp.Department;
        lBtnAdd_sendToQA.Enabled = false;

        uPnlDocReceiver.Update();

        if (isAdmin && hdnAdminButton.Value.ToLower() == "reassign")
        {
            txtLocal.Enabled = false;
            txtRemarks.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtBANo.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
        }
    }
    protected void Selected_Employee_MPD(string TransType, string EmpNo, string EmpEmail)
    {
        cl_Employee emp = new cl_Employee();
        emp.Select_EmployeeMaster_Email(TransType, EmpNo, EmpEmail);

        txtSendToMPD.Text = emp.Email1 + "; ";
        hdnEmpNoMPD.Value = EmpNo;
        hdnEmpNameMPD.Value = emp.EmpName;
        hdnDeptMPD.Value = emp.Department;
        lBtnAdd_sendToMPD.Enabled = false;

        uPnlDocReceiver.Update();

        if (isAdmin && hdnAdminButton.Value.ToLower() == "reassign")
        {
            txtLocal.Enabled = false;
            txtRemarks.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtBANo.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
        }
    }
    protected void Selected_Employee_CopyTo(string TransType, string EmpNo, string EmpEmail)
    {
        cl_Employee emp = new cl_Employee();
        emp.Select_EmployeeMaster_Email_CopyTo(TransType, EmpNo, EmpEmail);

        txtCopyTo.Text += emp.Email1 + "; ";
        hdnEmpNoCopy.Value += emp.EmpNo + "; ";
        hdnEmpNameCopy.Value += emp.EmpName + "; ";
        hdnDeptCopy.Value += emp.Department + "; ";

        uPnlDocReceiver.Update();

        if (isAdmin && hdnAdminButton.Value.ToLower() == "reassign")
        {
            txtLocal.Enabled = false;
            txtRemarks.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtBANo.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
        }
    }

    #endregion

    #region Check if all fields have value
    private Boolean isComplete()
    {
        bool result = true;
        if (ddlAffecBU.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select affected BU!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtLocal.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input local number!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtView.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select nature of damage!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (ddlDamageNoted.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select Damage Noted at!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtInvoiceNo.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input invoice number!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtItemCode.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input item code!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (ddlSupplier.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select supplier!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtDescription.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input description!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (ddlForwarder.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select forwader!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtInvoiceQty.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input invoice quantity!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtBANo.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input BL/AWH number!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtQtyAffected.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input quantity affected!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtReceiveDate.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input receiving date!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtNoOfBox.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input number of box affected!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtRemarks.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input remarks!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (gvAttachment.Rows.Count == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please attach file!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtSendToQA.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select document receiver!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtSendToMPD.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select document receiver!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtCopyTo.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select document receiver!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (ddlApprover.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select approver!");
            hdnAction.Value = "alert";
            result = false;
        }
        return result;

    }
    private Boolean isComplete_SQA()
    {
        bool result = true;
        if (ddlDisposition_SQA.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select disposition!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtComments_SQA.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input comment(s)!");
            hdnAction.Value = "alert";
            result = false;
        }
        return result;
    }
    private Boolean isComplete_MPD()
    {
        bool result = true;
        if (ddlDisposition_MPD.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select disposition!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtComments_MPD.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input comment(s)!");
            hdnAction.Value = "alert";
            result = false;
        }
        return result;
    }
    private Boolean isComplete_Final()
    {
        bool result = true;
        if (ddlFinalDisposition.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select disposition!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtFinalComments.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please input comment(s)!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (ddlDisposition_SQA.SelectedIndex == 0 || txtComments_SQA.Text =="" || txtBURemarksDate_SQA.Text == "")
        {
            AlertMessage.Show("warning", "Information:", "Damage report is not for final disposition!");
            hdnAction.Value = "alert";
            result = false;
        }

            return result;
    }
    #endregion


    #region Buttons

    protected void lBtnAdd_sendToQA_Click(object sender, EventArgs e)
    {
        EmailSearch_QA.Show("with username");
    }
    protected void lBtnRemove_sendToQA_Click(object sender, EventArgs e)
    {
        txtSendToQA.Text = String.Empty;
        hdnEmpNoQA.Value = String.Empty;
        hdnEmpNameQA.Value = String.Empty;
        hdnDeptQA.Value = String.Empty;
        lBtnAdd_sendToQA.Enabled = true;

        if (isAdmin && hdnAdminButton.Value.ToLower() == "reassign")
        {
            txtLocal.Enabled = false;
            txtRemarks.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtBANo.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
        }
    }

    protected void lBtnAdd_sendToMPD_Click(object sender, EventArgs e)
    {
        EmailSearch_MPD.Show("with username");
    }

    protected void lBtnRemove_sendToMPD_Click(object sender, EventArgs e)
    {
        txtSendToMPD.Text = String.Empty;
        hdnEmpNoMPD.Value = String.Empty;
        hdnEmpNameMPD.Value = String.Empty;
        hdnDeptMPD.Value = String.Empty;
        lBtnAdd_sendToMPD.Enabled = true;

        if (isAdmin && hdnAdminButton.Value.ToLower() == "reassign")
        {
            txtLocal.Enabled = false;
            txtRemarks.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtBANo.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
        }
    }

    protected void lBtnAdd_copyTo_Click(object sender, EventArgs e)
    {
        EmailSearch_CopyTo.Show("with username");
    }

    protected void lBtnRemove_copyTo_Click(object sender, EventArgs e)
    {
        txtCopyTo.Text = String.Empty;
        hdnEmpNoCopy.Value = String.Empty;
        hdnEmpNameCopy.Value = String.Empty;
        hdnDeptCopy.Value = String.Empty;

        if (isAdmin && hdnAdminButton.Value.ToLower() == "reassign")
        {
            txtLocal.Enabled = false;
            txtRemarks.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtBANo.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
        }
    }

    protected void lbtnSave_App_Click(object sender, EventArgs e)
    {
        if (isComplete())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to save this data?");
            hdnAction.Value = "save damage report";
        }
    }

    protected void lbtnUpdate_App_Click(object sender, EventArgs e)
    {
        string prompt = "Are you sure you want to update this data?";
        string action = "update damage report";
        if (isComplete())
        {
            if (isAdmin || isLeader)
            {
                if (hdnReqStat.Value.ToLower() == "for submission" || hdnReqStat.Value.ToLower() == "awaiting approval" || hdnReqStat.Value.ToLower() == "for sqa remarks" || hdnReqStat.Value.ToLower() == "for mpd remarks")
                {
                    if (hdnAdminButton.Value == "edit")
                    {
                        //AlertMessage.Show("confirmationwithremarks", "Confirmation:",);
                        //hdnAction.Value = "update by admin";
                        prompt = "Are you sure you want to update?";
                        action = "update by admin";
                    }
                    else if (hdnAdminButton.Value == "reassign")
                    {
                        //AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to re-assign?");
                        //hdnAction.Value = "reassign";
                        if (hdnSQAEmail.Value != txtSendToQA.Text) 
                        {
                            prompt = "Are you sure you want to re-assign new SQA?";
                            action = "reassign sqa";
                        }
                        else if (hdnMPDEmail.Value != txtSendToMPD.Text)
                        {
                            prompt = "Are you sure you want to re-assign new MPD?";
                            action = "reassign mpd";
                        }
                        else if (hdnAppEmpNo.Value != ddlApprover.SelectedValue)
                        {
                            prompt = "Are you sure you want to re-assign new approver?";
                            action = "reassign";
                        }

                        if (hdnSQAEmail.Value != txtSendToQA.Text && hdnMPDEmail.Value != txtSendToMPD.Text && hdnAppEmpNo.Value != ddlApprover.SelectedValue)
                        {
                            prompt = "You can't re-assign multiple!";
                            action = "refresh";

                        }
                        else if (hdnSQAEmail.Value != txtSendToQA.Text && hdnMPDEmail.Value != txtSendToMPD.Text)
                        {
                            prompt = "You can't re-assign multiple!";
                            action = "refresh";
                        }
                        else if (hdnSQAEmail.Value != txtSendToQA.Text && hdnAppEmpNo.Value != ddlApprover.SelectedValue)
                        {
                            prompt = "You can't re-assign multiple!";
                            action = "refresh";
                        }
                        else if (hdnMPDEmail.Value != txtSendToMPD.Text && hdnAppEmpNo.Value != ddlApprover.SelectedValue)
                        {
                            prompt = "You can't re-assign multiple!";
                            action = "refresh";
                        }



                    }
                    
                }
                else
                {
                    //AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to update this data?");
                    //hdnAction.Value = "update by admin";
                    action = "update by admin";
                }
            }


            if (prompt == "You can't re-assign multiple!")
            {
                AlertMessage.Show("warning", "Information:", "You can't re-assign multiple!");
            }
            else 
            {
                AlertMessage.Show("confirmationwithremarks", "Confirmation:", prompt);
            }

            
            hdnAction.Value = action;

        }
    }

    protected void lbtnApprove_App_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to approve this request?");
        hdnAction.Value = "approve damage report";
    }
    protected void ddlApprover_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (hdnUserEmpNo.Value == ddlApprover.SelectedValue)
        {
            AlertMessage.Show("warning", "Information:", "You can't select your self as approver!");
            hdnAction.Value = "select approver";
        }
        if (isAdmin) 
        {
            txtLocal.Enabled = false;
            txtRemarks.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtBANo.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
        }
        

    }

    #endregion

    #region Attachment Buttons

    protected void lbtnUpload_Command(object sender, CommandEventArgs e)
    {
        uPnlDamageInfo.Update();
        if (fuAttachment.FileName == "")
        {
            AlertMessage.Show("Warning", "Information:", "Please select file to attach!");
            hdnAction.Value = "alert";
        }
        else
        {
            try
            {
                string fileName = fuAttachment.FileName;
                string newFileName = hdnRefID.Value + "-" + fileName.Replace("#", "No.");
                int FileName_Length = fileName.Length;
                string FileName_Ext = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                if (".gif.jpg.tiff.raw.gif.bmp.png.ppm.pam.tiff.pdf.xls.xlsx.doc.docx.ppt.pptx".ToLower().IndexOf(FileName_Ext) < 0)
                {
                    AlertMessage.Show("Warning", "Information:", "Invalid File Format!");
                    hdnAction.Value = "alert";
                    return;
                }
                else if (fuAttachment.FileBytes.Length > cl_DBConn.FileSize())
                {
                    AlertMessage.Show("Warning", "Information:", "File exceeds 10MB! Please compress.");
                    hdnAction.Value = "alert";
                    return;
                }
                else
                {
                    string FilePath = Server.MapPath(@"~/FileDirectory/Attachment/" + newFileName);

                    dto.ID = "";
                    dto.RefID = hdnRefID.Value;
                    dto.FileName_Orig = fuAttachment.FileName;
                    dto.FileName_New = newFileName;
                    dto.FilePath = FilePath;
                    dto.ActionRemarks = "";
                    dto.CreatedBy_EmpNo = hdnUserEmpNo.Value;
                    dto.CreatedBy_EmpName = hdnUserEmpName.Value;
                    dto.Attribute1 = "-";
                    dto.Attribute2 = "-";

                    string result = dl.Insert_DR_Attachment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), dto, "save");
                    if (result.ToLower() == "request timeout")
                    {
                        AlertMessage.Show("Warning", "Information:", "Request Timeout: Please try again!");
                        hdnAction.Value = "alert";
                    }
                    else if (result.ToLower().Contains("exist"))
                    {
                        AlertMessage.Show("Information", "Information:", result);
                        hdnAction.Value = "alert";
                    }
                    else
                    {
                        fuAttachment.PostedFile.SaveAs(FilePath);
                    }
                    Load_Attachment();
                }

            }
            catch (Exception ex)
            {
                AlertMessage.Show("Warning", "Information:", "Request Timeout: Please try again!");
                hdnAction.Value = "alert";
            }
            fuAttachment.Dispose();
            uPnlDamageInfo.Update();
        }
    }

    protected void lbtnOpen_Command(object sender, CommandEventArgs e)
    {
        try
        {
            string filePath = e.CommandName.ToString();
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }
        catch (Exception ex)
        {
            //
        }
    }
    protected void lbtnDeleteAttachment_Command(object sender, CommandEventArgs e)
    {
        hdnAttachmentID.Value = e.CommandArgument.ToString();
        hdnAttachmentPath.Value = e.CommandName.ToString();
        upnlActionButton.Update();
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you delete this attachment?");
        hdnAction.Value = "delete attachment";
    }
    #endregion



    protected void lbtnDeny_App_Click(object sender, EventArgs e)
    {
        AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to deny this request?");
        hdnAction.Value = "deny damage report";
    }

    protected void lbtnSubmit_SQA_Click(object sender, EventArgs e)
    {
        if (isComplete_SQA())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to submit this remarks?");
            hdnAction.Value = "submit sqa remarks";
        }
    }


    protected void lbtnSubmit_MPD_Click(object sender, EventArgs e)
    {
        if (isComplete_MPD())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to submit this remarks?");
            hdnAction.Value = "submit mpd remarks";
        }

    }

    protected void lbtnSubmit_Final_Click(object sender, EventArgs e)
    {
        if (isComplete_Final())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to close this report?");
            hdnAction.Value = "submit final disposition";
        }

    }

    protected void lbtnSubmit_App_Click(object sender, EventArgs e)
    {
        if (isComplete())
        {
            AlertMessage.Show("confirmationwithremarks", "Confirmation:", "Are you sure you want to submit this data?");
            hdnAction.Value = "submit damage report";
        }
    }
    protected void gvHistoryLogs_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistoryLogs.PageIndex = e.NewPageIndex;
        Load_DR_HistoryLogs(hdnRefID.Value);
    }

    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        hdnAdminButton.Value = "edit";
        lbtnEdit.Visible = false;
        lbtnUpdate_App.Visible = true;
        lbtnCancel.Visible = true;

        ddlAffecBU.Enabled = true;
        txtLocal.Enabled = true;
        rBtnNature.Enabled = true;
        txtOthers.Enabled = true;
        ddlDamageNoted.Enabled = true;
        txtInvoiceNo.Enabled = true;
        txtItemCode.Enabled = true;
        ddlSupplier.Enabled = true;
        txtDescription.Enabled = true;
        ddlForwarder.Enabled = true;
        txtInvoiceQty.Enabled = true;
        txtBANo.Enabled = true;
        txtQtyAffected.Enabled = true;
        txtReceiveDate.Enabled = true;
        txtNoOfBox.Enabled = true;
        txtRemarks.Enabled = true;

        lbtnReassign.Visible = false;
        
        //Attachment 
        divUploadAttach.Visible = true;
        LinkButton lbtnDelete = new LinkButton();
        foreach (GridViewRow row in gvAttachment.Rows)
        {
            lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
            lbtnDelete.Visible = true;
        }
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        //Attachment 
        divUploadAttach.Visible = false;
        LinkButton lbtnDelete = new LinkButton();
        foreach (GridViewRow row in gvAttachment.Rows)
        {
            lbtnDelete = (LinkButton)gvAttachment.Rows[row.RowIndex].FindControl("lbtnDelete");
            lbtnDelete.Visible = false;
        }

        lbtnEdit.Visible = true;
        lbtnUpdate_App.Visible = false;
        lbtnCancel.Visible = false;

        ddlAffecBU.Enabled = false;
        txtLocal.Enabled = false;
        rBtnNature.Enabled = false;
        txtOthers.Enabled = false;
        ddlDamageNoted.Enabled = false;
        txtInvoiceNo.Enabled = false;
        txtItemCode.Enabled = false;
        ddlSupplier.Enabled = false;
        txtDescription.Enabled = false;
        ddlForwarder.Enabled = false;
        txtInvoiceQty.Enabled = false;
        txtBANo.Enabled = false;
        txtQtyAffected.Enabled = false;
        txtReceiveDate.Enabled = false;
        txtNoOfBox.Enabled = false;
        txtRemarks.Enabled = false;
        ddlApprover.Enabled = false;
        ddlApprover.Visible = false;
        txtApprover.Visible = true;

        Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);


    }




    protected void lbtnReassign_Click(object sender, EventArgs e)
    {
        hdnAdminButton.Value = "reassign";
        if (hdnReqStat.Value.ToLower() == "for submission" || hdnReqStat.Value.ToLower() == "awaiting approval")
        {
            ddlApprover.Enabled = true;
            ddlApprover.Visible = true;
            txtApprover.Visible = false;

            lbtnEdit.Visible = false;
            lbtnUpdate_App.Visible = true;
            lbtnReassign.Visible = false;
            lbtnCancelReassign.Visible = true;

            txtLocal.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtBANo.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
            txtRemarks.Enabled = false;

            lBtnAdd_sendToQA.Visible = true;
            lBtnRemove_sendToQA.Visible = true;
            lBtnAdd_sendToMPD.Visible = true;
            lBtnRemove_sendToMPD.Visible = true;
            lBtnAdd_copyTo.Visible = true;
            lBtnRemove_copyTo.Visible = true;

            //BU REMARKS
            //if (hdnReqStat.Value.ToLower() == "for submission" || hdnReqStat.Value.ToLower() == "awaiting approval" || hdnReqStat.Value.ToLower() == "for sqa remarks")
            //{
            //    lBtnAdd_sendToQA.Visible = true;
            //    lBtnRemove_sendToQA.Visible = true;
            //    lBtnAdd_sendToMPD.Visible = true;
            //    lBtnRemove_sendToMPD.Visible = true;
            //    lBtnAdd_copyTo.Visible = true;
            //    lBtnRemove_copyTo.Visible = true;
            //}
            //else if (hdnReqStat.Value.ToLower() == "for mpd remarks")
            //{
            //    lBtnAdd_sendToQA.Visible = false;
            //    lBtnRemove_sendToQA.Visible = false;
            //    lBtnAdd_sendToMPD.Visible = true;
            //    lBtnRemove_sendToMPD.Visible = true;
            //    lBtnAdd_copyTo.Visible = true;
            //    lBtnRemove_copyTo.Visible = true;
            //}
            //else if (hdnReqStat.Value.ToLower() == "for final disposition")
            //{
            //    lBtnAdd_sendToQA.Visible = false;
            //    lBtnRemove_sendToQA.Visible = false;
            //    lBtnAdd_sendToMPD.Visible = false;
            //    lBtnRemove_sendToMPD.Visible = false;
            //    lBtnAdd_copyTo.Visible = true;
            //    lBtnRemove_copyTo.Visible = true;
            //}
        }
        else if (hdnReqStat.Value.ToLower() == "for sqa remarks")
        {
            lbtnEdit.Visible = false;
            lbtnUpdate_App.Visible = true;
            lbtnReassign.Visible = false;
            lbtnCancelReassign.Visible = true;

            //disable approver
            ddlApprover.Enabled = false;
            ddlApprover.Visible = false;
            txtApprover.Visible = true;

            txtLocal.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtBANo.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
            txtRemarks.Enabled = false;

            //enable sqa, mpd and cc
            lBtnAdd_sendToQA.Visible = true;
            lBtnRemove_sendToQA.Visible = true;
            lBtnAdd_sendToMPD.Visible = true;
            lBtnRemove_sendToMPD.Visible = true;
            lBtnAdd_copyTo.Visible = true;
            lBtnRemove_copyTo.Visible = true;
        }
        else if (hdnReqStat.Value.ToLower() == "for mpd remarks")
        {
            lbtnEdit.Visible = false;
            lbtnUpdate_App.Visible = true;
            lbtnReassign.Visible = false;
            lbtnCancelReassign.Visible = true;

            //disable approver
            ddlApprover.Enabled = false;
            ddlApprover.Visible = false;
            txtApprover.Visible = true;

            txtLocal.Enabled = false;
            txtInvoiceNo.Enabled = false;
            txtItemCode.Enabled = false;
            txtReceiveDate.Enabled = false;
            txtDescription.Enabled = false;
            txtInvoiceQty.Enabled = false;
            txtBANo.Enabled = false;
            txtQtyAffected.Enabled = false;
            txtNoOfBox.Enabled = false;
            txtRemarks.Enabled = false;

            //disable sqa buttons
            lBtnAdd_sendToQA.Visible = false;
            lBtnRemove_sendToQA.Visible = false;

            //enable sqa, mpd and cc
            lBtnAdd_sendToMPD.Visible = true;
            lBtnRemove_sendToMPD.Visible = true;
            lBtnAdd_copyTo.Visible = true;
            lBtnRemove_copyTo.Visible = true;
        }

    }

    protected void lbtnCancelReassign_Click(object sender, EventArgs e)
    {
        Response.Redirect("Registration.aspx?RefID=" + hdnRefID.Value);
    }



    protected void txtReceiveDate_TextChanged(object sender, EventArgs e)
    {
        DateTime selectedDate = DateTime.Parse(txtReceiveDate.Text);
        String now = DateTime.Now.ToString("MM/dd/yyyy");
        DateTime dateNow = DateTime.Parse(now.ToString());

        if (selectedDate > dateNow)
        {
            AlertMessage.Show("Warning", "Information:", "Please enter a receiving date that is not in the future!");
            hdnAction.Value = "alert";
            txtReceiveDate.Text = String.Empty;
        }

    }
}