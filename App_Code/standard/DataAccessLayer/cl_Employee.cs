using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using TIPIS3WebAdmin.TIPEmployeeMaster;

/// <summary>
/// Summary description for cl_Employee
/// </summary>
public class cl_Employee : i_Employee
{
    public string EmpNo { get; set; }
    public string EmpName { get; set; }
    public string DepCode { get; set; }
    public string Dept1 { get; set; }
    public string Department { get; set; }
    public string EmpEmail { get; set; }
    public string EmailDomain { get; set; }
    public string Email1 { get; set; }


    cl_DBLayer dbLayer = new cl_DBLayer();
    public DataTable Select_EmployeeMaster(string transType, string Keyword)
    {
        string sqlQuery = "SELECT TOP 10 EmpNo, FirstEmpName, Department, Dept, EmpEmail + '@' + EmailDomain AS Email FROM [dbo].[VW_WEB_PORTAL_EMP] ";
        if (transType.ToLower() == "all employee" || transType.ToLower() == "all employee pj leader") sqlQuery = "SELECT TOP 10 EmpNo, FirstEmpName, Department, Dept, DepCode, EmpEmail, EmailDomain FROM [dbo].[VW_WEB_ALL_EMP] ";
        sqlQuery += "WHERE  EmpNo + FirstEmpName LIKE '%" + Keyword + "%' ORDER BY FirstEmpName";

        CustomQuery cQuery = new CustomQuery(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        try
        {
            cQuery.Fill(dt, sqlQuery);
        }
        catch (Exception ex)
        {
            //
        }
        return dt;
    }
    public DataTable Select_EmployeeOrGroupEmail(string transType, string Keyword)
    {
        string sqlQuery = "SELECT TOP 10 EmpNo, FirstEmpName, Department, [EmpEmail]+'@'+[EmailDomain] AS Email FROM [EMPLOYEE_MASTER].[dbo].[VW_WEB_ALL_EMP] WHERE EmpName LIKE '%" + Keyword + "%' AND BU = '1' AND UserID != ''" +
                          "UNION " +
                          "SELECT TOP 10 CONVERT(varchar(50), [GroupID]), [GroupName], [Site], [GroupEmail] + '@' +[EmailDomain] AS Email FROM [EMPLOYEE_MASTER].[dbo].[VW_WEB_PORTAL_GROUPEMAIL] WHERE GroupName LIKE '%" + Keyword + "%'";

        CustomQuery cQuery = new CustomQuery(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        try
        {
            cQuery.Fill(dt, sqlQuery);
        }
        catch (Exception ex)
        {
            //
        }
        return dt;
    }
    public void Select_EmployeeMaster_Email(string transType, string Keyword, string Email)
    {
        string sqlQuery = "SELECT TOP 1 EmpNo, FirstEmpName, Department, Dept, DepCode, EmpEmail + '@' + EmailDomain AS Email FROM [dbo].[VW_WEB_PORTAL_EMP]" +
                          " WHERE  EmpNo + FirstEmpName LIKE '%" + Keyword + "%'  AND BU = 1 AND EmpName NOT LIKE '%Common%' ORDER BY FirstEmpName";
      

        CustomQuery cQuery = new CustomQuery(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        try
        {
            cQuery.Fill(dt, sqlQuery);

            EmpNo = dt.Rows[0]["EmpNo"].ToString();
            EmpName = dt.Rows[0]["FirstEmpName"].ToString();
            Department = dt.Rows[0]["Department"].ToString();
            //Dept1 = dt.Rows[0]["Dept"].ToString();
            //DepCode = dt.Rows[0]["DepCode"].ToString();
            //EmpEmail = dt.Rows[0]["EmpEmail"].ToString();
            Email1 = dt.Rows[0]["Email"].ToString();
           // EmailDomain = dt.Rows[0]["EmailDomain"].ToString();
        }
        catch (Exception ex)
        {
            //
        }

    }
    public void Select_EmployeeMaster_Email_CopyTo(string transType, string Keyword, string Email)
    {
        string sqlQuery = "SELECT TOP 1 EmpNo, FirstEmpName, Department, [EmpEmail]+'@'+[EmailDomain] AS Email FROM [EMPLOYEE_MASTER].[dbo].[VW_WEB_ALL_EMP] WHERE [EmpEmail]+'@'+[EmailDomain] LIKE '%" + Keyword + "%' AND BU = '1' AND UserID != '' " +
                          "UNION " +
                          "SELECT TOP 1 CONVERT(varchar(50), [GroupID]), [GroupName], [Site], [GroupEmail] + '@' +[EmailDomain] AS Email FROM [EMPLOYEE_MASTER].[dbo].[VW_WEB_PORTAL_GROUPEMAIL] WHERE [GroupEmail] + '@' +[EmailDomain] LIKE '%" + Keyword + "%'";


        CustomQuery cQuery = new CustomQuery(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        try
        {
            cQuery.Fill(dt, sqlQuery);

            EmpNo = dt.Rows[0]["EmpNo"].ToString();
            EmpName = dt.Rows[0]["FirstEmpName"].ToString();
            Department = dt.Rows[0]["Department"].ToString();
            //Dept1 = dt.Rows[0]["Dept"].ToString();
            //DepCode = dt.Rows[0]["DepCode"].ToString();
            //EmpEmail = dt.Rows[0]["EmpEmail"].ToString();
            Email1 = dt.Rows[0]["Email"].ToString();
            // EmailDomain = dt.Rows[0]["EmailDomain"].ToString();
        }
        catch (Exception ex)
        {
            //
        }

    }

    public void Select_EmployeeMaster(string transType, string Keyword, string Dept)
    {
        string sqlQuery = "SELECT TOP 1 EmpNo, FirstEmpName, Department, Dept, DepCode FROM [dbo].[VW_WEB_PORTAL_EMP] ";
        if (transType.ToLower() == "all employee" || transType.ToLower() == "all employee pj leader") sqlQuery = "SELECT TOP 10 EmpNo, FirstEmpName, Department, Dept, DepCode FROM [dbo].[VW_WEB_ALL_EMP] ";
        sqlQuery += "WHERE  EmpNo + FirstEmpName LIKE '%" + Keyword + "%' AND Dept = '" + Dept + "' ORDER BY FirstEmpName";

        
        CustomQuery cQuery = new CustomQuery(cl_DBConn.MSSQLEmp());
        DataTable dt = new DataTable();
        try
        {
            cQuery.Fill(dt, sqlQuery);

            EmpNo = dt.Rows[0]["EmpNo"].ToString();
            EmpName = dt.Rows[0]["FirstEmpName"].ToString();
            Department = dt.Rows[0]["Department"].ToString();
            Dept1 = dt.Rows[0]["Dept"].ToString();
            DepCode = dt.Rows[0]["DepCode"].ToString();
        }
        catch (Exception ex)
        {
            //
        }

    }
}