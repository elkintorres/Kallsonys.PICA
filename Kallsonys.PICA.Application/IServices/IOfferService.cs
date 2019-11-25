using Kallsonys.PICA.Application.DTO.OfferDTO;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.IServices
{
    public interface IOfferService
    {
        Task<Int32> CreateAsync(Offer offer, CancellationTokenSource token);

        Task<IEnumerable<Offer>> GetActiveOffers(int pageSize, int pageIndex, CancellationTokenSource token);

        Task<Offer> GetByIdAsync(int id, CancellationTokenSource token);

        Task<int> GetCountAll(CancellationTokenSource token);

        Task<IList<Offer>> GetByCriteriaAsync(string criteria, int pageSize, int PageIndex, CancellationTokenSource token);

        Task<Boolean> DisableById(int id, CancellationTokenSource token);
    }
}