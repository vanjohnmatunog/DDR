using System.Data.Common;

/// <summary>
/// Summary description for i_Feedback
/// </summary>
public interface i_Feedback
{
    string Insert_Comment(DbProviderFactory factory, string ConStr, cl_FeedbackObjects fbo, string transType);
}