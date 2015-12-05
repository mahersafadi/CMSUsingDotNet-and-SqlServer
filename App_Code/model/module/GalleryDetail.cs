using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GalleryDetail
/// </summary>
namespace model.module
{
    public class GalleryDetail
    {
        public GalleryDetail()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.gallery_detail> list()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.gallery_details;
                return qry.ToList();
            }
            catch (Exception ex)
            {

            }
            return null;
        }





        public static bool delete(model.db.gallery_detail jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                gallery_detail g = db.gallery_details.Single(gg => gg._cdid == jt._cdid);
                db.gallery_details.DeleteOnSubmit(g);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("gallery.delete", ex);
            }
            return false;
        }

        public static bool insert(model.db.gallery_detail jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.gallery_details.InsertOnSubmit(jt);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("gallery_details.inert", ex);
                return false;
            }
        }

        public static bool update(model.db.gallery_detail jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                gallery_detail g = db.gallery_details.Single(gg => gg._cdid == jt._cdid);
                g.img_url = jt.img_url;
                g.rgllary = jt.rgllary;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("gallery.inert", ex);
                return false;
            }
        }

        public static model.db.gallery_detail getById(int id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.gallery_details.Where(jt => jt._cdid == id);
                return qry.First();
            }
            catch (Exception ex)
            {
                Log.logErr("Gallery.inert", ex);
                return null;
            }
        }

        public static List<model.db.gallery_detail> getGalleryDetails(int gId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.gallery_details.Where(jt => jt.rgllary == gId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Gallery_details.getgallerydetails", ex);
                return null;
            }
        }
    }
}