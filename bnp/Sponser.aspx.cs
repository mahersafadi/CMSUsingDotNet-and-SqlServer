using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using control.cmsmodules.bnp;

public partial class bnp_Sponser : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ret = "";
        body1.Attributes.Add("onLoad", "Generator.init()");
        control.cmsmodules.Module currModule = new SponsorModule();
        ret = currModule.generate();
        offersListDiv.InnerHtml = ret;
    }
}