using Kallsonys.PICA.Application.DTO.CommonDTO;
using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace Kallsonys.PICA.ApiProducts.Filters
{
    public class ProductFilterException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            //var controller = context.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            //var action = context.ActionContext.ActionDescriptor.ActionName;

            string error = string.Empty;
            int code = 0;

            if (context.Exception.GetType() == typeof(InfraestructureExcepcion))
            {
                error = MessagesInfraestructure.ErrorBD;
                code = 100;
            }
            else if (context.Exception.GetType() == typeof(KallsonysException))
            {
                error = MessagesApplication.ErrorApplication;
                code = 200;
            }
            else
            {
                error = MessagesApplication.ErrorNoControlado;
                code = 200;
            }

            Error errorApp = new Error()
            {
                Code = code,
                Description = error
            };

            var response = context.Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(errorApp), Encoding.UTF8, "application/json");

            context.Response = response;
            base.OnException(context);

        }

    }
}