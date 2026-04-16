using System;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for cl_Identity
/// </summary>
public class cl_Identity
{
    //Standard  - Do Not Alter the Keywords or method name
    public static string Get_UserID()
    {
        string UserName = string.Empty;

        // ================================
        UserName = TIPIS3WebAdmin.TIPUtilities.Identity.Website.Get_UserID();
        //Administrator
        //UserName = "z8316tip"; // Sir Van
        //UserName = "z7857tip"; //Warren
        //UserName = "z7832tip"; //Sir Jims
        //UserName = "z8226tip"; // Sir Neil
        //UserName = "z0879tip"; // Sir Joselito

        //Approver
        //UserName = "z0544tip"; // Sir R. Pascual
        //UserName = "z2244tip"; // Sir R. Rosuelo
        //UserName = "z1001tip"; // Sir A. Pesino
        //UserName = "z1872tip"; // Sir shed

        //Requestor
        //UserName = "z0545tip"; // Sir Pedro
        //UserName = "z7836tip"; // Sir Jaymar
        //UserName = "z8436tip"; // Sir Lester
        //UserName = "z7738tip"; // maam mary anne

        //SQA
        //UserName = "z3781tip"; // Maam Liezel
        //UserName = "z7571tip"; // coline faith

        //MPD
        //UserName = "z1694tip"; // Maam Duran

        //Leader
        //UserName = "z1166tip"; // Sir Hilario

        //UserName = "z8257tip"; // Sir Lloyd
        //UserName = "z1573tip"; // 
        //UserName = "z1460tip"; // 
        //UserName = "z3538tip"; // 


        return UserName;
    }

    public static string Get_ComputerName()
    {
        string PCName = string.Empty;
        try
        {
            PCName = (Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName);
            //if (PCName.Contains(".")) { PCName = PCName.Remove(10, ".tip1.ap.toshiba.dpg.local".Length); }
            if (PCName.Length > 10)
            {
                PCName = PCName.Substring(0, 10);
            }
        }
        catch (Exception ex)
        {
            string err = ex.Message;
            PCName = Get_IPAddress();
        }
        return PCName;
    }

    public static string Get_IPAddress()
    {
        string nowIP = "";
        try { if (nowIP == "") { nowIP = HttpContext.Current.Request.ServerVariables["remote_addr"].ToString(); } }
        catch (Exception ex) { string err = ex.Message; }
        return nowIP;
    }

    public static bool ISWebAdministrator()
    {
        bool PCName = true;
        return PCName;
    }
}