using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.module;

/// <summary>
/// Summary description for EngSortModule
/// </summary>
/// 
namespace control.cmsmodules
{
    public class EngSortModule : Module
    {
        public EngSortModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public EngSortModule(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";
            try
            {
                ContentModule content = new ContentModule();
                string css = model.module.ModuleInModelLayer.getCSSAttribtues(mdAsDictionary);
                int numOfContents = model.module.ModuleInModelLayer.getNumOfContents(mdAsDictionary);
                string bgColor = model.module.ModuleInModelLayer.getCSSAttribute(mdAsDictionary, "background-color");
                //int numOfCharacters = Convert.ToInt32(mdAsDictionary["num_of_characters"]);

                //res += "<div style=" + css + ">";
                res += @"<div class='top-grid'>";
                res += "<div style='background-color: " + bgColor + "'><h3 style='color: #DC483A;'>" + Lang.getByKey(this.module.category.name) + "</h3></div>";
                res += content.generateXContentThumbsByCategoryId(Convert.ToInt32(this.module.__rcategory), 1);
                res += "</div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.EngSortModule.generate", ex);
            }
            res = "<div ><h3 style='color: #DC483A;'>" + Lang.getByKey(this.module.category.name) + "</h3></div>";
            res += @"  <div class='row'>
                          <div class='col-sm-6'>
                            <!-- normal -->
                            <div class='ih-item square effect13 from_left_and_right'><a href='#' onclick='javascript:Engineer_Sort.generate_search_engine();return false;'>
                                <div class='img'><img style='height: 190px;' src='uploads/content_thumbnails/239_content_thumbnail_20140421.jpg' alt='img'></div>
                                <div class='info'>
                                  <div class='info-back'>
                                    <h3>" + Lang.getByKey("search")+@"</h3>
                                    <p>" + Lang.getByKey(this.module.category.name.ToLower() + "_desc") + @"</p>
                                  </div>
                                </div></a></div>
                          </div>
                        </div>";

            res = @"<div class='funny-boxes funny-boxes-top-yellow'>
                                <div class='row'>
                                    <div class='col-md-4 funny-boxes-img'>
                                        <img class='img-responsive' src='assets/img/new/img11.jpg' alt=''>
                                        <ul class='list-unstyled fa-fixed'>
                                            
                                        </ul>
                                    </div>
                                    <div class='col-md-8'>
                                        <h2>
                                            <a href='Default.aspx?eng_order=1'><i class='fa fa-briefcase'> &nbsp;</i>" + Lang.getByKey(this.module.category.name) + @"</h2>
                                        <ul class='list-unstyled funny-boxes-rating'>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                            <li><i class='fa fa-star'></i></li>
                                        </ul>
                                        <p>نتائج فرز المهندسين المتقدمين للحصول على تعيين في الجهات العامة</p>
</a>
                                    </div>
                                </div>
                            </div>";
            return res;
        }

        public string generateSearchEngine()
        {
            string res = "";
            try
            {
                res += "<center><h3>" + Lang.getByKey("eng_sort") + "</h3></center>";
                res += @"<table class='gridengtable'>
                        <tr>
                            <th colspan='4'></th>
                        </tr>
                        <tr>
                            <td>" + Lang.getByKey("name") + @"</td>
                            <td><input type='text' id='search_name' ></td>
                            <td>" + Lang.getByKey("specification") + @"</td>
                            <td><input type='text' id='search_specification'></td>
                        </tr>
                        <tr>
                            <td>" + Lang.getByKey("city") + @"</td>
                            <td><input type='text' id='search_city' ></td>
                            <td>" + Lang.getByKey("ministry") + @"</td>
                            <td><input type='text' id='search_ministry'></td>
                        </tr>
                        <tr>
                            <th colspan='4'><a href='#' onclick='javascript:Engineer_Sort.search();return false;' >" + Lang.getByKey("search") + @"</a></th>
                        </tr>
                    </table>
                    <div id='eng_search_result'></div><div id='eng_show_more' ></div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.EngSortModule.generateSearchEngine", ex);
            }

            res = "<div class='headline'><h2>" + Lang.getByKey("eng_sort") + @"</h2></div>";
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
                                    <input type='text' class='form-control' id='search_ministry' placeholder='" + Lang.getByKey("ministry") + @"'>
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

        public string generateSearchEngineResult(Dictionary<string, string> dic)
        {
            try
            {
                string res = "";
                List<model.db.eng_sort> engs = model.module.EngSort.searchEngineers(dic["name"], dic["specification"], dic["city"], dic["ministry"], Convert.ToInt32(dic["fromId"]));
                if (engs.Count > 0 && dic["fromId"] == "0")
                {
                    res += @"<table class='table table-striped' id='tbl_res' >
                                <tr>
                                <th>
                                    " + Lang.getByKey("name") + @"
                                </th>
                                <th>
                                    " + Lang.getByKey("specification") + @"
                                </th>
                                <th>
                                    " + Lang.getByKey("city") + @"
                                </th>
                                <th>
                                    " + Lang.getByKey("ministry") + @"
                                </th>
                                <tr>";
                }
                foreach (model.db.eng_sort eng in engs)
                {
                    res += "<tr>";
                    res += "<td>" + eng.name + "</td>";
                    res += "<td>" + eng.specification + "</td>";
                    res += "<td>" + eng.city + "</td>";
                    res += "<td>" + eng.ministry + "</td>";
                    res += "</tr>";
                }

                if (engs.Count > 0 && dic["fromId"] == "0")
                    res += "<table>";
                res += "###";
                if (engs.Count > 0)
                    if (engs[engs.Count - 1]._eid.ToString() != dic["fromId"] && engs.Count >= 20)
                        res += "<table class='gridtable' style='width:100%'><tr><th style='width:100%'><center> <a href='#' onclick='javascript:Engineer_Sort.search_more(" + engs[engs.Count - 1]._eid + "); return false;'>  " + Lang.getByKey("show_more") + " </a> </center></th></tr></table>";

                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("contro.cmsmodules.EngSortModule.generateSearchEngineResult", ex);
            }
            return "";
        }
        public static string EngSortHandler(string mode, string sName, string sSpecification, string sCity, string sMinistry, string fromId)
        {
            string res = "";
            try
            {
                EngSortModule eng = new EngSortModule();
                switch (mode)
                {
                    case "1":
                        res = eng.generateSearchEngine();
                        break;
                    case "2":
                    case "3":
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("name", sName);
                        dic.Add("specification", sSpecification);
                        dic.Add("city", sCity);
                        dic.Add("ministry", sMinistry);
                        dic.Add("fromId", fromId);
                        res = eng.generateSearchEngineResult(dic);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.EngSortModule.EngSortHandler", ex);
            }
            return res;
        }
    }
}
