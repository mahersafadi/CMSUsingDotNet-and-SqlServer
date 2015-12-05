using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.module;

/// <summary>
/// Summary description for SponsorModule
/// </summary>
namespace control.cmsmodules.bnp
{
    public class SponsorModule : Module
    {
        public static int sponserCategory = 1067;
        public SponsorModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SponsorModule(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            int i = 0;
            
            string res = "";
            res += "<div class=\"row_conten\">";
            Content _content = new Content();
            int numberOfContents = 0;
            List<model.db.content> contents = _content.getAllConetentsInCategory(sponserCategory);
                numberOfContents = contents.Count();

            for (int kk = 0; kk < numberOfContents; kk++)
            {
                model.db.content content = contents[kk];
                //get detail satistfied with session
                List<model.db.content_detail> cd = _content.getContentDetailsByContentId(content._cid);
                model.db.content_detail selDet = cd[0];
                int k = 0;
                while (k < cd.Count() && cd[k].__rlang != LangModule.sessionLang)
                    k++;
                if (cd[k].__rlang == LangModule.sessionLang)
                    selDet = cd[k];

                res += "<div  class=\"col-lg-4\"  >";
                res += "<div class=\"col-lg-4_box\">";
                res += "<div class=\"col-lg-4_image\">";
                string url = selDet.text;
                url = url.Trim();
                if (url.StartsWith("<p>"))
                    url = url.Substring("<p>".Length);
                if(url.EndsWith("</p>"))
                    url = url.Substring(0, url.Length - "</p>".Length);
                res += "<a class=\"fancybox\"  data-fancybox-group=\"gallery\" onclick=\"goTo1('"+url+"');\" target='_blank' title=\"\"><img class=\"img-circle\" src='/view/uploads/content_thumbnails/" + content.thumbnail + "' alt=\"generic placeholder image\" style=\"width: 231px; height:222px;\"></a>";
                res += "<h2 class=\"col-lg-4_box_title\">" + selDet.title + "</h2>";
                
               
                res += "<br clear=\"all\">";
                res += "</div>";
                res += "</div>";
                res += "</div>";
            }
            res += "</div>";
            return res;
        }

    }
}