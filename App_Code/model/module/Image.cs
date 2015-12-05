using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for Image
/// </summary>
/// 
namespace model.module
{
    public class Image
    {
        public Image()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //************************get all content images*****************************
        public static List<model.db.image> getAllContentImages(int contentId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.images.Where(i => i.__rcontent == contentId).OrderBy(i => i.create_date);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Image.getAllContentImages", ex);
                return null;
            }
        }


        //************************get image*****************************
        public static model.db.image getImage(int imageId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                return db.images.First(i => i._iid == imageId);
            }
            catch (Exception ex)
            {
                Log.logErr("Image.getImage", ex);
                return null;
            }
        }

        //********************insert new image*********************************
        public static bool insertImage(model.db.image image)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.images.InsertOnSubmit(image);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Image.insertImage", ex);
                return false;
            }
        }

        //********************delete image*********************************
        public static bool deleteImage(int imageId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var image = db.images.First(i => i._iid == imageId);
                db.images.DeleteOnSubmit(image);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Image.deleteImage", ex);
                return false;
            }
        }

        //********************delete content images*********************************
        public static bool deleteContentImages(int cid)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var images = db.images.Where(i => i.__rcontent == cid);
                foreach (var item in images)
                    db.images.DeleteOnSubmit(item);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Image.deleteContentImages", ex);
                return false;
            }
        }
    }
}