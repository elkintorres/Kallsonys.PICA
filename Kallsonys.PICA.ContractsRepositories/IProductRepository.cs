using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Kallsonys.PICA.ContractsRepositories.Base;
using Kallsonys.PICA.Domain.Entities;

namespace Kallsonys.PICA.ContractsRepositories
{
    public interface IProductRepository : IGenericRepository<B2CProduct, int>
    {
        Task<IQueryable<B2CProduct>> GetByCodeAsync(string code, int pageSize, int pageIndex, CancellationTokenSource cancellationToken);
        Task<IQueryable<B2CProduct>> GetAllAsync(int pageSize, int pageIndex, CancellationTokenSource token);
        Task<IQueryable<B2CProduct>> GetByCriteria(string criteria, int pageSize, int pageIndex, CancellationTokenSource token);
        Task<IQueryable<B2CProduct>> GetAllByIdsAsync(IList<int> ids, CancellationTokenSource token);
        Task<Int32> GetCountAll(CancellationTokenSource token);
        Task<Boolean> DisableById(int id, CancellationTokenSource token);
        Task<Boolean> ExistAsync(int id, CancellationTokenSource token);
    }
}