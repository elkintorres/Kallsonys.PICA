//using AutoMapper;
using Kallsonys.PICA.Application.DTO.OfferDTO;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Kallsonys.PICA.Application.Adapters
{
    public static class OfferAdapater
    {
        public static B2COffer AdapterOffer(this Offer origin)
        {
            if (origin == null)
                return null;
            else
                return new B2COffer()
                {
                    IdOffer = origin.Id,
                    IdProduct = origin.IdProduct,
                    Name = origin.Name,
                    Description = origin.Description,
                    BeginDate = origin.BeginDate,
                    EndDate = origin.EndDate,
                    Discount = origin.Discount,
                    IsActive = origin.IsActive,
                    B2CImage = new List<B2CImage>() { origin.Image?.AdapterImage() }
                };
        }

        public static Offer AdapterOffer(this B2COffer origin)
        {
            if (origin == null)
                return null;
            else
                return new Offer()
                {

                    Id = origin.IdOffer,
                    IdProduct = origin.IdProduct,
                    Name = origin.Name,
                    Description = origin.Description,
                    BeginDate = origin.BeginDate,
                    EndDate = origin.EndDate,
                    Discount = origin.Discount,
                    IsActive = origin.IsActive,
                    Image = origin.B2CImage?.AsQueryable().AdapterImage().First()
                };
        }

        public static IEnumerable<Offer> AdapterOffer(this IQueryable<B2COffer> source)
        {
            foreach (var item in source)
            {
                yield return item.AdapterOffer();
            }
        }

        public static IEnumerable<B2COffer> AdapterOffer(this IQueryable<Offer> source)
        {
            foreach (var item in source)
            {
                yield return item.AdapterOffer();
            }
        }
    }
}