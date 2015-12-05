using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ClientManagmentAdmin
/// </summary>
namespace control.cmsmodules
{
    public class ClientManagmentAdmin : Module
    {
        public static int clientRole = 17;
        public static int companyRole = 16;
        public ClientManagmentAdmin()
        {
            
        }
        public override string generate()
        {
            try
            {
                string res = "<div id='upanel' style='display:none;z-index:2000;background-color:#ccc;'>";
                res += "<div style='font-weight:bold;'>Add/Edit User Info</div>";
                res += "<div style='height:300px; overflow:auto;'>";
                res +=  "<table>";
                res += "<tr><td align='middle'>First Name:</td><td  align='middle' >Last Name</td><td  align='middle' >Birth Data</td></tr>";
                res += "<tr><td><input type='text' id='first_name' /></td><td><input type='text' id='last_name' /></td><td><input type='text' id='birthdate' /></td></tr>";

                res += "<tr><td  align='middle'>Start Date</td><td  align='middle'>Expire Date</td><td></td></tr>";
                res += "'<tr><td><input type='text' id='start_date' /></td><td><input type='text' id='end_date' /></td><td></td></tr>";

                res += "'<tr><td  align='middle'>Type</td><td  align='middle'>Company</td><td  align='middle'>Serial Number</td></tr>";
                res += "<tr><td><select id='type'><option value='2'>Partner</option><option value='3'>Client</option></select></td><td><input type='text' id='company' /></td><td><input type='text' id='serial_number'/></td></tr>";

                res += "<tr><td colspan='2'  align='middle'>Address</td><td  align='middle'>Gender</td></tr>";
                res += "<tr><td colspan='2'><input type='text' id='address'/></td><td>Male<input type='radio' id='uMale' name='gender' value='1' />&nbsp;&nbsp;Fmale<input type='radio' id='uFMale' name='gender' value='0' /></td></tr>";

                res += "<tr><td align='middle'>Location</td><td align='middle'>Work Category</td><td align='middle'>Social Number</td></tr>";
                res += "<tr><td><select id='location'>";
                var locations = model.module.Location.list();
                for (int m = 0; m < locations.Count; m++)
                {
                    res += "<option value='"+locations[m]._lid+"'>"+locations[m].name+"</option>";
                }
                 res += "</select></td><td><select id='work_category'>";
                //query to get them
                var categories = model.module.JobTypes.list();
                for (int k = 0; k < categories.Count(); k++)
                {
                    res += "'<option value='" + categories[k]._jid + "' >" + categories[k].name + "</option>";
                }
                res += "</select></td><td><input type='text' id='social_number' /></td></tr>";
                
                res += "<tr><td align='middle'>Phone Number</td><td>Mobile Number</td><td></td></tr>";
                res += "<tr><td><input type='text' id='phone' /></td><td><input type='text' id='mobile'/></td><td></td></tr>";

                res += "<tr><td align='middle'>Website</td><td>Email</td><td></td></tr>";
                res += "<tr><td><input type='text' id='website' /></td><td><input type='text' id='email' /></td><td></td></tr>";

                res += "<tr><td align='middle' id='userLabel'>User Name</td><td id='pwdLabel'>Password</td><td id='pwdConfirmLabel'>ConfirmPassword</td></tr>";
                res += "<tr><td><input type='text' id='user_name'/></td><td><input type='password' id='pwd' /></td><td><input type='password' id='confirm' /></td></tr>";

                res += "<tr><td align='middle'>Social Network1</td><td align='middle'>Social Network2</td><td align='middle'>Social Network3</td></tr>";
                res += "<tr><td><input type='text' id='social_network1'/></td><td><input type='text' id='social_network2'/></td><td><input type='text' id='social_network3'/></td></tr>";
                res += "<tr><td colspan='1' align='middle'>Upload Image</td><td><input type='file' id='img' /></td></tr>";
                res += "</table>";
                res += "'</div>";
                res += "<table width='100%'>";
                res += "<tr><td colspan='3' id='msg'></td></tr>";
                res += "<tr><td colspan='3' width='100%' align='right'><input type='hidden' id='id' /><a onclick='users.ok()' href='#'>OK</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a onclick='users.cancel()' href='#'>Cancel</a><input type='hidden' id='gid' /></td></tr>";
                res +=  "</table>";
                res += "</div>";

                res += "<div style='position:relative;float:down; top:10px;!important;'><div>";
                res += "<div class='users_main'>";
                res += "<div onclick=\"users.toggleCheckAll()\" style='position:relative;float:left'><a class='btn'>&nbsp;&nbsp;Check/uncheck All&nbsp;&nbsp;</a>&nbsp;&nbsp;</div>";
                res += "<div onclick=\"users.activeChecked()\" style='position:relative;float:left'><a class='btn'>&nbsp;&nbsp;Active Checked&nbsp;&nbsp;</a>&nbsp;&nbsp;</div>";
                res += "<div onclick=\"users.deActiveChecked()\" style='position:relative;float:left;'><a class='btn'>&nbsp;&nbsp;DeActive Checked&nbsp;&nbsp;</a>&nbsp;&nbsp;</div>";
                res += "<div onclick=\"users.add()\"><a class='btn'>&nbsp;&nbsp;New&nbsp;&nbsp;</a>&nbsp;&nbsp;</div>";
                res += "</div>";
                res += "<div style='position:relative;top:15px;!important;'><div>";
                List<model.db.user> users = model.module.security.User.getUsers(0);
                int i = 0;
                String ids = "";
                foreach (var item in users)
                {
                    //admin is type 3
                    if (item.type != 1)
                    {
                        ids += item._uid+",";
                        res += "<div id='user_" + item._uid + "' >";
                        res += "<table width='98%'><tr>";
                        res += "<td nowrap='nowrap' width='10px'><input type='checkbox' id='uchk"+item._uid+"' /></td>";
                        res += "<td width='50px' style='cursor:pointer;'>";
                        res += "<table >";
                        res += "<tr>"+
                            "<td  width='10%' style='font-size:10px !important;' class='grid_btn' nowrap='nowrap' onclick=\"users.edit('" + item._uid + "')\"><img src='' alt='Edit'></td>" +
                            "<td  width='10%' style='font-size:10px !important;' class='grid_btn' nowrap='nowrap' onclick=\"users.toggleActive('" + item._uid + "')\">Toggle Active</<td>" +
                            "</tr>";
                        res += "<tr><td  width='10%' class='grid_btn' nowrap='nowrap' onclick=\"users.delete('" + item._uid + "')\">Delete</td>";
                        res += "<td width='10%' class='grid_btn' nowrap='nowrap' onclick=\"users.resetPWD('" + item._uid + "')\">Reset password</<td></tr>";
                        res += "</table>";
                        res += "</td>";
                        res += "<td width='100%'><table width='100%'><tr><td>Full Name</td><td>User Name</td><td>Mobile</td><td>Phone</td><td>Type</td><td>Activate Date</td><td>Deactivate Date</td><td>Serial Number</td></tr><tr>";
                        res += "<td nowrap='nowrap' nowrap='nowrap'><a href='#' id='uname" + item._uid + "'><b>" + (item.first_name + " " + item.last_name) + "</b></a><a>  [" + ((item.is_active == 1) ? "Active" : "In Active") + "]" + "</a></td>";
                            res += "<td width='10%' nowrap='nowrap'><b>" + (item.user_name.Length > 10 ? item.user_name.Substring(0, 8) + "..." : item.user_name) + "</b></td>";
                        res += "<td width='10%' nowrap='nowrap'><b>" + item.mobile + "</b></td>";
                        res += "<td width='10%' nowrap='nowrap'><b>" + item.phone + "</b></td>";
                        res += "<td width='10%' nowrap='nowrap'>"+(item.type==2?"Company":"Client")+"</td>";
                        res += "<td width='10%' nowrap='nowrap'><b>" + item.company+ "</b></td>";
                        res += "<td width='10%' nowrap='nowrap'><b>" + item.start_date + "</b></td>";
                        res += "<td width='10%' nowrap='nowrap'><b>" + item.end_date + "</b></td>";
                        res += "<td width='10%' nowrap='nowrap'><b>" + item.serial_number + "</b></td>";
                        res += "</td>";
                        res += "</tr>";
                        res += "<tr>";
                        res += "<td id='ud" + item._uid+ "' mode='c' colspan='7'  style='display:none;' >";
                        //List<model.db.gallery_detail> gd = model.module.GalleryDetail.getGalleryDetails(item._uid);
                        //display details here [bnp cards]
                        res += "</td>";
                        res += "</tr>";
                        res += "</table>";
                        res += "</td>";
                        res += "</tr>";
                        res += "</table>";
                        res += "</div>";
                    }
                }
                res += "<input type='text' id='ids' value='"+ids+"' />";
                return res;
            }
            catch (Exception ex) {
                Log.logErr("control.cmsmodules.genrate ", ex);
            }
            return "";
        }

        public static string executeEvent(string mode, HttpContext context)
        {
            int uId = -1;
            if (context.Request["uid"] != null)
            {
                if (context.Request["uid"] != null)
                {
                    try { uId = int.Parse(context.Request["uid"]); }
                    catch { uId = -1; }
                }
            }
            else if (mode == "getclientinfo")
            {
                try
                {
                    uId = int.Parse(context.Request["id"]);
                    model.db.user u = model.module.security.User.getUserById(uId);
                    return "{\"id\":\"" + u._uid + "\",\"address\":\"" + u.address + "\",\"company\":\"" + u.company + "\",\"email\":\"" + u.email + "\",\"end_date\":\"" + u.end_date + "\",\"first_name\":\"" + u.first_name + "\",\"gender\":\"" + u.gender + "\",\"is_active\":\"" + u.is_active + "\",\"last_name\":\"" + u.last_name + "\",\"location\":\"" + u.location + "\",\"mobile\":\"" + u.mobile + "\",\"phone\":\"" + u.phone + "\",\"serial_number\":\"" + u.serial_number + "\",\"social_network1\":\"" + u.social_network1 + "\",\"social_network2\":\"" + u.social_network2 + "\",\"social_newwork3\":\"" + u.social_newwork3 + "\",\"social_number\":\"" + u.social_number + "\",\"start_date\":\"" + u.start_date + "\",\"type\":\"" + u.type + "\",\"user_name\":\"" + u.user_name + "\",\"website\":\"" + u.website + "\",\"work_category\":\"" + u.work_category + "\"}";
                }
                catch { uId = -1; }

            }
            else if (mode == "new") { 
                //get all fields, load image, save it then save data
                model.db.user u = new model.db.user();
                u.address = context.Request["address"];
                u.authenitication_mode = 1;
                u.birthdate = DateTime.Parse(context.Request["birthdate"]);
                u.company = context.Request["company"];
                u.create_date = DateTime.Now;
                try
                {
                    u.create_by = Global.getUserId();
                }
                catch (Exception ex) { }
                u.email = context.Request["email"];
                u.end_date = DateTime.Parse(context.Request["end_date"]);
                u.first_name = context.Request["first_name"];
                try
                {
                    u.gender = int.Parse(context.Request["gender"]);
                }
                catch { 
                }
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
                    str11 = str1[1];
                    u.img = "/view/users_imges/"+imgName + "." + str11;
                }
                u.is_active = 1;
                u.last_name = context.Request["last_name"];
                u.location = int.Parse(context.Request["location"]);
                u.mobile = context.Request["mobile"];
                u.partner_clients = null;
                u.phone = context.Request["phone"];
                u.pwd = Global.Encode(context.Request["pwd"]);
                u.serial_number = context.Request["serial_number"];
                u.social_network1 = context.Request["social_network1"];
                u.social_network2 = context.Request["social_network2"];
                u.social_newwork3 = context.Request["social_network3"];
                u.social_number = context.Request["social_number"];
                u.start_date = DateTime.Parse(context.Request["start_date"]);
                u.type = int.Parse(context.Request["type"]);
                u.user_name = context.Request["user_name"];
                u.pwd = StringCipher.Encrypt(context.Request["pwd"]);
                u.website = context.Request["website"];
                u.work_category = int.Parse(context.Request["work_category"]);
                if (model.module.security.User.insert(u))
                {
                    if (insertImg)
                    {
                        String rr = HttpContext.Current.Server.MapPath("~");
                        file.SaveAs(rr + "/view/users_imges/" + imgName + "." + str11);
                    }
                    //add user role
                    model.db.user_in_role uir = new model.db.user_in_role();
                    uir.__user = u._uid;
                    if (u.type == 2) {//comapny user 
                        uir.__role = ClientManagmentAdmin.companyRole;
                    }
                    else if (u.type == 3)//client user
                    {
                        uir.__role = ClientManagmentAdmin.clientRole;
                    }
                    model.module.security.UserInRole.insert(uir);
                }
            }
            else if (mode == "edit")
            {
                string idAsString = context.Request["id"];
                int id = int.Parse(idAsString);
                model.db.user u = model.module.security.User.getUserById(id);
                u.address = context.Request["address"];
                u.authenitication_mode = 1;
                u.birthdate = DateTime.Parse(context.Request["birthdate"]);
                u.company = context.Request["company"];
                u.create_date = DateTime.Now;
                u.create_by = Global.getUserId();
                u.email = context.Request["email"];
                u.end_date = DateTime.Parse(context.Request["end_date"]);
                u.first_name = context.Request["first_name"];
                try
                {
                    u.gender = int.Parse(context.Request["gender"]);
                }
                catch
                {
                }

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
                    str11 = str1[1];
                    u.img = "/view/users_imges/"+imgName + "." + str11;
                }
                u.last_name = context.Request["last_name"];
                u.location = int.Parse(context.Request["location"]);
                u.mobile = context.Request["mobile"];
                u.partner_clients = null;
                u.phone = context.Request["phone"];
                u.serial_number = context.Request["serial_number"];
                u.social_network1 = context.Request["social_network1"];
                u.social_network2 = context.Request["social_network2"];
                u.social_newwork3 = context.Request["social_network3"];
                u.social_number = context.Request["social_number"];
                u.start_date = DateTime.Parse(context.Request["start_date"]);
                u.type = int.Parse(context.Request["start_date"]);
                u.user_name = context.Request["user_name"];
                u.website = context.Request["website"];
                u.work_category = int.Parse(context.Request["work_category"]);

                bool res = model.module.security.User.update(u);
                if (res){
                    if (insertImg)
                    {
                        String rr = HttpContext.Current.Server.MapPath("~");
                        file.SaveAs(rr + "/view/users_imges/" + imgName + "." + str11);
                    }
                }
            }
            else if (mode == "delete")
            {
                int id = int.Parse(context.Request["id"]);
                model.db.user u = model.module.security.User.getUserById(id);
                model.module.security.User.delete(u);
            }
            else if (mode == "toogleActive")
            {
                string idAsString = context.Request["id"];
                int id = int.Parse(idAsString);
                model.db.user u = model.module.security.User.getUserById(id);
                u.is_active = u.is_active == 1?0:1;
                bool res = model.module.security.User.update(u);
            }
            else if (mode == "activeChecked" || mode == "deactiveChecked") {
                int toBeActiveMode = mode == "activeChecked" ? 1 : 0;
                string[] idsAsString = context.Request["ids"].Split(new char[]{','});
                foreach (String currIdAsString in idsAsString)
                {
                    try
                    {
                        int id = int.Parse(currIdAsString);
                        model.db.user u = model.module.security.User.getUserById(id);
                        u.is_active = toBeActiveMode;
                        bool res = model.module.security.User.update(u);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else if (mode == "resetpwd")
            {
                int id = int.Parse(context.Request["id"]);
                model.db.user u = model.module.security.User.getUserById(id);
                u.pwd = Global.Encode("123456");
                bool res = model.module.security.User.update(u);
                if (res)
                    return "Done Successfully!";
                else
                    return "Could not be reset, cause of internal error";
            }
            ClientManagmentAdmin gma = new ClientManagmentAdmin();
            return gma.generate();
        }
    }
}