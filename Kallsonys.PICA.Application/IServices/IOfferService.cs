using Kallsonys.PICA.Application.DTO.OfferDTO;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.IServices
{
    public interface IOfferService
    {
        Task<Offer> CreateAsync(Offer offer, CancellationTokenSource token);

        Task<IEnumerable<Offer>> GetAsync(CancellationTokenSource token);

        Task<Offer> GetByIdAsync(int id, CancellationTokenSource token);
    }
}