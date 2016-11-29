using System.Web;
using System.Web.Mvc;
using WebGIS.App_Start;

namespace WebGIS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add((new AddCustomHeaderFilter()));
        }
    }
}
