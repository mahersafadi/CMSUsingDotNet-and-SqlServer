using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for Module
/// </summary>
namespace model.module
{
    public class ModuleInModelLayer
    {
        public ModuleInModelLayer()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public model.db.module getModuleByRegion(string region)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.module m = db.modules.First(mm => mm.region == region);
                return m;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.getModuleByRegion", ex);
                return null;
            }
        }

        public model.db.module getModuleByName(string name)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.module m = db.modules.First(mm => mm.title.ToLower() == name.ToLower());
                return m;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.getModuleByName", ex);
                return null;
            }
        }

        public static model.db.module getModuleById(int mId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.module m = db.modules.First(mm => mm._mid == mId);
                return m;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.getModuleByName", ex);
                return null;
            }
        }

        public int insertModule(model.db.module m)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.modules.InsertOnSubmit(m);
                db.SubmitChanges();
                return m._mid;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.saveModule", ex);
            }
            return 0;
        }

        public int insertModuleDetail(model.db.module_detail md)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.module_details.InsertOnSubmit(md);
                db.SubmitChanges();
                return md._mdid;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.insertModuleDetail", ex);
            }
            return 0;
        }

        public model.db.module_detail getModuleDetailByAttributNameAndModuleId(string attrName, int moduleId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                module_detail md = db.module_details.First(mm => mm.attribute_name == attrName && mm.__rmodule == moduleId);
                return md;
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return null;
        }
        public static List<model.db.module> searchModule(string moduelName, string moduleDesc, string region, int catId, int moduleType)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.modules.Where(m => (m.title.Contains(moduelName) || moduelName == "")
                                                && (m.description.Contains(moduleDesc) || moduleDesc == "")
                                                && (m.region == region || region == "0")
                                                && (m.__rcategory == catId || catId == 0)
                                                && (m.type == moduleType || moduleType == 0));
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.searchModule", ex);
            }
            return null;
        }

        public bool updateModule(model.db.module module)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.module md = db.modules.Single(m => m._mid == module._mid);
                md.title = module.title;
                md.description = module.description;
                md.__rcategory = module.__rcategory;
                md.region = module.region;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.updateModule", ex);
                return false;
            }
        }

        public static bool deleteModule(int mId)
        {
            try
            {
                if (deleteModuleDetails(mId))
                {
                    CMSDataContext db = new CMSDataContext();
                    model.db.module m = db.modules.First(mm => mm._mid == mId);
                    if (m.type == 10)
                        Menu.deleteMenuElements(m._mid);
                    db.modules.DeleteOnSubmit(m);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.deleteModule", ex);
            }
            return false;
        }

        public bool updateModuleDetail(model.db.module_detail moduleDetail)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.module_detail md = db.module_details.Single(m => m._mdid == moduleDetail._mdid);
                md.attribute_value = moduleDetail.attribute_value;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.updateModule", ex);
                return false;
            }
        }

        public static bool deleteModuleDetails(int mId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.module_details.Where(md => md.__rmodule == mId);
                foreach (model.db.module_detail md in qry.ToList())
                {
                    db.module_details.DeleteOnSubmit(md);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.deleteModule", ex);
            }
            return false;
        }

        public static List<model.db.module_detail> getModuleDetailsById(int mId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.module_details.Where(md => md.__rmodule == mId);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.getModuleDetailsById", ex);
            }
            return null;
        }

        public static model.db.module_detail getModuleDetailByModuleIdAndAttrbuteName(int? moduleId, string attributeName)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                return db.module_details.First(md => md.__rmodule == moduleId && md.attribute_name.ToLower() == attributeName.ToLower());
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.getModuleDetailByModuleIdAndAttrbuteName", ex);
            }
            return null;
        }
        public static Dictionary<string, string> generateModuleDetailAsDictionary(int mId)
        {
            try
            {
                Dictionary<string, string> res = new Dictionary<string, string>();
                CMSDataContext db = new CMSDataContext();
                var qry = db.module_details.Where(md => md.__rmodule == mId);
                foreach (model.db.module_detail md in qry.ToList())
                {
                    res.Add(md.attribute_name, md.attribute_value);
                }
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("ModuleInModelLayer.generateModuleDetailAsString", ex);
            }
            return null;
        }

        public static string getCSSAttribute(Dictionary<string, string> dic, string attributeName)
        {
            foreach (KeyValuePair<string, string> kv in dic)
            {
                if (kv.Key.ToLower() == attributeName.ToLower())
                    return kv.Value;
            }
            return null;

        }
        public static string getCSSAttribtues(Dictionary<string, string> dic)
        {
            string res = "";
            string[] css = { "width", "height", "background-color", "color" };
            foreach (KeyValuePair<string, string> kv in dic)
            {
                foreach (string str in css)
                {
                    if (kv.Key == str)
                        res += kv.Key + ":" + kv.Value + ";";
                }
            }
            return res;
        }

        public static int getNumOfContents(Dictionary<string, string> dic)
        {

            foreach (KeyValuePair<string, string> kv in dic)
            {
                if (kv.Key == "num_of_contents")
                    return Convert.ToInt32(kv.Value);
            }
            return 0;
        }

        public static string getShowMore(Dictionary<string, string> dic)
        {
            string res = "";
            foreach (KeyValuePair<string, string> kv in dic)
            {
                if (kv.Key == "show_more")
                {
                    res = kv.Value;
                    return res;
                }
            }
            return res;
        }

    }
}