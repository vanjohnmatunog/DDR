using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for i_DataLayer
/// </summary>
public interface i_DataLayer
{
    #region Project Information

    //insert into tbl_DR_DMGTransaction
    string Insert_DR_DMGTransaction(DbProviderFactory factory, string ConStr, cl_DR_DMGTransactionObject dmg, string transType);
    string Update_DR_DMGTransaction(DbProviderFactory factory, string ConStr, cl_DR_DMGTransactionObject dmg, string transType);
    DataTable get_DR_DMGTransaction(DbProviderFactory factory, string ConStr, string TransType, string Keyword);
    DataTable get_DR_DMGTransaction(DbProviderFactory factory, string ConStr, string TransType, string Status, string Keyword, string fromDate, string toDate, string userEmpNo);
    DataTable get_DR_DMGTransaction(DbProviderFactory factory, string ConStr, string TransType, string Status, string Keyword, string userEmpNo);
    DataTable get_DR_Remarks(DbProviderFactory factory, string ConStr, string TransType, string Keyword);
    DataTable get_DR_Link(DbProviderFactory factory, string ConStr, string TransType);
    string Approve_DR_Request(DbProviderFactory factory, string ConStr, string TransType, string RefID, string Remarks, string CreatedBy);
    string Submit_BU_Remarks(DbProviderFactory factory, string ConStr, cl_DR_BURemarksObject bu, string TransType, string RefID, string Remarks);
    string Submit_FinalDisposition(DbProviderFactory factory, string ConStr, cl_DR_FinalDispositionObject fnl, string TransType, string RefID, string userEmpName, string Remarks);
    DataTable get_DR_HistoryLogs(DbProviderFactory factory, string ConStr, string Keyword);

    #endregion

    #region Maintenance
    DataTable getLOVMasterlist(DbProviderFactory factory, string ConStr, string transType, string Category, string ItemCode, string Dept);
    string Insert_LOVMasterList(DbProviderFactory factory, string ConStr, cl_MaintenanceObject mo, string transType);
    bool Check_UserAccess(DbProviderFactory factory, string ConStr, string transType, string Category, string ItemCode, string Param, string empDept);



    #endregion


    #region Attachment
    DataTable get_DR_Attachment(DbProviderFactory factory, string ConStr, string transType, string Keyword);
    string Insert_DR_Attachment(DbProviderFactory factory, string ConStr, cl_DataTransferObject dto, string transType);
    #endregion


}