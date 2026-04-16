using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;

public partial class PageView_ViewRecords : System.Web.UI.Page
{
    private static Boolean isApprover = false;
    private static Boolean isRequestor = false;
    private static Boolean isLeader = false;

    i_DataLayer dl = new cl_DataLayer();
    private static Boolean isAdmin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            if (emp.HasRows)
            {
                isAdmin = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is admin", "Administrator", emp.EmpNo, "",""));
                
                if (!isAdmin) ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "MenuHide('five-ddheader');", true);
                hdnUser_EmpNo.Value = emp.EmpNo;
                hdnUser_EmpName.Value = emp.EmpName;
                hdnUserDepCode.Value = emp.DepCode;

                isLeader = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is leader", "Warehouse Leader", emp.EmpNo, "", ""));
                isRequestor = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is requestor", "Requestor", emp.EmpNo, "", ""));
                isApprover = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is approver", "", emp.EmpNo, "", hdnUserDepCode.Value));

                //if (!isRequestor && !isAdmin && !isApprover && !isLeader)
                //{
                    //Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
                //}

            }
            else
            {
                Unauthorized();
            }
            emp.Dispose();

            cl_Common.fill_DropDownList(ddlStatus, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get status", "", "", ""), "ItemCode", "Description", "--Please select--");

			string status = !string.IsNullOrEmpty(Request.QueryString["status"]) ? Request.QueryString["status"] : "0";
            if (ddlStatus.Items.Contains(new ListItem(status, status))) ddlStatus.SelectedValue = status;
            else ddlStatus.SelectedIndex = 0;

            hdnKeyword.Value = (!string.IsNullOrEmpty(Request.QueryString["Keyword"])) ? Request.QueryString["Keyword"] : "";
            if (hdnKeyword.Value.Trim().Length > 0) txtKeyword.Text = hdnKeyword.Value;
            else txtKeyword.Text = "";

            Session["KEYWORD"] = hdnKeyword.Value;
            Session["FROMDATE"] = txtFromDate.Text;
            Session["TODATE"] = txtToDate.Text;
            Load_Records("view records");
        }
        else
        {
            if (gvRecords.Rows.Count > 0) gvRecords.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    private DataTable GetData()
    {
        TIPIS3WebAdmin.TIPEmployeeMaster.Data.Portal.EMP allEmp = new TIPIS3WebAdmin.TIPEmployeeMaster.Data.Portal.EMP(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        allEmp.Fill(dt);
        return dt;
    }
    
    private void Unauthorized()
    {
        Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
    }
    
    private void Load_Records(string transType)
    {
        hdnKeyword.Value = !string.IsNullOrEmpty(txtKeyword.Text.Trim()) ? txtKeyword.Text.Trim() : "";
        txtFromDate.Text = !string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? txtFromDate.Text.Trim() : "";
        txtToDate.Text = !string.IsNullOrEmpty(txtToDate.Text.Trim()) ? txtToDate.Text.Trim() : "";

        DataTable dtRecords = new DataTable();
        string selectedStatus = "";
        if (ddlStatus.SelectedValue != "0") selectedStatus = ddlStatus.SelectedValue;
        dtRecords = dl.get_DR_DMGTransaction(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), transType, selectedStatus, hdnKeyword.Value.Trim(), txtFromDate.Text.Trim(), txtToDate.Text.Trim(), hdnUser_EmpNo.Value);
        gvRecords.PageSize = int.Parse(ddlPageSize.SelectedValue);
        gvRecords.DataSource = dtRecords;
        gvRecords.DataBind();
        if (gvRecords.Rows.Count > 0)
        {
            lblCount.Text = "Record Count: " + dtRecords.Rows.Count.ToString();
            gvRecords.HeaderRow.TableSection = TableRowSection.TableHeader;
            lbtnExport.Visible = true;
        }
        else
        {
            lblCount.Text = "Record Count: 0";
            lbtnExport.Visible = false;
        }
        if (!isAdmin) lbtnExport.Visible = false;

        upnlRecords.Update();
        dtRecords.Dispose();

        Session["KEYWORD"] = hdnKeyword.Value;
        Session["FROMDATE"] = txtFromDate.Text;
        Session["TODATE"] = txtToDate.Text;
        //Session["DRSTATUS"] = selectedStatus;
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Records("view records");
    }

    protected void grdRecords_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvRecords.PageIndex = e.NewPageIndex;
        Load_Records("view records");
    }

    protected void lbtnView_Command(object sender, CommandEventArgs e)
    {
        Response.Redirect("~/PageTransaction/Registration.aspx?RefID=" + e.CommandName.ToString());
    }
    
    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        Load_Records("view records");
    }

    protected void ddlParameter_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Records("view records");
    }

    private void ExportToExcel(string ExportTemplate)
    {
        try
        {
            string strFilename = DateTime.Now.ToString("yyyy-MM-dd") + "-" + "Report.xlsx";
            string strExportFile = Server.MapPath("~/FileExport/") + strFilename;

            FileInfo fileTemplate = new FileInfo(ExportTemplate);
            FileInfo fileExport = new FileInfo(strExportFile);
            using (ExcelPackage xlPackage = new ExcelPackage(fileExport, fileTemplate))
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                int iRow = 12;
                int iCol = 1;

                DataTable dtRecords = new DataTable();
                string selectedStatus = "";
                if (ddlStatus.SelectedValue != "0") selectedStatus = ddlStatus.SelectedValue;
                dtRecords = dl.get_DR_DMGTransaction(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "export data", selectedStatus, txtKeyword.Text.Trim(), txtFromDate.Text, txtToDate.Text, hdnUser_EmpNo.Value);
                gvRecords.PageSize = int.Parse(ddlPageSize.SelectedValue);

                foreach (DataRow dr in dtRecords.Rows)
                {
                    iCol = 1;
                    #region ID
                    //worksheet.Cells[iRow, iCol].Value = iRow - 11;
                    //worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    //worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    //worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    //iCol++;
                    #endregion

                    #region Ref ID
                    //worksheet.Cells[iRow, iCol].Value = dr["RefID"].ToString();
                    //worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    //worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    //iCol++;
                    #endregion

                    #region Control Number
                    worksheet.Cells[iRow, iCol].Value = dr["DrNo"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Issued Date
                    worksheet.Cells[iRow, iCol].Value = dr["IssuedDate"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Issued by
                    worksheet.Cells[iRow, iCol].Value = dr["IssuedBy"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region BU
                    worksheet.Cells[iRow, iCol].Value = dr["AffectedBU"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Received Date
                    worksheet.Cells[iRow, iCol].Value = dr["ReceiveDate"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Supplier
                    worksheet.Cells[iRow, iCol].Value = dr["Supplier"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Forwarder
                    worksheet.Cells[iRow, iCol].Value = dr["Forwader"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region BL/AWB No
                    worksheet.Cells[iRow, iCol].Value = dr["BlAwhNo"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Item Code
                    worksheet.Cells[iRow, iCol].Value = dr["ItemCode"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Description
                    worksheet.Cells[iRow, iCol].Value = dr["Description"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Invoice Number
                    worksheet.Cells[iRow, iCol].Value = dr["InvoiceNo"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Affected Quantity
                    worksheet.Cells[iRow, iCol].Value = dr["QtyAffected"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region No. of Box
                    worksheet.Cells[iRow, iCol].Value = dr["NoOfBoxAffected"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Nature of Damage
                    worksheet.Cells[iRow, iCol].Value = dr["NatureOfDiscrepancy"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Damage Noted At
                    worksheet.Cells[iRow, iCol].Value = dr["DamageNote"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Approver1(LOD SV/MGR)
                    worksheet.Cells[iRow, iCol].Value = dr["Approver"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Approver2(SQA)
                    worksheet.Cells[iRow, iCol].Value = dr["QAEmpName"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Approver3(MPD)
                    worksheet.Cells[iRow, iCol].Value = dr["MPDEmpName"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    #region Status
                    worksheet.Cells[iRow, iCol].Value = dr["Status"].ToString();
                    worksheet.Cells[iRow, iCol].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[iRow, iCol].Style.Font.Size = 8;
                    worksheet.Cells[iRow, iCol].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    iCol++;
                    #endregion

                    iRow++;

                }
                worksheet.Name = "Damage Report Database";

                // set category
                worksheet.Cells[6, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[6, 2].Value = ddlStatus.SelectedItem.Text;

                // set parameter
                worksheet.Cells[7, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[7, 2].Value = txtKeyword.Text.Trim();

                // set From Date
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[6, 5].Value = txtFromDate.Text.Trim();

                // set To Date
                worksheet.Cells[7, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells[7, 5].Value = txtToDate.Text.Trim();

                //Set Number of records cell style
                worksheet.Cells[iRow, 1, iRow, iCol].Merge = true;
                worksheet.Cells[iRow, 1, iRow, iCol].Style.Font.Size = 8;
                worksheet.Cells[iRow, 1, iRow, iCol].Style.Font.Bold = true;
                worksheet.Cells[iRow, 1, iRow, iCol].Style.Border.Top.Style = ExcelBorderStyle.Dotted;
                worksheet.Cells[iRow, 1].Value = "Number of record(s) : " + (iRow - 12).ToString();

                //Set all columns to autofit
                //worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                xlPackage.Save();
            }

            fileExport = new FileInfo(strExportFile);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AppendHeader("Content-Disposition", "attachments; filename=" + strFilename);
            Response.AppendHeader("Content-Length", fileExport.Length.ToString());
            Response.WriteFile(strExportFile);
            Response.Flush();
            File.Delete(strExportFile);
            Response.End();
        }
        catch (IOException ex) { string strError = ex.ToString(); }
    }

    protected void lbtnExport_Click(object sender, EventArgs e)
    {
        lbtnExport.Enabled = false;
        upnlRecords.Update();
        if (gvRecords.Rows.Count == 0)
        {
            AlertMessage.Show("Information", "Information:", "No records found. Please search first.");
            hdnAction.Value = "alert";
        }
        else
        {
            ExportToExcel(Server.MapPath("~/FileExport/Template/Report Template.xlsx"));
        }
        lbtnExport.Enabled = true;
        upnlRecords.Update();
    }

    protected void lbtnSearchByDate_Click(object sender, EventArgs e)
    {
        if (isComplete())
        {
            DateTime fromDate = DateTime.Parse(txtFromDate.Text);
            DateTime toDate = DateTime.Parse(txtToDate.Text);
            if (fromDate > toDate)
            {
                AlertMessage.Show("warning", "Information:", "Please select proper date range!");
            }
            else 
            {
               Load_Records("search by date");
            }
        }
    }
    private Boolean isComplete()
    {
        bool result = true;
        if (txtFromDate.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select date from!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtToDate.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select date to!");
            hdnAction.Value = "alert";
            result = false;
        }
        return result;
    }
}