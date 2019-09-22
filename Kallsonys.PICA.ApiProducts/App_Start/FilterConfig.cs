using Kallsonys.PICA.ApiProducts.Filters;
using System.Web;
using System.Web.Mvc;

namespace Kallsonys.PICA.ApiProducts
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
        
            filters.Add(new HandleErrorAttribute());
        }
    }
}
