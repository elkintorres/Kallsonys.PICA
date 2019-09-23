// Template: Controller Interface (ApiControllerInterface.t4) version 3.0

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Kallsonys.PICA.ApiProducts.ApiProduct.Models;


namespace Kallsonys.PICA.ApiProducts.ApiProduct
{
    public interface Iv1ProductController
    {

        Task<IHttpActionResult> Post(Models.Product product);
        Task<IHttpActionResult> Get([FromUri] int id);
        Task<IHttpActionResult> GetByCriteria([FromUri] string criteria);
        Task<IHttpActionResult> GetTopFive([FromUri] IList<string > topfiveproducts);
    }
}
