using Solicita.TG.UI.View.Filters;
using System.Web;
using System.Web.Mvc;

namespace Solicita.TG.UI.View
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new MyFaultExceptionHandler());
        }
    }
}