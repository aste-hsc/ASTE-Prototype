using System.Web;
using System.Web.Mvc;

namespace ASTE.Modules.FormModule
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
