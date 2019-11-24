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

        Task<IList<Product>> GetByCodeAsync(string code, int pageSize, int pageIndex,  CancellationTokenSource token);

        Task<IList<Product>> GetByCriteriaAsync(string criteria, int pageSize, int PageIndex, CancellationTokenSource token);

        Task<IList<Product>> GetAllByIdsAsync(IList<Int32> ids, CancellationTokenSource token);

        Task<int> CreateAsync(Product register, CancellationTokenSource token);

        Task<IList<Product>>GetByAll(int pageCount, int pageIndex, CancellationTokenSource token);

        Task<int> GetCountAll(CancellationTokenSource token);
    }
}