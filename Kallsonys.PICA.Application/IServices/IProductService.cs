using Kallsonys.PICA.ApiProducts.ApiProduct.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.IServices
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id, CancellationTokenSource token);
        Task<IList<Product>> GetByCodeAsync(string code, CancellationTokenSource token);
        Task<IList<Product>> GetByCriteriaAsync(string criteria, CancellationTokenSource token);
        Task<IList<Product>> GetTopFiveAsync(IList<Int32> topFive, CancellationTokenSource token);
        Task<Boolean> CreateAsync(Product register, CancellationTokenSource token);

    }
}
