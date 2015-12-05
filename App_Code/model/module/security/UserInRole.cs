using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for UserInRole
/// </summary>
/// 
namespace model.module.security
{
    public class UserInRole
    {
        public UserInRole()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.user_in_role> getUserInRoleByRoleId(int roleId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.user_in_roles.Where(u => u.__role == roleId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.UserInRole.getUserInRoleByUserId", ex);
            }
            return null;
        }

        public static List<model.db.user_in_role> getUserInRoleByUserId(int userId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.user_in_roles.Where(u => u.__user == userId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.UserInRole.getUserInRoleByUserId", ex);
            }
            return null;
        }

        public static List<model.db.user> getUsersNotInRoleByRoleId(int roleId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                List<model.db.user_in_role> usersInRole = getUserInRoleByRoleId(roleId);
                List<Int32> userIds = new List<Int32>();
                foreach (var item in usersInRole)
                {
                    userIds.Add(item.user._uid);
                }

                var qry = db.users.Where(u => !userIds.Contains(u._uid));
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.UserInRole.getUsersNotInRoleByUserId", ex);
            }
            return null;
        }


        public static model.db.user_in_role getUserInRoleById(int id)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                model.db.user_in_role ur = db.user_in_roles.First(a => a._urid == id);
                return ur;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.UserInRole.getUserInRoleById", ex);
            }
            return null;
        }

        public static bool insert(model.db.user_in_role ur)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.user_in_roles.InsertOnSubmit(ur);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.module.security.UserInRole.insert", ex);
            }
            return false;
        }

        public static bool update(model.db.user_in_role ur)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.user_in_role u = db.user_in_roles.Single(a => a._urid == ur._urid);
                u.__role = ur.__role;
                u.__user = ur.__user;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.UserInRole.update", ex);
            }
            return false;
        }

        public static bool delete(model.db.user_in_role ur)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.user_in_role u = db.user_in_roles.Single(a => a._urid == ur._urid);
                db.user_in_roles.DeleteOnSubmit(u);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.UserInRole.delete", ex);
            }
            return false;
        }
    }
}