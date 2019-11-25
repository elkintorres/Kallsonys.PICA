using Kallsonys.PICA.Application.Adapters;
using Kallsonys.PICA.Application.DTO.OfferDTO;
using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository Repository;
        private readonly IImageService ServiceImage;

        public OfferService(IOfferRepository repository ,IImageService imageService)
        {
            this.Repository = repository;
            this.ServiceImage = imageService;
        }

        public async Task<Int32> CreateAsync(Offer offer, CancellationTokenSource token)
        {
            B2COffer newOffer = offer.AdapterOffer();
            newOffer.IsActive = true;
            var result = await Repository.CreateAsync(newOffer, token);
            if (offer.Image != null )
            {
                await ServiceImage.CreateAsync(offer.Image, offer.IdProduct, result, token);
            }

            return result;
        }
        public async Task<IEnumerable<Offer>> GetActiveOffers(int pageSize, int pageIndex, CancellationTokenSource token)
        {
            var result = await Repository.GetActiveOffers(pageSize, pageIndex, token);
            return result.AsQueryable().AdapterOffer();
        }

        public async Task<Offer> GetByIdAsync(int id, CancellationTokenSource token)
        {
            var register = await Repository.GetByKeyAsync(id, token);
            return register.AdapterOffer();
        }

        public async Task<int> GetCountAll(CancellationTokenSource token)
        {
            return await Repository.GetCountAll(token);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="pageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<IList<Offer>> GetByCriteriaAsync(string criteria, int pageSize, int PageIndex, CancellationTokenSource token)
        {
            var listOffer = await Repository.GetByCriteria(criteria, pageSize, PageIndex, token);
            return listOffer.AdapterOffer().ToList();
        }

        public async Task<bool> DisableById(int id, CancellationTokenSource token)
        {
            return await Repository.DisableById(id, token);
        }
    }
}