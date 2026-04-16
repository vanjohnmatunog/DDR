using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for cl_DataLayer
/// </summary>
public class cl_DataLayer : i_DataLayer
{
    cl_DBLayer dbLayer = new cl_DBLayer();

    #region Project Information

    //insert into tbl_DR_DMGTransaction
    public string Insert_DR_DMGTransaction(DbProviderFactory factory, string ConStr, cl_DR_DMGTransactionObject dmg, string transType)
    {
        try
        {
            //return "Request Timeout";
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_DR_DMGTransaction", CommandType.StoredProcedure, fill_DR_DMGTransaction(dmg, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    //update
    public string Update_DR_DMGTransaction(DbProviderFactory factory, string ConStr, cl_DR_DMGTransactionObject dmg, string transType)
    {
        try
        {
            //return "Request Timeout";
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Update_DR_DMGTransaction", CommandType.StoredProcedure, fill_DR_DMGTransaction_update(dmg, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fill_DR_DMGTransaction(cl_DR_DMGTransactionObject dmg, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@RefID", dmg.RefID),
            new SqlParameter("@IssuedBy", dmg.IssuedBy),
            new SqlParameter("@Local", dmg.Local),
            new SqlParameter("@AffectedBU", dmg.AffectedBU),
            new SqlParameter("@NatureOfDiscrepancy", dmg.NatureOfDiscrepancy),
            new SqlParameter("@DamageNote", dmg.DamageNote),
            new SqlParameter("@InvoiceNo", dmg.InvoiceNo),
            new SqlParameter("@ItemCode", dmg.ItemCode),
            new SqlParameter("@Supplier", dmg.Supplier),
            new SqlParameter("@Description", dmg.Description),
            new SqlParameter("@Forwarder", dmg.Forwarder),
            new SqlParameter("@InvoiceQty", dmg.InvoiceQty),
            new SqlParameter("@BlAwhNo", dmg.BlAwhNo),
            new SqlParameter("@QtyAffected", dmg.QtyAffected),
            new SqlParameter("@ReceiveDate", dmg.ReceivedDate),
            new SqlParameter("@NoOfBoxAffected", dmg.NoOfBoxAffected),
            new SqlParameter("@Remarks", dmg.Remarks),
            new SqlParameter("@SendToQA", dmg.SendToQA),
            new SqlParameter("@SendToMPD", dmg.SendToMPD),
            new SqlParameter("@CopyTo", dmg.CopyTo),
            new SqlParameter("@ApprovedBy", dmg.ApprovedBy),
            new SqlParameter("@ApproverName", dmg.ApproverName),
            new SqlParameter("@Attributes1", dmg.Attributes1),
            new SqlParameter("@Attributes2", dmg.Attributes2),
            new SqlParameter("@Attributes3", dmg.Attributes3),
            new SqlParameter("@EmpNoQA", dmg.EmpNoQA),
            new SqlParameter("@EmpNameQA", dmg.EmpNameQA),
            new SqlParameter("@DeptQA", dmg.DeptQA),
            new SqlParameter("@EmpNoMPD", dmg.EmpNoMPD),
            new SqlParameter("@EmpNameMPD", dmg.EmpNameMPD),
            new SqlParameter("@DeptMPD", dmg.DeptMPD),
            new SqlParameter("@EmpNoCopy", dmg.EmpNoCopy),
            new SqlParameter("@EmpNameCopy", dmg.EmpNameCopy),
            new SqlParameter("@DeptCopy", dmg.DeptCopy)


        };
    }
    SqlParameter[] fill_DR_DMGTransaction_update(cl_DR_DMGTransactionObject dmg, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@RefID", dmg.RefID),
            new SqlParameter("@IssuedBy", dmg.IssuedBy),
            new SqlParameter("@Local", dmg.Local),
            new SqlParameter("@AffectedBU", dmg.AffectedBU),
            new SqlParameter("@NatureOfDiscrepancy", dmg.NatureOfDiscrepancy),
            new SqlParameter("@DamageNote", dmg.DamageNote),
            new SqlParameter("@InvoiceNo", dmg.InvoiceNo),
            new SqlParameter("@ItemCode", dmg.ItemCode),
            new SqlParameter("@Supplier", dmg.Supplier),
            new SqlParameter("@Description", dmg.Description),
            new SqlParameter("@Forwarder", dmg.Forwarder),
            new SqlParameter("@InvoiceQty", dmg.InvoiceQty),
            new SqlParameter("@BlAwhNo", dmg.BlAwhNo),
            new SqlParameter("@QtyAffected", dmg.QtyAffected),
            new SqlParameter("@ReceiveDate", dmg.ReceivedDate),
            new SqlParameter("@NoOfBoxAffected", dmg.NoOfBoxAffected),
            new SqlParameter("@Remarks", dmg.Remarks),
            new SqlParameter("@SendToQA", dmg.SendToQA),
            new SqlParameter("@SendToMPD", dmg.SendToMPD),
            new SqlParameter("@CopyTo", dmg.CopyTo),
            new SqlParameter("@ApprovedBy", dmg.ApprovedBy),
            new SqlParameter("@ApproverName", dmg.ApproverName),
            new SqlParameter("@Attributes1", dmg.Attributes1),
            new SqlParameter("@Attributes2", dmg.Attributes2),
            new SqlParameter("@Attributes3", dmg.Attributes3),
            new SqlParameter("@EmpNoQA", dmg.EmpNoQA),
            new SqlParameter("@EmpNameQA", dmg.EmpNameQA),
            new SqlParameter("@DeptQA", dmg.DeptQA),
            new SqlParameter("@EmpNoMPD", dmg.EmpNoMPD),
            new SqlParameter("@EmpNameMPD", dmg.EmpNameMPD),
            new SqlParameter("@DeptMPD", dmg.DeptMPD),
            new SqlParameter("@EmpNoCopy", dmg.EmpNoCopy),
            new SqlParameter("@EmpNameCopy", dmg.EmpNameCopy),
            new SqlParameter("@DeptCopy", dmg.DeptCopy),
            new SqlParameter("@UserName", dmg.UserName),
            new SqlParameter("@isAdmin", dmg.isAdmin),
            new SqlParameter("@isLeader", dmg.isLeader)

        };
    }

    public DataTable get_DR_DMGTransaction(DbProviderFactory factory, string ConStr, string TransType, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Status", ""),
                new SqlParameter("@Keyword", Keyword),
                new SqlParameter("@FromDate", ""),
                new SqlParameter("@ToDate", ""),
                new SqlParameter("@UserEmpNo", "")
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_DR_DMGTransaction", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_DR_DMGTransaction(DbProviderFactory factory, string ConStr, string TransType, string Status, string Keyword, string fromDate, string toDate, string userEmpNo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Status", Status),
                new SqlParameter("@Keyword", Keyword),
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate),
                new SqlParameter("@UserEmpNo", userEmpNo)

            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_DR_DMGTransaction", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_DR_DMGTransaction(DbProviderFactory factory, string ConStr, string TransType, string Status, string Keyword, string userEmpNo)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Status", Status),
                new SqlParameter("@Keyword", Keyword),
                new SqlParameter("@FromDate", ""),
                new SqlParameter("@ToDate", ""),
                new SqlParameter("@UserEmpNo", userEmpNo)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_DR_DMGTransaction", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable get_DR_Remarks(DbProviderFactory factory, string ConStr, string TransType, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType),
                new SqlParameter("@Keyword", Keyword)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_DR_Remarks", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public DataTable get_DR_Link(DbProviderFactory factory, string ConStr, string TransType)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", TransType)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_DR_Link", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }

    }
    public DataTable get_DR_HistoryLogs(DbProviderFactory factory, string ConStr, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@RefID", Keyword)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_DR_DMGHistoryLogs", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
    public string Approve_DR_Request(DbProviderFactory factory, string ConStr, string TransType, string RefID, string Remarks, string CreatedBy)
    {
        SqlParameter[] parms = new SqlParameter[]
        {
            new SqlParameter("@TransType", TransType),
            new SqlParameter("@RefID", RefID),
            new SqlParameter("@Remarks", Remarks),
            new SqlParameter("@CreatedBy", CreatedBy)
        };
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Approve_DR_Request", CommandType.StoredProcedure, parms).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    public string Submit_BU_Remarks(DbProviderFactory factory, string ConStr, cl_DR_BURemarksObject bu,string TransType, string RefID, string Remarks)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Submit_BU_Remarks", CommandType.StoredProcedure, fill_BU_Remarks(bu,TransType, RefID, Remarks)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fill_BU_Remarks(cl_DR_BURemarksObject bu, string TransType, string RefID, string Remarks)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", TransType),
            new SqlParameter("@RefID", RefID),
            new SqlParameter("@Remarks", Remarks),
            new SqlParameter("@Disposition",bu.Disposition),
            new SqlParameter("@Comment", bu.Comment)
            {

            }
        };
    }
    public string Submit_FinalDisposition(DbProviderFactory factory, string ConStr, cl_DR_FinalDispositionObject fnl, string TransType, string RefID, string userEmpName, string Remarks)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Submit_FinalDisposition", CommandType.StoredProcedure, fill_FinalDisposition(fnl, TransType, RefID, userEmpName, Remarks)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fill_FinalDisposition(cl_DR_FinalDispositionObject fnl, string TransType, string RefID, string userEmpName, string Remarks)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", TransType),
            new SqlParameter("@RefID", RefID),
            new SqlParameter("@Remarks", Remarks),
            new SqlParameter("@EmpName",userEmpName),
            new SqlParameter("@Disposition",fnl.Disposition),
            new SqlParameter("@Comment", fnl.Comment)
            {

            }
        };
    }
    #endregion

    #region Maintenance
    public string Insert_LOVMasterList(DbProviderFactory factory, string ConStr, cl_MaintenanceObject mo, string transType)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_LOV_Masterlist", CommandType.StoredProcedure, fillLOVMasterList(mo, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    SqlParameter[] fillLOVMasterList(cl_MaintenanceObject mo, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType", transType),
            new SqlParameter("@ID", mo.ID),
            new SqlParameter("@Category", mo.Category),
            new SqlParameter("@ItemCode", mo.ItemCode),
            new SqlParameter("@Description", mo.Description),
            new SqlParameter("@Active", mo.Active),
            new SqlParameter("@CreatedBy", mo.CreatedBy_EmpName),
            new SqlParameter("@Attribute1", mo.Attribute1),
            new SqlParameter("@Attribute2", mo.Attribute2),
            new SqlParameter("@Attribute3", mo.Attribute3),
            new SqlParameter("@Attribute4", mo.Attribute4),
            new SqlParameter("@Attribute5", mo.Attribute5)
        };
    }
    
    public DataTable getLOVMasterlist(DbProviderFactory factory, string ConStr, string transType, string Category, string ItemCode, string Dept)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@Category", Category),
                new SqlParameter("@ItemCode", ItemCode),
                new SqlParameter("@Attribute1", Dept)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_LOV_Masterlist", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public bool Check_UserAccess(DbProviderFactory factory, string ConStr, string transType, string Category, string ItemCode, string Param, string empDept)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@Category", Category),
                new SqlParameter("@ItemCode", ItemCode),
                new SqlParameter("@Attribute1", Param),
                new SqlParameter("@EmpDept", empDept)
            };
        try
        {
            return Convert.ToBoolean(dbLayer.Scalar(factory, ConStr, "dbo.sp_Select_LOV_Masterlist", CommandType.StoredProcedure, parms));
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    #endregion


    #region Attachment

    public DataTable get_DR_Attachment(DbProviderFactory factory, string ConStr, string transType, string Keyword)
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@TransType", transType),
                new SqlParameter("@Keyword", Keyword)
            };
        try
        {
            return dbLayer.getDataTable(factory, ConStr, "dbo.sp_Select_DR_Attachment", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public string Insert_DR_Attachment(DbProviderFactory factory, string ConStr, cl_DataTransferObject dto, string transType)
    {
        try
        {
            return dbLayer.Scalar(factory, ConStr, "dbo.sp_Insert_DR_Attachment", CommandType.StoredProcedure, fillDRAttachment(dto, transType)).ToString();
        }
        catch (Exception ex)
        {
            return "Request Timeout";
        }
    }
    

    SqlParameter[] fillDRAttachment(cl_DataTransferObject dto, string transType)
    {
        return new SqlParameter[]
        {
            new SqlParameter("@TransType",  transType),
            new SqlParameter("@ID",  dto.ID),
            new SqlParameter("@RefID",  dto.RefID),
            new SqlParameter("@FileName_Orig",  dto.FileName_Orig),
            new SqlParameter("@FileName_New",  dto.FileName_New),
            new SqlParameter("@FilePath",  dto.FilePath),
            new SqlParameter("@ActionRemarks",  dto.ActionRemarks),
            new SqlParameter("@CreatedBy_EmpNo",  dto.CreatedBy_EmpNo),
            new SqlParameter("@CreatedBy_EmpName",  dto.CreatedBy_EmpName),
            new SqlParameter("@Attribute1",  dto.Attribute1),
            new SqlParameter("@Attribute2",  dto.Attribute2)
        };
    }
    #endregion







}