using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using control.cmsmodules;
using model.module;

/// <summary>
/// Summary description for ClientMenu
/// </summary>
/// 
namespace control.cmsmodules
{
    public class ClientMenu : Module
    {
        public ClientMenu()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public override string generate()
        {
            string res = "";
            try
            {
                res += "<button onclick='Client_Menu.new_menu(this)'>" + Lang.getByKey("new_menu") + "</button>";
                //res += "<button onclick='Client_Menu.cancel_menu(this)'>" + Lang.getByKey("new_content") + "</button>";
                res += "<input type='hidden' id='mdoule_menu_id' />";
                res += "<div id='div_client_menu_search'></div>";
                //for add a new men
                res += @"<div id='div_client_menu'>" + generateAllMenus() + "</div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.ClientMenu.generate", ex);
            }
            return res;
        }

        public string generateAllMenus()
        {
            string res = "<table class='gridtable'>";
            res += "<tr>";
            res += "<th> " + Lang.getByKey("menu_module_name") + " </th>";
            res += "<th></th>";
            res += "</tr>";
            try
            {
                List<model.db.module> menus = model.module.Menu.getMenuModulesFromMenuTable();
                foreach (model.db.module m in menus)
                {
                    res += "<tr>";
                    res += "<td> " + m.title + " </td>";
                    res += "<td> <a href='#' id='" + m._mid + "' onclick='Client_Menu.edit_menu(this)' > edit </a> " +
                        "</td>";
                    res += "</tr>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.generateAllMenus", ex);
            }
            return res;
        }

        public string generateNewMenu()
        {
            string res = "";
            try
            {
                res += @"<table class='gridtable'><tr>
                            <th>" + Lang.getByKey("element_menu_name") + @"</td>
                            <td><input  type='text' id='element_menu_name' /> </td>
                         </tr>
                         <tr>
                            <th>" + Lang.getByKey("menu_module_name") + @"</td>
                            <td>" + MenuModule.generateMenuModulesAsSelect("onchange='Client_Menu.get_menu_elements(this)'", null) + @"</td>
                         </tr>
                        <tr>
                            <th>" + Lang.getByKey("parent") + @"</td>
                            <td id='td_menu_elements'>" + MenuModule.generateMenuByModuleIdasSelect(1, true, null) + @"</td>
                        </tr>
                        <tr>
                            <th>" + Lang.getByKey("category") + @"</td>
                            <td id='td_menu_element_category'>" + CategoryModule.generateCategoriesAsHtmlSelect(true, null) + @"</td>
                        </tr>
                        <tr>
                            <th>" + Lang.getByKey("content_name") + @"</td>
                            <td>" + ContentModule.generateAllContentDetails(LangModule.sessionLang, null) + @" </td>
                         </tr>
                         <tr>
                            <td colspan=2>
                                <button id='save_menu' onclick='Client_Menu.save_new_menu(this)'>" + Lang.getByKey("ok") + @"</button> &nbsp;
                                <button id='cancel' onclick='Client_Menu.cancel_new_menu(this)'>" + Lang.getByKey("back") + @"</button>
                            </td>
                         </tr>
                        </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmscontrolmodules.Module.generateNewMenu", ex);
            }
            return res;
        }



        //***********handler*********************
        public static string CLientMenuHandler(string mode, string elementName, string moduleId, string parentId, string contentId, string menuElementId, string catId)
        {
            ClientMenu m = new ClientMenu();
            if (mode == "1")
                return m.generate();
            else if (mode == "2")
                return m.generateNewMenu();
            else if (mode == "3")
                return MenuModule.generateMenuByModuleIdasSelect(Convert.ToInt32(moduleId), true, null);
            else if (mode == "4")
            {
                model.db.menu mm = new model.db.menu();
                mm.name = elementName;
                mm.__rmodule = Convert.ToInt32(moduleId);
                mm.parent = Convert.ToInt32(parentId);
                mm.__rcontent = Convert.ToInt32(contentId);
                mm.__category = Convert.ToInt32(catId);
                mm.create_date = DateTime.Now;
                if (model.module.Menu.insertMenu(mm))
                    return "saved";
                return "error";
            }
            else if (mode == "5")
            {
                return MenuModule.generateEditMenuElements(Convert.ToInt32(moduleId));
            }
            else if (mode == "6")
            {
                return MenuModule.generateEditMenuElement(Convert.ToInt32(menuElementId));
            }
            else if (mode == "7")
            {
                model.db.menu menu = new model.db.menu();
                menu.name = elementName;
                menu.__rmodule = Convert.ToInt32(moduleId);
                menu.parent = Convert.ToInt32(parentId);
                menu.__rcontent = Convert.ToInt32(contentId);
                menu._mid = Convert.ToInt32(menuElementId);
                menu.__category = Convert.ToInt32(catId);
                if (model.module.Menu.updateMenu(menu))
                    return "updated";
                return "error";
            }
            else if (mode == "8")
            {
                if (model.module.Menu.deleteMenu(Convert.ToInt32(menuElementId)))
                    return "deleted";
                return "error";
            }
            return "";
        }
    }
}