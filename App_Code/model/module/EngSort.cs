using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using model.db;

/// <summary>
/// Summary description for EngSort
/// </summary>
/// 
namespace model.module
{
    public class EngSort
    {
        public EngSort()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static List<model.db.eng_sort> searchEngineers(string name, string specification, string city, string ministry, int fromId = 0)
        {
            try
            {
                CMSDataContext db = new CMSDataContext();
                var qry = db.eng_sorts.Where(e => e.name.Contains(name)
                                                && e.specification.Contains(specification)
                                                && e.city.Contains(city)
                                                && e.ministry.Contains(ministry)
                                                && e._eid > fromId).Take(20);
                return qry.ToList();
            }
            catch (Exception ex)
            {
                Log.logErr("model.module.searchEngineers", ex);
            }
            return null;
        }
    }
}