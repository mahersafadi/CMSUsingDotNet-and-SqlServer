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
    public class VerticalBasic1Module : Module
    {
        public VerticalBasic1Module()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public VerticalBasic1Module(model.db.module m)
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
                //ContentModule content = new ContentModule();
                
                //res = @"<h3><a href='Default.aspx?catId=" + this.module.category._cid + "'>" + Lang.getByKey(this.module.category.name) + @"</a></h3>";
                //res += content.generateXContentsByCategoryId_New(Convert.ToInt32(this.module.__rcategory), 0, mdAsDictionary);

                res = @"<div class='easy-block-v1'>
                                <div class='easy-block-v1-badge rgba-purple'><a style='color:#fff' href='Default.aspx?catId=" + this.module.__rcategory.Value + @"'>
                                    " + Lang.getByKey(this.module.category.name) + @"</a></div>";
                res += @"<div id='carousel-example-generic"+this.module._mid+@"' class='carousel slide' data-ride='carousel'>
                      <ol class='carousel-indicators'>";
                List<model.db.content> contents =  new Content().getLastXContentInCategory(this.module.__rcategory.Value, 3, 0);

                int i = 0;
                 foreach (model.db.content content in contents)
                 {

                     if(i==0)
                        res += "<li class='rounded-x active' data-target='#" + "carousel-example-generic" + this.module._mid + @"' data-slide-to='"+i+"'></li>";
                     else
                         res += "<li class='rounded-x' data-target='#" + "carousel-example-generic" + this.module._mid + @"' data-slide-to='" + i + "'></li>";

                     i++;
                     
                 }
                 res += @"</ol>
                            <div class='carousel-inner'>";

                 i = 0;
                 foreach (model.db.content content in contents)
                 {
                     if (i == 0)
                     {
                         res += @"<div class='item active'>
                                            <img alt='' src='" + ContentModule.uploadPath + content.thumbnail + @"'>
                                 </div>";
                         i++;
                     }
                     else
                     {
                         res += @"<div class='item'>
                                            <img alt='' src='" + ContentModule.uploadPath + content.thumbnail + @"'>
                                    </div>";
                     }

                 }
                 res += @"</div>
                                </div>
                            </div>";
//                                res =@"<div id='carousel-example-generic' class='carousel slide' data-ride='carousel'>
//                                    <ol class='carousel-indicators'>
//                                        <li class='rounded-x active' data-target='#"+"carousel-example-generic"+this.module._mid+@"' data-slide-to='0'>
//                                        </li>
//                                        <li class='rounded-x' data-target='#carousel-example-generic' data-slide-to='1'>
//                                        </li>
//                                        <li class='rounded-x' data-target='#carousel-example-generic' data-slide-to='2'>
//                                        </li>
//                                    </ol>
//                                    <div class='carousel-inner'>
//                                        <div class='item active'>
//                                            <img alt='' src='assets/img/job/high-rated-job-1.1.jpg'>
//                                        </div>
//                                        <div class='item'>
//                                            <img alt='' src='assets/img/job/high-rated-job-1.2.jpg'>
//                                        </div>
//                                        <div class='item'>
//                                            <img alt='' src='assets/img/job/high-rated-job-1.3.jpg'>
//                                        </div>
//                                    </div>
//                                </div>
//                            </div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules,BasicModule.generate", ex);
            }
            return res;
        }
    }
}