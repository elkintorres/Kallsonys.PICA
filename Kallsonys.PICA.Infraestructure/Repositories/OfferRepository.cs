using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using Kallsonys.PICA.Infraestructure.Base;
using Kallsonys.PICA.Infraestructure.Model;

namespace Kallsonys.PICA.Infraestructure.Repositories
{
    public class OfferRepository : GenericRepository<Campaña, int>, IOfferRepository
    {
        public OfferRepository() : base(new Entities())
        { }
    }
}