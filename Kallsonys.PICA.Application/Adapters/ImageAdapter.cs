using AutoMapper;
using Kallsonys.PICA.Application.DTO.ProductDTO;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Adapters
{
    public static class ImageAdapter
    {

        public static Imagenes AdapterImage(this Images source)
        {
            var config = new MapperConfiguration
                (
                    cfg =>
                    {
                        cfg.CreateMap<Images, Imagenes>()
                        .ForMember(orgin => orgin.Descripcion, destination => destination.MapFrom(m => m.Description))
                        .ForMember(orgin => orgin.IdProducto, destination => destination.MapFrom(m => m.IdProduct))
                        .ForMember(orgin => orgin.EsMiniatura, destination => destination.MapFrom(m => m.IsThumbnail))
                        .ForMember(orgin => orgin.Nombre, destination => destination.MapFrom(m => m.Name))
                        .ForMember(orgin => orgin.URL, destination => destination.MapFrom(m => m.Url));
                    }
                );

            var mapper = config.CreateMapper();
            return mapper.Map<Images, Imagenes>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Images AdapterImage(this Imagenes source)
        {
            var config = new MapperConfiguration
                (
                    cfg =>
                    {
                        cfg.CreateMap<Imagenes ,Images>()
                        .ForMember(orgin => orgin.Description, destination => destination.MapFrom(m => m.Descripcion))
                        .ForMember(orgin => orgin.IdProduct, destination => destination.MapFrom(m => m.IdProducto))
                        .ForMember(orgin => orgin.IsThumbnail, destination => destination.MapFrom(m => m.EsMiniatura))
                        .ForMember(orgin => orgin.Name, destination => destination.MapFrom(m => m.Nombre))
                        .ForMember(orgin => orgin.Url, destination => destination.MapFrom(m => m.URL));
                    }
                );

            var mapper = config.CreateMapper();
            return mapper.Map<Imagenes, Images>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Images> AdapterProduct(this IQueryable<Imagenes> source)
        {
            foreach (var item in source)
            {
                yield return item.AdapterImage();
            }
        }
    }
}
