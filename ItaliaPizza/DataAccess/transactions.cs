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
    
    public partial class transactions
    {
        public int idTransaction { get; set; }
        public string reason { get; set; }
        public int idCashbox { get; set; }
        public string worker { get; set; }
        public int amount { get; set; }
    
        public virtual cashbox cashbox { get; set; }
        public virtual worker worker1 { get; set; }
    }
}