﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kallsonys.PICA.Infraestructure.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Kallsonys.PICA.Domain.Entities;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<B2CCategory> B2CCategory { get; set; }
        public virtual DbSet<B2CImage> B2CImage { get; set; }
        public virtual DbSet<B2COffer> B2COffer { get; set; }
        public virtual DbSet<B2CProducer> B2CProducer { get; set; }
        public virtual DbSet<B2CProduct> B2CProduct { get; set; }
        public virtual DbSet<B2CProvider> B2CProvider { get; set; }
    }
}
