
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GalleryModuleAdmin
/// </summary>
namespace control.cmsmodules
{
    public class GalleryModuleAdmin : Module
    {
        public static int gId;
        public GalleryModuleAdmin()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        
        public override string generate()
        {
            try
            {
                string res = "<div id='gpanel' style='position: fixed; top: 200px; left: 400px; display:none;'>";
                res += "<table>";
                res += "<tr><td>Album Name:</td>";
                res += "<td><input type=\"text\" id=\"gName\" /></td></tr>";
                res += "<tr><td colspan='3'><a onclick='gallary.ok()' href='#'>OK</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a onclick='gallary.cancel()' href='#'>Cancel</a><input type='hidden' id='gid' /></td>";
                res += "</tr>";
                res += "</table>";
                res += "</div>";
                res += "<div class='gallary_main'>";
                res += "<div onclick=\"gallary.add()\">New</div>";
                res += "</div>";
                List<model.db.gallery> g = model.module.Gallery.list();
                //Generate Album List[Gallary list]
                int i = 0;
                foreach (var item in g)
                {
                    res += "<div id='gallary_disp"+item._gid+"' >";
                    res += "<table width='80%'><tr>";
                    res += "<td nowrap='nowrap'><a href='#' id='gname"+item._gid+"'>" + item.name + "</a><a>  ["+((item.is_active==1)?"Active":"In Active")+"]" +"</a></td>";
                    res += "<td width='10%' nowrap='nowrap'><b>" + item.create_date + "</b></td>";
                    res += "<td  width='10%' class='btn' nowrap='nowrap' onclick=\"gallary.edit('" + item._gid + "')\">Edit</td>";
                    res += "<td  width='10%' class='btn' nowrap='nowrap' onclick=\"gallary.delete('" + item._gid + "')\">Delete</td>";
                    res += "<td  width='10%' class='btn' nowrap='nowrap' onclick=\"gallary.toggleActive('" + item._gid + "')\">Toggle Active</<td>";
                    res += "<td  width='10%' class='btn' nowrap='nowrap' onclick=\"gallary.viewImges('" + item._gid + "')\">View Images</<td>";

                    res += "</tr>";
                    res += "<tr>";
                    res += "<td id='gd"+item._gid+"' mode='c' colspan='7' "+(item._gid == gId?"":"style='display:none;")+"'>";
                    List<model.db.gallery_detail> gd = model.module.GalleryDetail.getGalleryDetails(item._gid);
                    int k = 0;
                    res += "<table>";
                    res += "<tr>";
                    res += "<td colspan='5' nowrap='nowrap'> ";
                    res += "<input type='file' id='f" + item._gid + "' />";
                    res += "<a href='#' onclick='gallary.addImage(\"" + item._gid + "\")'>Add Image</a>";
                    res += "</td>";
                    res += "</tr>";

                    foreach (var item1 in gd)
                    {
                        if(k == 0)
                        {
                            res += "<tr>";
                        }
                        res += "<td id=\"'"+item1._cdid+"'\">";
                        res += "<a><img width='100px' src='/view/pg_rep/" + item1.img_url + "'/></a>";
                        res += "<img width='15px' onclick=\"gallary.deleteImg('"+item1._cdid+"')\" src='/view/imag/icons/deletered.png' />";
                        res += "<td>";
                        k++;
                        if (k % 5 == 0)
                        {
                            res += "</tr>";
                            k = 0;
                        }
                    }
                    res += "</table>";
                    res += "</td>";
                    res += "</tr>";
                    res += "</table>"; 
                    res += "</div>";
                }
                res += "</div>";
                return res;
            }
            catch (Exception ex)
            {
                Log.logErr("GallaryModelAdmin.genrate ", ex);
            }
            return "";
        }

        public static string executeEvent(string mode, HttpContext context){
            gId = -1;
            if (context.Request["gid"] != null)
            {
                try { gId = int.Parse(context.Request["gid"]); }
                catch { gId = -1; }
            }
            string str = "";
            try
            {
                string name = context.Request["name"];
                if (mode == "new")
                {
                    model.db.gallery g = new model.db.gallery();
                    g.is_active = 1;
                    g.name = name;
                    g.create_by = Global.getUserId();
                    g.create_date = DateTime.Now;
                    bool res = model.module.Gallery.insert(g);
                }
                else if (mode == "edit")
                {
                    string idAsString = context.Request["id"];
                    int id = int.Parse(idAsString);
                    model.db.gallery g = model.module.Gallery.getById(id);
                    g.name = name;
                    bool res = model.module.Gallery.update(g);
                }
                else if (mode == "delete")
                {
                    string idAsString = context.Request["id"];
                    int id = int.Parse(idAsString);
                    model.db.gallery g = model.module.Gallery.getById(id);
                    bool res = model.module.Gallery.delete(g);
                }
                else if (mode == "toogleActive")
                {
                    string idAsString = context.Request["id"];
                    int id = int.Parse(idAsString);
                    model.db.gallery g = model.module.Gallery.getById(id);
                    g.is_active = g.is_active == 1?0:1;
                    bool res = model.module.Gallery.update(g);
                }
                else if (mode == "addimg")
                {
                    string imgName = DateTime.Now.Year + "_" + DateTime.Now.Month + "_" + DateTime.Now.Day + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + DateTime.Now.Millisecond;
                    string idAsString = context.Request.Form["id"];
                    int id = int.Parse(idAsString);
                    HttpPostedFile file = context.Request.Files[0];
                    string str11 = file.FileName;
                    string[] str1 = str11.Split(new char[] { '.' });
                    str11 = str1[1];
                    model.db.gallery_detail gd = new model.db.gallery_detail();
                    gd.rgllary = id;
                    gd.img_url = imgName + "." + str11;
                    if (model.module.GalleryDetail.insert(gd)) { 
                        String rr = HttpContext.Current.Server.MapPath("~");
                        file.SaveAs(rr + "/view/pg_rep/" + imgName + "." + str11);
                    }
                    gId = (int)id;
                }
                else if (mode == "deleteimg")
                {
                    string idAsString = context.Request["id"];
                    int id = int.Parse(idAsString);
                    model.db.gallery_detail gd = model.module.GalleryDetail.getById(id);
                    bool res = model.module.GalleryDetail.delete(gd);
                    gId = (int) gd.rgllary;
                }
                {
                    GalleryModuleAdmin gam = new GalleryModuleAdmin();
                    return gam.generate();
                }
            }
            catch (Exception ex)
            {
                Log.logErr("GallaryModeuleAdmin.executeEvent ", ex);
            }
            return str;
        }
    }
}