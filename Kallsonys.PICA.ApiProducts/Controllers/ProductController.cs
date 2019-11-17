using Kallsonys.PICA.Application.DTO.ProductDTO;
using Kallsonys.PICA.Application.IServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace Kallsonys.PICA.ApiProducts.Controllers
{
    [RoutePrefix("v1/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductService Service;

        public ProductController(IProductService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Recurso para la creacion de productos - /Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>MultipleProductPost</returns>
        [ResponseType(typeof(Int32))]
        [HttpPost]
        [Route("")]
        public virtual async Task<IHttpActionResult> PostBase(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.CreateAsync(product, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

        /// <summary>
        /// Recurso para la consulta de un producto por id - /id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MultipleProductIdGet</returns>
        [ResponseType(typeof(Product))]
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

        /// <summary>
        /// Recurso para la consulta de productos por nombre o por descripción - /byCriteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>MultipleProductByCriteriaGet</returns>
        [ResponseType(typeof(IList<Product>))]
        [HttpGet]
        [Route("byCriteria")]
        public virtual async Task<IHttpActionResult> GetByCriteriaBase([FromUri] string criteria, int pageCount = 10)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.GetByCriteriaAsync(criteria, pageCount, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);

        }

        /// <summary>
        /// Recurso para la consulta del top 5 de productos - /topFive
        /// </summary>
        /// <param name="topfiveproducts"></param>
        /// <returns>MultipleProductTopFiveGet</returns>
        [ResponseType(typeof(IList<Product>))]
        [HttpGet]
        [Route("topFive")]
        public virtual async Task<IHttpActionResult> GetTopFiveBase([FromUri] IList<Int32> topfiveproducts)
        {
            if (topfiveproducts.Count() > 5)
                return BadRequest("Cantidad máxima de consulta es 5 productos ");

            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.GetTopFiveAsync(topfiveproducts, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

    }
}
