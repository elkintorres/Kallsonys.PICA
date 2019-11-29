using Kallsonys.PICA.Application.Adapters;
using Kallsonys.PICA.Application.DTO.ImageDTO;
using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Services
{
    public class ImageService : IImageService
    {
        private IImageRepository Repository { get; set; }
        public ImageService(IImageRepository repository)
        {
            this.Repository = repository;
        }
        public async Task<int> CreateAsync(IList<Image> registers, int idProduct, CancellationTokenSource token)
        {
            IList<B2CImage> newImages = registers.AdapterImage().ToList();

            foreach (var item in newImages)
            {
                item.IdProduct = idProduct;
            }

            var result = await Repository.CreateAsync(newImages.ToList(), token);
            return result;
        }

        public async Task<int> CreateAsync(Image register, int idProduct, int idOffer, CancellationTokenSource token)
        {
            B2CImage newImages = register.AdapterImage();

            newImages.IdProduct = idProduct;
            newImages.IdOffer = idOffer;

            var result = await Repository.CreateAsync(new List<B2CImage>() { newImages }, token);
            return result;
        }

        public async Task<int> UpdateAsync(IList<Image> images, CancellationTokenSource token)
        {
            IList<B2CImage> newImages = images.AdapterImage().ToList();


            var result = await Repository.UpdateAsync(newImages.ToList(), token);
            return result;
        }

        public async Task<int> UpdateAsync(Image image, CancellationTokenSource token)
        {
            B2CImage updateImage = image.AdapterImage();

            var result = await Repository.Update2Async(updateImage, token);
            return result;
        }
    }
}
