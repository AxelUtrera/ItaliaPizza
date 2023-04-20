//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
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
            this.supplierProduct = new HashSet<supplierProduct>();
            this.orders = new HashSet<orders>();
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
        public double quantity { get; set; }
    
        public virtual recipe recipe { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<supplierProduct> supplierProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<orders> orders { get; set; }
    }
}
