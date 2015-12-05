using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using control.cmsmodules;
using model.module;

/// <summary>
/// Summary description for LatestNewsModule
/// </summary>
/// 

namespace control.cmsmodules
{

    public class LatestNewsModule : Module
    {

        public LatestNewsModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public LatestNewsModule(model.db.module module)
        {
            this.module = module;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }
        public override string generate()
        {
            string res = "";
            try
            {
                // later on, get the latest contents from everything..

                string css = model.module.ModuleInModelLayer.getCSSAttribtues(mdAsDictionary);
                int numOfConts = model.module.ModuleInModelLayer.getNumOfContents(mdAsDictionary);
                List<model.db.content> contents = model.module.Content.getLastXContentInCategory(this.module.__rcategory.Value, Convert.ToInt32(mdAsDictionary["num_of_contents"]));

                res = @"<div class='blog-twitter' style='margin-top:-5%'>
                        <div class='headline'>
                            <h2 style='color:#ed1c24;'>
                                " + Lang.getByKey("latest_news") + @"</h2>
                        </div>";


                
                foreach (model.db.content c in contents)
                {
                    model.module.Content cc = new Content();
                    model.db.content_detail cd = cc.getContentDetail(c._cid, cmsmodules.LangModule.sessionLang);
                    string t = cd != null ? cd.title : "";
                    if (t != "")
                    res += @"<div class='blog-twitter-inner'>" + Global.getPartOfString(cd.title, 250) + @"   &nbsp;&nbsp;<a href='Default.aspx?id=" + cd.content._cid + @"'>" + Lang.getByKey("read_more") + @"</a> <span class='twitter-time'>
                                " + cd.create_date + @"</span>
                        </div>";


                }

                res += @"</div>";


            }
            catch (Exception ex)
            {
            }

            return res;
        }
    }
}