using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using Kallsonys.PICA.Infraestructure.Base;
using Kallsonys.PICA.Infraestructure.Model;

namespace Kallsonys.PICA.Infraestructure.Repositories
{
    public class ProductRepository : GenericRepository<Producto, int>, IProductRepository
    {
        public ProductRepository() : base(new Entities())
        { }
    }
}