using model.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BnpLatestNewsModule
/// </summary>

namespace control.cmsmodules.bnp
{
    public class BnpLatestNewsModule : Module
    {
        public BnpLatestNewsModule()
        {
            this.module = new model.db.module();
        }

        public BnpLatestNewsModule(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";
            try
            {
                int numberOfContents = 0;
                if(isAll == false)
                    numberOfContents = int.Parse(mdAsDictionary["num_of_contents"]);

                Content _content = new Content();
                List<model.db.content> contents = null;
                if (numberOfContents > 0)
                    contents = _content.getLastXContentInCategory(this.module.__rcategory.Value, numberOfContents, 0);
                else
                {
                    contents = _content.getAllConetentsInCategory((int)this.module.__rcategory);
                    numberOfContents = contents.Count;
                }
                //get offers based on number,
                res = "<table width=\"100%\">" +
                        "<tr>" +
                           "<td width=\"xxxpx\">" +
                             "<div class=\"bnplatest_news_header\">" +
                                "<div class=\"bnplatest_news_header1\">" + model.module.Lang.getByKey("latest_news") + "</div>";
                if (isAll == false)
                {
                    if (mdAsDictionary["show_all"] == "true")
                    {
                        res += "<div style=\"height:inherit;font-size:13px;color:" + mdAsDictionary["color"] + ";position:relative;float:right;margin-left:0px;margin-bottom: 0px;\" ><a href='/Default.aspx?show_mode=22&&module=107&catId=" + this.module.__rcategory.Value + "' style=\"height:inherit;font-size:13px;color:" + mdAsDictionary["color"] + ";\" >" + model.module.Lang.getByKey("see_all") + "</a></div>";
                    }
                }
                res += "</div>" +
                        "<div class=\"latest_header_line\"></div>" +
                        "<div class=\"latest_offer_body\">";
                res += "<table width='100%'>";
                res += "<tr><td height='20px'></td></tr>";
                for (int kk = 0; kk < numberOfContents; kk++)
                {
                    model.db.content content = contents[kk];
                    List<model.db.content_detail> cd = _content.getContentDetailsByContentId(content._cid);
                    model.db.content_detail selDet = cd[0];
                    int k = 0;
                    while (k < cd.Count() && cd[k].__rlang != LangModule.sessionLang)
                        k++;
                    if (cd[k].__rlang == LangModule.sessionLang)
                        selDet = cd[k];
                    res += "<tr>";
                    res += "<td width='100%'>";
                    res += "<table><tr>";
                    res += "<td>";
                    res += "<img style='border: 1px #EBEBEB solid;' width='" + mdAsDictionary["img_width"] + "' height='" + mdAsDictionary["img_height"] + "px' src='view/uploads/content_thumbnails/" + content.thumbnail + "' />";
                    res += "</td>";
                    res += "<td width='17px'></td>";
                    res += "<td>";
                    res += "<table width='90%'>";
                    res += "<tr><td height='35px'></td></tr>";
                    res += "<tr><td style='color:#3E3A37;font-size:18px;font-weight:bold;' align='left'>" + selDet.title + "</td></tr>";
                    string from = "";
                    string to = "";
                    string t = "";
                    string c = "";
                    try
                    {
                        string[] ks = selDet.key_word.Split(new char[] { '#', '#' });
                        t = ks[0];
                        from = ks[2];
                        to = ks[4];
                        if (selDet.text != null && selDet.text != "")
                        {
                            if (selDet.text.Length > 200)
                                c = selDet.text.Substring(0, 200);
                            else
                                c = selDet.text;
                        }
                        c += ".&nbsp;&nbsp;<a  style='color:" + mdAsDictionary["color"] + ";font-weight:bold;' href='/Default.aspx?id=" + content ._cid+ "'>" + Lang.getByKey("read_more") + "</a>";
                    }
                    catch (Exception ex)
                    {

                    }
                    res += "<tr><td style='color:#404040; align='left' >" + from + "&nbsp;" + Lang.getByKey("till") + "&nbsp;" + to + "</td></tr>";
                    res += "<tr><td style='color:#404040; align='left'>" + t + "</td></tr>";
                    res += "<tr><td  style='color:#404040; align='left'>" + c + "</td></tr>";
                    res += "</table>";
                    res += "</td>";
                    res += "</tr></table>";
                    res += "</td>";
                    res += "</tr>";
                    res += "<tr><td height='15px'></td></tr>";
                }
                res += "</table>";
            }
            catch (Exception ex)
            {
                Log.logErr("An error accured during generate BnpLatestNewsModule", ex);
            }
            return res;
        }
    }
}