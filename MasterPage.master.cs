using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        //Load Page Details from App Settings (To change the value to Web.Config)
        lblmastertitle.Text = cl_Utilities.SystemName();
        //imgIS3Logo.ImageUrl = cl_Utilities.TIPIS3Logo();
    }
    protected void Page_Load(object sender, EventArgs e)
    {        
        Page.MaintainScrollPositionOnPostBack = true;
        if (!Page.IsPostBack)
        {
            //Load Employee Details
            Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            if (emp.HasRows)
            {
                lblUser.Text = "  " + emp.EmpName;
            }
            else
            {
                Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
            }
            emp.Dispose();
        }
    }

    protected void lbtnFeedBack_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/PortalAdmin/Feedback.aspx");
        Response.Redirect("~/Default.aspx");
    }

    protected void lbtnSystemInfo_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/PortalAdmin/SystemInfo.aspx");
        Response.Redirect("~/Default.aspx");
    }

    protected void lbtnCreator_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/PortalAdmin/Creator.aspx");
    }

    protected void lbtnSearch_Click(object sender, EventArgs e)
    {
        if (txtSearch.Text.Trim().Length > 0) Response.Redirect("~/PageView/ViewRecords.aspx?user=all&Keyword=" + txtSearch.Text.Trim());
        else Response.Redirect("~/PageView/ViewRecords.aspx?user=all&Keyword=");
    }
}
