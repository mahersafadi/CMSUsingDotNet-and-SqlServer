using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for Visitor
/// </summary>
/// 
namespace model.module
{
    public class Visitor
    {
        public static string currentIpAddress = "";
        public Visitor()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        //**********get number of visitors for content***************************
        public static int getContentVisitorNumber(int cid)
        {
            try
            {
                CMSDataContext pm = new CMSDataContext();
                return pm.visitors.Where(v => v.__rcontent == cid).Count();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.getContentVisitorNumber", ex);
            }
            return 0;
        }


        public static bool insertVisitor(model.db.visitor visitor)
        {
            try
            {
                CMSDataContext pm = new CMSDataContext();
                pm.visitors.InsertOnSubmit(visitor);
                pm.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.insertVisitor", ex);
            }
            return false;

        }

        public static bool deleteVisitorByContentId(int contentId)
        {
            try
            {
                CMSDataContext pm = new CMSDataContext();
                var qry = pm.visitors.Where(v => v.__rcontent == contentId);
                foreach (var item in qry)
                {
                    pm.visitors.DeleteOnSubmit(item);
                }
                pm.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.deleteVisitorByContentId", ex);
            }
            return false;
        }

    }
}