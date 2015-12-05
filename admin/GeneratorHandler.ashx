<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using control.cmsmodules;

public class Handler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string rid = context.Request.QueryString["rid"];
        string type = context.Request.QueryString["type"];
        string m_name = context.Request.QueryString["m_name"];
        string __module_name = context.Request.QueryString["__module_name"];
        string mode = context.Request.QueryString["mode"];
        if (rid != null)
        {
            control.cmsmodules.Module currModule = control.cmsmodules.Module.getModuleByRegion(rid);
            if (currModule != null)
            {
                context.Response.Write(currModule.generate());
            }
        }
        if (type != null && m_name != null)
        {
            control.cmsmodules.Module currModule = control.cmsmodules.Module.getModuleByName(m_name.Substring(m_name.IndexOf("_") + 1).ToLower());
            context.Response.Write(currModule.generate());
        }
        if (__module_name != null && mode != null)
        {
            switch (__module_name.ToLower())
            {
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
                                rId = context.Request["role_id"]; //role id
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

class AdmiContentGenerator
{
    public string generateOffers()
    {
        return "";
    }
    public string generateNews()
    {
        return "";
    }

    public string generateVouchers()
    {
        return "";
    }

    public string generateParteners()
    {
        return "";
    }
}