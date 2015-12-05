using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Security
/// </summary>
public class Security
{
	public Security()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static string editUserFromRequest(HttpContext context, int type, int partnerId, int id)
    {
        try {
            string imgName = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;
            System.Collections.Specialized.NameValueCollection collection = context.Request.Form;
            string firstName = collection["first_name"];
            string lastName = collection["last_name"];
            string email = collection["email"];
            string userName = collection["user_name"];
            string password = collection["pwd"];
            string address = collection["address"];
            string phone = collection["phone"];
            string mobile = collection["mobile"];
            string socialNetWork1 = collection["social_network1"];
            string socialNetWork2 = collection["social_network1"];
            string birthDate = collection["birthdate"];
            string gender = collection["gender"];
            //---------------------------------------------------------
            model.db.user u = model.module.security.User.getUserById(id);
            u.address = address;
            u.authenitication_mode = 2;
            if(birthDate != "undefined")
                u.birthdate = DateTime.Parse(birthDate);
            u.email = email;
            u.first_name = firstName;
            if(gender != "undefined")
                u.gender = int.Parse(gender);
            try
            {
                HttpPostedFile file = context.Request.Files[0];
                string str = file.FileName;
                string[] str1 = str.Split(new char[] { '.' });
                str = str1[1];
                u.img = "/view/imag/" + imgName + "." + str;
            }
            catch
            {
                //u.img = imgName;
            }
            bool res = model.module.security.User.insert(u);
            try
            {
                HttpPostedFile file = context.Request.Files[0];
                string str = file.FileName;
                string[] str1 = str.Split(new char[] { '.' });
                str = str1[1];
                String rr = HttpContext.Current.Server.MapPath("~");
                file.SaveAs(rr + "/view/imag/" + imgName + "." + str);
            }
            catch (Exception ex)
            {
                //Log.logErr("BnpHandler.ProcessRequest", ex);
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(res ? 1 : 0);
        }
        catch (Exception ex)
        {
            Log.logErr("BnpHandler.ProcessRequest", ex);
        }
        return "";
    }
    public static string insertNewUserFromRequest(HttpContext context, int type, int partnerId){
        try
        {
            string imgName = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;
            System.Collections.Specialized.NameValueCollection collection = context.Request.Form;
            string firstName = collection["first_name"];
            string lastName = collection["last_name"];
            string email = collection["email"];
            string userName = collection["user_name"];
            string password = collection["pwd"];
            string address = collection["address"];
            string phone = collection["phone"];
            string mobile = collection["mobile"];
            string socialNetWork1 = collection["social_network1"];
            string socialNetWork2 = collection["social_network1"];
            string birthDate = collection["birthdate"];
            string gender = collection["gender"];
            
            //------------------------------------------------

            model.db.user u = new model.db.user();
            u.address = address;
            u.authenitication_mode = 2;
            u.birthdate = DateTime.Parse(birthDate);
            u.email = email;
            u.first_name = firstName;
            u.gender = int.Parse(gender);
            try
            {
                HttpPostedFile file = context.Request.Files[0];
                string str = file.FileName;
                string[] str1 = str.Split(new char[] { '.' });
                str = str1[1];
                u.img = "/view/imag/" + imgName + "." + str;
            }
            catch
            {
                u.img = imgName;
            }
            u.is_active = 1;
            u.last_name = lastName;
            u.location = null;
            u.mobile = mobile;
            u.pwd = Global.Encode(password);
            u.phone = phone;
            u.social_network1 = socialNetWork1;
            u.social_network2 = socialNetWork2;
            u.type = type;
            u.user_name = userName;
            u.website = "";
            u.work_category = null;
            model.db.role r = model.module.security.Role.getRoleByName("bnp_client");
            model.db.user_in_role uir = new model.db.user_in_role();
            uir.__user = u._uid;
            uir.__role = r._rid;
            model.db.partner_client pc = null;
            if(partnerId != null){
                pc = new model.db.partner_client();
                pc.client_id = u._uid;
                pc.partener_id = partnerId;
            }

            bool res = model.module.security.User.insert(u, uir, pc);
            try
            {
                HttpPostedFile file = context.Request.Files[0];
                string str = file.FileName;
                string[] str1 = str.Split(new char[] { '.' });
                str = str1[1];
                String rr = HttpContext.Current.Server.MapPath("~");
                file.SaveAs(rr + "/view/imag/" + imgName + "." + str);

                context.Response.ContentType = "text/plain";
                context.Response.Write(res ? 1 : 0);
            }
            catch (Exception ex)
            {
                Log.logErr("BnpHandler.ProcessRequest", ex);
            }
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
        return "";
    }
}