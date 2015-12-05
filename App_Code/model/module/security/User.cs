using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for User
/// </summary>
/// 
namespace model.module.security
{

    public class User
    {
        public static string[] user_types = { "", "root", "company", "client" };
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.user> getUsers(int appId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.users.OrderBy(u => u.user_name);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.User.getUsers", ex);
            }
            return null;
        }

        public static model.db.user getUserById(int id)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                user user = db.users.First(u => u._uid == id);
                return user;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.User.getUserById", ex);
            }
            return null;
        }

        public static List<model.db.user> getUserByNameLike(string name)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                var query = db.users.Where(u => u.user_name.ToLower().Contains(name.ToLower()));
                return query.ToList();
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
        public static model.db.user getUserByName(string userName)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                user user = db.users.First(u => u.user_name == userName);
                return user;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.User.getUserByName", ex);
            }
            return null;
        }

        public static model.db.user login(string userName, string password)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                user user = db.users.First(u => u.user_name == userName.ToString() && Global.Encode(password)== u.pwd);
                return user;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.User.login", ex);
            }
            return null;
        }

        public static bool insert(model.db.user u, model.db.user_in_role uir, model.db.partner_client pc)
        {
            CMSDataContext db = new CMSDataContext();
            try
            {
                db.Connection.Open();
                db.Transaction = db.Connection.BeginTransaction();
                List<model.db.user> all = db.users.ToList();
                int userId = 0;
                if(all.Count() > 0)
                    userId = all[all.Count() - 1]._uid;
                userId++;

                u._uid = userId;
                db.users.InsertOnSubmit(u);
                uir.__user = userId;
                db.user_in_roles.InsertOnSubmit(uir);
                if (pc != null)
                {
                    pc.client_id = userId;
                    db.partner_clients.InsertOnSubmit(pc);
                }

                db.SubmitChanges();
                db.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                db.Transaction.Rollback();
            }
            return false;
        }

        public static bool insert(model.db.user user)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                user.create_date = DateTime.Now;
                db.users.InsertOnSubmit(user);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.module.security.User.insert", ex);
            }
            return false;
        }

        public static bool update(model.db.user user)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.user u = db.users.Single(a => a._uid == user._uid);
                u.first_name = user.first_name;
                u.last_name = user.last_name;
                u.email = user.email;
                u.authenitication_mode = user.authenitication_mode;
                u.is_active = user.is_active;
                u.phone = user.phone;
                u.user_name = user.user_name;
                u.address = user.address;

                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.User.update", ex);
            }
            return false;
        }

        public static bool delete(model.db.user user)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                try
                {
                    model.db.user_in_role uir = db.user_in_roles.Single(uu => uu.__user == user._uid);
                    db.user_in_roles.DeleteOnSubmit(uir);
                }
                catch { }
                model.db.user u = db.users.Single(a => a._uid == user._uid);
                db.users.DeleteOnSubmit(u);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.User.delete", ex);
            }
            return false;
        }
    }
}