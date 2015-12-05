using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bnp_Partner : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        regions.Value = "region_header,client_region";
        regionsPriorities.Value = "1,2";
        body1.Attributes.Add("onLoad", "Generator.init()");
        myOffers.InnerHtml = getMyOffers();
        myVouchers.InnerHtml = getMyVouchers();
    }

    private string getMyOffers()
    {
        string ret = "";
        try
        {
            model.db.user u = (model.db.user)System.Web.HttpContext.Current.Session["user"];
            if (u != null)
            {
                ret += "<div><ul>";
                model.module.Content ctnt = new model.module.Content();
                List<model.db.content> offers = model.module.Content.getFirstXContentInCategory(control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt.offerCategory, 0);
                foreach (var item in offers)
                {
                    List<model.db.content_detail> details = ctnt.getContentDetailsByContentId(item._cid);
                    model.db.content_detail selectedDetail = null;
                    foreach (var det in details)
                    {
                        if (det.__rlang == Global.getLangFromSession())
                        {
                            if (det.key_word != null && det.key_word.Contains(u.user_name))
                            {
                                selectedDetail = det;
                            }
                            break;
                        }
                    }
                    if (selectedDetail != null)
                    {
                        string keyword = selectedDetail.key_word;
                        if (keyword != null)
                        {
                            string[] ks = keyword.Split(new char[] { '#', '#' });
                            if (u.user_name.Equals(ks[2]))
                            {
                                string offerText = selectedDetail.text;
                                if (offerText == null)
                                    offerText = "";
                                if (offerText.Length > 150)
                                    offerText = offerText.Substring(0, 150);
                                ret += "<div><li style='align:left;'><a href='/bnp/offer.aspx?id=" + item._cid + "'  <h2>" + selectedDetail.title + "</h2><b>" + offerText + "</b></a></li><div>";
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex) {
            ret = "<h4>An Error accured during try to get Offers<br/>Reason:" + ex.Message + "</h4>";

        }
        return ret;
    }

    private String getMyVouchers() {
        try
        {
            string ret = "";
            model.db.user u = (model.db.user)System.Web.HttpContext.Current.Session["user"];
            if (u != null)
            {
                ret += "<div><ul>";
                model.module.Content ctnt = new model.module.Content();
                List<model.db.content> vouchers = model.module.Content.getFirstXContentInCategory(control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt.voucherCategory, 0);
                foreach (var item in vouchers)
                {
                    List<model.db.content_detail> details = ctnt.getContentDetailsByContentId(item._cid);
                    model.db.content_detail selectedDetail = null;
                    foreach (var det in details)
                    {
                        if (det.__rlang == Global.getLangFromSession())
                        {
                            if (det.key_word != null && det.key_word.StartsWith(u.user_name))
                            {
                                selectedDetail = det;
                            }
                            break;
                        }
                    }
                    if (selectedDetail != null)
                    {
                        string keyword = selectedDetail.key_word;
                        if (keyword != null) {
                            string[] ks = keyword.Split(new char[] { '#', '#' });
                            if(u.user_name.Equals(ks[0]))
                                ret += "<li style='align:left;'><a href='#'  onclick='bnpmgmt.displyVoucherInfo(\"" + selectedDetail._cdid + "\", \"" + item._cid + "\")'><b>" + selectedDetail.title + "</b></a></li>";
                        }
                    }
                }
                ret += "</ul></div>";
            }
            return ret;
        }
        catch (Exception ex)
        {
            Log.logErr("Partener.getComapnyClients", ex);
        }
        return "";
    }
}