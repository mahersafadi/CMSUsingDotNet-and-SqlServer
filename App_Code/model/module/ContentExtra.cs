using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContentExtra
/// </summary>
namespace model.module
{

    public class ContentExtra
    {
        public ContentExtra()
        {

        }

        public static List<content_extra> getAll(int contentId)
        {
            try {
                CMSDataContext db = new CMSDataContext();
                var query = from c in db.content_extras
                            where c.content_id == contentId
                            select c;
                return query.ToList();
            }
            catch(Exception ex)
            {
                Log.logErr("", ex);
            }
            return null;
        }

        public static  int insert(content_extra ce)
        {
            try {
                CMSDataContext db = new CMSDataContext();
                db.content_extras.InsertOnSubmit(ce);
                db.SubmitChanges();
                return ce.ce_id;
            }
            catch (Exception ex)
            {
                Log.logErr("content_extra.insertContentExtra", ex);
            }
            return 0;
        }

        public static bool unpdate(content_extra ce)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_extra dbce = db.content_extras.Single(cee  => cee.ce_id == ce.ce_id);
                dbce._key = ce._key;
                dbce._val = ce._val;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("content_extra.update", ex);
            }
            return false;
        }

        public static bool delete(int contentExtraId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_extra ce = db.content_extras.Single(cee => cee.ce_id == contentExtraId);
                db.content_extras.DeleteOnSubmit(ce);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("content_extra.delete", ex);
            }
            return false;
        }
    }
}