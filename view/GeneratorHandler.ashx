<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using control.cmsmodules;

public class Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        /***
         type = 5 then it's menu
         */
        context.Response.ContentType = "text/plain";
        string rid = context.Request.QueryString["rid"];
        string type = context.Request.QueryString["type"];
        string m_name = context.Request.QueryString["m_name"];
        string __module_name = context.Request.QueryString["__module_name"];
        string mode = context.Request.QueryString["mode"];
        if ((__module_name == null) || __module_name== "") {
            __module_name = context.Request.Form["__module_name"];
        }
        if (mode == null || mode == "")
            mode = context.Request.Form["mode"];
        // generate Modules
        if (rid != null)
        {
            control.cmsmodules.Module currModule = control.cmsmodules.Module.getModuleByRegion(rid);
            //if (currModule != null)
            {
                context.Response.Write(currModule.generate());
            }
        }

        //when menu item clicked
        if (type != null && m_name != null)
        {
            control.cmsmodules.Module currModule = control.cmsmodules.Module.getModuleByName(m_name.Substring(m_name.IndexOf("_") + 1).ToLower());
            context.Response.Write(currModule.generate());
        }

        if (__module_name != null && mode != null){
            switch (__module_name.ToLower())
            {
                case "language":
                    string LangName = context.Request.QueryString["name"];
                    string langCode = context.Request.QueryString["code"];
                    string dir = context.Request.QueryString["dir"];

                    context.Response.Write(LangModule.languageHandler(mode, LangName, langCode, dir));
                    break;
                case "langdetail":

                    string key = context.Request.QueryString["key"];
                    string val = context.Request.QueryString["val"];
                    string lang = context.Request.QueryString["lang"];
                    string ___id = context.Request.QueryString["___id"];

                    context.Response.Write(LangDetailModule.langDetailHandler(mode, key, val, lang, ___id));
                    break;
                case "content":
                    string catId = context.Request.QueryString["cat_id"];
                    string title = context.Request.QueryString["content_title"];
                    string cid = context.Request.QueryString["__cid"];
                    string formData = context.Request.QueryString["form_data"];
                    string imageId = context.Request.QueryString["image_id"];

                    context.Response.Write(ContentModule.contentHandler(mode, catId, title, cid, formData, imageId));
                    break;
                case "category":
                    string catName = context.Request.QueryString["name"];
                    string subCat = context.Request.QueryString["sub_cat"];
                    string id = context.Request.QueryString["__id"];
                    string fromId = context.Request.QueryString["from_id"];
                    context.Response.Write(CategoryModule.categoryHandler(mode, catName, subCat, id, fromId));
                    break;
                case "module":
                    string moduleType = context.Request.QueryString["module_type"];

                    string name = context.Request.QueryString["m_name"];
                    string m_desc = context.Request.QueryString["m_desc"];
                    string m_region = context.Request.QueryString["m_region"];
                    string cat = context.Request.QueryString["cat"];
                    string m_type = context.Request.QueryString["m_type"];
                    string md_data = context.Request.QueryString["md_data"];
                    string module_id = context.Request.QueryString["__module_id"];

                    string elementMenuName = context.Request.QueryString["element_menu_name"];
                    string parent = context.Request.QueryString["parent"];
                    string contentId = context.Request.QueryString["all_contents"];

                    context.Response.Write(Module.ModuleHandler(mode, moduleType, name, m_desc, m_region, cat, m_type, md_data, module_id, elementMenuName, parent, contentId));
                    break;

                case "client_menu":
                    string element_menu_name = context.Request.QueryString["element_menu_name"];
                    string _module_id = context.Request.QueryString["module_id"];
                    string parentId = context.Request.QueryString["parent"];
                    string _contentId = context.Request.QueryString["all_contents"];
                    string menu_element_id = context.Request.QueryString["menu_element_id"];
                    string cat_id = context.Request.QueryString["cat_id"];
                    context.Response.Write(ClientMenu.CLientMenuHandler(mode, element_menu_name, _module_id, parentId, _contentId, menu_element_id, cat_id));
                    break;
                case "engineer_sort":
                    string sName = context.Request["s_name"];
                    string sSpecification = context.Request["s_specification"];
                    string sCity = context.Request["s_city"];
                    string sMinistry = context.Request["s_ministry"];
                    string from_Id = context.Request["from_id"];
                    context.Response.Write(EngSortModule.EngSortHandler(mode, sName, sSpecification, sCity, sMinistry, from_Id));
                    break;
                case "search_engine":
                    string txt = context.Request["txt"];
                    context.Response.Write(SearchModule.searchHandler(mode, txt));
                    break;
                case "gallary":
                    context.Response.Write(GalleryModuleAdmin.executeEvent(mode, context));
                    break;
                case "user":
                    context.Response.Write(ClientManagmentAdmin.executeEvent(mode, context));
                    break;
                case "clientmgmt":
                    context.Response.Write(ClientManagmentAdmin.executeEvent(mode, context));
                    break;
                break;
                case "security":
                    // type= 1 => application
                    /// type= 2 => permission
                    /// type= 3 => role
                    /// type= 4 => permision in role
                    /// type= 5 => user in role
                    if (type != null)
                    {
                        switch (type)
                        {
                            case "1":
                                string appName = context.Request["app_name"];
                                string isActive = context.Request["is_active"];
                                string _id = context.Request["id"];
                                context.Response.Write(control.cmsmodules.security.Application.applicationHandler(mode, _id, appName, isActive));
                                break;
                            case "2":
                                string menu = context.Request["menu"];
                                string desc = context.Request["desc"];
                                string __id = context.Request["id"];
                                context.Response.Write(control.cmsmodules.security.Permission.permissionHandler(mode, menu, desc, __id));
                                break;
                            case "3":
                                string roleId = context.Request["id"];
                                string rName = context.Request["r_name"];
                                string description = context.Request["desc"];
                                string fromDate = context.Request["from_date"];
                                string toDate = context.Request["to_date"];
                                context.Response.Write(control.cmsmodules.security.Role.roleHandler(mode, roleId, rName, description, fromDate, toDate));
                                break;
                            case "4":
                                string rId = context.Request["role_id"]; //role id
                                string selectedPermissions = context.Request["permission_vals"];

                                context.Response.Write(control.cmsmodules.security.PermissionInRole.roleHandler(mode, rId, selectedPermissions));
                                break;
                            case "5":
                                string data = context.Request["tbl_data"];
                                string userId = context.Request["id"];
                                context.Response.Write(control.cmsmodules.security.User.userHandler(mode, data, userId));
                                break;
                            case "6":
                                rId= context.Request["role_id"]; //role id
                                string selectedUsers = context.Request["user_vals"];

                                context.Response.Write(control.cmsmodules.security.UserInRole.userInRoleHandler(mode, rId, selectedUsers));
                                break;
                            case "7":
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}