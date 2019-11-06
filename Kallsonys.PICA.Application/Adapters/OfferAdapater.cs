//using AutoMapper;
using Kallsonys.PICA.Application.DTO.OfferDTO;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Kallsonys.PICA.Application.Adapters
{
    public static class OfferAdapater
    {
        public static Campaña AdapterOffer(this Offer origin)
        {
            //var config = new MapperConfiguration
            //    (
            //        cfg =>
            //        {
            //            cfg.CreateMap<Offer, Campaña>()
            //            .ForMember(orgin => orgin.IdCamapaña, destination => destination.MapFrom(m => m.Id))
            //            .ForMember(orgin => orgin.IdProducto, destination => destination.MapFrom(m => m.IdProduct))
            //            .ForMember(orgin => orgin.Nombre, destination => destination.MapFrom(m => m.Name))
            //            .ForMember(orgin => orgin.Descripcion, destination => destination.MapFrom(m => m.Description))
            //            .ForMember(orgin => orgin.FechaIncio, destination => destination.MapFrom(m => m.BeginDate))
            //            .ForMember(orgin => orgin.FechaFin, destination => destination.MapFrom(m => m.EndDate))
            //            .ForMember(orgin => orgin.Descuento, destination => destination.MapFrom(m => m.Discount));
            //        }
            //    );
            //var mapper = config.CreateMapper();
            //return mapper.Map<Offer, Campaña>(source);

            return new Campaña()
            {
                IdCamapaña = origin.Id,
                IdProducto = origin.IdProduct,
                Nombre = origin.Name,
                Descripcion = origin.Description,
                FechaIncio = origin.BeginDate,
                FechaFin = origin.EndDate,
                Descuento= origin.Discount
            };
        }

        public static Offer AdapterOffer(this Campaña origin)
        {
            //var config = new MapperConfiguration
            //    (
            //        cfg =>
            //        {
            //            cfg.CreateMap<Campaña, Offer>()
            //            .ForMember(orgin => orgin.Id, destination => destination.MapFrom(m => m.IdCamapaña))
            //            .ForMember(orgin => orgin.IdProduct, destination => destination.MapFrom(m => m.IdProducto))
            //            .ForMember(orgin => orgin.Name, destination => destination.MapFrom(m => m.Nombre))
            //            .ForMember(orgin => orgin.Description, destination => destination.MapFrom(m => m.Descripcion))
            //            .ForMember(orgin => orgin.BeginDate, destination => destination.MapFrom(m => m.FechaIncio))
            //            .ForMember(orgin => orgin.EndDate, destination => destination.MapFrom(m => m.FechaFin))
            //            .ForMember(orgin => orgin.Discount, destination => destination.MapFrom(m => m.Descuento));
            //        }
            //    );
            //var mapper = config.CreateMapper();
            //return mapper.Map<Campaña, Offer>(source);

            return new Offer()
            {
                Id= origin.IdCamapaña,
                IdProduct = origin.IdProducto,
                Name = origin.Nombre,
                Description = origin.Descripcion,
                BeginDate = origin.FechaIncio,
                EndDate = origin.FechaFin,
                Discount = origin.Descuento
            };
        }

        public static IEnumerable<Offer> AdapterOffer(this IQueryable<Campaña> source)
        {
            foreach (var item in source)
            {
                yield return item.AdapterOffer();
            }
        }
    }
}