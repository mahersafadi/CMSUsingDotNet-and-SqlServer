using control.cmsmodules;
using model.module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BnpClientProfile
/// </summary>
namespace control.cmsmodules.bnp
{
    public class BnpClientProfile : Module
    {
        public BnpClientProfile()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public BnpClientProfile(model.db.module m)
        {
            this.module = m;
            mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
        }

        public override string generate()
        {
            string res = "";
            try
            {
                model.db.user u = (model.db.user)System.Web.HttpContext.Current.Session["user"];
                if (u != null)
                {
                    res += "<table id='client_profile' width='100%'>";
                    res += "<tr style='padding-right:5px'><td align='left' class='label_main1' >Profile <a class='log_out' href='/logout.aspx'>logout</a></td><td align='right' width='5%'></td></tr>";
                    res += "<tr><td height='10px'></td></tr>";
                    res += "<tr><td width='100%' height='1px' bgcolor='#393939'></td></tr>";
                    res += "<tr><td height='10px'></td></tr>";

                    res += "<tr><td align='right'><img style='cursor:pointer;' src='/view/imag/icons/edit.png' onclick='editProfile(\"" + u._uid + "\")' /></td></tr>";
                    if (model.module.security.User.user_types[(int)u.type] == "client")
                        res += "<tr><td align='left' class='label_main3'>" + u.first_name + " " + u.last_name + "</td></tr>";
                    res += "<tr>";
                    res += "<td width='100%'>";
                    res += "<table width='100%'>";
                    res += "<tr>";
                    res += "<td align='left' width='250px'>";
                    //res +=   "<img width:230px;height:220px src='"+u.img+"'>";
                    res += "<img width:230px;height:220px src='" + u.img + "'>";
                    res += "</td>";
                    res += "<td width='5%'></td>";
                    res += "<td>";
                    res += "<table>";
                    res += "<tr>";
                    res += "<td colspan='2' height='20px'></td>";
                    res += "</tr>";
                    if (model.module.security.User.user_types[(int)u.type] == "company")
                    {
                        res += "<tr><td align='left' colspan='2' class='label_main3'>" + u.first_name + " " + u.last_name + "</td></tr>";
                        res += "<tr>";
                        res += "<td class='label_main2'>Mobile</td>";
                        res += "<td  align='left'>&nbsp; &nbsp;" + u.mobile + "</td>";
                        res += "</tr>";
                    }
                    res += "<tr>";
                    res += "<td  class='label_main2'>Phone</td>";
                    res += "<td  align='left' >&nbsp; &nbsp;" + u.phone + "</td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2' align='left' >Region</td>";
                    if (u.location != null)
                    {
                        model.db.location l = Location.getById((int)u.location);
                        if (l != null)
                        {
                            res += "<td align='left'>&nbsp; &nbsp;" + l.name + "</td>";
                        }
                    }
                    res += "</tr>";
                    res += "<tr><td  class='label_main2'>Email</td><td  align='left'>&nbsp; &nbsp;" + u.email + "</td></tr>";
                    res += "<tr><td  class='label_main2'>Address</td><td align='left'>&nbsp; &nbsp;" + u.address + "</td></tr>";

                    if (model.module.security.User.user_types[(int)u.type] == "company")
                    {
                        if (u.work_category != null)
                        {
                            res += "<tr><td  class='label_main2'>Category</td>";
                            model.db.job_type jt = JobTypes.getById((int)u.work_category);
                            res += "<td  align='left'>&nbsp; &nbsp;" + jt.name + "</td>";
                            res += "</tr>";
                        }
                        res += "<tr><td  class='label_main2'>Website</td><td align='left'>&nbsp; &nbsp;" + u.website + "</td></tr>";
                    }
                    else if (model.module.security.User.user_types[(int)u.type] == "client")
                    {
                        res += "<tr>";

                        res += "</tr>";
                        res += "<tr>";
                        res += "<td class='label_main2'>Social Net Work</td>";
                        res += "<td  align='left'>&nbsp; &nbsp;" + u.social_network1 + "</td>";
                        res += "</tr>";
                        res += "<tr>";
                        res += "<td></td>";
                        res += "<td  align='left'>&nbsp; &nbsp;" + u.social_network2 + "</td>";
                        res += "</tr>";
                        res += "<tr>";
                        res += "<td></td>";
                        res += "<td  align='left'>&nbsp; &nbsp;" + u.social_newwork3 + "</td>";
                        res += "</tr>";
                        res += "<tr>";
                        res += "<td align='left' class='label_main2'>Serial Number</td>";
                        res += "<td align='left'>&nbsp; &nbsp;" + u.social_number + "</td>";
                        res += "</tr>";
                    }
                    res += "</tr>";
                    res += "</table>";
                    res += "</td>";
                    res += "</tr>";
                    res += "</table>";
                    res += "</td>";
                    res += "</tr>";
                }
                res += "</table>";

                //-------------------------------------------------
                //-------------------------------------------------

                res += "<table id='client_profileEdit' width='100%' style='display:none;'>";
                res += "<tr><td align='left' class='label_main1' colspan='2'>Edit Profile</td></tr>";
                res += "<tr><td height='10px'  colspan='2'></td></tr>";
                res += "<tr><td width='100%' height='1px'  colspan='2' bgcolor='#393939'></td></tr>";
                res += "<tr><td height='10px'  colspan='2'></td></tr>";
                res += "<tr><td align='right'  colspan='2'><img style='cursor:pointer;' src='/view/imag/icons/back.png' onclick='backToProfile()' /><img style='cursor:pointer;' src='/view/imag/icons/save.png' onclick='doSaveNewClient(\"" + u._uid + "\")' /></td></tr>";
                res += "<tr><td height='10px'></td></tr>";
                if (model.module.security.User.user_types[(int)u.type] == "company")
                {
                    res += "<tr>";
                    res += "<td><table><tr>";
                    res += "<td>";
                    res += "<img width='230px' height='220px' src='" + u.img + "' />";
                    res += "<td>";
                    res += "<td width='10%'></td>";
                    res += "<td>";
                    res += "<table width='100%'>";
                    res += "<tr><td class='label_main2' align='right'>Company&nbsp;&nbsp;</td><td align='left'><input class='inp' type='text' id='genfirst_name' value='" + u.first_name + "' /></td></tr>";
                    res += "<tr><td  class='label_main2' align='right'>Mobile&nbsp;&nbsp;</td><td align='left'><input class='inp' type='text' id='genmobile'      value='" + u.mobile + "' /></td></tr>";
                    res += "<tr><td  class='label_main2' align='right'>Phone&nbsp;&nbsp;</td><td align='left'><input class='inp' type='text' id='genphone'        value='" + u.phone + "' /></td></tr>";
                    res += "<tr><td  class='label_main2' align='right'>Region&nbsp;&nbsp;</td><td align='left'><select class='inp' id='genregion' value='" + u.location + "'>";
                    List<model.db.location> locations = model.module.Location.list();
                    foreach (var item in locations)
                    {
                        res += "<option value='" + item._lid + "' ";
                        if (item._lid == u.location)
                            res += " selected='selected' ";
                        res += ">";
                        res += item.name;
                        res += "</option>";
                    }
                    res += "</select></td></tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2' class='inp' align='right'>Email&nbsp;&nbsp;</td>";
                    res += "<td align='left'><input type='text' class='inp' id='genemail' value='" + u.email + "' /></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2' align='right'>Adress&nbsp;&nbsp;</td>";
                    res += "<td align='left'><input type='text' class='inp' id='genaddress' value='" + u.address + "' /></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td align='right' class='label_main2'>Category&nbsp;&nbsp;</td>";
                    res += "<td align='left'><select id='gencategory' class='inp' value='" + u.work_category + "' >";
                    List<model.db.job_type> collection = model.module.JobTypes.list();
                    foreach (var item in collection)
                    {
                        res += "<option value='" + item._jid + "' ";
                        if (item._jid == u.work_category)
                            res += " selected='selected' ";
                        res += ">";
                        res += item.name;
                        res += "</option>";
                    }
                    res += "</select></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2' align='right'>Website&nbsp;&nbsp;</td>";
                    res += "<td align='left'><input type='text' class='inp' id='genwebsite' value='" + u.website + "' /></td>";
                    res += "</tr>";
                    res += "<td  class='label_main2' align='right'>User Name&nbsp;&nbsp;</td>";
                    res += "<td align='left'><input type='text' class='inp' readonly='true' id='genuser_name' value='" + u.user_name + "' /></td>";
                    res += "<tr>";
                    res += "<td align='right' class='label_main2'>New Image&nbsp;&nbsp;</td>";
                    res += "<td align='left'><input type='file'  id='genimg' /></td>";
                    res += "</tr>";
                    res += "</table>";
                    res += "</td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "</tr>";
                    res += "</table></td>";
                    res += "</tr>";
                }
                else if (model.module.security.User.user_types[(int)u.type] == "client")
                {
                    res += "<tr>";
                    res += "<td>";
                    res += "<table width='100%'>";
                    res += "<tr><td  class='label_main2'>First Name&nbsp;&nbsp;</td><td><input type='text' id='genfirst_name' value='" + u.first_name + "' /></td></tr>";
                    res += "<tr><td  class='label_main2'>Last Name&nbsp;&nbsp;</td><td><input type='text' id='genlast_name' value='" + u.last_name + "' /></td></tr>";
                    res += "<tr><td  class='label_main2'>Phone&nbsp;&nbsp;</td><td><input type='text' id='genphone'        value='" + u.phone + "' /></td></tr>";
                    res += "<tr><td  class='label_main2'>Region&nbsp;&nbsp;</td><td><select id='genregion' value='" + u.location + "'>";
                    List<model.db.location> locations = model.module.Location.list();
                    foreach (var item in locations)
                    {
                        res += "<option value='" + item._lid + "' ";
                        if (item._lid == u.location)
                            res += " selected='selected' ";
                        res += ">";
                        res += item.name;
                        res += "</option>";
                    }
                    res += "</select></td></tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2'>Email&nbsp;&nbsp;</td>";
                    res += "<td><input type='text' id='genemail' value='" + u.email + "' /></td>";
                    res += "</tr>";



                    res += "<tr><td  class='label_main2'>Mobile&nbsp;&nbsp;</td><td><input type='text' id='genmobile'      value='" + u.mobile + "' /></td></tr>";

                    res += "<tr>";
                    res += "<td  class='label_main2'>Adress&nbsp;&nbsp;</td>";
                    res += "<td><input type='text' id='genaddress' value='" + u.address + "' /></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2'>Social Netwrok1</td>";
                    res += "<td><input type='text' id='gensocial_network1' value='" + u.social_network1 + "' /></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2'>Social Netwrok1</td>";
                    res += "<td><input type='text' id='gensocial_network2' value='" + u.social_network2 + "' /></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2'>Serial Number</td>";
                    res += "<td><input type='text' id='gensocial_number' value='" + u.social_number + "' /></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2' align='right'>User Name&nbsp;&nbsp;</td>";
                    res += "<td align='left'><input type='text'  readonly='true'  class='inp' id='genuser_name' value='" + u.user_name + "' /></td>";
                    res += "</tr>";
                    res += "<tr>";
                    res += "<td  class='label_main2'>New Image&nbsp;&nbsp;</td>";
                    res += "<td><input type='file' id='genimg' /></td>";
                    res += "</tr>";
                    res += "</table>";
                    res += "</td>";
                    res += "</tr>";
                }
            }
            catch (Exception ex)
            {
                Log.logErr("BnpClientProfile.generate", ex);
            }
            return res;
        }
    }
}