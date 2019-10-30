using Kallsonys.PICA.Application.DTO.CommonDTO;
using System.Collections.Generic;

namespace Kallsonys.PICA.Application.DTO.OfferDTO
{
    public class MultipleOfferGet
    {
        public IEnumerable<Offer> Offers { get; set; }
        public Error Error { get; set; }
    }
}