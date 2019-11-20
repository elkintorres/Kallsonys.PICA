using Kallsonys.PICA.Application.DTO.ProductDTO;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Kallsonys.PICA.Application.Adapters
{
    public static class ProductAdapter
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static B2CProduct AdapterProduct(this Product source)
        {
            if (source == null)
                return null;
            else
                return new B2CProduct()
                {
                    Code = source.Code,
                    IdProduct = source.Id,
                    Name = source.Name,
                    Description = source.Description,
                    IdCategory = source.IdCategory,
                    IdProducer = source.IdProducer,
                    IdProvider = source.IdProvider,
                    ListPrice = source.ListPrice,
                    B2CImage = source.Images?.AsQueryable().AdapterImage().ToList()
                };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Product AdapterProduct(this B2CProduct source)
        {
            if (source == null)
                return null;
            else
                return new Product()
                {
                    Code = source.Code,
                    Id = source.IdProduct,
                    Name = source.Name,
                    Description = source.Description,
                    IdCategory = source.IdCategory,
                    IdProducer = source.IdProducer,
                    IdProvider = source.IdProvider,
                    ListPrice = source.ListPrice,
                    Images = source.B2CImage?.AsQueryable().AdapterImage().ToList()
                };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Product> AdapterProduct(this IQueryable<B2CProduct> source)
        {
            foreach (var item in source)
            {
                yield return item.AdapterProduct();
            }
        }
    }
}