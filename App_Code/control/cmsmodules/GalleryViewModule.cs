using control.cmsmodules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GalleryModule
/// </summary>
namespace control.cmsmodules
{
    public class GalleryViewModule : Module
    {
        public GalleryViewModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public override string generate()
        {
            int i = 0;
            string res = "<div class='gallery'>";
            try
            {
                if (i == 0)
                    res += "<div class=\"row_conten\">";

                List<model.db.gallery> g = model.module.Gallery.list();
                foreach (var item in g)
                {
                    // var item = g[0];
                    List<model.db.gallery_detail> gd = model.module.GalleryDetail.getGalleryDetails(item._gid);
                    var first = gd[0];
                    res += "<div  class=\"col-lg-4\"  >";
                    res += "<div class=\"col-lg-4_box\">";
                    res += "<div class=\"col-lg-4_image\">";
                    res += "<a class=\"fancybox\"  data-fancybox-group=\"gallery\" href=\"/view/pg_rep/" + first.img_url + "\"  title=\"" + item.name + "\"><img class=\"img-circle\" src=\"/view/pg_rep/" + first.img_url + "\" alt=\"generic placeholder image\" style=\"width: 240px; height: 255px;\"></a>";
                    foreach (var item1 in gd)
                    {
                        res += "<a class=\"fancybox\"  style=\"display:none;\"  rel=\"fancybox\" href=\"/view/pg_rep/" + item1.img_url + "\" data-fancybox-group=\"gallery\" title=\"" + item.name + "\"><img class=\"img-circle\" src=\"/view/pg_rep/" + item1.img_url + "\" alt=\"Generic placeholder image\" style=\"width: 240px; height: 255px;\"></a>";
                    }
                    res += "</div>";
                    res += "<h2 class=\"col-lg-4_box_title\">" + item.name + "</h2>";
                    res += "<br clear=\"all\">";
                    res += "</div>";
                    res += "</div>";
                    res += "</div>";

                }
            }
            catch (Exception ex)
            {
                Log.logErr("Gallery.generate", ex);
            }
            res += "</div>";
            return res;
        }
    }
}