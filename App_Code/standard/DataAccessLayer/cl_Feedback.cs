using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for cl_Feedback
/// </summary>
public class cl_Feedback : i_Feedback
{
    cl_DBLayer dbLayer = new cl_DBLayer();
    public string Insert_Comment(DbProviderFactory factory, string ConStr, cl_FeedbackObjects fbo, string transType)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_InsertSystemFeedback", CommandType.StoredProcedure, fillCommentParameter(fbo, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }

    SqlParameter[] fillCommentParameter(cl_FeedbackObjects fbo, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@transtype", transType),
            new SqlParameter("@SystemID", cl_Utilities.ApplicationID()),
            new SqlParameter("@SystemRating", fbo.SystemRating),
            new SqlParameter("@Comment", fbo.Comment),
            new SqlParameter("@EmpNo", fbo.EmpNo),
            new SqlParameter("@UserAccount", cl_Identity.Get_UserID())
        };
    }
}