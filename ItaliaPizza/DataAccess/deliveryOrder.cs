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
    
    public partial class deliveryOrder
    {
        public int idDeliveryOrder { get; set; }
        public int idOrder { get; set; }
        public int idCustomer { get; set; }
    
        public virtual customer customer { get; set; }
        public virtual customer customer1 { get; set; }
        public virtual orders orders { get; set; }
        public virtual orders orders1 { get; set; }
    }
}
