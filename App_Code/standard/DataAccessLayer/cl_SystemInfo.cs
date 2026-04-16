using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

/// <summary>
/// Summary description for cl_SystemInfo
/// </summary>
public class cl_SystemInfo : i_SystemInfo
{
    cl_DBLayer dbLayer = new cl_DBLayer();
    public DataTable getSystemInformation()
    {
        SqlParameter[] parms = new SqlParameter[]
            {
                new SqlParameter("@Param", "Display Sys Info"),
                new SqlParameter("@SystemID", cl_Utilities.ApplicationID())
            };
        try
        {
            return dbLayer.getDataTable(cl_ProvideFactory.getSqlFactory(), cl_DBConn.MSSQLSP(), "dbo.sp_SystemInfo_Master_Temp", CommandType.StoredProcedure, parms);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}