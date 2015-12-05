using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using control.cmsmodules;

/// <summary>
/// Summary description for SlideShowModule
/// </summary>
namespace control.cmsmodules { 
    public class SlideShowModule : Module
    {
        public SlideShowModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public SlideShowModule(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";
            try
            {
                //    res = @"<div class='slider-wrap'>
                //      <div class='slider'>
                //        <img src='web/slider/img/img06.jpg'/><p><strong>News: </strong> TITLE HERE..TITLE HERE..TITLE HERE..TITLE HERE..</p>
                //        <img src='web/slider/img/img07.jpg'/><p><strong>News: </strong> TITLE HERE..TITLE HERE..TITLE HERE..TITLE HERE..</p>
                //        <img src='web/slider/img/img05.jpg'/><p><strong>News: </strong> TITLE HERE..TITLE HERE..TITLE HERE..TITLE HERE..</p>
                //        <img src='web/slider/img/img02.jpg'/><p><strong>News: </strong> TITLE HERE..TITLE HERE..TITLE HERE..TITLE HERE..</p>
                //        
                //      </div>
                //    </div>";

                List<model.db.content> contents = model.module.Content.getLastXContentInCategory(control.cmsmodules.bnp.BnpVoucherOfferNewsSlideShowMgmt.slideShowCategory, Convert.ToInt32(mdAsDictionary["num_of_contents"]) + 1);
                model.module.Content c = new model.module.Content();

                //            res = @"<div id='page'>
                //			<ul id='slider' >";
                //            foreach (model.db.content content in contents)
                //            {
                //                model.db.content_detail currCOntentDetail = c.getContentDetail(content._cid, LangModule.sessionLang);
                //                res += "<li><img style='height:100%; width:100%' src='" + control.cmsmodules.ContentModule.uploadPath + currCOntentDetail.content.thumbnail + "' alt='' />";
                //                res += @"<div class='rhino-caption' onmouseover=javascript:this.style.cursor='pointer' onclick='javascript:Content.show_content(" + currCOntentDetail.__rcontent + ");return false;'><strong>" + currCOntentDetail.title + @"</strong> </div></li>";
                //            }
                //            res += "</ul></div>";


                //            res = @"<div id='page'>
                //			<ul id='slider' >
                //				<li><img style='height:100%' src='web/slider/img/img07.jpg' alt='' />
                //                    <div class='rhino-caption'><strong>Image 11111</strong> -
                //				        <a href='#'>Title goes here..</a>
                //			        </div>
                //                </li>
                //				<li><img style='height:100%' src='web/slider/img/img08.jpg' alt='' />
                //                    <div class='rhino-caption' ><strong>Image 22222</strong> -
                //				        <a href='3'>Title goes here..</a>
                //			        </div>
                //                </li>
                //				<li><img style='height:100%' src='web/slider/img/img06.jpg' alt='' />
                //                    <div class='rhino-caption' ><strong>Image 333333</strong> -
                //				        <a href='#'>Title goes here..</a>
                //			        </div>
                //                </li>
                //			</ul>
                //		</div>";


                int i = 0;
                res = @"<div class='fluid_container'>";
                res += "<div class='camera_wrap camera_azure_skin' id='camera_wrap_1'>";
                foreach (model.db.content content in contents)
                {
                    if (i == 0)
                    {
                        i++;
                    }
                    else
                    {
                        model.db.content_detail currCOntentDetail = c.getContentDetail(content._cid, LangModule.sessionLang);
                        res += "<div data-thumb='soso' data-src='" + control.cmsmodules.ContentModule.uploadPath + currCOntentDetail.content.thumbnail + "'>";
                        res += @"<div class='camera_caption fadeFromBottom'>
                                    " + currCOntentDetail.title + "</div>";
                        res += "</div>";
                    }

                }

                i = 0;

                res += "</div>";
                res += "</div>";

                res = @"<div class='carousel-inner'>";

                foreach (model.db.content content in contents)
                {

                    model.db.content_detail currCOntentDetail = c.getContentDetail(content._cid, LangModule.sessionLang);

                    if (i == 0)
                    {
                        res += @"<div class='item active'>
                                    <img alt='' style='height:60vh;width:100%' src='" + control.cmsmodules.ContentModule.uploadPath + currCOntentDetail.content.thumbnail + @"'>
                                    <div class='carousel-caption'>
                                        <a href='#' onclick=window.open('Default.aspx?id=" + currCOntentDetail.content._cid + @"','_self')><p>" + currCOntentDetail.title + @"</p></a>
                                    </div>
                                </div>";
                        i++;
                    }
                    else
                    {
                        res += @"<div class='item'>
                                    <img alt='' style='height:60vh;width:100%' src='" + control.cmsmodules.ContentModule.uploadPath + currCOntentDetail.content.thumbnail + @"'>
                                    <div class='carousel-caption'>
                                        <a href='#' onclick=window.open('Default.aspx?id=" + currCOntentDetail.content._cid + @"','_self')><p>" + currCOntentDetail.title + @"</p></a>
                                    </div>
                                </div>";
                    }
                }

                res += @"</div>
                            <div class='carousel-arrow'>
                                <a data-slide='prev' href='#myCarousel-1' class='left carousel-control'><i class='fa fa-angle-left'>
                                </i></a><a data-slide='next' href='#myCarousel-1' class='right carousel-control'><i
                                    class='fa fa-angle-right'></i></a>
                            </div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodule.SlideSHowModule", ex);
            }
            return res;
        }
    }
}