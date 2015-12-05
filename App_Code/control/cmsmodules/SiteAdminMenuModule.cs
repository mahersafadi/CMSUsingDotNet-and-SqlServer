using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SiteAdminMenu
/// </summary
namespace control.cmsmodules
{
    public class SiteAdminMenuModule : Module
    {
        public SiteAdminMenuModule()
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
                List<menu> m = model.module.Menu.getMenuByModuleId(1106);
                List<menu> parentMenus = new List<menu>();
                foreach (var item in m)
                {
                    if (item.parent == 0)
                        parentMenus.Add(item);
                }
                res = "<div>" +
                            "<aside class=\"column\" id=\"sidebar\">" +
                            "<hr>";
                foreach (menu item in parentMenus)
	            {
                    res += "<h3>" + item.name + "</h3>";
                    res += "<ul class=\"toggle\">";
                    foreach (menu det in m) {
                        if (det.parent == item._mid) {
                            res += "<li class=\"icn_new_article\"><a id=\""+det.name+"\" onclick=\"Menu.get_content(this)\" >"+det.name+"</a></li>";
                        }
                    }
                    res += "</ul>";
	            }
                res +=  "</div>";
            }
            catch (Exception ex)
            {
                Log.logErr("An errors accured during try to generate SiteAdminMenu Module", ex);
            }
            return res;
        }
    }
}