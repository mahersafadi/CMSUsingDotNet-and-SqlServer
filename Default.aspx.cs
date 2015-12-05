using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using control.cmsmodules;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _contentId = Request.QueryString["id"];
        string _categoryId = Request.QueryString["catId"];
        string _fromId = Request.QueryString["fromId"];
        string _engSort = Request.QueryString["eng_order"];
        string show_mode = Request.QueryString["show_mode"];
        if (_contentId == null && _categoryId == null &&  _fromId == null)
        {
            regions.Value = "region_header,r1_middle,region_content_middle";
            regionsPriorities.Value = "1,2,3,4";

            body1.Attributes.Add("onLoad", "Generator.init()");
        }
        else if (_contentId != null && _categoryId == null && _fromId == null)
        {
            regions.Value = "region_header";
            regionsPriorities.Value = "1";
            body1.Attributes.Add("onLoad", "Generator.init()");
            region_content_middle.InnerHtml = new control.cmsmodules.ContentModule().generateContentDetail(Convert.ToInt32(_contentId));
        }
        else if (_categoryId != null && _fromId == null)
        {
            regions.Value = "region_header";
            regionsPriorities.Value = "1";
            body1.Attributes.Add("onLoad", "Generator.init()");
            ContentModule cm = new ContentModule();
            if (show_mode == "" || show_mode == null)
                region_content_middle.InnerHtml = cm.generateXContentsByCategoryId2(Convert.ToInt32(_categoryId), 8, 0);
            else
            {
                int i = int.Parse(show_mode);
                control.cmsmodules.Module currModule = control.cmsmodules.ModuleFactory.createModule(i);
                currModule.isAll = true;
                currModule.setDBModel(int.Parse(Request.QueryString["module"]));
                region_content_middle.InnerHtml = currModule.generate();
            }
        }
        else if (_categoryId != null && _fromId != null)
        {
            ContentModule cm = new ContentModule();
            if (show_mode == "" || show_mode == null)
            {
                Response.Write(cm.generateXContentsByCategoryId2(Convert.ToInt32(_categoryId), 4, Convert.ToInt32(_fromId)));
                Response.End();
            }
            else {
                int i = int.Parse(show_mode);
                control.cmsmodules.Module currModule = control.cmsmodules.ModuleFactory.createModule(i);
                currModule.isAll = true;
                currModule.setDBModel(int.Parse(Request.QueryString["module"]));
                region_content_middle.InnerHtml = currModule.generate();
            }

        }

        string str = "<div id=\"myExample\" style=\"width: 100%; height:400px;margin: auto; visibility: hidden\">";
        model.module.Content _content = new model.module.Content();
        List<model.db.content> contents = _content.getLastXContentInCategory(control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt.slideShowCategory, 4, 0);
        model.module.Content ctnt = new model.module.Content();
        if (contents != null)
        {
            for (int i = 0; i < contents.Count; i++)
            {

                bool choosen = true;
                model.db.content content = contents[i];
                List<model.db.content_extra> ces = ctnt.getContentExtraByContentId(content._cid);
                if (ces.Count() > 0)
                {
                    model.db.content_extra ce = ces[0];
                    if (ce._val == "inactive")
                        choosen = false;
                }
                if (choosen)
                {
                    List<model.db.content_detail> cd = _content.getContentDetailsByContentId(content._cid);
                    if (cd != null)
                        if (cd.Count > 0)
                        {
                            model.db.content_detail selDet = cd[0];
                            int k = 0;
                            while (k < cd.Count() && cd[k].__rlang != LangModule.sessionLang)
                                k++;
                            if (cd[k].__rlang == LangModule.sessionLang)
                                selDet = cd[k];
                            str += "<img src=\"/view/uploads/content_thumbnails/" + content.thumbnail + "\" alt=\"slide " + (i + 1) + "\">";
                     }
                }
            }
        }
        
        str += "</div>";

        str += "<div id=\"Paging\"class=\"Paging Paging2\"  ><span class=\"jump\">•</span></div>";
       // str += "<div class=\"paging paging2 jump\" id=\"Paging\"><span>•</span></div>";
 
//        str += @"<style type='text/css'>
//	  .paging {
//	  text-align: center;
//	  }
//	  .paging span {
//	  text-decoration: none;
//	  color: silver;
//	  cursor: pointer;
//	  }
//	  .paging span.active {
//	  font-size: 2em;
//	  color: black;
//	  }
//
//	  .paging2 span {
//	  font-size: 2em;
//	  }
//
//	</style>";
        
  

        if (_contentId == null)
            region_headertop_1.InnerHtml = str;
    }
}