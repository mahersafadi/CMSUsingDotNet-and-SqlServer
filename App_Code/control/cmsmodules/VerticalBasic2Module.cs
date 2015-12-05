using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.module;

/// <summary>
/// Summary description for BasicModule
/// </summary>
/// 
namespace control.cmsmodules
{
    public class VerticalBasic2Module : Module
    {



        public VerticalBasic2Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public VerticalBasic2Module(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";

            //#B81D22 prevoius red color
            try
            {
                ContentModule content = new ContentModule();
                //res += "<div class='latest-news-grid' >";
                //res += "<h3>" + Lang.getByKey(this.module.category.name) + "</h3>";
                //res += content.generateXContentsByCategoryId(Convert.ToInt32(this.module.__rcategory), 0, mdAsDictionary);
                //res += "</div>";
                //res += "<div class='latest-news-grids'>";
                //res += "<h3>" + Lang.getByKey(this.module.category.name) + "</h3>";
                res = generateContents();
                //res += "</div>";

                


            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules,BasicModule.generate", ex);
            }
            return res;
        }

        public string generateContents()
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                //Convert.ToInt32(mdAsDictionary["num_of_contents"])
                List<model.db.content> contents = c.getLastXContentInCategory(this.module.__rcategory.Value, 1, 0);
                //if (mdAsDictionary["image_header"] == "1")
                  //  res += "<p>" + "<img style='width:100%; height:150px' src='" + ContentModule.uploadPath + contents[0].thumbnail + "' />" + "</p>";
                //else if (mdAsDictionary["image_header"] == "2")
                  //  res += "<p>" + "<img style='width:100%; height:150px' src='" + ContentModule.uploadPath + contents[contents.Count - 1].thumbnail + "' />" + "</p>";


                foreach (model.db.content content in contents)
                {
                    List<model.db.content_detail> lst = c.getContentDetailsByContentId(content._cid);
                    foreach (model.db.content_detail cd in lst)
                    {

                        if (LangModule.sessionLang == cd.__rlang)
                        {
                            //res += @"<div class='latest-news-grid'>";
                            //if (mdAsDictionary["show_content_thumbnail"] == "true")
                            //{
                            //    res += "<div class='latest-news-pic'>";
                            //    res += "<img src='" + ContentModule.uploadPath + content.thumbnail + "' />";
                            //    res += "</div>";
                            //}

                            //res += "<div class='latest-news-info'>";
                            //if (mdAsDictionary["show_create_date"] == "true")
                            //    res += "<a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;'>" + cd.create_date.Value.ToString("yyyy-MM-dd hh:mm") + "</a>";
                            //if (mdAsDictionary["show_title"] == "true")
                            //    res += "<p onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;'>" + Global.getPartOfString(cd.title, Convert.ToInt32(mdAsDictionary["num_of_title_characters"])) + @"</p>";
                            //if (mdAsDictionary["show_text"] == "true")
                            //    res += "<p>" + Global.getPartOfString(cd.text, Convert.ToInt32(mdAsDictionary["num_of_text_characters"])) + @"</p>";
                            //res += "</div>";
                            //res += "<div class='clear'> </div>";

                            //res += "</div>";
                            //easy-block-v1-badge rgba-purple
                            string lblColor = this.module.category.name.ToUpper() == "SESSIONS" ? "label-yellow" : "label-blue";
                            res += @"<div class='magazine-news-img' style='max-height:200px'>
                                    <a href='Default.aspx?id=" + cd.content._cid +@"'>
                                        <img class='img-responsive' style='height:200px' src='" + ContentModule.uploadPath + content.thumbnail + @"' alt=''></a> <span class='magazine-badge " + lblColor + @"'>
                                            " +Lang.getByKey(this.module.category.name)+@"</span>
                                </div>
                                <h3>
                                    <a href='Default.aspx?id=" + cd.content._cid + @"'>" + cd.title + @"</a></h3>
                                <div class='by-author'>
                                    <strong></strong> <span>" +Convert.ToDateTime(cd.create_date.ToString()).ToString("yyyy/MM/dd")+@"</span>
                                </div>
                                <p>"+Global.getPartOfString(Global.trimAllHTMLTags(cd.text), 250)+@"</p>";
                        }
                    }
                }

                res += @"<a href='Default.aspx?catId=" + this.module.__rcategory.Value + "' class='btn-u btn-u-md'><i class='fa fa-chevron-left'></i> " + Lang.getByKey("more_about") + " " + Lang.getByKey(this.module.category.name) + "</a>";

                //if (mdAsDictionary["show_more"] == "true")
                //{
                //    res += "<a class='button' onclick='javascript:Category.show_category(" + this.module.__rcategory.Value + ");return false;' href='#'>" + Lang.getByKey("show_more") + @"</a>";
                //}

                
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }
    }
}