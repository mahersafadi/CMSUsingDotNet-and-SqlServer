using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Permission
/// </summary>
/// 
namespace control.cmsmodules.security
{
    public class Permission
    {
        public Permission()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string generate()
        {
            string res = "";
            try
            {
                List<model.db.permission> pers = model.module.security.Permission.getApplicationPermissions(1);
                res += @"<a href='#' onclick='Permission.insert_permission()'>insert new permission</a> <br />";
                if (pers != null)
                {
                    if (pers.Count > 0)
                            res +=@"<table class='gridtable'>
                                <tr>
                                    <th style='width:100px;'>menu</th>
                                    <th style='width:200px;'>description</th>
                                    <th></th>
                                </tr>";
                    foreach (model.db.permission p in pers)
                    {
                        res += "<tr>";
                        res += "<td>" + p.menu_id + "</td>";
                        res += "<td>" + p.description + "</td>";
                        res += @"<td><a href='#' onclick='Permission.edit_permission(" + p._pid + @")' > edit </a>
                                    &nbsp;<a href='#' onclick='Permission.delete_Permission(" + p._pid + @")' > delete </a>
                                </td>
                              </tr>";
                    }
                    if (pers.Count > 0)
                        res += "</table>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Permission.generate", ex);
            }
            return res;
        }

        public string generateInsert()
        {
            string res = "";
            try
            {
                res = @"<table class='gridtable'>
                            <tr><th colspan='2'>new record</th></tr>
                            <tr>
                                <th>menu id</th><td><input type='text' id='menu_id' </td>
                            </tr>
                            <tr>
                                <th>desription</th><td><input type='text' id='desc' </td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='Permission.do_insert_permission()' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='Permission.generate()' >back</a></th>
                            </tr>
                         </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Permission.generateInsert", ex);
            }
            return res;
        }

        public string doInsert(model.db.permission per)
        {
            try
            {
                if (model.module.security.Permission.insertPermission(per))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Permission.doInsert", ex);
            }
            return "";
        }

        public string generateEdit(string id)
        {
            string res = "";
            try
            {
                model.db.permission per = model.module.security.Permission.getPermissionById(Convert.ToInt32(id));
                res = @"<table class='gridtable'>
                            <tr><th colspan='3'>edit record</th></tr>
                            <tr>
                                <th style='width:150px;'>menu</th><td><input type='text' id='menu_id' value='" + per.menu_id + @"' /></td>
                            </tr>
                            <tr>
                                <th style='width:150px;'>description</th><td><input type='text' id='desc' value='" + per.description + @"' /></td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='Permission.do_edit_permission(" + per._pid + @")' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='Permission.generate()' >back</a></th>
                            </tr>
                         </table>";

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Permission.generateEdit", ex);
            }
            return res;
        }


        public string doEdit(model.db.permission per)
        {
            try
            {
                if (model.module.security.Permission.updatePermission(per))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Permission.doEdit", ex);
            }
            return "0";
        }

        public string delete(model.db.permission per)
        {
            try
            {
                if (model.module.security.Permission.deletePermission(per))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Permission.delete", ex);
            }
            return "";
        }

        public static string permissionHandler(string mode, string menuId, string description, string id)
        {
            string res = "";
            try
            {
                /***
                 * mode= 1 => generate
                 * mode= 2 => generate insert
                 * mode= 3 => do insert
                 * mode= 4 => generate edit
                 * mode= 5 => do edit
                 * mode= 6 => delete
                 * **/
                model.db.permission per = new model.db.permission();
                switch (mode)
                {

                    case "1":
                        res = generate();
                        break;
                    case "2":
                        res = new Permission().generateInsert();
                        break;
                    case "3":
                        per.menu_id = menuId;
                        per.description = description;
                        per.__rapplication = Application.appId;
                        res = new Permission().doInsert(per);
                        break;
                    case "4":
                        res = new Permission().generateEdit(id);
                        break;
                    case "5":
                        per.menu_id = menuId;
                        per.description = description;
                        per._pid = Convert.ToInt32(id);
                        res = new Permission().doEdit(per);
                        break;
                    case "6":
                        model.db.permission p = model.module.security.Permission.getPermissionById(Convert.ToInt32(id));
                        res = new Permission().delete(p);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Permission.permissionHandler", ex);
            }
            return res;
        }
    }
}