using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInRole
/// </summary>
/// 
namespace control.cmsmodules.security
{
    public class UserInRole
    {
        public UserInRole()
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
                                    <td style='width:345px;'>" + new UserInRole().generateRolesSelect() + @"</td>
                                </tr>
                            </table>";
                res += "<div id='all_users'>";
                res += "</div>";
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return res;
        }

        public string generateAllUsers(int roleId)
        {
            string res = "";
            if (roleId == 0)
                return "";
            try
            {
                res += @"<table class='gridtable'>
                                <tr>
                                    <th style='width:150px;'>user in role</th>
                                    <th style='width:10px;'></th>
                                    <th style='width:150px;'>user not in role</th>
                                </tr>
                                <tr>
                                    <td>" + new UserInRole().generateUserInRole(roleId.ToString()) + @"</td>
                                    <td style='vertical-align: top;'>
                                        <button onclick='UserInRole.remove_user()'> > </button>
                                            <br />
                                        <button onclick='UserInRole.add_user()'> < </button>
                                    </td>
                                    <td>" + new UserInRole().generateUserNotInRole(roleId.ToString()) + @"</td>
                                </tr>
                                <tr>
                                    <th colspan=3>
                                        <a href='#' onclick='UserInRole.save_user_in_role()' > save </a>
                                    </th>
                                </tr>
                         </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.UserInRole.generateAllUsers", ex);
            }
            return res;
        }

        public string generateUserInRole(string roleId)
        {
            string res = "";
            try
            {
                List<model.db.user_in_role> usersInRole = model.module.security.UserInRole.getUserInRoleByRoleId(Convert.ToInt32(roleId));
                res += "<select multiple='multiple' style='width:200px; height: 100px;' id='lst_users_in_role'>";
                if (usersInRole.Count > 0)
                {
                    foreach (var item in usersInRole)
                    {
                        res += "<option value='" + item.user._uid+ "'" + ">" + item.user.first_name + " " + item.user.last_name + "</option>";
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

        public string generateUserNotInRole(string roleId)
        {
            string res = "";
            try
            {
                List<model.db.user> usersNotInRole = model.module.security.UserInRole.getUsersNotInRoleByRoleId(Convert.ToInt32(roleId));
                res += "<select multiple='multiple' style='width:200px; height: 100px;' id='lst_users_not_in_role'>";
                if (usersNotInRole.Count > 0)
                {
                    foreach (var item in usersNotInRole)
                    {
                        res += "<option value='" + item._uid + "'" + ">" + item.first_name + " " + item.last_name + "</option>";
                    }
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.UserInRole.generateUserNotInRole", ex);
            }
            return res;
        }

        public string generateRolesSelect()
        {
            string res = "";
            try
            {
                List<model.db.role> roles = model.module.security.Role.getApplicationRoles(Application.appId);
                res = "<select style='width:200px;' id='lst_roles1' onchange='UserInRole.get_users_role(this)'>";
                res += "<option value='0'>--------------</option>";
                foreach (model.db.role role in roles)
                {
                    res += "<option value='" + role._rid + "'>" + role.role_name + "</option>";
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.UserInRole.generateRolesSelect", ex);
            }
            return res;
        }

        public bool insertUsersInRole(string roleId, string selectedusers)
        {
            try
            {
                List<model.db.user_in_role> currentUsers = model.module.security.UserInRole.getUserInRoleByRoleId(Convert.ToInt32(roleId));
                if (currentUsers.Count > 0)
                {
                    foreach (var item in currentUsers)
                    {
                        model.module.security.UserInRole.delete(item);
                    }
                }

                if (selectedusers != null && selectedusers != "")
                {
                    string[] userIds = selectedusers.Split(',');
                    for (int i = 0; i < userIds.Length; i++)
                    {
                        model.db.user_in_role userInRole = new model.db.user_in_role();
                        userInRole.__user = Convert.ToInt32(userIds[i]);
                        userInRole.__role = Convert.ToInt32(roleId);
                        model.module.security.UserInRole.insert(userInRole);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.UserInRole.insertUsersInRole", ex);
            }
            return false;
        }
        public static string userInRoleHandler(string mode, string roleId, string selectedUsers)
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
                        res = new control.cmsmodules.security.UserInRole().generateAllUsers(Convert.ToInt32(roleId));
                        break;
                    case "2":
                        if (new UserInRole().insertUsersInRole(roleId, selectedUsers))
                            res = "done";
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.userInRoleHandler", ex);
            }
            return res;
        }
    }
}