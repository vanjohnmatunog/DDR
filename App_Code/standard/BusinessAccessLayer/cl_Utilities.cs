using System;
using System.Configuration;

/// <summary>
/// Summary description for cl_Utilities
/// </summary>
public class cl_Utilities
{
    public static string SystemName()
    {
        return ConfigurationManager.AppSettings["SystemName"].ToString();
    }

    public static string Version()
    {
        return ConfigurationManager.AppSettings["Version"].ToString();
    }

    public static string ApplicationID()
    {
        return ConfigurationManager.AppSettings["ApplicationID"].ToString();
    }

    public static string TIPIS3Logo()
    {
        return ConfigurationManager.AppSettings["TIPIS3Logo"].ToString();
    }

    public static string TIPIS3LogoRedirect()
    {
        return ConfigurationManager.AppSettings["TIPIS3LogoRedirect"].ToString();
    }

    public static string DefaultUserDisplay()
    {
        return ConfigurationManager.AppSettings["DefaultUserDisplay"].ToString();
    }


    public static Boolean ISValidDate(string dateTime)
    {
        bool result = false;
        DateTime d;
        try
        {
            d = Convert.ToDateTime(dateTime);
            result = true;
        }
        catch (Exception ex) { }
        return result;
    }

    public static bool ISNumeric(string numeric)
    {
        bool result = false;
        int d;
        try
        {
            d = Convert.ToInt16(numeric);
            result = true;
        }
        catch (Exception ex) { }
        return result;
    }
}