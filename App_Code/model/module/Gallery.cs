using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Gallery
/// </summary>
namespace model.module
{
    public class Gallery
    {
        public Gallery()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.gallery> list()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.galleries;
                return qry.ToList();
            }
            catch (Exception ex)
            {

            }
            return null;
        }





        public static bool delete(model.db.gallery jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                gallery g = db.galleries.Single(gg => gg._gid == jt._gid);
                db.galleries.DeleteOnSubmit(g);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("gallery.delete", ex);
            }
            return false;
        }

        public static bool insert(model.db.gallery jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.galleries.InsertOnSubmit(jt);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("gallery.inert", ex);
                return false;
            }
        }

        public static bool update(model.db.gallery jt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                gallery g = db.galleries.Single(gg=>gg._gid == jt._gid);
                g.name = jt.name;
                g.is_active = jt.is_active;
                g.create_by = jt.create_by;
                g.create_date = jt.create_date;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("gallery.inert", ex);
                return false;
            }
        }

        public static model.db.gallery getById(int id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.galleries.Where(jt => jt._gid == id);
                return qry.First();
            }
            catch (Exception ex)
            {
                Log.logErr("Gallery.inert", ex);
                return null;
            }
        }



    }
}