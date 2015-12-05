using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class bnp_Offer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        body1.Attributes.Add("onLoad", "Generator.init()");
        string _contentIdAsStr = Request.QueryString["id"];
        regions.Value = "region_header";
        regionsPriorities.Value = "1";
        if (_contentIdAsStr != null && _contentIdAsStr != "") {
            int _contentId = int.Parse(_contentIdAsStr);
            model.module.Content ctnt = new model.module.Content();
            model.db.content _content = ctnt.getContentById(_contentId);
            model.db.content_detail _cd = ctnt.getContentDetail(_contentId, "en");
            model.module.JobTypes jt = new model.module.JobTypes();
            string[] p = _cd.key_word.Split(new char[] { '#', '#' });
            string res = "<div id=\"container_marketing\">";
            res += "<br clear='all' />";
                res += "<div id=\"row\">";
                res += "<div id=\"row_conten\">";
                res += "<div id=\"offer_box\">";
            res += "<div id=\"offer_box1\">";
            res += "<h2>" + _cd.title + "</h2>";
            res += "<h3>" + (p.Length > 0 ? p[0] : "") + "</h3>";
            res += "<img src=\"/view/uploads/content_thumbnails/" + _content.thumbnail + "\" >";
            res += "</div>";
            res += "<div id=\"offer_box2\">";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<br clear='all'>";
            res += "<ul><li>Mobile</li><li>" + ctnt.getContentExtraValueByKey(_contentId, "mobile") + "</li></ul>";
            res += "<ul><li>Phone</li><li>" + ctnt.getContentExtraValueByKey(_contentId, "phone") + "</li></ul>";
            res += "<ul><li>Region</li><li>" + ctnt.getContentExtraValueByKey(_contentId, "region") + "</li></ul>";
            res += "<ul><li>Address</li><li>" + ctnt.getContentExtraValueByKey(_contentId, "address") + "</li></ul>";
            String jtName = jt.getNameById(int.Parse(ctnt.getContentExtraValueByKey(_contentId, "category")));
            if(jtName == "" || jtName == null)
                jtName = "&nbsp;";
            res += "<ul><li>Category</li><li>"+jtName+"</li></ul>";
            res += "<ul><li>Email</li><li>" + ctnt.getContentExtraValueByKey(_contentId, "email") + "</li></ul>";
            res += "<ul><li>Website</li><li>" + ctnt.getContentExtraValueByKey(_contentId, "website") + "</li></ul>";
            res += "<ul>" +
                "<li>Opening Hours</li>" +
                "<li style=\"color:#E51071;\">Mon to Fri: " + ctnt.getContentExtraValueByKey(_contentId, "open") + "am - " + ctnt.getContentExtraValueByKey(_contentId, "close") + " pm<br>" +
                "Sat: " + ctnt.getContentExtraValueByKey(_contentId, "sat_open") + " am - " + ctnt.getContentExtraValueByKey(_contentId, "sat_close") + " pm<br>" +
                "Sun: " + ctnt.getContentExtraValueByKey(_contentId, "sun_open") + " am - " + ctnt.getContentExtraValueByKey(_contentId, "sun_close") + " am</li>" +
              "</ul>";
            res += "<br clear=\"all\">";
            res += "<ul><li></li></ul>";
            res += "<div id=\"follow_us\"><h2>Follow On</h2>";
            res += "<a href=\"" + ctnt.getContentExtraValueByKey(_contentId, "facebook") + "\" class=\"social_icon\"><img src=\"/view/images/fb.png\" > </a>";
            res += "<a class=\"social_icon\" href=\"" + ctnt.getContentExtraValueByKey(_contentId, "tweeter") + "\"><img src=\"/view/images/twitter.png\" > </a>";
            res += "<a class=\"social_icon\" href=\"" + ctnt.getContentExtraValueByKey(_contentId, "google") + "\"><img src=\"/view/images/gmail.png\" > </a>";
            res += "<a class=\"social_icon\" href=\"" + ctnt.getContentExtraValueByKey(_contentId, "instagram") + "\"><img src=\"/view/images/insta.png\" > </a>";
            res += "<a class=\"social_icon\" href=\"" + ctnt.getContentExtraValueByKey(_contentId, "youtube") + "\"><img src=\"/view/images/youtube.png\" > </a>";
            res += "</div>";
            res += "<br clear=\"all\"> ";
            res += "</div>";
            res += "<br clear=\"all\">";
            res += "<h2>Description The Offer</h2><br clear=\"all\">";
            res += "<p>" + _cd.text + "</p>";
            res += "<div class=\"offer_footer\">";
            res += "<ul>";
            res += "<li><img src=\"/view/images/Upinit.fw.png\"><span>0</span></li>";
            res += "<li><img src=\"/view/images/twitter_offer.fw.png\"><span>0</span></li>";
            res += "<li><img src=\"/view/images/like.fw.png\"><span>0</span></li>";
            res += "</ul>";
            res += "<br clear='all' />";
            res += "</div></div></div></div></div><div id=\"offer_box\">";
            res += "<br clear='all' />";
            content.InnerHtml = res;
        }
    }
}