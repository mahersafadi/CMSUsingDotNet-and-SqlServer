using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;


/// <summary>
/// Summary description for Category
/// </summary>
namespace model.module
{
    public class Category
    {
        public Category()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //*****************************************************
        public List<model.db.category> getAllaCategories()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.categories.OrderBy(c => c.create_date);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Category.getCategories", ex);
                return null;
            }
        }

        //*****************************************************

        public string getCategoryParent(int? __ParentId)
        {
            try
            {
                if (__ParentId == 0)
                    return "------";

                CMSDataContext db = new CMSDataContext();
                return db.categories.First(c => c._cid == __ParentId).name;
            }
            catch (Exception ex)
            {
                Log.logErr("Category.getAllaCategories", ex);
                return null;
            }
        }

        //*****************************************************


        public bool insertCategory(model.db.category c)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.categories.InsertOnSubmit(c);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Category.insertIntoCategoris", ex);
                return false;
            }
        }



        public bool deleteCategory(int _id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var dc = db.categories.First(d => d._cid == _id);
                db.categories.DeleteOnSubmit(dc);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Category.deleteCategory", ex);
                return false;
            }
        }
        public List<model.db.category> getCategoryChildsByParentId(int pId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var cats = db.categories.Where(c => c.category_parent == pId);
                return cats.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Category.getCategoryChildsByParentId", ex);
                return null;
            }
        }


    }
}