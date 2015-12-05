<%@ WebHandler Language="C#" Class="BnpHandler" %>

using System;
using System.Web;

public class BnpHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        string res = "";
        try
        {
            string __module_name = context.Request["__module_name"];
            if (__module_name != null && __module_name != "")
            {
                if (__module_name == "BnpVoucherOfferNewsSlideShowMgmt") {
                    string mode = context.Request["mode"];
                    context.Response.Write(control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt.executeEvent(mode, context));
                    return;
                }
            }
            else { 
                System.Collections.Specialized.NameValueCollection collection = context.Request.Form;
                if (context.Request.Form["id"] != null && context.Request.Form["id"] != "")
                {
                    res = Security.editUserFromRequest(context, 3, int.Parse(context.Request.Form["iddddddd"]), int.Parse(context.Request.Form["id"]));
                }
                else
                {
                    res = Security.insertNewUserFromRequest(context, 3, int.Parse(context.Request.Form["iddddddd"]));
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(res);
                }
            }
        }
        catch (Exception ex)
        {
            Log.logErr("BnpHadler.ProcessRequest......", ex);
        }        
    }
    
    public bool IsReusable {
        get {
            return false;
        }
    }

}