using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ParentClient
/// </summary>
namespace model.module.bnp
{
    public class ParentClient
    {
        public ParentClient()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public static List<model.db.partner_client> list()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.partner_clients;
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.list", ex);
                return null;
            }
        }

        public static bool delete(model.db.partner_client jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.partner_clients.DeleteOnSubmit(jt);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.delete", ex);
            }
            return false;
        }

        public static bool insert(model.db.partner_client jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.partner_clients.InsertOnSubmit(jt);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.inert", ex);
                return false;
            }
        }

        public static model.db.partner_client getById(int id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.partner_clients.Where(jt => jt._cp_id == id);
                return qry.First();
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.inert", ex);
                return null;
            }
        }
    }
}