using AutoMapper;
using Kallsonys.PICA.ApiProducts.ApiProduct.Models;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Kallsonys.PICA.Application.Adapters
{
    public static class ProductAdapter
    {
        //https://csharp.hotexamples.com/examples/-/MapperConfiguration/-/php-mapperconfiguration-class-examples.html
        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Producto AdapterProduct(this Product source)
        {
            var config = new MapperConfiguration
                (
                    cfg =>
                    {
                        cfg.CreateMap<Product, Producto>()
                        .ForMember(orgin => orgin.IdentificadorProducto, destination => destination.MapFrom(m => m.Code))
                        .ForMember(orgin => orgin.IdProducto, destination => destination.MapFrom(m => m.Id));
                    }
                );

            var mapper = config.CreateMapper();
            return mapper.Map<Product, Producto>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Product AdapterProduct(this Producto source)
        {
            var config = new MapperConfiguration
                (
                    cfg =>
                    {
                        cfg.CreateMap<Producto, Product>()
                        .ForMember(orgin => orgin.Code, destination => destination.MapFrom(m => m.IdentificadorProducto));
                    }
                );

            var mapper = config.CreateMapper();
            return mapper.Map<Producto, Product>(source);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Product> AdapterProduct(this IQueryable<Producto> source)
        {
            foreach (var item in source)
            {
                yield return item.AdapterProduct();
            }
        }
    }
}
