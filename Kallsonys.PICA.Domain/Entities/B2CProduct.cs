//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Kallsonys.PICA.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class B2CProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public B2CProduct()
        {
            this.B2CImage = new HashSet<B2CImage>();
            this.B2COffer = new HashSet<B2COffer>();
        }
    
        public int IdProduct { get; set; }
        public int IdProducer { get; set; }
        public int IdProvider { get; set; }
        public int IdCategory { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal ListPrice { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    
        public virtual B2CCategory B2CCategory { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<B2CImage> B2CImage { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<B2COffer> B2COffer { get; set; }
        public virtual B2CProducer B2CProducer { get; set; }
        public virtual B2CProvider B2CProvider { get; set; }
    }
}
