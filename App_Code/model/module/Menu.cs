using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for Menu
/// </summary>
namespace model.module
{
    public class Menu
    {
        public Menu()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<menu> getMenuByName(string menuName)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.menus.Where(m => m.module.title == menuName && m.parent.Value == 0).OrderBy(m => m.create_date);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Menu.getMenuByName", ex);
            }
            return null;
        }

        public List<menu> getMenuChildsByParentId(int parentId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.menus.Where(m => m.parent == parentId).OrderBy(m => m.create_date);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Menu.getMenuChildsByParentId", ex);
            }
            return null;
        }

        public List<menu> getParentMenuEelementsByModuleId(int moduleId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.menus.Where(m => m.parent == 0 && m.__rmodule == moduleId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Menu.getParentMenuEelementsByModuleId", ex);
            }
            return null;
        }

        public static List<model.db.module> getMenuModules()
        {

            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.modules.Where(m => m.type == 10);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Menu.getMenuByModuleId", ex);
            }
            return null;
        }


        public static List<model.db.module> getMenuModulesFromMenuTable()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.modules.Where(m => m.type == 10 && m.region != null);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Menu.getMenuByModuleId", ex);
            }
            return null;
        }

        public static List<menu> getMenuByModuleId(int? moduleId)
        {

            try
            {
                if (moduleId == null)
                    return null;
                CMSDataContext db = new CMSDataContext();
                var qry = db.menus.Where(m => m.__rmodule == moduleId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Menu.getMenuByModuleId", ex);
            }
            return null;
        }

        public static string getMenuNameById(int menuId)
        {
            try
            {
                if (menuId == 0 || menuId == null)
                    return "-----";
                CMSDataContext db = new CMSDataContext();
                return db.menus.First(m => m._mid == menuId).name;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Menu.getMenuNameById", ex);
            }
            return "";
        }

        public static menu getMenuElementByMenuId(int menuId)
        {

            try
            {
                if (menuId == null)
                    return null;
                CMSDataContext db = new CMSDataContext();
                menu qry = db.menus.First(m => m._mid == menuId);
                return qry;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Menu.getMenuByModuleId", ex);
            }
            return null;
        }

       public static bool insertMenu(menu m)
        {
            // if in the root then parent = 0 else parent = the id of parent menu
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.menus.InsertOnSubmit(m);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Menu.insertMenu", ex);
                return false;
            }
        }

        public static bool updateMenu(menu m)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                menu mm = db.menus.Single(mmm => mmm._mid == m._mid);
                mm.name = m.name;
                mm.parent = m.parent;
                mm.__rmodule = m.__rmodule;
                mm.__rcontent = m.__rcontent;
                mm.__category = m.__category;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Menu.updateMenu", ex);
            }
            return false;
        }

        public static bool deleteMenu(int menuId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                menu mm = db.menus.Single(mmm => mmm._mid == menuId);
                db.menus.DeleteOnSubmit(mm);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Menu.deleteMenu", ex);
            }
            return false;
        }

        public static bool deleteMenuElements(int moduleId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.menus.Where(m => m.__rmodule == moduleId);
                foreach (var item in qry.ToList())
                {
                    db.menus.DeleteOnSubmit(item);
                }
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Menu.deleteMenuElements", ex);
            }
            return false;


        }
    }
}