using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for cl_DBLayer
/// </summary>
public class cl_DBLayer
{
    DbConnection CreateConnection(DbProviderFactory factory, string ConStr)
    {
        var connection = factory.CreateConnection();
        connection.ConnectionString = ConStr;
        connection.Open();
        return connection;
    }

    DbCommand CreateCommand(DbProviderFactory factory, DbConnection connection, string sql, CommandType commandtype, params object[] parms)
    {
        var command = factory.CreateCommand();
        command.Connection = connection;
        command.CommandText = sql;
        command.CommandType = commandtype;
        command.Parameters.AddRange(parms);
        command.CommandTimeout = 0;
        return command;
    }

    DbCommand CreateCommand(DbProviderFactory factory, DbConnection connection, string sql, CommandType commandtype)
    {
        var command = factory.CreateCommand();
        command.Connection = connection;
        command.CommandText = sql;
        command.CommandType = commandtype;
        command.CommandTimeout = 0;
        return command;
    }

    DbDataAdapter CreateAdapter(DbProviderFactory factory, DbCommand command)
    {
        var adapter = factory.CreateDataAdapter();
        adapter.SelectCommand = command;
        return adapter;
    }

    public object Scalar(DbProviderFactory factory, string ConStr, string sql, CommandType commandtype, params object[] parms)
    {
        using (var connection = CreateConnection(factory, ConStr))
        {
            using (var command = CreateCommand(factory, connection, sql, commandtype, parms))
            {
                return command.ExecuteScalar();
            }
        }
    }

    public DataTable getDataTable(DbProviderFactory factory, string ConStr, string sql, CommandType commandtype, params object[] parms)
    {
        using (var connection = CreateConnection(factory, ConStr))
        {
            using (var command = CreateCommand(factory, connection, sql, commandtype, parms))
            {
                using (var adapter = CreateAdapter(factory, command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }

    public DataTable getDataTable(DbProviderFactory factory, string ConStr, string sql, CommandType commandtype)
    {
        using (var connection = CreateConnection(factory, ConStr))
        {
            using (var command = CreateCommand(factory, connection, sql, commandtype))
            {
                using (var adapter = CreateAdapter(factory, command))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    return dt;
                }
            }
        }
    }
}