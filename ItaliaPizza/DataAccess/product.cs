//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.orderProduct = new HashSet<orderProduct>();
            this.orderProduct1 = new HashSet<orderProduct>();
            this.supplierProduct = new HashSet<supplierProduct>();
            this.supplierProduct1 = new HashSet<supplierProduct>();
        }
    
        public string productCode { get; set; }
        public string description { get; set; }
        public byte[] picture { get; set; }
        public double price { get; set; }
        public bool preparation { get; set; }
        public string productName { get; set; }
        public string restrictions { get; set; }
        public int idRecipe { get; set; }
        public bool active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderProduct> orderProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orderProduct> orderProduct1 { get; set; }
        public virtual recipe recipe { get; set; }
        public virtual recipe recipe1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<supplierProduct> supplierProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<supplierProduct> supplierProduct1 { get; set; }
    }
}
