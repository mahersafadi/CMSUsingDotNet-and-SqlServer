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
    public class VerticalBasic4Module : Module
    {



        public VerticalBasic4Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public VerticalBasic4Module(model.db.module m)
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
                res += generateContents();
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
                string res = @"<div class='tips'>
                            <h3>" + Lang.getByKey(this.module.category.name) + @"</h3>
                            <div class='menu_container'>";
                model.module.Content c = new Content();
                List<model.db.content> contents = c.getLastXContentInCategory(this.module.__rcategory.Value, Convert.ToInt32(mdAsDictionary["num_of_contents"]), 0);

                foreach (model.db.content content in contents)
                {
                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(content._cid))
                    {

                        if (LangModule.sessionLang == cd.__rlang)
                        {
                            res += @"<p class='menu_head'>
                                    " + cd.title + @"<span class='plusminus'>+</span></p>
                                <div class='menu_body' style='display: none;'>
                                    " + cd.text + @"</div>";
                        }
                    }
                }
                res += "</div>";
                //if (mdAsDictionary["show_more"] == "true")
                //{
                //    res += "<a class='button' onclick='javascript:Category.show_category(" + this.module.__rcategory.Value + ");' href='#'>" + Lang.getByKey("show_more") + @"</a>";
                //}
                res += "</div>";


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