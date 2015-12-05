using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using control.cmsmodules;
using model.module;

/// <summary>
/// Summary description for LangModule
/// </summary>
/// 
namespace control.cmsmodules
{


    public class LangModule : Module
    {
        public static String sessionLang = "en";
        public LangModule()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        public override string generate()
        {
            string res = "";
            res += generateLangSelection();
            res += "<table style='width:100%; vertical-align:top'><tr>";
            res += "<td style='vertical-align:text-top;'>";
            res += generateLang();
            res += "</td>";
            res += "<td>";
            res += "<td style='width:200px'>";
            res += "</td>";
            res += "<td>";
            // res += generateLangDetails();
            // res += "</td>";
            // res += "</tr><table>";

            return res;
        }

        public static string generateLangSelection()
        {
            string res = "<select id='lang_select' onchange='Lang.change_lang(this)'>";
            List<model.db.lang> langs = Lang.getLanguages();
            foreach (model.db.lang lan in langs)
            {
                string selected = lan.code == sessionLang ? "selected" : "";
                res += "<option value='" + lan.code + "' " + selected + ">" + lan.name + "</option>";
            }
            res += "</select>";
            return res;
        }

        private string generateLang()
        {

            string res = "<table class='gridtable'>";
            res += "<tr><td colspan='4'><h3>" + Lang.getByKey("all_languages") + "</h3></td></tr>";
            res += "<tr><th>" + Lang.getByKey("code") + "</th><th>" + Lang.getByKey("name") + "</th><th>" + Lang.getByKey("direction") + "</th><th></th></tr>";
            List<model.db.lang> langs = Lang.getLanguages();
            foreach (model.db.lang lan in langs)
            {
                res += "<tr>";
                res += "<td>" + lan.code + "</td>";
                res += "<td>" + lan.name + "</td>";
                res += "<td>" + lan.direction + "</td>";
                res += "<td><a href='#' onclick='Lang.delete_lang(this)' id='" + lan.code + "' >delete </a></td>";
                res += "</tr>";
            }
            res += "</table><br />";

            res += "<table class='gridtable'>";
            res += "<tr><td colspan='2'><h3>" + Lang.getByKey("new_record") + "</h3></td></tr>";
            res += "<tr><th>Code</th><td><input id='lang_code' /></td></tr>";
            res += "<tr><th>Name</th><td><input id='lang_name' /></td></tr>";
            res += "<tr><th>Direction</th><td><select id='lang_dir' ><option value='ltr' >Left to right</option><option value='rtl' >Right to left</option></select></td></tr>";
            res += "<tr><td colspan='2'><button id='btn_save' onclick='Lang.save_lang()'>Save</button></td></tr>";
            res += "</table>";
            return res;

        }
        private string generateLangDetails()
        {
            String res = "<table class='gridtable'>";
            res += "<tr><td colspan='4'><h3>" + Lang.getByKey("all_lang_keys") + "</h3></td></tr>";
            res += "<tr><th>Key</th><th>Value</th><th>Language</th><th></th></tr>";

            List<model.db.lang_detail> dtls = Lang.getLanguageDetails();
            foreach (model.db.lang_detail dtl in dtls)
            {
                res += "<tr>";
                res += "<td>" + dtl.k + "</td>";
                res += "<td>" + dtl.v + "</td>";
                res += "<td>" + dtl.lang.name + "</td>";
                res += "<td><a href='#' id='" + dtl._ldid + "' onclick='Lang.delete_lang_detail(this)' > delete </a></td>";
                res += "</tr>";
            }
            res += "</table><br />";

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
            res += "<tr><td colspan='2'><button id='btn_save_lang_detail' onclick='Lang.save_lang_detail()'>Save</button></td></tr>";
            res += "</table>";
            return res;
        }

        public static bool deleteLanguage(string code)
        {
            return Lang.deleteLanguage(Lang.getLanguageByCode(code));
        }


        #region LanguageHandler
        public static string languageHandler(string mode, string name, string langcode, string dir)
        {
            string res = "";

            /**
             * mode = 1 => generate again
             * mode = 2 => save new language
             * mode = 3 => get direction
             * mode = 4 => delete language key
       
             * ***/
            if (mode == "1")
            {

                LangModule l = new LangModule();
                res = l.generate();
            }
            else if (mode == "2")
            {
                bool isSaved = saveLang(langcode, name, dir);
                if (isSaved)
                    res = "Saved";
                else
                    res = "Error";
            }

            else if (mode == "3")
            {
                LangModule.sessionLang = langcode;
                LangModule l = new LangModule();
                res = l.generate();
                res = model.module.Lang.getDirection();

            }
            else if (mode == "4")
            {
                if (LangModule.deleteLanguage(langcode))
                    res = "Deleted";
                else
                    res = "Error";

            }
            return res;
        }

        private static bool saveLang(string code, string name, string dir)
        {
            model.module.Lang l = new model.module.Lang();
            model.db.lang lan = new model.db.lang();
            lan.name = name;
            lan.code = code;
            lan.direction = dir;
            lan.create_date = DateTime.Now;
            return l.inertIntoLanguages(lan);
        }
        #endregion
    }
}