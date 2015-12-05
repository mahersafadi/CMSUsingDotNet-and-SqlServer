using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net.Mail;
using System.Net;

public partial class ContactUs : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //top menu content without slide
        body1.Attributes.Add("onLoad", "Generator.init()");
        control.cmsmodules.Module currModule = control.cmsmodules.Module.getModuleByRegion("region_content_middle");
        
    }
   
    protected void Submit(object sender, EventArgs e)
    {
        if (this.IsValid)
        {
            // Create a new email message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("contactform@yourdomain.com");
            mail.To.Add("youremailaddress@yourdomain.com");
            mail.Subject = "Information Request";
            mail.IsBodyHtml = true;
            mail.Body = "Name: " + this.txtName.Text + "<br />";
            mail.Body += "Organization: " + txtOrganization.Text + "<br />";
            mail.Body += "Phone: " + txtPhone.Text + "<br />";
            mail.Body += "Email: " + txtEmail.Text + "<br />";
            mail.Body += "Request or Question: " + txtRequest.Text + "<br />";

            // Create an SMTP client
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "relayservername";

            // Send the email
            smtp.Send(mail);

            // Define the name and type of the client script on the page.
            String csName = "SuccessNotificationScript";
            Type csType = this.GetType();

            // Get a ClientScriptManager reference from the Page class.
            ClientScriptManager cs = Page.ClientScript;

            // Check to see if the client script is already registered.
            if (!cs.IsClientScriptBlockRegistered(csType, csName))
            {
                string csText = "<script language='javascript'>window.alert('Thank you for submitting your request');</script>";
                cs.RegisterClientScriptBlock(csType, csName, csText.ToString());
            }

            // Clear the form
            ClearForm();
        }
    }

    protected void Reset(object sender, EventArgs e)
    {
        ClearForm();
    }


    protected void ClearForm()
    {
        txtName.Text = "";
        txtOrganization.Text = "";
        txtPhone.Text = "";
        txtEmail.Text = "";
        txtRequest.Text = "";
    }

    }