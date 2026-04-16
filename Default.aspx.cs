using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TIPIS3WebAdmin.TIPEmployeeMaster;

public partial class _Default : System.Web.UI.Page
{
    i_DataLayer dl = new cl_DataLayer();
    private static DataTable dtImages;

    private static Boolean isAdmin = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            Details.Portal.EMP emp = new Details.Portal.EMP(cl_DBConn.MSSQLEmp(), cl_Identity.Get_UserID());
            if (emp.HasRows)
            {
                isAdmin = (dl.Check_UserAccess(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLTrans(), "is admin", "Administrator", emp.EmpNo, "",""));
                if (!isAdmin) ScriptManager.RegisterStartupScript(this, GetType(), "Javascript", "MenuHide('five-ddheader');", true);
            }
            else
            {
                Response.Redirect("~/PortalAdmin/Unauthorized.aspx");
            }
            emp.Dispose();
        }
    }

    private static void Initialize_DataTable()
    {
        dtImages = new DataTable();
        dtImages.Columns.Add("Title", typeof(string));
        dtImages.Columns.Add("Description", typeof(string));
        dtImages.Columns.Add("urlLink", typeof(string));
        dtImages.Columns.Add("ImagePath", typeof(string));

        DataRow dr = dtImages.NewRow();

        dr["Title"] = "Images 1";
        dr["Description"] = "Test Only - Description";
        dr["urlLink"] = "http://www.google.com.ph";
        dr["ImagePath"] = "/Media/Images/Slider/home1.jpg";

        dtImages.Rows.Add(dr);
        dtImages.AcceptChanges();
    }
}