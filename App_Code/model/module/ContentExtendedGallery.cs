using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ContentExtendedGallery
/// </summary>
namespace model.module
{

    public class ContentExtendedGallery
    {
        public ContentExtendedGallery()
        {

        }

        public static int insert(content_extended_gallery ce)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.content_extended_galleries.InsertOnSubmit(ce);
                db.SubmitChanges();
                return ce.__ceid;
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return 0;
        }

        public static bool unpdate(content_extended_gallery ce)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_extended_gallery cee = db.content_extended_galleries.Single(cee1 => cee1.__ceid == ce.__ceid);
                cee.name = ce.name;
                cee.status = ce.status;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("content_extra.update", ex);
            }
            return false;
        }

        public static bool delete(int contentExtraGallery)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_extra ce = db.content_extras.Single(cee => cee.ce_id == contentExtraGallery);
                db.content_extras.DeleteOnSubmit(ce);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("content_Extended_gallery.delete", ex);
            }
            return false;
        }

        public static List<content_extended_gallery> list()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var query = from c in db.content_extended_galleries
                                select c;
                return query.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return null;
        }
    }
}