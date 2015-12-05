using model.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for JqTilesSlideShow
/// </summary>
namespace control.cmsmodules
{
    public class JqTilesSlideShow: Module
    {
        public JqTilesSlideShow()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public JqTilesSlideShow(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";
            try
            {
                string width = "720px";
                try { width = mdAsDictionary["width"]; }
                catch { ; }
                string height = "400px";
                try { height = mdAsDictionary["height"]; }
                catch { ; }
                int numberOfSlides = 2;
                try { numberOfSlides = Int32.Parse(mdAsDictionary["num_of_slides"]); }
                catch { ; }
                string nextPrev = "true";
                try { nextPrev = mdAsDictionary["next_prev"]; }
                catch { ; }
                string sel_img = "true";
                try { sel_img = mdAsDictionary["sel_img"]; }
                catch { ; }
                string show_content_title = "true";
                try { show_content_title = mdAsDictionary["show_content_title"]; }
                catch { ; }
                string effect = "default";
                try { effect = mdAsDictionary["effect"]; }
                catch { ; }
                List<model.db.content> contents = model.module.Content.getLastXContentInCategory(Convert.ToInt32(this.module.__rcategory), numberOfSlides);
                model.module.Content c = new model.module.Content();
                res = "<div class=\"slider-wrap\"><div class=\"slider\">";
                foreach (model.db.content content in contents)
                {
                    model.db.content_detail currCOntentDetail = c.getContentDetail(content._cid, LangModule.sessionLang);
                    string img = "view/uploads/content_thumbnails/" + currCOntentDetail.content.thumbnail;
                    string title = currCOntentDetail.title;
                    res += "<img src=\""+img+"\"/><p><strong>"+title+"</strong></p>";
                }
                res += "</dic></div>";
                res += @"<script type='text/javascript'>
		                
                    </script>
                " + "!@#$runSlideShow";
            }
            catch (Exception ex) 
            {
                Log.logErr("Error During generate JqTilesSlideShow ", ex);
            }
            return res;
        }

        public string generate1()
        {
            string res = "";
            string p1 = "<div class=\"tiles-wrap tiles-wrap-current\" style=\"display: block;\">";
            string p2 = "";
            string p3 = "<div class=\"tiles-nav\" style=\"margin-left: -141px;\">";

            try
            {
                string width = "720px";
                try { width = mdAsDictionary["width"]; }
                catch { ; }
                string height = "400px";
                try { height = mdAsDictionary["height"] ; }
                catch { ; }
                int numberOfSlides = 2;
                try { numberOfSlides = Int32.Parse(mdAsDictionary["num_of_slides"]); }
                catch { ; }
                string nextPrev = "true";
                try { nextPrev = mdAsDictionary["next_prev"]; }
                catch { ; }
                string sel_img = "true";
                try { sel_img = mdAsDictionary["sel_img"]; }
                catch { ; }
                string show_content_title = "true";
                try { show_content_title = mdAsDictionary["show_content_title"]; }
                catch { ; }
                string effect = "default";
                try { effect = mdAsDictionary["effect"]; }
                catch { ; }
                List<model.db.content> contents = model.module.Content.getLastXContentInCategory(Convert.ToInt32(this.module.__rcategory), numberOfSlides);
                model.module.Content c = new model.module.Content();
                int i = 0;
                foreach (model.db.content content in contents)
                {
                    model.db.content_detail currCOntentDetail = c.getContentDetail(content._cid, LangModule.sessionLang);
                    string img = "view/uploads/content_thumbnails/"+ currCOntentDetail.content.thumbnail;
                    string title = currCOntentDetail.title;
                    p1 += "<div class=\"tiles-tile tiles-switchud-normal tiles-x1 tiles-y8 tiles-even\" style=\"width: 600px; height: 50px; background-image: url('"+img+"'); background-position: 0px 0px;\"></div>";
                    p2 += "<div class=\"tiles-description\">" +
                            "<p><strong>" + title + ":</strong>.</p>" +
                          "</div>" +
                          "<img src=\"view/uploads/content_thumbnails/"+currCOntentDetail.content.thumbnail+"\" style=\"display: none;\">";
                    p3 += "<a class=\"tiles-nav-item tiles-bullet\" href=\"#\">"+
			                 "<span style=\"top: -112px; left: -57px;\">"+
			                  "<img src=\"view/uploads/content_thumbnails/"+currCOntentDetail.content.thumbnail+"\" style=\"height: 80px; width: 120px;\">"+
			                 "</span>"+
		                  "</a>";
                }
                p1 += "</div>";
                p3 += "</div>";
                res = "<div class=\"slider tiles-slider-wrap tiles-slider-switchud tiles-slider-s300\">";
                res += p1;
                if(show_content_title == "true")
                    res += p2;
                if(sel_img == "true")
                    res += p3;
                if (nextPrev == "true")
                {
                    res += "<a class=\"tiles-prev\" href=\"#\">« " + Lang.getByKey("Prev") + "</a><a class=\"tiles-next\" href=\"#\">" + Lang.getByKey("Next") + " »</a>";
                }
                
                res += "</div>";
                res += @"<script type='text/javascript'>
                    funtion runSlideShow(args){
                        try{
                            alert(1);
                            var $slider = $('.slider-wrap');
                            var html = $slider.html();
                            var defaults = {
                                thumbSize: 20,
                                onSlideshowEnd: function(){ $('.stop, .start').toggle() }
                            };
                            var effects = {
                                'default'  : { x:6, y:4, random: true },
                                'simple'   : { x:6, y:4, effect: 'simple', random: true },
                                'left'     : { x:1, y:8, effect: 'left' },
                                'up'       : { x:20, y:1, effect: 'up', rewind: 60, backReverse: true },
                                'leftright': { x:1, y:8, effect: 'leftright' },
                                'updown'   : { x:20, y:1, effect: 'updown', cssSpeed: 500, backReverse: true },
                                'switchlr' : { x:20, y:1, effect: 'switchlr', backReverse: true },
                                'switchud' : { x:1, y:8, effect: 'switchud' },
                                'fliplr'   : { x:20, y:1, effect: 'fliplr', backReverse: true },
                                'flipud'   : { x:20, y:3, effect: 'flipud', reverse: true, rewind: 75 },
                                'reduce'   : { x:6, y:4, effect: 'reduce' },
                                '360'      : { x:1, y:1, effect: '360', fade: true, cssSpeed: 600 }
                            };
                            setTimeout(function() {
                                $('.slider').tilesSlider( $.extend( {}, defaults, effects[ '" + effect+"' ] ) );";                 
                            res +=   @"$slider.fadeTo( 0,1 );
                                $('body').removeClass('tiles-preload');
                            }, 100 );
                            $('.code').empty().html(function() {
                                var e = effects['"+effect+"'], c = [];";
                            res +=   @" for ( var i in e ) {
                                if ( i !== 'effect' ) {
                                    c.push('<code>'+ i +': '+ e[i] +'</code>');
                                }
                            });
                            alert(11);
                            $('.slider').tilesSlider('start');
                        }
                        catch(e){
                            alert(e.message);
                        }
                    }
                </script>!@#$runSlideShow";
            }
            catch (Exception ex) {
                Log.logErr("Error During generate JqTilesSlideShow ", ex);
            }
            return res;
        }
    }
}