using model.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BnpVoucherOfferNewsSlideShowMgmt
/// </summary>
namespace control.cmsmodules.bnp
{
    public class BnpVoucherOfferNewsSlideShowMgmt : Module
    {
        public static int offerCategory = 62;
        public static int slideShowCategory = 67;
        public static int newsCategory = 64;
        public static int voucherCategory = 66;
        public static int posterCategory = 1066;
        
	    public BnpVoucherOfferNewsSlideShowMgmt()
	    {
		    //
		    // TODO: Add constructor logic here
		    //
	    }
        
        public override string generate()
        {
            //generate search cretira, and insert events for categories
            string res = "<div id='mtitle'>Manage Bnp Items[News, SlideShow, Offers, Vouchers]</div>";
            res += "<div id='panel' style='position:fixed;display:none;'><table>"+
                        "<tr><td></td><td id='mainTitle'></td></tr>"+
                        "<tr><td valign='top' id='titleLabel'>Title:</td><td id='titleInp'><input type='text' id='title' /></td></tr>" +
                        "<tr><td valign='top' id='contentLabel'>Content:</td><td id='contentInp'><textarea id='_contnt' rows='5' cols='40'></textArea></td></tr>"+
                        "<tr><td id='imgLabel'>Image:</td><td><input type='file' id='contentImg' /></td></tr>" +
                        "<tr><td colspan='3' id='pmsg'></td></tr>"+
                        "<tr><td colspan='3' width='100%' align='right'><input type='hidden' id='id' /><input type='hidden' id='detId' /><a onclick='bnpmgmt.ok()' href='#'>OK</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a onclick='bnpmgmt.cancel()' href='#'>Cancel</a><input type='hidden' id='gid' /></td></tr>" +
                     "</table></div>";
            res += "<div id='searchCriteria'>";
            res += "<table width='100%'><tr><td>Filter By Type:&nbsp;<select id='search_type'><option value='0'>-----</option>" +
                            "<option value='"+offerCategory+"'>Offer</option>"+
                            "<option value='"+newsCategory+"'>News</option>"+
                            "<option value='"+voucherCategory+"'>Vouchers</option>"+
                            "<option value='"+slideShowCategory+"'>Slide Show</option>" +
                            "<option value='"+posterCategory+"'>Poster</option>" +
                            "</select></td><td width='10%'></td>";
            res += "<td>Filter By Name:&nbsp;&nbsp;<input type='text' id='search_name' />";
            res += "<td>Filter By Partner:&nbsp;&nbsp;<input type='text' id='search_partner' />";
            res += "</tr>";
            res += "<tr>";
            res += "<td colspan='3'><a href='#' class='btn' onclick='bnpmgmt.filter()'>&nbsp;&nbsp;Do Filter&nbsp;&nbsp;</a>" +
                                        "<a href='#' class='btn' onclick='bnpmgmt.newOffer()'>&nbsp;&nbsp;Add New Offer&nbsp;&nbsp;</a>" +
                                        "<a href ='#' class='btn' onclick='bnpmgmt.newVoucher()'>&nbsp;&nbsp;Add New Voucher&nbsp;&nbsp;</a>" +
                                        "<a href='#' class='btn' onclick='bnpmgmt.newNews()'>&nbsp;&nbsp;Add New News&nbsp;&nbsp;</a>" +
                                        "<a href='#' class='btn' onclick='bnpmgmt.newSlideShow()'>&nbsp;&nbsp;Add New SlideShow&nbsp;&nbsp;</a>" +
                                        "<a href='#' class='btn' onclick='bnpmgmt.newPoster()'>&nbsp;&nbsp;Add New Poster&nbsp;&nbsp;</a>" +
                      "</td>";
            res += "</tr>";
            res += "</table>";
            res += "</div>";
            res += "<div id='bnp_content'>";

            res += "</div>";
            return res;
        }
        public static string executeEvent(string mode, HttpContext context)
        {
            if (mode == "filter") { 
                string res = "";
                string js = "";
                try{
                    //get partner list:
                    List<model.db.user> partners = model.module.security.User.getUsers(0);
                    Dictionary<String, model.db.user> parntnersAsDict = new Dictionary<string,model.db.user>();
                    string parnterSelect = "<select id='partner_{1}' value='{2}'><option value='0'>----</option>";
                    foreach (var item in partners){
                        if (item.type == 2) {
                            parnterSelect += "<option {selected_"+item.user_name+"} value=\""+item.user_name+"\">"+(item.first_name+" "+item.last_name +"["+item.company+"]")+"</option>";
                            parntnersAsDict.Add(item.user_name, item);
                        }
                    }
                    parnterSelect += "</select>";
                    model.module.Content _content = new model.module.Content();
                    int type =  int.Parse(context.Request["type"]);
                    string name = context.Request["name"];
                    string partner = context.Request["partner"];
                    List<model.db.content> contents = null;
                    model.module.Content ctnt = new model.module.Content();
                    model.module.JobTypes jt = new model.module.JobTypes();
                    if (type != 0)
                        contents = model.module.Content.getFirstXContentInCategory(type, 0);
                    else
                        contents = model.module.Content.getAllConetents();
                    foreach (var item in contents)
                    {
                        model.db.content_detail selectedDetail = null;
                        List<model.db.content_extra> contentExtra = model.module.ContentExtra.getAll(item._cid);
                        List<model.db.content_detail> details = _content.getContentDetailsByContentId(item._cid);
                        bool choosen = true;
                        foreach (var detail in details)
                        {
                            if (detail.__rlang == Global.getLangFromSession())
                            {
                                selectedDetail = detail;
                                if (name != null && name != "" && detail.title != null && !detail.title.ToLower().Contains(name.ToLower())) {
                                    choosen = false;
                                }
                                if (choosen && partner != null && partner != "") {
                                    List<model.db.user> usersAsPartners = model.module.security.User.getUserByNameLike(partner);
                                    bool exist = false;
                                    foreach (var extra in contentExtra)
                                    {
                                        if (extra._key == "partner") {
                                            foreach (var _partner in usersAsPartners)
                                            {
                                                if (("" + _partner._uid).Equals(extra._val))
                                                {
                                                    exist = true;
                                                    break;
                                                }
                                            }
                                            if (exist)
                                                break;
                                        }
                                    }
                                   if (!exist)
                                        choosen = false;
                                }
                                break;
                            }
                        }
                        if (choosen)
                        {
                            res += "<div id='content_" + item._cid + "'>";
                            res += "<table>";
                            res += "<tr>";
                            if (item.__rcategory == offerCategory)
                            {
                                res += "<td width='10%'>" +
                                    "<table>" +
                                      "<tr><td ><a onclick='bnpmgmt.editOffer(\"" + item._cid + "\")' class='grid_btn'>Edit Offer</a></td></tr>" +
                                      "<tr><td ><a onclick='bnpmgmt.deleteBnpItem(\"" + item._cid + "\")' class='grid_btn'>Delete Offer</a></td></tr>" +
                                    "</table>" +
                                "</td>";
                                res += "<td width='80%'>" +
                                        "<table>" +
                                        "<tr><td><b>Offer</b><td>&nbsp;&nbsp;Image:<img width='60px' height='60px' src='/view/uploads/content_thumbnails/" + item.thumbnail + "' /></td><td>&nbsp;&nbsp;Title:" + (selectedDetail == null ? "No Title" : selectedDetail.title) + "</td></td></tr>" ;
                                try {
                                    string[] p = (selectedDetail.key_word !=null?selectedDetail.key_word.Split(new char[] { '#', '#' }) : (new string[]{})  );
                                    string s = new string(parnterSelect.ToCharArray());
                                    s = s.Replace("{1}", "" + selectedDetail._cdid);
                                    if (p.Length > 0)
                                    {
                                        s = s.Replace("{2}", "" + parntnersAsDict[p[2]]._uid);
                                        s = s.Replace("{selected_" + p[2] + "}", " selected='selected' ");
                                    }
                                    res += "<tr><td colspan='2'></td><td>Partner:" + s + "</td></tr>";
                                    res += "<tr><td colspan='2'></td><td>Mobile:&nbsp;<input id='mobile_"+item._cid+"' value='"+ctnt.getContentExtraValueByKey(item._cid, "mobile")+"' /></td>"+
                                        "<td>&nbsp;Phone:&nbsp;<input id='phone_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "phone") + "' /></td>"+
                                        "<td>&nbsp;Email:&nbsp;<input id='email_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "email") + "'  size='50' /></td></tr>";
                                    res += "<tr><td colspan='2'></td><td>Region:&nbsp;<input id='region_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "region") + "' /></td>";
                                        string __s = ctnt.getContentExtraValueByKey(item._cid, "category");
                                        __s = (__s == "") ? "0" : __s;
                                    res +=   "<td>&nbsp;Category:&nbsp;<select id='category_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "category") + "' />" + jt.toOptions(int.Parse(__s)) + "<select></td>" +
                                        "<td>&nbsp;Address:&nbsp;<input id='address_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "address") + "' size='50' /></td></tr>";
                                    res += "<tr><td colspan='2'></td><td colspan='2'>Opening hours From Mon. To Fri.:&nbsp;<br/>Open:&nbsp;<input id='open_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "open") + "'/></td>"+
                                           "<td>&nbsp;<br/>close:&nbsp;<input id='close_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "close") + "' /></td></tr>";
                                    res += "<tr><td colspan='2'></td><td colspan='2'>Opening hours Sat<br/>Open:&nbsp;<input id='sat_open_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "sat_open") + "' /></td>"+
                                        "<td>&nbsp;<br/>close:&nbsp;<input id='sat_close_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "sat_close") + "'/></td></tr>";
                                    res += "<tr><td colspan='2'></td><td colspan='2'>Opening hours Sun<br/>Open:&nbsp;<input id='sun_open_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "sun_open") + "' /></td>"+
                                        "<td>&nbsp;<br/>close:&nbsp;<input id='sun_close_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "sun_close") + "'/></td></tr>";
                                    res += "<tr><td colspan='2'></td><td colspan='2'>Folowing<br/>Facebook:&nbsp;<input id='facebook_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "facebook") + "' /></td>"+
                                        "<td>&nbsp;<br/>Tweeter:&nbsp;<input id='tweeter_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "tweeter") + "'/></td></tr>";
                                    res += "<tr><td colspan='2'></td><td colspan='2'>Youtube:&nbsp;<input id='youtube_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "youtube") + "' /></td>" +
                                        "<td>&nbsp;Inistagram:&nbsp;<input id='instagram_" + item._cid + "' value='" + ctnt.getContentExtraValueByKey(item._cid, "instagram") + "'/></td></tr>";                                    
                                    res += "<tr><td colspan='2'></td><td>Percentage:<input type='text' id='percentage_" + selectedDetail._cdid + "' value='" + (p.Length > 0 ? p[0] : "") + "' /></td><td valign='middle'><a onclick=\"bnpmgmt.setOfferPercentage('" + selectedDetail._cdid + "','"+item._cid+"')\">change</a></td></tr>";
                                }
                                catch{}
                                res +=        "</table>" +
                                    "</td>";
                            }
                            else if (item.__rcategory == newsCategory)
                            {
                                res += "<td width='10%'>" +
                                    "<table>" +
                                      "<tr><td ><a onclick='bnpmgmt.editNews(\"" + item._cid + "\")' class='grid_btn'>Edit News</a></td></tr>" +
                                      "<tr><td ><a onclick='bnpmgmt.deleteBnpItem(\"" + item._cid + "\")' class='grid_btn'>Delete News</a></td></tr>" +                                      
                                    "</table>" +
                                "</td>";
                                res += "<td width='80%'>" +
                                        "<table>" +
                                        "<tr><td><b>News</b></td><td>&nbsp;&nbsp;Image<img width='60px' height='60px' src='/view/uploads/content_thumbnails/" + item.thumbnail + "' /></td><td>&nbsp;&nbsp;Title:" + (selectedDetail == null ? "No Title" : selectedDetail.title) + "</td><td></td><td></td></tr>";
                                try
                                {
                                    string[] p = (selectedDetail.key_word !=null?selectedDetail.key_word.Split(new char[] { '#', '#' }) :new string[]{});
                                    res += "<tr><td colspan='2'></td><td>Place:<input type='text' id='place_" + selectedDetail._cdid + "' value='" + (p.Length>0?p[0]:"") + "' /></td><td valign='middle'></td>" +
                                        "<td colspan='2'></td><td>From:<input type='text' id='from_" + selectedDetail._cdid + "' value='" + (p.Length > 0 ? p[2] : "") + "' /></td><td valign='middle'></td>" +
                                        "<td colspan='2'></td><td>To:<input type='text' id='to_" + selectedDetail._cdid + "' value='" + (p.Length > 0 ? p[4] : "") + "' /></td><td valign='middle'><a onclick='bnpmgmt.setPlaceAndTime(\"" + selectedDetail._cdid + "\")'>change</a></td></tr>";

                                }
                                catch { }

                                 res +=       "</table>" +
                                    "</td>";
                            }
                            else if (item.__rcategory == voucherCategory)
                            {
                                res += "<td width='10%'>" +
                                    "<table>" +
                                      "<tr><td ><a onclick='bnpmgmt.editVoucher(\"" + item._cid + "\")' class='grid_btn'>Edit Voucher</a></td></tr>" +
                                      "<tr><td><a onclick='bnpmgmt.deleteBnpItem(\"" + item._cid + "\")' class='grid_btn'>Delete Voucher</a></td></tr>" +
                                     "</table>" +
                                "</td>";    
                                res += "<td width='80%'>" +
                                        "<table>" +
                                        "<tr><td><b>Voucher</b></td><td>&nbsp;&nbsp;Image:<img width='60px' height='60px' src='/view/uploads/content_thumbnails/" + item.thumbnail + "' /></td><td>Title:" + (selectedDetail == null ? "No Title" : selectedDetail.title) + "</td><td></td></tr>";
                                try
                                {
                                     string[] p = (selectedDetail.key_word !=null?selectedDetail.key_word.Split(new char[] { '#', '#' }) :new string[]{});
                                    string s = new string(parnterSelect.ToCharArray());
                                    s = s.Replace("{1}", ""+selectedDetail._cdid);
                                    if (p.Length > 0)
                                    {
                                        s = s.Replace("{2}", "" + parntnersAsDict[p[0]]._uid);
                                        s = s.Replace("{selected_"+p[0]+"}", " selected='selected' ");
                                    }
                                    //default_key_words
                                    string xx = (p.Length > 3 ? p[6] : "");
                                    if (xx == "default_key_words")
                                        xx = "";
                                    res += "<tr><td colspan='2'></td><td>Partner:"+s+"</td><td valign='middle'></td>" +
                                            "<td>Default Number:<input type='text' id='default_" + selectedDetail._cdid + "' value='" + (p.Length > 2 ? p[2] : "") + "' /></td><td valign='middle'></td>" +
                                            "<td>Provided Offer:<input type='text' id='provided_offer_" + selectedDetail._cdid + "' value='" + xx+ "' /></td>" +
                                            "<td><a onclick='bnpmgmt.setOwnerAndNumber(\""+selectedDetail._cdid+"\")'>Change</a></td>";
                                }
                                catch { }
                                 res +=     "</table>" +
                                    "</td>";
                            }
                            else if (item.__rcategory == slideShowCategory)
                            {
                                res += "<td width='10%'>" +
                                    "<table>" +
                                      "<tr><td ><a onclick='bnpmgmt.editSlideShow(\"" + item._cid + "\")' class='grid_btn'>Edit slide show</a></td></tr>" +
                                      "<tr><td ><a onclick='bnpmgmt.deleteBnpItem(\"" + item._cid + "\")' class='grid_btn'>Delete slide show</a></td></tr>" +
                                      "<tr><td ><a onclick='bnpmgmt.toggleSlideShowDisplay(\"" + item._cid + "\")' class='grid_btn'>Toggle Display</a></td></tr>" +
                                    "</table>" +
                                "</td>";
                                List<model.db.content_extra> ces = ctnt.getContentExtraByContentId(item._cid);
                                res += "<td width='80%'>" +
                                        "<table>" +
                                        "<tr><td><b>Slide Show</b></td><td>&nbsp;&nbsp;Image:<img width='60px' height='60px' src='/view/uploads/content_thumbnails/" + item.thumbnail + "' /></td><td>&nbsp;&nbsp;Title:" + (selectedDetail == null ? "No Title" : selectedDetail.title) + "</td></tr>";
                                try
                                {
                                    model.db.content_extra ce = ces[0];
                                    if(ce._key == "display")
                                        res += "<tr><td>"+(ce._val=="active"?"<b>Active</b>":"Inactive")+"</td></tr>";
                                    else
                                        res += "<tr><td><b>Active</b></d></tr>";
                                }
                                catch (Exception ex){
                                    res += "<tr><td><b>Active</b></d></tr>";
                                }
                                res += "</table></td>";
                            }
                            else if (item.__rcategory == posterCategory)
                            {
                                res += "<td>";
                                res += "<table>";
                                res += "<tr><td ><a onclick='bnpmgmt.editPoster(\"" + item._cid + "\")' class='grid_btn'>Edit poster</a></td></tr>";
                                res += "<tr><td ><a onclick='bnpmgmt.deleteBnpItem(\"" + item._cid + "\")' class='grid_btn'>Delete poster</a></td></tr>";
                                res += "</table>";
                                res += "</td>";
                                res += "<td width='80%'>";
                                res += "<table>";
                                string panner = "";
                                string status = "";
                                string fromDate = "";
                                string toDate = "";
                                string url = "";
                                List<model.db.content_extra> ces = ctnt.getContentExtraByContentId(item._cid);
                                foreach (var ce in ces)
                                {
                                    if (ce._key == "panner")
                                        panner = ce._val;
                                    else if (ce._key == "status")
                                        status = ce._val;
                                    else if (ce._key == "fromdate")
                                        fromDate = ce._val;
                                    else if (ce._key == "todate")
                                        toDate = ce._val;
                                    else if(ce._key == "url")
                                        url = ce._val;
                                }
                                res += "<tr><td><b>Poster</b></td><td>&nbsp;&nbsp;Image:<img width='60px' height='60px' src='/view/uploads/content_thumbnails/" + item.thumbnail + "' /></td><td>&nbsp;&nbsp;Title:" + (selectedDetail == null ? "No Title" : selectedDetail.title) + "</td></tr>";
                                res += "<tr><td>&nbsp;&nbsp;Panner:&nbsp;&nbsp;<select id='panner_" + item._cid + "' value='" + panner + "'><option " + (panner == "upper" ? "selected='selected'" : "") + " value='upper'>Upper Panner</option><option value='middle' " + (panner == "middle" ? "selected='selected'" : "") + ">Middle Panner</option><option value='bottom' " + (panner == "bottom" ? "selected='selected'" : "") + ">Bottom Panner</option></select></td>" +
                                    "<td>&nbsp;&nbsp;Status:<select id='poststatus_" + item._cid + "' value='"+status+"'><option value='active' "+(status=="active"?"selected='selected'":"")+">Active</option><option value='inactive' "+(status=="inactive"?"selected='selected'":"")+">InActive</option></select></td>" +
                                               "<td>From Date:<input type='text' id='from_date_" + item._cid + "' value='" + fromDate + "' /></td><td>To Date:<input type='text' id='to_date_" + item._cid + "'  value='" + toDate + "'  /></td>" +
                                               "<td><a onclick=bnpmgmt.changePosterInfo('"+item._cid+"')>Change</a></td></tr>"+
                                               "<tr><td colspan='4'>URL:<input type='text' id='url_"+item._cid+"' value='"+url+"' /></td></tr>";
                                res += "</table>";
                                res += "</td>";
                                js += item._cid + ",";
                            }
                            res += "</tr>";
                            res += "</table>";
                            res += "</div>";
                        }
                    }
                    res += "<input type='hidden' id='idsToDated' value='"+js+"'>";
                    return res;
                }
                catch(Exception ex){
                    int i = 0;
                }
            }
            else if (mode == "new")
            {
                string cat = context.Request["cat"].ToLower();
                int catAsInt = offerCategory;
                if (cat == "offer")
                    catAsInt = offerCategory;
                else if (cat == "voucher")
                    catAsInt = voucherCategory;
                else if (cat == "news")
                    catAsInt = newsCategory;
                else if (cat == "slideshow")
                    catAsInt = slideShowCategory;
                else if (cat == "poster")
                    catAsInt = posterCategory;
                model.db.content c = new model.db.content();
                string imgName = "";
                bool insertImg = false;
                HttpPostedFile file = null;
                string str11 = "";
                if (context.Request.Files != null && context.Request.Files[0] != null)
                {
                    insertImg = true;
                    imgName = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;
                    file = context.Request.Files[0];
                    str11 = file.FileName;
                    string[] str1 = str11.Split(new char[] { '.' });
                    str11 = str1[str1.Length-1];
                    c.thumbnail = imgName + "." + str11;
                }
                c.__rcategory = catAsInt;
                model.module.Content ctnt = new model.module.Content();
                int id = ctnt.insertContent(c);
                if (id > 0)
                {
                    if (insertImg)
                    {
                        String rr = HttpContext.Current.Server.MapPath("~");
                        file.SaveAs(rr + "/view/uploads/content_thumbnails/" + imgName + "." + str11);
                    }
                    //insert details
                    model.db.content_detail det = new model.db.content_detail();
                    det.__rcontent = Convert.ToInt32(id);
                    det.__rlang = Global.getLangFromSession();
                    //det.__rgallery = catAsInt;
                    det.publish_date = DateTime.Now;
                    det.create_date = DateTime.Now;
                    //det.create_by = ((model.db.user)System.Web.HttpContext.Current.Session["user"])._uid;
                    try
                    {
                        string un = StringCipher.Decrypt(context.Request["sk"]);
                        model.db.user uu = model.module.security.User.getUserByName(un);
                        det.create_by = uu._uid;
                    }
                    catch (Exception ex)
                    {
                        ;
                    }
                    det.text = StringCipher.Encrypt(context.Request["content"]);
                    det.title = StringCipher.Encrypt(context.Request["Title"]);
                    bool res = ctnt.insertContentDetail(det);
                }
            }
            else if (mode == "update")
            {
                string idAsString = context.Request["id"];
                string detIdAsString = context.Request["detId"];
                model.module.Content ctnt = new model.module.Content();
                model.db.content c = ctnt.getContentById(int.Parse(idAsString));
                model.db.content_detail cd = ctnt.getContentDetailById(int.Parse(detIdAsString));

                string imgName = "";
                HttpPostedFile file = null;
                string str11 = "";
                if (context.Request.Files != null && context.Request.Files.Count > 0)
                {
                    if (context.Request.Files[0] != null)
                    {
                        imgName = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;
                        file = context.Request.Files[0];
                        str11 = file.FileName;
                        string[] str1 = str11.Split(new char[] { '.' });
                        str11 = str1[str1.Length - 1];
                        c.thumbnail = imgName + "." + str11;
                        //Delete old image
                        if(ctnt.updateContent(c))
                        {
                            String rr = HttpContext.Current.Server.MapPath("~");
                            file.SaveAs(rr + "/view/uploads/content_thumbnails/" + imgName + "." + str11);
                        }
                    }
                }
                cd.text = StringCipher.Encrypt(context.Request["content"]);
                cd.title = StringCipher.Encrypt(context.Request["Title"]);
                ctnt.updateContentDetial(cd);
            }
            else if (mode == "setOfferPercent") { 
                string dIdAsStr = context.Request["did"];
                int did = int.Parse(dIdAsStr);
                string user = context.Request["partner"];
                model.module.Content ctnt = new model.module.Content();
                model.db.content_detail cd = ctnt.getContentDetailById(did);
                string percent = context.Request["percent"];
                percent = percent.Trim();
                if (!percent.EndsWith("%"))
                    percent += percent + "%";
                cd.key_word = percent + "##"+user+"##default_key_words";
                ctnt.updateContentDetial(cd);
                int contentId = (int)cd.__rcontent;
                ctnt.removeContentExtraByContentId(contentId);
                ctnt.insertContentExtra("mobile", context.Request["mobile"], contentId);
                ctnt.insertContentExtra("phone", context.Request["phone"], contentId);
                ctnt.insertContentExtra("region", context.Request["region"], contentId);
                ctnt.insertContentExtra("category", context.Request["category"], contentId);
                ctnt.insertContentExtra("email", context.Request["email"], contentId);
                ctnt.insertContentExtra("address", context.Request["address"], contentId);
                ctnt.insertContentExtra("open", context.Request["open"], contentId);
                ctnt.insertContentExtra("close", context.Request["close"], contentId);
                ctnt.insertContentExtra("sat_open", context.Request["sat_open"], contentId);
                ctnt.insertContentExtra("sat_close", context.Request["sat_close"], contentId);
                ctnt.insertContentExtra("sun_open", context.Request["sun_open"], contentId);
                ctnt.insertContentExtra("sun_close", context.Request["sun_close"], contentId);
                ctnt.insertContentExtra("facebook", context.Request["facebook"], contentId);
                ctnt.insertContentExtra("tweeter", context.Request["tweeter"], contentId);
                ctnt.insertContentExtra("youtube", context.Request["youtube"], contentId);
                ctnt.insertContentExtra("instagram", context.Request["instagram"], contentId);
            }
            else if (mode == "setPlaceAndTime")
            {
                string dIdAsStr = context.Request["did"];
                int did = int.Parse(dIdAsStr);
                model.module.Content ctnt = new model.module.Content();
                model.db.content_detail cd = ctnt.getContentDetailById(did);
                string place = context.Request["place"];
                string from = context.Request["from"];
                string to = context.Request["to"];
                cd.key_word = place+"##"+from+"##"+to+"##default_key_words";
                ctnt.updateContentDetial(cd);
            }
            else if (mode == "setOwnerAndNumber"){
                string didAsStr = context.Request["did"];
                int did = int.Parse(didAsStr);
                model.module.Content ctnt = new model.module.Content();
                model.db.content_detail cd = ctnt.getContentDetailById(did);
                string user = context.Request["partner"];
                string defNum = context.Request["defaultNumber"];
                string offerProvided = context.Request["op"];
                model.db.user uu = model.module.security.User.getUserByName(user);
                cd.key_word = user + "##" + defNum + "##" + uu.company + "##" + offerProvided + "##default_key_words";
                ctnt.updateContentDetial(cd);
            }
            else if (mode == "displyVoucherInfo")
            {
                string ctntIdAsStr = context.Request["id"];
                string ctntDetAsStr = context.Request["did"];
                int ctntId = int.Parse(ctntIdAsStr);
                int detId = int.Parse(ctntDetAsStr);
                model.module.Content c = new model.module.Content();
                model.db.content ctnt = c.getContentById(ctntId);
                model.db.content_detail det = c.getContentDetailById(detId);
                return "<div>" +
                               "<table><tr><td><img width='100px'  height='100px' src='/view/uploads/content_thumbnails/" + ctnt.thumbnail + "' /></td><td><b>&nbsp;&nbsp;&nbsp;&nbsp;Search User By Searial Number:&nbsp;&nbsp;</b></td><td><input type='text' id='searchSerialNumber'/></td><td><a class='btn' onclick='bnpmgmt.searchSerialNumber()'>&nbsp;&nbsp;seach</a></td></tr>" +
                                         "<tr><td colspan='4'><p>"+det.text+"</p></td></tr>"+
                               "</table>"+                            
                        "</div>";
            }
            else if (mode == "searchSerialNumber")
            {
                string ctntIdAsStr = context.Request["id"];
                string ctntDetAsStr = context.Request["did"];
                int ctntId = int.Parse(ctntIdAsStr);
                int detId = int.Parse(ctntDetAsStr);
                string searialNumber = context.Request["sn"];
                CMSDataContext db = new CMSDataContext();
                model.db.user u = db.users.Single(uu => uu.serial_number == searialNumber);
                if (u != null)
                {
                    string numberOfItems = "";
                    int vchrId = -1;
                    //check if user get benifit from this voucher previouslly
                    try { 
                        model.db.bnp_vchr_client voucher = db.bnp_vchr_clients.Single(bnp => bnp.vchr_id == detId && bnp.client_id == u._uid);
                        vchrId = voucher.id;
                        numberOfItems = ""+voucher.rest;
                    }
                    catch(Exception eee){
                        model.db.content_detail det = db.content_details.Single(dd=>dd._cdid == detId);
                        string[] p = det.key_word.Split(new char[] {'#', '#'});
                        numberOfItems = p[2];
                    }
                    if (u.is_active == 1)
                    {
                        return "<div></div><div style='font-size:26px;'>" +
                                    "<img width='200px' src='" + u.img + "'>" +
                                        "<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + u.first_name + " " + u.last_name + "&nbsp;&nbsp;</b>" +
                                 "</div>" +
                                 "<div>" +
                                    "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Number Of Vouchers:<input id='numberOfVouchers' value='" + numberOfItems + "' /><a class='btn' onclick='bnpmgmt.changeVoucherNumbers(\"" + vchrId + "\")'>Change</a>" +
                                 "</div>";
                    }
                    else
                    {
                        return "<div></div><div style='font-size:26px;'>" +
                                    "<img width='200px' src='" + u.img + "'>" +
                                        "<b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + u.first_name + " " + u.last_name + "&nbsp;&nbsp;</b>" +
                                 "</div>" +
                                 "<div>" +
                                    "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; User Account is Not Active it may be expired"+
                                 "</div>";
                    }
                }
                else
                    return "User You are searching for Not Exist in DataBase";
            }
            else if (mode == "changeVoucherNumbers")
            {
                string vchrAsString = context.Request["vid"];
                string detIdAsString = context.Request["did"];
                string numberOfVchrAsString = context.Request["nov"];
                int vchrId = int.Parse(vchrAsString);
                int detId = int.Parse(detIdAsString);
                int numOfVchrs = int.Parse(numberOfVchrAsString);
                CMSDataContext db = new CMSDataContext();
                string searialNumber = context.Request["sn"];
                try
                {
                    model.db.user u = db.users.Single(uu => uu.serial_number == searialNumber);
                    if (vchrId == -1)
                    {
                        //insert
                        model.db.bnp_vchr_client bvc = new bnp_vchr_client();
                        bvc.client_id = u._uid;
                        bvc.vchr_id = detId;
                        bvc.rest = numOfVchrs;
                        db.bnp_vchr_clients.InsertOnSubmit(bvc);
                        db.SubmitChanges();
                    }
                    else
                    {
                        model.db.bnp_vchr_client bvc = db.bnp_vchr_clients.Single(xx => xx.id == vchrId);
                        bvc.client_id = u._uid;
                        bvc.vchr_id = detId;
                        bvc.rest = numOfVchrs;
                        db.SubmitChanges();
                    }
                    return "";
                }
                catch(Exception exx){
                    //return error message
                }
            }
            else if (mode == "changePosterInfo")
            {
                model.module.Content ctnt = new model.module.Content();
                string idAsStr = context.Request["id"];
                string panner = context.Request["panner"];
                string status = context.Request["status"];
                string fromDate = context.Request["fromDate"];
                string toDate =context.Request["toDate"];
                string url = context.Request["url"];
                int id = int.Parse(idAsStr);
                //add extra items to poster
                ctnt.removeContentExtraByContentId(id);
                model.db.content_extra ce = new content_extra();
                ce.content_id = id;
                ce._key = "panner";
                ce._val = panner;
                ce.status = 0;
                int insertId = ctnt.insertContentExtra(ce);

                ce = new content_extra();
                ce.content_id = id;
                ce._key = "status";
                ce._val = status;
                ce.status = 1;
                insertId = ctnt.insertContentExtra(ce);

                ce = new content_extra();
                ce.content_id = id;
                ce._key = "fromdate";
                ce._val = fromDate;
                ce.status = 1;
                insertId = ctnt.insertContentExtra(ce);

                ce = new content_extra();
                ce.content_id = id;
                ce._key = "todate";
                ce._val = toDate;
                ce.status = 1;
                insertId = ctnt.insertContentExtra(ce);

                ce = new content_extra();
                ce.content_id = id;
                ce._key = "url";
                ce._val = url;
                ce.status = 1;
                insertId = ctnt.insertContentExtra(ce);
            }
            else if (mode == "toggleSlideShowDisplay")
            {
                string idAsStr = context.Request["id"];
                model.module.Content ctnt = new model.module.Content();
                List<model.db.content_extra> ces = ctnt.getContentExtraByContentId(int.Parse(idAsStr));
                if(ces.Count() > 0){
                    foreach (var ce in ces)
                    {
                        if (ce._key == "display")
                        {
                            ce._val = (ce._val == "active") ? "inactive" : "active";
                            model.db.CMSDataContext db = new CMSDataContext();
                            model.db.content_extra ce1 = db.content_extras.Single(cc => cc.ce_id == ce.ce_id);
                            ce1._val = ce._val;
                            db.SubmitChanges();
                        }
                    }
                }
                else{
                    model.db.content_extra ce = new content_extra();
                    ce._key = "display";
                    ce._val = "active";
                    ce.content_id = int.Parse(idAsStr);
                    ctnt.insertContentExtra(ce);
                }
            }
            else if (mode == "deleteBnpItem") {
                string idAsStr = context.Request["id"];
                model.module.Content ctnt = new model.module.Content();
                ctnt.deleteContent(int.Parse(idAsStr));
            }
            else if (mode == "getDataForEdit") {
                string idAsStr = context.Request["cid"];
                model.module.Content ctnt = new model.module.Content();
                content c = ctnt.getContentById(int.Parse(idAsStr));
                if (c != null)
                {
                    List<content_detail> dets = ctnt.getContentDetailsByContentId(int.Parse(idAsStr));
                    string lang = Global.getLangFromSession();
                    string title = "";
                    string content = "";
                    string detId = "";
                    bool found = false;
                    for (int i = 0; i < dets.Count() && !found; i++)
                    {
                        if (dets[i].__rlang == lang) {
                            detId = ""+dets[i]._cdid;
                            found = true;
                            title = dets[i].title;
                            content = dets[i].text;
                        }
                    }
                    return c._cid + "@@@@" + detId + "@@@@" + title + "@@@@" + content;
                }
            }
            return "";
        }
    }
}