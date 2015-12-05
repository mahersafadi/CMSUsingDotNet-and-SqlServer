using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bnp_Client : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        regions.Value = "region_header,client_region";
        regionsPriorities.Value = "1,2";
        body1.Attributes.Add("onLoad", "Generator.init()");
        //Get My vouchers
        model.db.user u = (model.db.user)System.Web.HttpContext.Current.Session["user"];
        model.db.CMSDataContext db = new model.db.CMSDataContext();
        List<model.db.bnp_vchr_client> bncs = db.bnp_vchr_clients.Where(bb=>bb.client_id == u._uid).ToList();
        string _myVouchers = "";
        foreach (var item in bncs)
        {
            _myVouchers += "<div  class=\"col-lg-4\"  >";
            _myVouchers += "<div class=\"col-lg-4_box\">";
            _myVouchers += "<div class=\"col-lg-4_image\">";
            _myVouchers += "<a class=\"fancybox\"  data-fancybox-group=\"gallery\" href=\"#\"  title=\"\"><img class=\"img-circle\" src='/view/uploads/content_thumbnails/" + item.content_detail.content.thumbnail + "' alt=\"generic placeholder image\" style=\"width: 231px; height:222 px;\"></a>";
            _myVouchers += "<h2 class=\"col-lg-4_box_title\">" + StringCipher.Decrypt(item.content_detail.text) + "</h2>";
            _myVouchers += "<h3 class=\"col-lg-4_box_title col-lg-4_price\">Rest: " + item.rest + "</h3>";
            _myVouchers += "<br clear=\"all\">";
            _myVouchers += "</div>";
            _myVouchers += "</div>";
            _myVouchers += "</div>";
        }
        myVouchers.InnerHtml = _myVouchers;
        List<model.db.content_detail> vouchers = db.content_details.Where(cc=>cc.content.__rcategory == control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt.voucherCategory).ToList();
        string _bnpVouchers = "";
        foreach (var item in vouchers)
        {
            _bnpVouchers += "<div  class=\"col-lg-4\"  >";
            _bnpVouchers += "<div class=\"col-lg-4_box\">";
            _bnpVouchers += "<div class=\"col-lg-4_image\">";
            _bnpVouchers += "<a class=\"fancybox\"  data-fancybox-group=\"gallery\" href=\"#\"  title=\"\"><img class=\"img-circle\" src='/view/uploads/content_thumbnails/" + item.content.thumbnail+ "' alt=\"generic placeholder image\" style=\"width: 231px; height:222 px;\"></a>";
            _bnpVouchers += "<h2 class=\"col-lg-4_box_title\">" + StringCipher.Decrypt(item.text) + "</h2>";
            _bnpVouchers += "<h2 class=\"col-lg-4_box_title col-lg-4_price\" style='font-size:10px !important;'>Rest: " + item.key_word.Split(new char[] { '#', '#' })[2] + "</h2>";
            _bnpVouchers += "<br clear=\"all\">";
            _bnpVouchers += "</div>";
            _bnpVouchers += "</div>";
            _bnpVouchers += "</div>";
        }
        bnpVouchers.InnerHtml = _bnpVouchers;
    }
}