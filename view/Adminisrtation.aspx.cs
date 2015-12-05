using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using control.cmsmodules;
using model.module;

public partial class view_Adminisrtation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Module.isAdmin = true;
        //model.db.user user = model.module.security.User.getUserByName("admin");
        //System.Web.HttpContext.Current.Session["user"] = user;

        string logout = Request["logout"];
        if (logout != null)
            System.Web.HttpContext.Current.Session["user"] = null;

        if (!IsPostBack)
        {
            if (System.Web.HttpContext.Current.Session["user"] == null)
            {
                mvAll.SetActiveView(vwLogin);
                btnOk.Text = model.module.Lang.getByKey("ok");
            }
            else
            {
                mvAll.SetActiveView(vwAll);
                mvLogin.SetActiveView(vwUser);
                lblUser.Text = "Welcome, " + ((model.db.user)System.Web.HttpContext.Current.Session["user"]).last_name + " " +
                                    ((model.db.user)System.Web.HttpContext.Current.Session["user"]).last_name + "<br /> <a href='Adminisrtation.aspx?logout=true' >logout</a>";
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text.Trim() == "")
        {
            lblInfo.Text = Lang.getByKey("enter_user_name");
            return;
        }
        else if (txtPassword.Text == "")
        {
            lblInfo.Text = Lang.getByKey("enter_password");
            return;
        }

        bool canAccess = control.cmsmodules.Security.canAccess(txtUserName.Text, txtPassword.Text);
        if (canAccess)
        {
            model.db.user user = model.module.security.User.getUserByName(txtUserName.Text);
            System.Web.HttpContext.Current.Session["user"] = user;
            secretkey.Value = StringCipher.Encrypt(user.user_name);
            Global.loggedInUserAsAdmin = user;
            mvAll.SetActiveView(vwAll);
            mvLogin.SetActiveView(vwUser);
            lblUser.Text = "Welcome, " + ((model.db.user)System.Web.HttpContext.Current.Session["user"]).first_name + " " +
                                    ((model.db.user)System.Web.HttpContext.Current.Session["user"]).last_name + "<br /> <a style='color:white;' href='Adminisrtation.aspx?logout=true' >logout</a>";
        }
        else
            lblInfo.Text = Lang.getByKey("user_name_or_password_is_not_correct");


    }
    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        System.Web.HttpContext.Current.Session["user"] = null;
        //Global.loggedInUserAsAdmin = null;
        secretkey.Value = null;
        Response.Redirect("Adminisrtation.aspx");
    }
}