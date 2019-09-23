// Template: Models (ApiModel.t4) version 3.0

// This code was generated by RAML Server Scaffolder

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace Kallsonys.PICA.ApiProducts.ApiProduct.Models
{
    public partial class Product
    {
        

        [Required]
        [Range(0,int.MaxValue)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int IdCategoria { get; set; }

        [Required]
        public string ImageBigURL { get; set; }

        [Required]
        public string ImagaSmallURL { get; set; }
    } // end class

} // end Models namespace

