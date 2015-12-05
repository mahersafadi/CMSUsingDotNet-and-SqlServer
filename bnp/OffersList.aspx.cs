using control.cmsmodules.bnp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bnp_OffersList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ret = "";
        body1.Attributes.Add("onLoad", "Generator.init()");
        control.cmsmodules.bnp.OfferModule currModule = (control.cmsmodules.bnp.OfferModule)control.cmsmodules.Module.getModuleByRegion("region_content_middle");
        string sortBy = Request.QueryString["sort"];
        if(sortBy != null && sortBy != "")
            currModule.setSortBy(sortBy);
        ret = currModule.generate();
        offersListDiv.InnerHtml = ret;
    }
}