// Template: Controller Implementation (ApiControllerImplementation.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Kallsonys.PICA.ApiProducts.Product.Models;
using Kallsonys.PICA.Application.IServices;

namespace Kallsonys.PICA.ApiProducts.Product
{
    public partial class ProductController : IProductController
    {

        private readonly IProductService Service;
        public ProductController(IProductService service)
        {
            this.Service = service;
        }


        /// <summary>
        /// Recurso para la creacion de productos - /product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>MultipleProductPost</returns>
        public async Task<IHttpActionResult> Post(Models.Product product)
        {
            // TODO: implement Post - route: product/
            // var result = new MultipleProductPost();
            // return Ok(result);
            return Ok();
        }

        /// <summary>
        /// Recurso para la consulta de un producto por id - /{id}
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MultipleProductIdGet</returns>
        public async Task<IHttpActionResult> Get([FromUri] string id)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var result = await this.Service.GetByIdAsync(Convert.ToInt32(id), token);
            return Ok(result);
        }

        /// <summary>
        /// Recurso para la consulta de productos por codigo - /byCode
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="type"></param>
        /// <returns>MultipleProductByCodeGet</returns>
        public async Task<IHttpActionResult> GetByCode([FromUri] string criteria, [FromUri] string type)
        {
            // TODO: implement GetByCode - route: product/byCode
            // var result = new MultipleProductByCodeGet();
            // return Ok(result);
            return Ok();
        }

        /// <summary>
        /// Recurso para la consulta de productos por nombre o por descripcion - /byCriteria
        /// </summary>
        /// <returns>MultipleProductByCriteriaGet</returns>
        public async Task<IHttpActionResult> GetByCriteria()
        {
            // TODO: implement GetByCriteria - route: product/byCriteria
            // var result = new MultipleProductByCriteriaGet();
            // return Ok(result);
            return Ok();
        }

        /// <summary>
        /// Recurso para la consulta del top 5 de productos - /topFive
        /// </summary>
        /// <returns>MultipleProductTopFiveGet</returns>
        public async Task<IHttpActionResult> GetTopFive()
        {
            // TODO: implement GetTopFive - route: product/topFive
            // var result = new MultipleProductTopFiveGet();
            // return Ok(result);
            return Ok();
        }

        public Task<IHttpActionResult> GetByCode([FromUri] string code)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> GetByCriteria([FromUri] string criteria)
        {
            throw new NotImplementedException();
        }

        public Task<IHttpActionResult> GetTopFive([FromUri] IList<string> ids)
        {
            throw new NotImplementedException();
        }
    }
}
