using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for Permission
/// </summary>
/// 
namespace model.module.security
{
    public class Permission
    {
        public Permission()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.permission> getApplicationPermissions(int appId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.permissions.Where(a => a.__rapplication == appId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Permission.getApplicationPermissions", ex);
            }
            return null;
        }

        public static model.db.permission getPermissionById(int id)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                permission per = db.permissions.First(a => a._pid == id);
                return per;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Permission.getPermissionById", ex);
            }
            return null;
        }

        public static model.db.permission getPermissionByName(string name)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                permission per = db.permissions.First(a => a.menu_id.ToLower()== name.ToLower());
                return per;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Permission.getPermissionByName", ex);
            }
            return null;
        }

        public static bool insertPermission(model.db.permission per)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.permissions.InsertOnSubmit(per);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.module.security.Permission.insertPermission", ex);
            }
            return false;
        }

        public static bool updatePermission(model.db.permission per)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.permission p = db.permissions.Single(a => a._pid == per._pid);
                p.page = per.page;
                p.menu_id = per.menu_id;
                p.description = per.description;
                p.security_level = per.security_level;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Permission.updatePermission", ex);
            }
            return false;
        }

        public static bool deletePermission(model.db.permission per)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.permission p = db.permissions.Single(a => a._pid == per._pid);
                db.permissions.DeleteOnSubmit(p);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Permission.deletePermission", ex);
            }
            return false;
        }


    }
}