﻿using Kallsonys.PICA.ContractsRepositories.Base;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.ContractsRepositories
{
    public interface IOfferRepository : IGenericRepository<B2COffer, int>
    {
        Task<IEnumerable<B2COffer>> GetActiveOffers(int pageSize, int pageIndex, CancellationTokenSource token);
    }
}