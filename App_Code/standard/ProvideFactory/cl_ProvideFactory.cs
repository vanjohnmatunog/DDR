using System.Data.Common;

/// <summary>
/// Summary description for cl_ProvideFactory
/// </summary>
public class cl_ProvideFactory
{
    public static DbProviderFactory getSqlFactory()
    {
        return DbProviderFactories.GetFactory("System.Data.SqlClient");
    }

    public static DbProviderFactory getOracleFactory()
    {
        return DbProviderFactories.GetFactory("System.Data.OracleClient");
    }

    public static DbProviderFactory getMySqlFactory()
    {
        return DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
    }
}