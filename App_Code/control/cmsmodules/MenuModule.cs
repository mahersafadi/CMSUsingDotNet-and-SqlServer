using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;
using control.cmsmodules;
using model.module;

/// <summary>
/// Summary description for MenuModule
/// </summary>

namespace control.cmsmodules
{
    public class MenuModule : Module
    {
        public MenuModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public MenuModule(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override void setDBModel(module m)
        {
            if(m != null && m.type == 10)
                base.setDBModel(m);
        }

        public override string generate()
        {

            try
            {
                List<menu> parents = null;
                Menu m = new Menu();
                string str = "<ul id='trans-nav'>";
                if (this.module == null)
                {
                    str = "<ul id='trans-nav'>";

                    if (isAdmin)
                        parents = m.getMenuByName("admin_menu");
                    else
                        parents = m.getMenuByName("main_menu");

                    foreach (menu parent in parents)
                    {
                        if (Security.hasPermission(parent.name))
                            str += "<li><a href='#' id='menu_" + parent.name + "' onclick='Menu.get_content(this)'>" + Lang.getByKey(parent.name) + "</a>";
                        List<menu> childs = m.getMenuChildsByParentId(parent._mid);
                        str += "<ul>";
                        foreach (menu child in childs)
                        {
                            if (Security.hasPermission(child.name))
                                str += "<li><a href='#' id='menu_" + child.name + "' onclick='Menu.get_content(this)'>" + Lang.getByKey(child.name) + "</a></li>";
                        }
                        str += "</ul></li>";
                    }
                    str += "</ul>";
                    return str;
                }
                else
                {
                    parents = m.getParentMenuEelementsByModuleId(this.module._mid);
//                    str = @"<div class='collapse navbar-collapse navbar-responsive-collapse'>
//                                <ul class='nav navbar-nav'>";

                    str = @"<div  class='navbar navbar-right' role='navigation'>
                            <div class='container'>
                                <!-- Brand and toggle get grouped for better mobile display -->
                                <div class='navbar-header'>
                                    <button type='button' class='navbar-toggle' data-toggle='collapse' data-target='.navbar-responsive-collapse'>
                                        <span class='sr-only'>Toggle navigation</span>
                                        <span class='fa fa-bars'></span>
                                    </button>
                                    
                             </div>";

                    str += @" <div class='collapse navbar-collapse navbar-responsive-collapse'>
                                <ul class='nav navbar-nav'>";

                    str += @"<li id='home'><a href='Default.aspx' >
                                <i class='fa fa-home' style='font-size:130%'></i>
                            </a></li>";
                    foreach (menu parent in parents)
                    {

                        List<menu> childs = m.getMenuChildsByParentId(parent._mid);
                        if (childs.Count > 0)
                        {
                            str += @"<!-- Home -->
                        <li class='dropdown' >
                            <a href='javascript:void(0);' class='dropdown-toggle' data-toggle='dropdown'>
                                " + Lang.getByKey(parent.name) + @"
                            </a>
                            <ul class='dropdown-menu'>";
                             foreach (var child in childs)
                            {
                                str += "<li id='" + child.content._cid + "'><a  href='Default.aspx?id=" + child.content._cid + @"'>" + Lang.getByKey(child.name) + @"</a></li>";
                            }
                                
                            str +="</ul></li><!-- End Home -->";

//                            str += @"<li class='dropdown'>
//                                <a href='javascript:void(0);' class='dropdown-toggle' data-toggle='dropdown'>
//                                    " + Lang.getByKey(parent.name) + @"
//                                </a>";
//                            str += "<ul class='dropdown-menu'>";

//                            foreach (var child in childs)
//                            {
//                                str += "<li><a href='page_home1.html'>" + Lang.getByKey(child.name) + "</a></li>";
//                            }
//                            str += "</ul></li>";
                        }
                        else
                        {
                            str += @"<li id='" + parent.__category.Value + "' ><a  href='Default.aspx?catId=" + parent.__category.Value + @"' >
                                " + Lang.getByKey(parent.name) + @"
                            </a></li>";
                        }
                    }

//                    str += @"<li>
//                            <i class='search fa fa-search search-btn'></i>
//                            <div class='search-open'>
//                                <div class='input-group animated fadeInDown'>
//                                    <input type='text' class='form-control' placeholder='Search'>
//                                    <span class='input-group-btn'>
//                                        <button class='btn-u' type='button'>Go</button>
//                                    </span>
//                                </div>
//                            </div>    
//                        </li>";
                    //str += @"<li></li></ul></div><!--/navbar-collapse-->";



                    str += @"
                      
                        <!-- Search Block -->
                        <li>
                            <i class='search fa fa-search search-btn'></i>
                            <div class='search-open'>
                                <div class='input-group animated fadeInDown'>
                                    <input type='text' class='form-control' placeholder='"+Lang.getByKey("search")+@"'>
                                    <span class='input-group-btn'>
                                        <button class='btn-u' type='button'>" + Lang.getByKey("search") + @"</button>
                                    </span>
                                </div>
                            </div>    
                        </li>
                        <!-- End Search Block -->
                    </ul>
                </div><!--/navbar-collapse-->
            </div>    
        </div> ";
                    return str;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("MenuModule.generate", ex);
            }

            return null;
        }

        public static string generateMenuByModuleIdasSelect(int? moduleId, bool includeNoParent, int? selectedValue)
        {
            string res = "<select id='menu_parent'>";
            if (includeNoParent)
                res += "<option value='0'>--------</option>";
            try
            {
                List<menu> m = model.module.Menu.getMenuByModuleId(moduleId);
                if (m != null)
                {
                    foreach (menu mm in m)
                    {
                        string selected = "";
                        if (selectedValue != null && selectedValue == mm._mid)
                            selected = "selected=selected";

                        res += "<option " + selected + " value='" + mm._mid + "'" + ">" + mm.name + "</option>";
                    }
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmscontrol.MenyModule.generateMenuByModuleIdasSelect", ex);
            }
            return res;
        }

        public static string generateEditMenuElements(int moduleId)
        {
            string res = "";
            try
            {
                res = @"<table class='gridtable'>";
                List<model.db.menu> menus = model.module.Menu.getMenuByModuleId(moduleId);
                if (menus != null)
                {
                    string title = "";
                    if (menus.Count > 0)
                    {
                        title = menus[0].module.title + " " + Lang.getByKey("menu_elements");
                        res += @"<tr>
                                <th colspan='4'>" + title + @"</th>
                            </tr>
                            <tr>
                                <th>" + Lang.getByKey("element_menu_name") + @"</th>
                                <th>" + Lang.getByKey("element_menu_parent") + @"</th>
                                <th></th>
                            </tr>";
                    }
                    else
                        res += @"<tr>
                                <th colspan='4'>" + Lang.getByKey("no_menu_elements") + @"</th>
                            </tr>";


                    foreach (model.db.menu m in menus)
                    {
                        res += @"<tr>
                                <td>" + Lang.getByKey(m.name) + @"<input type='hidden' value='" + m.__rmodule + @"' id='mdoule_menu_id' /></td>
                                <td>" + Lang.getByKey(model.module.Menu.getMenuNameById(Convert.ToInt32(m.parent))) + @"</td>
                                <td> <a href='#' id='" + m._mid + "' onclick='Client_Menu.edit_menu_element(this)' > edit </a>" +
                                "<a href='#' id='" + m._mid + "' onclick='Client_Menu.delete_menu_element(this)' > delete </a>" +
                            "</td></tr>";
                    }
                }
                res += "<tr>";
                res += "<th colspan='4'><a href='#' onclick='Client_Menu.back_to_menu(this)' > back </a> </th>";
                res += "</tr>";
                res += "</table>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.MenuModules.generateMenuElements", ex);
            }
            return "";
        }

        public static string generateEditMenuElement(int menuId)
        {
            string res = "";
            try
            {
                res = @"<table class='gridtable'>";
                model.db.menu menu = model.module.Menu.getMenuElementByMenuId(menuId);
                if (menu != null)
                {
                    res += @"<tr>
                                <th colspan='2'>" + Lang.getByKey("edit_menu_element") + @"</th>
                            </tr>";
                    res += @"<tr>
                                <th>" + Lang.getByKey("element_menu_name") + @"</th>
                                <td> <input id='element_menu_name' type='text' value= '" + menu.name + @"' />
                                    <input id='menu_element_id' type='hidden' value= '" + menu._mid + @"' /></td>
                            </tr>
                            <tr>
                                <th>" + Lang.getByKey("menu_module_name") + @"</th>
                                <td>" + MenuModule.generateMenuModulesAsSelect("onchange='Client_Menu.get_menu_elements(this)'", menu.__rmodule) + @"</td>
                         </tr>
                        <tr>
                            <th>" + Lang.getByKey("parent") + @"</td>
                            <td id='td_menu_elements'>" + MenuModule.generateMenuByModuleIdasSelect(menu.__rmodule, true, menu.parent) + @"</td>
                         </tr>
                        <tr>
                            <th>" + Lang.getByKey("category") + @"</td>
                            <td id='td_menu_element_category'>" + CategoryModule.generateCategoriesAsHtmlSelect(true, menu.__category.ToString()) + @"</td>
                        </tr>
                        <tr>
                            <th>" + Lang.getByKey("content_name") + @"</td>
                            <td>" + ContentModule.generateAllContentDetails(LangModule.sessionLang, menu.__rcontent) + @" </td>
                         </tr>
                         <tr>
                            <th colspan=2>
                                <button id='save_menu' onclick='Client_Menu.update_menu(this)'>" + Lang.getByKey("ok") + @"</button> &nbsp;
                                <button id='" + menu.__rmodule + "' onclick='Client_Menu.back_to_menu_elements(this)'>" + Lang.getByKey("back") + @"</button>
                            </th>
                         </tr>";
                }
                res += "</table>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.MenuModules.generateMenuElements", ex);
            }
            return "";
        }

        public static string generateMenuModulesAsSelect(string attributes, int? selectedValue)
        {
            string res = "<select id='menu_modules' " + attributes + ">";
            try
            {
                List<model.db.module> m = model.module.Menu.getMenuModules();
                if (m != null)
                {
                    foreach (model.db.module mm in m)
                    {
                        if (mm.region != null)
                        {
                            string selected = "";
                            if (selectedValue != null && selectedValue == mm._mid)
                                selected = "selected";

                            res += "<option " + selected + " value='" + mm._mid + "'" + ">" + mm.title + "</option>";
                        }
                    }
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmscontrol.MenyModule.generateMenuByModuleIdasSelect", ex);
            }
            return res;
        }
    }


}