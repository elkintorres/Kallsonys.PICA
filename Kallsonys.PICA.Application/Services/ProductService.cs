using Kallsonys.PICA.Application.Adapters;
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
            //string baseURL = ConfigurationManager.AppSettings["StorageImages"];

            //var directory = $"{baseURL}{register.Code}";
            //if (Directory.Exists(directory))
            //    Directory.CreateDirectory(directory);

            //foreach (var item in register.Images)
            //{
            //    using (MemoryStream mStream = new MemoryStream(item.Image))
            //    {
            //        Image _image = Image.FromStream(mStream);
            //        var urlImage = $"{directory}//{item.Name}";
            //        _image.Save(urlImage);
            //        item.Url = urlImage;
            //    }
            //}

            Producto newProduct = register.AdapterProduct();
            var result = await Repository.CreateAsync(newProduct, token);
            return result.IdProducto > 0;
        }

        public async Task<IList<Product>> GetByCodeAsync(string code, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => x.IdentificadorProducto == code, token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<IList<Product>> GetByCriteriaAsync(string criteria, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => x.Nombre.ToUpper().Contains(criteria.ToUpper()) || x.Descripcion.ToUpper().Contains(criteria.ToUpper()), token);
            return listProducts.AdapterProduct().ToList();
        }

        public async Task<Product> GetByIdAsync(int id, CancellationTokenSource token)
        {
            var register = await Repository.GetByKeyAsync(id, token);

            return register.AdapterProduct();
        }

        public async Task<IList<Product>> GetTopFiveAsync(IList<int> topFive, CancellationTokenSource token)
        {
            var listProducts = await Repository.GetByExpressionAsync(x => topFive.Contains(x.IdProducto), token);
            return listProducts.AdapterProduct().ToList();
        }
    }
}