﻿using Kallsonys.PICA.ContractsRepositories.Base;
using Kallsonys.PICA.Domain.Entities;

namespace Kallsonys.PICA.ContractsRepositories
{
    public interface IProductRepository : IGenericRepository<B2CProduct, int>
    {
    }
}