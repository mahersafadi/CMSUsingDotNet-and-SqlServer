using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.module;

/// <summary>
/// Summary description for VerticalBasic5Module
/// </summary>
/// 
namespace control.cmsmodules
{
    public class VerticalBasic5Module : Module
    {
        public VerticalBasic5Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public VerticalBasic5Module(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";

            //#B81D22 prevoius red color
            try
            {
                ContentModule content = new ContentModule();

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules,BasicModule.generate", ex);
            }

            List<model.db.content> contents = new model.module.Content().getLastXContentInCategory(this.module.__rcategory.Value, 100, 0);

            res = @"<div class='magazine-sb-categories margin-bottom-20'>
                        <div class='headline headline-md'>
                            <h2>" + model.module.Lang.getByKey(this.module.category.name) + @"</h2>
                        </div>
                        <div class='row'>";

            res += "<ul class='list-unstyled col-xs-6'>";

            foreach (model.db.content content in contents)
            {
                foreach (model.db.content_detail cd in new model.module.Content().getContentDetailsByContentId(content._cid))
                {
                    if (LangModule.sessionLang == cd.__rlang)
                    {
                        //                            res += "<div style='font-family: droidKufi; text-align: center;'><img style='width:50%' src='" + ContentModule.uploadPath + content.thumbnail + @"' />
                        //                                
                        //                            <a class='top-grid' target='_blank' href='" + Global.trimAllHTMLTags(cd.text) + @"'>" + cd.title + @"</a></div>";
                        //res += "<li><a target='_blank' href='" + Global.trimAllHTMLTags(cd.text) + ">" + cd.title + "</a></li>";

                        res += "<li><i class='fa fa-bookmark'></i> <a target='_blank' href='" + Global.trimAllHTMLTags(cd.text) + @"' href='#'>" + cd.title + "</a></li>";
                    }
                }
            }

            res += @"</ul>
                        </div>
                    </div>";

//            res = @"<div class='magazine-sb-categories margin-bottom-20'>
//                        <div class='headline headline-md'>
//                            <h2>
//                                Quick Links</h2>
//                        </div>
//                        <div class='row'>
//                            <ul class='list-unstyled col-xs-6'>
//                                <li><a href='#'>Revolution Slider</a></li>
//                            </ul>
//                        </div>
//                    </div>";
            return res;
        }
    }
}