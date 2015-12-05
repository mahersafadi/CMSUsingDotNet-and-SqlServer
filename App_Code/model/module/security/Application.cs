using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for application
/// </summary>
/// 
namespace model.module.security
{
    public class Application
    {
        public Application()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.application> getAllApplications()
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.applications.OrderBy(a => a.app_name);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Application.getAllApplications", ex);
            }
            return null;
        }

        public model.db.application getApplicationById(int id)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                application app = db.applications.First(a => a._app_id == id);
                return app;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Application.getApplicationById", ex);
            }
            return null;
        }

        public model.db.application getApplicationByName(string appName)
        {
            try
            {
                model.db.CMSDataContext db = new db.CMSDataContext();
                application app = db.applications.First(a => a.app_name == appName);
                return app;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.moduel.Application", ex);
            }
            return null;
        }

        public static bool insertApplication(model.db.application application)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                db.applications.InsertOnSubmit(application);
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.module.securityApplication.insertApplication", ex);
            }
            return false;
        }

        public static bool updateApplication(model.db.application application)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                model.db.application app = db.applications.Single(a => a._app_id == application._app_id);
                app.is_active = application.is_active;
                app.app_name = application.app_name;
                db.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.security.Application.activateApplication", ex);
            }
            return false;
        }
    }
}