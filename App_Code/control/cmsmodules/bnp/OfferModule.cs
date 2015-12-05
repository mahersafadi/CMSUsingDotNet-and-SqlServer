using control.cmsmodules;
using model.db;
using model.module;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Offer
/// </summary>
/// 
namespace control.cmsmodules.bnp
{
    public class OfferModule : Module
    {
        private string sortBy;
        public void setSortBy(string sortBy)
        {
            this.sortBy = sortBy;
        }
        public OfferModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public OfferModule(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            int i = 0;
            int numberOfContents = int.Parse(mdAsDictionary["num_of_contents"]);
            string res = "";
            res += "<div class=\"row_conten\">";
            Content _content = new Content();
            List<model.db.content> contents = null;
            if (this.sortBy == null || this.sortBy == "")
                contents = _content.getLastXContentInCategory(this.module.__rcategory.Value, numberOfContents, 0);
            else {
                CMSDataContext db = new CMSDataContext();

                IEnumerable qry = null;
                if (sortBy == "latest")
                    contents = db.contents.Where(m => m.category._cid == this.module.__rcategory.Value).OrderByDescending(m => m.create_date).ToList();
                else if (sortBy == "newest")
                    contents = db.contents.Where(m => m.category._cid == this.module.__rcategory.Value).OrderBy(m => m.create_date).ToList();
                else
                {
                    List<content_extra> ll = db.content_extras.Where(m => m._key == "category" && m._val == sortBy).ToList();
                    contents = new List<content>();
                    foreach (var item in ll)
                    {
                        contents.Add(item.content);
                    }
                }
            }
            if (mdAsDictionary["show_all"] == "true")
            {
                numberOfContents = contents.Count();
            }
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
                res += "<a class=\"fancybox\"  data-fancybox-group=\"gallery\" href=\"/bnp/Offer.aspx?id=" + content._cid + "\"  title=\"\"><img class=\"img-circle\" src='/view/uploads/content_thumbnails/" + content.thumbnail + "' alt=\"generic placeholder image\" style=\"width: " + mdAsDictionary["img_width"] + "px; height: " + mdAsDictionary["img_height"] + "px;\"></a>";
                res += "<h2 class=\"col-lg-4_box_title\">" + selDet.title + "</h2>";
                string x = "";
                if (selDet.key_word != null)
                    x = selDet.key_word.Substring(0, selDet.key_word.IndexOf('#'));
                res += "<h2 class=\"col-lg-4_box_title col-lg-4_price\">" + x + "</h2>";
                res += "<br clear=\"all\">";
                res += "</div>";
                res += "</div>";
                res += "</div>";
            }
            res += "</div>";
            return res;
        }

        public override string generateModuleForAdmin()
        {
            string ret = "";
            return ret;
        }
    }
}