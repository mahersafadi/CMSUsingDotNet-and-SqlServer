using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PermissionInRole
/// </summary>
/// 
namespace control.cmsmodules.security
{

    public class PermissionInRole
    {
        public PermissionInRole()
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
                res += @"<table class='gridtable'>
                                <tr>
                                    <th style='width:100px;'>Select role</th>
                                    <td style='width:345px;'>" + new PermissionInRole().generateRolesSelect() + @"</td>
                                </tr>
                            </table>";
                res += "<div id='all_permissions'>";
                res += "</div>";
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return res;
        }

        public string generateAllPermissions(int roleId)
        {
            string res = "";
            if (roleId == 0)
                return "";
            try
            {
                res += @"<table class='gridtable'>
                                <tr>
                                    <th style='width:150px;'>permission in role</th>
                                    <th style='width:10px;'></th>
                                    <th style='width:150px;'>permission not in role</th>
                                </tr>
                                <tr>
                                    <td>" + new PermissionInRole().generatePermissionInRole(roleId.ToString()) + @"</td>
                                    <td style='vertical-align: top;'>
                                        <button onclick='PermissionInRole.remove_permission()'> > </button>
                                            <br />
                                        <button onclick='PermissionInRole.add_permission()'> < </button>
                                    </td>
                                    <td>" + new PermissionInRole().generatePermissionNotInRole(roleId.ToString()) + @"</td>
                                </tr>
                                <tr>
                                    <th colspan=3>
                                        <a href='#' onclick='PermissionInRole.save_Permission_in_role()' > save </a>
                                    </th>
                                </tr>
                         </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.PermissionInRole.generateAllPermissions", ex);
            }
            return res;
        }

        public string generatePermissionInRole(string roleId)
        {
            string res = "";
            try
            {
                List<model.db.permission_in_role> persInRole = model.module.security.PermissionInRole.getPermissionsInRoleByRoleId(Convert.ToInt32(roleId));
                res += "<select multiple='multiple' style='width:200px; height:100px;' id='lst_permissions_in_role'>";
                if (persInRole.Count > 0)
                {
                    foreach (var item in persInRole)
                    {
                        res += "<option value='" + item.permission._pid + "'" + ">" + item.permission.menu_id + "</option>";
                    }
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.PermissionInRole.generatePermissions", ex);
            }
            return res;
        }

        public string generatePermissionNotInRole(string roleId)
        {
            string res = "";
            try
            {
                List<model.db.permission> persNotInRole = model.module.security.PermissionInRole.getPermissionsNotInRoleByRoleId(Convert.ToInt32(roleId));
                res += "<select multiple='multiple' style='width:200px; height: 100px;' id='lst_permissions_not_in_role'>";
                if (persNotInRole.Count > 0)
                {
                    foreach (var item in persNotInRole)
                    {
                        res += "<option value='" + item._pid + "'" + ">" + item.menu_id + "</option>";
                    }
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.PermissionInRole.generatePermissionNotInRole", ex);
            }
            return res;
        }

        public string generateRolesSelect()
        {
            string res = "";
            try
            {
                List<model.db.role> roles = model.module.security.Role.getApplicationRoles(Application.appId);
                res = "<select style='width:200px;' id='lst_roles' onchange='PermissionInRole.get_permissions_role(this)'>";
                res += "<option value='0'>--------------</option>";
                foreach (model.db.role role in roles)
                {
                    res += "<option value='" + role._rid + "'>" + role.role_name + "</option>";
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.PermissionInRole.generateRolesSelect", ex);
            }
            return res;
        }

        public bool insertPermissionsInRole(string roleId, string selectedPermissions)
        {
            try
            {
                List<model.db.permission_in_role> currentPers = model.module.security.PermissionInRole.getPermissionsInRoleByRoleId(Convert.ToInt32(roleId));
                if (currentPers.Count > 0)
                {
                    foreach (var item in currentPers)
                    {
                        model.module.security.PermissionInRole.deletePermissionInRole(item);
                    }
                }

                if (selectedPermissions != null && selectedPermissions != "")
                {
                    string[] permissionIds = selectedPermissions.Split(',');
                    for (int i = 0; i < permissionIds.Length; i++)
                    {
                        model.db.permission_in_role perInRole = new model.db.permission_in_role();
                        perInRole.__rpermission = Convert.ToInt32(permissionIds[i]);
                        perInRole.__role = Convert.ToInt32(roleId);
                        model.module.security.PermissionInRole.insertPermissionInRole(perInRole);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.PermissionInRole.insertPermissionsInRole", ex);
            }
            return false;
        }
        public static string roleHandler(string mode, string roleId, string selectedPermissions)
        {
            string res = "";
            try
            {
                /***
                 * mode= 1 => generate
                 * mode= 2 => insert
                 * **/
                model.db.role role = new model.db.role();
                switch (mode)
                {
                    case "1":
                        res = new control.cmsmodules.security.PermissionInRole().generateAllPermissions(Convert.ToInt32(roleId));
                        break;
                    case "2":
                        if (new PermissionInRole().insertPermissionsInRole(roleId, selectedPermissions))
                            res = "done";
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