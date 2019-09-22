// Template: Controller Interface (ApiControllerInterface.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Kallsonys.PICA.ApiProducts.Product.Models;


namespace Kallsonys.PICA.ApiProducts.Product
{
    public interface IProductController
    {

        Task<IHttpActionResult> Post(Models.Product product);
        Task<IHttpActionResult> Get([FromUri] string id);
        Task<IHttpActionResult> GetByCode([FromUri] string code);
        Task<IHttpActionResult> GetByCriteria([FromUri] string criteria);
        Task<IHttpActionResult> GetTopFive([FromUri] IList<string> ids);
    }
}
