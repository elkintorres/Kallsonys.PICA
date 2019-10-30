using Kallsonys.PICA.ApiProducts.Filters;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Kallsonys.PICA.ApiProducts
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new FilterException());

            config.Formatters.Add(new RAML.Api.Core.XmlSerializerFormatter());
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}