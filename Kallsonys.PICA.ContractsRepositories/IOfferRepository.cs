using Kallsonys.PICA.ContractsRepositories.Base;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.ContractsRepositories
{
    public interface IOfferRepository : IGenericRepository<B2COffer, int>
    {
        Task<IEnumerable<B2COffer>> GetActiveOffers(int pageSize, int pageIndex, CancellationTokenSource token);
        Task<int> GetCountAll(CancellationTokenSource token);
        Task<IQueryable<B2COffer>> GetByCriteria(string criteria, int pageSize, int pageIndex, CancellationTokenSource token);
        Task<Boolean> DisableById(int id, CancellationTokenSource token);
    }
}