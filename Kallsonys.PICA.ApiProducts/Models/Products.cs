// Template: Models (ApiModel.t4) version 3.0

// This code was generated by RAML Server Scaffolder

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Kallsonys.PICA.ApiProducts.Product.Models
{
    public partial class Products
    {
        

        /// <summary>
        /// Llave primaria
        /// </summary>
        [Range(0.00,double.MaxValue)]
        public int? Id { get; set; }
    } // end class

} // end Models namespace

