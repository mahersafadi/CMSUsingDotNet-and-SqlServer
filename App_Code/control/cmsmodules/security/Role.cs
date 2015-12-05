using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Role
/// </summary>
/// 
namespace control.cmsmodules.security
{
    public class Role
    {
        public Role()
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
                //res += new Role().generateApplicationSelect();
                //res += "<br />";
                List<model.db.role> roles = model.module.security.Role.getApplicationRoles(1);
                res = @"<a href='#' onclick='Role.insert_role()'>insert new role</a> <br />";
                if (roles != null)
                {
                    if (roles.Count > 0)
                        res += @"<table class='gridtable'>
                                <tr>
                                    <th style='width:100px;'>role name</th>
                                    <th style='width:200px;'>description</th>
                                    <th>from date</th>
                                    <th>to date</th>
                                    <th></th>
                                </tr>";
                    foreach (model.db.role r in roles)
                    {

                        res += "<tr><td>" + r.role_name + "</td>";
                        res += "<td>" + r.description + "</td>";
                        res += "<td>" + Convert.ToDateTime(r.from_date).ToString("yyyy/MM/dd") + "</td>";
                        res += "<td>" + Convert.ToDateTime(r.to_date).ToString("yyyy/MM/dd") + "</td>";
                        res += @"<td><a href='#' onclick='Role.edit_role(" + r._rid + @")' > edit </a>
                                    &nbsp;<a href='#' onclick='Role.delete_role(" + r._rid + @")' > delete </a>
                                </td>
                              </tr>";
                    }
                    if (roles.Count > 0)
                        res += "</table>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.generate", ex);
            }
            return res;
        }

        private string generateApplicationSelect()
        {
            string res = "";
            try
            {
                List<model.db.application> apps = model.module.security.Application.getAllApplications();
                res = "<select id='lst_applications' onchange='Role.get_app_roles(this)'>";
                foreach (model.db.application app in apps)
                {
                    res += "<option value='" + app._app_id + "'>" + app.app_name + "</option>";
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.generateApplicationSelect", ex);
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
                                <th>role name</th><td><input type='text' id='r_name' </td>
                            </tr>
                            <tr>
                                <th>desription</th><td><input type='text' id='desc' </td>
                            </tr>
                            <tr>
                                <th>from date</th><td><input type='text' id='from_date' </td>
                            </tr>
                            <tr>
                                <th>to date</th><td><input type='text' id='to_date' </td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='Role.do_insert_role()' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='Role.generate()' >back</a></th>
                            </tr>
                         </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Role.generateInsert", ex);
            }
            return res;
        }

        public string doInsert(model.db.role role)
        {
            try
            {
                if (model.module.security.Role.insertRole(role))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Role.doInsert", ex);
            }
            return "";
        }

        public string generateEdit(string id)
        {
            string res = "";
            try
            {
                model.db.role role = model.module.security.Role.getRoleById(Convert.ToInt32(id));
                res = @"<table class='gridtable'>
                            <tr><th colspan='3'>edit record</th></tr>
                            <tr>
                                <th style='width:150px;'>role name</th><td><input type='text' id='r_name' value='" + role.role_name + @"' /></td>
                            </tr>
                            <tr>
                                <th style='width:150px;'>description</th><td><input type='text' id='desc' value='" + role.description + @"' /></td>
                            </tr>
                            <tr>
                                <th style='width:150px;'>from date</th><td><input type='text' id='from_date' value='" + Convert.ToDateTime(role.from_date).ToString("yyyy/MM/dd") + @"' /></td>
                            </tr>
                            <tr>
                                <th style='width:150px;'>to date</th><td><input type='text' id='to_date' value='" + Convert.ToDateTime(role.to_date).ToString("yyyy/MM/dd") + @"' /></td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='Role.do_edit_role(" + role._rid + @")' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='Role.generate()' >back</a></th>
                            </tr>
                         </table>";

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Role.generateEdit", ex);
            }
            return res;
        }


        public string doEdit(model.db.role role)
        {
            try
            {
                if (model.module.security.Role.updateRole(role))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Role.doEdit", ex);
            }
            return "0";
        }

        public string delete(model.db.role role)
        {
            try
            {
                if (model.module.security.Role.deleteRole(role))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.Role.delete", ex);
            }
            return "";
        }



        public static string roleHandler(string mode, string id, string rName, string description, string fromDate, string toDate)
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
                model.db.role role = new model.db.role();
                switch (mode)
                {

                    case "1":
                        res = generate();
                        break;
                    case "2":
                        res = new Role().generateInsert();
                        break;
                    case "3":
                        role.role_name = rName;
                        role.description = description;
                        role.from_date = Convert.ToDateTime(fromDate);
                        role.to_date = Convert.ToDateTime(toDate);
                        role.__rapplication = Application.appId;
                        res = new Role().doInsert(role);
                        break;
                    case "4":
                        res = new Role().generateEdit(id);
                        break;
                    case "5":
                        role.role_name = rName;
                        role.description = description;
                        role.from_date = Convert.ToDateTime(fromDate);
                        role.to_date = Convert.ToDateTime(toDate);
                        role._rid = Convert.ToInt32(id);
                        res = new Role().doEdit(role);
                        break;
                    case "6":
                        model.db.role r = model.module.security.Role.getRoleById(Convert.ToInt32(id));
                        res = new Role().delete(r);
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