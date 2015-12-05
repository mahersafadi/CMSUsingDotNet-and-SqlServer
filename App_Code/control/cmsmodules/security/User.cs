using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.module;

/// <summary>
/// Summary description for User
/// </summary>
/// 
namespace control.cmsmodules.security
{

    public class User
    {
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string generate()
        {
            string res = "";
            try
            {
                //res += new Role().generateApplicationSelect();
                //res += "<br />";
                List<model.db.user> users = model.module.security.User.getUsers(Application.appId);
                res = @"<a href='#' onclick='User.insert_user()'>insert new user</a> <br />";
                if (users != null)
                {
                    if (users.Count > 0)
                        res += @"<table class='gridtable'>
                                <tr>
                                    <th style='width:150px;'>name</th>
                                    <th >user id</th>
                                    <th>address</th>
                                    <th>email</th>
                                    <th></th>
                                </tr>";
                    foreach (model.db.user user in users)
                    {

                        res += "<tr><td>" + user.first_name + " " + user.last_name + "</td>";
                        res += "<td>" + user.user_name + "</td>";
                        res += "<td>" + user.address + "</td>";
                        res += "<td>" + user.email + "</td>";
                        res += @"<td><a href='#' onclick='User.edit_user(" + user._uid + @")' > edit </a>
                                    &nbsp;<a href='#' onclick='User.delete_user(" + user._uid + @")' > delete </a>
                                </td>
                              </tr>";
                    }
                    if (users.Count > 0)
                        res += "</table>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.generate", ex);
            }
            return res;
        }

        public string generateInsert()
        {
            string res = "";
            try
            {
                res = @"<table class='gridtable' id='tbl_insert_user'>
                            <tr><th colspan='2' >new record</th></tr>
                            <tr>
                                <th style='width:150px;'>first name</th><td><input type='text' id='f_name' /> </td>
                            </tr>
                            <tr>
                                <th>last name</th><td><input type='text' id='l_name' /> </td>
                            </tr>
                            <tr>
                                <th>user name</th><td><input type='text' id='user_name' /> </td>
                            </tr>
                            <tr>
                                <th>password</th><td><input type='password' id='pass' /> </td>
                            </tr>
                            <tr>
                                <th>confirm password</th><td><input type='password' id='conf_pass' /> </td>
                            </tr>
                            <tr>
                                <th>email</th><td><input type='text' id='email' /> </td>
                            </tr>
                            <tr>
                                <th>address</th><td><input type='text' id='address' /> </td>
                            </tr>
                            <tr>
                                <th>authentication mode</th><td>" + generateAuthenticatioModeASelect() + @" </td>
                            </tr>
                            <tr>
                                <th>active</th><td><input type='checkbox' id='is_active' /></td>
                            </tr>
                            <tr>
                                <th>captcha</th><td><img src='Captcha.ashx' /> <br /><input type='text' id='captcha' /> </td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='javascript:User.do_insert_user();return false;' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='User.generate()' >back</a></th>
                            </tr>
                            <tr>
                               <td colspan='2'><div id='div_info' style='width:100%'></div> </td>
                            </tr>
                         </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.generateInsert", ex);
            }
            return res;
        }

        private static string generateAuthenticatioModeASelect(string selectedValue = null)
        {
            string res = "";
            try
            {
                Array itemValues = System.Enum.GetValues(typeof(AutheniticationMode));
                Array itemNames = System.Enum.GetNames(typeof(AutheniticationMode));
                res = "<Select id='authentication_mode'>";
                for (int i = 0; i <= itemNames.Length - 1; i++)
                {
                    int val = (int)itemValues.GetValue(i);
                    string selected = "";
                    if (selectedValue != null && val.ToString() == selectedValue)
                        selected = "selected";
                    res += "<option " + selected + " value='" + val + "' >" + itemNames.GetValue(i) + "</option>";
                }
                res += "</select>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.generateInsert", ex);
            }
            return res;
        }

        public string doInsert(model.db.user user)
        {
            try
            {
                if (model.module.security.User.insert(user))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.doInsert", ex);
            }
            return "";
        }


        public string generateEdit(string id)
        {
            string res = "";
            try
            {
                model.db.user user = model.module.security.User.getUserById(Convert.ToInt32(id));
                string isActive = user.is_active == 1 ? "checked" : "";
                res = @"<table class='gridtable' id='tbl_edit_user'>
                            <tr><th colspan='2' >edit record</th></tr>
                            <tr>
                                <th style='width:150px;'>first name</th><td><input type='text' value='" + user.first_name + @"' id='f_name' /> </td>
                            </tr>
                            <tr>
                                <th>last name</th><td><input type='text' id='l_name' value='" + user.last_name + @"'/> </td>
                            </tr>
                            <tr>
                                <th>user name</th><td><input type='text' id='user_name' readonly value='" + user.user_name + @"'/> </td>
                            </tr>
                            <tr>
                                <th>email</th><td><input type='text' id='email' readonly value='" + user.email + @"'/> </td>
                            </tr>
                            <tr>
                                <th>address</th><td><input type='text' id='address' value='" + user.address + @"'/> </td>
                            </tr>
                            <tr>
                                <th>authentication mode</th><td>" + generateAuthenticatioModeASelect(user.authenitication_mode.ToString()) + @" </td>
                            </tr>
                            <tr>
                                <th>active</th><td><input type='checkbox' id='is_active' " + isActive + @"/></td>
                            </tr>
                            <tr>
                                <th>captcha</th><td><img src='Captcha.ashx' /> <br /><input type='text' id='captcha' /> </td>
                            </tr>
                            <tr>
                                <th colspan='2'><a href='#' onclick='javascript:User.do_edit_user(" + user._uid + @");return false;' >save</a> &nbsp;&nbsp;
                                <a href='#' onclick='User.generate()' >back</a></th>
                            </tr>
                            <tr>
                               <td colspan='2'><div id='div_info' style='width:100%'></div> </td>
                            </tr>
                         </table>";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.generateEdit", ex);
            }
            return res;
        }


        public string doEdit(model.db.user user)
        {
            try
            {
                if (model.module.security.User.update(user))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.doEdit", ex);
            }
            return "0";
        }


        public string delete(model.db.user user)
        {
            try
            {
                if (model.module.security.User.delete(user))
                    return "done";
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.delete", ex);
            }
            return "";
        }

        public static string userHandler(string mode, string data, string id)
        {
            string res = "";
            try
            {
                /***
                 * mode= 1 => generate
                 * mode= 2 => generate insert
                 * mode= 3 => do insert
                 * mode= 4 => generate edit
                 * mode= 5 => do edit
                 * mode= 6 => delete
                 * **/
                model.db.user u = new model.db.user();
                switch (mode)
                {

                    case "1":
                        res = generate();
                        break;
                    case "2":
                        res = new User().generateInsert();
                        break;
                    case "3":
                        string[] fields = data.Split(',');
                        bool isEmptyField = false;
                        string confirmPass = "";
                        string captchaInSession = "";
                        string captcha = "";
                        try
                        {
                            captchaInSession = HttpContext.Current.Session["Captcha"].ToString();
                        }
                        catch { };

                        foreach (string fld in fields)
                        {
                            if (fld.Substring(0, fld.IndexOf(":")) == "f_name")
                                u.first_name = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "l_name")
                                u.last_name = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "user_name")
                                u.user_name = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "pass")
                                u.pwd = Global.Encode(fld.Substring(fld.IndexOf(":") + 1));
                            else if (fld.Substring(0, fld.IndexOf(":")) == "conf_pass")
                                confirmPass = Global.Encode(fld.Substring(fld.IndexOf(":") + 1));
                            else if (fld.Substring(0, fld.IndexOf(":")) == "email")
                                u.email = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "address")
                                u.address = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "is_active")
                                u.is_active = fld.Substring(fld.IndexOf(":") + 1) == "true" ? 1 : 0;
                            else if (fld.Substring(0, fld.IndexOf(":")) == "authentication_mode")
                                u.authenitication_mode = Convert.ToInt32(fld.Substring(fld.IndexOf(":") + 1));
                            else if (fld.Substring(0, fld.IndexOf(":")) == "captcha")
                                captcha = fld.Substring(fld.IndexOf(":") + 1);



                            if (fld.Substring(fld.IndexOf(":") + 1) == "")
                            {
                                isEmptyField = true;
                                break;
                            }
                        }
                        if (isEmptyField)
                            res = Lang.getByKey("fill_all_fields");
                        else if (u.user_name.Contains(" "))
                            res = Lang.getByKey("user_name_must_not_contain_spaces");
                        else if (u.pwd.ToLower() != confirmPass.ToLower())
                            res = Lang.getByKey("password_not_matched");
                        else if (!Global.isValidEmail(u.email))
                            res = Lang.getByKey("email_not_valid");
                        else if (captchaInSession != captcha)
                            res = Lang.getByKey("captcha_not_matched");
                        else if (model.module.security.User.getUserByName(u.user_name) != null)
                            res = Lang.getByKey("user_name_already_exists");
                        else if (PasswordAdvisor.CheckStrength(u.pwd) < PasswordScore.Medium)
                            res = Lang.getByKey("password_weak");
                        else
                            res = new User().doInsert(u);
                        break;
                    case "4":
                        res = new User().generateEdit(id);
                        break;
                    case "5":
                        fields = data.Split(',');
                        isEmptyField = false;
                        confirmPass = "";
                        captchaInSession = "";
                        captcha = "";
                        try
                        {
                            captchaInSession = HttpContext.Current.Session["Captcha"].ToString();
                        }
                        catch { };
                        u._uid = Convert.ToInt32(id);
                        foreach (string fld in fields)
                        {
                            if (fld.Substring(0, fld.IndexOf(":")) == "f_name")
                                u.first_name = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "l_name")
                                u.last_name = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "user_name")
                                u.user_name = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "email")
                                u.email = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "address")
                                u.address = fld.Substring(fld.IndexOf(":") + 1);
                            else if (fld.Substring(0, fld.IndexOf(":")) == "is_active")
                                u.is_active = fld.Substring(fld.IndexOf(":") + 1) == "true" ? 1 : 0;
                            else if (fld.Substring(0, fld.IndexOf(":")) == "authentication_mode")
                                u.authenitication_mode = Convert.ToInt32(fld.Substring(fld.IndexOf(":") + 1));
                            else if (fld.Substring(0, fld.IndexOf(":")) == "captcha")
                                captcha = fld.Substring(fld.IndexOf(":") + 1);

                            if (fld.Substring(fld.IndexOf(":") + 1) == "")
                            {
                                isEmptyField = true;
                                break;
                            }
                        }
                        if (isEmptyField)
                            res = Lang.getByKey("fill_all_fields");
                        else if (u.user_name.Contains(" "))
                            res = Lang.getByKey("user_name_must_not_contain_spaces");
                        else if (!Global.isValidEmail(u.email))
                            res = Lang.getByKey("email_not_valid");
                        else if (captchaInSession != captcha)
                            res = Lang.getByKey("captcha_not_matched");
                        else
                            res = new User().doEdit(u);
                        break;
                    case "6":
                        u = model.module.security.User.getUserById(Convert.ToInt32(id));
                        res = new User().delete(u);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.security.User.userHandler", ex);
            }
            return res;
        }

    }
}