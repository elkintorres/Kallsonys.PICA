using System;
using System.IO;

namespace Kallsonys.PICA.Application.DTO.ImageDTO
{
    public class Image
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public String Name { get; set; }
        public bool IsThumbnail { get; set; } = false;
        public String Url { get; set; }
        public byte[] SourceImage { get; set; }
        public int IdProduct { get; set; }
        public int? IdOffer { get; set; }

        internal static Image FromStream(MemoryStream mStream)
        {
            throw new NotImplementedException();
        }
    }
}