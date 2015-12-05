using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Log
/// </summary>
public class Log
{
    public Log()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static string __log = "c:\\PMinitry.log";
    private static System.IO.StreamWriter sw;

    public static System.IO.StreamWriter getLog()
    {
        try
        {
            if (sw == null)
            {
                sw = new System.IO.StreamWriter(__log);
            }
        }
        catch (Exception ex)
        {

        }
        return sw;
    }

    public static void log(string msg)
    {
        try
        {
            getLog().WriteLine("PMinstry info:" + DateTime.Now.ToString() + ":" + msg);
        }
        catch (Exception ex)
        {

        }
    }

    public static void logErr(string msg, Exception ex)
    {
        try
        {
            //getLog().WriteLine("PMinstry err:" + DateTime.Now.ToString() + ":" + msg+", "+ex.Message);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(__log, true);
            sw.WriteLine("PMinstry err:" + DateTime.Now.ToString() + ":" + msg + ", " + ex.Message);
            sw.Close();
        }
        catch (Exception ex1)
        {

        }
    }
}