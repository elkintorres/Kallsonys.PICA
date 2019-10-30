﻿using Kallsonys.PICA.Application.DTO.OfferDTO;
using Kallsonys.PICA.Application.IServices;
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

        [ResponseType(typeof(SingleOfferPost))]
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> PostBase(Offer offer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CancellationTokenSource token = new CancellationTokenSource();
            var result = await Service.CreateAsync(offer, token);
            SingleOfferPost response = new SingleOfferPost()
            {
                Error = null,
                IdOffer = result.Id
            };
            return Ok(response);
        }

        [ResponseType(typeof(MultipleOfferGet))]
        [HttpGet]
        [Route("")]
        public virtual async Task<IHttpActionResult> Get()
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var result = await Service.GetAsync(token);
            MultipleOfferGet response = new MultipleOfferGet()
            {
                Error = null,
                Offers = result
            };
            return Ok(response);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(SingleOfferById))]
        [HttpGet]
        [Route("id")]
        public virtual async Task<IHttpActionResult> GetBase([FromUri] int id)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var result = await Service.GetByIdAsync(id, token);
            SingleOfferById response = new SingleOfferById()
            {
                Error = null,
                Offer = result
            };
            return Ok(response);
        }
    }
}