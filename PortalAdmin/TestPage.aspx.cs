using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PortalAdmin_TestPage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Load_Result();
        }
        BootstrapCollapsExpand();
    }

    private void Load_Result()
    {
        DataTable dt = new DataTable();
        dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id"), new DataColumn("Name"), new DataColumn("Country") });
        dt.Rows.Add(1, "John Hammond", "United States");
        dt.Rows.Add(2, "Mudassar Khan", "India");
        dt.Rows.Add(3, "Suzanne Mathews", "France");
        dt.Rows.Add(4, "Robert Schidner", "Russia");
        grdTest.DataSource = dt;
        grdTest.DataBind();

        BootstrapCollapsExpand();
        UpdatePanel1.Update();
    }

    private void BootstrapCollapsExpand()
    {
        if (this.grdTest.Rows.Count > 0)
        {
            //Attribute to show the Plus Minus Button.
            grdTest.HeaderRow.Cells[1].Attributes["data-class"] = "expand";

            //Attribute to hide column in Phone.
            grdTest.HeaderRow.Cells[2].Attributes["data-hide"] = "phone";
            grdTest.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            //GridView1.HeaderRow.Cells[3].Attributes["data-hide"] = "phone";
            //GridView1.HeaderRow.Cells[4].Attributes["data-hide"] = "expand";
            //GridView1.HeaderRow.Cells[5].Attributes["data-hide"] = "expand";
            //Adds THEAD and TBODY to GridView.
            grdTest.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    protected void grdTest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTest.PageIndex = e.NewPageIndex;
        Load_Result();
        BootstrapCollapsExpand();
    }

    #region MessageBox_Events

    protected override void OnInit(EventArgs e)
    {
       
        //LinkButton btnAccept = (LinkButton)AlertMessage.FindControl("lbtnAccept");
        //LinkButton btnDecline = (LinkButton)AlertMessage.FindControl("lbtnDecline");
        //btnAccept.Click += new EventHandler(btnAccept_Click);
        //btnDecline.Click += new EventHandler(btnDecline_Click);
        base.OnInit(e);
    }

    void btnDecline_Click(object sender, EventArgs e)
    {
        //hdnAction.Value = "";
        //uPnlMain.Update();
    }

    void btnAccept_Click(object sender, EventArgs e)
    {
        //TextBox msgRemarks = (TextBox)AlertMessage.FindControl("txtRemarks");
        //switch (hdnAction.Value.ToLower())
        //{
        //    case "alert":
        //        hdnAction.Value = "";
        //        break;
        //    case "save":
        //        AlertMessage.Show("information", "Information", "Yehey successful!");
        //        hdnAction.Value = "alert";
        //        break;
        //}
        //uPnlMain.Update();
    }
    #endregion

    protected void lbtnSearchEmp_Click(object sender, EventArgs e)
    {
        EmpSearch.Show("all employee");
        hdnAction.Value = "employee search";
    }

    protected void Selected_Employee(string TransType, string EmpNo, string EmpName)
    {
        txtEmpNo.Text = EmpNo;
        txtEmpName.Text = EmpName;
        uPnlMain.Update();
    }
}