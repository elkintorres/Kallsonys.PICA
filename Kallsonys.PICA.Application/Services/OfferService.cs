using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.ContractsRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Services
{
    public class OfferService : IOfferService
    {
        private readonly IOfferRepository Repository;
        public OfferService(IOfferRepository repository)
        {
            this.Repository = repository;
        }
    }
}
