using model.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Signin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Check Security
        regions.Value = "region_header";
        regionsPriorities.Value = "1";
        var _regions = model.module.Location.list();
        for (int i = 0; i < _regions.Count(); i++)
            selRegion.Items.Add(new ListItem(_regions[i].name, ""+_regions[i]._lid));
        var categories = model.module.JobTypes.list();
        for (int i = 0; i < categories.Count(); i++) 
            selCategory.Items.Add(new ListItem(categories[i].name, ""+categories[i]._jid));
        body1.Attributes.Add("onLoad", "Generator.init()");
    }
    protected void lnkSignUp_Click(object sender, EventArgs e)
    {
        model.db.user u = new model.db.user();
        String textId = txtKind.Value;
        u.type = int.Parse(textId);
        //vilidate
        if (txtDate.Text != null && txtDate.Text != "")
            u.first_name = txtFirstName.Text;
        else 
        {
            Label31.Text = "You must enter First Name,Please Try Again";
            Label21.Text = "You must enter First Name,Please Try Again";
            return;
        }
        u.last_name = txtLastName.Text;
        u.mobile = txtMobile.Text;
        u.phone = txtPhone.Text;
        u.email = txtEmail.Text;
        u.address = txtAddress1.Text;
        //u. = txtAddress2.Text;
        u.location = int.Parse(selRegion.Value);
        u.gender = int.Parse(raGender.SelectedValue);
        if (txtDate.Text != null && txtDate.Text!="")
            u.birthdate = DateTime.Parse(txtDate.Text);
        else if(u.type==3)
        {
            Label31.Text = "You must enter birth date,Please Try Again";
            Label21.Text = "You must enter birth date,Please Try Again";
            return;
        }
        u.company = Company.Text;
        u.company = txtWebsite.Text;
        u.work_category = int.Parse(selCategory.Value);
        u.is_active = 0;
        u.user_name = u.first_name + "_" + u.last_name + "_" + u.birthdate+"_"+u.company;
        u.pwd = "123456";
        //insert data into data base as primary user needed to be confirmed

        bool result = model.module.security.User.insert(u);
        Label21.Text = "";
        Label31.Text = "";
        Labe22.Text = "";
        Labe32.Text = "";

        if (result)
        {
            //confirm of ok
            Labe32.Text = "Your request hase been regesterd successfully, Bnp Team Wil Contacts you ASAP";
            Labe22.Text = "Your request hase been regesterd successfully, Bnp Team Wil Contacts you ASAP";
        }
        else { 
            //error
            Label31.Text = "Your request hase not been regesterd successfully, Please Try Again";
            Label21.Text = "Your request hase not been regesterd successfully, Please Try Again";
        }
    }
    protected void lnkLogin_Click(object sender, EventArgs e)
    {
        string un = txtUserName.Text;
        string pw = txtPassword.Text;
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
            string defaultUrl = user.user_in_roles.First().role.default_url;
            if (defaultUrl == null)
                defaultUrl = "";
            Response.Redirect(defaultUrl);

        }
        else {
            lblInfo.Text = Lang.getByKey("user_name_or_password_is_not_correct");

        }
    }

    protected void register_click(object sender, EventArgs e) 
    {
        string fn = txtFirstName.Text;
        string ln = txtLastName.Text;
        string phone = txtPhone.Text;
        string email = txtEmail.Text;
        string addrs1 = txtAddress1.Text;
        string addrs2 = txtAddress2.Text;

    }
}