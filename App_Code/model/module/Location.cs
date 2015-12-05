using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Location
/// </summary>
namespace model.module
{
    public class Location
    {
        public Location()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.location> list()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.locations;
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Location.list", ex);
                return null;
            }
        }

        public static bool delete(model.db.location l)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.locations.DeleteOnSubmit(l);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("location.delete", ex);
            }
            return false;
        }

        public static bool insert(model.db.location l)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.locations.InsertOnSubmit(l);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Location.inert", ex);
                return false;
            }
        }

        public static model.db.location getById(int id) {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.locations.Where(l => l._lid == id);
                return qry.First();
            }
            catch (Exception ex)
            {
                Log.logErr("Location.inert", ex);
                return null;
            }   
        }
    }
}