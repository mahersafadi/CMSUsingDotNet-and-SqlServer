using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class changePWD : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        regions.Value = "region_header";
        regionsPriorities.Value = "1";
        body1.Attributes.Add("onLoad", "Generator.init()");
        if (x.Value == "changePWD")
        {
            model.db.user u = (model.db.user)System.Web.HttpContext.Current.Session["user"];
            if (u != null)
            {
                if (u.user_name == userName.Value)
                {
                    if (StringCipher.Decrypt(u.user_name) == currPWD.Value)
                    {
                        if (newPWD.Value == confirmPWD.Value)
                        {
                            model.db.CMSDataContext db = new model.db.CMSDataContext();
                            model.db.user uu = db.users.Single(uu1 => uu1._uid == u._uid);
                            uu.pwd = StringCipher.Encrypt(newPWD.Value);
                            db.SubmitChanges();
                            msg.InnerHtml = "<font size='+2' color='Green'>Done Successfully</font>";
                        }
                        else
                        {
                            msg.InnerHtml = "<font size='+2' color='red'>New Passowrd and its confirm are mismatched</font>";
                        }
                    }
                    else
                    {
                        msg.InnerHtml = "<font size='+2' color='red'>Current password is incorrect</font>";
                    }
                }
                else
                {
                    msg.InnerHtml = "<font size='+2' color='red'>Incorrect user name</font>";
                }
            }
            else
            {
                msg.InnerHtml = "<font size='+1' color='red'>Not Even Logged In,</font>";
            }
        }
    }
}