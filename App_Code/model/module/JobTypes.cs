using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JobTypes
/// </summary>
namespace model.module
{
    public class JobTypes
    {
        public JobTypes()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.job_type> list()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.job_types;
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.list", ex);
                return null;
            }
        }

        public string toSortableList(string url) {
            string res = "";
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.job_types;
                List<job_type> rr = qry.ToList();
                for (int i = 0; i < rr.Count; i++)
                {
                    job_type jt = rr[i];
                    string str = url.Replace("{id}", jt._jid.ToString());
                    str = str.Replace("{name}", jt.name);
                    res += "<li ><a href=\""+str+"\">"+jt.name+"</a></li>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.list", ex);
            }
            return res;
        }

        public string toOptions(int seleId) {
            string res = "";
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.job_types;
                List<job_type> rr = qry.ToList();
                for (int i = 0; i < rr.Count; i++)
                {
                    job_type jt = rr[i];
                    res += "<option ";
                    if (jt._jid == seleId)
                        res += " selected='selected' ";
                    res += " value='"+jt._jid+"'>"+jt.name+"</option>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.list", ex);
            }
            return res;
        }

        public String getNameById(int id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.job_types;
                job_type rr = qry.Single(cc=>cc._jid == id);
                return rr.name;
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.getNameById", ex);
            }
            return "";
        }

        public static bool delete(model.db.job_type jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.job_types.DeleteOnSubmit(jt);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("JobType.delete", ex);
            }
            return false;
        }

        public static bool insert(model.db.job_type jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.job_types.InsertOnSubmit(jt);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.inert", ex);
                return false;
            }
        }

        public static model.db.job_type getById(int id) {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.job_types.Where(jt => jt._jid == id);
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