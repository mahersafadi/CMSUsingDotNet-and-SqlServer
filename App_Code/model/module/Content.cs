using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;
/// <summary>
/// Summary description for Content
/// </summary>
namespace model.module
{
    public class Content
    {
        public Content()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public List<content> getAllContents()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = from c in db.contents
                          orderby c.create_date
                          select c;

                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getAllContents", ex);
            }
            return null;
        }

        public content getContentById(int _id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                return db.contents.First(c => c._cid == _id);
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentById", ex);
                return null;
            }
        }

        public List<content> getLastXContent(int num, Int32 fromId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();

                if (fromId == 0)
                {
                    var qry = db.contents
                         .Where(m => m._cid > fromId)
                         .OrderByDescending(m => m.create_date)
                         .Take(num);
                    return qry.ToList();
                }
                else
                {
                    var qry = db.contents
                         .Where(m => m._cid < fromId)
                         .OrderByDescending(m => m.create_date)
                         .Take(num);
                    return qry.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getLastXContent", ex);
            }
            return null;
        }

        public List<content> getLastXContents(int catId, int num, int? ignoreContent = null)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.contents
                    .Where(c => c.__rcategory == catId && c._cid != ignoreContent)
                     .OrderByDescending(m => m.create_date)
                     .Take(num);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getLastXContent", ex);
            }
            return null;
        }


        public static List<content> getAllConetents()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.contents;
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getAllConetentInCategory", ex);
                return null;
            }
        }



        public List<content> getAllConetentsInCategory(int catId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.contents.Where(c => c.category._cid == catId || catId == 0).OrderBy(c => c.create_by);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getAllConetentInCategory", ex);
                return null;
            }
        }

        public content getFirstContentInCategory(int catId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content cont = db.contents.First(c => c.category._cid == catId);
                return cont;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getFirstContentInCategory", ex);
            }
            return null;
        }

        public static List<content> getFirstXContentInCategory(int categoryId, int num)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();

                if (num != 0)
                {
                    var allContents = (from c in db.contents
                                                     where c.__rcategory == categoryId
                                                     orderby c.create_date ascending
                                                     select c).Take(num);
                    return allContents.ToList();
                }
                else {
                    var allContents = (from c in db.contents
                                                     where c.__rcategory == categoryId
                                                     orderby c.create_date
                                                     select c);
                    return allContents.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getFirstXContentInCategory", ex);
            }
            return null;
        }

        public static List<content> getLastXContentInCategory(int categoryId, int num)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var allContents = (from c in db.contents
                                   where c.__rcategory == categoryId
                                   orderby c.create_date descending
                                   select c).Take(num);
                return allContents.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getFirstXContentInCategory", ex);
            }
            return null;
        }

        public content getLastContentInCategory(int catId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content cont = db.contents.OrderBy(c => c.create_date).Last(c => c.category._cid == catId);
                return cont;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getLastContentInCategory", ex);
            }
            return null;
        }

        public List<content> getLastXContentInCategory(int categoryId, int num, Int32 fromId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();

                if (fromId == 0)
                {
                    var qry = db.contents
                         .Where(m => m.category._cid == categoryId && m._cid > fromId)
                         .OrderByDescending(m => m.create_date)
                         .Take(num);
                    return qry.ToList();
                }
                else
                {
                    var qry = db.contents
                         .Where(m => m.category._cid == categoryId && m._cid < fromId)
                         .OrderByDescending(m => m.create_date)
                         .Take(num);
                    return qry.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getLastXContentInCategory", ex);
            }
            return null;
        }

        public int insertContent(content cnt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                cnt.create_date = DateTime.Now;
                db.contents.InsertOnSubmit(cnt);
                db.SubmitChanges();
                return cnt._cid;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.insertContent", ex);
                return 0;
            }
        }

        public bool updateContent(content cnt)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                //db.contents.Attach(c);
                content c = db.contents.Single(cc => cc._cid == cnt._cid);
                c.thumbnail = cnt.thumbnail;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.updateContent", ex);
                return false;
            }
        }

        public bool deleteContent(int cid)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                //Delete Content Details:
                //Maher: Delete imge file must be done, it is not implemented yet
                //---------------------------------------
                List<content_detail> details = db.content_details.Where(dd => dd.__rcontent == cid).ToList();
                if (details != null)
                {
                    for (int i = 0; i < details.Count(); i++)
                        db.content_details.DeleteOnSubmit(details[i]);
                }
                List<content_extra> extras = db.content_extras.Where(e => e.content_id == cid).ToList();
                if (extras != null)
                {
                    for (int i = 0; i < extras.Count(); i++)
                        db.content_extras.DeleteOnSubmit(extras[i]);
                }
                //---------------------------------------
                content content = db.contents.First(c => c._cid == cid);
                db.contents.DeleteOnSubmit(content);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Content.deleteContent", ex);
            }
            return false;
        }


        public static bool deleteContentDetail(int cdId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_detail cd = db.content_details.First(c => c._cdid == cdId);
                db.content_details.DeleteOnSubmit(cd);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Content.deleteContentDetail", ex);
            }
            return false;
        }

        public bool deleteContentDetails(int cid)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.content_details.Where(c => c.__rcontent == cid);
                foreach (var item in qry)
                {
                    db.content_details.DeleteOnSubmit(item);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.Content.deleteContentDetails", ex);
            }
            return false;
        }

        public List<content_detail> getContentDetailsByContentId(int cntId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.content_details.Where(c => c.__rcontent.Value == cntId);
                foreach (var item in qry)
                {
                    item.title = StringCipher.Decrypt(item.title);
                    item.text = StringCipher.Decrypt(item.text);
                }
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentDetailsByContentId", ex);
            }
            return null;
        }

        public List<content_detail> getContentDetailsByKeyWords(int num, int fromId, string keyWord)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                if (fromId == 0)
                {
                    var qry = db.content_details.Where(c => c.key_word.Contains(keyWord) && c.__rcontent > fromId).
                        OrderByDescending(m => m.create_date).
                        Take(num);
                    foreach (var item in qry)
                    {
                        item.title = StringCipher.Decrypt(item.title);
                        item.text = StringCipher.Decrypt(item.text);
                    }
                    return qry.ToList();
                }
                else
                {
                    var qry = db.content_details
                         .Where(m => m.__rcontent < fromId && m.key_word.Contains(keyWord))
                         .OrderByDescending(m => m.create_date)
                         .Take(num);
                    foreach (var item in qry)
                    {
                        item.title = StringCipher.Decrypt(item.title);
                        item.text = StringCipher.Decrypt(item.text);
                    }
                    return qry.ToList();
                }
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentDetailsByKeyWords", ex);
            }
            return null;
        }

        public List<content_detail> getContentDetailsByContentIdAndKeyWord(int cId, string keyWord)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.content_details.Where(c => (c.key_word.Contains(keyWord) || keyWord == "") && c.__rcontent == cId);
                foreach (var item in qry)
                {
                    item.title = StringCipher.Decrypt(item.title);
                    item.text = StringCipher.Decrypt(item.text);
                }
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentDetailsByContentIdAndKeyWord", ex);
            }
            return null;
        }

        public content_detail getContentDetail(int contentId, string lang)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_detail qry = db.content_details.First(c => c.__rcontent == contentId && c.__rlang == lang);
                qry.title = StringCipher.Decrypt(qry.title);
                qry.text = StringCipher.Decrypt(qry.text);
                return qry;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentDetail", ex);
                return null;
            }
        }

        public content_detail getContentDetailById(int cdId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_detail qry = db.content_details.First(c => c._cdid == cdId);
                qry.title = StringCipher.Decrypt(qry.title);
                qry.text = StringCipher.Decrypt(qry.text);
                return qry;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentDetailById", ex);
            }
            return null;
        }

        public bool insertContentDetail(content_detail cd)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.content_details.InsertOnSubmit(cd);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.insertContentDetail", ex);
                return false;
            }
        }

        public bool updateContentDetial(content_detail cd)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                //db.content_details.Attach(cd);
                content_detail c = db.content_details.Single(cc => cc._cdid == cd._cdid);
                c.title = cd.title;
                c.text = cd.text;
                c.__rlang = cd.__rlang;
                c.key_word = cd.key_word;
                c.publish_date = cd.publish_date;
                c.no = cd.no;
                c.create_by = cd.create_by;
                c.create_date = cd.create_date;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.updateContentDetial", ex);
                return false;
            }
        }

        public List<content_detail> searchContentDetails(int catId, string title)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.content_details.Where(c => c.content.__rcategory == catId && c.title.Contains(title)).OrderBy(c => c.create_date);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.searchContentDetails", ex);
                return null;
            }
        }
        #region ContentExtra
        public int insertContentExtra(string key, string value, int content)
        {
            model.db.content_extra ce = new model.db.content_extra ();
            ce._key = key;
            ce._val = value;
            ce.status = 1;
            ce.content_id = content;
            return this.insertContentExtra(ce);
        }
        public string getContentExtraValueByKey(int contentId, string key)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_extra ce = db.content_extras.Single(cc => cc._key == key && cc.content_id == contentId);
                return ce._val;
            }
            catch (Exception ex)
            {
                Log.logErr("An Error accured during try to insert content extra", ex);
            }
            return "";
        }
        public int insertContentExtra(model.db.content_extra ce)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.content_extras.InsertOnSubmit(ce);
                db.SubmitChanges();
                return ce.ce_id;
            }
            catch (Exception ex)
            {
                Log.logErr("An Error accured during try to insert content extra", ex);
                return -1;
            }
        }

        public bool removeContentExtraByContentId(int id)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                List<model.db.content_extra> etraToRemove = db.content_extras.Where(cc => cc.content_id == id).ToList();
                foreach (var item in etraToRemove)
                {
                    db.content_extras.DeleteOnSubmit(item);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Faced an error durig try to remove content extra by content id ["+id+"]", ex);
            }
            return false;
        }
        public List<model.db.content_extra> getContentExtraByContentId(int id){
            try{
                CMSDataContext db = new CMSDataContext();
                var query = db.content_extras.Where(ce => ce.content_id == id);
                return query.ToList();
            }
            catch(Exception ex){
                Log.logErr("an error accured during try to get content ["+id+"] extra items ", ex);
            }
            return null;
        }
        #endregion


        #region ContentForm
        public content_form getContentFormByContentId(int cId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                return db.content_forms.First(f => f.__rcontent == cId);
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentFormByContentId", ex);
            }
            return null;
        }

        public bool insertContentForm(model.db.content_form cf)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.content_forms.InsertOnSubmit(cf);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("COntent.insertContentForm", ex);
            }
            return false;
        }

        public bool updateContentForm(content_form cf)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_form c = db.content_forms.Single(cc => cc._cfid == cf._cfid);
                c.display_first_name = cf.display_first_name;
                c.display_last_name = cf.display_last_name;
                c.display_alias_name = cf.display_alias_name;
                c.display_email = cf.display_email;
                c.display_address = cf.display_address;
                c.login_needed = cf.login_needed;
                c.review_needed = cf.review_needed;
                c.create_date = DateTime.Now;
                c.display_text = cf.display_text;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.updateContentForm", ex);
                return false;
            }
        }

        public static bool insertContentFormResult(model.db.content_form_result cfr)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.content_form_results.InsertOnSubmit(cfr);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("COntent.insertContentFormResult", ex);
            }
            return false;
        }

        public bool deleteContentForm(int cid)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.content_forms.Where(c => c.__rcontent == cid);
                foreach (var item in qry)
                {
                    db.content_forms.DeleteOnSubmit(item);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.deleteContentForm", ex);
            }
            return false;
        }


        public static content_form getContentFormByContentId(int? cId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                content_form qry = db.content_forms.First(c => c.__rcontent == cId);
                return qry;

            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentFormsByContentId", ex);
            }
            return null;
        }

        public static List<content_form_result> getContentFormResultsByContentId(int? cId)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.content_form_results.Where(c => c.__rcontent == cId).OrderBy(c => c.create_date);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("Content.getContentFormsByContentId", ex);
            }
            return null;
        }

        public bool deleteContentFormResult(int cid)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.content_form_results.Where(cf => cf.__rcontent == cid);
                foreach (var item in qry)
                {
                    db.content_form_results.DeleteOnSubmit(item);
                }
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("Content.deleteContentFormResult", ex);
            }
            return false;
        }

        #endregion


    }
}