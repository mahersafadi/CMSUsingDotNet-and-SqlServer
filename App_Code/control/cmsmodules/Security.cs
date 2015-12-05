using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Security
/// </summary>
/// 
namespace control.cmsmodules
{

    public enum AutheniticationMode
    {
        root = 1,
        user = 2,
    }

    public enum PermissionName
    {
        category,
        insert_category,
        delete_category,
        module,
        insert_module,
        update_module,
        delete_module,
        content,
        insert_content,
        delete_content,
        update_content,
        language,
        lang_detail,
        security,
        client_menu,
    }

    public class Security : Module
    {
        public Security()
        {
            //
            // TODO: Add constructor logic here
            //
        }




        public override string generate()
        {
            string res = "";
            try
            {
                res = @"<div id='mainContainer'>
		              <h2>Security</h2>
		              <div class='simpleTabs'>
		                <ul class='simpleTabsNavigation'>
		                  <li><a href='#'>application</a></li>
		                  <li><a href='#'>permission</a></li>
		                  <li><a href='#'>role</a></li>
                          <li><a href='#'>permission in role</a></li>
                          <li><a href='#'>user</a></li>
                          <li><a href='#'>user in role</a></li>
		                </ul>
		                <div class='simpleTabsContent'>
		                  <div id='app_contents'>" + new control.cmsmodules.security.Application().generate() + @"</div>
		                </div>
                        <div class='simpleTabsContent'>
		                  <div id='permission_contents'>" + control.cmsmodules.security.Permission.generate() + @"</div>
		                </div>
		                <div class='simpleTabsContent'>
		                  <div id='role_contents'>" + control.cmsmodules.security.Role.generate() + @"</div>
		                </div>
		                <div class='simpleTabsContent'>
		                  <div id='permission_in_role_contents'>" + control.cmsmodules.security.PermissionInRole.generate() + @"</div>
		                </div>
                        <div class='simpleTabsContent'>
		                  <div id='user_contents'>" + control.cmsmodules.security.User.generate() + @"</div>
		                </div>
                        <div class='simpleTabsContent'>
		                  <div id='user_in_role_contents'>" + control.cmsmodules.security.UserInRole.generate() + @"</div>
		                </div>
		              </div>
                </div>";
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return res;
        }


        public static bool canAccess(string userName, string password)
        {
            model.db.user user = model.module.security.User.login(userName, password);
            if (user != null)
                return true;
            return false;
        }


        public static bool hasPermission(string permissionName)
        {
            try
            {
                /** 1- check if permission in database;
                 *  2- get permission from permission roles
                 *  3- check if user in database;
                 *  4- get user from user in role
                 *  5- compare
                 * 
                 */

                model.db.user user = (model.db.user)System.Web.HttpContext.Current.Session["user"];

                if (user == null)
                    return false;

                // for permission
                model.db.permission permission = model.module.security.Permission.getPermissionByName(permissionName);
                List<model.db.role> permissionRoles = new List<model.db.role>();
                if (permission != null)
                {
                    List<model.db.permission_in_role> permissionInRoles = model.module.security.PermissionInRole.getPermissionsInRoleByPermissionId(permission._pid);
                    foreach (var item in permissionInRoles)
                    {
                        permissionRoles.Add(item.role);
                    }
                }

                //for user
                model.db.user u = model.module.security.User.getUserById(user._uid);
                List<model.db.role> userRoles = new List<model.db.role>();
                if (u != null)
                {
                    List<model.db.user_in_role> userInRoles = model.module.security.UserInRole.getUserInRoleByUserId(u._uid);
                    foreach (var item in userInRoles)
                    {
                        userRoles.Add(item.role);
                    }
                }

                foreach (var uRole in userRoles)
                {
                    foreach (var pRole in permissionRoles)
                    {
                        if (uRole._rid == pRole._rid)
                            return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return false;
        }
    }
}