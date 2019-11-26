using Kallsonys.PICA.Application.DTO.CommonDTO;
using Kallsonys.PICA.CrossCutting.Configuration.Exceptions;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http.Filters;

namespace Kallsonys.PICA.ApiOffers.Filters
{
    public class OfferFilterException : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            string error = string.Empty;
            int code = 0;

            if (context.Exception.GetType() == typeof(InfraestructureExcepcion))
            {
                error = context.Exception.Message;
                code = 100;
            }
            else if (context.Exception.GetType() == typeof(BussinesException))
            {
                error = context.Exception.Message;
                code = 200;
            }
            else if (context.Exception.GetType() == typeof(KallsonysException))
            {
                error = MessagesApplication.ErrorApplication;
                code = 300;
            }
            else
            {
                error = MessagesApplication.ErrorNoControlado;
                code = 400;
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