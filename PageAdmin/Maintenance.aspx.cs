using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;

public partial class PageAdmin_Maintenance : System.Web.UI.Page
{
    cl_MaintenanceObject mo = new cl_MaintenanceObject();
    i_DataLayer dl = new cl_DataLayer();
    private static Boolean isAdmin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            if (emp.HasRows)
            {
                hdnUser_EmpNo.Value = emp.EmpNo;
                hdnUser_EmpName.Value = emp.EmpName;
                isAdmin = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is admin", "Administrator", emp.EmpNo, "",""));
                if (!isAdmin)
                {
                    Unauthorized();
                }
            }
            else
            {
                Unauthorized();
            }
            emp.Dispose();

            cl_Common.fill_DropDownList(ddlMainCTQ, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per category", "Main CTQ", "", ""), "ItemCode", "Description", "-- Please Select --");
            cl_Common.fill_DropDownList(ddlPJCategory, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per category", "Project Category", "", ""), "ItemCode", "Description", "-- Please Select --");
            cl_Common.fill_DropDownList(ddlCategory, dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "get category", "", "", ""), "ItemCode", "Description", "-- Please Select --");
            ddlPageSize.SelectedIndex = 1;
            Load_Records();
            hdnItemID.Value = "0";

            divEmpNo.Visible = false;
        }
    }

    private void Unauthorized()
    {
        Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
    }

    private void Load_Records()
    {
        grdMasterList.PageSize = int.Parse(ddlPageSize.SelectedValue);
        grdMasterList.DataSource = dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per category", ddlCategory.SelectedValue, txtKeyword.Text.Trim(), "");
        grdMasterList.DataBind();
        if (grdMasterList.Rows.Count > 0) grdMasterList.HeaderRow.TableSection = TableRowSection.TableHeader;
        uPnlMaintenance.Update();
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            txtCategory.Text = "";            
            Set_Buttons(false);
        }
        else
        {
            txtCategory.Text = ddlCategory.SelectedValue;
            Set_Buttons(true);
        }
        Clear_Text();
        Load_Records();
        uPnlMaintenance.Update();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCategory.SelectedIndex == 0)
        {
            txtCategory.Text = "";
            Set_Buttons(false);
        }
        else
        {      
            switch (ddlCategory.SelectedValue.ToLower())
            {
                case "administrator":
                case "requestor":
                case "warehouse leader":
                    grdMasterList.Columns[2].HeaderText = "Employee Number";
                    lblItem.Text = "Employee Number:";
                    grdMasterList.Columns[3].HeaderText = "Employee Name";
                    grdMasterList.Columns[4].Visible = !true;
                    grdMasterList.Columns[4].HeaderText = "Main CTQ";
                    lblDescription.Text = "Employee Name:";
                    divMainCTQ.Visible = false;
                    divDept.Visible = false;
                    divPJCategory.Visible = false;
                    divEmpNo.Visible = true;
                    txtItemCode.Visible = false;
                    break;
                case "damage noted":
                case "forwarder":
                case "affected bu":
                case "supplier":
                    grdMasterList.Columns[2].HeaderText = "Item";
                    grdMasterList.Columns[3].HeaderText = "Description";
                    grdMasterList.Columns[4].Visible = !true;
                    lblItem.Text = "Item:";
                    lblDescription.Text = "Description:";
                    divEmpNo.Visible = false;
                    txtItemCode.Visible = true;
                    break;
                //case "methodology":
                //    grdMasterList.Columns[2].HeaderText = "Item";
                //    grdMasterList.Columns[3].HeaderText = "Description";
                //    grdMasterList.Columns[4].Visible = !true;
                //    grdMasterList.Columns[4].HeaderText = "Main CTQ";
                //    lblItem.Text = "Item:";
                //    lblDescription.Text = "Description:";
                //    divMainCTQ.Visible = false;
                //    divDept.Visible = false;
                //    divPJCategory.Visible = false;
                //    divEmpNo.Visible = false;
                //    txtItemCode.Visible = true;
                //    break;
                //case "project category sub":
                //    grdMasterList.Columns[2].HeaderText = "Item";
                //    grdMasterList.Columns[3].HeaderText = "Description";
                //    grdMasterList.Columns[4].Visible = true;
                //    grdMasterList.Columns[4].HeaderText = "Project Category";
                //    lblItem.Text = "Item:";
                //    lblDescription.Text = "Description:";
                //    divMainCTQ.Visible = false;
                //    divDept.Visible = false;
                //    divPJCategory.Visible = true;
                //    divEmpNo.Visible = false;
                //    txtItemCode.Visible = true;
                //    break;
                default:
                    grdMasterList.Columns[2].HeaderText = "Item";
                    grdMasterList.Columns[3].HeaderText = "Description";
                    grdMasterList.Columns[4].Visible = !true;
                    grdMasterList.Columns[4].HeaderText = "Main CTQ";
                    lblItem.Text = "Item:";
                    lblDescription.Text = "Description:";
                    divMainCTQ.Visible = false;
                    divDept.Visible = false;
                    divPJCategory.Visible = false;
                    divEmpNo.Visible = false;
                    txtItemCode.Visible = true;
                    break;
            }     
            
            txtCategory.Text = ddlCategory.SelectedValue;
            Set_Buttons(true);
        }
        Clear_Text();
        Load_Records();
        uPnlMaintenance.Update();
    }

    private void Set_Buttons(bool show)
    {
        lbtnCancel.Visible = show;
        lbtnSave.Visible = show;
        lbtnUpdate.Visible = false;
    }

    protected void lbtnEdit_Command(object sender, CommandEventArgs e)
    {
        hdnItemID.Value = e.CommandArgument.ToString();
        if (grdMasterList.Rows.Count > 0) grdMasterList.HeaderRow.TableSection = TableRowSection.TableHeader;

        DataTable dtItem = dl.getLOVMasterlist(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "per id", e.CommandArgument.ToString(),"", "");
        if (dtItem.Rows.Count > 0)
        {
            txtItemCode.Text = dtItem.Rows[0]["ItemCode"].ToString();
            txtEmpNo.Text = dtItem.Rows[0]["ItemCode"].ToString();
            txtDescription.Text = dtItem.Rows[0]["Description"].ToString();
            chkActive.Checked = bool.Parse(dtItem.Rows[0]["Active"].ToString());
            if (ddlCategory.SelectedValue.ToLower() == "sub ctq") ddlMainCTQ.SelectedValue = !string.IsNullOrEmpty(dtItem.Rows[0]["Attribute1"].ToString())? dtItem.Rows[0]["Attribute1"].ToString(): "0";
            if (ddlCategory.SelectedValue.ToLower() == "project category sub") ddlPJCategory.SelectedValue = !string.IsNullOrEmpty(dtItem.Rows[0]["Attribute1"].ToString())? dtItem.Rows[0]["Attribute1"].ToString(): "0";
            else txtDept.Text = !string.IsNullOrEmpty(dtItem.Rows[0]["Attribute1"].ToString())? dtItem.Rows[0]["Attribute1"].ToString(): "0";
            lbtnUpdate.Visible = true;
            lbtnSave.Visible = false;
        }
        uPnlMaintenance.Update();
    }

    protected void lbtnDelete_Command(object sender, CommandEventArgs e)
    {
        AlertMessage.Show("confirmation", "Confirmation:", "Are you sure you want to delete " + e.CommandName.ToString() + "?");
        hdnAction.Value = "delete";
        hdnItemID.Value = e.CommandArgument.ToString();
        if (grdMasterList.Rows.Count > 0) grdMasterList.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    #region MessageBox_Events

    protected override void OnInit(EventArgs e)
    {
        //find the button control within the user control
        LinkButton btnAccept = (LinkButton)AlertMessage.FindControl("lbtnAccept");
        LinkButton btnDecline = (LinkButton)AlertMessage.FindControl("lbtnDecline");
        btnAccept.Click += new EventHandler(btnAccept_Click);
        btnDecline.Click += new EventHandler(btnDecline_Click);
        base.OnInit(e);
    }

    void btnDecline_Click(object sender, EventArgs e)
    {
        hdnAction.Value = "";
        if (grdMasterList.Rows.Count > 0) grdMasterList.HeaderRow.TableSection = TableRowSection.TableHeader;
        uPnlMaintenance.Update();
    }

    void btnAccept_Click(object sender, EventArgs e)
    {
        TextBox txtRemarks = (TextBox)AlertMessage.FindControl("txtRemarks");
        switch (hdnAction.Value.ToLower())
        {
            case "alert":
                hdnAction.Value = "";
                break;
            case "warning":
                hdnAction.Value = "";
                break;
            case "save":
                Save_Transaction("save");
                break;
            case "update":
                Save_Transaction("update");
                break;
            case "delete":
                Save_Transaction("delete");
                break;
            case "refresh":
                Set_Buttons(true);
                Clear_Text();
                Load_Records();
                break;
        }
        if (grdMasterList.Rows.Count > 0) grdMasterList.HeaderRow.TableSection = TableRowSection.TableHeader;
        uPnlMaintenance.Update();
    }

    private void Save_Transaction(string TransType)
    {
        Assign_Values(TransType);
        string result = dl.Insert_LOVMasterList(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), mo, TransType);
        if (result.ToLower() != "request timeout")
        {
            AlertMessage.Show("Information", "Information:", result);
            hdnAction.Value = "refresh";
        }
        else
        {
            AlertMessage.Show("Information", "Information:", "Transaction Failed: Please check input details then try again!");
            hdnAction.Value = "alert";
        }
    }
    private void Assign_Values(string TransType)
    {
        mo.ID = hdnItemID.Value;
        mo.Category = txtCategory.Text.Trim();
       
        switch (txtCategory.Text.Trim().ToLower())
        {
            case "administrator":
            case "requestor":
            case "warehouse leader":
                mo.ItemCode = txtEmpNo.Text.Trim().Replace("'", "`");
                mo.Attribute1 = ddlMainCTQ.SelectedValue;
                break;
            //case "approver1":
            //    mo.ItemCode = txtEmpNo.Text.Trim().Replace("'", "`");
            //    mo.Attribute1 = txtDept.Text;
            //    break;
            //case "approver2":
            //    mo.ItemCode = txtEmpNo.Text.Trim().Replace("'", "`");
            //    mo.Attribute1 = txtDept.Text;
            //    break;
            //case "acknowledged":
            //    mo.ItemCode = txtEmpNo.Text.Trim().Replace("'", "`");
            //    mo.Attribute1 = txtDept.Text;
            //    break;
            //case "project category sub":
            //    mo.ItemCode = txtEmpNo.Text.Trim().Replace("'", "`");
            //    mo.Attribute1 = ddlPJCategory.SelectedValue;
            //    break;
            default:
                mo.ItemCode = txtItemCode.Text.Trim().Replace("'", "`");
                mo.Attribute1 = ddlMainCTQ.SelectedValue;
                break;
        }
        mo.Description = txtDescription.Text.Trim().Replace("'", "`");
        mo.Active = chkActive.Checked.ToString();
        mo.CreatedBy_EmpName = hdnUser_EmpName.Value;
        mo.Attribute2 = "-";
        mo.Attribute3 = "-";
        mo.Attribute4 = "-";
        mo.Attribute5 = "-";
    }

    #endregion

    private Boolean isComplete()
    {
        bool result = true;
        if (txtCategory.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "All fields are required to complete this transaction!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtItemCode.Text.Trim().Length == 0 && txtEmpNo.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "All fields are required to complete this transaction!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (txtDescription.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Information:", "All fields are required to complete this transaction!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (ddlCategory.SelectedValue.ToLower() == "sub ctq" && ddlMainCTQ.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select main ctq to complete transaction!");
            hdnAction.Value = "alert";
            result = false;
        }
        else if (ddlCategory.SelectedValue.ToLower() == "project category sub" && ddlPJCategory.SelectedIndex == 0)
        {
            AlertMessage.Show("warning", "Information:", "Please select project category to complete transaction!");
            hdnAction.Value = "alert";
            result = false;
        }
        if (grdMasterList.Rows.Count > 0) grdMasterList.HeaderRow.TableSection = TableRowSection.TableHeader;
        return result;
    }

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        if (isComplete())
        {
            AlertMessage.Show("confirmation", "Confirmation:", "Are you sure you want to update " + txtItemCode.Text.Trim() + "?");
            hdnAction.Value = "update";
        }
        uPnlMaintenance.Update();
    }

    protected void lbtnSave_Click(object sender, EventArgs e)
    {
        if (isComplete())
        {
            AlertMessage.Show("confirmation", "Confirmation:", "Are you sure you want to save " + txtItemCode.Text.Trim() + "?");
            hdnAction.Value = "save";
        }
        uPnlMaintenance.Update();
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        Set_Buttons(true);
        Clear_Text();
    }

    private void Clear_Text()
    {
        txtItemCode.Text = "";
        txtEmpNo.Text = "";
        txtItemCode.ReadOnly = false;
        txtDescription.Text = "";
        chkActive.Checked = false;
        hdnItemID.Value = null;
        ddlMainCTQ.SelectedIndex = 0;
        txtDept.Text = "";
        if (grdMasterList.Rows.Count > 0) grdMasterList.HeaderRow.TableSection = TableRowSection.TableHeader;
        uPnlMaintenance.Update();
    }

    protected void grdMasterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdMasterList.PageIndex = e.NewPageIndex;
        Load_Records();
    }

    protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
    {
        Load_Records();
    }

    protected void lbtnEmpSearch_Click(object sender, EventArgs e)
    {
        EmpSearch.Show("with username");
    }

    protected void Selected_Employee(string TransType, string EmpNo, string Dept)
    {
        cl_Employee emp = new cl_Employee();
        emp.Select_EmployeeMaster(TransType, EmpNo, Dept);
        
        txtDept.Text = emp.Dept1;
        txtEmpNo.Text = emp.EmpNo;
        txtDescription.Text = emp.EmpName;

        uPnlMaintenance.Update();
    }
}