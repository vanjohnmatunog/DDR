using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

public partial class EmailSearch_CopyTo_EmailSearch_CopyTo : System.Web.UI.UserControl
{
    public delegate void EmployeeSelectedHandler(string TransType, string EmpNo, string EmpEmail);
    i_Employee emp = new cl_Employee();
    public event EmployeeSelectedHandler EmployeeSelected;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
        }
    }

    public void Show(string TransType)
    {
        txtKeyword.Text = "";
        hdnTransType.Value = TransType;
        Search_Employee();
        mpEmpSearch.Show();
        //Session["SessionEmp"] = null;
    }

    private void Search_Employee()
    {
        string Keyword = "xxxx";
        if (txtKeyword.Text.Trim().Length > 0) Keyword = txtKeyword.Text.Trim().Replace(" ", "%");
        DataTable dtEmp = new DataTable();
        dtEmp = emp.Select_EmployeeOrGroupEmail(hdnTransType.Value, Keyword);
        grdEmployee.DataSource = dtEmp;
        grdEmployee.DataBind();
        BootstrapCollapsExpand();


        uPnlMain.Update();
    }

    private void BootstrapCollapsExpand()
    {
        if (this.grdEmployee.Rows.Count > 0)
        {
            //Attribute to show the Plus Minus Button.
            grdEmployee.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            //grdEmployee.HeaderRow.Cells[0].Attributes["data-hide"] = "phone";
            grdEmployee.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            //GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            //GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "expand";
            //GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "expand";
            //Adds THEAD and TBODY to GridView.
            grdEmployee.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        Search_Employee();
        mpEmpSearch.Show();
    }

    protected void grdEmployee_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEmployee.PageIndex = e.NewPageIndex;
        Search_Employee();
        uPnlMain.Update();
        mpEmpSearch.Show();
    }
    protected void btnSelect_Command(object sender, CommandEventArgs e)
    {
        EmployeeSelected(hdnTransType.Value, e.CommandArgument.ToString(), e.CommandName.ToString());

        mpEmpSearch.Hide();
        this.Dispose();
    }


    protected void lbntCancel_Click(object sender, EventArgs e)
    {
        mpEmpSearch.Hide();
        this.Dispose();
    }

}