using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for PermissionInRole
/// </summary>
/// 
namespace model.module.security
{
    public class PermissionInRole
    {
        public PermissionInRole()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static List<model.db.permission_in_role> getPermissionsInRoleByRoleId(int roleId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.permission_in_roles.Where(p => p.__role == roleId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.PermissionInRole.getPermissionsByRoleId", ex);
            }
            return null;
        }

        public static List<model.db.permission_in_role> getPermissionsInRoleByPermissionId(int permissionId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.permission_in_roles.Where(p => p.__rpermission == permissionId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.PermissionInRole.getPermissionsByRoleId", ex);
            }
            return null;
        }

        public static List<model.db.permission> getPermissionsNotInRoleByRoleId(int roleId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                List<model.db.permission_in_role> persInRole = getPermissionsInRoleByRoleId(roleId);
                List<Int32> permissionIds = new List<Int32>();
                foreach (var item in persInRole)
                {
                    permissionIds.Add(item.permission._pid);
                }

                var qry = db.permissions.Where(p => !permissionIds.Contains(p._pid));
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.PermissionInRole.getPermissionsNotByRoleId", ex);
            }
            return null;
        }


        public static model.db.permission_in_role getPermissionInRoleById(int id)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                permission_in_role per = db.permission_in_roles.First(a => a._prId == id);
                return per;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.PermissionInRole.getPermissionInRoleById", ex);
            }
            return null;
        }

        public static bool insertPermissionInRole(model.db.permission_in_role per)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.permission_in_roles.InsertOnSubmit(per);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.module.security.PermissionInRole.insertPermissionInRole", ex);
            }
            return false;
        }

        public static bool updatePermissionInRole(model.db.permission_in_role per)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.permission_in_role p = db.permission_in_roles.Single(a => a._prId == per._prId);
                p.__role = per.__role;
                p.__rpermission = per.__rpermission;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.PermissionInRole.updatePermissionInRole", ex);
            }
            return false;
        }

        public static bool deletePermissionInRole(model.db.permission_in_role per)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.permission_in_role p = db.permission_in_roles.Single(a => a._prId == per._prId);
                db.permission_in_roles.DeleteOnSubmit(p);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.PermissionInRole.deletePermissionInRole", ex);
            }
            return false;
        }


    }
}