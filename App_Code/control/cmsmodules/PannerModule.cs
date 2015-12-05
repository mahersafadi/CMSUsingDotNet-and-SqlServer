using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PannerModule
/// </summary>
namespace control.cmsmodules
{
    public class PannerModule : Module
    {
        public PannerModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public override string generate()
        {
            return "";
        }

        public static string generatePanner(string pannerName, string effect, string height)
        {
            bool choosen = false;
            string str = "<script type=\"text/javascript\">"+
                "$(window).load(function() { "+
                " $('#"+pannerName+"').theatre({ "+
                 " 'effect': '"+effect+"', "+
                 " 'selector': 'img, embed, video', "+
                 " 'controls': true, "+
                 " 'paging': '.paging' "+
             " }); });</script>";
            str += "<div id=\"" + pannerName + "\" style=\"width: 100%; height:"+height+"; margin: auto; visibility: hidden\">";
            model.module.Content ctnt = new model.module.Content();
            List<model.db.content> posters = ctnt.getAllConetentsInCategory(control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt.posterCategory);
            if (posters != null)
            {
                foreach (var item in posters)
                {
                    choosen = false;
                    List<model.db.content_detail> dets = ctnt.getContentDetailsByContentId(item._cid);
                    model.db.content_detail selectedDet = null;
                    foreach (var currDet in dets)
                    {
                        if (currDet.__rlang == Global.getLangFromSession())
                        {
                            selectedDet = currDet;
                            break;
                        }
                    }
                    List<model.db.content_extra> ces = ctnt.getContentExtraByContentId(item._cid);
                    string fromDate = "";
                    string toDate = "";
                    string status = "";
                    string url = "";
                    foreach (var currCE in ces)
                    {
                        if (currCE._key == "panner")
                        {
                            if (currCE._val == pannerName)
                            {
                                choosen = true;
                            }
                        }
                        else if (currCE._key == "status")
                            status = currCE._val;
                        else if (currCE._key == "fromdate")
                            fromDate = currCE._val;
                        else if (currCE._key == "todate")
                            toDate = currCE._val;
                        else if (currCE._key == "url")
                            url = currCE._val;
                    }
                    choosen = choosen && (status == "active") ? true : false;
                    choosen = choosen && DateTime.Now.CompareTo(DateTime.Parse(fromDate)) >= 0;
                    choosen = choosen && DateTime.Parse(toDate).CompareTo(DateTime.Now) >= 0;
                    if (choosen)
                    {
                        str += "<img onclick='goTo1(\"" + url + "\")' style='width:100%; height:100%' src=\"/view/uploads/content_thumbnails/" + item.thumbnail + "\" >";
                    }
                }
            }
            str += "</div>";
            return str;
        }
    }
}