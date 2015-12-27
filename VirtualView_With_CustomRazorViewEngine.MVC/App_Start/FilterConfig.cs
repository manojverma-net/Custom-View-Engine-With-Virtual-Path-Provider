using System.Web;
using System.Web.Mvc;

namespace VirtualView_With_CustomRazorViewEngine.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
