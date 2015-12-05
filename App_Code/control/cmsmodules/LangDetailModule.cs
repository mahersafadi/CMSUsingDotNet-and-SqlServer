using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using control.cmsmodules;
using model.module;


/// <summary>
/// Summary description for LangDetailModule
/// </summary>
namespace control.cmsmodules
{


    public class LangDetailModule : Module
    {
        public static String sessionLang = "";
        public LangDetailModule()
        {
            //
            // TODO: Add constructor logic here
            //

        }



        public override string generate()
        {
            string res = "";

            res += generateLangDetails();
            res += "</td>";
            res += "</tr><table>";

            return res;
        }




        private string generateLangDetails()
        {
            String res = "";

            List<model.db.lang> langs = Lang.getLanguages();
            string langsInList = "<select id='lang_lan'>";
            foreach (model.db.lang lan in langs)
            {
                langsInList += "<option value='" + lan.code + "'>" + lan.name + "</option>";
            }
            langsInList += "</select>";

            res += "<table class='gridtable'>";
            res += "<tr><td colspan='2'><h3>Insert New Key</h3></td></tr>";
            res += "<tr><th>Key</th><td><input id='lang_key' /></td></tr>";
            res += "<tr><th>Value</th><td><input id='lang_value' /></td></tr>";
            res += "<tr><th>Language</th><td>" + langsInList + "</td></tr>";
            res += "<tr><td colspan='2'><button id='btn_save_lang_detail' onclick='LangDetail.save_lang_detail()'>Save</button></td></tr>";
            res += "</table>";

            res += "<table class='gridtable'>";
            res += "<tr><td colspan='4'><h3>" + Lang.getByKey("all_lang_keys") + "</h3></td></tr>";
            res += "<tr><th>" + Lang.getByKey("key") + "</th><th>" + Lang.getByKey("Value") + "</th><th>" + Lang.getByKey("language") + "</th><th></th></tr>";

            List<model.db.lang_detail> dtls = Lang.getLanguageDetails();
            foreach (model.db.lang_detail dtl in dtls)
            {
                res += "<tr>";
                res += "<td>" + dtl.k + "</td>";
                res += "<td>" + dtl.v + "</td>";
                res += "<td>" + dtl.lang.name + "</td>";
                res += "<td><a href='#' id='" + dtl._ldid + "' onclick='LangDetail.delete_lang_detail(this)' > delete </a></td>";
                res += "</tr>";
            }
            res += "</table><br />";




            return res;
        }

        public static bool deleteLanguage(string code)
        {
            return Lang.deleteLanguage(Lang.getLanguageByCode(code));
        }


        #region LangDetailHandler
        public static string langDetailHandler(string mode, string key, string val, string lang, string ___id)
        {
            string res = "";

            /**
             * mode = 1 => generate again
             * mode = 2 => save new language Detail
             * mode = 3 => delete language detail
         
             * ***/

            if (mode == "1")
            {
                LangDetailModule l = new LangDetailModule();
                res = l.generate();
            }

            else if (mode == "2")
            {

                bool isSaved = saveLangDetail(key, val, lang);
                if (isSaved)
                    res = "Saved";
                else
                    res = "Error";
            }
            else if (mode == "3")
            {

                model.module.Lang l = new model.module.Lang();
                bool isDeleted = l.deleteLanguageDetail(Convert.ToInt32(___id));
                if (isDeleted)
                    res = "Deleted";
                else
                    res = "Error";
            }

            return res;
        }

        private static bool saveLangDetail(string key, string val, string lang)
        {
            model.module.Lang l = new model.module.Lang();
            model.db.lang_detail dtl = new model.db.lang_detail();
            dtl.k = key;
            dtl.v = val;
            dtl.__rlang = lang;
            dtl.create_date = DateTime.Now;
            return l.inertIntoLanguageDetail(dtl);
        }
        #endregion
    }
}