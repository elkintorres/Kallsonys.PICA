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
    }
}
