using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Application
/// </summary>
/// 
namespace control.cmsmodules.security
{
    public class Application
    {
        public static int appId = 1;
        public Application()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public string generate()
        {
            string res = "";
            try
            {
                List<model.db.application> apps = model.module.security.Application.getAllApplications();
                if (apps != null)
                {
                    if (apps.Count > 0)
                        res = @" <a href='#' ></a> <br />
                            <table class='gridtable'>
                                <tr>
                                    <th style='width:150px;'>application name</th>
                                    <th>is active</th>
                                    <th></th>
                                </tr>";
                    foreach (model.db.application app in apps)
                    {
                        string isActive = app.is_active == 1 ? "Yes" : "No";
                        res += "<tr><td>" + app.app_name + "</td>";
                        res += "<td>" + isActive + "</td>";
                        res += "<td><a href='#' onclick='Application.edit_application(" + app._app_id + ")' > edit </a></td></tr>";
                    }
                    if (apps.Count > 0)
                        res += "</table>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.generate", ex);
            }
            return res;
        }

        public string generateEditApplication(int appId)
        {
            string res = "";
            try
            {
                model.module.security.Application application = new model.module.security.Application();
                model.db.application app = application.getApplicationById(appId);
                string isActive = app.is_active == 1 ? "checked" : "";
                res = @"<table class='gridtable'>
                            <tr><th colspan='3'>edit record</th></tr>
                            <tr>
                                <th style='width:150px;'>application name</th><td><input type='text' id='app_name' value='" + app.app_name + @"' /></td>
                            </tr>
                            <tr>
                                <th>is active</th><td><input type='checkbox' id='is_active' " + isActive + @" /></td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='Application.update_application(" + app._app_id + @")' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='Application.generate()' >back</a></th>
                            </tr>
                         </table>";

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.generateEditApplication", ex);
            }
            return res;
        }

        public string generateInsertApplication()
        {
            string res = "";
            try
            {
                res = @"<table class='gridtable'>
                            <tr><th colspan='2'>new record</th></tr>
                            <tr>
                                <th>application name</th><td><input type='text' id='app_name' </td>
                            </tr>
                            <tr>
                                <th>is active</th><td><input type='checkbox' id='is_active' /></td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='Application.save_application()' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='Application.generate()' >back</a></th>
                            </tr>
                         </table>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.generateInsertApplication", ex);
            }
            return "";
        }
        public static string applicationHandler(string mode, string appId, string appName, string isActive)
        {
            string res = "";
            try
            {
                Application app = new Application();
                switch (mode)
                {
                    case "1":
                        res = app.generate();
                        break;
                    case "2":
                        res = app.generateEditApplication(Convert.ToInt32(appId));
                        break;
                    case "3":
                        model.db.application a = new model.db.application();
                        a._app_id = Convert.ToInt32(appId);
                        a.app_name = appName;
                        isActive = isActive == "true" ? "1" : "0";
                        a.is_active = Convert.ToInt32(isActive);
                        if (model.module.security.Application.updateApplication(a))
                            res = "updated";
                        else
                            res = "error";
                        break;
                    case "4":
                        res = app.generateInsertApplication();
                        break;
                    case "5":
                        model.db.application aa = new model.db.application();
                        aa.app_name = appName;
                        isActive = isActive == "true" ? "1" : "0";
                        aa.is_active = Convert.ToInt32(isActive);
                        if (model.module.security.Application.insertApplication(aa))
                            res = "saved";
                        else
                            res = "error";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.applicationHandler", ex);
            }
            return res;
        }
    }
}