using Kallsonys.PICA.Application.DTO.ImageDTO;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Kallsonys.PICA.Application.Adapters
{
    public static class ImageAdapter
    {
        public static B2CImage AdapterImage(this Image source)
        {
            return new B2CImage()
            {
                IsThumbnail = source.IsThumbnail,
                IdImage = source.Id,
                IdProduct = source.IdProduct,
                IdOffer = source.IdOffer,
                Name = source.Name,
                Url = source.Url
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Image AdapterImage(this B2CImage source)
        {
            return new Image()
            {
                IsThumbnail = source.IsThumbnail,
                Id = source.IdImage,
                IdProduct = source.IdProduct,
                IdOffer = source.IdOffer,
                Name = source.Name,
                Url = source.Url
            };
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<Image> AdapterProduct(this IQueryable<B2CImage> source)
        {
            foreach (var item in source)
            {
                yield return item.AdapterImage();
            }
        }
    }
}