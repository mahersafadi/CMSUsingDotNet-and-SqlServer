using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ModuleFactory
/// </summary>
namespace control.cmsmodules{
public class ModuleFactory
{
	public ModuleFactory()
	{
    }
		public static control.cmsmodules.Module createModule(int number){
            control.cmsmodules.Module module = null;
            switch(number){
                case 1:
                    break;
                case 21:
                    module = new control.cmsmodules.bnp.OfferModule();
                    break;
                case 22:
                    module = new control.cmsmodules.bnp.BnpLatestNewsModule();
                    break;
            }
            return module;
        }
	}
}