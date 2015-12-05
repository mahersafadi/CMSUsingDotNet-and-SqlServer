using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using control.cmsmodules;

/// <summary>
/// Summary description for SearchModule
/// </summary>
public class SearchModule : Module
{
    public SearchModule()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public SearchModule(model.db.module m)
    {
        this.module = m;
        mdAsDictionary = model.module.ModuleInModelLayer.generateModuleDetailAsDictionary(this.module._mid);
    }

    public override string generate()
    {
        try
        {
            string res = @"<div class='top-search'>";
                //<input type='submit' id='search_btn' onclick='Search.do_search()' value='' /><input type='text' style='width:" + mdAsDictionary["input_text_width"] + @"' id='things_to_search' />
            res += @"<table><tr>
                                <td>
                                    <input type='submit' id='search_btn' onclick='Search.do_search()' value='' />
                                </td>
                                <td style='vertical-align:top;'>
                                    <input type='text' style='width:" + mdAsDictionary["input_text_width"] + @"' id='things_to_search' />
                                </td>
                            </tr></table>";
            res +="</div>";
            return res;
        }
        catch (Exception ex)
        {
            Log.logErr("", ex);
        }
        return "";
    }


    public string generateSearchEngineResult(string str)
    {
        try
        {

            ContentModule cm = new ContentModule();
            string res = cm.generateXContents( 4, 1, str);
            return res;
        }
        catch (Exception ex)
        {
            Log.logErr("contro.cmsmodules.EngSortModule.generateSearchEngineResult", ex);
        }
        return "";
    }


    public static string searchHandler(string mode, string str)
    {
        string res = "";
        try
        {
            ContentModule c= new ContentModule();
            switch (mode)
            {
                case "1":
                    res = c.generateXContents(10, 0, str);
                    break;
                default:
                    break;
            }
        }
        catch (Exception ex)
        {
            Log.logErr("control.cmsmodules.SearchModule.searchHandler", ex);
        }
        return res;
    }
}