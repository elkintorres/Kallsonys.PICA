using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.IServices
{
    public interface IImageService
    {
        B2CImage SaveSingleImage(Kallsonys.PICA.Application.DTO.ImageDTO.Image source, string productCode);

        IEnumerable<B2CImage> SaveMultipleImage(IList<Kallsonys.PICA.Application.DTO.ImageDTO.Image> source, string productCode);
    }
}
