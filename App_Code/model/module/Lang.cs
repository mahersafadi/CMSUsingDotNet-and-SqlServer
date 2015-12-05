using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;
using control.cmsmodules;


/// <summary>
/// Summary description for Lang
/// </summary>
namespace model.module
{
    public class Lang
    {
        public Lang()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<lang> getLanguages()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.langs.OrderBy(l => l.create_date);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.getLanguages", ex);
                return null;
            }
        }

        public static bool deleteLanguage(model.db.lang l)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                foreach (lang_detail ld in db.lang_details)
                {
                    if (ld.lang.code == l.code)
                    {
                        db.lang_details.DeleteOnSubmit(ld);
                    }
                }

                var delpatfile = db.langs.SingleOrDefault(p => p.code == l.code);
                db.langs.DeleteOnSubmit(delpatfile);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.deleteLanguage", ex);
            }
            return false;
        }

        public static model.db.lang getLanguageByCode(string code)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                return db.langs.First(l => l.code == code);
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.getLanguageByCode", ex);
            }
            return null;
        }
        public bool inertIntoLanguages(lang l)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.langs.InsertOnSubmit(l);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.inertIntoLanguages", ex);
                return false;
            }
        }

        public static List<model.db.lang_detail> getLanguageDetails()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.lang_details.OrderBy(l => l.k);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.getLanguageDetails", ex);
                return null;
            }
        }

        public bool inertIntoLanguageDetail(lang_detail l)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.lang_details.InsertOnSubmit(l);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.inertIntoLanguageDetail", ex);
                return false;
            }
        }

        public bool deleteLanguageDetail(int _id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                lang_detail dtl = db.lang_details.First(d => d._ldid == _id);
                db.lang_details.DeleteOnSubmit(dtl);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.deleteLanguageDetail", ex);
                return false;
            }
        }

        public static string getByKey(string key)
        {
            //Based on session, get key
            //if not exist return key
            try
            {
                CMSDataContext db = new CMSDataContext();
                if (LangModule.sessionLang == null || LangModule.sessionLang == "")
                    LangModule.sessionLang = "en";

                key = db.lang_details.First(d => d.k == key && d.lang.code == LangModule.sessionLang).v;

            }
            catch (Exception ex)
            {
                Log.logErr("Lang.getByKey", ex);
            }
            return key;
        }

        public static string getDirection()
        {
            //Based on session get from Lang Table direction
            try
            {
                CMSDataContext db = new CMSDataContext();
                if (LangModule.sessionLang == null || LangModule.sessionLang == "")
                    LangModule.sessionLang = "en";

                return db.langs.First(l => l.code == LangModule.sessionLang).direction;
            }
            catch (Exception ex)
            {
                Log.logErr("Lang.getDirection", ex);
            }
            return null;

        }

        public static string getAntiDirection(string sessionLang)
        {
            return getDirection() == "rtl" ? "ltr" : "rtl";
        }
    }
}