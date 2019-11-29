using Kallsonys.PICA.Application.DTO.ImageDTO;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.IServices
{
    public interface IImageService
    {
        Task<int> CreateAsync(IList<Image> register, int idProduct,  CancellationTokenSource token);

        Task<int> CreateAsync(Image register, int idProduct, int idOffer, CancellationTokenSource token);
        Task <int> UpdateAsync(IList<Image> images, CancellationTokenSource token);
        Task <int> UpdateAsync(Image image, CancellationTokenSource token);
    }
}
