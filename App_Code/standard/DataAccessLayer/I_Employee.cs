using System.Data;
using System.Data.Common;

/// <summary>
/// Summary description for I_Employee
/// </summary>
public interface i_Employee
{
    DataTable Select_EmployeeMaster(string transType, string Keyword);
    DataTable Select_EmployeeOrGroupEmail(string transType, string Keyword);
    void Select_EmployeeMaster_Email(string transType, string Keyword, string Email);
    void Select_EmployeeMaster_Email_CopyTo(string transType, string Keyword, string Email);
    void Select_EmployeeMaster(string transType, string Keyword, string DepCode);
    
}