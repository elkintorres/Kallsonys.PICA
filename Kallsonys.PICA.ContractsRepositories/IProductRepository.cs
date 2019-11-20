using System;
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
        Task<IQueryable<B2CProduct>> GetByCodeAsync(string code, CancellationTokenSource cancellationToken);
        Task<IQueryable<B2CProduct>> GetByExpressionAsync(Expression<Func<B2CProduct, bool>> predecate, int pageCount, CancellationTokenSource token);
        Task<IQueryable<B2CProduct>> GetAllAsync(int pageCount, int pageIndex, CancellationTokenSource token);
    }
}