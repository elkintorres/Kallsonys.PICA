using Kallsonys.PICA.Application.Adapters;
using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Services
{
    public class ImageService : IImageService
    {
        public B2CImage SaveSingleImage(Kallsonys.PICA.Application.DTO.ImageDTO.Image source, string productCode)
        {
            string baseURL = ConfigurationManager.AppSettings["StorageImages"];

            var directory = $"{baseURL}{productCode}";
            if (Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (MemoryStream mStream = new MemoryStream(source.SourceImage))
            {
                Image _image = Image.FromStream(mStream);
                var urlImage = $"{directory}//{source.Name}";
                _image.Save(urlImage);
                source.Url = urlImage;
            }

            return source.AdapterImage();
        }

        public IEnumerable<B2CImage> SaveMultipleImage(IList<Kallsonys.PICA.Application.DTO.ImageDTO.Image> source, string productCode)
        {
            foreach (var item in source)
            {
                yield return SaveSingleImage(item, productCode);
            }
        }
    }
}
