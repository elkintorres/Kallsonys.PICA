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
    
        public virtual DbSet<Campaña> Campaña { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Imagenes> Imagenes { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Productor> Productor { get; set; }
        public virtual DbSet<ProveedorMensjeria> ProveedorMensjeria { get; set; }
        public virtual DbSet<ProveedorProducto> ProveedorProducto { get; set; }
    }
}