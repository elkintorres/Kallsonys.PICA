using Kallsonys.PICA.Application;
using System;
using System.Web;
using System.Web.Http;

namespace Kallsonys.PICA.ApiProducts
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            IoC.RegistrarTipos(GlobalConfiguration.Configuration);
        }
    }
}
