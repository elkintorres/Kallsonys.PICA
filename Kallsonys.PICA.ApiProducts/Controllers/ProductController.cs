using Kallsonys.PICA.Application.DTO.ProductDTO;
using Kallsonys.PICA.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [ResponseType(typeof(MultipleProductPost))]
        [HttpPost]
        [Route("")]
        public virtual async Task<IHttpActionResult> PostBase(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            CancellationTokenSource token = new CancellationTokenSource();
            var result = await this.Service.CreateAsync(product, token);
            MultipleProductPost response = new MultipleProductPost()
            {
                Error = null,
                IdProduct = result
            };
            return Ok(response);
        }

        /// <summary>
        /// Recurso para la consulta de un producto por id - /id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MultipleProductIdGet</returns>
        [ResponseType(typeof(MultipleProductIdGet))]
        [HttpGet]
        [Route("id")]
        public virtual async Task<IHttpActionResult> GetBase([FromUri] int id)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var result = await this.Service.GetByIdAsync(id, token);
            MultipleProductIdGet response = new MultipleProductIdGet()
            {
                Error = null,
                Product = result
            };
            return Ok(response);
        }

        /// <summary>
        /// Recurso para la consulta de productos por nombre o por descripción - /byCriteria
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>MultipleProductByCriteriaGet</returns>
        [ResponseType(typeof(MultipleProductByCriteriaGet))]
        [HttpGet]
        [Route("byCriteria")]
        public virtual async Task<IHttpActionResult> GetByCriteriaBase([FromUri] string criteria)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var result = await this.Service.GetByCriteriaAsync(criteria, token);
            MultipleProductByCriteriaGet response = new MultipleProductByCriteriaGet()
            {
                Error = null,
                Product = result
            };
            return Ok(response);
        }

        /// <summary>
        /// Recurso para la consulta del top 5 de productos - /topFive
        /// </summary>
        /// <param name="topfiveproducts"></param>
        /// <returns>MultipleProductTopFiveGet</returns>
        [ResponseType(typeof(MultipleProductTopFiveGet))]
        [HttpGet]
        [Route("topFive")]
        public virtual async Task<IHttpActionResult> GetTopFiveBase([FromUri] IList<string> topfiveproducts)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            IList<Int32> topFive = topfiveproducts.Select(x => Convert.ToInt32(x)).ToList();
            var result = await this.Service.GetTopFiveAsync(topFive, token);
            MultipleProductTopFiveGet response = new MultipleProductTopFiveGet()
            {
                Error = null,
                Product = result
            };
            return Ok(response);
        }

    }
}
