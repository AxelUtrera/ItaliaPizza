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
    
    public partial class supplierProduct
    {
        public int idSupplierProduct { get; set; }
        public string idSupplierOrder { get; set; }
        public string productCode { get; set; }
        public double quantity { get; set; }
    
        public virtual product product { get; set; }
        public virtual supplierOrder supplierOrder { get; set; }
    }
}
