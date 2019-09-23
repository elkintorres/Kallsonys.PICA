// Template: Controller Implementation (ApiControllerImplementation.t4) version 3.0

using Kallsonys.PICA.ApiProducts.ApiProduct.Models;
using Kallsonys.PICA.Application.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kallsonys.PICA.ApiProducts.ApiProduct
{
    public partial class v1ProductController : Iv1ProductController
    {
        private readonly IProductService Service;
        public v1ProductController(IProductService service)
        {
            this.Service = service;
        }

        /// <summary>
        /// Recurso para la creacion de productos - /Product
        /// </summary>
        /// <param name="product"></param>
        /// <returns>MultipleProductPost</returns>
        public async Task<IHttpActionResult> Post(Models.Product product)
        {
            CancellationTokenSource token = new CancellationTokenSource();
            var result = await this.Service.CreateAsync(product, token);
            MultipleProductPost response = new MultipleProductPost()
            {
                Error = null,
                Ipbool = result
            };
            return Ok(response);
        }

        /// <summary>
        /// Recurso para la consulta de un producto por id - /id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>MultipleProductIdGet</returns>
        public async Task<IHttpActionResult> Get([FromUri] int id)
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
        public async Task<IHttpActionResult> GetByCriteria([FromUri] string criteria)
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
        public async Task<IHttpActionResult> GetTopFive([FromUri] IList<string> topfiveproducts)
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
