using System;

public partial class MessageBox_AlertMessage : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //
        }
    }

    public void Show(string transType, string _title, string _msg)
    {
        lblRemarks.ForeColor = System.Drawing.Color.Black;
        txtRemarks.Text = "";
        lblTitle.Text = _title.ToUpper();
        lblMessage.Text = _msg;
        switch (transType.ToLower())
        {
            case "information":
                lblImage.CssClass = "fa fa-info-circle fa-3x text-success";
                lbtnAccept.Text = "<i class='fa fa-thumbs-up'> </i> OK ";
                lbtnDecline.Visible = false;
                lblRemarks.Visible = false;
                txtRemarks.Visible = false;
                break;
            case "warning":
                lblImage.CssClass = "fa fa-exclamation-circle fa-3x text-warning";
                lbtnAccept.Text = "<i class='fa fa-thumbs-up'> </i> OK ";
                lbtnDecline.Visible = false;
                lblRemarks.Visible = false;
                txtRemarks.Visible = false;
                break;
            case "confirmation":
                lblImage.CssClass = "fa fa-question-circle fa-3x text-primary";
                lbtnAccept.Text = "<i class='fa fa-thumbs-up'> </i> YES ";
                lblRemarks.Visible = false;
                txtRemarks.Visible = false;
                lbtnDecline.Visible = !false;
                break;
            case "confirmationwithremarks":
                lblImage.CssClass = "fa fa-question-circle fa-3x text-primary";
                lbtnAccept.Text = "<i class='fa fa-thumbs-up'> </i> YES ";
                lblRemarks.Visible = !false;
                txtRemarks.Visible = !false;
                lbtnDecline.Visible = !false;
                break;
            default:
                lblImage.CssClass = "fa fa-info-circle fa-3x text-success";
                lbtnAccept.Text = "<i class='fa fa-thumbs-up'> </i> YES ";
                lbtnDecline.Visible = false;
                lblRemarks.Visible = false;
                txtRemarks.Visible = false;
                break;
        }
        uPnlMain.Update();
        mpMsgBox.Show();
    }        
    protected void lbtnDecline_Click(object sender, EventArgs e)
    {
        mpMsgBox.Hide();
        this.Dispose();
    }


    protected void lbtnAccept_Click(object sender, EventArgs e)
    {
        mpMsgBox.Hide();
        this.Dispose();
    }
}