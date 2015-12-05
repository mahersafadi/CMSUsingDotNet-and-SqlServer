using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.module;

/// <summary>
/// Summary description for CategoryModule
/// </summary>
namespace control.cmsmodules
{
    public class CategoryModule : Module
    {
        public CategoryModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public override string generate()
        {
            string res = "";
            //if (Module.isAdmin)
            //{

            if (Security.hasPermission(PermissionName.insert_category.ToString()))
                res += generateNewCat();
            if (Security.hasPermission(PermissionName.category.ToString()))
                res += generateAllCats();
            //}
            return res;
        }

        private string generateAllCats()
        {
            string res = "<table class='gridtable'>";
            res += "<tr><td colspan='3'><h3>All Categories</h3></td></tr>";
            res += "<tr><th>Category Name</th><th style='width:150px;'> Category Parent</th>";
            if (Security.hasPermission(PermissionName.delete_category.ToString()))
                res += "<th></th>";
            res += "</tr>";
            Category c = new Category();
            List<model.db.category> cats = c.getAllaCategories();
            foreach (model.db.category cat in cats)
            {

                res += "<tr>";
                res += "<td>" + cat.name + "</td>";
                res += "<td>" + c.getCategoryParent(cat.category_parent) + "</td>";
                if (Security.hasPermission(PermissionName.delete_category.ToString()))
                    res += "<td><a href='#' onclick='Category.delete_cat(this)' id='" + cat._cid + "'>delete </a></td>";
                res += "</tr>";
            }
            res += "</table><br />";
            return res;
        }

        public string generateNewCat()
        {
            try
            {
                string res = "";
                Category c = new Category();
                List<model.db.category> cats = c.getAllaCategories();
                string lst = "<select id='sub'><option value='0'>------------</option>";
                foreach (model.db.category cat in cats)
                {
                    lst += "<option value='" + cat._cid + "' >" + cat.name + "</option>";
                }
                lst += "</select>";

                res += "<table class='gridtable'>";
                res += "<tr><td colspan='2'><h3>Insert New Category</h3></td></tr>";
                res += "<tr><th>Name</th><td><input id='cat_name' /></td></tr>";
                res += "<tr><th>Sub Categroy</th><td>" + lst + "</td></tr>";
                res += "<tr><td colspan='2'><button id='btn_save' onclick='Category.save_cat()'>Save</button></td></tr>";
                res += "</table><br/>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("Category.generateNewCat", ex);
                return null;
            }
        }

        public static string generateCategoriesAsHtmlSelect(bool incluedNoOption, string selectedValue)
        {
            Category cat = new Category();
            string res = "<select  id='list_categories'>";
            if (incluedNoOption)
                res += "<option value='0'>------</option>";
            foreach (model.db.category c in cat.getAllaCategories())
            {
                string selected = "";
                if (selectedValue != null && selectedValue == c._cid.ToString())
                    selected = "selected";
                res += "<option value='" + c._cid + "' " + selected + ">" + Lang.getByKey(c.name) + "</option>";
            }
            res += "</select>";
            return res;
        }

        #region CategoryHandler
        public static string categoryHandler(string mode, string name, string subCat, string __id, string lastId)
        {
            /**
             * mode = 1 => generate again
             * mode = 2 => save new category
             * mode = 3 => delete category
             * mode = 5 => show category
             * mode = 6 => show more
            ****/
            string res = "";
            if (mode == "1")
            {
                //if (Security.hasPermission(PermissionName.category.ToString()))
                {
                    CategoryModule c = new CategoryModule();
                    res = c.generate();
                }
            }
            else if (mode == "2")
            {
                //if (Security.hasPermission(PermissionName.insert_category.ToString()))
                {
                    model.module.Category cat = new model.module.Category();
                    model.db.category c = new model.db.category();
                    c.name = name;
                    c.category_parent = Convert.ToInt32(subCat);
                    bool isSaved = cat.insertCategory(c);
                    if (isSaved)
                        res = "Saved";
                    else
                        res = "Error";
                }
                //else
                 //   res = "no permission";
            }
            else if (mode == "3")
            {
                //if (Security.hasPermission(PermissionName.delete_category.ToString()))
                {
                    model.module.Category c = new model.module.Category();
                    bool isDeleted = c.deleteCategory(Convert.ToInt32(__id));
                    if (isDeleted)
                        res = "Deleted";
                    else
                        res = "Error";
                }
                //else
                 //   res = "no permission";
            }
            else if (mode == "5")
            {

                ContentModule cm = new ContentModule();
                res = cm.generateXContentsByCategoryId1(Convert.ToInt32(__id), 9, "0", 0);
                //res = "Hello" + 
            }
            else if (mode == "6")
            {
                ContentModule cm = new ContentModule();
                res = cm.generateXContentsByCategoryId1(Convert.ToInt32(__id), 3, "0", Convert.ToInt32(lastId));
                //res = "Hello" + 
            }
            return res;
        }

        private bool saveCategory(string name, string parent)
        {
            try
            {
                model.module.Category cat = new model.module.Category();
                model.db.category catdb = new model.db.category();
                catdb.name = name;
                catdb.category_parent = Convert.ToInt32(parent);
                catdb.create_date = DateTime.Now;
                return cat.insertCategory(catdb);
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.saveCategory", ex);
            }
            return false;
        }
        #endregion

    }
}