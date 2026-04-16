using System;
using System.Data;
using System.Drawing;
using System.Web.UI;

public partial class PortalAdmin_SystemInfo : System.Web.UI.Page
{
    i_SystemInfo sysInfo = new cl_SystemInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        if (!Page.IsPostBack)
        {
            DataTable dtSysInfo = new DataTable();
            dtSysInfo = sysInfo.getSystemInformation();
            if (dtSysInfo.Rows.Count > 0)
            {
                txtPrimaryOwner.Text = dtSysInfo.Rows[0]["PrimaryOwner"].ToString();
                txtSubOwner.Text = dtSysInfo.Rows[0]["SecondaryOwner"].ToString();
                txtDepartment.Text = dtSysInfo.Rows[0]["Department"].ToString();
                txtSection.Text = dtSysInfo.Rows[0]["Section"].ToString();
                txtLocalNo.Text = dtSysInfo.Rows[0]["PLocalNo"].ToString();
                txtAppID.Text = dtSysInfo.Rows[0]["SystemID"].ToString();
                txtSysName.Text = dtSysInfo.Rows[0]["SystemName"].ToString();
                txtDBName.Text = dtSysInfo.Rows[0]["DBName"].ToString();
                txtDBServer.Text = dtSysInfo.Rows[0]["DBServer"].ToString();
                txtAppServer.Text = dtSysInfo.Rows[0]["ServerName"].ToString();
                txtSupport.Text = dtSysInfo.Rows[0]["SupportPIC"].ToString();
            }

            txtPrimaryOwner.ReadOnly = true;
            txtPrimaryOwner.BackColor = Color.White;
            txtSubOwner.ReadOnly = true;
            txtSubOwner.BackColor = Color.White;
            txtDepartment.ReadOnly = true;
            txtDepartment.BackColor = Color.White;
            txtLocalNo.ReadOnly = true;
            txtLocalNo.BackColor = Color.White;
            txtAppID.ReadOnly = true;
            txtAppID.BackColor = Color.White;
            txtSysName.ReadOnly = true;
            txtSysName.BackColor = Color.White;
            txtDBName.ReadOnly = true;
            txtDBName.BackColor = Color.White;
            txtDBName.TextMode = System.Web.UI.WebControls.TextBoxMode.Password;
            txtDBServer.ReadOnly = true;
            txtDBServer.BackColor = Color.White;
            txtDBServer.TextMode = System.Web.UI.WebControls.TextBoxMode.Password;
            txtAppServer.ReadOnly = true;
            txtAppServer.BackColor = Color.White;
            txtAppServer.TextMode = System.Web.UI.WebControls.TextBoxMode.Password;
            txtSupport.ReadOnly = true;
            txtSupport.BackColor = Color.White;

        }

    }
}