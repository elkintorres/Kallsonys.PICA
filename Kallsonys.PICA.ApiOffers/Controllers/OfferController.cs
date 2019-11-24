using Kallsonys.PICA.Application.DTO.OfferDTO;
using Kallsonys.PICA.Application.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Kallsonys.PICA.ApiOffers.Controllers
{
    [System.Web.Http.RoutePrefix("v1/Offer")]
    public class OfferController : ApiController
    {
        private readonly IOfferService Service;

        public OfferController(IOfferService service)
        {
            this.Service = service;
        }

        [ResponseType(typeof(Int32))]
        [HttpPost]
        [Route("")]
        public virtual async Task<IHttpActionResult> Post(Offer offer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.CreateAsync(offer, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

        [ResponseType(typeof(IList<Offer>))]
        [HttpGet]
        [Route("GetActiveOffers/{pageSize:int:min(1)}/{pageIndex:int:min(1)}")]
        public virtual async Task<IHttpActionResult> GetActiveOffers(int pageSize, int pageIndex)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var registers = await Service.GetActiveOffers(pageSize, pageIndex, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(registers), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Offer))]
        [HttpGet]
        [Route("id")]
        public virtual async Task<IHttpActionResult> GetBase([FromUri] int id)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.GetByIdAsync(id, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }
    }
}