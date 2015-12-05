using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PhotoGallary : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        body1.Attributes.Add("onLoad", "Generator.init()");

        control.cmsmodules.GalleryViewModule gm = new control.cmsmodules.GalleryViewModule();
        mediaGallery.InnerHtml = gm.generate();
    }
}