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
    public class VerticalBasic3Module : Module
    {



        public VerticalBasic3Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public VerticalBasic3Module(model.db.module m)
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
                //res += @"<div class='top-grid'>";
                //res += "<h3>" + Lang.getByKey(this.module.category.name) + "</h3>";
                //res += "<h3> &nbsp;</h3>";
                res = generateContents();
                //res += @"</div>";

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules,VerticalBasic3Module.generate", ex);
            }
            return res;
        }

        public string generateContents()
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                model.db.content content = c.getFirstContentInCategory(this.module.category._cid);
                res += @"
                        <img style='height:400px;' src='" + ContentModule.uploadPath + content.thumbnail + @"' title='image-name' />
                        <a onclick='javascript:Category.show_category(" + this.module.__rcategory.Value + ");return false;' href='#'>" + Lang.getByKey("show_more") + "</a>";


                //res = @"<div class='span_of_3'><!-- start span_of_3 -->

                //res =@"<div class='col-md-4 holder1_of_3'>


//                res = @"<div class='holder smooth'>
//			            <img  src='" + ContentModule.uploadPath + content.thumbnail + @"' alt=''>
//			            
//						<div class='go-top'>
//						   <p>" + Lang.getByKey(this.module.category.name.ToLower() + "_desc") + @"</p>
//						<a onclick='javascript:Category.show_category(" + this.module.__rcategory.Value + ");return false;' href='#'>" + Lang.getByKey("show_more") + @"</a>
//						</div>
//			        </div>
//			     ";

                res = "<div ><h3 style='color: #DC483A;'>" + Lang.getByKey(this.module.category.name) + "</h3></div>";
                res += @"  <div class='row'>
                          <div class='col-sm-6'>
                            <!-- normal -->
                            <div class='ih-item square effect13 from_left_and_right'><a href='#' onclick='javascript:Category.show_category(" + this.module.__rcategory.Value + @");return false;'>
                                <div class='img'><img  src='"+ContentModule.uploadPath + content.thumbnail+@"' alt='img'></div>
                                <div class='info'>
                                  <div class='info-back'>
                                    <h3>" + Lang.getByKey("search") + @"</h3>
                                    
                                  </div>
                                </div></a></div>
                          </div>
                        </div>";
                //<p>" + Lang.getByKey(this.module.category.name.ToLower() + "_desc") + @"</p>
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