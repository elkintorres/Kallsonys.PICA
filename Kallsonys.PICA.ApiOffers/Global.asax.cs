using Kallsonys.PICA.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace Kallsonys.PICA.ApiOffers
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
