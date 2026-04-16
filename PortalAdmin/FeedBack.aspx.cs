using System;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PortalAdmin_FeedBack : System.Web.UI.Page
{
    i_Feedback fb = new cl_Feedback();
    cl_FeedbackObjects fbo = new cl_FeedbackObjects();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }
    }


    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        if (rdbRatings.SelectedIndex == -1)
        {
            AlertMessage.Show("warning", "Alert:", "Please select ratings.");
            hdnAction.Value = "warning";
        }
        else if (txtComment.Text.Trim().Length == 0)
        {
            AlertMessage.Show("warning", "Alert:", "Please input your comments or suggestions.");
            hdnAction.Value = "warning";
        }
        else
        {
            AlertMessage.Show("confirmation", "Confirmation:", "Are you sure you want to submit this comment/suggestion?");
            hdnAction.Value = "confirm";
        }
        uPnlMain.Update();
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
        uPnlMain.Update();
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
            case "confirm":
                Submit_Feedback();
                break;
        }
        uPnlMain.Update();
    }

    private void Submit_Feedback()
    {
        fbo.SystemRating = rdbRatings.SelectedValue;
        fbo.Comment = txtComment.Text.Trim();
        fbo.EmpNo = txtEmpNo.Text.Trim();
        if (fb.Insert_Comment(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLSP(), fbo, "insert").ToLower() != "request timeout")
        {
            AlertMessage.Show("Information", "Information:", "Transaction Successful! Thank you for sharing your comment/suggestion.");
            hdnAction.Value = "alert";
            txtEmpNo.Text = "";
            rdbRatings.ClearSelection();
            txtComment.Text = "";
            uPnlMain.Update();
        }
        else
        {
            AlertMessage.Show("Information", "Information:", "Request Timeout! Please try again.");
            hdnAction.Value = "alert";
        }
        uPnlMain.Update();
    }
    #endregion
}