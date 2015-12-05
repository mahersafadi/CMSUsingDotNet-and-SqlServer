using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.module;
using System.IO;
using System.Reflection;
using model.db;

/// <summary>
/// Summary description for Module3
/// 
/// </summary>
namespace control.cmsmodules
{
    public class Module
    {
        public model.db.module module;
        public Dictionary<string, string> mdAsDictionary = new Dictionary<string, string>();
        public static bool isAdmin = true;
        public bool isAll = false;
        public static string appcodefolder = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "view");
        public static string module_details = "/assets/module_details.txt";
        public static string regions = "/assets/regions.txt";
        public static bool isLogedIn = false;

        public static string[] modules = {
            /*0*/   "", /*1*/  "control.cmsmodules.MenuModule",         /*2*/ "control.cmsmodules.LangModule" ,
            /*3*/   "control.cmsmodules.CategoryModule",                /*4*/ "control.cmsmodules.MenuModule",   
            /*5*/   "control.cmsmodules.ContentModule",                 /*6*/ "control.cmsmodules.LangDetailModule", 
            /*7*/   "control.cmsmodules.Module",                        /*8*/  "control.cmsmodules.Security", /*9*/ "", 
            /*10*/  "control.cmsmodules.MenuModule",                    /*11*/ "control.cmsmodules.VerticalBasic1Module", 
            /*12*/  "control.cmsmodules.LatestNewsModule",              /*13*/"control.cmsmodules.SlideShowModule", 
            /*14*/  "control.cmsmodules.EngSortModule",                 /*15*/ "control.cmsmodules.VerticalBasic2Module", 
            /*16*/  "control.cmsmodules.VerticalBasic3Module",          /*17*/ "control.cmsmodules.VerticalBasic4Module", 
            /*18*/  "control.cmsmodules.SearchModule",                  /*19*/"control.cmsmodules.VerticalBasic5Module", 
            /*20*/  "control.cmsmodules.LawDecreePopulationModule",     /*21*/ "control.cmsmodules.bnp.OfferModule", 
            /*22*/  "control.cmsmodules.bnp.BnpLatestNewsModule",       /*23*/ "control.cmsmodules.JqTilesSlideShow",  
            /*24*/  "control.cmsmodules.SiteAdminMenuModule",           /*25*/  "control.cmsmodules.bnp.BnpMenu",     
            /*26*/  "control.cmsmodules.bnp.BnpClientProfile",          /*27*/ "control.cmsmodules.GalleryModule",  
            /*28*/  "control.cmsmodules.GalleryModuleAdmin",            /*29*/ "control.cmsmodules.ClientManagmentAdmin",     
            /*30*/ "control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt",                                                  
            /*31*/  "control.cmsmodules.PannerModule",     
            /*32*/ "control.cmsmodules.bnp.SponsorModule",      
            /*33*/ "",  
            /*34*/ "",                                                  /*35*/ "",     
            /*36*/ "",                                                  /*37*/  "",
            /*38*/ "",                                                  /*39*/ "", 
            /*40*/ "",                                                  /*41*/ "",
            /*42*/ "",                                                  /*43*/  "",
            /*44*/ "",                                                  /*45*/ "",
            /*46*/ "",                                                  /*47*/ "",     
            /*48*/ "",                                                  /*49*/""};
        public static string[] dbmodels = {/*0*/"",/*1*/"",/*2*/"", /*3*/"",/*4*/"",/*5*/"",/*6*/"",/*7*/"",/*8*/"",/*9*/"",
                                           /*10:*/"menu",           /*11:*/"vertical_basic1",   /*12:*/"latest_news",
                                           /*13:*/"slide_show",     /*14:*/ "eng_sort",         /*15:*/"vertical_basic2", 
                                           /*16:*/"vertical_basic3",/*17:*/"vertical_basic4",   /*18:*/"search_engine", 
                                           /*19:*/"vertical_basic5",/*20:*/"law_decree_population"};
        public Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Module(model.db.module m)
        {
            this.module = m;
        }

        virtual public void setDBModel(model.db.module m)
        {
            this.module = m;
            if (mdAsDictionary == null || mdAsDictionary.Count() == 0)
            {
                mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
            }
        }

        virtual public void setDBModel(int moduleId)
        {
               this.module = model.module.ModuleInModelLayer.getModuleById(moduleId);
               if (mdAsDictionary == null || mdAsDictionary.Count() == 0)
               {
                   mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
               }  
        }

        public virtual string generate()
        {
            //Get Parent Id = 0;
            //for each one, get its children
            string res = "";
            if (Security.hasPermission(PermissionName.module.ToString()))
                res += "<button onclick='Module.search_module(this)'>" + Lang.getByKey("search_module") + "</button>";
            if (Security.hasPermission(PermissionName.insert_module.ToString()))
                res += "<button onclick='Module.new_module(this)'>" + Lang.getByKey("new_module") + "</button>";
            res += "<div id='div_module'></div>";
            res += "<div id='div_module_search_result'></div>";
            return res;
        }

        public string generateNewModule()
        {
            string res = "";
            try
            {
                res = @"<table class='gridtable'>
                            <tr>
                                <th colspan='2'>" + Lang.getByKey("insert_new_record") + @"</th>
                            </tr>
                            <tr>
                                <th >
                                    " + Lang.getByKey("module_name") + @"
                                </th>
                                <td>
                                    <input type='text' id='module_name' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_description") + @"
                                </th>
                                <td>
                                    <input type='text' id='module_description' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_region") + @"
                                </th>
                                <td>
                                    <select id='module_region'>";

                foreach (KeyValuePair<string, string> item in getRegions())
                {
                    res += "<option value='" + item.Value + "'" + " > " + item.Key + "</option>";
                }
                res += @"   </select></td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("category_name") + @"
                                </th>
                                <td>
                                    " + CategoryModule.generateCategoriesAsHtmlSelect(false, null) + @"
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_type") + @"
                                </th>
                                <td>
                                    " + generateModulesAsHTMLSelect("onchange='Module.get_module_details(this)'") + @"
                                </td>
                            </tr>
                            <table class='gridtable'  id='tbl_module_details'>" + generateModuleDetailsAsHTMLTableRows("menu") + @"
                            </table>
                            <tr>
                                <th colspan='2'>
                                   <button id='save_module' onclick='Module.save_module(this)'>" + Lang.getByKey("ok") + @"</button> &nbsp;
                                   <button id='cancel' onclick='Module.cancel_module(this)'>" + Lang.getByKey("cancel") + @"</button>
                                </th>
                            </tr>
                            </table>
                        </table>";

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.generateNewModule", ex);
            }
            return res;
        }

        public string generateSearchModule()
        {
            string res = "";
            try
            {
                res = @"<table class='gridtable'>
                            <tr>
                                <th colspan='4'>" + Lang.getByKey("search_module") + @"</th>
                            </tr>
                            <tr>
                                <th >
                                    " + Lang.getByKey("module_name") + @"
                                </th>
                                <td>
                                    <input type='text' id='module_name' />
                                </td>
                            
                                <th>
                                    " + Lang.getByKey("module_description") + @"
                                </th>
                                <td>
                                    <input type='text' id='module_description' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_region") + @"
                                </th>
                                <td>
                                    <select id='module_region'>";
                foreach (KeyValuePair<string, string> item in getRegions())
                {
                    res += "<option value='" + item.Value + "'" + " > " + item.Key + "</option>";
                }
                res += @"   </select></td>
                                </td>
                                <th>
                                    " + Lang.getByKey("category_name") + @"
                                </th>
                                <td>
                                    " + CategoryModule.generateCategoriesAsHtmlSelect(true, null) + @"
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_type") + @"
                                </th>
                                <td colspan='3'>
                                    " + generateModulesAsHTMLSelect("onchange='Module.get_module_details(this)'", true) + @"
                                </td>
                            </tr>
                            <tr>
                                <th colspan='4'>
                                   <button id='do_search_module' onclick='Module.do_search_module(this)'>" + Lang.getByKey("ok") + @"</button> &nbsp;
                                   <button id='cancel' onclick='Module.cancel_module(this)'>" + Lang.getByKey("cancel") + @"</button>
                                </th>
                            </tr>
                          </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.generateSearchModule", ex);
            }
            return res;
        }

        public string generateModuleSearchResult(string moduelName, string moduleDesc, string region, int catId, int moduleType)
        {
            string res = "";
            res = @"<table class='gridtable'>";
            try
            {
                List<model.db.module> modules = model.module.ModuleInModelLayer.searchModule(moduelName, moduleDesc, region, catId, moduleType);
                if (modules != null)
                {
                    if (modules.Count > 0)
                    {
                        res += "<th></th>";
                        res += "<h4>" + Lang.getByKey("search_result") + "</h4>";
                        res += "<th>" + Lang.getByKey("module_name") + "</th>";
                        res += "<th>" + Lang.getByKey("module_desc") + "</th>";
                        res += "<th>" + Lang.getByKey("region") + "</th>";
                        res += "<th>" + Lang.getByKey("category_name") + "</th>";
                        res += "<th>" + Lang.getByKey("module_type") + "</th>";
                        foreach (model.db.module m in modules)
                        {
                            if (m.__rcategory != null)
                            {
                                res += "<tr>";
                                res += "<td><a href='#' onclick='Module.edit_module(this)' id='" + m._mid + "' > " + Lang.getByKey("edit") + @"</a> &nbsp;
                                            <a href='#' onclick='Module.delete_module(this)' id='" + m._mid + "' > " + Lang.getByKey("delete") + @"</a>
                                        </td>";
                                res += "<td>" + m.title + "</td>";
                                res += "<td>" + m.description + "</td>";
                                res += "<td>" + m.region + "</td>";
                                res += "<td>" + m.category.name + "</td>";
                                res += "<td>" + convertModuleTypeToName(m.type.ToString()) + "</td>";
                                res += "</tr>";
                            }
                        }
                    }
                    else
                    {
                        res = "<h4>" + Lang.getByKey("no_results") + "</h4>";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.generateModuleSearchResult", ex);
            }
            return res;
        }

        public string generateModulesAsHTMLSelect(string properties, bool includeNoOption = false)
        {
            string res = "<select id='module_types' " + properties + ">";
            if (includeNoOption)
                res += "<option value='0' >------</option>";
            try
            {
                string[] lines = System.IO.File.ReadAllLines(appcodefolder + module_details);
                for (int i = 0; i < lines.Length; i++)
                {
                    res += "<option value='" + lines[i].Substring(0, lines[i].IndexOf(":")) + "'>" + Lang.getByKey(lines[i].Substring(0, lines[i].IndexOf(":"))) + "</option>";
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Modules.generateModuleNamesAsHTMLSelect", ex);
            }
            return res;
        }

        public string generateModuleDetailsAsHTMLTableRows(string moduleName, int? moduleId = null)
        {
            string res = "";
            try
            {
                string[] lines = System.IO.File.ReadAllLines(appcodefolder + module_details);
                foreach (string line in lines)
                {
                    if (line.Substring(0, line.IndexOf(":")) == moduleName)
                    {
                        string[] attrs = line.Split(new string[] { "];[" }, StringSplitOptions.None);
                        for (int i = 0; i < attrs.Length; i++)
                        {


                            if (i == 0)
                            {
                                //omit module name from the first
                                attrs[i] = attrs[i].Substring(attrs[i].IndexOf("[") + 1);
                            }
                            string key = attrs[i].Substring(0, attrs[i].IndexOf(","));
                            string val = attrs[i].Substring(attrs[i].IndexOf(",") + 1, 1);


                            //when edit mode get values of attribute from db
                            model.db.module_detail md = null;
                            if (moduleId != null)
                                md = model.module.ModuleInModelLayer.getModuleDetailByModuleIdAndAttrbuteName(moduleId, key);

                            switch (val)
                            {
                                case "1": //text
                                    res += "<tr>";
                                    res += "<th style='width:129px'>";
                                    res += Lang.getByKey(key);
                                    res += "</th>";
                                    res += "<td>";
                                    string color = "";
                                    if (key.ToLower().EndsWith("color")) // for color
                                        color = "class='color'";

                                    string db_value = "";
                                    if (moduleId != null)
                                        db_value = md.attribute_value;

                                    res += "<input type='text' id='" + key + "' " + color + " value='" + db_value + "' />";
                                    res += "</td>";
                                    break;
                                case "2": //select
                                    res += "<tr>";
                                    res += "<th>";
                                    res += Lang.getByKey(key);
                                    res += "</th>";
                                    res += "<td>";
                                    res += "<select id='" + key + "'>";

                                    string[] options = attrs[i].Substring(attrs[i].LastIndexOf(",") + 1).Split(new string[] { "};{" }, StringSplitOptions.None);
                                    foreach (string option in options)
                                    {
                                        string selected = "";

                                        if (option.StartsWith("{"))
                                        {
                                            if (moduleId != null)
                                                if (md.attribute_value == option.Substring(1, option.IndexOf(":") - 1))
                                                    selected = "selected=selected";

                                            res += "<option " + selected + " value='" + option.Substring(1, option.IndexOf(":") - 1) + "'>" + option.Substring(option.IndexOf(":") + 1) + "</option>";
                                        }
                                        else if (option.EndsWith("}"))
                                        {
                                            if (moduleId != null)
                                                if (md.attribute_value == option.Substring(0, option.IndexOf(":")))
                                                    selected = "selected=selected";
                                            string ss = option.Substring(option.IndexOf(":") + 1);
                                            ss = ss.Substring(0, ss.Length - 1);
                                            res += "<option " + selected + " value='" + option.Substring(0, option.IndexOf(":")) + "'>" + ss + "</option>";
                                        }
                                        else
                                        {
                                            if (moduleId != null)
                                                if (md.attribute_value == option.Substring(0, option.IndexOf(":")))
                                                    selected = "selected=selected";

                                            res += "<option " + selected + " value='" + option.Substring(0, option.IndexOf(":")) + "'>" + option.Substring(option.IndexOf(":") + 1) + "</option>";
                                        }
                                    }

                                    res += "</select>";
                                    res += "</td>";
                                    res += "</tr>";
                                    break;
                                case "3": //checkbox
                                    res += "<tr>";
                                    res += "<th>";
                                    res += Lang.getByKey(key);
                                    res += "</th>";
                                    res += "<td>";
                                    string _checked = "";
                                    if (moduleId != null)
                                        if (md.attribute_value == "true")
                                            _checked = "checked";
                                    res += "<input type='checkbox' id='" + key + "' " + _checked + "/>";
                                    res += "</td>";
                                    res += "</tr>";
                                    break;
                                default:
                                    break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Modules.getMOduleDetails", ex);
            }
            return res;
        }

        public string generateNewMenu(int? moduleId)
        {
            string res = "";
            try
            {
                res += @"<tr>
                            <td>" + Lang.getByKey("element_menu_name") + @"</td>
                            <td><input  type='text' id='element_menu_name' /> </td>
                         <tr>
                        <tr>
                            <td>" + Lang.getByKey("element_menu_parent") + @"</td>
                            <td>" + MenuModule.generateMenuByModuleIdasSelect(moduleId, true, null) + @" </td>
                         <tr>
                        <tr>
                            <td>" + Lang.getByKey("content_name") + @"</td>
                            <td>" + ContentModule.generateAllContentDetails(LangModule.sessionLang, null) + @" </td>
                         <tr>
                         <tr>
                            <td>
                                <button id='save_menu_module' onclick='Module.save_menu_module(this)'>" + Lang.getByKey("ok") + @"</button> &nbsp;
                                <button id='cancel' onclick='Module.cancel_module(this)'>" + Lang.getByKey("cancel") + @"</button>
                            </td>
                         </tr>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmscontrolmodules.Module.generateNewMenu", ex);
            }
            return res;
        }

        public string generateEditModule(int moduleId)
        {
            try
            {
                string res = "";
                model.db.module module = model.module.ModuleInModelLayer.getModuleById(moduleId);

                if (module != null)
                {

                    res = @"<table class='gridtable'>
                            <tr>
                                <th colspan='2'>" + Lang.getByKey("edit_record") + @"</th>
                            </tr>
                            <tr>
                                <th >
                                    " + Lang.getByKey("module_name") + @"
                                </th>
                                <td>
                                    <input type='text' id='module_name' value='" + module.title + @"'/>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_description") + @"
                                </th>
                                <td>
                                    <input type='text' id='module_description' value='" + module.description + @"' />
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_region") + @"
                                </th>
                                <td>
                                    <select id='module_region'>";

                    foreach (KeyValuePair<string, string> item in getRegions())
                    {
                        string selected = "";
                        if (module.region == item.Value)
                            selected = "selected=selected";
                        res += "<option " + selected + " value='" + item.Value + "'" + " > " + item.Key + "</option>";
                    }
                    res += @"   </select></td>

                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("category_name") + @"
                                </th>
                                <td>
                                    " + CategoryModule.generateCategoriesAsHtmlSelect(false, module.__rcategory.ToString()) + @"
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    " + Lang.getByKey("module_type") + @"
                                </th>
                                <td>
                                    " + convertModuleTypeToName(module.type.ToString()) + @"
                                </td>
                            </tr>
                            <table class='gridtable'  id='tbl_module_details'>" + generateModuleDetailsAsHTMLTableRows(convertModuleTypeToName(module.type.ToString()), module._mid) + @"
                            </table><tr>
                                <th colspan='2'>
                                   <button id='" + module._mid + "' onclick='Module.update_module(this)'>" + Lang.getByKey("ok") + @"</button> &nbsp;
                                   <button id='cancel' onclick='Module.cancel_edit_module(this)'>" + Lang.getByKey("cancel") + @"</button>
                                </th>
                            </tr>
                            </table>
                        </table>";
                }
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Modules.generateEditModule", ex);
            }
            return null;
        }
        public static Module getModuleByRegion(string region)
        {
            try
            {
                model.module.ModuleInModelLayer m = new model.module.ModuleInModelLayer();
                model.db.module mm = m.getModuleByRegion(region);
                Type t = Type.GetType(Module.modules[(int)mm.type]);
                control.cmsmodules.Module moduleToGenerate = (control.cmsmodules.Module)Activator.CreateInstance(t);
                t.InvokeMember("setDBModel", BindingFlags.InvokeMethod, null, moduleToGenerate, new object[] { mm });
                //, new object[] { mm }
                return moduleToGenerate;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.getModuleByRegion", ex);
            }
            return null;
        }

        public static Module getModuleByName(string name)
        {
            try
            {
                model.module.ModuleInModelLayer m = new model.module.ModuleInModelLayer();
                model.db.module mm = m.getModuleByName(name);
                Type t = Type.GetType(Module.modules[(int)mm.type]);
                control.cmsmodules.Module moduleToGenerate = (control.cmsmodules.Module)Activator.CreateInstance(t);
                return moduleToGenerate;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.getModuleByName", ex);
            }
            return null;
        }

        public string saveModule(string module, string moduleDetails)
        {
            return "";
        }
        public static string ModuleHandler(string mode, string moduleType, string name, string m_desc, string m_region, string cat, string m_type, string moduleDetails, string moduleId, string elementMenuName, string parent, string contentId)
        {
            try
            {
                model.module.ModuleInModelLayer mm = new ModuleInModelLayer();
                Module m = new Module();
                if (mode == "1")
                {
                    if (Security.hasPermission(PermissionName.module.ToString()))
                        return m.generate();
                }
                else if (mode == "2")
                {
                    if (Security.hasPermission(PermissionName.insert_module.ToString()))
                        return m.generateNewModule();
                }
                else if (mode == "3")
                {
                    if (Security.hasPermission(PermissionName.module.ToString()))
                        return m.generateModuleDetailsAsHTMLTableRows(moduleType);
                }
                else if (mode == "4")
                {
                    if (Security.hasPermission(PermissionName.insert_module.ToString()))
                    {
                        model.db.module mod = new model.db.module();
                        mod.__rcategory = Convert.ToInt32(cat);
                        mod.title = name;
                        mod.description = m_desc;
                        mod.region = m_region;
                        mod.type = convertModuleTypeNameToNumber(m_type);

                        int mId = mm.insertModule(mod);

                        string[] attributes = moduleDetails.Split(',');

                        foreach (string attr in attributes)
                        {
                            model.db.module_detail md = new model.db.module_detail();
                            md.attribute_name = attr.Substring(0, attr.IndexOf(":"));
                            md.attribute_value = attr.Substring(attr.IndexOf(":") + 1);
                            md.__rmodule = mId;
                            mm.insertModuleDetail(md);
                        }
                        return "saved";
                    }
                }
                else if (mode == "5")
                {
                    if (Security.hasPermission(PermissionName.module.ToString()))
                        return m.generateSearchModule();
                }
                else if (mode == "6")
                {
                    if (Security.hasPermission(PermissionName.module.ToString()))
                        return m.generateModuleSearchResult(name, m_desc, m_region, Convert.ToInt32(cat), convertModuleTypeNameToNumber(m_type));
                }
                else if (mode == "7")
                {
                    if (Security.hasPermission(PermissionName.update_module.ToString()))
                        return m.generateEditModule(Convert.ToInt32(moduleId));
                }
                else if (mode == "8")
                {
                    if (Security.hasPermission(PermissionName.delete_module.ToString()))
                    {
                        if (model.module.ModuleInModelLayer.deleteModule(Convert.ToInt32(moduleId)))
                            return "deleted";
                        else
                            return "error";
                    }
                }
                else if (mode == "9")
                {

                    if (Security.hasPermission(PermissionName.update_module.ToString()))
                    {
                        model.db.module mod = new model.db.module();
                        mod.__rcategory = Convert.ToInt32(cat);
                        mod.title = name;
                        mod.description = m_desc;
                        mod.region = m_region;
                        mod.type = convertModuleTypeNameToNumber(m_type);
                        mod._mid = Convert.ToInt32(moduleId);
                        if (mm.updateModule(mod))
                        {
                            string[] attributes = moduleDetails.Split(',');
                            foreach (string attr in attributes)
                            {
                                model.db.module_detail md = mm.getModuleDetailByAttributNameAndModuleId(attr.Substring(0, attr.IndexOf(":")), Convert.ToInt32(moduleId));
                                if (md != null)
                                {
                                    md.attribute_name = attr.Substring(0, attr.IndexOf(":"));
                                    md.attribute_value = attr.Substring(attr.IndexOf(":") + 1);
                                    mm.updateModuleDetail(md);
                                }
                            }
                            return "updated";
                        }
                        return "error";
                    }
                }
                else if (mode == "10")
                {
                    model.db.menu menu = new model.db.menu();
                    menu.name = elementMenuName;
                    menu.parent = Convert.ToInt32(parent);
                    menu.__rmodule = 15;
                    menu.__rcontent = Convert.ToInt32(contentId);

                    return model.module.Menu.insertMenu(menu).ToString();
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.ModuleHandler", ex);
            }
            return null;
        }

        public string newSeect()
        {
            string res = "";
            try
            {
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return res;
        }

        public static string convertModuleTypeToName(string t)
        {
            try
            {
                int i = int.Parse(t);
                return Module.dbmodels[i];
                //switch (t)
                //{
                //    case "10":
                //        return "menu";
                //    case "11":
                //        return "vertical_basic1";
                //    case "12":
                //        return "latest_news";
                //    case "13":
                //        return "slide_show";
                //    case "14":
                //        return "eng_sort";
                //    case "15":
                //        return "vertical_basic2";
                //    case "16":
                //        return "vertical_basic3";
                //    case "17":
                //        return "vertical_basic4";
                //    case "18":
                //        return "search_engine";
                //    case "19":
                //        return "vertical_basic5";
                //    case "20":
                //        return "law_decree_population";
                //}
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.convertModuleType", ex);
            }
            return null;
        }

        public static int convertModuleTypeNameToNumber(string t)
        {
            try
            {
                int k = 0;
                while (k < Module.dbmodels.Length && Module.dbmodels[k] != t)
                    k++;
                if (Module.dbmodels[k] == t)
                    return k;
                //switch (t)
                //{
                //    case "menu":
                //        return 10;
                //    case "vertical_basic1":
                //        return 11;
                //    case "latest_news":
                //        return 12;
                //    case "slide_show":
                //        return 13;
                //    case "eng_sort":
                //        return 14;
                //    case "vertical_basic2":
                //        return 15;
                //    case "vertical_basic3":
                //        return 16;
                //    case "vertical_basic4":
                //        return 17;
                //    case "search_engine":
                //        return 18;
                //    case "vertical_basic5":
                //        return 19;
                //    case "law_decree_population":
                //        return 20;
                //}
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.Module.convertModuleType", ex);
            }
            return 0;
        }

        public Dictionary<string, string> getRegions()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("----", "0");
            try
            {
                string[] lines = System.IO.File.ReadAllLines(appcodefolder + regions);
                for (int i = 0; i < lines.Length; i++)
                    dic.Add(lines[i], lines[i]);
            }
            catch (Exception ex)
            {
                Log.logErr("", ex);
            }
            return dic;
        }


        public virtual string generateModuleForAdmin()
        {
            return "Module generating for admin";
        }
    }
}