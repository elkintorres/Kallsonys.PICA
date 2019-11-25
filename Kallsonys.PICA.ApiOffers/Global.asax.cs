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

        protected void Application_PreSendRequestHeaders()
        {
            // removing excessive headers. They don't need to see this.
            Response.Headers.Remove("Server");
            Response.Headers.Remove("X-Powered-By");
        }

    }
}
