using System.ComponentModel.DataAnnotations;

namespace Kallsonys.PICA.Application.DTO.OfferDTO
{
    public partial class Offer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime BeginDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public System.DateTime EndDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public int IdProduct { get; set; }
    }
}