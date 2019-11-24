﻿using Kallsonys.PICA.Application.Adapters;
using Kallsonys.PICA.Application.DTO.ProductDTO;
using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Kallsonys.PICA.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository Repository;
        private readonly IImageService ServiceImage;

        public ProductService(IProductRepository repository, IImageService imageService)
        {
            this.Repository = repository;
            this.ServiceImage = imageService;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="register"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<int> CreateAsync(Product register, CancellationTokenSource token)
        {
            B2CProduct newProduct = register.AdapterProduct();
            var result = await Repository.CreateAsync(newProduct, token);
            if(register.Images != null && register.Images.Any())
            {
                await ServiceImage.CreateAsync(register.Images, result, token);
            }
            return result;
        }

        public async Task<IList<Product>> GetByAll(int pageCount, int pageIndex, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetAllAsync(pageCount, pageIndex, token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<IList<Product>> GetByCodeAsync(string code, int pageSize, int pageIndex, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByCodeAsync(code,  pageSize, pageIndex, token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<IList<Product>> GetByCriteriaAsync(string criteria, int pageSize, int PageIndex, CancellationTokenSource token)
        {
            //int startPage = (pageSize * (PageIndex - 1)) + 1, endPage = startPage + pageSize;

            //var criteriaSql = criteria.Replace("*", "");
            //Expression<Func<B2CProduct, bool>> predecate = null;
            //if (criteria.StartsWith("*"))
            //{
            //    predecate = (x => x.Name.EndsWith(criteriaSql) || x.Description.EndsWith(criteriaSql) && x.IdProduct > startPage && x.IsActive == true);
            //}
            //else if (criteria.EndsWith("*"))
            //{
            //    predecate = (x => x.Name.StartsWith(criteriaSql) || x.Description.StartsWith(criteriaSql) && x.IdProduct > startPage && x.IsActive == true);
            //}
            //else
            //{
            //    predecate = (x => (x.Name.Contains(criteriaSql) || x.Description.Contains(criteriaSql)) && x.IdProduct > startPage && x.IsActive == true);
            //}

            var listProducts = await Repository.GetByCriteria(criteria, pageSize, PageIndex, token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<Product> GetByIdAsync(int id, CancellationTokenSource token)
        {
            var register = await Repository.GetByKeyAsync(id, token);

            return register.AdapterProduct();
        }

        public async Task<IList<Product>> GetAllByIdsAsync(IList<int> ids, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetAllByIdsAsync(ids, token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<int> GetCountAll(CancellationTokenSource token)
        {
            return await Repository.GetCountAll(token);
        }
    }
}