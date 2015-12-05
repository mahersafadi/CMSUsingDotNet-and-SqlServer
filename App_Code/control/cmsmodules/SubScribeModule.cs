using control.cmsmodules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SubScribeModule
/// </summary>
public class SubScribeModule : Module
{
	public SubScribeModule()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public SubScribeModule(model.db.module m)
    {
        this.module = m;
        mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
    }

    public override string generate()
    {
        string res = "";
        try
        {
            res = "<div>" +
                     "<div id=\"subscribe_title\">" + model.module.Lang.getByKey("sub_scribe") + "</div>" +
                     "<div height=\"12px\"></div>" +
                     "<div id=\"subscribe_tbox\"><input type=\"text\" id=\"subscribe_email\" name=\"subscribe_email\" /></div>" +
                     "<div height=\"5px\"></div>" +
                     "<div id=\"subscribe_btn\"><input type=\"button\" id=\"subscribeBtn\" name=\"subscribeBtn\" value=\"&nbsp;&nbsp;" + model.module.Lang.getByKey("subscribe") + "&nbsp;&nbsp;\" onclick=\"doSubScribeModule(this)\" /></div>" +
                  "</div>" +
                  "<script type=\"text/javascript\">" +
                  " function doSubScribeModule(btn){"+
                  " alert(1); "+
                  " } "+
                  "</script>";
        }
        catch (Exception ex)
        {
            Log.logErr("control.cmsmodules,SubScribeModule.generate", ex);
        }
        return res;
    }
}