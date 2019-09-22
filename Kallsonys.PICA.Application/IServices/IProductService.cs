using Kallsonys.PICA.ApiProducts.Product.Models;
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
        Task<MultipleProductIdGet> GetByIdAsync(int id, CancellationTokenSource token);
        Task<Object> GetByCodeAsync(Decimal code, CancellationTokenSource token);

        Task<Object> GetByCriteriaAsync(string criteria, CancellationTokenSource token);

        Task<Object> GetTopFiveAsync(IList<Int32> topFive, CancellationTokenSource token);

        Task<MultipleProductPost> CreateAsync(Product register, CancellationTokenSource token);

    }
}
