using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;
using model.module;

/// <summary>
/// Summary description for ContentModule
/// </summary>
namespace control.cmsmodules
{
    public class ContentModule : Module
    {
        public static string uploadPath = "view/uploads/content_thumbnails/";
        public ContentModule()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static string currentThumbnail = "";
        public static model.db.content currentContent = null;
        public override string generate()
        {
            LangModule lan = new LangModule();

            string res = "";
            if (Security.hasPermission(PermissionName.content.ToString()))
                res += "<button onclick='Content.show_search_content(this)'>" + Lang.getByKey("search_content") + "</button>";
            if (Security.hasPermission(PermissionName.insert_content.ToString()))
                res += "<button onclick='Content.open_new_content(this)'>" + Lang.getByKey("new_content") + "</button>";

            //res += generateSearchContent();
            res += "<div id='div_content_search'></div>";
            res += "<div id='div_content'></div>"; //for add a new content
            //res += generateContents(0,"");

            //            res += @"<div  id='div_upload_file' style = 'margin-top:-75px; margin-left:220px; display: none; position: relative;'>
            //                    <input type='file' id='FileUpload1' />
            //                </div>";

            return res;
        }

        public string generateSearchContent()
        {
            string res = "";
            res = @"<table class='gridtable'>
                        <tr>
                            <th colspan='2'>" + Lang.getByKey("search_content") + @"</th>
                        </tr>
                        <tr>
                            <th>" + Lang.getByKey("category_name") + @"</th>
                            <td> " + generateCategoriesSelect() + @" </td>
                        </tr>
                        <tr>
                            <th> " + Lang.getByKey("key_word") + @"</th>
                            <td> <input type='text' id='content_title' /></td>
                        </tr>
                        <tr>
                            <td colspan='2'> <button onclick='Content.do_search_content(this)' id='content_search' >" + Lang.getByKey("search") + @"</button></td>
                        </tr>
                    </table>";

            return res;
        }

        public string generateNewContent()
        {
            string res = "";
            res += @"
                    <table class='gridtable'>
                        <tr>
                            <th colspan='2'>" + Lang.getByKey("insert_new_content") + @"</th>
                        </tr>
                        <tr>
                            <th>" + Lang.getByKey("category_name") + @"</th>
                            <td style='width:200px'>" + generateCategoriesSelect() + @"</td>
                        </tr>
                        <tr>
                            <td colspan='2'>
                                <button id='ok_new_content' onclick='Content.save_new_content(this)' >" + Lang.getByKey("ok") + @"</button>
                                <button id='cancel_new_content' onclick='Content.cancel_content(this)' >" + Lang.getByKey("cancel") + @"</button>
                            </td>
                        </tr>
                    </table>";

            return res;
        }


        public string generateNewContentDetail()
        {
            string res = "";
            res += @"<table class='gridtable'>
                        <tr>
                            <td>" + Lang.getByKey("content_detail_lang") + @"</td>
                            <td>" + cmsmodules.LangModule.generateLangSelection() + @"</td>
                        </tr>
                        <tr>
                            <td>" + Lang.getByKey("content_detail_title") + @"</td>
                            <td><input type='text' id='content_detail_title' /></td>
                        </tr>
                        <tr>
                            <td>" + Lang.getByKey("content_detail_text") + @"</td>
                            <td><textarea class='ckeditor' cols='80' id='editor1' name='editor1' rows='10' style='visibility: hidden; display: none;'>	
                            </textarea></td>
                        </tr>
                        <tr>
                            <td colspan='2'>
                                <button id='save_content_detail' >save</button>
                            </td>
                        </tr>
                     </table>";
            return res;
        }

        public string generateContents(int catId, string keyWord)
        {
            try
            {
                string res = "";//@"<table class='gridtable'>";
                model.module.Content c = new Content();
                foreach (model.db.content cc in c.getAllConetentsInCategory(catId))
                {
                    foreach (model.db.content_detail cd in c.getContentDetailsByContentIdAndKeyWord(cc._cid, keyWord))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {

                            res += @"<div class='content-list' id='content_div_" + cc._cid + @"' onmouseout='Content.th_mouseout(this)'  onmouseover='Content.th_mouseover(this)'>
                                <table style='width:100%' style='direction: " + Lang.getDirection() + @"'><tr><td style='width:155px;'>
                                <div class='content-thumb' id='" + cc._cid + @"' onclick='Content.open_new_content(this)'>
                                    <img src='/" + uploadPath + cc.thumbnail + "' style='width:150px; height:100px;' />" + @"
                                </div>
                                </td><td style='vertical-align:top; padding:10px;width:60%'>
                                <div class='content-info' id='" + cc._cid + @"' onclick='Content.open_new_content(this)'>
                                    <div class='content-title'>" + cd.title + @"</a></div>
                                    
                                    <div class='content-detail'>" + Convert.ToDateTime(cc.create_date).ToString("yyyy/MM/dd") + @" <span></span> </div>
                                </div>
                                <div class='clear'></div>
                                </td>
                                    <td style='vertical-align: text-top;'>";
                            if (control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.delete_content.ToString()))
                                res += "<img onclick='Content.delete_content(" + cc._cid + ")' title='" + Lang.getByKey("delete") + @"' src='/view/imag/icons/deletered.png' style='width:24px; height:24px;' />";
                            res += "</td></tr></table></div>";


                            //res += "<tr id='" + cc._cid + "' onmouseout='Content.th_mouseout(this)' onclick='Content.open_new_content(this)' onmouseover='Content.th_mouseover(this)'>";
                            //res += "<th>";
                            //res += "<img style='width:200px; height:100px;' src='" + uploadPath + cc.thumbnail + "' />";
                            //res += "</th>";
                            //res += "<th style='width:100%; vertical-align:top;background-color:#dedede; border-bottom-style:none;'>";

                            //res += "&nbsp;&nbsp;" + cd.title + "<br />";
                            //res += "&nbsp;&nbsp;" + cd.text.Substring(0, cd.text.IndexOf(".") + 1);
                            //res += "</th></tr>";
                            //res += "<tr><th colspan='2' style='border-top-style:none;height:10px;'>";
                            //res += "<br />" + Lang.getByKey("category_name") + ": " + Lang.getByKey(cc.category.name);
                            //res += "    | " + Lang.getByKey("create_date") + ": " + Convert.ToDateTime(cd.create_date).ToString("yyyy/MM/dd hh:mm tt");
                            //res += "</th></tr>";
                        }
                    }

                    //res += "" + Lang.getByKey("edit") + "</a>";
                }
                //res += "</table>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }

        public string generateXContentsByCategoryId(int catId, Int32 fromId, Dictionary<string, string> dic)
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                List<model.db.content> contents = c.getLastXContentInCategory(catId, Convert.ToInt32(dic["num_of_contents"]), fromId);
                if (dic["image_header"] == "1")
                {
                    res += "<p>" + "<img style='width:100%; height:200px' src='" + uploadPath + contents[0].thumbnail + "' />" + "</p>";
                    content_detail cdtl = c.getContentDetail(contents[0]._cid, LangModule.sessionLang);
                    if (cdtl != null)
                        res = @"<div class='holder smooth'>
			            <img style='width:100%; height:200px' src='" + uploadPath + contents[0].thumbnail + @"' alt=''>
			            
						<div class='go-top'>
                        
						   <p>" + Global.getPartOfString(Global.trimAllHTMLTags(cdtl.text).Replace("&nbsp;&nbsp;", ""), 350) + @"</p>
						
						</div>
			        </div>
			     ";

                    //<a class='content-detail' onclick='javascript:Content.show_content(" + cdtl.__rcontent + ");return false;' href='#'>" + Lang.getByKey("show_more") + @"</a>
                }
                else if (dic["image_header"] == "2")
                    res += "<p>" + "<img style='width:100%; height:200px' src='" + uploadPath + contents[contents.Count - 1].thumbnail + "' />" + "</p>";

                res += @"<ul>";
                foreach (model.db.content content in contents)
                {
                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(content._cid))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {
                            res += @"<li>";

                            if (dic["show_title"] == "true")
                                res += "<a onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' href='#'>" +
                                      getSomeContentDetailTitle(cd.title, Convert.ToInt32(dic["num_of_title_characters"])) + @"</a>";

                            if (dic["show_content_thumbnail"] == "true")
                                res += "<img style='width:100%; height:170px' src='" + uploadPath + content.thumbnail + "' />";

                            if (dic["show_text"] == "true")
                                res += "<a onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' href='#'>" +
                                      getSomeContentDetailTitle(cd.text, Convert.ToInt32(dic["num_of_text_characters"])) + @"</a>";


                            if (dic["show_create_date"] == "true")
                            {
                                res += "<span>";
                                res += cd.create_date.Value.ToString("yyyy-MM-dd hh:mm");
                                res += "<span>";
                            }
                            res += "</li>";

                        }
                    }
                }
                res += "</ul>";

                if (dic["show_more"] == "true")
                {
                    res += "<a class='button' onclick='javascript:Category.show_category(" + catId + "); return false;' href='#'>" + Lang.getByKey("show_more") + @"</a>";
                }
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }

        public string generateXContentsByCategoryId_New(int catId, Int32 fromId, Dictionary<string, string> dic)
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                List<model.db.content> contents = c.getLastXContentInCategory(catId, 3, fromId);

                foreach (model.db.content content in contents)
                {
                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(content._cid))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {

                            res += @"<span><i class='color-green'>" + cd.create_date + "</i> <br/> " + Global.getPartOfString(cd.title, 100) + @"</span>
                            <div class='magazine-posts-img'>
                                <a href='Default.aspx?id=" + cd.content._cid + @"'>
                                    <img class='img-responsive' src='" + uploadPath + content.thumbnail + @"' alt=''></a> <span class='magazine-badge magazine-badge-red'>
                                        جديد</span>
                            </div>
                            <br />";
                        }
                    }
                }


                //if (dic["show_more"] == "true")
                //{
                //    res += "<a class='button' onclick='javascript:Category.show_category(" + catId + "); return false;' href='#'>" + Lang.getByKey("show_more") + @"</a>";
                //}

                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }

        public string generateXContentsByCategoryId2(int catId, Int32 fromId, Dictionary<string, string> dic)
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                List<model.db.content> contents = c.getLastXContentInCategory(catId, 3, fromId);

                res = @"<div class='sidebar top'>	
				<!---start-da-slider----->
				<div id='da-slider' class='da-slider'>";
                foreach (model.db.content content in contents)
                {
                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(content._cid))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {
                            //res += @"<li>";

                            //if (dic["show_title"] == "true")
                            //    res += "<a onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' href='#'>" +
                            //          getSomeContentDetailTitle(cd.title, Convert.ToInt32(dic["num_of_title_characters"])) + @"</a>";

                            //if (dic["show_content_thumbnail"] == "true")
                            //    res += "<img style='width:100%; height:170px' src='" + uploadPath + content.thumbnail + "' />";

                            //if (dic["show_text"] == "true")
                            //    res += "<a onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' href='#'>" +
                            //          getSomeContentDetailTitle(cd.text, Convert.ToInt32(dic["num_of_text_characters"])) + @"</a>";


                            //if (dic["show_create_date"] == "true")
                            //{
                            //    res += "<span>";
                            //    res += cd.create_date.Value.ToString("yyyy-MM-dd hh:mm");
                            //    res += "<span>";
                            //}
                            //res += "</li>";

                            res += @"<div class='da-slide'>
					<h2>" + cd.title + @"</h2>
					<p><img style='width:40%;' src='" + uploadPath + content.thumbnail + @"' /></p>
					<a class='da-link' target='_blank' href='" + Global.trimAllHTMLTags(cd.text) + @"'>" + Lang.getByKey("website_link") + @"</a>
				</div>";
                            //                            res += @"<div class='da-slide'>
                            //					                    <h2><table style='width:100%'><tr><td style='vertical-align: top; text-align: center; width: 100%'><a href='" + Global.trimAllHTMLTags(cd.text) + "'>" + cd.title + @" 
                            //                                        </td></tr><tr><td style='vertical-align: bottom;'>
                            //                                            <img style='width:40%;' src='" + uploadPath + content.thumbnail + @"' />
                            //                                        </td</tr></table>
                            //                                        </h2>
                            //                                        <a class='da-link' href='details.html'>meet our works </a>
                            //				                    </div>";
                        }
                    }
                }

                res += @"<nav class='da-arrows'>
					<span class='da-arrows-prev'></span>
					<span class='da-arrows-next'></span>
				</nav>
			</div>
 			<!---//End-da-slider----->
	 	
	</div>";
                //res += "</ul>";

                //if (dic["show_more"] == "true")
                //{
                //    res += "<a class='button' onclick='javascript:Category.show_category(" + catId + "); return false;' href='#'>" + Lang.getByKey("show_more") + @"</a>";
                //}

                //                res = @"<h3 class='top-grid'>مواقع صديقة</h3>    <div class='sidebar top'>	
                //				<!---start-da-slider----->
                //				<div id='da-slider' class='da-slider'>
                //				<div class='da-slide'>
                //					<h2>Clean & Flat Design</h2>
                //					<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                //					<a class='da-link' href='details.html'>meet our works </a>
                //				</div>
                //				<div class='da-slide'>
                //					<h2>recent projects</h2>
                //					<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                //					<a class='da-link' href='details.html'>meet our works </a>
                //				</div>
                //				<div class='da-slide'>
                //					<h2>Clean & Flat Design</h2>
                //					<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                //					<a class='da-link' href='details.html'>meet our works </a>
                //				</div>
                //				<div class='da-slide'>
                //					<h2>recent projects</h2>
                //					<p>Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book.</p>
                //					<a class='da-link' href='details.html'>meet our works </a>
                //				</div>			
                //				<nav class='da-arrows'>
                //					<span class='da-arrows-prev'></span>
                //					<span class='da-arrows-next'></span>
                //				</nav>
                //			</div>
                // 			<!---//End-da-slider----->
                //	 	
                //	</div>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }

        public string generateXContentThumbsByCategoryId(int catId, int num, string type)
        {
            try
            {
                string cssClass = "";

                string res = @"<table class='gridtable' style='width:100%'>";
                model.module.Content c = new Content();
                int counter = 0;
                foreach (model.db.content cc in c.getLastXContentInCategory(catId, num, 0))
                {

                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(cc._cid))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {

                            res += "<tr><td onmouseout='Content.th_mouseout(this)' onmouseover='Content.th_mouseover(this)' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' style='width:100%; vertical-align:top;'>";

                            res += "<img style='width:100%; height:100px;' src='" + uploadPath + cc.thumbnail + "' />";



                            res += "</td></tr>";


                        }
                    }

                }
                res += "</table>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateXContentThumbsByCategoryId", ex);
            }
            return null;
        }


        public string generateXContentThumbsByCategoryId(int catId, int num)
        {
            try
            {
                string res = @"<table style='width:100%'>";
                model.module.Content c = new Content();
                foreach (model.db.content cc in c.getLastXContentInCategory(catId, num, 0))
                {

                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(cc._cid))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {

                            res += "<tr><td onmouseout='Content.th_mouseout(this)' onmouseover='Content.th_mouseover(this)' onclick='Engineer_Sort.generate_search_engine()' style='width:100%; vertical-align:top;'>";

                            res += "<img style='width:100%; height:200px;' src='" + uploadPath + cc.thumbnail + "' />";



                            res += "</td></tr>";

                            res = @"<div class='holder smooth'>
			            <img style='width:100%; height:200px;' src='" + uploadPath + cc.thumbnail + @"' alt='' />
			            
						<div class='go-top'>
						   <p>" + Lang.getByKey(cc.category.name.ToLower() + "_desc") + @"</p>
						<a onclick='javascript:Engineer_Sort.generate_search_engine();return false;' href='#'>" + Lang.getByKey("show_more") + @"</a>
						</div>
			        </div>
			     ";
                        }
                    }

                }

                //res += "</table>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateXContentThumbsByCategoryId", ex);
            }
            return null;
        }

        public string generateXContentsByCategoryId1(int catId, int num, string type, Int32 fromId)
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                int counter = 0;
                List<model.db.content> contents = c.getLastXContentInCategory(catId, num, fromId);



                res = @"<div class='portfolio' id='portfolio'>
                               <div class='wrap'>";
                foreach (model.db.content cc in contents)
                {

                    //                    if (counter == 0 && fromId == 0)
                    //                    {
                    //                        string margin = "";
                    //                        if (Lang.getDirection() == "rtl")
                    //                            margin = "margin-right:10px;";
                    //                        else
                    //                            margin = "margin-left:10px;";
                    //                        res += "<div class='categories' style='" + margin + "'><center><h3>" + Lang.getByKey(cc.category.name) + "</h3></center></div>";
                    //                        counter++;
                    //                    }

                    //                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(cc._cid))
                    //                    {
                    //                        if (LangModule.sessionLang == cd.__rlang)
                    //                        {
                    //                            res += @"<div class='content-list'>
                    //                                <table><tr><td>
                    //                                <div class='content-thumb'>
                    //                                    <a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' ><img src='" + uploadPath + cc.thumbnail + "' style='width:150px; height:100px;' />" + @"</a> 
                    //                                </div>
                    //                                </td><td style='vertical-align:top; padding:10px;'>
                    //                                <div class='content-info'>
                    //                                    <div class='content-title'><a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' >" + cd.title + @"</a></div>
                    //                                    
                    //                                    <div class='content-detail'>" + Convert.ToDateTime(cc.create_date).ToString("yyyy/MM/dd") + @" |<span>" + Visitor.getContentVisitorNumber(Convert.ToInt32(cd.__rcontent)) + " " + Lang.getByKey("views") + @"</span> </div>
                    //                                </div>
                    //                                <div class='clear'></div>
                    //                                </td></tr></table>
                    //                            </div>
                    //                           ";
                    //                        }
                    //                    }
                    //                }
                    //                res += "###<a href='#' onclick='javascript:Category.show_more(" + catId + "," + contents[contents.Count - 1]._cid + "); return false;'>" + Lang.getByKey("show_more") + "</a>";




                    if (counter == 0 && fromId == 0)
                    {
                        res += @"<h3>" + Lang.getByKey(contents[0].category.name) + @"</h3>
                                     ";
                        counter++;
                    }

                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(cc._cid))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {
                            res += @"<div class='col_1_of_3 span_1_of_3'>
                                        <div class='view'>
                                            <img style='width:100%; height:200px;' src='" + uploadPath + cc.thumbnail + @"'>
                                            <div class='mask'>                                           
                                            </div>
                	                    </div>
                                    <h4><a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;'>" + Global.getPartOfString(cd.title, 150) + @"</a></h4>
                                    </div>";
                        }
                    }

                }
                res += @"<div class='clear'></div>								  						
			</div>   		 
   		 </div>";

                res += "###";
                if (contents.Count > 0)
                {
                    if (contents[contents.Count - 1]._cid.ToString() != fromId.ToString() && contents.Count >= 9)
                        res += "<div class='reply'><a href='#' onclick='javascript:Category.show_more(" + catId + "," + contents[contents.Count - 1]._cid + "); return false;'>" + Lang.getByKey("show_more") + "</a></div>";
                }


                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }


        public string generateXContentsByCategoryId2(int catId, int num, Int32 fromId, int? moduleType = null)
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                int counter = 0;
                int items = 1;
                List<model.db.content> contents = c.getLastXContentInCategory(catId, num, fromId);
                foreach (model.db.content cc in contents)
                {
                    if (counter == 0 && fromId == 0)
                    {
                        res = "<div class='sorting-block' style='width:100%;' >";
                        res += "<div class='headline' style='width:100%;'><h2 style='width:100%;'>" + Lang.getByKey(contents[0].category.name)+ "</h2></div>";
                        res += "<ul class='row sorting-grid' id='category_contents' style='width:100%;'> ";
                        counter++;
                    }
                    foreach (model.db.content_detail cd in c.getContentDetailsByContentId(cc._cid))
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {
                            res += "<li class='col-md-3 col-sm-6 col-xs-12 mix category_1 category_3' data-cat='1' >";
                            res += "<a href='Default.aspx?id=" + cd.content._cid + "'>";
                            res += "<div class='easy-block-v1'>";
                            res += "<img class='content_img img' src='" + uploadPath + cc.thumbnail + "' alt='' />";
                            res += "<span class='sorting-cover'>";
                            res += "<span>" + Global.getPartOfString(cd.title, 75) + "</span>";
                            res += "</span>";
                            res += "</div>";
                            res += "</a>";
                            res += "</li>";
                        }
                    }
                    if(items % 4 == 0)
                        res += "<br clear='all' /><br clear='all' />";
                    items++;

                }
                if (fromId == 0)
                {
                    res += "<br clear='all' />";
                    res += "</ul>";
                    res += "</div>";
                }
                res += "<div class='btn_show_more'>";
                if (contents.Count > 0 && fromId == 0)
                {
                    if (contents[contents.Count - 1]._cid.ToString() != fromId.ToString() && contents.Count >= num)
                        res += "<button onclick='javascript:Category.get_more_contents(" + catId + "," + contents[contents.Count - 1]._cid + "); return false;' id='lnk_show_more' class='btn-u btn-u-orange' type='button'>" + Lang.getByKey("show_more") + @"</button>";
                }
                else
                {
                    res += "###";
                    res += contents[contents.Count - 1]._cid;
                }
                res += "</div>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }

        public string generateXContents(int num, Int32 fromId, string str)
        {
            try
            {
                string res = "";
                model.module.Content c = new Content();
                int counter = 0;
                List<model.db.content_detail> contents = c.getContentDetailsByKeyWords(num, fromId, str);
                foreach (model.db.content_detail cd in contents)
                {
                    if (LangModule.sessionLang == cd.__rlang)
                    {
                        res += @"<div class='content-list'>
                                <table><tr><td>
                                <div class='content-thumb'>
                                    <a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' ><img src='" + uploadPath + cd.content.thumbnail + "' style='width:150px; height:100px;' />" + @"</a> 
                                </div>
                                </td><td style='vertical-align:top; padding:10px;'>
                                <div class='content-info'>
                                    <div class='content-title'><a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' >" + cd.title + @"</a></div>
                                    
                                    <div class='content-detail'>" + Convert.ToDateTime(cd.create_date).ToString("yyyy/MM/dd") + @" |<span>" + Visitor.getContentVisitorNumber(Convert.ToInt32(cd.__rcontent)) + " " + Lang.getByKey("views") + @"</span> </div>
                                </div>
                                <div class='clear'></div>
                                </td></tr></table>
                            </div>
                           ";
                    }
                }
                //res += "###<a href='#' onclick='javascript:Category.show_more(" + catId + "," + contents[contents.Count - 1]._cid + "); return false;'>" + Lang.getByKey("show_more") + "</a>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("COntentModule.generateContents", ex);
            }
            return null;
        }

        public string getSomeContentDetailTitle(string title, int numOfCharacters)
        {
            string res = "";
            try
            {
                if (title.Length > 0)
                {
                    if (title.Length > numOfCharacters)
                        res = title.Substring(0, numOfCharacters) + "..";
                    else
                        res = title;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.ContentModule.getSomeContentDetailTitle", ex);
            }
            return res;
        }
        public string getSomeContentDetailText(string text)
        {
            string res = "";
            try
            {
                if (text.Length > 0)
                {
                    if (text.Contains("."))
                        res = text.Substring(0, text.IndexOf("."));
                    else if (text.Length > 100)
                        res = text.Substring(0, text.LastIndexOf(" "));
                    else
                        res = text;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.ContentModule.getSomeContentDetailText", ex);
            }
            return res;
        }

        public static string generateAllContentDetails(string lang, int? selectedValue)
        {
            try
            {
                string res = "<select id='all_contents'";
                Content c = new Content();
                List<model.db.content> contents = c.getAllContents();
                foreach (content cont in contents)
                {
                    model.db.content_detail cd = c.getContentDetail(cont._cid, lang);
                    if (cd != null)
                    {
                        string selected = "";
                        if (selectedValue != null && cont._cid == selectedValue)
                            selected = "selected";

                        if (cd != null)
                            res += "<option " + selected + " value='" + cd.__rcontent + "'" + ">" + cd.title + "</option>";
                    }
                }
                res += "</select>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodule.ContentModule.generateAllContentDetails", ex);
            }
            return null;
        }


        public string generateRelatedLinks(int catId, int cId, int count)
        {
            string res = "";
            res = @"<div class='blog-twitter'>
                        <div class='headline'>
                            <h2>
                                " + Lang.getByKey("related_contents") + @"</h2>
                        </div>";

            model.module.Content c = new Content();
            List<model.db.content> contents = c.getLastXContentInCategory(catId, count, cId);

            foreach (model.db.content content in contents)
            {
                foreach (model.db.content_detail cd in c.getContentDetailsByContentId(content._cid))
                {
                    if (LangModule.sessionLang == cd.__rlang)
                    {
                        res += @"<div class='blog-twitter-inner'>" + Global.getPartOfString(cd.title, 250) + @"   &nbsp;&nbsp;<a href='Default.aspx?id=" + cd.content._cid + @"'>" + Lang.getByKey("read_more") + @"</a> <span class='twitter-time'>
                                " + cd.create_date + @"</span>
                        </div>";
                    }
                }
            }
            res += @"</div>";
            return res;
        }


        public string generateCategoriesSelect()
        {
            Category cat = new Category();
            string res = "<select  id='all_categories'>";
            res += "<option value='0'>------</option>";


            List<model.db.category> c = new model.module.Category().getAllaCategories();
            List<model.db.category> newCats = new List<model.db.category>();
            foreach (model.db.category item in c)
            {
                //if (control.cmsmodules.Security.hasPermission(item.name))
                newCats.Add(item);
            }


            foreach (model.db.category cc in newCats)
            {

                res += "<option value='" + cc._cid + "'>" + Lang.getByKey(cc.name) + "</option>";
            }
            res += "</select>";
            return res;
        }

        public string generateContentDetail(int cId)
        {
            string res = "";
            try
            {
                model.module.Content content = new Content();
                model.db.content_detail contentDtl = content.getContentDetail(Convert.ToInt32(cId), LangModule.sessionLang);
                res = @"<hr style='border-top: 2px solid #333;' />
                            <div class='content-log'>
                            <div class='heading'><h3><span style='font-family: droidKufi; color: #444444'>" + contentDtl.title + @"</span></h3></div>
                            <div class='content-list'>
                                <div class='content-info'>
                             <p>       
                            
                            </p>
                                    <div class='content-desc' style='font-size:14px;font-family: droidKufi'>" + Global.trimHTML(contentDtl.text) + @"</div> <br />
                                    <div class='content-detail'>" + Convert.ToDateTime(contentDtl.create_date).ToString("yyyy/MM/dd") + @"</div>
                                </div>
                                <div class='clear'></div>
                            </div>
                        </div>";

                //res += "<div class='heading'><h3><span style='font-family: Tahoma'>" + contentDtl.title + @"</span></h3></div><br /><br />";
                //res += "<div class='content-desc'>" + contentDtl.text + @"</div>";


                res += generateContentForm(contentDtl.__rcontent);

                res += generateContentFormResults(contentDtl.__rcontent);

                res += generateRelatedContents(contentDtl, 3);

                model.db.visitor visitor = new visitor();
                visitor.__rcontent = cId;
                visitor.address = Visitor.currentIpAddress;
                visitor.create_date = DateTime.Now.Date;
                Visitor.insertVisitor(visitor);


                res = @"<!--=== Breadcrumbs ===-->

 
    <div class='breadcrumbs '>
    	<div class='container  '>
            <h2 class='pull-right row_title'>" + contentDtl.title + @"</h2>
        </div><!--/container-->

    </div><!--/breadcrumbs-->
    <!--=== End Breadcrumbs ===-->
    <!--=== Content Part ===-->
    <div class='container content'>
        <div class='row team margin-bottom-20'>
            <div class='col-md-4'>
                <div class='thumbnail-style'>
                    <img alt='' src='" + uploadPath + contentDtl.content.thumbnail + @"' class='img-responsive'>
                    <h3><a></a> <small>" + contentDtl.create_date + @"</small></h3>
                    
                </div>
<br />

" + generateRelatedLinks(contentDtl.content.category._cid, contentDtl.content._cid, 5) + @"


            </div>

            <div class='col-md-8'>
                " + contentDtl.text + @"
            </div>
        </div>

        
    </div><!--/container-->		
    <!--=== End Content Part ===-->
    </div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.COntentModule.generateContentDetail", ex);
            }
            return res;

        }

        public string generateContentForm(int? cId)
        {
            string res = "";
            try
            {
                model.db.content_form contentForm = Content.getContentFormByContentId(cId);

                if (contentForm.display_first_name != 1 &&
                        contentForm.display_last_name != 1 &&
                        contentForm.display_alias_name != 1 &&
                        contentForm.display_email != 1 &&
                        contentForm.display_address != 1 &&
                        contentForm.display_text != 1)
                    res = "";
                else
                {
                    if (contentForm.login_needed == 1 && !Module.isLogedIn)
                        res = generateLoginForm();
                    else
                    {
                        //<h1>" + Lang.getByKey("content_form") + @"</h1>
                        res += "<hr style='border-top: 0.3em solid #DC483A;' />";
                        res += @"<div id='stylized' class='myform'>
                            <input type='hidden' id='content_id' value='" + cId + @"' />
                            
                            <p>" + Lang.getByKey("add_your_comment_here") + @"</p>
                                   <table id='tbl_content_form'>
                                        <tr>
                                            <td>
                                                " + generateContentFormField("first_name", contentForm.display_first_name.ToString()) + @"
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                " + generateContentFormField("last_name", contentForm.display_last_name.ToString()) + @"
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                " + generateContentFormField("alias_name", contentForm.display_alias_name.ToString()) + @"
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                " + generateContentFormField("email", contentForm.display_email.ToString()) + @"
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                " + generateContentFormField("address", contentForm.display_address.ToString()) + @"
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                " + generateContentFormField("text", contentForm.display_text.ToString()) + @"
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <button onclick='Content.save_content_form_result()'>" + Lang.getByKey("ok") + @"</button>
                                                <div class='spacer'></div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>";
                    }
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.ContentModule.generateContentForm", ex);
            }
            return res;
        }

        public string generateLoginForm()
        {
            string res = "";
            try
            {
                res = @"<div id='stylized' class='myform'>
                            <h1>" + Lang.getByKey("log_in") + @"</h1>";
                res += @"<table id='tbl_log_in'>
                            <tr>
                                <td>
                                    <label>" + Lang.getByKey("login_name") + @"
                                    </label>
                                    <input type='text' id='login_name' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>" + Lang.getByKey("login_password") + @"
                                    </label>
                                    <input type='text' id='login_password' />
                                </td>
                            </tr>
                            <tr>
                               <td>
                                   <button>" + Lang.getByKey("ok") + @"</button><div class='spacer'></div>
                               </td>
                            </tr>
                           </table>";

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmdmodules.contentModuel.generateLoginForm", ex);
            }
            return res;

        }

        public string generateContentFormField(string fldName, string fldValue)
        {
            string res = "";
            try
            {
                if (fldValue == "1")
                {
                    res = @" <label><span class='small'>" + Lang.getByKey("add_your") + " " + Lang.getByKey(fldName) + @"</span>
                           </label>";
                    if (fldName != "text")
                        res += "<input type='text'  id='" + fldName + "' required/>";
                    else
                        res += "<textarea type='text' rows='4' id='" + fldName + "' ></textarea>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmdmodules.ContentModule.generateContentFormField", ex);
            }
            return res;
        }

        public string generateContentFormResults(int? cid)
        {
            string res = "";
            try
            {

                //                List<model.db.content_form_result> contentForms = model.module.Content.getContentFormResultsByContentId(cid);
                //                if (contentForms.Count > 0)
                //                {
                //                    res += "<hr style='border-top: 0.3em solid #DC483A;' />";
                //                    res += "<h1>" + Lang.getByKey("comments") + "</h1>";
                //                }
                //                foreach (model.db.content_form_result cf in contentForms)
                //                {
                //                    res += @"<div class='content-list'>
                //                                <div class='content-info'>
                //                                    <div class='content-title'>" + cf.first_name + " " + cf.last_name + @"</a></div>
                //                                    <div class='content-desc'>" + cf.text + @"</div> <br />
                //                                    <div class='content-detail'>" + cf.create_date + @" </div>
                //                                </div>
                //                                <div class='clear'></div>
                //                                
                //                            </div>
                //                           ";
                //                }


                res = "<hr style='border-top: 0.3em solid #DC483A;' />";
                res += @"<div class='comments'>";
                List<model.db.content_form_result> contentForms = model.module.Content.getContentFormResultsByContentId(cid);

                if (contentForms.Count > 0)
                {
                    res += "<h2>" + contentForms.Count + " " + Lang.getByKey("comments") + "</h2>";
                    res += "<div class='comment-area'>";
                    res += "<ul>";

                }

                //else
                //    res += "<h3>" + Lang.getByKey("no_comments") + "</h3>";

                foreach (model.db.content_form_result cf in contentForms)
                {
                    res += @"<li>
 	          			     <div class='comment-details'>
 	          				    <div class='commnet-user'>
 	          					    <img src='images/icons/user1.jpg' alt='' />
 	          				    </div>
 	          				    <div class='commnet-desc'>
                                    <h3>" + cf.first_name + " " + cf.last_name + @"</h3><span class='time'>" + cf.create_date + @"</span>
                                    <p>" + cf.text + @"</p>
                                </div>
                                <div class='clear'></div>
 	          			     </div>
 	          			 </li>";
                }
                if (contentForms.Count > 0)
                {
                    res += "</ul></div>";
                }

                //res += "<div class='reply'><a href='#'>" + Lang.getByKey("leave_comment") + "</a></div>";

                res += "</div>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmoduls.ContentModule.generateContentFormResults", ex);
            }
            return res;
        }

        public string generateRelatedContents(model.db.content_detail content_detail, int num)
        {
            string res = "";
            try
            {
                res = "<hr style='border-top: 0.3em solid #DC483A;' />";
                res += "<h3>" + Lang.getByKey("related_contents") + "</h3>";
                List<model.db.content> contents = new model.module.Content().getLastXContents(Convert.ToInt32(content_detail.content.__rcategory), num, content_detail.__rcontent);
                res += "<table style='width:100%'>";
                res += "<tr>";

                res = "";

                res += @"<div class='portfolio' id='portfolio'>
                               <div class='wrap'>";
                res += "<h3>" + Lang.getByKey("related_contents") + "</h3>";

                foreach (var item in contents)
                {
                    model.db.content_detail cd = new model.module.Content().getContentDetail(Convert.ToInt32(item._cid), LangModule.sessionLang);

                    if (cd != null)
                    {
                        if (LangModule.sessionLang == cd.__rlang)
                        {

                            //                            res += "<td class='content-log' style='width:15%; font-family:droidKufi;'>";
                            //                            res += @"<div>
                            //                                <div>
                            //                                    <a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' ><img src='" + uploadPath + cd.content.thumbnail + "' style='height:150px;' />" + @"</a> 
                            //                                </div>
                            //                                <div >
                            //                                    <div class='content-title'><a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;' >" + Global.getPartOfString(cd.title, 50) + @"</a></div>
                            //                                    <div style='font-size:12px;'>" + Global.trimAllHTMLTags(Global.getPartOfString(cd.text, 250)) + @"</div>
                            //                                </div>
                            //                            </div>";
                            //                            res += "<br/>";
                            //                            res += "</td>";


                            res += @"<div class='col_1_of_3 span_1_of_3'>
                                        <div class='view'>
                                            <img style='width:100%; height:200px;' src='" + uploadPath + cd.content.thumbnail + @"'>
                                            <div class='mask'>                                           
                                            </div>
                	                    </div>
                                    <h4><a href='#' onclick='javascript:Content.show_content(" + cd.__rcontent + ");return false;'>" + Global.getPartOfString(cd.title, 150) + @"</a></h4>
                                    </div>";
                        }
                    }

                }
                //res += "</tr>";
                //res += "<tr><td>";
                //res += "</td></tr>";
                //res += "<table>";
                res += @"<div class='clear'></div>								  						
			</div>   		 
   		 </div>";

            }
            catch (Exception ex)
            {
                Log.logErr("control.cmdmodules.ContentModule.generateRelatedContents", ex);
            }
            return res;
        }
        #region ContentHandler
        public static string contentHandler(string mode, string catId, string title, string cId, string formData, string imageId)
        {
            ContentModule c = new ContentModule();
            string res = "";

            switch (mode)
            {
                case "1":
                    res = c.generate();
                    break;
                case "2":
                    res = c.generateSearchContent();
                    break;
                case "3":
                    res = c.generateContents(Convert.ToInt32(catId), title);
                    break;
                case "4":
                    //model.module.Content cntModule = new model.module.Content();
                    //model.db.content cntDb = new model.db.content();
                    //cntDb.__rcategory = Convert.ToInt32(catId);
                    //cntModule.insertContent(cntDb);
                    //ContentModule.currentContent = cntDb;
                    break;
                case "5":
                    res = c.generateContentDetail(Convert.ToInt32(cId));
                    break;
                case "6":

                    string[] fields = formData.Split(',');
                    model.db.content_form_result contentForm = new model.db.content_form_result();
                    bool isEmptyField = false;
                    foreach (string fld in fields)
                    {
                        if (fld.Substring(0, fld.IndexOf(":")) == "first_name")
                            contentForm.first_name = fld.Substring(fld.IndexOf(":") + 1);
                        else if (fld.Substring(0, fld.IndexOf(":")) == "last_name")
                            contentForm.last_name = fld.Substring(fld.IndexOf(":") + 1);
                        else if (fld.Substring(0, fld.IndexOf(":")) == "alias_name")
                            contentForm.alias_name = fld.Substring(fld.IndexOf(":") + 1);
                        else if (fld.Substring(0, fld.IndexOf(":")) == "email")
                            contentForm.email = fld.Substring(fld.IndexOf(":") + 1);
                        else if (fld.Substring(0, fld.IndexOf(":")) == "address")
                            contentForm.address = fld.Substring(fld.IndexOf(":") + 1);
                        else if (fld.Substring(0, fld.IndexOf(":")) == "email")
                            contentForm.email = fld.Substring(fld.IndexOf(":") + 1);
                        else if (fld.Substring(0, fld.IndexOf(":")) == "text")
                            contentForm.text = fld.Substring(fld.IndexOf(":") + 1);

                        if (fld.Substring(fld.IndexOf(":") + 1) == "")
                        {
                            isEmptyField = true;
                            break;
                        }

                    }
                    if (!isEmptyField)
                    {
                        contentForm.__rcontent = Convert.ToInt32(cId);
                        contentForm.create_date = DateTime.Now;
                        contentForm.create_by = Global.getUserId();
                        bool Isinserted = model.module.Content.insertContentFormResult(contentForm);
                        if (Isinserted)
                            res = "Done";
                        else
                            res = "Error";
                    }
                    else
                    {
                        res = Lang.getByKey("fill_all_fields");
                    }
                    break;
                case "7":
                    model.db.image image = model.module.Image.getImage(Convert.ToInt32(imageId));

                    if (model.module.Image.deleteImage(image._iid))
                    {
                        String filePath = HttpContext.Current.Server.MapPath("uploads/content_images/" + image.__rcontent + "/" + image.name);
                        System.IO.File.Delete(filePath);
                        res = "1";
                    }
                    else
                        res = Lang.getByKey("error_deleting_image");
                    break;
                case "8":
                    res = Lang.getByKey("error_when_deleting");
                    model.module.Content content = new Content();
                    if (content.deleteContentFormResult(Convert.ToInt32(cId)))
                        if (content.deleteContentForm(Convert.ToInt32(cId)))
                            if (content.deleteContentDetails(Convert.ToInt32(cId)))
                                if (Image.deleteContentImages(Convert.ToInt32(cId)))
                                    if (Visitor.deleteVisitorByContentId(Convert.ToInt32(cId)))
                                        if (content.deleteContent(Convert.ToInt32(cId)))
                                            if (Global.clearFolder(HttpContext.Current.Server.MapPath("uploads/content_images/" + cId), true))
                                            {
                                                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(HttpContext.Current.Server.MapPath(uploadPath));
                                                if (dir == null)
                                                {
                                                    foreach (System.IO.FileInfo fi in dir.GetFiles())
                                                    {
                                                        if (fi.Name.StartsWith(cId))
                                                            fi.Delete();
                                                    }
                                                    if (Global.deleteFile(HttpContext.Current.Server.MapPath(uploadPath + cId)))
                                                        res = Lang.getByKey("deleted");
                                                }
                                            }
                    break;
                default:
                    break;
            }
            return res;
        }
        #endregion

    }
}