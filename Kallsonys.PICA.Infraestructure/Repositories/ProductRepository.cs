using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using Kallsonys.PICA.Infraestructure.Base;
using Kallsonys.PICA.Infraestructure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Infraestructure.Repositories
{
    public class ProductRepository : GenericRepository<Producto, int>, IProductRepository
    {
        public ProductRepository() : base(new Entities())
        { }
    }
}
