using Kallsonys.PICA.Application.DTO.ProductDTO;
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
        public virtual async Task<IHttpActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.CreateAsync(product, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

        [ResponseType(typeof(Product))]
        [HttpGet]
        [Route("GetAll/{pageSize:int:min(1)}/{pageIndex:int:min(1)}")]
        public virtual async Task<IHttpActionResult> GetAll([FromUri] int pageSize = 100, int pageIndex = 0)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.GetByAll(pageSize, pageIndex, token);
            var countRegisters = await Service.GetCountAll(token);
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");

            response.Headers.Add("X-Total-Count", countRegisters.ToString());
            return ResponseMessage(response);
        }

        /// <summary>
        /// Recurso para la consulta de un producto por id - /id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MultipleProductIdGet</returns>
        [ResponseType(typeof(Product))]
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
        /// Recurso para la consulta de un producto por id - /id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MultipleProductIdGet</returns>
        [ResponseType(typeof(IList<Product>))]
        [HttpGet]
        [Route("GetByCode/{code:length(3,50)}/{pageSize:int:min(1)}/{pageIndex:int:min(1)}")]
        public virtual async Task<IHttpActionResult> GetByCode([FromUri] string code,int pageSize = 100, int pageIndex = 0)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var registers = await Service.GetByCodeAsync(code, pageSize, pageIndex, token);
            var countRegisters = await Service.GetCountAll(token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(registers), Encoding.UTF8, "application/json");
            response.Headers.Add("X-Total-Count", countRegisters.ToString());

            return ResponseMessage(response);
        }

        /// <summary>
        /// Recurso para la consulta de productos por nombre o por descripción - /byCriteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>MultipleProductByCriteriaGet</returns>
        [ResponseType(typeof(IList<Product>))]
        [HttpGet]
        [Route("GetByCriteria/{criterialength(4,50)}/{pageSize:int:min(1)}/{pageIndex:int:min(1)}")]
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

        /// <summary>
        /// Recurso para la consulta del top 5 de productos - /topFive
        /// </summary>
        /// <param name="topfiveproducts"></param>
        /// <returns>MultipleProductTopFiveGet</returns>
        [ResponseType(typeof(IList<Product>))]
        [HttpGet]
        [Route("GetAllByIds")]
        public virtual async Task<IHttpActionResult> GetAllByIds([FromUri] IList<Int32> ids)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var register = await Service.GetAllByIdsAsync(ids, token);

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            return ResponseMessage(response);
        }

    }
}
