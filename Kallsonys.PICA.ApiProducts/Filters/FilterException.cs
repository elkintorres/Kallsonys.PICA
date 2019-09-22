using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;


namespace Kallsonys.PICA.ApiProducts.Filters
{
    public class FilterException: ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is KallsonysException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, "Error message");
            }
            else
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, context.Exception.Message);
            }
            base.OnException(context);
        }
    }
}