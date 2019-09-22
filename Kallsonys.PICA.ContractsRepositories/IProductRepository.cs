using Kallsonys.PICA.ContractsRepositories.Base;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.ContractsRepositories
{
    public interface IProductRepository : IGenericRepository<Producto, int>
    {
    }
}
