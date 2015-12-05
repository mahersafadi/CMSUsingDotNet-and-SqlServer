using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for Role
/// </summary>
/// 
namespace model.module.security
{
    public class Role
    {
        public Role()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.role> getApplicationRoles(int appId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.roles.Where(a => a.__rapplication == appId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Role.getApplicationRoles", ex);
            }
            return null;
        }

        public static model.db.role getRoleById(int id)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                role r = db.roles.First(a => a._rid == id);
                return r;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Role.getRoleById", ex);
            }
            return null;
        }

        public static model.db.role getRoleByName(string name)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                role r = db.roles.First(a => a.role_name == name);
                return r;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Role.getRoleById", ex);
            }
            return null;
        }

        public static bool insertRole(model.db.role role)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.roles.InsertOnSubmit(role);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.module.security.Role.insertRole", ex);
            }
            return false;
        }

        public static bool updateRole(model.db.role role)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.role r = db.roles.Single(a => a._rid == role._rid);
                r.role_name = role.role_name;
                r.description = role.description;
                r.default_url = role.default_url;
                r.from_date = role.from_date;
                r.to_date = role.to_date;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Role.updateRole", ex);
            }
            return false;
        }

        public static bool deleteRole(model.db.role role)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.role p = db.roles.Single(a => a._rid == role._rid);
                db.roles.DeleteOnSubmit(p);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Role.deleteRole", ex);
            }
            return false;
        }
    }
}