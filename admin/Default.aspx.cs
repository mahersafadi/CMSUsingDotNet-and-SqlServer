using model.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string _contentId = Request.QueryString["id"];
        string _categoryId = Request.QueryString["catId"];
        string _fromId = Request.QueryString["fromId"];
        string _engSort = Request.QueryString["eng_order"];
        if (_contentId == null && _categoryId == null && _fromId == null)
        {
            regions.Value = "r1_left_1";
            regionsPriorities.Value = "1";
            body1.Attributes.Add("onLoad", "Generator.init()");
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
            mvAll.SetActiveView(vwAll);
            mvLogin.SetActiveView(vwUser);
            lblUser.Text = "Welcome, " + ((model.db.user)System.Web.HttpContext.Current.Session["user"]).first_name + " " +
                                    ((model.db.user)System.Web.HttpContext.Current.Session["user"]).last_name + "<br /> <a style='color:white;' href='default.aspx?logout=true' >logout</a>";
        }
        else
            lblInfo.Text = Lang.getByKey("user_name_or_password_is_not_correct");


    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        System.Web.HttpContext.Current.Session["user"] = null;
        Response.Redirect("Default.aspx");
    }
}