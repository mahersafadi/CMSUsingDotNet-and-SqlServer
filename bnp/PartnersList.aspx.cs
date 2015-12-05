using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PartnersList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string ret  = "";
        body1.Attributes.Add("onLoad", "Generator.init()");
        List<model.db.user> users =  model.module.security.User.getUsers(-1).ToList();
        if (users != null)
        {
            ret = "<table width='100%'>";
            int numberOfItemsInEachRow = 3;
            int currItems = 0;
            foreach (var selUsr in users)
            {
                if (selUsr.type != null)
                {
                    if (model.module.security.User.user_types[(int)selUsr.type] == "client")
                    {
                        if (currItems == 0)
                            ret += "<tr>";

                        ret += "<td>";
                        ret += "<table>";
                        ret += "<tr><td height='20px'></td></tr>";
                        ret += "<tr><td><img width='231px' height='222px1' src='" + selUsr.img + "' /></td></tr>";
                        ret += "<tr><td height='10px'></td></tr>";
                        ret += "<tr><td><table width='100%'><tr><td align='left'>" + (selUsr.first_name + " " + selUsr.last_name) + "</td><td align='right' style='color:#D83473'></td><td align='right' style='cursor:pointer;'></td></tr></table></td></tr>";
                        ret += "<tr><td height='1px' bgcolor='#DD1762'></td></tr>";
                        ret += "</table>";
                        ret += "</td>";
                        currItems++;
                        if (currItems % numberOfItemsInEachRow == 0)
                        {
                            ret += "</tr>";
                            currItems = 0;
                        }
                    }
                }
            }
        }
        partnerListDiv.InnerHtml = ret;
    }


}