using System.Configuration;

/// <summary>
/// Summary description for cl_DBConn
/// </summary>
public class cl_DBConn
{
    //Standard  - Do Not Alter the Keywords or method name
    public static string MSSQLTrans()
    {
        return ConfigurationManager.ConnectionStrings["MSSQLTrans"].ToString();
    }

    public static string MSSQLEmp()
    {
        return ConfigurationManager.ConnectionStrings["MSSQLEmp"].ToString();
    }

    public static string ORAPRAS()
    {
        return ConfigurationManager.ConnectionStrings["ORAPRAS"].ToString();
    }

    public static string ORATKS()
    {
        return ConfigurationManager.ConnectionStrings["ORATKS"].ToString();
    }

    public static string ORACBS()
    {
        return ConfigurationManager.ConnectionStrings["ORACBS"].ToString();
    }
    public static string MSSQLSP()
    {
        return ConfigurationManager.ConnectionStrings["IS3SysyemProfile"].ToString();
    }
    //Add additional method - add below
    public static int FileSize()
    {
        return int.Parse(ConfigurationManager.AppSettings["FileSize"].ToString());
    }
}