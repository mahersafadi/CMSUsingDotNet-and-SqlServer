using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using control.cmsmodules;
using model.module;

/// <summary>
/// Summary description for LawDecreePopulationModule
/// </summary>
/// 
namespace control.cmsmodules
{
    public class LawDecreePopulationModule : Module
    {
        public LawDecreePopulationModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public LawDecreePopulationModule(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }


        public override string generate()
        {
            string res = "";
            try
            {
                res = @"<div class='funny-boxes funny-boxes-top-sea'>
                                <div class='row'>
                                    <div class='col-md-4 funny-boxes-img'>
                                        <img class='img-responsive' src='assets/img/law.jpg' alt=''>
                                    </div>
                                    <div class='col-md-8'>
                                        <h2>
                                            <a href='Default.aspx?ldp=1'><i class='fa fa-calendar-o'> &nbsp;</i>" + Lang.getByKey("law_decree_population") + @"</a></h2>
                                        <ul class='list-unstyled funny-boxes-rating'>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                        </ul>
                                        <p>" + Lang.getByKey("law_decree_population_desc") + @"</p>
                                    </div>
                                </div>
                            </div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules,BasicModule.generate", ex);
            }

            return res;
        }

        public string generateSearchEngine()
        {
            string res = "";
            res = "<div class='headline'><h2>" + Lang.getByKey("law_decree_population") + @"</h2></div>";
            res += @"<div class='panel panel-grey margin-bottom-40'>
                    <div class='panel-heading'>
                        <h3 class='panel-title'><i class='fa fa-tasks'></i> " + Lang.getByKey("search") + @"</h3>
                    </div>
                    <div class='panel-body'>
                        <div class='row margin-bottom-40'>
                            <div class='col-md-6'>
                                <div class='input-group'>
                                    <span class='input-group-addon'>@</span>
                                    <input type='text' class='form-control' id='search_name' placeholder='" + Lang.getByKey("name") + @"'>
                                </div>

                                <div class='margin-bottom-10'></div>

                                <div class='input-group'>
                                    <span class='input-group-addon'>
                                        @
                                    </span>
                                    <input type='text' class='form-control' id='search_specification' placeholder='" + Lang.getByKey("specification") + @"'>
                                </div>

                            </div>
							
                            <div class='col-md-6'>
                                <div class='input-group'>
                                    <span class='input-group-addon'>
                                        @
                                    </span>
                                    <input type='text' id='search_city' class='form-control' placeholder='" + Lang.getByKey("city") + @"'>
                                </div>

                                <div class='margin-bottom-10'></div>

                                <div class='input-group'>
                                    <span class='input-group-addon'>
                                        @
                                    </span>
                                    <select  class='btn btn-default dropdown-toggle' style='width:100%;height:35px'>
                                        <option value='0'>" + Lang.getByKey("type")+@"</option>
                                        <option value='1'>"+Lang.getByKey("law")+@"</option>
                                        <option value='2'>" + Lang.getByKey("decree") + @"</option>
                                        <option value='3'>" + Lang.getByKey("population") + @"</option>
                                    </select>
                                </div>
                            </div>
                            <!--End Checkboxes and Radio Addons-->
                        </div>
                         <a href='#' style='background:#72c02c;' class='btn-u btn-u-green' onclick='javascript:Engineer_Sort.search();return false;' >" + Lang.getByKey("search") + @"</a>
                        </div>
                </div>
                <div id='eng_search_result'></div><div id='eng_show_more' ></div>";
            return res;
        }
        
    }
}