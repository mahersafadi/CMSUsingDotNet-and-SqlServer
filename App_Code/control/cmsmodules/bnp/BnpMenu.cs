
using model.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BnpMenu
/// </summary>
namespace control.cmsmodules.bnp
{
    public class BnpMenu : Module
    {
        public BnpMenu()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public BnpMenu(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";
            try
            {
                res += "<div class='navbar-wrapper'>" +
                "<div id='menu_container' class='container' style='marker-offset: 1px'>" +
                "<div class='navbar-header'>" +
                "<button type='button' class='navbar-toggle' data-toggle='collapse' data-target='.navbar-collapse'> <span class='sr-only'>Toggle navigation</span> <span class='icon-bar'></span> <span class='icon-bar'></span> <span class='icon-bar'></span> <span class='icon-bar'></span> </button>" +
                "<a class='navbar-brand' href='/Default.aspx'><img src='/view/bnp/logo.png' id='logo'></a> </div>" +
                "<div class='navbar navbar-inverse navbar-static-top' role='navigation'>" +
                "<div class='container'>" +
                "<div class='navbar-collapse collapse'>" +
                "<ul class='nav navbar-nav'>" +
                "<!------------>" +
                "<li class='dropdown'> <a href='#' class='dropdown-toggle' data-toggle='dropdown'>About BNP <span class='caret'></span></a>" +
                "<ul class='dropdown-menu' role='menu'>" +
                "<li><a href='/Default.aspx?ID=4434'>The Concept</a></li>" +
                "<li class='divider'></li>" +
                "<li><a href='/Default.aspx?ID=4435'>Profile</a></li><li class='dropdown-header'></li>" +
                 "<li class='divider'></li>" +
                 "<li><a href='/Default.aspx?id=4436'>Privacy Policy</a></li>" +
                "<li class='divider'></li>" +
                "<li><a href='/Default.aspx?id=4437'>Terms of Use</a></li>" +
                "<li class='dropdown-header'></li>" +
                "<!------------>" +
                "</ul></li>" +
                "<li class='dropdown'> <a href='#' class='dropdown-toggle' data-toggle='dropdown'>Partners <span class='caret'></span></a>" +
                "<ul class='dropdown-menu' role='menu'>" +
                "<li><a href='/bnp/PartnersList.aspx'>Partner List</a></li>" +
                "<li class='divider'></li>" +
                "<li><a href='/Signin.aspx'>Become a Partner</a></li>" +
                "<li class='dropdown-header'></li>" +
                "</ul></li>" +
                  "<li><a href='/bnp/Sponser.aspx'>Sponsors</a></li>" +
                "<li><a href='/bnp/OffersList.aspx'>Offers</a></li>" +
                "<li><a href='/ViewPhotoGallary.aspx'>Media Gallery</a></li>" +
                "<li><a href='/contactUs.aspx'>Contact us</a></li>";
                if (System.Web.HttpContext.Current.Session["user"] == null)
                {
                    res += "<li><a href='/Signin.aspx'>Sign In or Register</a></li>";
                }
                else
                {
                    res += "<li>";
                    res += "<div>";
                    model.db.user u = (model.db.user)System.Web.HttpContext.Current.Session["user"];
                    if (u.type == 3)
                    {
                        if (u.gender == 1)
                        {
                            res += "<img src='/assets/img/male.fw.png'><a href='/changePWD.aspx'>Change Password</a>";
                        }
                        else
                        {
                            res += "<img src='/assets/img/female.fw.png'><a href='/changePWD.aspx'>Change Password</a>";
                        }
                    }
                    else
                    {
                        res += "<img src='/assets/img/company.fw.png'><a href='/changePWD.aspx'>Change Password</a>";
                    }
                    res += "<div>";
                    res += "</li>";
                }
                res += "<li class='dropdown'> <a href='#' class='dropdown-toggle' data-toggle='dropdown'><img src='/view/bnp/social_mediaa.png' ><span class='caret'></span></a>" +
                "<ul class='dropdown-menu' role='menu' style='left:-25px;min-width:160px;'>" +
                "<li><a href='#' class='social_icon'><img src='/view/bnp/fb.png' > </a>" +
                "<a class='social_icon' href='#'><img src='/view/bnp/twitter.png' > </a>" +
                "<a class='social_icon' href='#'><img src='/view/bnp/g_plus.png' > </a>" +
                "<a class='social_icon' href='#'><img src='/view/bnp/insta.png' > </a>" +
                "<a class='social_icon' href='#'><img src='/view/bnp/youtube.png' > </a></li>" +
                "<br clear='all'></ul></li>" +
                "<li><input type='text' value='Search' class='search_box'><button class='btn_search'><img src='/view/bnp/serach.png'></button></li>" +
                "</ul></div></div></div></div></div>";

                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("control.cmsmodules.bnp.BnpMenu.generate", ex);
            }
            return res;
        }
    }
}