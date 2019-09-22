using Kallsonys.PICA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kallsonys.PICA.ApiProducts.Product.Models
{
    public static class ProductAdapterDTO
    {
        public static Models.Product Adpater(this Producto origin)
        {
            return new Product()
            {
                Code = origin.IdentificadorProducto.ToString(),
                Descripction = origin.Descripcion,
                Id = origin.IdProducto,
                IdCategory = origin.IdCategoria,
                IdProducer = (int)origin.IdProductor,
                ListPrice = (decimal)origin.PrecioDeLista,
                Name = origin.Nombre,
                ImgURL = string.Empty
            };
        }
    }
}
