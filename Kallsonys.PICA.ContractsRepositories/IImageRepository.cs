using Kallsonys.PICA.ContractsRepositories.Base;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.ContractsRepositories
{
    public interface IImageRepository : IGenericRepository<B2CImage, int>
    {
        Task<int> CreateAsync(IList<B2CImage> entity, CancellationTokenSource cancellationToken);
    }
}
