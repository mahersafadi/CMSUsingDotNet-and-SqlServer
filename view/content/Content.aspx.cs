using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using model.module;

public partial class view_content_Content : System.Web.UI.Page
{
    private static string specialCategories = "55,56,57,58";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!control.cmsmodules.Security.hasPermission(control.cmsmodules.PermissionName.insert_content.ToString()))
        {
            mvAll.SetActiveView(vwNoPermission);
            return;
        }

        if (!IsPostBack)
        {
            Page.DataBind();
            string _cid = Request.QueryString["__id"];
            if (_cid != null)
            {
                model.module.Content contentToEdit = new model.module.Content();
                Session["currentContent"] = contentToEdit.getContentById(Convert.ToInt32(_cid));
                grdAll.DataSource = contentToEdit.getContentDetailsByContentId(Convert.ToInt32(_cid));
                grdAll.DataBind();
                mvAll.SetActiveView(vwAll);

                showContentInfo();
                showContentForm();

            }
            else
            {
                Session["currentContent"] = null;
            }

            List<model.db.category> c = new model.module.Category().getAllaCategories();
            List<model.db.category> newCats = new List<model.db.category>();
            foreach (model.db.category item in c)
            {
                //if (control.cmsmodules.Security.hasPermission(item.name))
                {
                    item.name = Lang.getByKey(item.name);
                    newCats.Add(item);
                }
            }
            lstCategories.DataSource = newCats;
            lstCategories.DataTextField = "name";
            lstCategories.DataValueField = "_cid";
            lstCategories.DataBind();

            btnSave.Text = btnSaveAll.Text = model.module.Lang.getByKey("ok");

            lstLangiages.DataSource = model.module.Lang.getLanguages();
            lstLangiages.DataTextField = "name";
            lstLangiages.DataValueField = "code";
            lstLangiages.DataBind();

            btnNewContentDetail.Text = model.module.Lang.getByKey("add_new_content_detail");
            saveImage.Text = model.module.Lang.getByKey("upload_image");

        }

    }

    private void showContentInfo()
    {
        imgThumb.Src = "../uploads/content_thumbnails/" + ((model.db.content)Session["currentContent"]).thumbnail;
        lblContentInfo.Text = @"<table class='gridtable'><tr><td>" + model.module.Lang.getByKey("category_name") + ": " + ((model.db.content)Session["currentContent"]).category.name + "  </td><td>" +
                                model.module.Lang.getByKey("create_date") + ": " + Convert.ToDateTime(((model.db.content)Session["currentContent"]).create_date).ToString("yyyy/MM/dd hh:mm tt") + "</td></tr></table>";
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        model.module.Content contentModule = new model.module.Content();
        model.db.content contentDB = new model.db.content();

        string extenssion = "";
        string fileName = "";
        if (!fileUpload1.HasFile && !chkDefaultThumbnail.Checked)
        {
            //extenssion = "jpg";
            //fileName = Server.MapPath("../uploads/content_thumbnails/default_thumbnail.jpg");

            lblImage.Text = Lang.getByKey("select_thumbnail");
            return;
        }
        else
        {
            extenssion = fileUpload1.FileName.Substring(fileUpload1.FileName.LastIndexOf(".") + 1);
        }
        contentDB.__rcategory = Convert.ToInt32(lstCategories.SelectedValue.ToString());
        contentDB.create_date = DateTime.Now;
        contentDB.create_by = ((model.db.user)System.Web.HttpContext.Current.Session["user"])._uid;
        contentModule.insertContent(contentDB);
        if (fileUpload1.HasFile)
            contentDB.thumbnail = contentDB._cid + "_content_thumbnail_" + DateTime.Now.ToString("yyyyMMdd") + "." + extenssion;
        else
            contentDB.thumbnail = "default_thumbnail_" + contentDB.category.name + ".jpg";

        contentModule.updateContent(contentDB);

        string savePath = Server.MapPath("../uploads/content_thumbnails/" + contentDB.thumbnail);

        if (fileUpload1.HasFile)
            fileUpload1.SaveAs(savePath);

        Session["currentContent"] = contentDB;

        insertContentForm(contentDB._cid);
        showContentForm();


        Page.DataBind();
        mvAll.SetActiveView(vwInsertContentDetail);


    }
    protected void btnSaveAll_Click(object sender, EventArgs e)
    {
        model.db.content cnt = (model.db.content)Session["currentContent"];
        model.module.Content contentModule = new model.module.Content();
        if (cnt != null && hfCdId.Value == "")
        {
            model.db.content_detail contentDetailDB = new model.db.content_detail();
            contentDetailDB.__rcontent = Convert.ToInt32(cnt._cid);
            contentDetailDB.title = StringCipher.Encrypt(txtTitle.Text);
            contentDetailDB.text = StringCipher.Encrypt(editor1.InnerText);
            contentDetailDB.create_date = DateTime.Now;
            contentDetailDB.create_by = ((model.db.user)System.Web.HttpContext.Current.Session["user"])._uid;
            contentDetailDB.__rlang = lstLangiages.SelectedValue.ToString();
            contentDetailDB.key_word = collectKeyWords();
            contentDetailDB.no = txtNo.Text;
            if (txtPublishDate.Text.Length > 0)
                contentDetailDB.publish_date = Convert.ToDateTime(txtPublishDate.Text);
            contentModule.insertContentDetail(contentDetailDB);
            imgThumb.Src = "../uploads/content_thumbnails/" + cnt.thumbnail;

            //insertContentForm(cnt._cid);

            mvAll.SetActiveView(vwAll);
            resetContent();
            grdAll.DataSource = contentModule.getContentDetailsByContentId(cnt._cid);
            grdAll.DataBind();
            showContentInfo();
        }
        else
        {
            model.db.content_detail cd = contentModule.getContentDetailById(Convert.ToInt32(hfCdId.Value));
            cd.title = StringCipher.Encrypt(txtTitle.Text);
            cd.text = StringCipher.Encrypt(editor1.InnerText);
            cd.__rlang = lstLangiages.SelectedValue.ToString();
            cd.key_word = collectKeyWords();
            if (txtPublishDate.Text.Length > 0)
                cd.publish_date = Convert.ToDateTime(txtPublishDate.Text);
            cd.no = txtNo.Text;
            contentModule.updateContentDetial(cd);
            //updateContentForm(cd.content._cid);
            if (cnt != null)
            {
                grdAll.DataSource = contentModule.getContentDetailsByContentId(cnt._cid);
                grdAll.DataBind();
                mvAll.SetActiveView(vwAll);
            }
        }
    }
    protected void btnNewContentDetail_Click(object sender, EventArgs e)
    {
        resetContent();
        mvAll.SetActiveView(vwInsertContentDetail);
    }

    private void resetContent()
    {
        txtTitle.Text = null;
        editor1.InnerText = null;
        hfCdId.Value = null;
        txtNo.Text = null;
        txtPublishDate.Text = null;
    }
    protected void grdAll_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {
        model.module.Content.deleteContentDetail(Convert.ToInt32(((LinkButton)sender).CommandArgument));
        grdAll.DataBind();
    }

    protected void lnkEdit_Click(object sender, EventArgs e)
    {
        model.module.Content contentModule = new model.module.Content();
        model.db.content_detail cd = contentModule.getContentDetailById(Convert.ToInt32(((LinkButton)sender).CommandArgument));

        if (cd != null)
        {
            txtTitle.Text = StringCipher.Decrypt(cd.title);
            editor1.InnerText = StringCipher.Decrypt(cd.text);
            lstLangiages.SelectedValue = cd.__rlang;
            //lstLangiages.SelectedItem.Text = cd.lang.name;
            hfCdId.Value = cd._cdid.ToString();
            fillKeyWordTextBoxes(cd.key_word);
            txtPublishDate.Text = cd.publish_date == null ? "" : cd.publish_date.Value.ToString("yyyy/MM/dd");
            txtNo.Text = cd.no;

            showContentForm();
            showContentImages();
            mvAll.SetActiveView(vwInsertContentDetail);
        }

    }

    public bool insertContentForm(int cId)
    {
        try
        {
            model.module.Content c = new model.module.Content();
            model.db.content_form cf = new model.db.content_form();
            cf.display_first_name = chkDisplayFirstName.Checked ? 1 : 0;
            cf.display_last_name = chkDisplayLastName.Checked ? 1 : 0;
            cf.display_email = chkDisplayEamil.Checked ? 1 : 0;
            cf.display_alias_name = chkDisplayAliasName.Checked ? 1 : 0;
            cf.display_address = chkDisplayAddress.Checked ? 1 : 0;
            cf.login_needed = chkLongNeeded.Checked ? 1 : 0;
            cf.review_needed = chkReviewNeeded.Checked ? 1 : 0;
            cf.display_text = chkDisplayText.Checked ? 1 : 0;
            cf.__rcontent = cId;

            return c.insertContentForm(cf);

        }
        catch (Exception ex)
        {
            Log.logErr("view.content.Content.cs.insertContentForm", ex);
        }

        return false;
    }

    public bool updateContentForm(int cId)
    {
        try
        {
            model.module.Content c = new model.module.Content();
            model.db.content_form cf = c.getContentFormByContentId(cId);
            cf.display_first_name = chkDisplayFirstName.Checked ? 1 : 0;
            cf.display_last_name = chkDisplayLastName.Checked ? 1 : 0;
            cf.display_email = chkDisplayEamil.Checked ? 1 : 0;
            cf.display_alias_name = chkDisplayAliasName.Checked ? 1 : 0;
            cf.display_address = chkDisplayAddress.Checked ? 1 : 0;
            cf.login_needed = chkLongNeeded.Checked ? 1 : 0;
            cf.review_needed = chkReviewNeeded.Checked ? 1 : 0;
            cf.display_text = chkDisplayText.Checked ? 1 : 0;

            return c.updateContentForm(cf);

        }
        catch (Exception ex)
        {
            Log.logErr("view.content.Content.cs", ex);
        }

        return false;
    }

    public void showContentForm()
    {
        try
        {
            model.module.Content c = new model.module.Content();
            if (Session["currentContent"] != null)
            {
                model.db.content_form cf = c.getContentFormByContentId(((model.db.content)Session["currentContent"])._cid);

                chkDisplayFirstName.Checked = cf.display_first_name == 1 ? true : false;
                chkDisplayLastName.Checked = cf.display_last_name == 1 ? true : false;
                chkDisplayEamil.Checked = cf.display_email == 1 ? true : false;
                chkDisplayAliasName.Checked = cf.display_alias_name == 1 ? true : false;
                chkDisplayAddress.Checked = cf.display_address == 1 ? true : false;
                chkLongNeeded.Checked = cf.login_needed == 1 ? true : false;
                chkReviewNeeded.Checked = cf.review_needed == 1 ? true : false;
                chkDisplayText.Checked = cf.display_text == 1 ? true : false;
            }

        }
        catch (Exception ex)
        {
            Log.logErr("view.content.Content.cs.showContentForm", ex);
        }

    }

    public string trimHTML(object o)
    {
        try
        {
            string pattern = @"<(.|\n)*?>";
            return Regex.Replace(o.ToString(), pattern, string.Empty);
        }
        catch (Exception ex)
        {
            Log.logErr("COntent.trimHTML", ex);
        }
        return null;
    }

    protected void btnSaveForm_Click(object sender, EventArgs e)
    {
        updateContentForm(((model.db.content)Session["currentContent"])._cid);
    }

    public void showContentImages()
    {
        try
        {
            List<model.db.image> images = model.module.Image.getAllContentImages(((model.db.content)Session["currentContent"])._cid);
            contentImages.InnerHtml = "";
            if (images != null)
            {
                foreach (model.db.image image in images)
                {

                    contentImages.InnerHtml += @"<table id='tbl_" + image._iid + @"'><tr>
                                                <td>
                                                    <a title='" + image.description + "' target='_blank' href='../uploads/content_images/" + image.__rcontent + "/" + image.name + @"' >
                                                        <img style='width:100px;height:100px;'
                                                        src='../uploads/content_images/" + image.__rcontent + "/" + image.name + @"' 
                                                        title='" + image.description + "' alt='" + image.description + @"' />
                                                    </a>
                                                </td>
                                                <td style='vertical-align:top;'>
                                                    <img title='delete'  alt='delete' onclick=Content.delete_content_image('" + image._iid + @"') src='../images/icons/deletered.png' style='width:24px;height:24px;margin-left:-20px;' />
                                                </td>";
                }
            }
        }
        catch (Exception ex)
        {
            Log.logErr("view.Content.showContentImages", ex);
        }
    }

    protected void saveImage_Click(object sender, EventArgs e)
    {
        model.db.image img = new model.db.image();
        img.name = "img_" + DateTime.Now.ToString("yyyyMMddhhmmss") + "." + fileUpload2.FileName.Substring(fileUpload2.FileName.LastIndexOf(".") + 1);
        img.create_date = DateTime.Now;
        img.create_by = ((model.db.user)System.Web.HttpContext.Current.Session["user"])._uid;
        img.description = txtImageDesc.Text;
        img.__rcontent = ((model.db.content)Session["currentContent"])._cid;

        string pathString = Server.MapPath("../uploads/content_images/" + img.__rcontent);
        if (!System.IO.Directory.Exists(pathString))
            System.IO.Directory.CreateDirectory(pathString);

        string savePath = Server.MapPath("../uploads/content_images/" + img.__rcontent + "/" + img.name);

        if (model.module.Image.insertImage(img))
            fileUpload2.SaveAs(savePath);
        showContentImages();

    }

    private string collectKeyWords()
    {
        string res = "";
        if (txtKeyWord1.Text.Trim() != "")
            res += txtKeyWord1.Text + "##";
        if (txtKeyWord2.Text.Trim() != "")
            res += txtKeyWord2.Text + "##";
        if (txtKeyWord3.Text.Trim() != "")
            res += txtKeyWord3.Text + "##";
        if (txtKeyWord4.Text.Trim() != "")
            res += txtKeyWord4.Text + "##";
        if (txtKeyWord5.Text.Trim() != "")
            res += txtKeyWord5.Text + "##";
        if (txtKeyWord6.Text.Trim() != "")
            res += txtKeyWord6.Text + "##";
        if (txtKeyWord7.Text.Trim() != "")
            res += txtKeyWord7.Text + "##";
        if (txtKeyWord8.Text.Trim() != "")
            res += txtKeyWord8.Text + "##";
        if (txtKeyWord9.Text.Trim() != "")
            res += txtKeyWord9.Text + "##";
        if (txtKeyWord10.Text.Trim() != "")
            res += txtKeyWord10.Text + "##";

        res += Lang.getByKey("default_key_words");

        if (res.EndsWith("##"))
            res = res.Substring(0, res.Length - 2);

        return res;
    }

    private void fillKeyWordTextBoxes(string str)
    {
        try
        {
            txtKeyWord1.Text =
            txtKeyWord2.Text =
            txtKeyWord3.Text =
            txtKeyWord4.Text =
            txtKeyWord5.Text =
            txtKeyWord6.Text =
            txtKeyWord7.Text =
            txtKeyWord8.Text =
            txtKeyWord9.Text =
            txtKeyWord10.Text = "";


            str = str.Replace(Lang.getByKey("default_key_words"), "");

            string[] result = str.Split(new string[] { "##" }, StringSplitOptions.None);
            try { txtKeyWord1.Text = result[0]; }
            catch { }
            try { txtKeyWord2.Text = result[1]; }
            catch { }
            try { txtKeyWord3.Text = result[2]; }
            catch { }
            try { txtKeyWord4.Text = result[3]; }
            catch { }
            try { txtKeyWord5.Text = result[4]; }
            catch { }
            try { txtKeyWord6.Text = result[5]; }
            catch { }
            try { txtKeyWord7.Text = result[6]; }
            catch { }
            try { txtKeyWord8.Text = result[7]; }
            catch { }
            try { txtKeyWord9.Text = result[8]; }
            catch { }
            try { txtKeyWord10.Text = result[9]; }
            catch { }
        }
        catch { }
    }

    public Boolean isVisible()
    {

        string[] arr = specialCategories.Split(',');

        foreach (var item in arr)
        {
            if (lstCategories.SelectedValue.ToString() == item)
                return true;
        }
        return false;
    }
}