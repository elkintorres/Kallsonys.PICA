﻿using Kallsonys.PICA.Application.Adapters;
using Kallsonys.PICA.Application.DTO.ProductDTO;
using Kallsonys.PICA.Application.IServices;
using Kallsonys.PICA.ContractsRepositories;
using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
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
            if(register.Images!= null)
            {
                IEnumerable<B2CImage> imagesProduct = ServiceImage.SaveMultipleImage(register.Images.ToList(), register.Code);
                newProduct.B2CImage = imagesProduct.ToList();
            }

            var result = await Repository.CreateAsync(newProduct, token);
            return result.IdProduct;
        }

        public async Task<IList<Product>> GetByCodeAsync(string code, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => x.Code == code, token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<IList<Product>> GetByCriteriaAsync(string criteria, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => x.Name.ToUpper().Contains(criteria.ToUpper()) || x.Description.ToUpper().Contains(criteria.ToUpper()), token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<Product> GetByIdAsync(int id, CancellationTokenSource token)
        {
            var register = await Repository.GetByKeyAsync(id, token);

            return register.AdapterProduct();
        }

        public async Task<IList<Product>> GetTopFiveAsync(IList<int> topFive, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => topFive.Contains(x.IdProduct), token);
            return listProducts.AdapterProduct().ToList();
        }
    }
}