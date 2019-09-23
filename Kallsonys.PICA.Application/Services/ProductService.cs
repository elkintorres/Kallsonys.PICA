using Kallsonys.PICA.ApiProducts.ApiProduct.Models;
using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.CrossCutting.Configuration.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository Repository;
        public ProductService(IProductRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="register"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<Boolean> CreateAsync(Product register, CancellationTokenSource token)
        {
            var result = await Repository.CreateAsync(new Domain.Entities.Producto(), token);
            return result.IdProducto > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<object> GetByCodeAsync(Decimal code, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => x.IdentificadorProducto == code, token);
            return listProducts.ToList();
        }

        public Task<IList<Product>> GetByCodeAsync(string code, CancellationTokenSource token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<object> GetByCriteriaAsync(string criteria, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => x.Nombre.ToUpper().Contains(criteria.ToUpper()) || x.Descripcion.ToUpper().Contains(criteria.ToUpper()), token);
            return listProducts.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<MultipleProductIdGet> GetByIdAsync(int id, CancellationTokenSource token)
        {
            MultipleProductIdGet result = new MultipleProductIdGet();
            var register = await Repository.GetByKeyAsync(id, token);

            if (register == null)
            {
                result.Product = null;
                result.Error = new Error()
                {
                    Code = Convert.ToInt32(MessagesApplication_Product.NotFoundCode),
                    Description = String.Format(MessagesApplication_Product.NotFoundMessage, id)
                };
            }
            else
            {
               // result.Product = register.Adpater();
                result.Error = null;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topFive"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<object> GetTopFiveAsync(IList<int> topFive, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => topFive.Contains(x.IdProducto), token);
            return listProducts.ToList();
        }

        Task<IList<Product>> IProductService.GetByCriteriaAsync(string criteria, CancellationTokenSource token)
        {
            throw new NotImplementedException();
        }

        Task<Product> IProductService.GetByIdAsync(int id, CancellationTokenSource token)
        {
            throw new NotImplementedException();
        }

        Task<IList<Product>> IProductService.GetTopFiveAsync(IList<int> topFive, CancellationTokenSource token)
        {
            throw new NotImplementedException();
        }
    }
}
