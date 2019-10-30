using Kallsonys.PICA.Application.Adapters;
using Kallsonys.PICA.Application.DTO.OfferDTO;
using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository Repository;

        public OfferService(IOfferRepository repository)
        {
            this.Repository = repository;
        }

        public async Task<Offer> CreateAsync(Offer offer, CancellationTokenSource token)
        {
            Campaña newOffer = offer.AdapterOffer();
            var result = await Repository.CreateAsync(newOffer, token);
            return result.AdapterOffer();
        }

        public async Task<IEnumerable<Offer>> GetAsync(CancellationTokenSource token)
        {
            var result = await Repository.GetByExpressionAsync(x => x.FechaFin <= System.DateTime.Now, token);
            return result.AdapterOffer();
        }

        public async Task<Offer> GetByIdAsync(int id, CancellationTokenSource token)
        {
            var register = await Repository.GetByKeyAsync(id, token);
            return register.AdapterOffer();
        }
    }
}