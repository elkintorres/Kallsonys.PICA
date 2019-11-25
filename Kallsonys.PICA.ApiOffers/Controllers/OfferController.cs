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
        [Route("GetActive/{pageSize:int:min(1)}/{pageIndex:int:min(1)}")]
        public virtual async Task<IHttpActionResult> GetActive(int pageSize, int pageIndex)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var registers = await Service.GetActiveOffers(pageSize, pageIndex, token);
            var countRegisters = await Service.GetCountAll(token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(registers), Encoding.UTF8, "application/json");
            response.Headers.Add("X-Total-Count", countRegisters.ToString());

            return ResponseMessage(response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Offer))]
        [HttpGet]
        [Route("GetById/{id:int:min(1)}")]
        public virtual async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.GetByIdAsync(id, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

        /// <summary>
        /// Recurso para la consulta de productos por nombre o por descripción - /byCriteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>MultipleProductByCriteriaGet</returns>
        [ResponseType(typeof(IList<Offer>))]
        [HttpGet]
        [Route("GetByCriteria/{criteria:length(4,50)}/{pageSize:int:min(1)}/{pageIndex:int:min(1)}")]
        public virtual async Task<IHttpActionResult> GetByCriteria([FromUri] string criteria, int pageSize = 100, int pageIndex = 0)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.GetByCriteriaAsync(criteria, pageSize, pageIndex, token);
            var countRegisters = await Service.GetCountAll(token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            response.Headers.Add("X-Total-Count", countRegisters.ToString());

            return ResponseMessage(response);
        }

        [ResponseType(typeof(Boolean))]
        [HttpDelete]
        [Route("DisableById/{id}")]
        public virtual async Task<IHttpActionResult> DisableById([FromUri] int id)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.DisableById(id, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }
    }
}