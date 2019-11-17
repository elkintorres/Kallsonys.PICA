using Kallsonys.PICA.Application.DTO.ProductDTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.IServices
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id, CancellationTokenSource token);

        Task<IList<Product>> GetByCodeAsync(string code, CancellationTokenSource token);

        Task<IList<Product>> GetByCriteriaAsync(string criteria, int pageCount, CancellationTokenSource token);

        Task<IList<Product>> GetTopFiveAsync(IList<Int32> topFive, CancellationTokenSource token);

        Task<int> CreateAsync(Product register, CancellationTokenSource token);
    }
}