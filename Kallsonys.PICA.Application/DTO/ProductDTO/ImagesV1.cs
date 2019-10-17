using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.DTO.ProductDTO
{
    public class Images
    {
        public string Description { get; set; }
        public String Name { get; set; }
        public bool IsThumbnail { get; set; } = false;
        public String Url { get; set; }
        public byte[] Image { get; set; }

        public int IdProduct { get; set; }

    }
}
