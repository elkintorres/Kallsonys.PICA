using System;
using System.IO;

namespace Kallsonys.PICA.Application.DTO.ImageDTO
{
    public class Image
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public bool IsThumbnail { get; set; } = false;
        public String Url { get; set; }
        public int IdProduct { get; set; }
        public int? IdOffer { get; set; }

    }
}